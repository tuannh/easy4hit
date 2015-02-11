<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="hits4u.aspx.cs" Inherits="JSONApp.hits4u" %>

<div id="divContainer" runat="server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
    </head>
    <body>
        <form id="form1" runat="server">
        <div>
            Source data folder:
            <asp:TextBox ID="txtUrl" Width="500px" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="btnSlitter" runat="server" Text="Slitter" OnClick="btnSlitter_Click" />
            <br />
            <br />
            <br />
            <asp:Button ID="btnProcess" runat="server" Text="Process" OnClick="btnProcess_Click" />
        </div>
        </form>
    </body>
    </html>
</div>
