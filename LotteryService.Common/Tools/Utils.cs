using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace LotteryService.Common.Tools
{
    public class Utils
    {
        #region 时间 时间戳

        /// <summary>
        /// unix时间戳转换成日期
        /// </summary>
        /// <param name="timestamp">时间戳（秒）</param>
        /// <returns></returns>
        public static DateTime UnixTimestampToDateTime(long timestamp)
        {
            //create a new DateTime value based on the Unix Epoch
            DateTime converted = new DateTime(1970, 1, 1, 0, 0, 0, 0);

            //add the timestamp to the value
            DateTime newDateTime = converted.AddSeconds(timestamp);

            //return the value in string format
            return newDateTime.ToLocalTime();
        }

        #endregion

        #region 哈希 加密

        /// <summary>
        /// SHA256哈希
        /// </summary>
        /// <param name="instr">要加密的字符串</param>
        /// <returns>64位大写SHA256哈希值</returns>
        public static string EncryptSHA256(string instr)
        {
            return EncryptSHA256(instr, Encoding.UTF8);
        }

        /// <summary>
        /// SHA256哈希
        /// </summary>
        /// <param name="instr">要加密的字符串</param>
        /// <param name="bytesEncoding">编码</param>
        /// <returns>64位大写SHA256哈希值</returns>
        public static string EncryptSHA256(string instr, Encoding bytesEncoding)
        {
            byte[] toByte = EncryptSHA256ToBytes(instr, bytesEncoding);
            string result = BitConverter.ToString(toByte).ToUpper().Replace("-", "");
            return result;
        }

        /// <summary>
        /// SHA256哈希
        /// </summary>
        /// <param name="instr">要加密的字符串</param>
        /// <param name="bytesEncoding">编码</param>
        /// <returns>32长度的字节数组</returns>
        public static byte[] EncryptSHA256ToBytes(string instr, Encoding bytesEncoding)
        {
            byte[] toByte = bytesEncoding.GetBytes(instr);
            using (SHA256Managed sha256 = new SHA256Managed())
            {
                toByte = sha256.ComputeHash(toByte);
                return toByte;
            }
        }

        /// <summary>
        /// SHA哈希
        /// </summary>
        /// <param name="instr">要加密的字符串</param>
        /// <returns>40位大写SHA哈希值</returns>
        public static string EncryptSHA(string instr)
        {
            return EncryptSHA(instr, Encoding.UTF8);
        }

        /// <summary>
        /// SHA哈希
        /// </summary>
        /// <param name="instr">要加密的字符串</param>
        /// <param name="bytesEncoding">编码</param>
        /// <returns>40位大写SHA哈希值</returns>
        public static string EncryptSHA(string instr, Encoding bytesEncoding)
        {
            try
            {
                byte[] toByte = EncryptSHAToBytes(instr, bytesEncoding);
                string result = BitConverter.ToString(toByte).ToLower().Replace("-", "");
                return result;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// SHA哈希
        /// </summary>
        /// <param name="instr">要加密的字符串</param>
        /// <param name="bytesEncoding">编码</param>
        /// <returns>20长度的字节数组</returns>
        public static byte[] EncryptSHAToBytes(string instr, Encoding bytesEncoding)
        {
            SHA1Managed sha = null;
            try
            {
                byte[] toByte = bytesEncoding.GetBytes(instr);
                sha = new SHA1Managed();
                toByte = sha.ComputeHash(toByte);
                return toByte;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (sha != null)
                    sha.Clear();
            }
        }



        /// <summary>
        /// MD5哈希
        /// </summary>
        /// <param name="instr">要加密的字符串</param>
        /// <returns>32位大写MD5哈希值</returns>
        public static string EncryptMD5(string instr)
        {
            return EncryptMD5(instr, Encoding.UTF8);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string StrToMD5(string str)
        {
            byte[] data = EncryptMD5ToBytes(str, Encoding.UTF8);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] OutBytes = md5.ComputeHash(data);

            string OutString = "";
            for (int i = 0; i < OutBytes.Length; i++)
            {
                OutString += OutBytes[i].ToString("x2");
            }
            // return OutString.ToUpper();
            return OutString.ToLower();
        }

        public static string EncryptToSHA1(string str)
        {
            using (SHA1 sha1 = new SHA1CryptoServiceProvider())
            {
                byte[] bytes_sha1_in = System.Text.UTF8Encoding.Default.GetBytes(str);
                byte[] bytes_sha1_out = sha1.ComputeHash(bytes_sha1_in);
                string signature = BitConverter.ToString(bytes_sha1_out);
                signature = signature.Replace("-", "").ToLower();
                return signature;
            }
        }


        public static string EncryptMD5_1(string instr)
        {
            var md5 = MD5.Create();
            var bs = md5.ComputeHash(Encoding.UTF8.GetBytes(instr));
            var sb = new StringBuilder();
            foreach (byte b in bs)
            {
                sb.Append(b.ToString("x2"));
            }
            //所有字符转为大写
            return sb.ToString().ToUpper();
        }


        /// <summary>
        /// MD5哈希
        /// </summary>
        /// <param name="instr">要加密的字符串</param>
        /// <param name="bytesEncoding">字节流编码</param>
        /// <returns>32位大写MD5哈希值</returns>
        public static string EncryptMD5(string instr, Encoding bytesEncoding)
        {
            try
            {
                byte[] toByte = EncryptMD5ToBytes(instr, bytesEncoding);
                string result = BitConverter.ToString(toByte).ToUpper().Replace("-", "");
                return result;
            }
            catch
            {
                return "";
            }
        }


        // MD5哈希
        /// </summary>
        /// <param name="instr">要加密的字符串</param>
        /// <param name="bytesEncoding">字节流编码</param>
        /// <returns>16长度的MD5哈希字节数组</returns>
        public static byte[] EncryptMD5ToBytes(string instr, Encoding bytesEncoding)
        {
            MD5CryptoServiceProvider md5 = null;
            try
            {
                byte[] toByte = bytesEncoding.GetBytes(instr);
                md5 = new MD5CryptoServiceProvider();
                toByte = md5.ComputeHash(toByte);
                return toByte;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (md5 != null)
                    md5.Clear();
            }
        }

        #endregion

        #region 字符串

        /// <summary>  
        /// 判断输入的字符串只包含汉字  
        /// </summary>  
        /// <param name="input"></param>  
        /// <returns></returns>  
        public static bool IsChineseCh(string input)
        {
            Regex regex = new Regex("^[\u4e00-\u9fa5]+$");
            return regex.IsMatch(input);
        }


        /// <summary>  
        /// 匹配3位或4位区号的电话号码，其中区号可以用小括号括起来，  
        /// 也可以不用，区号与本地号间可以用连字号或空格间隔，  
        /// 也可以没有间隔  
        /// \(0\d{2}\)[- ]?\d{8}|0\d{2}[- ]?\d{8}|\(0\d{3}\)[- ]?\d{7}|0\d{3}[- ]?\d{7}  
        /// </summary>  
        /// <param name="input"></param>  
        /// <returns></returns>  
        public static bool IsPhone(string input)
        {
            string pattern = "^\\(0\\d{2}\\)[- ]?\\d{8}$|^0\\d{2}[- ]?\\d{8}$|^\\(0\\d{3}\\)[- ]?\\d{7}$|^0\\d{3}[- ]?\\d{7}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        }

        /// <summary>  
        /// 判断输入的字符串是否是一个合法的手机号  
        /// </summary>  
        /// <param name="input"></param>  
        /// <returns></returns>  
        public static bool IsMobilePhone(string input)
        {
            Regex regex = new Regex("^(13[0-9]|15[012356789]|17[013678]|18[0-9]|14[57])[0-9]{8}$");
            return regex.IsMatch(input);

        }

        /// <summary>
        /// 判断输入的字符串是否是一个合法的Email地址
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsEmail(string input)
        {
            string pattern = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        }

        public static bool IsLegalUserName(string input)
        {
            Regex regex = new Regex("^[a-zA-z][a-zA-Z0-9_]{2,16}$");
            return regex.IsMatch(input);
        }


        #endregion

        #region Read Config 

        public static string GetConfigValuesByKey(string key)
        {

            string values = ConfigurationManager.AppSettings[key];
            if (values == null)
            {
                throw new Exception(string.Format("应用程序中没有key为{0}的设置", key));
            }
            return values;
        }


        #endregion

        #region 生成随机密码/字符串

        /// <summary>
        /// 生成随机数的种子
        /// </summary>
        /// <returns></returns>
        private static int GetNewSeed()
        {
            byte[] rndBytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(rndBytes);
            return BitConverter.ToInt32(rndBytes, 0);
        }
             
        /// <summary>
        /// 生成8位随机数
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetRandomString(int len)
        {
            string s = "123456789abcdefghijklmnpqrstuvwxyzABCDEFGHIJKLMNPQRSTUVWXYZ";
            string reValue = string.Empty;
            Random rnd = new Random(GetNewSeed());
            while (reValue.Length < len)
            {
                string s1 = s[rnd.Next(0, s.Length)].ToString();
                if (reValue.IndexOf(s1) == -1) reValue += s1;
            }
            return reValue;
        }

        #endregion

        public static string GetBrowserInfo()
        {
           return HttpContext.Current.Request.Browser.Browser + " " + HttpContext.Current.Request.Browser.Version;
        }

        public static string GetOsInfo()
        {
            string agent = HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"];
            if (agent.IndexOf("NT 4.0", StringComparison.Ordinal) > 0)
                return "Windows NT ";
            if (agent.IndexOf("NT 5.0", StringComparison.Ordinal) > 0)
                return "Windows 2000";
            if (agent.IndexOf("NT 5.1", StringComparison.Ordinal) > 0)
                return "Windows XP";
            if (agent.IndexOf("NT 5.2", StringComparison.Ordinal) > 0)
                return "Windows 2003";
            if (agent.IndexOf("NT 6.0", StringComparison.Ordinal) > 0)
                return "Windows Vista";
            if (agent.IndexOf("NT 7.0", StringComparison.Ordinal) > 0)
                return "Windows 7";
            if (agent.IndexOf("NT 8.0", StringComparison.Ordinal) > 0)
                return "Windows 8";
            if (agent.IndexOf("NT 10.0", StringComparison.Ordinal) > 0)
                return "Windows 10";
            if (agent.IndexOf("WindowsCE", StringComparison.Ordinal) > 0)
                return "Windows CE";
            if (agent.IndexOf("NT", StringComparison.Ordinal) > 0)
                return "Windows NT ";
            if (agent.IndexOf("9x", StringComparison.Ordinal) > 0)
                return "Windows ME";
            if (agent.IndexOf("98", StringComparison.Ordinal) > 0)
                return "Windows 98";
            if (agent.IndexOf("95", StringComparison.Ordinal) > 0)
                return "Windows 95";
            if (agent.IndexOf("Win32", StringComparison.Ordinal) > 0)
                return "Win32";
            if (agent.IndexOf("Linux", StringComparison.Ordinal) > 0)
                return "Linux";
            if (agent.IndexOf("SunOS", StringComparison.Ordinal) > 0)
                return "SunOS";
            if (agent.IndexOf("Mac", StringComparison.Ordinal) > 0)
                return "Mac";
            if (agent.IndexOf("Linux", StringComparison.Ordinal) > 0)
                return "Linux";
            if (agent.IndexOf("Windows", StringComparison.Ordinal) > 0)
                return "Windows";
            return "未知类型";
        }

        public static T StringConvertEnum<T>(string enumStr)
            where T:  struct
        {
            return (T)Enum.Parse(typeof(T), enumStr);
        }
    }
}