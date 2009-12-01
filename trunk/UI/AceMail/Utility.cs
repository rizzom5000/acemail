/*
Copyright 2009 Andrew Miller
Distributed under the terms of the GNU General Public License
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;

namespace AceMail
{
    public static class Utility
    {
        public static string Sanitize(string html)
        {
            html.Replace("\"", "\\&quot;");
            html.Replace("'", "\\&#39;");
            return HttpUtility.HtmlEncode(html);
        }

        public static string ClearTags(string input)
        {
            input = Regex.Replace(input, @"<p>|</p>|<br>|<br />", "\r\n");
            input = Regex.Replace(input, @"<.+?>", string.Empty);
            input = Regex.Replace(input, @"<(.|\n)*?>", "");
            return HttpUtility.HtmlDecode(input);
        }
    }
}
