using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// JsonHelper 的摘要说明
/// </summary>
public class JsonHelper
{
    public JsonHelper()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    #region 輸出Json結果-string        
    /// <summary>
    /// 返回Json結果
    /// </summary>
    /// <param name="resultcode">結果代碼</param>
    /// <param name="reason">提示信息null时返回""</param>
    /// <param name="data">返回內容</param>
    public static string Success(object data = null, int resultcode = 100, string reason = "success")
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("{");
        sb.AppendFormat("\"resultcode\":{0},", resultcode);
        sb.AppendFormat("\"reason\":\"{0}\",", reason);
        sb.AppendFormat("\"data\":[{0}]", data == null ? "" : JsonConvert.SerializeObject(data).TrimStart('[').TrimEnd(']'));
        sb.Append("}");
        return sb.ToString();
    }
    public static string Fail(int resultcode = 500, string reason = "fail")
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("{");
        sb.AppendFormat("\"resultcode\":{0},", resultcode);
        sb.AppendFormat("\"reason\":\"{0}\",", reason);
        sb.AppendFormat("\"data\":[]");
        sb.Append("}");
        return sb.ToString();
    }

    public static void WriteSuccess(object data = null, int resultcode = 100, string reason = "success")
    {
        HttpContext.Current.Response.Write(Success(data, resultcode, reason));
    }
    public static void WriteFail(int resultcode = 500, string reason = "fail")
    {
        HttpContext.Current.Response.Write(Fail(resultcode, reason));
    }
    #endregion
}