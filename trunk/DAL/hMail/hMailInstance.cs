/*
Copyright 2009 Andrew Miller
Distributed under the terms of the GNU General Public License
*/
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using anmar.SharpMimeTools;
using hMailServer;

namespace hMail
{
    /// <summary>
    ///
    /// </summary>
    public class hMailInstance
    {
        private string adminName = ConfigurationManager.AppSettings["adminname"];
        private string adminPass = ConfigurationManager.AppSettings["adminpass"];
        private string domainName = ConfigurationManager.AppSettings["hmaildomain"];
        internal ApplicationClass projApplication;
        internal DomainClass projDomain;
        internal AccountClass userAccount;
        private string root;

        /// <summary>
        ///constructor loads an existing hMail account
        /// </summary>
        public hMailInstance(string accountName, string dirRoot)
        {
            projApplication = new ApplicationClass();
            projApplication.Authenticate(adminName, adminPass); //authenticate to our local mail server
            projDomain = (DomainClass)projApplication.Domains.get_ItemByName(domainName); //load the server domain
            userAccount = (AccountClass)projDomain.Accounts.get_ItemByAddress(accountName); //load the user by hMail address
            this.root = dirRoot;
        }

        /// <summary>
        ///overloaded constructor adds a new account...
        /// </summary>
        public hMailInstance(string accountName, string userPass, string dirRoot)
        {
            projApplication = new ApplicationClass();
            projApplication.Authenticate(adminName, adminPass); //authenticate to our local mail server
            projDomain = (DomainClass)projApplication.Domains.get_ItemByName(domainName); //load the server domain
            userAccount = (AccountClass)projDomain.Accounts.Add();
            userAccount.Address = accountName;
            userAccount.Password = userPass;
            userAccount.Active = true;
            userAccount.Save();
            FolderSetUp();
            this.root = dirRoot;
        }

        /// <summary>
        ///return this account instance
        /// </summary>
        public Account GetAccount()
        {
            return this.userAccount;
        }

        /// <summary>
        ///save this account instance
        /// </summary>
        public void Save()
        {
            userAccount.Save();
        }

        //this section handles hMail Server account settings
        #region hMail Server Account Settings
        /// <summary>
        ///validate the user's password
        /// </summary>
        public bool PasswordCorrect(string userPassword)
        {
            return userAccount.ValidatePassword(userPassword);
        }

        /// <summary>
        ///enable the vacation mesage
        /// </summary>
        public bool VacationMessageEnabled
        {
            get { return userAccount.VacationMessageIsOn; }
            set { userAccount.VacationMessageIsOn = value; }
        }

        /// <summary>
        ///get/set vacation message subject
        /// </summary>
        public string VacationMessageSubject
        {
            get { return userAccount.VacationSubject; }
            set { userAccount.VacationSubject = value; }
        }

        /// <summary>
        ///get/set vacation message text
        /// </summary>
        public string VacationMessageText
        {
            get { return userAccount.VacationMessage; }
            set { userAccount.VacationMessage = value; }
        }

        /// <summary>
        ///enable mail forwarding
        /// </summary>
        public bool ForwardEnabled
        {
            get { return userAccount.ForwardEnabled; }
            set { userAccount.ForwardEnabled = value; }
        }

        /// <summary>
        ///get/set forward keeps original
        /// </summary>
        public bool ForwardKeepsOriginal
        {
            get { return userAccount.ForwardKeepOriginal; }
            set { userAccount.ForwardKeepOriginal = value; }
        }

        /// <summary>
        ///get/set forwarding address
        /// </summary>
        public string ForwardAddress
        {
            get { return userAccount.ForwardAddress; }
            set { userAccount.ForwardAddress = value; }
        }

        /// <summary>
        ///get/set status of this account instance
        /// </summary>
        public bool Active
        {
            get { return userAccount.Active; }
            set { userAccount.Active = value; }
        }

        /// <summary>
        ///get/set this account max size
        /// </summary>
        public int MaxSize
        {
            get { return userAccount.MaxSize; }
            set { userAccount.MaxSize = value; }
        }

        /// <summary>
        ///get size of this account instance
        /// </summary>
        public float Size
        {
            get { return userAccount.Size; }
        }

        /// <summary>
        ///get value of this account quota used
        /// </summary>
        public int QuotaUsed
        {
            get { return userAccount.QuotaUsed; }
        }
        #endregion

        /*this section handles hMail Server IMAP folder 
        NOTE: the hMail Server API handles IMAP folder generation/manipulation
        but does not appear to efficiently handle moving mails between folders!
        because of this limitation, the methods are not used in this implementation
        I left them in here for future use if anyone wants to dig into the 
        hMail Server source code and determine the feasiblity of moving 
        messages around... or for anyone who has better knowledge of how to do this 
        with the hMail API.
        In particular: hMailMessage.Copy(toFolder) will not work with drafts or other 
        saved message types because the hMailMessage.save() will send the mail. 
        Also, the copy() method requires extra housekeeping for managing messages, and 
        may require direct database manipulation to persist a copied messages state data.
        Ideally we would like a new method like move(), that would handle moving an existing 
        message to a different directory, and updating the database to reflect the message's
        current directory. Additionally, we would like save() and send() to be two different 
        methods. 
        */
        #region hMail Server IMAP Folders
        /// <summary>
        ///add hMail Server IMAP folders to this account instance
        /// </summary>
        private void FolderSetUp()
        {
            userAccount.IMAPFolders.Add("DELETED");
            userAccount.IMAPFolders.Add("JUNK");
        }

        /// <summary>
        ///retrieve an hMail Server IMAP folder by id
        /// </summary>
        private IMAPFolder FolderGetByID(int folderid, IMAPFolders folders)
        {
            for (int i = 0; i < folders.Count; i++)
            {
                if (folders[i].ID == folderid)
                    return folders[i];
                else
                    FolderGetByID(folderid, folders[i].SubFolders);
            }
            return null;
        }

        /// <summary>
        ///retrieve an hMail Server IMAP folder by name and root
        ///--userAccount.IMAPFolders.get_ItemByName(folderName);
        /// </summary>
        private IMAPFolder FolderGetByName(string folderName, IMAPFolders folders)
        {
            for (int i = 0; i < folders.Count; i++)
            {
                if (folders[i].Name == folderName.ToUpper())
                    return folders[i];
                else
                    FolderGetByName(folderName, folders[i].SubFolders);
            }
            return null;
        }

        /// <summary>
        ///retrieve an hMail Server IMAP folder id
        /// </summary>
        private int FolderGetID(string folderName, IMAPFolders folders)
        {
            for (int i = 0; i < folders.Count; i++)
            {
                if (folders[i].Name == folderName.ToUpper())
                    return folders[i].ID;
                else
                    FolderGetID(folderName, folders[i].SubFolders);
            }
            return -1;
        }

        /// <summary>
        ///add a new hMail Server IMAP folder to a parent folder
        /// </summary>
        public void FolderAdd(string name, string parentName)
        {
            if (parentName == null)
                userAccount.IMAPFolders.Add(name);
            else
            {
                IMAPFolder parent = FolderGetByName(parentName, userAccount.IMAPFolders);
                if (parent != null)
                    parent.SubFolders.Add(name);
            }
        }

        /// <summary>
        ///delete an hMail Server IMAP folder
        /// </summary>
        public bool FolderDelete(string folderName)
        {
            int folderID = FolderGetID(folderName, userAccount.IMAPFolders);
            if (folderID >= 0)
            {
                userAccount.IMAPFolders.DeleteByDBID(folderID);
                return true;
            }
            return false;
        }

        /// <summary>
        ///retrieve all hMail Server IMAP folders for this account
        /// </summary>
        public List<IMAPFolder> FoldersGetAll()
        {
            List<IMAPFolder> folders = new List<IMAPFolder>();
            for (int i = 0; i < userAccount.IMAPFolders.Count; i++)
            {
                folders.Add(userAccount.IMAPFolders[i]);
            }
            if (folders.Count > 0)
                return folders;
            return null;
        }

        /// <summary>
        ///retrieve all hMail Server IMAP folders for a given parent folder
        /// </summary>
        public List<IMAPFolder> SubFoldersGetAll(IMAPFolder folder)
        {
            List<IMAPFolder> subfolders = new List<IMAPFolder>();
            for (int i = 0; i < folder.SubFolders.Count; i++)
            {
                subfolders.Add(folder.SubFolders[i]);
            }
            if (subfolders.Count > 0)
                return subfolders;
            return null;
        }
        #endregion

        //this section handles messages from the inbox
        #region hMail Messages 
        /// <summary>
        ///return all hMail Messages for a given hMail IMAP folder
        /// </summary>
        public List<hMailServer.Message> MessagesGetAll(MailType folderName)
        {
            List<hMailServer.Message> messages = new List<hMailServer.Message>();
            IMAPFolder folder = FolderGetByName(folderName.ToString(), userAccount.IMAPFolders);
            if (folder != null)
                for (int i = 0; i < folder.Messages.Count; i++)
                {
                    messages.Add(folder.Messages[i]);
                }
            if (messages.Count > 0)
                return messages;
            return null;
        }

        /// <summary>
        ///the hMailMessage class is a liteweight version of an 
        ///     this allows data about messages to be passsed to the 
        ///     UI without sending the entire mail message
        /// </summary>
        public List<hMailMessage> hMailMessagesGetAll(MailType folderName)
        {
            if (folderName == MailType.inbox)
            {
                List<hMailMessage> messages = new List<hMailMessage>();
                IMAPFolder folder = FolderGetByName(folderName.ToString(), userAccount.IMAPFolders);
                if (folder != null)
                    for (int j = 0; j < folder.Messages.Count; j++)
                        messages.Add(new hMailMessage(folder.Messages[j]));
                if (messages.Count > 0)
                    return messages;
            }
            return null;
        }

        /// <summary>
        ///retrieve an hMail Message by id
        ///     NOTE: the userAccount.Messages.get_ItemByDBID(id) 
        ///     does not appear to work in real-time. That is, if
        ///     I send a mail to myself, the mail will show up in the 
        ///     inbox folder, but the message is not retrievable 
        ///     through the above method call until logout/login !
        /// </summary>
        public hMailServer.Message hMailMessageGetByID(int id)
        {
            //the following method call doesn't seem to work without logging out
            //return userAccount.Messages.get_ItemByDBID(id);
            IMAPFolder folder = FolderGetByName("INBOX", userAccount.IMAPFolders);
            if (folder != null)
            {
                for (int j = 0; j < folder.Messages.Count; j++)
                {
                    if (folder.Messages[j].ID == id)
                        return folder.Messages[j];
                }
            }
            return null;
        }

        /// <summary>
        ///return a .NET MailMessage object by the hMail Message id
        /// </summary>
        public MailMessage MailMessageGetByID(int id)
        {
            //this is not working without logout/login -!
            //return hMailConvert(userAccount.Messages.get_ItemByDBID(id));
            IMAPFolder folder = FolderGetByName("INBOX", userAccount.IMAPFolders);
            if (folder != null)
            {
                for (int j = 0; j < folder.Messages.Count; j++)
                {
                    if (folder.Messages[j].ID == id)
                        return hMailConvert(folder.Messages[j]);
                }
            }
            return null;
        }

        /// <summary>
        ///move an hMail Message to a destination folder based by id
        /// </summary>
        public string MessageMove(int id, MailType dest)
        {
            hMailServer.Message msg = hMailMessageGetByID(id);
            MailMessage mm = null;
            try
            {
                mm = hMailConvert(msg);
                MessageFileWrite(mm, dest);
                userAccount.Messages.DeleteByDBID(msg.ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (mm != null)
                    mm.Dispose();
            }
            return MessageFileGetByMailType(dest);
        }


        /// <summary>
        ///delete an hMail Message from the inbox
        ///     this method will transfer the message to the 
        ///     deleted items folder before deleting from the inbox
        /// </summary>
        public void MessageDelete(int id, MailType folderType)
        {
            hMailServer.Message msg = hMailMessageGetByID(id);
            MessageMove(msg.ID, MailType.deleted);
            msg.set_Flag(eMessageFlag.eMFDeleted, true);
            userAccount.Messages.DeleteByDBID(id);
            userAccount.Save();
        }

        /// <summary>
        ///send an hMail Message from hMail Server from a .NET MailMessage object
        /// </summary>
        public string SendMail(MailMessage msg, MailType type)
        {
            hMailServer.Message outMsg = new hMailServer.Message();
            outMsg.FromAddress = userAccount.Address;
            outMsg.From = msg.From.Address;
            ArrayList to = new ArrayList();
            try
            {
                foreach (MailAddress toAddr in msg.To)
                {
                    to.Add(toAddr.Address);
                }
                outMsg.set_HeaderValue("To", RecipientsAdd(to, outMsg));
                if (msg.CC.Count > 0)
                {
                    ArrayList cc = new ArrayList();
                    foreach (MailAddress ccAddr in msg.CC)
                    {
                        cc.Add(ccAddr.Address);
                    }
                    outMsg.set_HeaderValue("CC", RecipientsAdd(cc, outMsg));
                }
                if (msg.Bcc.Count > 0)
                {
                    ArrayList bcc = new ArrayList();
                    foreach (MailAddress bccAddr in msg.Bcc)
                    {
                        bcc.Add(bccAddr.Address);
                    }
                    outMsg.set_HeaderValue("BCC", RecipientsAdd(bcc, outMsg));
                }
                outMsg.Subject = Utility.Sanitize(msg.Subject);
                if (msg.IsBodyHtml)
                {
                    outMsg.HasBodyType("text/html");
                    outMsg.HTMLBody = Utility.Sanitize(msg.Body);
                }
                else
                {
                    outMsg.HasBodyType("text/plain");
                    outMsg.Body = Utility.ClearTags(msg.Body);
                }
                AttachmentsAdd(outMsg);
                if (type.Equals(MailType.replied))
                    outMsg.set_Flag(eMessageFlag.eMFAnswered, true);
                MessageSave(msg, MailType.sent);
                outMsg.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (msg != null)
                    msg.Dispose();
            }
            return MessageFileGetByMailType(MailType.sent);
        }

        /// <summary>
        ///add recipients to an hMail Message
        /// </summary>
        private string RecipientsAdd(ArrayList recipients, hMailServer.Message msg)
        {
            string outStr = "";
            foreach (string str in recipients)
            {
                if (str.Length > 0)
                {
                    msg.AddRecipient("", str);
                    outStr += "<" + str + ">, ";
                }
            }
            if (outStr.Length > 0)
                outStr = outStr.Substring(0, outStr.Length - 2);
            return outStr;
        }

        /// <summary>
        ///add attachments to an hMail Message 
        ///     this will gather all files from the account owner's 
        ///     temp file and add them to the outgoing message as attachments
        /// </summary>
        private void AttachmentsAdd(hMailServer.Message msg)
        {
            DirectoryInfo dinfo = new DirectoryInfo(this.root + @"temp\");
            FileInfo[] files = dinfo.GetFiles();
            if (files.Length > 0)
                foreach (FileInfo file in files)
                {
                    msg.Attachments.Add(file.FullName);
                }
        }

        /// <summary>
        ///set an hMail Message as seen
        /// </summary>
        public void MessageSetSeen(hMailServer.Message msg)
        {
            msg.set_Flag(eMessageFlag.eMFSeen, true);
        }

        /// <summary>
        ///set an hMail Message as answered
        /// </summary>
        public void MessageSetAnswered(hMailServer.Message msg)
        {
            msg.set_Flag(eMessageFlag.eMFAnswered, true);
        }

        /// <summary>
        ///set an hMail Message as draft
        /// </summary>
        public void MessageSetDraft(hMailServer.Message msg)
        {
            msg.set_Flag(eMessageFlag.eMFDraft, true);
        }

        /// <summary>
        ///convert an hMail Message to a .NET MailMessage object
        /// </summary>
        private MailMessage hMailConvert(hMailServer.Message msg)
        {
            MailMessage mm = new MailMessage();
            if (!string.IsNullOrEmpty(msg.From))
            {
                MailAddress from = new MailAddress(msg.From);
                mm.From = from;
            }
            mm.Subject = msg.Subject;
            if (!string.IsNullOrEmpty(msg.Body))
            {
                mm.Body = Utility.ClearTags(msg.Body);
                mm.IsBodyHtml = false;
            }
            else
            {
                mm.Body = Utility.Sanitize(msg.HTMLBody);
                mm.IsBodyHtml = true;
            }
            string[] to = msg.To.Split(';');
            if (!string.IsNullOrEmpty(to[0]))
            {
                foreach (string toAddr in to)
                {
                    MailAddress addr = new MailAddress(toAddr);
                    mm.To.Add(addr);
                }
            }
            if (msg.CC.Length > 0)
            {
                string[] cc = msg.CC.Split(';');
                if (!string.IsNullOrEmpty(cc[0]))
                {
                    foreach (string ccAddr in cc)
                    {
                        MailAddress addr = new MailAddress(ccAddr);
                        mm.CC.Add(addr);
                    }
                }
            }
            AttachmentsSet(msg, mm);
            return mm;
        }

        /// <summary>
        ///convert hMail Message attachments to .NET MailMessage attachments
        /// </summary>
        private void AttachmentsSet(hMailServer.Message msg, MailMessage mm)
        {
            TempClear();
            for (int i = 0; i < msg.Attachments.Count; i++)
            {
                AttachmentClass attach = (AttachmentClass)msg.Attachments[i];
                string outPath = this.root + @"temp\" + attach.Filename;
                FileInfo finfo = new FileInfo(outPath);
                attach.SaveAs(outPath);
                System.Net.Mail.Attachment att;
                switch (finfo.Extension)
                {
                    case ".pdf":
                        att = new System.Net.Mail.Attachment(outPath, MediaTypeNames.Application.Pdf);
                        break;
                    case ".rtf":
                        att = new System.Net.Mail.Attachment(outPath, MediaTypeNames.Application.Rtf);
                        break;
                    case ".zip":
                        att = new System.Net.Mail.Attachment(outPath, MediaTypeNames.Application.Zip);
                        break;
                    default:
                        att = new System.Net.Mail.Attachment(outPath, MediaTypeNames.Application.Octet);
                        break;
                }
                mm.Attachments.Add(att);
            }
        }
        #endregion

        //this section handles AceMail managed messages
        #region AceMail Messages
        /// <summary>
        ///return a .NET MailMessage from a file
        /// </summary>
        public MailMessage MailMessageGetByGuid(string messageGuid, MailType messageType)
        {
            TempClear();
            FileInfo finfo = null;
            finfo = new FileInfo(this.root + @"mail\" + messageType.ToString() + @"\" + messageGuid);
            if (finfo != null)
            {
                MailMessage msg = MessageFileParse(finfo);
                return msg;
            }
            return null;
        }

        /// <summary>
        ///send a AceMail message 
        /// </summary>
        public void SendMail(string name, MailType type)
        {
            MailMessage msg = null;
            try
            {
                msg = MailMessageGetByGuid(name, type);
                SendMail(msg, MailType.sending);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (msg != null)
                    msg.Dispose();
            }
        }

        /// <summary>
        ///save a AceMail message to a dest folder
        /// </summary>
        public string MessageSave(MailMessage msg, MailType dest)
        {
            MessageFileWrite(msg, dest);
            return MessageFileGetByMailType(dest);
        }

        /// <summary>
        ///move a AceMail message from one folder to another
        /// </summary>
        public void MessageMove(string fileName, MailType src, MailType dest)
        {
            FileInfo finfo = new FileInfo(this.root + src.ToString() + @"\" + fileName);
            if (System.IO.File.Exists(finfo.FullName))
                finfo.MoveTo(this.root + dest.ToString() + @"\" + fileName);
        }

        /// <summary>
        ///delete a AceMail message from a given folder
        /// </summary>
        public void MessageDelete(string fileName, MailType folderName)
        {
            if (folderName.Equals("deleted"))
            {
                FileInfo finfo = new FileInfo(this.root + folderName.ToString() + @"\" + fileName);
                if (File.Exists(finfo.FullName))
                    finfo.Delete();
            }
            else
                MessageMove(fileName, folderName, MailType.deleted);
        }

        /// <summary>
        ///write a .NET MailMessage object to file
        /// </summary>
        private void MessageFileWrite(MailMessage msg, MailType dest)
        {
            SmtpClient smtp = new SmtpClient(userAccount.ADDomain);
            smtp.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
            string path = "";
            switch (dest)
            {
                case MailType.drafts:
                     path = this.root + @"mail\drafts\";
                    break;
                case MailType.events:
                    path = this.root + @"mail\events\";
                    break;
                case MailType.storage:
                    path = this.root + @"mail\storage\";
                    break;
                case MailType.sent:
                    path = this.root + @"mail\sent\";
                    break;
                case MailType.deleted:
                    path = this.root + @"mail\deleted\";
                    break;
                default:
                    break;
            }
            try
            {
                smtp.PickupDirectoryLocation = path;
                smtp.Send(msg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (msg != null)
                    msg.Dispose();
            }
        }

        /// <summary>
        ///find the most recently added AceMail message in a folder
        /// </summary>
        private string MessageFileGetByMailType(MailType dest)
        {
            string path = "";
            switch (dest)
            {
                case MailType.drafts:
                    path = "drafts";
                    break;
                case MailType.events:
                    path = "events";
                    break;
                case MailType.sent:
                    path = "sent";
                    break;
                case MailType.storage:
                    path = "storage";
                    break;
                case MailType.deleted:
                    path = "deleted";
                    break;
                default:
                    break;
            }
            DirectoryInfo dinfo = new DirectoryInfo(this.root + @"mail\" + path);
            FileInfo[] files = dinfo.GetFiles();
            bool first = true;
            FileInfo temp = null;
            foreach (FileInfo nfinfo in dinfo.GetFiles())
            {
                if (first)
                {
                    temp = nfinfo;
                    first = false;
                }
                else if (temp.CreationTime < nfinfo.CreationTime)
                    temp = nfinfo;
            }
            return temp.Name;
        }

        /// <summary>
        ///extract a .NET MailMessage attachments to the account's temp folder
        /// </summary>
        private void AttachmentsSet(MailMessage mm)
        {
            TempClear();
            int size = 1024;
            if (mm.Attachments.Count > 0)
            {
                foreach (System.Net.Mail.Attachment att in mm.Attachments)
                {
                    System.IO.Stream input = null;
                    FileStream output = null;
                    try
                    {
                        string outPath = this.root + @"temp\" + att.Name;
                        input = att.ContentStream;
                        output = new FileStream(outPath, FileMode.Create, FileAccess.Write);
                        Byte[] buff = new Byte[size - 1];
                        int count = input.Read(buff, 0, size);
                        while (count > 0)
                        {
                            output.Write(buff, 0, count);
                            count = input.Read(buff, 0, size);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        input.Close();
                        output.Close();
                    }
                }
            }
        }

        /// <summary>
        ///parse a AceMail message file and return as a .NET MailMessage object
        /// </summary>
        private MailMessage MessageFileParse(FileInfo finfo)
        {
            MailMessage msg = new MailMessage();
            FileStream fs = null;
            try
            {
                fs = new FileStream(finfo.FullName, FileMode.Open);
                SharpMessage gmsg = new SharpMessage(fs);
                MailAddress from = new MailAddress(gmsg.From);
                msg.From = from;
                if (gmsg.To != null)
                {
                    foreach (object toAddr in gmsg.To)
                    {
                        MailAddress addr = new MailAddress(Convert.ToString(toAddr));
                        msg.To.Add(addr);
                    }

                }
                if(gmsg.Cc != null)
                {
                    foreach (object ccAddr in gmsg.Cc)
                    {
                        MailAddress addr = new MailAddress(Convert.ToString(ccAddr));
                        msg.CC.Add(addr);
                    }

                }
                msg.Subject = gmsg.Subject;
                if (!gmsg.HasHtmlBody)
                {
                    msg.Body = Utility.ClearTags(gmsg.Body);
                    msg.IsBodyHtml = false;
                }
                else
                {
                    msg.Body = gmsg.Body;
                    msg.IsBodyHtml = true;
                }
                AttachmentsSet(gmsg, msg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                fs.Close();
            }
            return msg;
        }

        /// <summary>
        ///extract the attachments from a parsed AceMail message and store in
        ///a .NET MailMessage object
        /// </summary>
        private void AttachmentsSet(SharpMessage gmsg, MailMessage msg)
        {
            TempClear();
            foreach (SharpAttachment att in gmsg.Attachments)
            {
                att.Save(this.root + @"temp\", true); 
                System.Net.Mail.Attachment mailAtt = new System.Net.Mail.Attachment(att.SavedFile.FullName);
                msg.Attachments.Add(mailAtt);
            }
        }

        /// <summary>
        ///clear this account's temp folder
        /// </summary>
        private void TempClear()
        {
            DirectoryInfo dinfo = new DirectoryInfo(this.root + @"temp\");
            FileInfo[] files = dinfo.GetFiles();
            if (files.Length > 0)
                foreach (FileInfo finfo in files)
                    finfo.Delete();
        }
        #endregion
    }
}