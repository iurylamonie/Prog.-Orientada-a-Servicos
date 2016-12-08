<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendToast.aspx.cs" Inherits="SendToast.SendToast" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:TextBox ID="TextBoxUri" runat="server"></asp:TextBox><br />
        <asp:TextBox ID="TextBoxTitle" runat="server"></asp:TextBox><br />
        <asp:TextBox ID="TextBoxSubTitle" runat="server"></asp:TextBox><br />
        <asp:Button ID="ButtonSendToast" runat="server" Text="Enviar Notificação" OnClick="ButtonSendToast_Click" /><br />
        <asp:TextBox ID="TextBoxResponse" runat="server"></asp:TextBox><br />
    
    </div>
    </form>
</body>
</html>
