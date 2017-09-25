using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace AVE
{
    public class rc4 //extends Applet
    {
        internal string sTempUN = "";
        internal string sTempPW = "";
        internal int[] sbox = new int[256];
        internal int[] KEY = new int[256];
        internal string sUser = "";
        internal string sPassw = "";

        public virtual void RC4Initialize(string strPwd)
        {
            int tempSwap = 0;
            int i = 0;
            int b = 0;
            int intLength = 0;

            intLength = strPwd.Length;
            for (i = 0; i <= 255; i++) // For a = 0 To 255
            {
                KEY[i] = (int)(strPwd[i % intLength]);
                sbox[i] = i;
            }

            b = 0;
            for (i = 0; i <= 255; i++) // For a = 0 To 255
            {
                b = (b + sbox[i] + KEY[i]) % 256;
                tempSwap = sbox[i];
                sbox[i] = sbox[b];
                sbox[b] = tempSwap;
            }
        }


        public virtual string Salaa(string plaintxt, string key)
        {
            RC4Initialize(key);
            int temp = 0;
            int a = 0;
            int i = 0;
            int j = 0;
            int k;
            int cipherby = 0;
            string cipher = "";
            //psw = "&h01 &h23 &h45 &h67 &h89 &hab &hcd &hef"; //kiinte� avain
            //RC4Initialize(psw);

            for (a = 0; a < plaintxt.Length; a++)
            {
                i = (i + 1) % 256;
                j = (j + sbox[i]) % 256;
                temp = sbox[i];
                sbox[i] = sbox[j];
                sbox[j] = temp;

                k = sbox[(sbox[i] + sbox[j]) % 256];

                cipherby = ((int)(plaintxt[a])) ^ k;
                cipher += (char)cipherby;
            }
            return cipher;
        }
        public virtual string Pura(string plaintxt, string key)
        {
            RC4Initialize(key);
            int temp = 0;
            int a = 0;
            int i = 0;
            int j = 0;
            int k;
            int cipherby = 0;
            string cipher = "";

            for (a = 0; a < plaintxt.Length; a++)
            {
                i = (i + 1) % 256;
                j = (j + sbox[i]) % 256;
                temp = sbox[i];
                sbox[i] = sbox[j];
                sbox[j] = temp;

                k = sbox[(sbox[i] + sbox[j]) % 256];

                cipherby = ((int)(plaintxt[a])) ^ k;
                cipher += (char)cipherby;
            }
            return cipher;
        }

        public virtual string Avain()
        {
            // Testaaminen
            string Avain = "";
            for (int i = 0; i <= 255; i++) // For a = 0 To 255
            {
                Avain += (char)KEY[i];
            }
            return Avain;
        }
        public virtual string hexStringToString(string s)
        {
            string Result = "";
            sbyte[] b = new sbyte[s.Length / 2];
            for (int i = 0; i < b.Length; i++)
            {
                int index = i * 2;
                int v = Convert.ToInt32(s.Substring(index, 2), 16);
                Result += (char)v;
            }
            return Result;
        }

        public virtual string StringToHexString(string b)
        {
            StringBuilder sb = new StringBuilder(b.Length * 2);
            for (int i = 0; i < b.Length; i++)
            {
                int v = b[i] & 0xff;
                if (v < 16)
                {
                    sb.Append('0');
                }
                sb.Append(v.ToString("x"));
            }
            return sb.ToString().ToUpper();
        }
    }
}