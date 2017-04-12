using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace Module.Utils
{
    public enum ConvertMode
    {
        Convert16To32 = 0,
        Convert16To64 = 1,
        Convert32To16 = 2,
        Convert32To64 = 3,
        Convert64To16 = 4,
        Convert64To32 = 5
    }

    public class Parser
    {
        #region ����ת��
        private static readonly string[] arr_16 = new string[] { "0000", "0001", "0010", "0011", "0100", "0101", "0110", "0111", "1000", "1001", "1010", "1011", "1100", "1101", "1110", "1111" };

        private static readonly string[] arr_32 = new string[] { "00000", "00001", "00010", "00011", "00100", "00101", "00110", "00111", "01000", "01001", "01010", "01011", "01100", "01101", "01110", "01111", "10000", "10001", "10010", "10011", "10100", "10101", "10110", "10111", "11000", "11001", "11010", "11011", "11100", "11101", "11110", "11111" };

        private static readonly string[] arr_64 = new string[] { "000000", "000001", "000010", "000011", "000100", "000101", "000110", "000111", "001000", "001001", "001010", "001011", "001100", "001101", "001110", "001111", "010000", "010001", "010010", "010011", "010100", "010101", "010110", "010111", "011000", "011001", "011010", "011011", "011100", "011101", "011110", "011111", "100000", "100001", "100010", "100011", "100100", "100101", "100110", "100111", "101000", "101001", "101010", "101011", "101100", "101101", "101110", "101111", "110000", "110001", "110010", "110011", "110100", "110101", "110110", "110111", "111000", "111001", "111010", "111011", "111100", "111101", "111110", "111111" };

        private static readonly string[] char_arr = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "+", "-" };

        private static int IndexOf(string[] list, string s)
        {
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i] == s)
                    return i;
            }
            return -1;
        }
        /// <summary>
        /// ����ת�����ṩ16��32��64���ƻ�ת
        /// </summary>
        /// <param name="str"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static string ConvertString(string str, ConvertMode mode)
        {
            string[] FromArr = null;
            string[] ToArr = null;
            int bit = 0;
            switch (mode)
            {
                case ConvertMode.Convert16To32:
                    FromArr = arr_16;
                    ToArr = arr_32;
                    bit = 5;
                    break;
                case ConvertMode.Convert16To64:
                    FromArr = arr_16;
                    ToArr = arr_64;
                    bit = 6;
                    break;
                case ConvertMode.Convert32To16:
                    FromArr = arr_32;
                    ToArr = arr_16;
                    bit = 4;
                    break;
                case ConvertMode.Convert32To64:
                    FromArr = arr_32;
                    ToArr = arr_64;
                    bit = 6;
                    break;
                case ConvertMode.Convert64To16:
                    FromArr = arr_64;
                    ToArr = arr_16;
                    bit = 4;
                    break;
                case ConvertMode.Convert64To32:
                    FromArr = arr_64;
                    ToArr = arr_32;
                    bit = 5;
                    break;

            }

            StringBuilder result_str = new StringBuilder();
            StringBuilder s = new StringBuilder();
            //ת2�����ַ�
            for (int i = 0; i < str.Length; i++)
            {
                s.Append(FromArr[IndexOf(char_arr, str[i].ToString())]);
            }
            //����bitλ��0
            while (s.Length % bit != 0)
            {
                s.Insert(0, '0');
            }
            str = s.ToString();
            //ÿbit���ַ����һ��ToArr���Ƶ��ַ�
            for (int i = 0; i < str.Length; i += bit)
            {
                result_str.Append(char_arr[IndexOf(ToArr, str.Substring(i, bit))]);
            }
            //�������ǰ���0
            while (result_str[0] == '0')
            {
                result_str.Remove(0, 1);
            }
            return result_str.ToString();
        }

        #endregion

        #region Convertת��
        /// <summary>
        /// objectתInt��Ĭ�Ϸ���-1
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ParseInt(object obj)
        {
            int reInt = -1;
            if (obj != null)
            {
                if (!(int.TryParse(obj.ToString(), out reInt)))
                {
                    return -1;
                }
            }
            return reInt;
        }

        /// <summary>
        /// objectתInt��Ĭ�Ϸ���result
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ParseInt(object obj, int result)
        {
            int reInt = result;
            if (obj != null)
            {
                if (!(int.TryParse(obj.ToString(), out reInt)))
                {
                    return result;
                }
            }
            return reInt;
        }

        /// <summary>
        /// ��objectת��ΪLong��Ĭ�Ϸ���-1
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static long ParserLong(object obj)
        {
            long reLong = -1;
            if (obj != null)
            {
                if (!(long.TryParse(obj.ToString(), out reLong)))
                    return -1;
            }
            return reLong;
        }

        /// <summary>
        /// objectתstring��Ĭ�Ϸ��ؿ�
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ParserString(object obj)
        {
            string reInt = "";
            if (obj != null)
                reInt = obj.ToString();
            return reInt;
        }

        /// <summary>
        /// objectתDobule��Ĭ�Ϸ���-1
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Double ParserDouble(object obj)
        {
            Double reInt = -1;
            if (obj != null)
                Double.TryParse(obj.ToString(), out reInt);
            return reInt;
        }

        /// <summary>
        /// objectתDateTime��Ĭ�Ϸ���1900-1-1 0:0:0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime ParseDateTime(object obj)
        {
            DateTime reInt = new DateTime(1900, 1, 1, 0, 0, 0);
            if (obj != null)
                DateTime.TryParse(obj.ToString(), out reInt);
            return reInt;
        }

        public static bool ParserBool(string s)
        {
            if (s == "1")
                return true;
            else
                return false;
        }

        public static bool GetBitValue(object obj)
        {
            bool flag = false;
            if (obj != null)
            {
                if (!(bool.TryParse(obj.ToString(), out flag)))
                {
                    return flag;
                }
            }
            return flag;
        }
        #endregion

        #region �ַ�������
        /// <summary>
        /// ���ַ���
        /// </summary>
        /// <param name="s">�����ַ�</param>
        /// <param name="l">���볤��,���ĳ��ȡ�2</param>
        /// <returns>���ؽ��</returns>
        public static string getStr(string inputString, int len)
        {
            if (string.IsNullOrEmpty(inputString))
            {
                return "";
            }

            len = len - 2;

            ASCIIEncoding ascii = new ASCIIEncoding();
            int tempLen = 0;
            string tempString = "";
            byte[] s = ascii.GetBytes(inputString);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                {
                    tempLen += 2;
                }
                else
                {
                    tempLen += 1;
                }

                try
                {
                    tempString += inputString.Substring(i, 1);
                }
                catch
                {
                    break;
                }

                if (tempLen > len)
                    break;
            }
            //����ع�����ϰ��ʡ�Ժ� 
            byte[] mybyte = System.Text.Encoding.Default.GetBytes(inputString);
            if (mybyte.Length > len)
                tempString += "...";


            return tempString;
        }
        public static void GetOrderData(DataTable data)
        {
            int i = 0;
            data.Columns.Add("RowNumber", typeof(int));
            foreach (DataRow row in data.Rows)
            {
                row["RowNumber"] = ++i;
            }
            data.AcceptChanges();
        }


        /// <summary>
        /// ����Ƿ���XSS�ű�������в����False������в����True
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static bool XssValidate(string inputString)
        {
            bool result;
            if (inputString.Contains("<") || inputString.Contains("&lt") || inputString.Contains(">") || inputString.Contains("&gt") || inputString.Contains("\"") || inputString.Contains("&quot") || inputString.Contains("\'") || inputString.ToLower().IndexOf("viewstate") > -1)
            {
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }

        public static bool ContainXssDengerChar(string str)
        {
            var dengerCharList = new[] { "~", "<", "&lt", ">", "&gt", "\"", "&quot", "\'", "viewstate", "|", "\\", "0x0a", "0x0d", ";", ")", "%28", "%29", "(", "~", "%7e", "&#126;" };

            return dengerCharList.Any(str.Contains);
        }

        /// <summary>
        /// Random ʵ��
        /// </summary>
        public static readonly Random globalRandom = new Random();

        /// <summary>
        /// ��ȡ�ַ������ȣ�����replaceStr �滻��������
        /// </summary>
        /// <param name="obj">�������</param>
        /// <param name="replaceStr">�滻����</param>
        /// <returns></returns>
        public static string GetHidenFetionId(string obj, string replaceStr)
        {
            string result = "";
            if (obj != null)
            {
                while (obj.Length < 9)
                {
                    obj += "0";
                }
                result = obj.Substring(0, 3);
                result += replaceStr;
                result += obj.Substring(6);
            }
            return result;
        }
        /// <summary>
        /// ��ȡ�ֻ��ַ������ȣ�����replaceStr �滻��������
        /// </summary>
        /// <param name="obj">�������</param>
        /// <param name="replaceStr">�滻����</param>
        /// <returns></returns>
        public static string GetHidenMobile(string obj, string replaceStr)
        {
            string result = "";
            if (obj != null)
            {
                while (obj.Length < 11)
                {
                    obj += "0";
                }
                result = obj.Substring(0, 3);
                result += replaceStr;
                result += obj.Substring(7);
            }
            return result;
        }

        /// <summary>
        /// �ж����ַ����Ƿ������ͬ��ֵ�����Դ�Сд
        /// </summary>
        /// <param name="strA"></param>
        /// <param name="strB"></param>
        /// <returns></returns>
        public static bool CheckEquals(string strA, string strB)
        {
            return string.Equals(strA, strB, StringComparison.InvariantCultureIgnoreCase);
        }

        #endregion

        /// <summary>
        /// XML���ˣ������ַ����еļ����ţ������ţ���б�ܣ����з���
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string ReplaceStr(string inputString)
        {
            string s = "";
            if (inputString != null)
            {
                s = inputString.Replace("<", "&lt;");
                s = s.Replace(">", "&gt;");
                s = s.Replace("'", "��");
                s = s.Replace("\r", "");
                s = s.Replace("\n", "");
                s = s.Replace("\"", "&quot;");
                s = s.Replace("\\", "");
            }

            return s;
        }
 


        #region Url��ַ���롢����
        public static string UrlHtmlEncode(string str)
        {
            return System.Web.HttpContext.Current.Server.HtmlEncode(str);
        }
        public static string UrlHtmlDecode(string str)
        {
            return System.Web.HttpContext.Current.Server.HtmlDecode(str);
        }
        #endregion
    }

}
