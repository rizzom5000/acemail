/*
Copyright 2009 Andrew Miller
Distributed under the terms of the GNU General Public License
*/
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace FileManager
{
    public class FileManagerInstance
    {
        const int INDENT = 7;
        string root = ConfigurationManager.AppSettings["root"];
        string userRoot;
        string mailPath; 
        string tempPath;
        string dataPath;
        string eventPath;
        DirectoryInfo dinfo;

        public FileManagerInstance(string login, bool create)
        {
            this.userRoot = root + login;
            this.mailPath = userRoot + @"\mail";
            this.tempPath = userRoot + @"\temp";
            this.dataPath = userRoot + @"\data";
            this.eventPath = userRoot + @"\event";
            if (create)
            {
                Directory.CreateDirectory(userRoot);
                dinfo = new DirectoryInfo(userRoot);
                Directory.CreateDirectory(mailPath);
                Directory.CreateDirectory(mailPath + @"\storage");
                Directory.CreateDirectory(mailPath + @"\sent");
                Directory.CreateDirectory(mailPath + @"\drafts");
                Directory.CreateDirectory(mailPath + @"\events");
                Directory.CreateDirectory(mailPath + @"\deleted");
                Directory.CreateDirectory(tempPath);
                Directory.CreateDirectory(dataPath);
                Directory.CreateDirectory(eventPath);
            }
            else
            {
                this.userRoot = root + login;
                dinfo = new DirectoryInfo(this.userRoot);
            }
        }

        public string RootPath()
        {
            return this.userRoot + @"\";
        }

        public int GetFile(string address, ref Byte[] fileData)
        {
            FileStream file = null;
            int fileSize = 0;
            try
            {
                file = new FileStream(address, FileMode.Open);
                FileInfo finfo = new FileInfo(address);
                fileSize = (int)finfo.Length;
                fileData = new Byte[fileSize];
                file.Read(fileData, 0, fileSize);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                file.Close();
            }
            return fileSize;
        }

        public string GetFile(string address)
        {
            string file = "";
            StreamReader sr = null;
            try
            {
                sr = File.OpenText(address);
                file = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sr.Close();
            }
            return file;
        }

        public string GetXSLTransform(string address, string path)
        {
            string xslPath = this.tempPath + @"\XSLTransform";
            XPathDocument xpd = new XPathDocument(address);
            XslCompiledTransform xt = new XslCompiledTransform();
            xt.Load(path + @"Event_History.xslt");
            XmlTextWriter xw = new XmlTextWriter(xslPath, null);
            xt.Transform(xpd, null, xw);
            xw.Close();

            StreamReader sr = null;
            string file = "";
            try
            {
                sr = File.OpenText(xslPath);
                file = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                xw.Close();
                sr.Close();
            }
            return file;
        }

        public List<string> GetFileNames()
        {
            List<string> fileNames = new List<string>();
            DirectoryInfo dinfo = new DirectoryInfo(dataPath);
            FileInfo[] files = dinfo.GetFiles();
            if (files.Length > 0)
                foreach (FileInfo finfo in files)
                {
                    fileNames.Add(finfo.Name);
                }
            return fileNames;
        }

        public bool DeleteFile(string address)
        {
            if(File.Exists(address))
            {
                File.Delete(address);
                return true;
            }
            else
                return false;
        }

        public void SaveFile(string fullName, ref Byte[] fileData)
        {
            FileStream file = null;
            try
            {
                file = new FileStream(fullName, FileMode.OpenOrCreate, FileAccess.Write);
                file.Write(fileData, 0, fileData.Length);
                file.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                file.Close();
            }
        }

        public void SaveFile(string fullName, string fileData)
        {
            FileStream file = null;
            StreamWriter sw = null;
            try
            {
                File.Delete(fullName);
                file = new FileStream(fullName, FileMode.OpenOrCreate, FileAccess.Write);
                sw = new StreamWriter(file);
                sw.Write(fileData);
                sw.Close();
                file.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                file.Close();
                sw.Close();
            }
        }

        public void EventWrite(DateTime date, int type, string name, string note, string subject)
        {
            string path = this.eventPath + @"\" + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString() + ".xml";
            XmlDocument doc = new XmlDocument();
            if (!File.Exists(path))
            {
                XmlTextWriter xmlWriter = new XmlTextWriter(path, System.Text.Encoding.UTF8);
                xmlWriter.Formatting = Formatting.Indented;
                xmlWriter.Indentation = INDENT;
                xmlWriter.WriteProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");
                xmlWriter.WriteStartElement("EventHistory");
                xmlWriter.Close();
            }
            doc.Load(path);
            XmlElement root = doc.DocumentElement;
            root.SetAttribute("updated", DateTime.Now.ToString());
            XmlElement evnt = doc.CreateElement("event");
            root.AppendChild(evnt);
            XmlElement dateNode = doc.CreateElement("date");
            XmlText dateText = doc.CreateTextNode(date.ToString());
            evnt.AppendChild(dateNode);
            dateNode.AppendChild(dateText);
            XmlElement typeNode = doc.CreateElement("type");
            XmlText typeText = doc.CreateTextNode(getType(type));
            evnt.AppendChild(typeNode);
            typeNode.AppendChild(typeText);
            XmlElement nameNode = doc.CreateElement("name");
            XmlText nameText = doc.CreateTextNode(name);
            evnt.AppendChild(nameNode);
            nameNode.AppendChild(nameText);
            XmlElement noteNode = doc.CreateElement("note");
            XmlText noteText = doc.CreateTextNode(note);
            evnt.AppendChild(noteNode);
            noteNode.AppendChild(noteText);
            if (!string.IsNullOrEmpty(subject))
            {
                XmlElement mail = doc.CreateElement("mail");
                evnt.AppendChild(mail);
                XmlElement subNode = doc.CreateElement("subject");
                XmlText subText = doc.CreateTextNode(subject);
                mail.AppendChild(subNode);
                subNode.AppendChild(subText);
            }
            doc.Save(path);
        }


        public bool EventHistoryExists(DateTime date)
        {
            bool found = false;
            string compDate = date.Day.ToString() + "-" + date.Month.ToString() + "-" + date.Year.ToString() + ".xml";
            DirectoryInfo dinfo = new DirectoryInfo(this.eventPath);
            FileInfo[] files = dinfo.GetFiles();
            foreach (FileInfo finfo in files)
            {
                if (finfo.Name.Equals(compDate))
                {
                    found = true;
                    break;
                }
            }
            return found;
        }

        private string getType(int t)
        {
            string type;
            switch (t)
            {
                case 0:
                    type = "notice";
                    break;
                case 1:
                    type = "mail";
                    break;
                default:
                    type = "";
                    break;
            }
            return type;
        }
    }
}
