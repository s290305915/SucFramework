<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxFileUpload11.aspx.cs" Inherits="WebApplication1.AjaxFileUpload11" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="Scripts/jquery-1.8.3.min.js"></script>
    <script src="Scripts/ajaxfileupload1.11.1.js"></script>
    <title></title>
    <script>
        $(function () {
            $("#btn_sub").click(function () {
                //照片异步上传
                //$('#id_photos').change(function () {  //此处用了change事件，当选择好图片打开，关闭窗口时触发此事件
                $.ajaxFileUpload({
                    url: 'Handler/EntityHandler.ashx?opt=ReadFile',   //处理图片的脚本路径
                    type: 'post',       //提交的方式
                    secureuri: false,   //是否启用安全提交
                    fileElementId: 'id_photos',     //file控件ID
                    dataType: 'json',  //服务器返回的数据类型      
                    success: function (data, status) {  //提交成功后自动执行的处理函数
                        if (1 != data.total) return;　　 //因为此处指允许上传单张图片，所以数量如果不是1，那就是有错误了
                        var url = data.files[0].path;
                        $('.id_photos').empty();
                        //此处效果是：当成功上传后会返回一个json数据，里面有url，取出url赋给img标签，然后追加到.id_photos类里显示出图片
                        $('.id_photos').append('<img src="' + url + '" value="' + url + '" style="width:80%" >');
                        //$('.upload-box').remove();
                    },
                    error: function (data, status, e) {   //提交失败自动执行的处理函数
                        alert(e);
                    }
                });
                //});
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div data-role="fieldcontain" class="upload-box">
                <label for="id_photos"><span class="red">* </span>您的有效证件照：</label>
                <input type="file" id="id_photos" name="id_photos" value="上传" style="filter: alpha(opacity=10); -moz-opacity: 10; opacity: 10;" />
                <p style="margin-top: 0.5em; color: #999; font-size: 11pt;">说明：请上传手持证件的半身照，请确保照片内证件信息清晰可读。</p>
            </div>
            <div class="id_photos">
            </div>
            <input type="button" id="btn_sub" value="提交" />
        </div>
        <div runat="server" id="ret" style="text-align:right"></div>
    </form>
</body>
</html>
