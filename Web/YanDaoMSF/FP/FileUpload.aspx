<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileUpload.aspx.cs" Inherits="YanDaoMSF.FP.FileUpload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../js/uploadify/default.css" rel="stylesheet" />
    <link href="../js/uploadify/uploadify.css" rel="stylesheet" />
    <script src="../js/uploadify/jquery-1.7.2.min.js"></script>
    <script src="../js/uploadify/jquery.uploadify.min.js"></script>
    <script src="../js/uploadify/swfobject.js"></script>


    <%--<script src="../js/jquery-1.7.1.js"></script>--%>
    <%--<script src="../js/ajaxfileupload.js"></script>--%>


    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <link href="../js/Jquery-easyui-1.4/themes/default/easyui.css" rel="stylesheet" />
    <link href="../js/Jquery-easyui-1.4/themes/icon.css" rel="stylesheet" />
    <%--<script src="../js/Jquery-easyui-1.4/jquery.min.js"></script>--%>
    <script src="../js/Jquery-easyui-1.4/jquery.easyui.min.js"></script>
    <script src="../layer/layer.js"></script>
    <style>
        .fu_list
        {
            width: 600px;
            height: 230px;
            background: #ebebeb;
            font-size: 12px;
        }

            .fu_list td
            {
                padding: 5px;
                line-height: 20px;
                background-color: #fff;
            }

            .fu_list table
            {
                width: 100%;
                border: 1px solid #ebebeb;
            }

            .fu_list thead td
            {
                background-color: #f4f4f4;
            }

            .fu_list b
            {
                font-size: 14px;
            }
        /*file容器样式*/
        a.files
        {
            width: 90px;
            height: 30px;
            overflow: hidden;
            display: block;
            border: 1px solid #BEBEBE;
            background: url(Images//fu_btn.gif) left top no-repeat;
            text-decoration: none;
        }

            a.files:hover
            {
                background-color: #FFFFEE;
                background-position: 0 -30px;
            }
            /*file设为透明，并覆盖整个触发面*/
            a.files input
            {
                margin-left: -350px;
                font-size: 30px;
                cursor: pointer;
                filter: alpha(opacity=0);
                opacity: 0;
            }
            /*取消点击时的虚线框*/
            a.files, a.files input
            {
                outline: none; /*ff*/
                hide-focus: expression(this.hideFocus=true); /*ie*/
            }

        .cent
        {
            width: 600px;
            margin-left: auto;
            margin-right: auto;
            height: 300px;
            margin-top: 100px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            //var fname = $("#tname").val();
            //var filetype = $("#filecomb").combobox("getValue");
            //var filetree = $("#filetree").combotree('tree').tree('getSelected');
            $('#filecomb').combobox({
                url: 'Handler/FileHandler.ashx?opt=FileType',
                valueField: 'ID',
                required: true,
                textField: 'NAME',
                onSelect: function () {
                    var sValue = $("#filecomb").combobox("getValue");
                    $("#<%=sComb.ClientID %>").val(sValue);
                }

            });
            $('#filetree').combotree({
                url: 'Handler/FileHandler.ashx?opt=treeList',
                required: true,
                width: 260,
                onSelect: function (node, checked) {
                    var tree = $(this).tree;
                    var isLeaf = tree('isLeaf', node.target);
                    if (!isLeaf) {
                        $('#filetree').combotree('clear');
                        layer.alert("不能选择目录！", { icon: 0 });
                    }
                    else {
                        if (node.level == "6") {
                            var sTree = $("#filetree").combotree('tree');
                            var sSelected = sTree.tree('getSelected');
                            $("#<%=sTree.ClientID %>").val(sSelected.id);
                        }
                        else {
                            $('#filetree').combotree('clear');
                            layer.alert("不能选择目录！", { icon: 0 });
                        }
                    }
                }
            });

            //$("#btnExSubmit").click(function () {
            //    //把文件传到后台，读取里面内容并存表
            //    ajaxFileUpload();
            //});


            $("#uploadify").uploadify({
                //开启调试
                'debug': false,
                //是否自动上传
                'auto': false,
                'buttonText': '浏览...',
                //flash
                'swf': "../js/uploadify/uploadify.swf",
                //文件选择后的容器ID
                'queueID': 'uploadfileQueue',
                'uploader': 'Handler/FileHandler.ashx?opt=ReadFile',//&fname=' + fname + '&ftype=' + ftype + '&ftree=' + ftree + "&uid=" + '<%=SucLib.Common.SucCookie.Read("username")%>',
                //'formData': { 'fname': $("#tname").val(), 'ftype': $("#filecomb").combobox("getValue"), 'ftree': $("#filetree").combotree('tree').tree('getSelected'), 'uid': '<%=SucLib.Common.SucCookie.Read("username")%>' },
                'width': '75',
                'height': '24',
                'multi': false,
                'fileTypeDesc': '支持的格式：',
                'fileTypeExts': '*.doc;*.mp4;*.wmv;*.ppt;*.docx;*.pptx;*.txt;*.xls;*.xlsx;*.rar;*.zip;*.jpg;*.jpeg;*.mp3;*.mkv;*.iso',
                'fileSizeLimit': '100MB',
                'removeTimeout': 1,

                //返回一个错误，选择文件的时候触发
                'onSelectError': function (file, errorCode, errorMsg) {
                    switch (errorCode) {
                        case -100:
                            $.messager.alert("信息", "上传的文件数量已经超出系统限制的" + $('#uploadify').uploadify('settings', 'queueSizeLimit') + "个文件！", "info");
                            break;
                        case -110:
                            $.messager.alert("信息", "文件 [" + file.name + "] 大小超出系统限制的" + $('#uploadify').uploadify('settings', 'fileSizeLimit') + "大小！", "info");
                            break;
                        case -120:
                            $.messager.alert("信息", "文件 [" + file.name + "] 大小异常！", "info");
                            break;
                        case -130:
                            $.messager.alert("信息", "文件 [" + file.name + "] 类型不正确！", "info");
                            break;
                    }
                },
                //检测FLASH失败调用
                'onFallback': function () {
                    $.messager.alert("错误", "上传错误，请稍后再试。", "error");
                },
                //上传到服务器，服务器返回相应信息到data里
                'onUploadSuccess': function (file, data, response) {
                    var qdata = 'type:' + data + ',fname:' + $("#tname").val() + ',ftype:' + $("#sComb").val() + ',ftree:' + $("#sTree").val() + ',uid:' + '<%=SucLib.Common.SucCookie.Read("username")%>';
                    $.ajax({
                        type: "Post",
                        url: "Handler/FileHandler.ashx?opt=SaveInfo&q=" + qdata,
                        data: {},
                        contentType: "application/text; charset=utf-8",
                        dataType: "text",
                        success: function (data) {
                            if (data != null) {
                                //$.messager.alert("提示", "OK", "info");
                                $.messager.alert("信息", data, "info");
                                return;
                            }
                        }
                    });
                }
                //,
                //'onUploadStart': function (file) {
                //    $("#file_upload").uploadify("settings", "formData", { 'fname': fname, 'ftype': ftype, 'ftree': ftree, 'uid': '<%=SucLib.Common.SucCookie.Read("username")%>' });
                //    //在onUploadStart事件中，也就是上传之前，把参数写好传递到后台。  
                //}
            });


        });



        function ajaxFileUpload() {
            //var fname = $("#tname").val();
            //var filetype = $("#filecomb").combobox("getValue");
            //var filetree = $("#filetree").combotree('tree').tree('getSelected');
            var chk = checkForm();
            if (chk != false) {
                debugger;
                $.ajaxFileUpload
                ({
                    url: 'Handler/FileHandler.ashx?opt=ReadFile&fname=' + fname + '&ftype=' + filetype + '&ftree=' + filetree, //用于文件上传的服务器端请求地址
                    secureuri: false, //一般设置为false
                    fileElementId: 'exfile', //文件上传空间的id属性  <input type="file" id="file" name="file" />
                    dataType: 'text', //返回值类型 一般设置为json
                    success: function (data)  //服务器成功响应处理函数
                    {
                        $.messager.alert("信息", data, "info");// 
                    },
                    error: function (data)//服务器响应失败处理函数
                    {
                        $.messager.alert("错误", data, "error");
                    }
                })
                return false;
            }
        }

        function CheckFile(obj) {
            var array = new Array('doc', 'mp4', 'wmv', 'ppt', 'docx', 'pptx', 'txt', 'xls', 'xlsx', 'rar', 'zip', 'jpg', 'jpeg', 'mp3', 'mkv', 'iso');
            if (obj.value == '') {
                layer.alert("请选择要上传的文件！", { icon: 0 });
                return false;
            }
            else {
                var fileContentType = obj.value.match(/^(.*)(\.)(.{1,8})$/)[3];
                var isExists = false;
                for (var i in array) {
                    if (fileContentType.toLowerCase() == array[i].toLowerCase()) {
                        isExists = true;
                        return true;
                    }
                }
                if (isExists == false) {
                    obj.value = null;
                    layer.alert("上传文件类型不正确！", { icon: 0 });
                    return false;
                }
                return false;
            }
        }

        function checkForm() {
            if (!checkVal("#tname")) {
                layer.alert("请输入要上传的文件名称！", { icon: 0 });
                return false;
            }
            else if (!checkComb("#filecomb")) {
                layer.alert("请选择文件类型！", { icon: 0 });
                return false;
            }
            else if (!checkTree("#filetree")) {
                layer.alert("请选择文件组织！", { icon: 0 });
                return false;
            }
            else {
                return true;
            }
        }

        function checkVal(element) {
            var eleLen = $(element).val();
            if (eleLen.length > 0) {
                return true;
            }
            return false;
        }

        function checkComb(element) {
            var sValue = $(element).combobox("getValue");
            if (sValue.length > 0) {
                return true;
            }
            return false;
        }

        function checkTree(element) {
            var sTree = $(element).combotree('tree');
            var sSelected = sTree.tree('getSelected');
            if (sSelected != null) {
                return true;
            }
            return false;
        }


        function doUplaod() {
            if (checkForm()) {
                var fname = $("#tname").val();
                var ftype = $("#filecomb").combobox("getValue");
                var ftree = $("#filetree").combotree('tree').tree('getSelected');
                //$('#uploadify').uploadify("settings", "formData", { 'fname': fname, 'ftype': ftype, 'ftree': ftree, uid: '<%=SucLib.Common.SucCookie.Read("username")%>' });
                $('#uploadify').uploadify('upload', '*');
            }
        }

        function closeLoad() {
            $('#uploadify').uploadify('cancel', '*');
        }

    </script>
    <title>文件上传</title>
</head>
<body>
    <div class="cent">
        <form id="uploadForm" runat="server" style="height: 230px;">
            <table border="0" cellspacing="1" class="fu_list">
                <thead>
                    <tr>
                        <td colspan="2">
                            <b>上传文件</b>
                        </td>
                        <td>
                            <b><a href="FilePage.aspx">返回首页</a>
                            </b>
                        </td>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td align="right" width="15%" style="line-height: 35px;" id="filename">文件名称：</td>
                        <td colspan="2">
                            <asp:TextBox ID="tname" CssClass="easyui-textbox" runat="server" Width="250px" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" width="15%" style="line-height: 35px;" id="filetype">文件上传类型：</td>
                        <td colspan="2"><span style="color: #ff0000;">DOC,MP4,WMV,PPT,DOCX,PPTX,TXT,XLS,XLSX,RAR,ZIP,JPG,JPEG,MP3</span></td>

                    </tr>
                    <tr>
                        <td align="right" width="15%" style="line-height: 35px;" id="filetype">文件类型：</td>
                        <td colspan="2">
                            <input id="filecomb" class="easyui-combobox">
                            <asp:HiddenField ID="sComb" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" width="20%" style="line-height: 35px;">组织树：</td>
                        <td colspan="2">
                            <input id="filetree" class="easyui-combobox">
                            <asp:HiddenField ID="sTree" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" width="15%" style="line-height: 35px;">添加文件：</td>
                        <td>
                            <asp:FileUpload runat="server" ID="file_open" Visible="false" Width="300px" onchange="CheckFile(this);" />
                            <%--<input type="file" id="exfile" name="exfile" onchange="CheckFile(this);" />--%>
                            <input type="file" name="uploadify" id="uploadify" />
                            <img id="idProcess" style="display: none;" src="Images/loading.gif" /></td>
                        <td>
                            <label id="lblPercent"></label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <div id="uploadfileQueue" style="padding: 3px;">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <table border="0" cellspacing="0">
                                <thead>
                                    <tr>
                                        <td>上传须知&nbsp;&nbsp;</td>
                                        <td width="100">
                                            <asp:Label runat="server" ID="lb_filepath"></asp:Label></td>
                                    </tr>
                                </thead>
                                <tbody id="idFileList"></tbody>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="color: gray">
                            <br />
                            1、只允许上传固定后缀名文件(视频，文档，幻灯片)。
                        <br />
                            2、视频固定格式为.mp4或wmv、文档固定格式为.doc、幻灯片固定格式为.ppt。<br />
                            (<b style="color: red;">视频格式可用视频工厂进行转换，文档幻灯片格式请在保存类型下拉列表中选择97-2003文件类型。</b>)
                    <br />
                            3、360浏览器如果点击添加文件无响应，请使用360极速模式或者更换浏览器。
                        <br />
                            4、文件上传成功后，可在几分钟后根据文件列表查看已通过审核的文件。  </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="center" id="idMsg">
                            <asp:Button runat="server" Text="开始上传" ID="btn_upload" OnClientClick="return checkForm()" OnClick="btn_upload_Click" Visible="false" />
                            <%--<a id="btnExSub" href="javascript:$('#uploadify').uploadify('upload','*');" class="easyui-linkbutton" plain="true" iconcls="icon-save">开始上传</a>--%>
                            <a id="btnExSubmit" href="javascript://" class="easyui-linkbutton" plain="true" onclick="doUplaod()" iconcls="icon-save">开始上传</a>
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button runat="server" Text="全部取消" ID="btn_cancel" Enabled="false" Visible="false" OnClick="btn_cancel_Click" />
                            <a id="btnEndSubmit" href="javascript://" class="easyui-linkbutton" plain="true" onclick="closeLoad()" iconcls="icon-cancel">取消上传</a>
                        </td>
                    </tr>
                </tbody>
            </table>
        </form>
    </div>
</body>
</html>
