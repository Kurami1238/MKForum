<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MKForum.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
             <asp:PlaceHolder ID="plcLogin" runat="server">
            Account:<asp:TextBox ID="txtAccount" runat="server"></asp:TextBox><br />
            Password:<asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox><br />
                
            <asp:Button ID="btnlogin" runat="server" Text="登入" OnClick="btnlogin_Click" /><br />
            <asp:Literal ID="ltlMessage" runat="server"></asp:Literal>
                <asp:Button ID="btnReg" runat="server" Text="註冊" OnClick="btnReg_Click"/>


            </asp:PlaceHolder>

            <asp:PlaceHolder ID="plcInfo" runat="server">
                <asp:Literal ID="ltlAccount" runat="server"></asp:Literal><br />
                請前往:<a href="\BackAdmin\Index.aspx">後台</a>

            </asp:PlaceHolder>
        </div>
    </form>
</body>
</html>
