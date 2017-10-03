<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Master/HeadPage.Master" AutoEventWireup="true" CodeBehind="ChcekFileList.aspx.cs" Inherits="YanDaoMSF.Admin.CheckFileList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../js/Jquery-easyui-1.4/themes/gray/easyui.css" rel="stylesheet" />
    <link href="../js/Jquery-easyui-1.4/themes/icon.css" rel="stylesheet" />
    <script src="../js/Jquery-easyui-1.4/jquery.easyui.min.js"></script>
    <script>
        $(function () {
            $('#divList').datagrid({
                url: 'Handler/BFileHandler.ashx?opt=backchecklist',
                toolbar: "#tb",
                idField: "ID",
                rownumbers: true,
                pagination: true,
                //striped: true,
                singleSelect: true,
                pageSize: 50,
                pagination: true,
                queryParams: { s_name: $("#s_name").val() },
                frozenColumns: [[{
                    title: '文件名', field: 'NAME', width: 90, formatter: function (value, data, index) {
                        return "<a target='_blank' style='text-decoration:none' href='../FP/FileContent.aspx?id=" + data.ID + "'>" + value + "</a>";
                    }
                }]],
                columns: [[
                    { title: '审核状态', field: 'CHECKSTATE', width: 90 },
                    { title: '上传人', field: 'USERNAME', width: 90 },
                    { title: '文件类型', field: 'TYPE', width: 90 },
                    { title: '版本', field: 'GRADE_NAME', width: 90 },
                    { title: '单位', field: 'UNIT', width: 90 },
                    {
                        title: '发布时间', field: 'PUBLISH_DATE', width: 110, align: 'center', formatter: function (value, data, index) {
                            return jsonDateFormat(value, "year");//, "year" 
                        }
                    }
                ]]
            });

            $("#btnAcc").click(function () {
                debugger;
                var row = $("#divList").datagrid("getSelected");
                if (row != null) {
                    $("#hdId").val(row.ID);
                    $.post("Handler/BFileHandler.ashx?opt=chkfile",
                               { id: row.ID, isacc: 1 },
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
                else {
                    $.messager.alert("提示", "没有选中任何数据", "error");
                }
            });

            $("#btnDeny").click(function () {
                debugger;
                var row = $("#divList").datagrid("getSelected");
                if (row != null) {
                    $("#hdId").val(row.ID);
                    $("#hdId").val(row.ID);
                    $.post("Handler/BFileHandler.ashx?opt=chkfile",
                               { id: row.ID, isacc: 2 },
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
                else {
                    $.messager.alert("提示", "没有选中任何数据", "error");
                }
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
        <input id="TypeId" type="hidden" />
        <input id="hdId" type="hidden" />
        <div id="divList"></div>
        <div id="tb">
            <div>
                <% if (true)//funcs.Contains("Edit"))
                   { %>
                <a id="btnAcc" href="javascript://" class="easyui-linkbutton" plain="true" iconcls="icon-ok">通过</a>
                <a id="btnDeny" href="javascript://" class="easyui-linkbutton" plain="true" iconcls="icon-no">不通过</a>
                <%--<a id="btnDelete" href="javascript://" class="easyui-linkbutton" plain="true" iconcls="icon-remove">删除文件</a>--%>
                <%}%>
            </div>
            <div>
                文件名：
            <input id="s_name" type="text" />
                <a id="btnSearch" href="javascript://" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-search'">查询</a>

            </div>
        </div>
    </form>
</asp:Content>
