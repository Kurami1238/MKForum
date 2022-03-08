<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="MKForum.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:PlaceHolder ID="plcReg" runat="server">
            帳號:<asp:TextBox ID="txtAcc" runat="server"></asp:TextBox>
            <asp:Button ID="btnAcc" runat="server" Text="帳號驗證" OnClick="btnAcc_Click" />
            <asp:Literal ID="ltlAcc" runat="server" Text="-"></asp:Literal>
            <br />
            密碼:<asp:TextBox ID="txtPwd" runat="server" TextMode="Password"></asp:TextBox>
            <br />
            E-mail:<asp:TextBox ID="txtMail" runat="server"></asp:TextBox><br />
            驗證碼:<asp:TextBox ID="txtCapt" runat="server"></asp:TextBox><br />

            <asp:Button ID="btnSubmit" runat="server" Text="Button" OnClick="btnSubmit_Click" /><br />
            <asp:Literal ID="ltlReg" runat="server" Text="-"></asp:Literal>
        </asp:PlaceHolder>

        <asp:PlaceHolder ID="plcInfo" runat="server">
            <asp:Literal ID="ltlAccount" runat="server" Text="成功Reg"></asp:Literal><br />
            請前往:<a href="\BackAdmin\Registersuccess.aspx">後台</a>

        </asp:PlaceHolder>
    </form>
</body>
</html>
