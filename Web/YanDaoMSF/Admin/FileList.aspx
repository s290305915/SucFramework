<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Master/HeadPage.Master" AutoEventWireup="true" CodeBehind="FileList.aspx.cs" Inherits="YanDaoMSF.Admin.FileList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../js/Jquery-easyui-1.4/themes/gray/easyui.css" rel="stylesheet" />
    <link href="../js/Jquery-easyui-1.4/themes/icon.css" rel="stylesheet" />
    <script src="../js/Jquery-easyui-1.4/jquery.easyui.min.js"></script>
    <script>
        $(function () {
            $("#divGrade").window({
                closed: true,
                noheader: true,
                modal: true,
                shadow: true
            });

            var Clear = function () {
                $("#txtFileName,#ul_grade,#txtUnit,#txtPhone,#hdId,#roleId").val("");
                $("#cmbFileType").combobox('setValue', "");
                grad_no = "";
                grade = "";
            };

            $("#btnChooseGrade").click(function () {
                openGrade();
            });

            $('#divList').datagrid({
                url: 'Handler/BFileHandler.ashx?opt=backlist',
                toolbar: "#tb",
                idField: "ID",
                rownumbers: true,
                pagination: true,
                //striped: true,
                singleSelect: true,
                pageSize: 20,
                //pagination: true,
                queryParams: { s_name: $("#s_name").val() },
                frozenColumns: [[
                    //{ title: '文件名', field: 'NAME', width: 90 }
                {
                    title: '文件名', field: 'NAME', width: 90, formatter: function (value, data, index) {
                        return "<a target='_blank' style='text-decoration:none' href='../FP/FileContent.aspx?id=" + data.ID + "'>" + value + "</a>";
                    }
                }
                ]],
                columns: [[
                    { title: '作者', field: 'USER_NAME', width: 90 },
                    { title: '文件类型', field: 'TYPENAME', width: 90 },
                    { title: '版本', field: 'GRADENAME', width: 90 },
                    { title: '单位', field: 'UNIT', width: 90 },
                    {
                        title: '发布时间', field: 'PUBLISH_DATE', width: 110, align: 'center', formatter: function (value, data, index) {
                            return jsonDateFormat(value, "year");//, "year"
                        }
                    }
                ]]
            });

            $('#cmbFileType').combobox({
                url: 'Handler/BFileHandler.ashx?opt=GetFileType',
                valueField: 'ID',
                textField: 'NAME',
                onSelect: function (index) {
                    $("#TypeId").val(index.ID);
                }
            });

            $("#btnAdd").click(function () {
                Clear();
                $("getfile").show();
                $("#divFile").dialog({ title: "添加文件" }).dialog("open");
            });


            $("#btnSearch").click(function () {
                $("#divList").datagrid('reload',
               {
                   s_name: $("#s_name").val()
               });
            });

            $("#btnEdit").click(function () {
                debugger;
                var row = $("#divList").datagrid("getSelected");
                if (row != null) {
                    $("getfile").hide();
                    $("#hdId").val(row.id); //文件id
                    $("txtFileName").val(row.NAME);
                    $("cmbFileType").combobox(row.TYPENAME);
                    //$("").val(row.NAME);
                    //$("").val(row.NAME);
                    $("#divFile").dialog({ title: "编辑文件" }).dialog("open");
                }
                else {
                    $.messager.alert("提示", "没有选中任何文件", "error");
                }
            });

            $("#btnDelete").click(function () {
                var row = $("#divList").datagrid("getSelected");
                //alert(row.id);
                if (row != null) {
                    $.messager.confirm("提示", "此操作会删除该条数据，确定要删除吗？", function (r) {
                        if (r) {
                            $.post("Handler/BFileHandler.ashx?opt=delete",
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

            $("#btnSubmit").click(function () {
                //$.post("Handler/BFileHandler.ashx?opt=check", {
                //        id: $("#hdId").val(),
                //        filename: $("#txtFileName").val()
                //    }, function (data) {
                //        if (data == "success") {
                $.post("Handler/BFileHandler.ashx?opt=save", {
                    id: $("#hdId").val(),
                    filename: $("#txtFileName").val(),
                    filepath: '<%=filePath%>',//attFile
                    grade: $("#grade_no").val(),
                    typeid: $("#TypeId").val()
                }, function (data) {
                    if (data == "success") {
                        $.messager.alert("提示", "操作成功", "info", function () {
                            $('#divFile').window('close');
                            $("#divList").datagrid("reload");
                        });
                    }
                    else {

                    }
                });
                // }
                // else {
                //     $.messager.alert("错误", "添加错误！", "error");
                // }
                //});

            });
        });

        //关闭选择窗口函数
        function closeGrade() {
            $("#divGrade").window("close");
        }
        //打开选择窗口函数
        function openGrade() {
            $("#divGrade").window("open");
            $("#divGrade").window("move", { top: $(document).scrollTop() + 10 });
            showGrade();
        }

        function showChecked() {
            var checkedNodes = $("#sltGrade").tree("getSelected");//获取选中的节点
            var grade = "<li orgid='" + checkedNodes.id + "'>" + checkedNodes.text + "</li>";//组装
            $("#ul_grade").html(grade);
            closeGrade();
        }

        var grade_no;
        var grade;
        function showGrade() {
            $("#sltGrade").tree({
                url: 'Handler/BFileHandler.ashx?opt=treeList',
                border: false,
                toolbar: "#tb",
                idField: "id",
                treeField: 'text',
                rownumbers: true,
                autoRowHeight: false,
                columns: [[
                    { title: '模块名称', field: 'text', width: 180 }
                ]],
                onSelect: function (node) {
                    grade_no = node.id;
                    $("#grade_no").val(node.id);
                    grade = node.text;
                    $("#grade").val(node.text);
                }
            });
        }

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
        <input id="grade_no" type="hidden" />
        <input id="grade" type="hidden" />
        <input id="fullpath" type="hidden" />
        <div id="divList"></div>
        <div id="tb">
            <div>
                <%if (funcs.Contains("Add"))
                  { %>
                <a id="btnAdd" href="javascript://" class="easyui-linkbutton" plain="true" iconcls="icon-add">添加文件</a>
                <%} if (funcs.Contains("Edit"))
                  { %>
                <a id="btnEdit" href="javascript://" class="easyui-linkbutton" plain="true" iconcls="icon-edit">编辑文件</a>
                <%} if (funcs.Contains("Delete"))
                  { %>
                <a id="btnDelete" href="javascript://" class="easyui-linkbutton" plain="true" iconcls="icon-remove">删除文件</a>
                <%}%>
            </div>
            <div>
                文件名：
            <input id="s_name" type="text" />
                <a id="btnSearch" href="javascript://" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-search'">查询</a>

            </div>
        </div>

        <div id="divFile" title="添加文件" style="width: 500px; height: 300px; padding: 10px" class="easyui-dialog" data-options="buttons:'#buttons',closed:true,modal:true">
            <table>
                <tr>
                    <td style="width: 90px; height: 30px; text-align: right">文件名：</td>
                    <td>
                        <input id="txtFileName" class="easyui-validatebox txt" required="true" type="text" /></td>
                </tr>
                <tr id="getfile">
                    <td style="width: 90px; height: 30px; text-align: right">选择文件：</td>
                    <td>
                        <%--<input type="file" name="attachment" id="attFile" />--%>
                        <asp:FileUpload runat="server" ID="file" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 90px; height: 30px; text-align: right">文件类型：</td>
                    <td>
                        <select class="easyui-combobox" panelheight="auto" style="width: 150px" id="cmbFileType">
                        </select>
                    </td>
                </tr>
                <tr>
                    <td style="width: 90px; height: 30px; text-align: right">版本：</td>
                    <td>
                        <ul id="ul_grade" class="field_list fl ul_hz_qx"></ul>
                        <input type="button" name="button" id="btnChooseGrade" class="select_btn" sort="1" value="选择" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <br />
                        管理员添加文件默认为审核通过。
                    </td>
                </tr>
            </table>

            <div id="buttons">
                <a id="btnSubmit" href="javascript://" class="easyui-linkbutton" plain="true" iconcls="icon-save">确定</a>
            </div>
        </div>


        <div id="divGrade" title="选择版本" style="width: 400px; height: 500px; border: 0px" class="easyui-dialog" data-options="buttons:'#buttons',closed:true,modal:true">
            <div>
                <div></div>
                <a id="closeGrade" href="javascript:" onclick="closeGrade()"><%--class="error_ico fr"--%>
                    <%--  <img src="images/error.png" />--%>
                </a><span class="tit"></span>
            </div>
            <div>
                <div style="padding: 0px 0px 20px 0px">
                    <ul id="sltGrade">
                        <li style="text-align: center">
                            <%--<img src="Images/ajax-loader.gif" />--%>
                            版本选择
                        </li>
                    </ul>
                </div>
                <table border="0" cellspacing="0" cellpadding="0" class="ma">
                    <tbody>
                        <tr>
                            <td>
                                <input type="button" class="easyui-linkbutton" onclick="showChecked()" value="保存" /></td>
                            <td>
                                <input type="button" class="easyui-linkbutton" value="取消" onclick="closeGrade()" /></td>
                        </tr>
                    </tbody>
                </table>
                <div></div>
            </div>
        </div>
    </form>
</asp:Content>
