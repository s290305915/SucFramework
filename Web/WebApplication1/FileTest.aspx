<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileTest.aspx.cs" Inherits="WebApplication1.FileTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="Scripts/jquery-1.7.1.js"></script>
    <script src="Scripts/ajaxfileupload.js"></script>
    <script src="Scripts/Jquery-easyui-1.4/jquery.easyui.min.js"></script>
    <link href="Scripts/Jquery-easyui-1.4/themes/icon.css" rel="stylesheet" />
    <link href="Scripts/Jquery-easyui-1.4/themes/gray/easyui.css" rel="stylesheet" />
    <title></title>
    <script>
        var cnt = 1;
        $(function () {
            //导入到临时表把数据
            $("#btnExSubmit").click(function () {
                //把文件传到后台，读取里面内容并存到临时表
                ajaxFileUpload();
            });
        });

        function AddFiles() {
            var n = "exfile" + cnt
            var nn = "exfile";
            $("#td_files").append("<label>文件" + cnt + "</label>  <input type='file' id='" + n + "' name='" + nn + "'  onchange='CreateIcons(\"" + n + "\")' style='display: none' />");
            //$(n).trigger('click');
            ChoiceFile("#" + n);
            cnt = cnt + 1;
        }

        function CreateIcons(n) {
            var sn = "";
            var t = "";
            var fileName = $("#" + n).val();
            if (IsExsistFile(fileName)) {   //ie8 报错，不支持indexOf fnames.indexOf(fileName) != -1
                $("#" + n).remove();
                alert("文件已存在，请重新添加！");
                return;
            }
        }

        function ChoiceFile(e) {
            return $(e).click();
        }

        function ajaxFileUpload() {
            var enttype = 3;
            var arrIdList = [];
            var tx = "";
            $("#td_files").children("input").each(function () {
                var msg = $(this).attr("id");
                arrIdList.push(msg);
                tx += msg + ",";
            });
            //alert(tx);
            $.ajaxFileUpload
            ({
                url: 'Handler/EntityHandler.ashx?opt=ReadFile&et=' + enttype, //用于文件上传的服务器端请求地址
                secureuri: false, //一般设置为false
                fileElementId: arrIdList, //['exfile', 'exfile1'], //文件上传空间的id属性  <input type="file" id="file" name="file" />
                dataType: 'text', //返回值类型 一般设置为json
                success: function (data)  //服务器成功响应处理函数
                {
                    $("#sp").text(data);
                    //$.messager.alert("信息", data);// 
                },
                error: function (data)//服务器响应失败处理函数
                {
                    $.messager.alert("错误", data, "error");
                }
            })
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server"  enctype="multipart/form-data">
        <div>

            <div id="divExport" title="外导用户" style="display: block; width: 440px; height: auto; padding: 10px">
                <table>
                    <tr>
                        <td colspan="2" style="width: 300px; height: 30px; text-align: left">外导活动目标用户群数：<label id="lb_extgr" style="color: red"></label>
                            确认目标用户数：<label id="lb_cmttgr" style="color: red"></label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">请先下载模板：</td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <input type="button" id="btn_addfile" value="增加" onclick="AddFiles()" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <a href="a.txt" class="easyui-linkbutton" plain="true">下载模板</a>&nbsp;&nbsp;
                        </td>
                        <td id="td_files">
                            <%-- <label>文件1</label>
                            <input type="file" id="exfile" name="exfile" style="display: none" />--%>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 30px"><a id="btnExSubmit" href="javascript://" class="easyui-linkbutton" plain="true" iconcls="icon-save">导入目标</a>
                        </td>
                        <td style="height: 30px"><a id="btnEndSubmit" href="javascript://" class="easyui-linkbutton" plain="true" iconcls="icon-save">确认导入</a>
                        </td>
                    </tr>
                </table>
                <asp:Button runat="server" ID="btn_xeff" Text="后台" OnClick="btn_xeff_Click" />
            </div>
        </div>
        <span id="sp"></span>
    </form>
</body>
</html>
