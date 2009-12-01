/*
Copyright 2009 Andrew Miller
Distributed under the terms of the GNU General Public License
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;

namespace hMail
{
    /// <summary>
    /// Mail Type Enumeration
    /// </summary>
    public enum MailType
    {
        /// <summary>
        /// sent
        /// </summary>
        sent = 1,
        /// <summary>
        /// drafs
        /// </summary>
        drafts = 2,
        /// <summary>
        /// storage
        /// </summary>
        storage = 3,
        /// <summary>
        /// events
        /// </summary>
        events = 4,
        /// <summary>
        /// deleted
        /// </summary>
        deleted = 5,
        /// <summary>
        /// inbox
        /// </summary>
        inbox = 6,
        /// <summary>
        /// replied
        /// </summary>
        replied = 7,
        /// <summary>
        /// sending
        /// </summary>
        sending = 8,
    }

    /// <summary>
    ///static utility class for cleaning user input
    /// </summary>
    public static class Utility
    {
        
        /// <summary>
        /// sanitize browser-based input
        /// </summary>
        public static string Sanitize(string html)
        {
            html.Replace("\"", "\\&quot;");
            html.Replace("'", "\\&#39;");
            return HttpUtility.HtmlEncode(html);
        }

        /// <summary>
        ///replace html with line break characters
        /// </summary>
        public static string ClearTags(string input)
        {
            Regex.Replace(input, @"<p>|</p>|<br>|<br />", "\r\n");
            Regex.Replace(input, @"<.+?>", string.Empty);
            return HttpUtility.HtmlDecode(Regex.Replace(input, @"<(.|\n)*?>", ""));
        }
    }
}

