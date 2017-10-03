<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Master/HeadPage.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="YanDaoMSF.Admin.UserList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../js/Jquery-easyui-1.4/themes/gray/easyui.css" rel="stylesheet" />
    <link href="../js/Jquery-easyui-1.4/themes/icon.css" rel="stylesheet" />
    <script src="../js/Jquery-easyui-1.4/jquery.easyui.min.js"></script>
    <script type="text/javascript">
        $(function () {
            var Clear = function () {
                $("#txtName,#txtLogName,#txtUnit,#txtPhone,#hdId,#roleId").val("");
                $("#cmbRole").combobox('setValue', "");
            };
            var Vali = function () {
                return $("#txtName").validatebox("isValid") * $("#cmbRole").combobox("isValid") * $("#txtLogName").validatebox("isValid");
            }
            $('#cmbRole').combobox({
                onSelect: function (index) {
                    $("#roleId").val(index.ID);
                }
            });
            $('#cmbRole').combobox({
                url: 'Handler/UserHandler.ashx?opt=GetRole',
                valueField: 'ID',
                textField: 'NAME',
                onSelect: function (index) {
                    $("#roleId").val(index.ID);
                }
            });
            $('#divList').datagrid({
                url: 'Handler/UserHandler.ashx?opt=GetUserList',
                toolbar: "#tb",
                idField: "ID",
                rownumbers: true,
                pagination: true,
                //striped: true,
                singleSelect: true,
                queryParams: { s_name: $("#s_name").val() },
                frozenColumns: [[{ title: '姓名', field: 'NAME', width: 90 }]],
                columns: [[
                    { title: '登录名', field: 'LOGIN_NAME', width: 90 },
                    { title: '单位', field: 'UNIT', width: 90 },
                    {
                        title: '最后登陆时间', field: 'LAST_LOGIN_TIME', width: 110, align: 'center', formatter: function (value, data, index) {
                            return  jsonDateFormat(value, "year");//, "year"
                        }
                    }
                ]]
            });

            $("#btnSubmit").click(function () {
                if (Vali) {
                    $.post("Handler/UserHandler.ashx?opt=check", {
                        id: $("#hdId").val(),
                        name: $("#txtName").val()
                    }, function (data) {
                        if (data == "success") {
                            $.post("Handler/UserHandler.ashx?opt=save", {
                                id: $("#hdId").val(),
                                name: $("#txtName").val(),
                                login_name: $("#txtLogName").val(),
                                unit: $("#txtUnit").val(),
                                phoneno: $("#txtPhone").val(),
                                role_id: $("#roleId").val(),
                                is_password: $("input[name='chkpassword']:checked").map(function () {
                                    return $(this).val();
                                }).get().join('#')
                            }, function (data) {
                                if (data == "success") {
                                    $.messager.alert("提示", "操作成功", "info", function () {
                                        $('#divUser').window('close');
                                        $("#divList").datagrid("reload");
                                    });
                                }
                                else {

                                }
                            });
                        }
                        else {
                            $.messager.alert("错误", "登录名已存在，请重新添加", "error");
                        }
                    });
                }
            });
            $("#btnSearch").click(function () {
                $("#divList").datagrid('reload',
               {
                   s_name: $("#s_name").val()
               }
           );
            });
            $("#btnAdd").click(function () {
                Clear();
                $("#divUser").dialog({ title: "添加用户" }).dialog("open");
            });
            $("#btnEdit").click(function () {
                var row = $("#divList").datagrid("getSelected");
                if (row != null) {
                    debugger;
                    $("#hdId").val(row.ID);
                    $("#txtLogName").val(row.LOGIN_NAME);
                    $("#txtName").val(row.NAME);
                    $("#cmbRole").combobox("setValue", row.ROLE_ID);
                    $("#roleId").val(row.ROLE_ID);
                    $("#txtPhone").val(row.PHONENO);
                    $("#txtUnit").val(row.UNIT);
                    $("#divUser").dialog({ title: "编辑用户" }).dialog("open");
                }
                else {
                    $.messager.alert("提示", "没有选中任何用户", "error");
                }
            });

            $("#btnDelete").click(function () {
                var row = $("#divList").datagrid("getSelected");
                if (row != null) {
                    $.messager.confirm("提示", "此操作会删除该用户，确定要删除吗？", function (r) {
                        if (r) {
                            $.post("Handler/UserHandler.ashx?opt=delete",
                                { id: row.ID },
                                function (data) {
                                    if (data == "success") {
                                        $.messager.alert("提示", "操作成功", "info", function () {
                                            $("#divList").datagrid("reload");
                                        });
                                    }
                                    else
                                        $.messager.alert("提示", data, "info");
                                });
                        }
                    });
                }
                else { $.messager.alert("提示", "此操作需要选中一条记录", "info"); }
            });

            $("#btnSearch").click(function () {
                $("#divList").datagrid('reload',
               {
                   s_name: $("#s_name").val()
               })
            });
        });

        function jsonDateFormat(jsonDate, year) {//json日期格式转换为正常格式
            try {//出自http://www.cnblogs.com/ahjesus 尊重作者辛苦劳动成果,转载请注明出处,谢谢!
                var date = new Date(parseInt(jsonDate.replace("/Date(", "").replace(")/", ""), 10));
                var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
                var day = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
                var hours = date.getHours();
                var minutes = date.getMinutes();
                var seconds = date.getSeconds();
                var milliseconds = date.getMilliseconds();
                if (year == 'year')
                    return date.getFullYear() + "-" + month + "-" + day;
                return date.getFullYear() + "-" + month + "-" + day + " " + hours + ":" + minutes + ":" + seconds + "." + milliseconds;
            } catch (ex) {//出自http://www.cnblogs.com/ahjesus 尊重作者辛苦劳动成果,转载请注明出处,谢谢!
                return "";
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form" runat="server">
        <input id="orgId" type="hidden" />
        <input id="roleId" type="hidden" />
        <input id="hdId" type="hidden" />
        <div id="divList"></div>
        <div id="tb">
            <div>
                <%if (funcs.Contains("Add"))
                  { %>
                <a id="btnAdd" href="javascript://" class="easyui-linkbutton" plain="true" iconcls="icon-add">添加用户</a>
                <%} if (funcs.Contains("Edit"))
                  { %>
                <a id="btnEdit" href="javascript://" class="easyui-linkbutton" plain="true" iconcls="icon-edit">编辑用户</a>
                <%} if (funcs.Contains("Delete"))
                  { %>
                <a id="btnDelete" href="javascript://" class="easyui-linkbutton" plain="true" iconcls="icon-remove">删除用户</a>
                <%}%>
            </div>
            <div>
                姓名：
            <input id="s_name" type="text" />
                <a id="btnSearch" href="javascript://" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-search'">查询</a>

            </div>
        </div>

        <div id="divUser" title="添加用户" style="width: 500px; height: 250px; padding: 10px" class="easyui-dialog" data-options="buttons:'#buttons',closed:true,modal:true">
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
                    <td style="width: 90px; height: 30px; text-align: right">角色：</td>
                    <td>
                        <select class="easyui-combobox" panelheight="auto" style="width: 150px" id="cmbRole">
                        </select>
                    </td>
                </tr>
                <tr>
                    <td style="width: 90px; height: 30px; text-align: right">电话：</td>
                    <td>
                        <input id="txtPhone" class="easyui-validatebox txt" type="text" /></td>
                </tr>
                <tr>
                    <td style="width: 90px; height: 30px; text-align: right">单位：</td>
                    <td colspan="3">
                        <input id="txtUnit" class="easyui-validatebox txt" type="text" style="width: 382px;" /></td>
                </tr>
                <tr>
                    <td style="width: 90px; height: 30px; text-align: right">重置密码：</td>
                    <td>
                        <input type="checkbox" name="chkpassword" id="chkpassword" value="1">密码为1</input>
                    </td>
                </tr>
            </table>
            <div id="buttons">
                <a id="btnSubmit" href="javascript://" class="easyui-linkbutton" plain="true" iconcls="icon-save">确定</a>
            </div>
        </div>
    </form>
</asp:Content>
