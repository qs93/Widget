<%@ WebHandler Language="C#" Class="AjaxHelper" %>

using System;
using System.Web;
using System.Linq;

public class AjaxHelper : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        string option = StringHelper.GetInStr("option");
        switch (option)
        {
            case "angular_sub_ajax":
                string name = StringHelper.PostInStr("UserName");
                JsonHelper.WriteSuccess(name);
                break;
        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}