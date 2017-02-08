using System;
using System.Web;

using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Security;
using System.Collections.Generic;
using System.Configuration;

/// <summary>
/// 字符串处理类
/// </summary>
public static class StringHelper
{
    /// <summary>
    /// html to text
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    public static string HtmlToText(this string str, string content)
    {
        StringBuilder sb = new StringBuilder(content);
        sb.Replace("<br />", "\n");
        sb.Replace("<br/>", "\n");
        //  sb.Replace("\r", "");
        sb.Replace("&nbsp;&nbsp;", "\t");
        sb.Replace("&nbsp;", " ");
        sb.Replace("&#39;", "\'");
        sb.Replace("&quot;", "\"");
        sb.Replace("&gt;", ">");
        sb.Replace("&lt;", "<");
        sb.Replace("&amp;", "&");

        return sb.ToString();
    }
    #region Request
    /// <summary>
    /// 獲取Post傳過來的值-返回string
    /// </summary>
    /// <param name="inputname">參數名稱</param>
    public static string PostInStr(string inputname)
    {
        string retStr = HttpContext.Current.Request.Form[inputname];  //Post
        if (string.IsNullOrEmpty(retStr))
        {
            retStr = "";
        }
        return retStr;
    }
    /// <summary>
    /// 獲取Post傳過來的值-返回int
    /// </summary>
    /// <param name="inputname">參數名稱</param>
    /// <param name="def">為空返回值</param>
    /// <returns></returns>
    public static int PostInInt(string inputname, int def)
    {
        string retStr = HttpContext.Current.Request.Form[inputname];  //Post
        if (!string.IsNullOrEmpty(retStr))
        {
            return StrToInt(retStr, def);
        }
        else
        {
            return def;
        }
    }
    /// <summary>
    /// 獲取Post傳過來的值-返回decimal
    /// </summary>
    /// <param name="inputname">參數名稱</param>
    /// <param name="def">為空返回值</param>
    /// <returns></returns>
    public static decimal PostInDecimal(string inputname, decimal def)
    {
        string retStr = HttpContext.Current.Request.Form[inputname];  //Post
        if (!string.IsNullOrEmpty(retStr))
        {
            return StrToDecimal(retStr, def);
        }
        else
        {
            return def;
        }
    }
    /// <summary>
    /// 獲取Post傳過來的值-返回DateTime
    /// </summary>
    /// <param name="inputname">參數名稱</param>
    /// <param name="def">為空返回值</param>
    /// <returns></returns>
    public static DateTime PostInDateTime(string inputname, DateTime def)
    {
        string retStr = HttpContext.Current.Request.Form[inputname];  //Post
        if (!string.IsNullOrEmpty(retStr))
        {
            return StrToDateTime(retStr, def);
        }
        else
        {
            return def;
        }
    }
    /// <summary>
    /// 獲取Post傳過來的值-返回float
    /// </summary>
    /// <param name="inputname">參數名稱</param>
    /// <param name="def">為空返回值</param>
    /// <returns></returns>
    public static float PostInFloat(string inputname, float def)
    {
        string retStr = HttpContext.Current.Request.Form[inputname];  //Post
        if (!string.IsNullOrEmpty(retStr))
        {
            return StrToFloat(retStr, def);
        }
        else
        {
            return def;
        }
    }
    /// <summary>
    /// 獲取Get傳過來的值-返回string
    /// </summary>
    /// <param name="inputname">參數名稱</param>
    /// <returns></returns>
    public static string GetInStr(string inputname)
    {
        string retStr = HttpContext.Current.Request.QueryString[inputname];  //Get
        if (string.IsNullOrEmpty(retStr))
        {
            retStr = "";
        }
        return retStr;
    }
    /// <summary>
    /// 獲取Get傳過來的值-返回int
    /// </summary>
    /// <param name="inputname">參數名稱</param>
    /// <param name="def">為空返回的值</param>
    /// <returns></returns>
    public static int GetInInt(string inputname, int def)
    {
        string retStr = HttpContext.Current.Request.QueryString[inputname];  //Get
        if (!string.IsNullOrEmpty(retStr))
        {
            return StrToInt(retStr, def);
        }
        else
        {
            return def;
        }
    }
    /// <summary>
    /// 獲取Get傳過來的值-返回decimal
    /// </summary>
    /// <param name="inputname">參數名稱</param>
    /// <param name="def">為空返回的值</param>
    /// <returns></returns>
    public static decimal GetInDecimal(string inputname, decimal def)
    {
        string retStr = HttpContext.Current.Request.QueryString[inputname];  //Get
        if (!string.IsNullOrEmpty(retStr))
        {
            return StrToDecimal(retStr, def);
        }
        else
        {
            return def;
        }
    }
    /// <summary>
    /// 獲取Get傳過來的值-返回DateTime
    /// </summary>
    /// <param name="inputname">參數名稱</param>
    /// <param name="def">為空返回值</param>
    /// <returns></returns>
    public static DateTime GetInDateTime(string inputname, DateTime def)
    {
        string retStr = HttpContext.Current.Request.QueryString[inputname];  //Post
        if (!string.IsNullOrEmpty(retStr))
        {
            return StrToDateTime(retStr, def);
        }
        else
        {
            return def;
        }
    }
    /// <summary>
    /// 獲取Get傳過來的值-返回float
    /// </summary>
    /// <param name="inputname">參數名稱</param>
    /// <param name="def">為空返回的值</param>
    /// <returns></returns>
    public static float GetInFloat(string inputname, float def)
    {
        string retStr = HttpContext.Current.Request.QueryString[inputname];  //Get
        if (!string.IsNullOrEmpty(retStr))
        {
            return StrToFloat(retStr, def);
        }
        else
        {
            return def;
        }
    }
    /// <summary>
    /// 獲取Post Or Get傳過來的值-返回string
    /// </summary>
    /// <param name="inputname">參數名稱</param>
    /// <returns></returns>
    public static string PostOrGetInStr(string inputname)
    {
        string retStr = HttpContext.Current.Request.Form[inputname];  //Post
        if (string.IsNullOrEmpty(retStr))
        {
            retStr = HttpContext.Current.Request.QueryString[inputname];  //Get
            if (string.IsNullOrEmpty(retStr))
            {
                retStr = "";
            }
        }
        return retStr;
    }

    /// <summary>
    /// 獲取Post Or Get傳過來的值-返回string
    /// </summary>
    /// <param name="inputname">參數名稱</param>
    /// <param name="def">為空返回的值</param>
    /// <returns></returns>
    public static int PostOrGetInInt(string inputname, int def)
    {
        string retStr = PostOrGetInStr(inputname);
        if (!string.IsNullOrEmpty(retStr))
        {
            return StrToInt(retStr, def);
        }
        else
        {
            return def;
        }
    }

    /// <summary>
    /// 獲取Post Or Get傳過來的值-返回decimal
    /// </summary>
    /// <param name="inputname">參數名稱</param>
    /// <param name="def">為空返回的值</param>
    /// <returns></returns>
    public static decimal PostOrGetInDecimal(string inputname, decimal def)
    {
        string retStr = PostOrGetInStr(inputname);
        if (!string.IsNullOrEmpty(retStr))
        {
            return StrToDecimal(retStr, def);
        }
        else
        {
            return def;
        }
    }
    /// <summary>
    /// 獲取Post Or Get傳過來的值-返回DateTime
    /// </summary>
    /// <param name="inputname">參數名稱</param>
    /// <param name="def">為空返回值</param>
    /// <returns></returns>
    public static DateTime PostOrGetInDateTime(string inputname, DateTime def)
    {
        string retStr = PostOrGetInStr(inputname);
        if (!string.IsNullOrEmpty(retStr))
        {
            return StrToDateTime(retStr, def);
        }
        else
        {
            return def;
        }
    }
    /// <summary>
    /// 獲取Post Or Get傳過來的值-返回float
    /// </summary>
    /// <param name="inputname">參數名稱</param>
    /// <param name="def">為空返回的值</param>
    /// <returns></returns>
    public static float PostOrGetInFloat(string inputname, float def)
    {
        string retStr = PostOrGetInStr(inputname);
        if (!string.IsNullOrEmpty(retStr))
        {
            return StrToFloat(retStr, def);
        }
        else
        {
            return def;
        }
    }
    #endregion

    #region AppSettings
    /// <summary>
    /// 返回string
    /// </summary>
    /// <param name="inputname">參數名稱</param>
    public static string AppSettingsInStr(string inputname)
    {
        string retStr = ConfigurationManager.AppSettings[inputname];
        if (string.IsNullOrEmpty(retStr))
        {
            retStr = "";
        }
        return retStr;
    }
    /// <summary>
    /// 返回int
    /// </summary>
    /// <param name="inputname">參數名稱</param>
    /// <param name="def">為空返回值</param>
    /// <returns></returns>
    public static int AppSettingsInInt(string inputname, int def)
    {
        string retStr = ConfigurationManager.AppSettings[inputname];
        if (!string.IsNullOrEmpty(retStr))
        {
            return StrToInt(retStr, def);
        }
        else
        {
            return def;
        }
    }
    /// <summary>
    /// 返回decimal
    /// </summary>
    /// <param name="inputname">參數名稱</param>
    /// <param name="def">為空返回值</param>
    /// <returns></returns>
    public static decimal AppSettingsInDecimal(string inputname, decimal def)
    {
        string retStr = ConfigurationManager.AppSettings[inputname];
        if (!string.IsNullOrEmpty(retStr))
        {
            return StrToDecimal(retStr, def);
        }
        else
        {
            return def;
        }
    }
    /// <summary>
    /// 返回DateTime
    /// </summary>
    /// <param name="inputname">參數名稱</param>
    /// <param name="def">為空返回值</param>
    /// <returns></returns>
    public static DateTime AppSettingsInDateTime(string inputname, DateTime def)
    {
        string retStr = ConfigurationManager.AppSettings[inputname];
        if (!string.IsNullOrEmpty(retStr))
        {
            return StrToDateTime(retStr, def);
        }
        else
        {
            return def;
        }
    }
    /// <summary>
    /// 返回float
    /// </summary>
    /// <param name="inputname">參數名稱</param>
    /// <param name="def">為空返回值</param>
    /// <returns></returns>
    public static float AppSettingsInFloat(string inputname, float def)
    {
        string retStr = ConfigurationManager.AppSettings[inputname];
        if (!string.IsNullOrEmpty(retStr))
        {
            return StrToFloat(retStr, def);
        }
        else
        {
            return def;
        }
    }
    /// <summary>
    /// 返回bool
    /// </summary>
    /// <param name="inputname">參數名稱</param>
    public static bool AppSettingsInBool(string inputname)
    {
        string retStr = ConfigurationManager.AppSettings[inputname];
        if (retStr == "1")
        {
            return true;
        }
        return false;
    }
    #endregion

    /// <summary>
    /// HtmlEncode
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    public static string HtmlEncode(string content)
    {
        return HttpContext.Current.Server.HtmlEncode(content);
    }

    /// <summary>
    ///  HtmlDecode
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    public static string HtmlDecode(string content)
    {
        return HttpContext.Current.Server.HtmlDecode(content);
    }

    public static decimal NewSort()
    {
        return StrToDecimal(DateTime.Now.ToString("yyyyMMddHHmmss"), 0);
    }

    /// <summary>
    /// 返回 URL 字符串的编码结果
    /// </summary>
    /// <param name="str">字符串</param>
    /// <returns>编码结果</returns>
    public static string UrlEncode(string str)
    {
        return HttpUtility.UrlEncode(str);
    }

    /// <summary>
    /// 返回 URL 字符串的编码结果
    /// </summary>
    /// <param name="str">字符串</param>
    /// <returns>解码结果</returns>
    public static string UrlDecode(string str)
    {
        return HttpUtility.UrlDecode(str);
    }

    /// <summary>
    /// 生成指定长度的字符串,即生成strLong个str字符串
    /// </summary>
    /// <param name="strLong">生成的长度</param>
    /// <param name="str">以str生成字符串</param>
    /// <returns></returns>
    public static string StringOfChar(int strLong, string str)
    {
        string returnStr = "";
        for (int i = 0; i < strLong; i++)
        {
            returnStr += str;
        }
        return returnStr;
    }

    /// <summary>
    /// 是否为ip
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    public static bool IsIP(string ip)
    {
        return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
    }

    /// <summary>
    /// 返回字符串真实长度, 1个汉字长度为2
    /// </summary>
    /// <returns></returns>
    public static int GetLength(string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return 0;
        }
        return Encoding.Default.GetBytes(str).Length;
    }

    /// <summary>
    /// 编码成 sql 文本可以接受的格式
    /// </summary>
    public static string SqlEncode(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }
        return s.Trim().Replace("'", "''");
    }

    /// <summary>
    /// 检测是否有Sql危险字符
    /// 没有返回true
    /// </summary>
    /// <param name="str">要判断字符串</param>
    /// <returns>判断结果</returns>
    public static bool IsSafeSqlString(string str)
    {
        return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
    }

    /// <summary>
    /// 从字符串的指定位置截取指定长度的子字符串(过时)
    /// </summary>
    /// <param name="str">原字符串</param>
    /// <param name="startIndex">子字符串的起始位置</param>
    /// <param name="length">子字符串的长度</param>
    /// <returns>子字符串</returns>
    public static string CutString(string str, int startIndex, int length)
    {
        return CutString(str, startIndex, length, string.Empty);
    }

    /// <summary>
    /// 从字符串的指定位置开始截取到字符串结尾的了符串
    /// </summary>
    /// <param name="str">原字符串</param>
    /// <param name="length"></param>
    /// <returns>子字符串</returns>
    public static string CutString(string str, int length)
    {
        return CutString(str, 0, length, string.Empty);
    }

    /// <summary>
    /// 截取字符
    /// </summary>
    /// <param name="str"></param>
    /// <param name="length"></param>
    /// <param name="def"></param>
    /// <returns></returns>
    public static string CutString(string str, int length, string def)
    {
        return CutString(str, 0, length, def);
    }

    /// <summary>
    /// 截取字符
    /// </summary>
    /// <param name="str"></param>
    /// <param name="startIndex"></param>
    /// <param name="length"></param>
    /// <param name="def"></param>
    /// <returns></returns>
    public static string CutString(string str, int startIndex, int length, string def)
    {
        if (string.IsNullOrEmpty(str))
        {
            return "";
        }
        str = ClearHTML(str);//清除html标签再截取字符串
        if (startIndex >= 0)
        {
            if (length < 0)
            {
                length = length * -1;
                if (startIndex - length < 0)
                {
                    length = startIndex;
                    startIndex = 0;
                }
                else
                {
                    startIndex = startIndex - length;
                }
            }
            if (startIndex > str.Length)
            {
                return "";
            }
        }
        else
        {
            if (length < 0)
            {
                return "";
            }
            else
            {
                if (length + startIndex > 0)
                {
                    length = length + startIndex;
                    startIndex = 0;
                }
                else
                {
                    return "";
                }
            }
        }

        if (str.Length - startIndex <= length)
        {
            length = str.Length - startIndex;
            def = string.Empty;
        }

        try
        {
            return str.Substring(startIndex, length) + def;
        }
        catch
        {
            return str + def;
        }
    }

    /// <summary>
    /// 移除Html标记
    /// </summary>
    /// <param name="Content"></param>
    /// <returns></returns>
    public static string RemoveHtml(string Content)
    {
        string regexstr = @"<[^>]*>";
        return Regex.Replace(Content, regexstr, string.Empty, RegexOptions.IgnoreCase).Trim();
    }

    /// <summary>
    /// 去除整个网页HTML标记 实测可用
    /// </summary>
    /// <param name="Htmlstring"></param>
    /// <returns></returns>
    public static string ClearHTML(string Htmlstring)
    {
        //删除脚本
        Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
        //删除HTML
        Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
        Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
        Htmlstring = Htmlstring.Replace("<", "");
        Htmlstring = Htmlstring.Replace(">", "");
        Htmlstring = Htmlstring.Replace("\r\n", "");
        Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
        return Htmlstring;
    }

    #region 删除HTML标签
    public static string ClearHTMLTags(string HTML)
    {
        string[] Regexs ={
                        @"<script[^>]*?>.*?</script>",
                        @"<(///s*)?!?((/w+:)?/w+)(/w+(/s*=?/s*(([""'])(//[""'tbnr]|[^/7])*?/7|/w+)|.{0})|/s)*?(///s*)?>",
                        @"([/r/n])[/s]+",
                        @"&(quot|#34);",
                        @"&(amp|#38);",
                        @"&(lt|#60);",
                        @"&(gt|#62);",
                        @"&(nbsp|#160);",
                        @"&(iexcl|#161);",
                        @"&(cent|#162);",
                        @"&(pound|#163);",
                        @"&(copy|#169);",
                        @"&#(/d+);",
                        @"-->",
                        @"<!--.*/n"
                            };

        string[] Replaces ={
                        "",
                        "",
                        "",
                        "\"",
                        "&",
                        "<",
                        ">",
                        "   ",
                        "\xa1",//chr(161),
                        "\xa2",//chr(162),
                        "\xa3",//chr(163),
                        "\xa9",//chr(169),
                        "",
                        "\r\n",
                        ""
                                };
        string s = HTML;
        for (int i = 0; i < Regexs.Length; i++)
        {
            s = new Regex(Regexs[i], RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(s, Replaces[i]);
        }
        s = s.Replace("<", "");
        s = s.Replace(">", "");
        s = s.Replace("/r/n", "");
        return s;
    }

    #endregion

    /// <summary>
    ///  判断字符串是否合法的日期格式
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsDate(string value)
    {
        try
        {
            System.DateTime.Parse(value);
        }
        catch
        {
            return false;
        }
        return true;
    }

    /// <summary>
    ///  判断字符串是否合法的日期格式
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsDate(object value)
    {
        if (value == null) return false;
        try
        {
            System.DateTime.Parse(value.ToString());
        }
        catch
        {
            return false;
        }
        return true;
    }

    /// <summary>
    ///  判断字符串是否合法的日期格式
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    public static string DateToString(object value, string format, string def)
    {
        if (value == null) return def;
        if (IsDate(value))
        {
            return Convert.ToDateTime(value).ToString(format);
        }
        return def;
    }

    /// <summary>
    /// 判断给定的字符串(strInt)是否是数值型
    /// </summary>
    /// <param name="strNumber">要确认的字符串</param>
    /// <returns>是则返加true 不是则返回 false</returns>
    public static bool IsNumber(string strNumber)
    {
        if (string.IsNullOrEmpty(strNumber))
        {
            return false;
        }
        //return new Regex(@"^([0-9])[0-9]*(\.\w*)?$").IsMatch(strInt);	//整数和小数
        return new Regex(@"^(0|[1-9]\d*)$").IsMatch(strNumber); //正整数
    }

    /// <summary>
    /// 判断给定的字符串數組(numberArray)中每個元素是否是数值型
    /// </summary>
    /// <param name="numberArray">要确认的字符串數組</param>
    /// <returns>是则返加true 不是则返回 false</returns>
    public static bool IsNumbers(string[] numberArray)
    {
        if (numberArray != null && numberArray.Length > 0)
        {
            Regex regEx = new Regex(@"^(0|[1-9]\d*)$"); //正整数
            foreach (var number in numberArray)
            {
                if (!regEx.IsMatch(number))
                {
                    return false;
                }
            }

            return true;
        }

        return false;
    }

    /// <summary>
    /// 判断给定的字符串(strFloat)是否是Float型
    /// </summary>
    /// <param name="strFloat">要确认的字符串</param>
    /// <returns>是则返加true 不是则返回 false</returns>
    public static bool IsFloat(string strFloat)
    {
        if (string.IsNullOrEmpty(strFloat))
        {
            return false;
        }
        return new Regex(@"^([0-9])[0-9]*(\.\w*)?$").IsMatch(strFloat); //整数和小数
                                                                        //return new Regex(@"^(0|[1-9]\d*)$").IsMatch(strInt);	//正整数
    }

    /// <summary>
    /// 检测是否符合email格式
    /// </summary>
    /// <param name="strEmail">要判断的email字符串</param>
    /// <returns>判断结果</returns>
    public static bool IsEmail(string strEmail)
    {
        return Regex.IsMatch(strEmail, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
    }

    /// <summary>
    /// 检测是否符合域名格式
    /// </summary>
    /// <param name="strEmail">要判断的域名字符串</param>
    /// <returns>判断结果</returns>
    public static bool IsDomain(string strDomain)
    {                                    //   ([a-z0-9][a-z0-9\-]*?\.(?:com|cn|net|org|gov|info|la|cc|co)(?:\.(?:cn|jp))?)$
        return Regex.IsMatch(strDomain, @"^(?<=://[/w-]+/.)([/w-]+/.)+[/w-]+(?<=/?)$");
    }

    /// <summary>
    /// string型转换为byte型,转换失败返回缺省值
    /// </summary>
    /// <param name="str">要转换的字符串</param>
    /// <param name="def">缺省值</param>
    /// <returns>转换后的byte类型结果</returns>
    public static byte StrToByte(string str, byte def = 0)
    {
        byte result;
        if (!byte.TryParse(str, out result))
        {
            return def;
        }
        return result;
    }

    /// <summary>
    /// string型转换为ushort型,转换失败返回缺省值
    /// </summary>
    /// <param name="str">要转换的字符串</param>
    /// <param name="def">缺省值</param>
    /// <returns>转换后的ushort类型结果</returns>
    public static ushort StrToUShort(string str, ushort def)
    {
        ushort result;

        if (!ushort.TryParse(str, out result))
        {
            return def;
        }
        return result;
    }

    /// <summary>
    /// string型转换为int型,转换失败返回缺省值
    /// </summary>
    /// <param name="str">要转换的字符串</param>
    /// <param name="def">缺省值</param>
    /// <returns>转换后的int类型结果</returns>
    public static int StrToInt(this string str, int def = 0)
    {

        int result;
        if (!int.TryParse(str, out result))
        {
            return def;
        }
        return result;
    }

    /// <summary>
    /// string型转换为uint型,转换失败返回缺省值
    /// </summary>
    /// <param name="str">要转换的字符串</param>
    /// <param name="def">缺省值</param>
    /// <returns>转换后的uint类型结果</returns>
    public static uint StrToUInt(string str, uint def = 0)
    {
        uint result;
        if (!uint.TryParse(str, out result))
        {
            return def;
        }
        return result;
    }

    /// <summary>
    /// string型转换为float型,转换失败返回缺省值
    /// </summary>
    /// <param name="str">要转换的字符串</param>
    /// <param name="def">缺省值</param>
    /// <returns>转换后的float类型结果</returns>
    public static float StrToFloat(string str, float def = 0)
    {
        float result;
        if (!float.TryParse(str, out result))
        {
            return def;
        }
        return result;
    }
    public static double StrToDouble(this string str, double def = 0)
    {
        double result;
        if (!double.TryParse(str, out result))
        {
            return def;
        }
        return result;
    }

    /// <summary>
    /// string型转换为decimal型,转换失败返回缺省值
    /// </summary>
    /// <param name="str">要转换的字符串</param>
    /// <param name="def">缺省值</param>
    /// <returns>转换后的decimal类型结果</returns>
    public static decimal StrToDecimal(string str, decimal def = 0)
    {
        decimal result;
        if (!decimal.TryParse(str, out result))
        {
            return def;
        }
        return result;
    }

    /// <summary>
    /// string型转换为DateTime型,转换失败返回缺省值
    /// </summary>
    /// <param name="str">要转换的字符串</param>
    /// <param name="def">缺省值</param>
    /// <returns>转换后的DateTime类型结果</returns>
    public static DateTime StrToDateTime(string str, DateTime def)
    {
        DateTime result;
        if (!DateTime.TryParse(str, out result))
        {
            return def;
        }
        return result;
    }

    /// <summary>
    /// MD5加密
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string GetMD5(string str)
    {
        if (str == null)
        {
            str = string.Empty;
        }
#pragma warning disable CS0618 // Type or member is obsolete
        return FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
#pragma warning restore CS0618 // Type or member is obsolete
    }

    /// <summary>
    /// 返回URL中结尾的文件名
    /// </summary>
    public static string GetFileName(string url)
    {
        if (url == null)
        {
            return "";
        }
        //是否有参数
        if (url.IndexOf("?") != -1)
        {
            //去掉参数
            string noquery = url.Substring(0, url.IndexOf("?"));

            //根据/分组
            string[] filenames = noquery.Split(new char[] { '/' });

            //文件名
            string filename = filenames[filenames.Length - 1];

            return filename;
        }
        else
        {
            return System.IO.Path.GetFileName(url);
        }
    }

    /// <summary>
    /// 将时间换成中文
    /// </summary>
    /// <param name="datetime">时间</param>
    /// <returns></returns>
    public static string DateToChineseString(DateTime datetime)
    {
        TimeSpan ts = DateTime.Now - datetime;
        if ((int)ts.TotalDays >= 365)
        {
            return (int)ts.TotalDays / 365 + "年前";
        }
        if ((int)ts.TotalDays >= 30 && ts.TotalDays <= 365)
        {
            return (int)ts.TotalDays / 30 + "月前";
        }
        if ((int)ts.TotalDays == 1)
        {
            return "昨天";
        }
        if ((int)ts.TotalDays == 2)
        {
            return "前天";
        }
        if ((int)ts.TotalDays >= 3 && ts.TotalDays <= 30)
        {
            return (int)ts.TotalDays + "天前";
        }
        if ((int)ts.TotalDays == 0)
        {
            if ((int)ts.TotalHours != 0)
            {
                return (int)ts.TotalHours + "小时前";
            }
            else
            {
                if ((int)ts.TotalMinutes == 0)
                {
                    return "1分钟前";
                }
                else
                {
                    return (int)ts.TotalMinutes + "分钟前";
                }
            }
        }
        return datetime.ToString("yyyy年MM月dd日 HH:mm");
    }

    public static int ObjectToInt(object expression)
    {
        return ObjectToInt(expression, 0);
    }

    /// <summary>
    /// 将对象转换为Int32类型
    /// </summary>
    /// <param name="expression"></param>
    /// <param name="defValue"></param>
    /// <returns></returns>
    public static int ObjectToInt(object expression, int defValue)
    {
        if (expression != null)
            return StrToInt(expression.ToString(), defValue);

        return defValue;
    }

    /// <summary>
    /// 唯一數字 19位
    /// </summary>
    /// <returns></returns>
    public static long GenerateIntID()
    {
        byte[] buffer = Guid.NewGuid().ToByteArray();
        return BitConverter.ToInt64(buffer, 0);
    }

    /// <summary>
    /// 唯一字符串 16位
    /// </summary>
    /// <returns></returns>
    public static string GenerateStringID()
    {
        long i = 1;
        foreach (byte b in Guid.NewGuid().ToByteArray())
        {
            i *= ((int)b + 1);
        }
        return string.Format("{0:x}", i - DateTime.Now.Ticks);
    }

    /// <summary>
    /// 獲取英文月份
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string GetMonth(string str)
    {
        string rq = "";
        switch (str)
        {
            case "1": rq = " Jan"; break;
            case "2": rq = " Feb"; break;
            case "3": rq = " Mar"; break;
            case "4": rq = " Apr"; break;
            case "5": rq = " May"; break;
            case "6": rq = " Jun"; break;
            case "7": rq = " Jul"; break;
            case "8": rq = " Aug"; break;
            case "9": rq = " Sep"; break;
            case "10": rq = " Oct"; break;
            case "11": rq = " Nov"; break;
            case "12": rq = " Dec"; break;
        }
        return rq;
    }
    public static List<string> GetStrArray(string str, char speater, bool toLower)
    {
        List<string> list = new List<string>();
        string[] ss = str.Split(speater);
        foreach (string s in ss)
        {
            if (!string.IsNullOrEmpty(s) && s != speater.ToString())
            {
                string strVal = s;
                if (toLower)
                {
                    strVal = s.ToLower();
                }
                list.Add(strVal);
            }
        }
        return list;
    }
    public static string[] GetStrArray(string str)
    {
        return str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
    }
    public static string[] GetStringArray(string str)
    {
        if (str == null)
        {
            return null;
        }
        if (str.Split('|').Length > 0)
        {
            return str.Split('|');
        }
        return null;
    }
    public static string GetArrayString(string str, int index)
    {
        string strTemp = string.Empty;
        if (str == null)
        {
            return strTemp;
        }
        try
        {
            if (str.Split('|').Length > 0)
            {
                strTemp = str.Split('|')[index];
            }
        }
        catch (Exception)
        {
        }
        return strTemp;
    }
    public static string GetArrayStr(List<string> list, string speater)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < list.Count; i++)
        {
            if (i == list.Count - 1)
            {
                sb.Append(list[i]);
            }
            else
            {
                sb.Append(list[i]);
                sb.Append(speater);
            }
        }
        return sb.ToString();
    }

    #region 删除最后一个字符之后的字符
    /// <summary>
    /// 删除最后结尾的一个逗号
    /// </summary>
    public static string DelLastComma(string str)
    {
        return str.Substring(0, str.LastIndexOf(","));
    }

    /// <summary>
    /// 删除最后结尾的指定字符后的字符
    /// </summary>
    public static string DelLastChar(string str, string strchar)
    {
        return str.Substring(0, str.LastIndexOf(strchar));
    }
    #endregion

    /// <summary>
    /// 转全角的函数(SBC case)
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string ToSBC(string input)
    {
        //半角转全角：
        char[] c = input.ToCharArray();
        for (int i = 0; i < c.Length; i++)
        {
            if (c[i] == 32)
            {
                c[i] = (char)12288;
                continue;
            }
            if (c[i] < 127)
                c[i] = (char)(c[i] + 65248);
        }
        return new string(c);
    }

    /// <summary>
    ///  转半角的函数(SBC case)
    /// </summary>
    /// <param name="input">输入</param>
    /// <returns></returns>
    public static string ToDBC(string input)
    {
        char[] c = input.ToCharArray();
        for (int i = 0; i < c.Length; i++)
        {
            if (c[i] == 12288)
            {
                c[i] = (char)32;
                continue;
            }
            if (c[i] > 65280 && c[i] < 65375)
                c[i] = (char)(c[i] - 65248);
        }
        return new string(c);
    }

    public static List<string> GetSubStringList(string o_str, char sepeater)
    {
        List<string> list = new List<string>();
        string[] ss = o_str.Split(sepeater);
        foreach (string s in ss)
        {
            if (!string.IsNullOrEmpty(s) && s != sepeater.ToString())
            {
                list.Add(s);
            }
        }
        return list;
    }


    #region 将字符串样式转换为纯字符串
    public static string GetCleanStyle(string StrList, string SplitString)
    {
        string RetrunValue = "";
        //如果为空，返回空值
        if (StrList == null)
        {
            RetrunValue = "";
        }
        else
        {
            //返回去掉分隔符
            string NewString = "";
            NewString = StrList.Replace(SplitString, "");
            RetrunValue = NewString;
        }
        return RetrunValue;
    }
    #endregion

    #region 将字符串转换为新样式
    public static string GetNewStyle(string StrList, string NewStyle, string SplitString, out string Error)
    {
        string ReturnValue = "";
        //如果输入空值，返回空，并给出错误提示
        if (StrList == null)
        {
            ReturnValue = "";
            Error = "请输入需要划分格式的字符串";
        }
        else
        {
            //检查传入的字符串长度和样式是否匹配,如果不匹配，则说明使用错误。给出错误信息并返回空值
            int strListLength = StrList.Length;
            int NewStyleLength = GetCleanStyle(NewStyle, SplitString).Length;
            if (strListLength != NewStyleLength)
            {
                ReturnValue = "";
                Error = "样式格式的长度与输入的字符长度不符，请重新输入";
            }
            else
            {
                //检查新样式中分隔符的位置
                string Lengstr = "";
                for (int i = 0; i < NewStyle.Length; i++)
                {
                    if (NewStyle.Substring(i, 1) == SplitString)
                    {
                        Lengstr = Lengstr + "," + i;
                    }
                }
                if (Lengstr != "")
                {
                    Lengstr = Lengstr.Substring(1);
                }
                //将分隔符放在新样式中的位置
                string[] str = Lengstr.Split(',');
                foreach (string bb in str)
                {
                    StrList = StrList.Insert(int.Parse(bb), SplitString);
                }
                //给出最后的结果
                ReturnValue = StrList;
                //因为是正常的输出，没有错误
                Error = "";
            }
        }
        return ReturnValue;
    }
    #endregion

    #region 截取字符串
    /// <summary>
    /// 截取字符串
    /// </summary>
    /// <param name="strContent">字符串内容</param>
    /// <param name="length">长度</param>
    /// <returns></returns>
    public static string SubString(string strContent, int length)
    {
        if (string.IsNullOrEmpty(strContent))
        {
            return "";
        }
        return SubString(strContent, length, "");
    }
    #endregion

    #region 截取字符串
    /// <summary>
    /// 截取字符串
    /// </summary>
    /// <param name="strContent">字符串内容</param>
    /// <param name="length">长度</param>
    /// <param name="strInstead">超长代替字符串</param>
    /// <returns></returns>
    public static string SubString(string strContent, int length, string strInstead)
    {
        if (string.IsNullOrEmpty(strContent))
        {
            return "";
        }
        if (Encoding.Default.GetByteCount(strContent) <= length)
        {
            return strContent;
        }
        string strChar = string.Empty;
        int charCount = 0;
        StringBuilder sbContent = new StringBuilder(length);
        for (int i = 0; i < strContent.Length; i++)
        {
            strChar = strContent.Substring(i, 1);
            if (Encoding.Default.GetByteCount(strChar) == 1)
            {
                charCount += 1;
            }
            else
            {
                charCount += 2;
            }
            if (charCount > length)
            {
                break;
            }
            else
            {
                sbContent.Append(strChar);
            }
        }
        return sbContent.ToString() + strInstead;
    }
    #endregion

    #region 截取指定长度的数据
    /// <summary>
    /// 截取指定长度的数据
    /// </summary>
    /// <param name="DataValue">待截取的数据</param>
    /// <param name="SubStringLength">截取的长度</param>
    /// <param name="InsteadStr">超過長度替換字符串</param>
    /// <returns>返回 截取後的字符串</returns>
    public static string SubString(object DataValue, int Length, string InsteadStr)
    {
        if (DataValue == null || DataValue == DBNull.Value)
        {
            return string.Empty;
        }
        string Str = DataValue.ToString();
        StringBuilder SubStr = new StringBuilder();
        string StrChar = string.Empty;
        int CharCount = 0;
        if (Encoding.Default.GetByteCount(Str) <= Length)
        {
            return Str;
        }
        for (int i = 0; i < Str.Length; i++)
        {
            StrChar = Str.Substring(i, 1);
            if (Encoding.Default.GetByteCount(StrChar) == 1)
            {
                CharCount += 1;
            }
            else
            {
                CharCount += 2;
            }
            if (CharCount > Length)
            {
                break;
            }
            else
            {
                SubStr.Append(StrChar);
            }
        }
        return SubStr.ToString() + InsteadStr;
    }
    #endregion


    public static string ListToJson(object objList)
    {
        //IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
        //timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
        return JsonConvert.SerializeObject(objList);
    }

    #region ========加密========

    /// <summary>
    /// 加密 密匙為 DESEncrypt
    /// </summary>
    /// <param name="Text"></param>
    /// <returns></returns>
    public static string Encrypt(string Text)
    {
        return string.IsNullOrEmpty(Text) ? "" : Encrypt(Text, "OJSbigline");
    }

    /// <summary>
    /// 加密 密匙為 DESEncrypt
    /// </summary>
    /// <param name="Text"></param>
    /// <returns></returns>
    public static string Encrypt2(string Text)
    {
        return string.IsNullOrEmpty(Text) ? "" : Encrypt(Text, "OJSbigline");
    }
    /// <summary>
    /// 加密数据
    /// </summary>
    /// <param name="Text"></param>
    /// <param name="sKey"></param>
    /// <returns></returns>
    public static string Encrypt(string Text, string sKey)
    {
        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        byte[] inputByteArray;
        inputByteArray = Encoding.Default.GetBytes(Text);
#pragma warning disable CS0618 // Type or member is obsolete
        des.Key = ASCIIEncoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
#pragma warning restore CS0618 // Type or member is obsolete
#pragma warning disable CS0618 // Type or member is obsolete
        des.IV = ASCIIEncoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
#pragma warning restore CS0618 // Type or member is obsolete
        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
        cs.Write(inputByteArray, 0, inputByteArray.Length);
        cs.FlushFinalBlock();
        StringBuilder ret = new StringBuilder();
        foreach (byte b in ms.ToArray())
        {
            ret.AppendFormat("{0:X2}", b);
        }
        return ret.ToString();
    }

    #endregion

    #region ========解密========


    /// <summary>
    /// 解密
    /// </summary>
    /// <param name="Text"></param>
    /// <returns></returns>
    public static string Decrypt(string Text)
    {
        return string.IsNullOrEmpty(Text) ? "" : Decrypt(Text, "OJSbigline");
    }

    /// <summary>
    /// 解密数据
    /// </summary>
    /// <param name="Text"></param>
    /// <param name="sKey"></param>
    /// <returns></returns>
    public static string Decrypt(string Text, string sKey)
    {
        try
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            int len;
            len = Text.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(Text.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
#pragma warning disable CS0618 // Type or member is obsolete
            des.Key = ASCIIEncoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
#pragma warning restore CS0618 // Type or member is obsolete
#pragma warning disable CS0618 // Type or member is obsolete
            des.IV = ASCIIEncoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
#pragma warning restore CS0618 // Type or member is obsolete
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }
        catch (Exception)
        {
            //throw new MsgEx("無法解密該數據");
            return "1";
        }
    }

    #endregion
}
