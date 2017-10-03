<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="BootstrapUI.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>点击弹出登陆</title>
    <link href="Content/login.css" rel="stylesheet" />
    <link href="Scripts/layer/skin/layer.css" rel="stylesheet" />
    <link href="Scripts/layer/skin/layer.ext.css" rel="stylesheet" />
    <script src="Scripts/jquery.min.js"></script>
    <script src="Scripts/layer/layer.js"></script>
    <script src="Scripts/layer/extend/layer.ext.js"></script>
    <script type="text/javascript">
        document.write("<p>浏览器：")
        document.write(navigator.appName + "</p>")

        document.write("<p>浏览器版本：")
        document.write(navigator.appVersion + "</p>")

        document.write("<p>代码：")
        document.write(navigator.appCodeName + "</p>")

        document.write("<p>平台：")
        document.write(navigator.platform + "</p>")

        document.write("<p>Cookies 启用：")
        document.write(navigator.cookieEnabled + "</p>")

        document.write("<p>浏览器的用户代理报头：")
        document.write(navigator.userAgent + "</p>")


        onerror = handleErr
        var txt = ""
        function handleErr(msg, url, l) {
            txt = "本页中存在错误。\n\n"
            txt += "错误：" + msg + "\n"
            txt += "URL: " + url + "\n"
            txt += "行：" + l + "\n\n"
            txt += "点击“确定”继续。\n\n"
            layer.alert(txt, { icon: 0 });
            return true
        }

        jQuery(document).ready(function ($) {

            $('#login').click(function () {
                $('.theme-popover-mask').fadeIn(100);
                $('#dialog1').slideDown(200);
            })
            $('.theme-poptit .close').click(function () {
                $('.theme-popover-mask').fadeOut(100);
                $('#dialog1').slideUp(200);
                $('#dialog2').slideUp(200);
            })

            $("#logout").click(function () {
                $('.theme-popover-mask').fadeIn(100);
                $('#dialog2').slideDown(200);
            });


            $("#submit").click(function () {
                var lg = $("#log").val();
                var pd = $("#pwd").val();
                layer.confirm("您确定要执行操作？",
                    { title: "xxxxx" },
                    {
                        btt: ['OK']
                    }, function (index) {
                        $.ajax({
                            type: "Post",
                            url: "Login.aspx/Check",
                            data: "{'a':'" + lg + "','b':'" + pd + "'}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (data) {
                                var res = eval(data);
                                layer.alert(res.d, { icon: 0 });
                            }
                        });
                    });

            });
        })

        /////取cookie根据名字
        //function getCookie(c_name) {
        //    if (document.cookie.length > 0) {                            //表示有cookie
        //        c_start = document.cookie.indexOf(c_name + "=")     //a=xx；b=cc ------------>  "a="
        //        if (c_start != -1) {                                                    //表示查找到有这个cookie
        //            c_start = c_start + c_name.length + 1                 // "a=" 在里面是 24,+ 名字的长度再+1  
        //            c_end = document.cookie.indexOf(";", c_start)       //从刚刚的位置找到到后面第一个;作为结尾
        //            if (c_end == -1)
        //                c_end = document.cookie.length                 //没找到？ 那么直接到结尾
        //            return unescape(document.cookie.substring(c_start, c_end))
        //        }
        //    }
        //    return ""
        //}
        /////存cookie 根据 名字，值，存在天数
        //function setCookie(c_name, value, expiredays) {
        //    var exdate = new Date()
        //    exdate.setDate(exdate.getDate() + expiredays)       //cookie存在天数
        //    document.cookie = c_name + "=" + escape(value) + ((expiredays == null) ? "" : "; expires=" + exdate.toGMTString())
        //}

        //function timedMsg() {
        //    var t = setTimeout("alert('5 秒！')", 5000)
        //    for (i = 5; i > 0; i++)
        //        var t1 = setTimeout("document.getElementById('msgbtn').value='倒计时" + i + "'", i * 1000);
        //}

    </script>

</head>

<body>

    <input type="button" id="msgbtn" value="显示定时的警告框" onclick="timedMsg()">

    <div class="theme-buy">
        <a class="btn btn-primary btn-large theme-login" id="login" href="javascript:;">点击登陆</a>
        <a class="btn btn-primary btn-large theme-login" id="logout" href="javascript:;">点击删除</a>
    </div>
    <div class="theme-popover" id="dialog1">
        <div class="theme-poptit">
            <a href="javascript:;" title="关闭" class="close">×</a>
            <h3>登录 是一种态度</h3>
        </div>
        <div class="theme-popbod dform">
            <div class="theme-signin">
                <ol>
                    <li>
                        <h4>你必须先登录！</h4>
                    </li>
                    <li><strong>用户名：</strong><input class="ipt" name="username" type="text" id="log" value="username" size="20" /></li>
                    <li><strong>密码：</strong><input class="ipt" type="password" id="pwd" value="password" size="20" /></li>
                    <li>
                        <input class="btn btn-primary" type="button" id="submit" value=" 登 录 " /></li>
                </ol>
            </div>
        </div>
    </div>

    <div class="theme-popover" id="dialog2">
        <div class="theme-poptit">
            <a href="javascript:;" title="关闭" class="close">×</a>
            <h3>登录 是一种态度</h3>
        </div>
        <div class="theme-popbod dform">
            <div class="theme-signin">
                <ol>
                    <li>
                        <h4>你必须先登录！</h4>
                    </li>
                    <li><strong>啊哈：</strong><input class="ipt" type="text" id="xd" value="username" size="20" /></li>
                    <li><strong>吧嗒：</strong><input class="ipt" type="text" id="xdr" value="username" size="20" /></li>
                    <li>
                        <input class="btn btn-primary" type="button" id="btn_xd" value=" 笑 Cry " /></li>
                </ol>
            </div>
        </div>
    </div>
    <div class="theme-popover-mask" id="mask"></div>
</body>
</html>

