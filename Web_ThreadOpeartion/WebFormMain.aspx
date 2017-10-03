<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebFormMain.aspx.cs" Inherits="Web_ThreadOpeartion.WebFormMain" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table align="center" bgcolor="#CDF2FC">
            <tr>
                <td class="style2" style="text-align: center">使用ASP.NET结束进程</td>
            </tr>
            <tr>
                <td class="style4">
                    <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="刷新进程" />
                    <%--<asp:Button ID="btnKill" runat="server" OnClick="btnKill_Click" Text="结束进程" />--%>
                </td>
            </tr>
            <tr>
                <td align="left" class="style4">选择进程：<br />
                    <asp:DropDownList ID="procname" runat="server" AutoPostBack="true" Height="19px" Width="160px" OnSelectedIndexChanged="procname_SelectedIndexChanged">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td class="style3">线程列表：<br />
                    <asp:ListBox runat="server" ID="listthread" Height="200px" Width="160px"></asp:ListBox>
                </td>
            </tr>

        </table>
    </form>

</body>
</html>
