<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="YanDaoMSF.Admin.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <title>后台登录</title>
    <meta name="author" content="DeathGhost" />
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <link href="../layer/skin/layer.ext.css" rel="stylesheet" />
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../js/jquery.js"></script>
    <script src="../js/verificationNumbers.js"></script>
    <script src="../js/Particleground.js"></script>
    <script src="../layer/layer.js"></script>
    <style>
        body
        {
            height: 100%;
            background: #16a085;
            overflow: hidden;
        }

        canvas
        {
            z-index: -1;
            position: absolute;
        }

        .admin_login dd:before
        {
            line-height: 40px;
        }

        .admin_login dd .login_txtbx
        {
            height: 40px;
            width: 100%;
        }
    </style>

    <script>
        $(document).ready(function () {
            $('body').particleground({
                dotColor: '#5cbdaa',
                lineColor: '#5cbdaa'
            });
            function checkuser(username, userpwd) {
                $.ajax({
                    type: "Post",
                    url: "Login.aspx/checkUser",
                    data: "{'usern':'" + username + "','userp':'" + userpwd + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        var res = eval(data);
                        if (res.d != "ok") {
                            layer.alert("用户名或密码错误！", { icon: 0 });
                            return false;
                        }
                        else if (res.d == "noauth") {
                            layer.alert("用户无权限登陆！", { icon: 0 });
                            return false;
                        }
                        else {
                            location.href = "Default.aspx";
                        }
                    },
                    error: function (err) {
                        layer.alert("登录异常！", { icon: 6 });
                    }
                });
                return false;
            }
            $(".submit_btn").click(function () {
                var username = $("#username").val();
                var userpwd = $("#userpwd").val();
                if (username == "") {
                    layer.alert("请输入用户名！", { icon: 0 });
                    return false;
                }
                else if (userpwd == "") {
                    layer.alert("请输入密码！", { icon: 0 });
                    return false;
                }
                else {
                    checkuser(username, userpwd);
                }
                return false;
            });


        });
    </script>
</head>
<body>
    <form runat="server" id="form">
        <dl class="admin_login">
            <dt>
                <strong>后台管理系统</strong>
                <em>Management System</em>
            </dt>
            <dd class="user_icon">
                <asp:TextBox CssClass="login_txtbx" placeholder="账号" runat="server" ID="username" />
            </dd>
            <dd class="pwd_icon">
                <asp:TextBox CssClass="login_txtbx" placeholder="密码" runat="server" ID="userpwd" TextMode="Password" />
            </dd>
            <dd>
                <asp:Button ID="login" CssClass="submit_btn" runat="server" Text="立即登录"></asp:Button>
            </dd>
            <dd>
                <p>2015-2016 版权所有</p>
                <p>川A-20100823-1</p>
            </dd>
        </dl>
    </form>
</body>
</html>
