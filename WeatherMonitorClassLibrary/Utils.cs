﻿using System;
using System.Linq;
using System.Net;
using System.Xml;

namespace WeatherMonitorClassLibrary
{
    public static class Utils
    {
        public static bool ArrayInBounds(int index, string[] array)
        {
            return (index >= 0) && (index < array.Count());
        }
        public static bool IsStringEmpty(string check)
        {
            if (string.IsNullOrEmpty(check.Trim()) || string.IsNullOrWhiteSpace(check.Trim()))
            {
                return true;
            } else
            {
                return false;
            }
        }
        public static bool HasProperty(this Type obj, string propertyName)
        {
            return obj.GetProperty(propertyName) != null;
        }
        public static string Truncate(this string str, int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("length", "Length must be >= 0");
            }
            if (str == null)
            {
                return null;
            }

            int maxLength = Math.Min(str.Length, length);
            return str.Substring(0, maxLength);
        }
        public static XmlDocument GetXmlDocument(string path)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(path);
                return doc;
            }
            catch (WebException e)
            {
                XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                doc.AppendChild(docNode);
                return doc;
            }
            finally { doc = null; }
        }
        public static bool IsNetworkAvailable()
        {
            bool connection;
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable() == true)
            {
                connection = true;
            }
            else
            {
                connection = false;
            }
            return connection;
        }

    }
}
