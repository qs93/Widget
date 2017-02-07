using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Class1 的摘要说明
/// </summary>
public class Class1
{
    public Class1()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    public static string sss()
    {
        string json = "{\"productcode\": \"QP20160110121\",\"productname\": \"小米手機 \"}";
        dynamic infos = JsonConvert.DeserializeObject(json);
        string ssttt = "productcode";

        string test = "aaaa{test1};bbbb{test2}";
        while (test.Contains("{"))
        {
            string testss = test.Substring(test.IndexOf("{") + 1, test.IndexOf("}") - test.IndexOf("{") - 1);
            test = test.Replace("{" + testss + "}", "9999");
        }
        return test;
    }
}