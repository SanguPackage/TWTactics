/*
 * Serializer.cs
 * This is the Serializer class for the PHPSerializationLibrary
 *  
 * Copyright 2004 Conversive, Inc.
 * 
 *  http://sourceforge.net/projects/csphpserial/
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TribalWars.Tools
{
    /// <summary>
    /// Serializer Class.
    /// </summary>
    public class Serializer
    {
        //types:
        // N = null
        // s = string
        // i = int
        // d = double
        // a = array (hashtable)

        //private Dictionary<Hashtable, bool> _seenHashtables; //for serialize (to infinte prevent loops)
        //private Dictionary<ArrayList, bool> _seenArrayLists; //for serialize (to infinte prevent loops) lol

        private int _pos; //for unserialize

        public const bool XmlSafe = true; //This member tells the serializer wether or not to strip carriage returns from strings when serializing and adding them back in when deserializing
        //http://www.w3.org/TR/REC-xml/#sec-line-ends

        private readonly Encoding _stringEncoding = new UTF8Encoding();

        private readonly System.Globalization.NumberFormatInfo _nfi;

        public Serializer()
        {
            _nfi = new System.Globalization.NumberFormatInfo();
            _nfi.NumberGroupSeparator = "";
            _nfi.NumberDecimalSeparator = ".";
        }

        public object Deserialize(string str)
        {
            _pos = 0;
            return DeserializeCore(str);
        }

        private object DeserializeCore(string str)
        {
            if (str == null || str.Length <= _pos)
                return new Object();

            int start, end, length;
            string stLen;
            switch (str[_pos])
            {
                case 'N':
                    _pos += 2;
                    return null;
                case 'b':
                    char chBool = str[_pos + 2];
                    _pos += 4;
                    return chBool == '1';
                case 'i':
                    start = str.IndexOf(":", _pos) + 1;
                    end = str.IndexOf(";", start);
                    string stInt = str.Substring(start, end - start);
                    _pos += 3 + stInt.Length;
                    object oRet;
                    try
                    {
                        //firt try to parse as int
                        oRet = Int32.Parse(stInt, _nfi);
                    }
                    catch
                    {
                        //if it failed, maybe it was too large, parse as long
                        oRet = Int64.Parse(stInt, _nfi);
                    }
                    return oRet;
                case 'd':
                    start = str.IndexOf(":", _pos) + 1;
                    end = str.IndexOf(";", start);
                    string stDouble = str.Substring(start, end - start);
                    _pos += 3 + stDouble.Length;
                    return Double.Parse(stDouble, _nfi);
                case 's':
                    start = str.IndexOf(":", _pos) + 1;
                    end = str.IndexOf(":", start);
                    stLen = str.Substring(start, end - start);
                    int bytelen = Int32.Parse(stLen);
                    length = bytelen;
                    //This is the byte length, not the character length - so we might  
                    //need to shorten it before usage. This also implies bounds checking
                    if ((end + 2 + length) >= str.Length) length = str.Length - 2 - end;
                    string stRet = str.Substring(end + 2, length);
                    while (_stringEncoding.GetByteCount(stRet) > bytelen)
                    {
                        length--;
                        stRet = str.Substring(end + 2, length);
                    }
                    _pos += 6 + stLen.Length + length;
                    if (XmlSafe)
                    {
                        stRet = stRet.Replace("\n", "\r\n");
                    }
                    return stRet;
                case 'a':
                    //if keys are ints 0 through N, returns an ArrayList, else returns Hashtable
                    start = str.IndexOf(":", _pos) + 1;
                    end = str.IndexOf(":", start);
                    stLen = str.Substring(start, end - start);
                    length = Int32.Parse(stLen);
                    var htRet = new Hashtable(length);
                    var alRet = new ArrayList(length);
                    _pos += 4 + stLen.Length; //a:Len:{
                    for (int i = 0; i < length; i++)
                    {
                        //read key
                        object key = DeserializeCore(str);
                        //read value
                        object val = DeserializeCore(str);

                        if (alRet != null)
                        {
                            if (key is int && (int) key == alRet.Count)
                                alRet.Add(val);
                            else
                                alRet = null;
                        }
                        htRet[key] = val;
                    }
                    _pos++; //skip the }
                    if (_pos < str.Length && str[_pos] == ';') //skipping our old extra array semi-colon bug (er... php's weirdness)
                        _pos++;
                    if (alRet != null)
                        return alRet;
                    return htRet;
                default:
                    return "";
            }
        }
    }
}