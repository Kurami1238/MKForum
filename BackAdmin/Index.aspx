<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="MKForum.BackAdmin.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            目前登入者:<asp:Literal ID="ltlAccount" runat="server"></asp:Literal><br />
            <asp:Button ID="btnLogout" runat="server" Text="登出" OnClick="btnLogout_Click" />
        </div>
    </form>
</body>
</html>
