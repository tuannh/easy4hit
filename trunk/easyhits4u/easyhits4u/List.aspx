<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="easyhits4u.List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Label ID="lblMsg" runat="server" />
    <div>
        <asp:Repeater ID="rptCategories" runat="server">
            <ItemTemplate>
                <a href="/List.aspx?type=<%# Eval("Id") %>">
                    <%# Eval("Easyhits4uTypeName") %></a>
            </ItemTemplate>
            <SeparatorTemplate>
                &nbsp;|&nbsp;
            </SeparatorTemplate>
        </asp:Repeater>
        <p>
            Search:
            <asp:TextBox ID="txtKeyword" runat="server" Width="200" />
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
        </p>
        <p>
            Total records:
            <asp:Literal ID="litTotal" runat="server" />
            <br />
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
        </p>
        <asp:Repeater ID="rptList" runat="server">
            <HeaderTemplate>
                <table style="width: 100%; border: 1px;">
                    <tr>
                        <td>
                            ID
                        </td>
                        <td>
                            Name
                        </td>
                        <td>
                            Image
                        </td>
                        <td>
                            Length
                        </td>
                        <td>
                            Type
                        </td>
                        <td>
                            Checked
                        </td>
                        <td>
                            Count
                        </td>
                        <td>
                            Note
                        </td>
                        <td>
                            Actions
                        </td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%# Eval("Id") %>
                    </td>
                    <td>
                        <asp:HiddenField ID="hfId" runat="server" Value='<%# Eval("Id") %>' />
                        <%# Eval("Name") %>
                    </td>
                    <td>
                        <asp:Image ID="imgPhoto" runat="server" />
                    </td>
                    <td>
                        <%# Eval("Length")%>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlType" runat="server" />
                    </td>
                    <td>
                        <%#  Convert.ToBoolean( Eval("IsApproved")) ? "Yes" : "No"%>
                    </td>
                    <td>
                        <%# Eval("Count")%>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNote" runat="server" Text='<%# Eval("Note") %>' />
                    </td>
                    <td>
                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="delete" CommandArgument='<%# Eval("Id") %>'
                            Text="Delete" />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    </form>
</body>
</html>
