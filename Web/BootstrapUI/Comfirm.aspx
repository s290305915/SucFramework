<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Comfirm.aspx.cs" Inherits="BootstrapUI.Comfirm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <script src="Scripts/jquery-easyui-1.5.2/jquery.min.js"></script>
    <script src="Scripts/jquery-easyui-1.5.2/jquery.easyui.min.js"></script>
    <link href="Scripts/jquery-easyui-1.5.2/themes/icon.css" rel="stylesheet" />
    <%--<link href="Scripts/jquery-easyui-1.5.2/themes/metro/easyui.css" rel="stylesheet" />--%>
    <link href="Scripts/jquery-easyui-1.5.2/themes/material/easyui.css" rel="stylesheet" />
    <%--<link href="Scripts/jquery-easyui-1.5.2/themes/gray/easyui.css" rel="stylesheet" />--%>
    <%--<link href="Scripts/jquery-easyui-1.5.2/themes/default/easyui.css" rel="stylesheet" />--%>
    <%--<link href="Scripts/jquery-easyui-1.5.2/themes/bootstrap/easyui.css" rel="stylesheet" />--%>
    <%--<link href="Scripts/jquery-easyui-1.5.2/themes/black/easyui.css" rel="stylesheet" />--%>
    <title></title>
    <script>
        $(function () {
            $("#btn").click(function () {
                $.messager.confirm("提示", "是否继续操作？", function (r) {
                    if (r)
                        $.messager.alert("成功", "你点了确定");
                    else
                        $.messager.alert("错误", "你点了取消");
                })
            });
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <input type="button" id="btn" value="弹窗" />
    </div>
    </form>
</body>
</html>
