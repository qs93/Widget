using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary> 
    /// 生成签名 
    /// </summary> 
    /// <param name="sender"></param> 
    /// <param name="e"></param> 
    protected void Button1_Click(object sender, EventArgs e)
    {
        DSACryptoServiceProvider objdsa = new DSACryptoServiceProvider();
        objdsa.FromXmlString(tbxKey.Text);
        byte[] source = System.Text.UTF8Encoding.UTF8.GetBytes(tbxContent.Text);
        //数字签名 
        tbxSign.Text = BitConverter.ToString(objdsa.SignData(source));
    }
    /// <summary> 
    /// 随机生成密钥 
    /// </summary> 
    /// <param name="sender"></param> 
    /// <param name="e"></param> 
    protected void btncreateMY_Click(object sender, EventArgs e)
    {
        DSACryptoServiceProvider objdsa = new DSACryptoServiceProvider();
        tbxcreateMY_publicKey.Text = objdsa.ToXmlString(false);
        tbxcreateMY_key.Text = objdsa.ToXmlString(true);
    }
    /// <summary> 
    /// 验证签名 
    /// </summary> 
    /// <param name="sender"></param> 
    /// <param name="e"></param> 
    protected void Button3_Click(object sender, EventArgs e)
    {
        DSACryptoServiceProvider objdsa = new DSACryptoServiceProvider();
        byte[] fileHashValue = new SHA1CryptoServiceProvider().ComputeHash(System.Text.UTF8Encoding.UTF8.GetBytes(tbxContentYZ.Text));
        string[] strSplit = tbxSignYZ.Text.Split('-');
        byte[] SignedHash = new byte[strSplit.Length];
        for (int i = 0; i < strSplit.Length; i++)
            SignedHash[i] = byte.Parse(strSplit[i], System.Globalization.NumberStyles.AllowHexSpecifier);
        objdsa.FromXmlString(tbxPublickeyYZ.Text);
        bool ret = objdsa.VerifySignature(fileHashValue, SignedHash);
        Response.Write(ret.ToString());
        // Qcd.Core.Web.Messages.ShowDialog(ret.ToString()); 
    }
}