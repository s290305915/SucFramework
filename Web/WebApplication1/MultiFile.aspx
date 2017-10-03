<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MultiFile.aspx.cs" Inherits="WebApplication1.MultiFile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="Scripts/Jquery-easyui-1.4/jquery.min.js"></script>
    <script src="Scripts/Jquery-easyui-1.4/jquery.easyui.min.js"></script>
    <link href="Scripts/Jquery-easyui-1.4/themes/color.css" rel="stylesheet" />
    <link href="Scripts/Jquery-easyui-1.4/themes/icon.css" rel="stylesheet" />
    <link href="Scripts/Jquery-easyui-1.4/themes/metro/easyui.css" rel="stylesheet" />
    <title></title>
    <script type="text/javascript">
        $(function () {
            $('#divList').datagrid({
                url: 'Handler/EntityHandler.ashx?opt=list',
                toolbar: "#tb",
                idField: "id",
                rownumbers: true,
                pagination: true,
                striped: true,
                singleSelect: true,
                //queryParams: {    //查询条件
                //    s_org: $("#s_org").combotree("getText"),
                //    s_name: $("#s_name").val()
                //},
                frozenColumns: [[{
                    title: '编辑', field: 'id', width: 90, formatter: function (value, data, index) {
                        //return value == null ? "<a herf='javascript://' class='op_win' onclick='OpenWin(" + index + ")'>编辑</a>" : "已上传";
                        return "<a herf='javascript://' class='op_win' onclick='OpenWin(" + index + ")'>编辑</a>";//
                    }
                }]],
                columns: [[
                    { title: '登录名', field: 'login_name', width: 90 },
                    { title: '单位', field: 'unit', width: 90 }
                ]]
            });
        });
        function OpenWin(index) {
            $('#divList').datagrid('selectRow', index); 
            var row = $("#divList").datagrid("getSelected");
            if (row != null) {
                //$("#hdId").val(row.id);
                $("#txtLogName").val(row.login_name);
                $("#txtName").val(row.name);
                $("#txtUnit").val(row.unit);

                $("#divWin").dialog({ title: "编辑用户" }).dialog("open");
            }
            else {
                $.messager.alert("提示", "没有选中任何角色", "error");
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="divList"></div>
        <div id="divWin" title="添加用户" style="width: 470px; height: 230px; padding: 10px" class="easyui-dialog" data-options="buttons:'#buttons',closed:true,modal:true">
            <table>
                <tr>
                    <td style="width: 90px; height: 30px; text-align: right">登录名：</td>
                    <td>
                        <input id="txtLogName" class="easyui-validatebox txt" required="true" type="text" /></td>
                    <td style="width: 90px; height: 30px; text-align: right">姓名：</td>
                    <td>
                        <input id="txtName" class="easyui-validatebox txt" required="true" type="text" /></td>
                </tr>
                <tr>
                    <td style="width: 90px; height: 30px; text-align: right">单位：</td>
                    <td>
                        <input id="txtUnit" class="easyui-validatebox txt" required="true" type="text" /></td>
                </tr>
            </table>
            <div id="buttons">
                <a id="btnSubmit" href="javascript://" class="easyui-linkbutton" plain="true" iconcls="icon-save">确定</a>
            </div>
        </div>
    </form>
</body>
</html>
