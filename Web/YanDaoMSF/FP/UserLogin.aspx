<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserLogin.aspx.cs" Inherits="YanDaoMSF.FP.UserLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>用户登陆</title>
    <link href="css/login.css" rel="stylesheet" type="text/css" />
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../js/jquery.js"></script>
    <script src="../js/jquery-1.10.2.min.js"></script>
    <script src="../layer/layer.js"></script>
    <script type="text/javascript">
        $(function () {
            //登录
            $("#btnLogin").click(function () {
                var username = $("#txtName").val();
                var userpwd = $("#txtPassword").val();
                var code = $("#txtCode").val();
                if (username == "") {
                    layer.alert("请输入用户名！", { icon: 0 });
                    return false;
                }
                else if (userpwd == "") {
                    layer.alert("请输入密码！", { icon: 0 });
                    return false;
                }
                else if (code == "") {
                    layer.alert("请输入验证码！", { icon: 0 });
                    return false;
                }
                else {
                    checkuser(username, userpwd, code);
                }
                return false;
            });


            var screenwidth, screenheight, mytop, getPosLeft, getPosTop
            screenwidth = $(window).width();
            screenheight = $(window).height();
            //获取滚动条距顶部的偏移
            mytop = $(document).scrollTop();
            //计算弹出层的left
            getPosLeft = screenwidth / 2 - 200;
            //计算弹出层的top
            getPosTop = screenheight / 2 - 150;
            //css定位弹出层
            $("#login").css({ "left": getPosLeft, "top": getPosTop });
            //当浏览器窗口大小改变时
            $(window).resize(function () {
                screenwidth = $(window).width();
                screenheight = $(window).height();
                mytop = $(document).scrollTop();
                getPosLeft = screenwidth / 2 - 200;
                getPosTop = screenheight / 2 - 150;
                $("#login").css({ "left": getPosLeft, "top": getPosTop + mytop });
            });
            //当拉动滚动条时，弹出层跟着移动
            $(window).scroll(function () {
                screenwidth = $(window).width();
                screenheight = $(window).height();
                mytop = $(document).scrollTop();
                getPosLeft = screenwidth / 2 - 200;
                getPosTop = screenheight / 2 - 150;
                $("#login").css({ "left": getPosLeft, "top": getPosTop + mytop });
            });
            //失去焦点与得到焦点
            $("#txtCode").focus(function () {
                $(".divCode").fadeIn(1200);
            });
            $("#txtCode").blur(function () {
                $(".divCode").fadeOut();
            });
            $("#login").fadeIn("slow"); //toggle("slow"); 
            //获取页面文档的高度
            var docheight = $(document).height();
            //追加一个层，使背景变灰
            $("body").append("<div id='greybackground'></div>");
            $("#greybackground").css({ "opacity": "0.1", "height": docheight });


            function checkuser(username, userpwd, code) {
                //debugger;
                $.ajax({
                    type: "Post",
                    url: "UserLogin.aspx/checkUser",
                    data: "{'usern':'" + username + "','userp':'" + userpwd + "','code':'" + code + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        var res = eval(data);
                        if (res.d == "codeerror") {
                            layer.alert("验证码错误！", { icon: 0 });
                            return false;
                        }
                        else if (res.d == "no") {
                            layer.alert("用户名或密码错误！", { icon: 0 });
                            return false;
                        }
                        else {
                            //location.href = "FileUpload.aspx";
                            location.href = "../Default.aspx";
                            //history.go(-1);
                        }
                    },
                    error: function (err) {
                        layer.alert("登录异常！", { icon: 6 });
                    }
                });
                return false;
            }
            //$("#btnLogin").click(function () {
            //    $.get("../ashx/login.ashx",
            //    {
            //        name: $("#").val(),
            //        pwd: encodeURIComponent($("#").val()),
            //        code: $("#").val()
            //    },
            //    function (data) {
            //        switch (data) {
            //            case "code error":
            //                alert("验证码错误！");
            //                break;
            //            case "success":
            //                alert("登录成功！");
            //                break;
            //            case "false":
            //                alert("登录失败！");
            //                break;
            //            default:
            //                alert("数据加载失败，请稍后再试！");
            //                break;
            //        }
            //    });
            //});
        });

        //关闭当前窗口
        function CloseWebPage() {
            if (navigator.userAgent.indexOf("MSIE") > 0) {
                if (navigator.userAgent.indexOf("MSIE 6.0") > 0) {
                    window.opener = null;
                    window.close();
                } else {
                    window.open('', '_top');
                    window.top.close();
                }
            }
            else if (navigator.userAgent.indexOf("Firefox") > 0) {
                window.location.href = 'about:blank ';
            } else {
                window.opener = null;
                window.open('', '_self', '');
                window.close();
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="login" class="login">
            <div class="title"><b>用户登录</b></div>
            <div class="pad">
                <div>
                    <p class="selectinput loginpsw">
                        <label>用户名：</label>
                        <input type="text" tabindex="1" class="txt" size="36" name="username" runat="server" id="txtName" />
                    </p>
                </div>
                <div>
                    <p class="selectinput loginpsw">
                        <label>密&nbsp;&nbsp;&nbsp;码：</label>
                        <input type="password" tabindex="1" class="txt" size="36" name="password" runat="server" id="txtPassword" />
                    </p>
                </div>
                <div>
                    <div>
                        <p class="selectinput loginpsw">
                            <label>验证码：</label>
                            <input type="text" tabindex="1" class="txt" size="36" name="code" runat="server" id="txtCode" />
                        </p>
                    </div>
                </div>
                <div>
                    <input id="btnLogin" type="button" runat="server" value="登 录" />
                </div>
                <div style="margin-top: 10px; margin-left: 3px;">
                    <a href="FilePage.aspx" style="text-decoration: none">返回首页</a>
                </div>
            </div>
            <div class="divCode">
                <img alt="验证码" title="点击刷新验证码" src="Handler/ValidateCode.aspx" onclick="this.src='Handler/ValidateCode.aspx'" />
            </div>
        </div>
    </form>
</body>
</html>
