<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebLoadTest.aspx.cs" Inherits="WebApplication1.WebLoadTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="Scripts/jquery-1.7.1.js"></script>
    <title></title>
    <script>
        var cnt = "xxoo";
        $(function () {
            $("#<%=bt_test.ClientID%>").click(function () {
                alert(cnt);
                $("#<%=test_value.ClientID%>").val(cnt);
                $("#<%=div_test.ClientID%>").append("这是前台页面的按钮事件！！");
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="up_p1">
            <ContentTemplate>
                <div>
                    <asp:Button ID="bt_test" runat="server" Text="测试顺序" OnClick="bt_test_Click" />
                </div>
                <div runat="server" id="div_test"></div>
                <input type="hidden" runat="server" id="test_value" />
            </ContentTemplate>
        </asp:UpdatePanel>

    </form>
</body>
</html>
