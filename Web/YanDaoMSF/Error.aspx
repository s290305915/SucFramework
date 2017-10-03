<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="YanDaoMSF.Error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <p style="color: red; font-size: 20px; text-align: center; font-family: 微软雅黑">似乎遇到了点问题，请联系管理员。</p>
            <p>错误信息：</p>
            <br />
            <p>
                <div runat="server" id="er_msg">
                </div>
            </p>
            <p>
                <asp:LinkButton runat="server" ID="btn_export" OnClick="btn_export_Click" Style="text-decoration: none"></asp:LinkButton>
            </p>
        </div>
    </form>
</body>
</html>
