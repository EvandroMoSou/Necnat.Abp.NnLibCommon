﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Necnat.Abp.NnLibCommon.Extensions
{
    public static class StringExtension
    {
        public static string FirstCharacterUpperCase(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            return char.ToUpperInvariant(s[0]) + s.Substring(1);
        }

        public static string FirstCharacterLowerCase(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            return char.ToLowerInvariant(s[0]) + s.Substring(1);
        }

        public static string RemoveAccents(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            return new string(s
                .Normalize(NormalizationForm.FormD)
                .Where(ch => char.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)
                .ToArray());
        }

        public static string RemoveSpaces(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            return s.Replace(" ", "");
        }

        public static string RemoveDuplicateSpaces(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            Regex r = new Regex("[ ]{2,}", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            return r.Replace(s, " ");
        }

        public static bool IsDigitsOnly(this string s)
        {
            foreach (char c in s)
                if (!char.IsDigit(c))
                    return false;

            return true;
        }

        public static string OnlyDigits(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            return new string(s.Where(char.IsDigit).ToArray());
        }

        public static string OnlyDigitsOrSpace(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            StringBuilder sb = new StringBuilder();
            foreach (char c in s)
                if ((c >= '0' && c <= '9') || (c == ' '))
                    sb.Append(c);

            return sb.ToString();
        }

        public static string OnlyLetters(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            return new string(s.Where(char.IsLetter).ToArray());
        }

        public static string OnlyLettersOrSpace(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            StringBuilder sb = new StringBuilder();
            foreach (char c in s.RemoveAccents())
                if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || (c == ' '))
                    sb.Append(c);

            return sb.ToString();
        }

        public static string OnlyLettersOrDigits(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            return new string(s.Where(char.IsLetterOrDigit).ToArray());
        }

        public static string OnlyLettersDigitsOrSpace(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            StringBuilder sb = new StringBuilder();
            foreach (char c in s.RemoveAccents())
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || (c == ' '))
                    sb.Append(c);

            return sb.ToString();
        }

        public static string Monetary(this string s, string currencySymbol, string currencyDecimalSeparator)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            var b = currencySymbol + " ";
            if (s.Trim().StartsWith("-"))
                b = currencySymbol + " -";

            if (s.Length >= 1)
                if (s.Substring(s.Length - 1, 1) == "." || s.Substring(s.Length - 1, 1) == currencyDecimalSeparator)
                    s = s + "00";

            if (s.Length >= 2)
                if (s.Substring(s.Length - 1, 1) == "." || s.Substring(s.Length - 2, 1) == currencyDecimalSeparator)
                    s = s + "0";

            s = s.OnlyDigits().TrimStart('0');

            if (s.Length < 1)
                s = b + "0" + currencyDecimalSeparator + "00";
            else if (s.Length == 1)
                s = b + "0" + currencyDecimalSeparator + "0" + s;
            else if (s.Length == 2)
                s = b + "0" + currencyDecimalSeparator + s;
            else if (s.Length > 2)
                s = b + s.Substring(0, s.Length - 2) + currencyDecimalSeparator + s.Substring(s.Length - 2, 2);

            return s;
        }

        public static string RemoveIndexOf(this string s, char c)
        {
            var index = s.ToString().IndexOf(c);
            if (index >= 0)
                s = s.Remove(index, 1);

            return s;
        }

        public static string RemoveIndexOf(this string s, string str)
        {
            var index = s.ToString().IndexOf(str);
            if (index >= 0)
                s = s.Remove(index, str.Length);

            return s;
        }

        public static string RemoveLastIndexOf(this string s, char c)
        {
            var index = s.ToString().LastIndexOf(c);
            if (index >= 0)
                s = s.Remove(index, 1);

            return s;
        }

        public static string RemoveLastIndexOf(this string s, string str)
        {
            var index = s.ToString().LastIndexOf(str);
            if (index >= 0)
                s = s.Remove(index, str.Length);

            return s;
        }

        public static List<int> AllIndexesOf(this string str, string value)
        {
            if (String.IsNullOrEmpty(value))
                return new List<int>();

            List<int> indexes = new List<int>();
            for (int index = 0; ; index += value.Length)
            {
                index = str.IndexOf(value, index);
                if (index == -1)
                    return indexes;
                indexes.Add(index);
            }
        }

        public static T? ToNullableEnum<T>(this string enumString) where T : struct
        {
            if (string.IsNullOrWhiteSpace(enumString))
                return null;

            T outUnidadeFederativa;
            if (Enum.TryParse(enumString, out outUnidadeFederativa))
                return outUnidadeFederativa;
            else
                return null;
        }

        public static string ReplaceFirst(this string s, string search, string replace)
        {
            int pos = s.IndexOf(search);
            if (pos < 0)
            {
                return s;
            }
            return s.Substring(0, pos) + replace + s.Substring(pos + search.Length);
        }

        public static string ReplaceLast(this string s, string search, string replace)
        {
            int pos = s.LastIndexOf(search);
            if (pos < 0)
            {
                return s;
            }
            return s.Substring(0, pos) + replace + s.Substring(pos + search.Length);
        }

        public static string RemoveQuotationMarks(this string s)
        {
            return s.ReplaceFirst("\"", "").ReplaceLast("\"", "");
        }

        public static string ToCompare(this string s)
        {
            return s.ToUpper().RemoveAccents().OnlyLettersDigitsOrSpace().RemoveDuplicateSpaces().Trim();
        }
    }
}
