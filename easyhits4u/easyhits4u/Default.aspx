<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="easyhits4u.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    ID1: <asp:TextBox ID="txtID1" runat="server"></asp:TextBox><br />
    ID1: <asp:TextBox ID="txtID2" runat="server"></asp:TextBox><br />
    <br />

    <asp:Button ID="btnCompare" runat="server" Text="Compare" 
            onclick="btnCompare_Click" />
    
    </div>
    </form>
</body>
</html>
