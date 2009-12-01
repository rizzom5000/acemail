using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using DataObjects;

namespace DataObjects
{
    public class EventReader
    {
        private const int ADMIN = 1;
        private const int DELETE = -1;
        private string connectionString = ConfigurationManager.AppSettings["connectionstring"];
        private SqlConnection conn = null;
        private Project db = null;
        private Guid guid;
        private int credentials;
        private string login;

        public EventReader(string login, string password)
        {
            try
            {
                this.conn = new SqlConnection(connectionString);
                this.db = new Project(conn);
            }
            catch (Exception e)
            {
                throw e;
            }
            User usr = db.User.Single(su => su.Login == login);
            if (usr.Password.Equals(password))
            {
                UserCredentials uc = db.UserCredentials.Single(s => s.Userid == usr.Userid);
                switch (uc.Credentials)
                {
                    case ADMIN:
                        this.credentials = ADMIN;
                        break;
                    case DELETE:
                        throw new Exception("User " + login + "'s Credentials have been Revoked.");
                        break;
                    default:
                        this.credentials = 0;
                        break;
                }
                this.guid = usr.Userid;
                this.login = usr.Login;
            }
            else
            {
                throw new Exception("User was not authenticated.");
            }
        }

        public List<Event> GetEventListing(DateTime day)
        {
            var lst = from e in db.Event
                      where (e.EventDate >= day.Subtract(new TimeSpan(0,1,0)) && e.EventDate <= day.Add(new TimeSpan(0,1,0)))
                      orderby e.EventDate descending
                      select e;
            return lst.ToList<Event>();
        }

        public User GetEventUser(Event evnt)
        {
            return db.User.Single(us => us.Userid == evnt.Userid);
        }
    }
}
