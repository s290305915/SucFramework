<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="my97datapicker.aspx.cs" Inherits="WebApplication1.my97datapicker" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="Scripts/My97DatePicker/WdatePicker.js"></script>
    <link href="Scripts/My97DatePicker/skin/WdatePicker.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input id="d11" type="text" onClick="WdatePicker()"/>
        </div>
    </form>
</body>
</html>
