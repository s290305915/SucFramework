<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileUpload.aspx.cs" Inherits="WebApplication1.FileUpload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="Scripts/jquery-1.11.1.min.js"></script>
    <script>
        $("#btnUpload").click(function (evt) {
            var fileUpload = $("#fupload").get(0);
            var files = fileUpload.files;

            var data = new FormData();
            for (var i = 0; i < files.length; i++) {
                data.append(files[i].name, files[i]);
            }

            $.ajax({
                url: "Handler/MultiFileHandler.ashx",
                type: "POST",
                data: data,
                contentType: false,
                processData: false,
                success: function (result) { alert(result); },
                error: function (err) {
                    alert(err.statusText)
                }
            });

            evt.preventDefault();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:FileUpload runat="server" ID="file_up" />
            <br />
            <asp:Button runat="server" Text="上传" ID="btn_up" OnClick="btn_up_Click" />
        </div>
        <h3>Upload File using Jquery AJAX in Asp.net</h3>
        <table>
            <tr>
                <td>File:</td>
                <td>
                    <asp:FileUpload ID="fupload" runat="server" onchange='prvimg.UpdatePreview(this)' /></td>
                <td>
                    <asp:Image ID="imgprv" runat="server" Height="90px" Width="75px" /></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="btnUpload" runat="server" CssClass="button" Text="Upload Selected File" /></td>
            </tr>
        </table>
    </form>
</body>
</html>
