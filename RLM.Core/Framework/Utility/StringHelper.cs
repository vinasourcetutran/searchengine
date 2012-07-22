using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using RLM.Core.Framework.Log;
using System.Collections;
using System.Globalization;
using RLM.Core.Framework.Constant;

namespace RLM.Core.Framework.Utility
{
    public class StringHelper
    {
        #region Plural
        public static string Plurized(string name)
        {
            return name + "es";
        }
        #endregion

        #region String function
        public static string Reverse(string input)
        {
            if (string.IsNullOrEmpty(input)) { return string.Empty; }
            char[] items = input.ToCharArray();
            items = (char[])items.Reverse();
            return new string(items);
        }

        public static string Truncat(string input, int maxLenght)
        {
            if (input.Length <= maxLenght) { return input; }
            int pos = input.IndexOf(' ', maxLenght);
            return (pos <= 0 ? input : input.Substring(0, pos) + " ...");
        }

        public static string RemoveQuote(string input)
        {
            return input == null ? string.Empty : StringHelper.Replace("'", "\\'", input);
        }

        public static string RemoveNewline(string input)
        {
            string result=StringHelper.Replace("\r", "", input);
            result=StringHelper.Replace("\n", "<br/>", result);
            result = StringHelper.Replace("\t", " ", result);
            return result;
        }

        public static string RemoveNewlineForText(string input)
        {
            string result = StringHelper.Replace("\r", "\\r", input);
            result = StringHelper.Replace("\n", "\\n", result);
            result = StringHelper.Replace("\t", "\\t", result);
            return result;
        }

        public static string HtmlFormat(string input, bool isHtmlFormat)
        {
            input = StringHelper.RemoveQuote(input);
            if (isHtmlFormat)
            {
                input = StringHelper.RemoveNewline(input);
            }
            else
            {
                input = StringHelper.RemoveNewlineForText(input);
            }
            
            return input;
        }

        public static string JsonFormat(string input, bool isHtmlFormat)
        {
            input = StringHelper.RemoveQuote(input);
            if (isHtmlFormat)
            {
                input = StringHelper.RemoveNewline(input);
            }
            else
            {
                input = StringHelper.RemoveNewlineForText(input);
            }

            return input;
        }

        public static string Replace(string pattern, string replaceText, string input)
        {
            try
            {
                string value = Regex.Replace(input, pattern, replaceText, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                return value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string Remove(string input, string pattern)
        {
            try
            {
                if (string.IsNullOrEmpty(pattern)) { return input; }
                //return Regex.Replace(input, pattern, replaceText, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                string[] indicators = pattern.Split(':');
                if (indicators.Length < 2) { return input; }
                while (input.IndexOf(indicators[0], StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    int startPos = input.IndexOf(indicators[0], StringComparison.OrdinalIgnoreCase);
                    int endPos = input.IndexOf(indicators[1], StringComparison.OrdinalIgnoreCase) + indicators[1].Length;
                    if (endPos < 0) { endPos = input.Length - 1; }
                    if (startPos >= endPos) { break; }
                    input = input.Remove(startPos, endPos - startPos);
                }
                return input;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string Remove(string input, string[] patterns)
        {
            try
            {
                foreach (string item in patterns)
                {
                    input = Remove(input, item);
                }
                return input;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string Format(string pattern, params string[] values)
        {
            for (int index = 0; index < values.Length; index++)
            {
                string bookMark = "{" + index + "}";
                while (pattern.IndexOf(bookMark) >= 0)
                {
                    pattern = pattern.Replace(bookMark, values[index]);
                }

            }
            return pattern;
        }
        public static string Format(string pattern, params object[] values)
        {
            for (int index = 0; index < values.Length; index++)
            {
                string bookMark = "{" + index + "}";
                while (pattern.IndexOf(bookMark) >= 0)
                {
                    pattern = pattern.Replace(bookMark, values[index] == null ? string.Empty : values[index].ToString());
                }

            }
            return pattern;
        }
        #endregion

        #region Expression
        public static string RemoveByPattern(string input, string pattern)
        {
            try
            {
                if (string.IsNullOrEmpty(input)) { return string.Empty; }
                return Regex.Replace(input, pattern, "", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            }
            catch (Exception ex)
            {
                return input;
            }
        }
        public static bool IsMatch(string pattern, string input)
        {
            return Regex.IsMatch(input, pattern, RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        public static string GetMatch(string pattern, string input, int index)
        {
            string value = "";

            if (Regex.IsMatch(input, pattern, RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.Compiled))
            {
                value = Regex.Match(input, pattern, RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.Compiled).Groups[index].Value;
            }
            return value;
        }

        public static string GetMatch(string pattern, string input)
        {
            return GetMatch(pattern, input,1);
            //string value = "";

            //if (Regex.IsMatch(input, pattern, RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.Compiled))
            //{
            //    value = Regex.Match(input, pattern, RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.Compiled).Groups[1].Value;
            //}
            //return value;
        }

        public static MatchCollection GetMatches(string pattern, string input)
        {
            if (Regex.IsMatch(input, pattern, RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.Compiled))
            {
                return Regex.Matches(input, pattern, RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            }
            return null;
        }
        #endregion

        #region Random Guid
        public static string GetGuid(int length)
        {
            string guid=Guid.NewGuid().ToString().Replace("-","");
            if(length>0 && guid.Length>length){ guid=guid.Substring(0, length);}
            return guid;
        }

        public static string GetGuid()
        {
            return StringHelper.GetGuid(32);
        }
        #endregion

        #region String function
        public static string SubString(string text, int maxLength)
        {
            if (string.IsNullOrEmpty(text)) { return string.Empty; }
            if (text.Length <= maxLength) { return text; }
            return text.Substring(0, maxLength);
        }

        public static string GetAsciiString(string text)
        {
            text = StringHelper.RemoveDiacritics(text);
            return Regex.Replace(text, ExpressionPattern.NOTSEOALPHABET, "_", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            //return StringHelper.RemoveByPattern(text, ExpressionPattern.NOTSEOALPHABET);
        }

        public static string RemoveDiacritics(string stIn)
        {
            try
            {
                if (string.IsNullOrEmpty(stIn)) { return string.Empty; }
                string stFormD = stIn.Normalize(NormalizationForm.FormD);
                StringBuilder sb = new StringBuilder();

                for (int ich = 0; ich < stFormD.Length; ich++)
                {
                    UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                    if (uc != UnicodeCategory.NonSpacingMark)
                    {
                        sb.Append(stFormD[ich]);
                    }
                }

                stFormD = (sb.ToString().Normalize(NormalizationForm.FormC));

                stFormD = ReplaceString(stFormD, ExpressionPattern.REPLACEALPHABET);
                return StringHelper.RemoveByPattern(stFormD, ExpressionPattern.NOTALPHABET);

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return stIn;
            }
        }

        private static string ReplaceString(string stFormD, string pattern)
        {
            string[] items = pattern.Split(';');
            foreach(string item in items)
            {
                string[] subitems = item.Split(':');
                stFormD = Replace(subitems[0], subitems[1], stFormD);
            }
            return stFormD;
        }

        //public static string GetSeoString(string text)
        //{
        //    try
        //    {
        //        return string.Empty;
        //        //if (string.IsNullOrEmpty(text)) { return string.Empty; }
        //        //text = RemoveDiacritics(text);
        //        //return Regex.Replace(text, ExpressionPattern.NOTSEOALPHABET, "-", RegexOptions.Multiline | RegexOptions.IgnoreCase);
        //    }
        //    catch (Exception ex)
        //    {
        //        return text;
        //    }
        //}
        #endregion

        #region File
        public static string FormatFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) { return string.Empty; }
            fileName = StringHelper.RemoveDiacritics(fileName);
            fileName = fileName.Replace(' ', '_');
            return fileName;
        }

        public static string FormatFileNameUrl(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) { return string.Empty; }
            fileName = StringHelper.FormatFileName(fileName);
            fileName = fileName.Replace('\\', '/');
            return fileName;
        }
        #endregion

        public static string GetUrlRewriteString(string data)
        {
            try
            {
                if (string.IsNullOrEmpty(data)) { return string.Empty; }
                data = data.Replace(" ","_");
                data = data.Replace(".","");
                data = data.Replace(",", "");
                data = data.Replace("/","");
                data = data.Replace("\\","");
                data = data.Replace("@", "");
                data = data.Replace("%", "");
                return data.ToLower();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return data;
            }
        }

        public static bool IsValidEmail(string email)
        {
            return StringHelper.IsMatch(ExpressionPattern.EMAIL,email);
        }


        
    }
}
