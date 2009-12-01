/*
Copyright 2009 Andrew Miller
Distributed under the terms of the GNU General Public License
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using hMailServer;

namespace hMail
{
    /// <summary>
    ///represents a liteweight version of a hMail Server Message
    /// </summary>
    [Serializable]
    public class hMailMessage: IComparable
    {
        /// <summary>
        /// sort field enumeration
        /// </summary>
        public enum SortMethod
        {
            /// <summary>
            /// from
            /// </summary>
            from = 0,
            /// <summary>
            /// subject
            /// </summary>
            subject = 1,
            /// <summary>
            /// date
            /// </summary>
            date = 2
        }

        /// <summary>
        ///constructor
        /// </summary>
        public hMailMessage(hMailServer.Message msg)
        {
            this.emlid = Convert.ToString(msg.ID);
            this.updateddate = Convert.ToDateTime(msg.Date);

            if (!string.IsNullOrEmpty(msg.From))
            {
                this.fromAddress = msg.From;
            }
            this.subject = msg.Subject;
        }

        private string _emlid;
        /// <summary>
        ///get/set the id
        /// </summary>
        public string emlid
        {
            get { return this._emlid; }
            set { this._emlid = value; }
        }

        private DateTime _updateddate;
        /// <summary>
        ///get/set the updateddate
        /// </summary>
        public DateTime updateddate
        {
            get { return this._updateddate; }
            set { this._updateddate = value; }
        }

        private string _subject;
        /// <summary>
        ///get/set the subject
        /// </summary>
        public string subject
        {
            get { return this._subject; }
            set { this._subject = value; }
        }

        private string _from;
        /// <summary>
        ///get/set the from field
        /// </summary>
        public string fromAddress
        {
            get { return this._from; }
            set { this._from = value; }
        }

        private SortMethod _sort;
        /// <summary>
        ///get/set the sort direction
        /// </summary>
        public SortMethod sort
        {
            get { return _sort; }
            set { _sort = value; }
        }

        /// <summary>
        ///IComparable CompareTo method
        /// </summary>
        public int CompareTo(object obj)
        {
            hMailMessage mi = (hMailMessage)obj;
            switch (_sort)
            {
                case SortMethod.from:
                    return this.fromAddress.CompareTo(mi.fromAddress);
                case SortMethod.subject:
                    return this.subject.CompareTo(mi.subject);
                case SortMethod.date:
                    return this.updateddate.CompareTo(mi.updateddate);
                default:
                    return this.updateddate.CompareTo(mi.updateddate);
            }
        }
    }
}
