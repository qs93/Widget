<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        随机生成密钥：<asp:Button ID="btncreateMY" runat="server" Text="随机生成密钥" OnClick="btncreateMY_Click" /><br />
        公钥：<asp:TextBox ID="tbxcreateMY_publicKey" runat="server" TextMode="MultiLine" Height="59px" ReadOnly="True" Width="711px"></asp:TextBox><br />
        私钥：<asp:TextBox ID="tbxcreateMY_key" runat="server" TextMode="MultiLine" Height="59px" ReadOnly="True" Width="710px"></asp:TextBox><br />
        <hr />
        <br />
        生成签名：<br />
        原文：  
        <asp:TextBox ID="tbxContent" runat="server" TextMode="MultiLine" Height="59px" Width="711px"></asp:TextBox>
        <br />
        私钥： 
        <asp:TextBox ID="tbxKey" runat="server" TextMode="MultiLine" Height="59px" Width="711px"></asp:TextBox><br />
        签名： 
        <asp:TextBox ID="tbxSign" runat="server" TextMode="MultiLine" Height="59px" ReadOnly="True" Width="711px"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="生成签名" />
        <br />
        <br />
        <hr />
        <br />
        验证签名：<br />
        原文：<asp:TextBox ID="tbxContentYZ" runat="server" TextMode="MultiLine" Height="59px" Width="711px"></asp:TextBox><br />
        公钥：<asp:TextBox ID="tbxPublickeyYZ" runat="server" TextMode="MultiLine" Height="59px" Width="711px"></asp:TextBox><br />
        签名：<asp:TextBox ID="tbxSignYZ" runat="server" TextMode="MultiLine" Height="59px" Width="711px"></asp:TextBox>
        <br />
        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="验证签名" />
        </form>
</body>
</html>
