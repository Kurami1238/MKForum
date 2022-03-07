<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registersuccess.aspx.cs" Inherits="MKForum.BackAdmin.Registersuccess" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="成功註冊 已完成SQL"></asp:Label>
            <asp:Button ID="btnLogout" runat="server" Text="回到登入畫面" OnClick="btnLogout_Click" />
        </div>
    </form>
</body>
</html>
