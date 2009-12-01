/*
Copyright 2009 Andrew Miller
Distributed under the terms of the GNU General Public License
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MySql.Data;
using MySql.Data.MySqlClient;
using SubSonic;
using DataObjectsDAL;
using hMail;
using FileManager;

namespace DataObjects
{
    public class UserInstance
    {
        private const int ADMIN = 1;
        private const int DELETE = -1;
        private string hMailDomain = ConfigurationManager.AppSettings["hmaildomain"];

        private Guid guid;
        private int credentials;
        private string login;
        private string email;
        private hMailInstance hm;
        private FileManagerInstance fm;

        #region Constructor
        public UserInstance(string login, string password)
        {
            try
            {
                User user = new Select()
                                .From(User.Schema)
                                .Where("Login").IsEqualTo(login)
                                .And("Password").IsEqualTo(password)
                                .ExecuteSingle<User>();
                if (user.Password.Equals(password))
                {
                    try
                    {
                        Usercredential usercred = new Select()
                                                .From(Usercredential.Schema)
                                                .Where("Userid").IsEqualTo(user.Userid)
                                                .ExecuteSingle<Usercredential>();
                        switch (usercred.Credentials)
                        {
                            case ADMIN:
                                this.credentials = ADMIN;
                                break;
                            case DELETE:
                                throw new Exception("User " + login + "'s Credentials have been Revoked.");
                            default:
                                this.credentials = 0;
                                break;
                        }
                        this.guid = new Guid(user.Userid);
                        this.login = user.Login;
                        this.email = user.ContactEmail;
                        this.fm = new FileManagerInstance(user.Login, false);
                        this.hm = new hMailInstance(user.ContactEmail, fm.RootPath());
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
                else
                {
                    throw new Exception("User was not authenticated.");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region Properties
        public Guid UserGUID()
        {
            return this.guid;
        }

        public string UserLogin()
        {
            return this.login;
        }

        public string UserEmail()
        {
            return this.email;
        }

        public int UserCredentials()
        {
            return this.credentials;
        }

        public hMailInstance UserMailInstance()
        {
            return this.hm;
        }

        public string RootPath()
        {
            return fm.RootPath();
        }

        public string UserPassword()
        {
            User user = new Select()
                            .From(User.Schema)
                            .Where("login").IsEqualTo(this.login)
                            .ExecuteSingle<User>();
            return user.Password;
        }
        #endregion

        #region Admin
        public Guid UserAdd(string login, string password, int credentials)
        {
            if (this.credentials != ADMIN)
                throw new Exception("Improper credentials.");
            UserCollection users = new UserCollection()
                                       .Where("login", login)
                                       .Load();
            if (users.Count != 0)
            {
                throw new Exception("Invalid login value: login is not unique.");
            }
            else
            {
                string userEmail = login + "@" + hMailDomain;
                Guid id = Guid.NewGuid();
                try
                {
                    User user = new User();
                    user.Userid = id.ToString();
                    user.Login = login;
                    user.Password = password;
                    user.ContactEmail = login + "@" + hMailDomain;
                    user.Createddate = DateTime.Now;
                    user.Updateddate = DateTime.Now;

                    Usercredential usercred = new Usercredential();
                    usercred.Userid = user.Userid;
                    usercred.Credentials = credentials;

                    user.Save();
                    usercred.Save();

                    fm = new FileManagerInstance(login, true); //create their file manager
                    hm = new hMailInstance(userEmail, password, fm.RootPath()); //create their email account
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return id;
            }
        }

        public User UserGetByID(Guid userId)
        {
            if (this.credentials != ADMIN)
                throw new Exception("Improper credentials.");
            return new User(userId);
        }

        public void UserUpdateName(Guid userId, string firstname, string lastname)
        {
            if (this.credentials != ADMIN)
                throw new Exception("Improper credentials.");
            try
            {
                User user = new User(userId);
                user.Firstname = firstname;
                user.Lastname = lastname;
                user.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UserUpdate(Guid userId, string firstname, string lastname, string login, string password)
        {
            if (this.credentials != ADMIN)
                throw new Exception("Improper credentials.");
            try
            {
                User user = new User(userId);
                user.Firstname = firstname;
                user.Lastname = lastname;
                user.Login = login;
                user.Password = password;
                user.Updateddate = DateTime.Now;
                user.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UserDelete(Guid userId)
        {
            if (this.credentials != ADMIN)
                throw new Exception("Improper credentials.");
            try
            {
                Usercredential usercred = new Select()
                                              .From(Usercredential.Schema)
                                              .Where(Usercredential.UseridColumn).IsEqualTo(userId.ToString())
                                              .ExecuteSingle<Usercredential>();
                usercred.Credentials = -1;
                usercred.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<User> UsersGetAll()
        {
            if (this.credentials != ADMIN)
                throw new Exception("Improper credentials.");
            UserCollection users = new UserCollection().Load();
            return users.GetList();
        }

        public List<User> UsersGetAllActive()
        {
            if (this.credentials != ADMIN)
                throw new Exception("Improper credentials.");
            UserCollection users = new Select()
                                       .From(User.Schema)
                                       .InnerJoin(Usercredential.UseridColumn, User.UseridColumn)
                                       .Where(Usercredential.Columns.Credentials).IsGreaterThan(0)
                                       .ExecuteAsCollection<UserCollection>();
            return users.GetList();
        }

        public int UserGetCredentials(Guid userId)
        {
            if (this.credentials != ADMIN)
                throw new Exception("Improper credentials.");
            Usercredential usercred = new Select()
                                          .From(Usercredential.Schema)
                                          .InnerJoin(User.UseridColumn, Usercredential.UseridColumn)
                                          .Where(User.Columns.Userid).IsEqualTo(userId.ToString())
                                          .ExecuteSingle<Usercredential>();
            return usercred.Credentials;
        }

        public List<Email> UserGetAllEmails()
        {
            EmailCollection emails = new Select()
                                         .From(Email.Schema)
                                         .InnerJoin(Client.ClientidColumn, Email.OwneridColumn)
                                         .InnerJoin(Userclient.ClientidColumn, Client.ClientidColumn)
                                         .Where(Userclient.Columns.Userid).IsEqualTo(this.guid.ToString())
                                         .ExecuteAsCollection<EmailCollection>();
            return emails.GetList();
        }
        #endregion

        #region Client
        public Guid ClientAdd(string first, string middle, string last, string address1, string address2, string city, string state, string zip, string country, string primary, string secondary, string mobile, string photo)
        {
            Guid id = Guid.NewGuid();
            Client client = new Client();
            client.Clientid = id.ToString();
            client.FirstName = first;
            client.MiddleName = middle;
            client.LastName = last;
            client.Address1 = address1;
            client.Address2 = address2;
            client.City = city;
            client.StateCode = state;
            client.PostalCode = zip;
            client.CountryCode = country;
            client.PhonePrimary = primary;
            client.PhoneSecondary = secondary;
            client.PhoneMobile = mobile;
            client.Photo = photo;
            client.Createddate = DateTime.Now;
            client.Updateddate = DateTime.Now;

            Userclient userclient = new Userclient();
            userclient.Clientid = id.ToString();
            userclient.Userid = this.guid.ToString();

            client.Save();
            userclient.Save();
            return id;
        }

        public List<Client> ClientsGetAll()
        {
            ClientCollection clients = new Select()
                                          .From(Client.Schema)
                                          .InnerJoin(Userclient.ClientidColumn, Client.ClientidColumn)
                                          .Where(Userclient.Columns.Userid).IsEqualTo(this.guid)
                                          .ExecuteAsCollection<ClientCollection>();
            return clients.GetList();
        }

        public Client ClientGetByID(Guid clientId)
        {
            return new Client(clientId.ToString());
        }

        public void ClientUpdateField(Guid clientId, string updateField, string updateValue)
        {
            Client client = new Client(clientId.ToString());
            switch (updateField)
            {
                case "FirstName":
                    client.FirstName = updateValue;
                    break;
                case "MiddleName":
                    client.MiddleName = updateValue;
                    break;
                case "LastName":
                    client.LastName = updateValue;
                    break;
                case "PhonePrimary":
                    client.PhonePrimary = updateValue;
                    break;
                case "PhoneSecondary":
                    client.PhoneSecondary = updateValue;
                    break;
                case "PhoneMobile":
                    client.PhoneMobile = updateValue;
                    break;
                default:
                    break;
            }
            client.Save();
        }

        public void ClientUpdateAddress(Guid clientId, string address1, string address2, string city, string stateCode, string postalCode, string countryCode)
        {
            Client client = new Client(clientId.ToString());
            client.Address1 = address1;
            client.Address2 = address2;
            client.City = city;
            client.StateCode = stateCode;
            client.PostalCode = postalCode;
            client.CountryCode = countryCode;
            client.Updateddate = DateTime.Now;
            client.Save();
        }

        public void ClientUpdatePhoto(Guid clientId, string path)
        {
            Client client = new Client(clientId.ToString());
            client.Photo = path;
            client.Updateddate = DateTime.Now;
            client.Save();
        }

        public void ClientUpdateEmail(Guid clientId, string email)
        {
            List<Email> emails = this.ClientGetAllEmailsByID(clientId);
            bool found = false;
            if (emails.Count > 0)
            {
                foreach (Email e in emails)
                {
                    if (e.Emailaddress.Equals(email))
                    {
                        found = true;
                        break;
                    }
                }
            }
            if (!found)
            {
                this.EmailAdd(clientId, email);
            }
        }

        public string ClientGetPhoto(Guid clientId)
        {
            Client client = new Client(clientId.ToString());
            return client.Photo;
        }

        public void ClientRemove(Guid clientId)
        {
            try
            {
                DB.Delete()
               .From(Userclient.Schema)
               .Where(Userclient.ClientidColumn).IsEqualTo(clientId.ToString())
               .Execute();
                EmailCollection emails = new EmailCollection()
                                             .Where("ownerid", clientId.ToString())
                                             .Load();
                emails.BatchDelete();
                ClientCollection dependents = new Select()
                                                  .From(Client.Schema)
                                                  .InnerJoin(Userclient.Schema)
                                                  .Where(Userclient.Columns.Userid).IsEqualTo(clientId.ToString())
                                                  .ExecuteAsCollection<ClientCollection>();
                dependents.BatchDelete();
                ReferralCollection referrals = new ReferralCollection()
                                                   .Where("referredclientid", clientId.ToString())
                                                   .Load();
                referrals.BatchDelete();
                GroupclientCollection groupclients = new GroupclientCollection()
                                                         .Where("clientid", clientId.ToString())
                                                         .Load();
                groupclients.BatchDelete();
                DB.Delete()
                .From(Client.Schema)
                .Where(Client.ClientidColumn).IsEqualTo(clientId.ToString())
                .Execute();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Email> ClientGetAllEmailsByID(Guid clientId)
        {
            EmailCollection emails = new Select()
                                         .From(Email.Schema)
                                         .InnerJoin(Client.ClientidColumn, Email.OwneridColumn)
                                         .Where(Email.OwneridColumn).IsEqualTo(clientId.ToString())
                                         .ExecuteAsCollection<EmailCollection>();
            return emails.GetList();
        }

        public List<Group> ClientGetAllGroupsByID(Guid clientId)
        {
            GroupCollection groups = new Select()
                                         .From(Group.Schema)
                                         .InnerJoin(Groupclient.GroupidColumn, Group.GroupidColumn)
                                         .Where(Groupclient.Columns.Clientid).IsEqualTo(clientId.ToString())
                                         .ExecuteAsCollection<GroupCollection>();
            return groups.GetList();
        }
        #endregion

        #region Email
        public Guid EmailAdd(Guid ownerId, string emailaddress)
        {
            Guid id = Guid.NewGuid();
            Email email = new Email();
            email.Emailid = id.ToString();
            email.Ownerid = ownerId.ToString();
            email.Emailaddress = emailaddress;
            email.Createddate = DateTime.Now;
            email.Updateddate = DateTime.Now;

            email.Save();
            return id;
        }

        public List<Email> EmailsGetAllByID(Guid ownerId)
        {
            EmailCollection emails = new EmailCollection()
                                  .Where("ownerid", ownerId.ToString())
                                  .Load();
            return emails.GetList();
        }

        public void EmailRemove(Guid ownerId, string emailAddress)
        {
            DB.Delete()
            .From(Email.Schema)
            .Where(Email.OwneridColumn).IsEqualTo(ownerId.ToString())
            .And(Email.EmailaddressColumn).IsEqualTo(emailAddress)
            .Execute();
        }
        #endregion

        #region Referral
        public Guid ReferralAdd(Guid referredById, Guid referredClientId, DateTime date, string note)
        {
            Guid id = Guid.NewGuid();
            Referral referral = new Referral();
            referral.Referralid = id.ToString();
            referral.ReferredClientID = referredClientId.ToString();
            referral.ReferredByID = referredById.ToString();
            referral.ReferralDate = date;
            referral.Notes = note;
            referral.Createddate = DateTime.Now;
            referral.Updateddate = DateTime.Now;

            referral.Save();
            return id;
        }

        public List<Referral> ReferralsGetAllByID(Guid referredClientId)
        {
            ReferralCollection referrals = new ReferralCollection()
                                               .Where("referredclientid", referredClientId.ToString())
                                               .Load();
            return referrals.GetList();
        }

        public List<Referral> ReferredGetAllByID(Guid referredById)
        {
            ReferralCollection referrals = new ReferralCollection()
                                               .Where("referredbyid", referredById)
                                               .Load();
            return referrals.GetList();
        }

        public void ReferralRemove(Guid referralId)
        {
            DB.Delete()
            .From(Referral.Schema)
            .Where(Referral.ReferralidColumn).IsEqualTo(referralId.ToString())
            .Execute();
        }
        #endregion

        #region Dependent
        public void DependentAdd(Guid parentId, Guid dependentId)
        {
            Clientdependent dependent = new Clientdependent();
            dependent.Clientid = parentId.ToString();
            dependent.Dependentid = dependentId.ToString();
            dependent.Save();
        }

        public List<Client> DependentsGetAllByID(Guid clientId)
        {
            ClientCollection dependents = new Select()
                                              .From(Client.Schema)
                                              .InnerJoin(Clientdependent.ClientidColumn, Client.ClientidColumn)
                                              .Where(Clientdependent.Columns.Clientid).IsEqualTo(clientId.ToString())
                                              .ExecuteAsCollection<ClientCollection>();
            return dependents.GetList();
        }

        public void DependentRemove(Guid clientId, Guid dependentId)
        {
            DB.Delete()
            .From(Clientdependent.Schema)
            .Where(Clientdependent.ClientidColumn).IsEqualTo(clientId.ToString())
            .And(Clientdependent.DependentidColumn).IsEqualTo(dependentId.ToString())
            .Execute();
        }
        #endregion

        #region Group
        public Guid GroupAdd(string name, string note)
        {
            Guid id = Guid.NewGuid();
            Group group = new Group();
            group.Groupid = id.ToString();
            group.GroupName = name;
            group.Notes = note;
            group.Createddate = DateTime.Now;
            group.Updateddate = DateTime.Now;

            Usergroup usergroup = new Usergroup();
            usergroup.Userid = this.guid.ToString();
            usergroup.Groupid = group.Groupid;

            group.Save();
            usergroup.Save();
            return id;
        }

        public List<Group> GroupsGetAll()
        {
            GroupCollection groups = new Select()
                                         .From(Group.Schema)
                                         .InnerJoin(Usergroup.GroupidColumn, Group.GroupidColumn)
                                         .Where(Usergroup.Columns.Userid).IsEqualTo(this.guid.ToString())
                                         .ExecuteAsCollection<GroupCollection>();
            return groups.GetList();
        }

        public Group GroupGetByID(Guid groupId)
        {
            return new Group(groupId.ToString());
        }

        public void GroupAddClient(Guid groupId, Guid clientId, Guid clientEmailId)
        {
            Groupclient groupclient = new Groupclient();
            groupclient.Groupid = groupId.ToString();
            groupclient.Clientid = clientId.ToString();
            groupclient.Clientemailid = clientEmailId.ToString();
            groupclient.Clientsmsid = null;
            groupclient.Save();
        }

        public List<Client> GroupGetAllClients(Guid groupid)
        {
            ClientCollection clients = new Select()
                                           .From(Client.Schema)
                                           .InnerJoin(Groupclient.ClientidColumn, Client.ClientidColumn)
                                           .Where(Groupclient.Columns.Groupid).IsEqualTo(groupid.ToString())
                                           .ExecuteAsCollection<ClientCollection>();
            return clients.GetList();
        }

        //return the email addresses associated with a group
        public List<Email> GroupGetAllClientEmails(Guid groupid)
        {
            EmailCollection emails = new Select()
                                         .From(Email.Schema)
                                         .InnerJoin(Groupclient.ClientidColumn, Email.OwneridColumn)
                                         .Where(Groupclient.Columns.Groupid).IsEqualTo(groupid.ToString())
                                         .ExecuteAsCollection<EmailCollection>();
            return emails.GetList();
        }

        public void GroupRemoveClient(Guid groupId, Guid clientId)
        {
            DB.Delete()
            .From(Groupclient.Schema)
            .Where(Groupclient.GroupidColumn).IsEqualTo(groupId.ToString())
            .And(Groupclient.ClientidColumn).IsEqualTo(clientId.ToString())
            .Execute();
        }

        public void GroupRemove(Guid groupId)
        {
            Group group = new Group(groupId);
            GroupclientCollection groupclients = new GroupclientCollection()
                                                     .Where("groupid", groupId.ToString())
                                                     .Load();
            groupclients.BatchDelete();
            UsergroupCollection usergroups = new UsergroupCollection()
                                                 .Where("groupid", groupId.ToString())
                                                 .Load();
            usergroups.BatchDelete();
            DB.Delete()
            .From(Group.Schema)
            .Where(Group.GroupidColumn).IsEqualTo(groupId.ToString())
            .Execute();
        }

        public void GroupUpdate(Guid groupId, string name, string note)
        {
            Group group = new Group(groupId.ToString());
            group.GroupName = name;
            group.Notes = note;
            group.Updateddate = DateTime.Now;
            group.Save();
        }
        #endregion

        #region File
        public Guid FileAdd(string name, string ext, ref Byte[] fileData)
        {
            string fullName = fm.RootPath() + name + ext;
            Guid id = Guid.NewGuid();
            File file = new File();
            file.Fileid = id.ToString();
            file.Filename = name + ext;
            file.Extension = ext;
            file.Address = fullName;
            file.Createddate = DateTime.Now;
            file.Updateddate = DateTime.Now;

            Userfile userfile = new Userfile();
            userfile.Userid = this.guid.ToString();
            userfile.Fileid = file.Fileid;
            
            file.Save();
            userfile.Save();
            this.fm.SaveFile(fullName, ref fileData);
            return id;
        }

        public Guid FileAdd(string name, string ext, string address)
        {
            Guid id = Guid.NewGuid();
            File file = new File();
            file.Fileid = id.ToString();
            file.Filename = name;
            file.Extension = ext;
            file.Address = address;
            file.Createddate = DateTime.Now;
            file.Updateddate = DateTime.Now;

            Userfile userfile = new Userfile();
            userfile.Userid = this.guid.ToString();
            userfile.Fileid = file.Fileid;

            file.Save();
            userfile.Save();
            return id;
        }

        public List<File> FilesGetAll()
        {
            FileCollection files = new Select()
                                       .From(File.Schema)
                                       .InnerJoin(Userfile.FileidColumn, File.FileidColumn)
                                       .Where(Userfile.Columns.Userid).IsEqualTo(this.guid.ToString())
                                       .ExecuteAsCollection<FileCollection>();
            return files.GetList();
        }

        public int FileGetByID(Guid fileId, ref Byte[] fileData)
        {
            File file = new File(fileId);
            return this.fm.GetFile(file.Address, ref fileData);
        }

        public string FileGetPathByID(Guid fileId)
        {
            File file = new File(fileId);
            return this.fm.GetFile(file.Address);
        }

        public File FileGetByID(Guid fileId)
        {
            return new File(fileId);
        }

        public void FileUpdate(Guid fileId, string fileData)
        {
            File file = new File(fileId);
            this.fm.SaveFile(file.Address, fileData);
            file.Updateddate = DateTime.Now;
            file.Save();
        }

        public void FileRemove(Guid fileId)
        {
            File file = new File(fileId);
            DB.Delete()
            .From(Userfile.Schema)
            .Where(Userfile.FileidColumn).IsEqualTo(file.Fileid)
            .Execute();
            this.fm.DeleteFile(file.Address);
            DB.Delete()
            .From(File.Schema)
            .Where(File.FileidColumn).IsEqualTo(fileId.ToString())
            .Execute();
        }
        #endregion

        #region Eml
        public Guid EmlAdd(string fileId, string subject, string from, MailType type, bool answered, bool seen)
        {
            Guid id = Guid.NewGuid();         
            Eml eml = new Eml();
            eml.Emlid = id.ToString();
            eml.Emlpath = fileId;
            eml.Subject = subject;
            eml.Fromaddress = from;
            eml.Type = (int)type;
            eml.Answered = answered;
            eml.Seen = seen;
            eml.Createddate = DateTime.Now;
            eml.Updateddate = DateTime.Now;

            Usereml usereml = new Usereml();
            usereml.Userid = this.guid.ToString();
            usereml.Emlid = eml.Emlid;

            eml.Save();
            usereml.Save();
            return id;
        }

        public void EmlDelete(Guid emlId)
        {
            DB.Delete()
            .From(Eml.Schema)
            .Where(Eml.EmlidColumn).IsEqualTo(emlId.ToString())
            .Execute();
            DB.Delete()
            .From(Usereml.Schema)
            .Where(Usereml.EmlidColumn).IsEqualTo(emlId.ToString())
            .Execute();
        }

        public void EmlUpdate(Guid emlId, string fileId, string subject, string from, MailType type, bool answered, bool seen)
        {
            Eml eml = new Eml(emlId.ToString());
            eml.Emlpath = fileId;
            eml.Subject = subject;
            eml.Fromaddress = from;
            eml.Type = (int)type;
            eml.Answered = answered;
            eml.Seen = seen;
            eml.Updateddate = DateTime.Now;
            eml.Save();
        }

        public void EmlUpdate(Guid emlId, MailType type)
        {
            Eml eml = new Eml(emlId);
            eml.Type = (int)type;
            eml.Updateddate = DateTime.Now;
            eml.Save();
        }

        public List<Eml> EmlGetByType(int type)
        {
            EmlCollection emls = new Select()
                                     .From(Eml.Schema)
                                     .InnerJoin(Usereml.EmlidColumn, Eml.EmlidColumn)
                                     .Where(Usereml.Columns.Userid).IsEqualTo(this.guid.ToString())
                                     .And(Eml.TypeColumn).IsEqualTo(type)
                                     .ExecuteAsCollection<EmlCollection>();
            return emls.GetList();
        }

        public List<Eml> EmlGetByType(MailType type)
        {
            EmlCollection emls = new Select()
                                     .From(Eml.Schema)
                                     .InnerJoin(Usereml.EmlidColumn, Eml.EmlidColumn)
                                     .Where(Usereml.Columns.Userid).IsEqualTo(this.guid.ToString())
                                     .And(Eml.TypeColumn).IsEqualTo((int)type)
                                     .ExecuteAsCollection<EmlCollection>();
            return emls.GetList();
        }

        public string EmlGetPathByID(Guid emlId)
        {
            Eml eml = new Eml(emlId);
            return eml.Emlpath;
        }
        #endregion

        #region Notice
        public Guid NoticeAdd(int type, DateTime date, string name, string note)
        {
            Guid id = Guid.NewGuid();
            Notice notice = new Notice();
            notice.Noticeid = id.ToString();
            notice.Noticedate = date;
            notice.Name = name;
            notice.Notes = note;
            notice.Createddate = DateTime.Now;
            notice.Updateddate = DateTime.Now;
            notice.Save();

            Usernotice usernotice = new Usernotice();
            usernotice.Userid = this.guid.ToString();
            usernotice.Noticeid = id.ToString();
            usernotice.Save();
            return id;
        }

        public void NoticeDelete(Guid noticeId)
        {
            DB.Delete()
            .From(Usernotice.Schema)
            .Where(Usernotice.NoticeidColumn).IsEqualTo(noticeId.ToString())
            .Execute();
            NoticeemlCollection noticeemls = new NoticeemlCollection()
                                                    .Where("noticeid", noticeId.ToString())
                                                    .Load();
            noticeemls.BatchDelete();
            NoticeclientCollection noticeclients = new NoticeclientCollection()
                                                        .Where("noticeid", noticeId.ToString())
                                                        .Load();
            noticeclients.BatchDelete();
            NoticegroupCollection noticegroups = new NoticegroupCollection()
                                                        .Where("noticeid", noticeId.ToString())
                                                        .Load();
            noticegroups.BatchDelete();
            NoticefileCollection noticefiles = new NoticefileCollection()
                                                    .Where("noticeid", noticeId.ToString())
                                                    .Load();
            noticefiles.BatchDelete();
            DB.Delete()
            .From(Notice.Schema)
            .Where(Notice.NoticeidColumn).IsEqualTo(noticeId.ToString())
            .Execute();
        }

        public void NoticeUpdate(Guid noticeId, DateTime date, string name, string note)
        {
            Notice notice = new Notice(noticeId.ToString());
            notice.Noticedate = date;
            notice.Name = name;
            notice.Notes = note;
            notice.Updateddate = DateTime.Now;
            notice.Save();
        }

        public Notice NoticeGetByID(Guid noticeId)
        {
            return new Notice(noticeId.ToString());
        }

        public List<Notice> NoticesGetAll()
        {
            NoticeCollection notices = new Select()
                                        .From(Notice.Schema)
                                        .InnerJoin(Usernotice.NoticeidColumn, Notice.NoticeidColumn)
                                        .Where(Usernotice.UseridColumn).IsEqualTo(this.guid.ToString())
                                        .ExecuteAsCollection<NoticeCollection>();
            return notices.GetList();
        }

        public List<Notice> NoticesGetByType(int type)
        {
            NoticeCollection notices = new Select()
                                        .From(Notice.Schema)
                                        .InnerJoin(Usernotice.NoticeidColumn, Notice.NoticeidColumn)
                                        .Where(Usernotice.UseridColumn).IsEqualTo(this.guid.ToString())
                                        .And(Notice.TypeColumn).IsEqualTo(type)
                                        .ExecuteAsCollection<NoticeCollection>();
            return notices.GetList();
        }

        public List<Notice> NoticesGetByDate(DateTime date)
        {
            NoticeCollection notices = new Select()
                                        .From(Notice.Schema)
                                        .InnerJoin(Usernotice.NoticeidColumn, Notice.NoticeidColumn)
                                        .Where(Usernotice.UseridColumn).IsEqualTo(this.guid.ToString())
                                        .And(Notice.NoticedateColumn).IsBetweenAnd(date.AddDays(-1), date.AddDays(1))
                                        .OrderAsc("noticedate")
                                        .ExecuteAsCollection<NoticeCollection>();
            return notices.GetList();
        }

        public List<Notice> NoticesGetByMonth(DateTime start)
        {
            int daysInMonth = DateTime.DaysInMonth(start.Year, start.Month);
            NoticeCollection notices = new Select()
                                        .From(Notice.Schema)
                                        .InnerJoin(Usernotice.NoticeidColumn, Notice.NoticeidColumn)
                                        .Where(Usernotice.UseridColumn).IsEqualTo(this.guid.ToString())
                                        .And(Notice.NoticedateColumn).IsBetweenAnd(start.AddDays(-1*start.Day), start.AddDays(daysInMonth - start.Day))
                                        .OrderAsc("noticedate")
                                        .ExecuteAsCollection<NoticeCollection>();
            return notices.GetList();
        }

        public List<Notice> NoticesGetByDateRange(DateTime start, DateTime end)
        {
            NoticeCollection notices = new Select()
                                        .From(Notice.Schema)
                                        .InnerJoin(Usernotice.NoticeidColumn, Notice.NoticeidColumn)
                                        .Where(Usernotice.UseridColumn).IsEqualTo(this.guid.ToString())
                                        .And(Notice.NoticedateColumn).IsBetweenAnd(start.AddDays(-1), end.AddDays(1))
                                        .OrderAsc("noticedate")
                                        .ExecuteAsCollection<NoticeCollection>();
            return notices.GetList();
        }

        public List<Notice> NoticesGetByDateRangeAndType(DateTime start, DateTime end, int type)
        {
            NoticeCollection notices = new Select()
                                        .From(Notice.Schema)
                                        .InnerJoin(Usernotice.NoticeidColumn, Notice.NoticeidColumn)
                                        .Where(Usernotice.UseridColumn).IsEqualTo(this.guid.ToString())
                                        .And(Notice.NoticedateColumn).IsBetweenAnd(start.AddDays(-1), end.AddDays(1))
                                        .And(Notice.TypeColumn).IsEqualTo(type)
                                        .OrderAsc("noticedate")
                                        .ExecuteAsCollection<NoticeCollection>();
            return notices.GetList();
        }

        public void NoticeAddClient(Guid noticeId, Guid clientId)
        {
            Noticeclient noticeclient = new Noticeclient();
            noticeclient.Noticeid = noticeId.ToString();
            noticeclient.Clientid = clientId.ToString();
            noticeclient.Save();
        }

        public void NoticeRemoveClient(Guid noticeId, Guid clientId)
        {
            DB.Delete()
            .From(Noticeclient.Schema)
            .Where(Noticeclient.NoticeidColumn).IsEqualTo(noticeId.ToString())
            .And(Noticeclient.ClientidColumn).IsEqualTo(clientId.ToString())
            .Execute();
        }

        public List<Client> NoticeGetClientList(Guid noticeId)
        {
            ClientCollection clients = new Select()
                                        .From(Client.Schema)
                                        .InnerJoin(Noticeclient.ClientidColumn, Client.ClientidColumn)
                                        .Where(Noticeclient.Columns.Noticeid).IsEqualTo(noticeId.ToString())
                                        .ExecuteAsCollection<ClientCollection>();
            return clients.GetList();
        }

        public void NoticeAddGroup(Guid noticeId, Guid groupId)
        {
            Noticegroup noticegroup = new Noticegroup();
            noticegroup.Noticeid = noticeId.ToString();
            noticegroup.Groupid = groupId.ToString();
            noticegroup.Save();
        }

        public void NoticeRemoveGroup(Guid noticeId, Guid groupId)
        {
            DB.Delete()
            .From(Noticegroup.Schema)
            .Where(Noticegroup.NoticeidColumn).IsEqualTo(noticeId.ToString())
            .And(Noticegroup.GroupidColumn).IsEqualTo(groupId.ToString())
            .Execute();
        }

        public List<Group> NoticeGetGroupList(Guid noticeId)
        {
            GroupCollection groups = new Select()
                                        .From(Group.Schema)
                                        .InnerJoin(Noticegroup.GroupidColumn, Group.GroupidColumn)
                                        .Where(Noticegroup.Columns.Noticeid).IsEqualTo(noticeId.ToString())
                                        .ExecuteAsCollection<GroupCollection>();
            return groups.GetList();
        }

        public void NoticeAddFile(Guid noticeId, Guid fileId)
        {
            Noticefile noticefile = new Noticefile();
            noticefile.Noticeid = noticeId.ToString();
            noticefile.Fileid = fileId.ToString();
            noticefile.Save();
        }

        public void NoticeRemoveFile(Guid noticeId, Guid fileId)
        {
            DB.Delete()
            .From(Noticefile.Schema)
            .Where(Noticefile.NoticeidColumn).IsEqualTo(noticeId.ToString())
            .And(Noticefile.FileidColumn).IsEqualTo(fileId.ToString())
            .Execute();
        }

        public List<File> NoticeGetFileList(Guid noticeId)
        {
            FileCollection files = new Select()
                                    .From(File.Schema)
                                    .InnerJoin(Noticefile.FileidColumn, File.FileidColumn)
                                    .Where(Noticefile.Columns.Noticeid).IsEqualTo(noticeId.ToString())
                                    .ExecuteAsCollection<FileCollection>();
            return files.GetList();
        }

        public void NoticeAddEml(Guid noticeId, Guid emlId)
        {
            Noticeeml noticeeml = new Noticeeml();
            noticeeml.Noticeid = noticeId.ToString();
            noticeeml.Emlid = emlId.ToString();
            noticeeml.Save();
        }

        public void NoticeRemoveEml(Guid noticeId, Guid emlId)
        {
            DB.Delete()
            .From(Noticeeml.Schema)
            .Where(Noticeeml.NoticeidColumn).IsEqualTo(noticeId.ToString())
            .And(Noticeeml.EmlidColumn).IsEqualTo(emlId.ToString())
            .Execute();
        }

        public List<Eml> NoticeGetEmlList(Guid noticeId)
        {
            EmlCollection emls = new Select()
                                    .From(Eml.Schema)
                                    .InnerJoin(Noticeeml.EmlidColumn, Eml.EmlidColumn)
                                    .Where(Noticeeml.Columns.Noticeid).IsEqualTo(noticeId.ToString())
                                    .ExecuteAsCollection<EmlCollection>();
            return emls.GetList();
        }
        #endregion
    }
}
