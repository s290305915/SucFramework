<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="YanDaoMSF.FP.WebForm1" %>

<!DOCTYPE html>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<title>DataBase Test Demo</title>
    <link href="js/treeview/jquery.treeview.css" rel="stylesheet" type="text/css" />
    <!--<script src="js/jquery-1.4.2-vsdoc.js" type="text/javascript"></script>-->
    <script src="js/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="js/treeview/jquery.treeview.js" type="text/javascript"></script>
    <style type="text/css">
    body{margin:0 auto;font-family:Verdana;font-size:12px;}
    .top{margin:0 auto; width:100%; text-align:center; margin-top:20px;}
    #browser{display:none;}
    </style>
</head>
<body>
<div class="top"><h1>DataBase Test Demo</h1></div>
<ul id="browser" class="databasetree">
        <li ><span class="server">SQL Server</span>
            <ul>
                <li class="closed"><span class="folder">DataBases</span>
                    <ul>
                        <li class="closed"><span class="database">Test</span>
                            <ul></ul>
                        </li>
                            
                    </ul>
                </li>
            </ul>
        </li>
</ul>
<style type="text/css">
#greybackground{background: #000;display: block;z-index: 100;width: 100%;position: absolute;top: 0;left: 0; }
#login{margin:0 auto;width:420px;height:auto;border:solid 1px #ccc;position:absolute;z-index:200;background-color:#fff;}
#login .heard{width:420px; height:29px;background-image:url(images/top_bg.gif); border-bottom:solid 1px #ccc;}
#login .heard .left{float:left;line-height:29px;margin-right:2px;padding-left:10px; color:#5aa608;}
#login .heard .right{float:right;line-height:29px;margin-right:5px;}
#login .heard .right a{color:#999;text-decoration:none;}
#login .heard .right a:hover{color:red;text-decoration:underline;}
#login .content{width:420px; height:200px;}
#login .content li{ list-style:none; padding:5px 0px 5px 30px;}
#login .content .top{ width:100%; margin-top:5px;height:30px;line-height:30px;}
#login .content .top .left{ float:left;width:120px; text-align:right;}
#login .content .top .right{ float:right;width:280px;text-align:right;padding-right:20px;}
#login input,#login,select,#login,button{font-family:inherit;font-size:inherit;font-weight:inherit;*font-size:100%;}
#login input{width: 80%;padding: 7px 7px 6px;border-color: #B3B3B3 #EAEAEA #EAEAEA #B3B3B3;border-style: solid;border-width: 1px;color:black;}
#login select{width: 86%;padding: 7px 7px 6px; border-color: #B3B3B3 #EAEAEA #EAEAEA #B3B3B3;border-style: solid;border-width: 1px;color:black;}
#login button,#login .btn-submit,#login .button,#login .btn-submit:focus,#login .button:focus,.btn-submit,.button{border-left: 1px solid #C6C6C6;
border-right: 1px solid #DDDDDD;border-top: 1px solid #DDDDDD;border-bottom: 1px solid #C6C6C6;
cursor:pointer;width:auto;margin:0 10px 0 0;padding-bottom:3px;line-height:1.3em;
color:#515151;font-weight:bold;background:url(images/button.png) #e5e3e3 repeat-x 0 0;
height:32px;padding-left:12px;padding-right: 12px;padding-top: 6px;}
#login button:hover,#login .btn-submit:hover,.button:hover,.btn-submit:hover {background-image:none;}
#login .btn-submit,#login .btn-submit:focus,.btn-submit {width:auto;border-color:#5C91A4 #2B7089 #1A6480 #2A6F89;background-image:url(images/button_highlight.png);background-color:#4e85bb;color:#fff;}
#login .bottom-btn{width:90%; margin:0 auto; padding-top:7px; border-top:1px #ccc solid; margin-top:5px;}

#layer{position:relative;}
#poper{position:absolute;z-index:10;display:none;left:39px;border:solid 1px #ccc; background-color:#fff;}

#poper .heard{height:25px;line-height:25px; width:100%;text-align:right; }
#poper .heard a{color:#999;text-decoration:none;}
#poper .heard a:hover{color:red;text-decoration:underline;}
#poper .first{height:30px;line-height:30px; width:100%;text-align:center; color:#5aa608;}
#poper .first a{color:#5aa608;text-decoration:underline;}
#poper .second{height:20px;line-height:20px; width:100%;text-align:left; margin-left:10px;}
#poper .second a{color:#999; text-decoration:none;}
#poper .second a:hover{color:#5aa608; text-decoration:underline;}
</style>
<div id="login">
    <div class="heard"><div class="left">Connect to Server</div><div class="right"><a href="javascript:void(0);" id="login_close" title="close">close</a></div></div>
    <div class="content">
        <div class="top">
            <div class="left">Server name:</div>
            <div class="right">
                <div id="layer">
                    <input id="txtServer" type="text" />
                    <div id="poper">
                        <div class="heard"><a id="layer_close" href="javascript:void(0);" title="close">close</a>&nbsp;&nbsp;</div>
                        <div class="first"><a id="loadServer" href="javascript:void(0);" title="点击加载SQL Server服务列表">点击加载SQL Server服务列表</a></div>
                    </div>
                </div>
            </div>
        </div>
        <div class="top"><div class="left">Authentication:</div><div class="right"><select id="selectAuthentication"><option value="windows">Windows Authentication</option><option value="sql" selected="selected">SQL Server Authentication</option></select></div></div>
        <div class="top"><div class="left">Login:</div><div class="right"><input id="txtUserName" type="text" /></div></div>
        <div class="top"><div class="left">Password:</div><div class="right"><input id="txtPassword" type="password" /></div></div>
        <div class="bottom-btn"><input id="btnConnect" type="button" class="btn-submit" value="Connect" /><input id="btnCancel" type="button" class="button" value="Cancel" /><font id="message"></font></div>
    </div>
</div>
</body>
</html>
<script type="text/javascript">
    $(document).ready(function () {
        $("#browser").treeview();

        $("#selectAuthentication").change(function () {
            if ($(this).val() == "windows") {
                $("#txtUserName,#txtPassword").css("background-color", "#eee");
                $("#txtUserName,#txtPassword").attr("disabled", "disabled");
            } else {
                $("#txtUserName,#txtPassword").css("background-color", "#fff");
                $("#txtUserName,#txtPassword").removeAttr("disabled");
            }
        });

        $("#txtServer").focus(function () { $("#poper").fadeIn("fast"); });

        $("#poper").css({ "top": $("#txtServer").outerHeight() + 1, "width": $("#txtServer").outerWidth() });

        $("#layer_close").click(function () {
            $(this).parent().parent().fadeOut("fast");
        });

        $("#loadServer").click(function () {
            $.ajax({
                type: "get",
                dataType: "text",
                timeout: 300000,
                url: "ashx/Handler.ashx",
                data: "flag=server",
                beforeSend: function () { $("#loadServer").fadeOut("fast"); $("#poper .first").html("<img src='images/ajax-loader.gif' title='正在加载中，请稍后……' />正在加载中，请稍后……") },
                success: function (data) {
                    if (data == "error") {
                        $("#poper .first").html("<font color=red>服务列表加载失败，请刷新重新加载</font>");
                    } else if (data == "empty") {
                        $("#poper .first").html("<font color=red>没有数据，请手动填写</font>");
                    } else {
                        $("#poper .first").remove();
                        $("#poper").append(data);
                        alink();
                    }
                },
                error: function () { $("#poper .first").html("<font color=red>系统发生错误，请联系管理员！</font>"); }
            });
        });
    })


    $("#btnConnect").click(function () {
        if ($("#selectAuthentication").val() == "sql") { //SQL Server Authentication
            if ($("#txtServer").val().length < 1) {
                $("#message").css("color", "red"); $("#message").html("请输入Server name");
            } else if ($("#txtUserName").val().length < 1) {
                $("#message").css("color", "red"); $("#message").html("请输入Login");
            } else if ($("#txtPassword").val().length < 1) {
                $("#message").css("color", "red"); $("#message").html("请输入Password");
            } else {
                $.ajax({
                    type: "get",
                    dataType: "text",
                    timeout: 300000,
                    url: "ashx/Handler.ashx",
                    data: "flag=login&sqlServer=" + encodeURIComponent($("#txtServer").val()) + "&user=" + encodeURIComponent($("#txtUserName").val()) + "&password=" + encodeURIComponent($("#txtPassword").val()),
                    beforeSend: function () { $("#message").css("color", "#5aa608"); $("#message").html("<img src='images/ajax-loader.gif' title='正在加载中，请稍后……' />正在验证，请稍后……"); },
                    success: function (data) {
                        if (data == "True") {
                            $("#message").html("");
                            hideLogin();
                            $("#browser").fadeIn("fast");
                        }
                    },
                    error: function () { $("#message").css("color", "red"); $("#message").html("登录失败"); }
                });
            }
        }
    });

    $(function () {
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
        //点击关闭按钮
        $("#login_close").click(function () {
            hideLogin();
        });

        $("#btnOK").click(function () {
            $("#login").fadeOut("slow");
            $("#browser").fadeIn("slow");
            //删除变灰的层
            $("#greybackground").remove();
            return false;
        });
        $.get("ashx/Handler.ashx?flag=islogin", function (data) {
            if (data == "True") {//没有登录，显示登录框
                hideLogin();
            } else {
                showLogin();
            }
        });
    });

    function showLogin() {//显示登陆框
        $("#login").fadeIn("slow");
        //获取页面文档的高度
        var docheight = $(document).height();
        //追加一个层，使背景变灰
        $("body").append("<div id='greybackground'></div>");
        $("#greybackground").css({ "opacity": "0.5", "height": docheight });
        return false;
    }
    function hideLogin() {
        $("#login").fadeOut("slow");
        //删除变灰的层
        $("#greybackground").remove();
        return false;
    }
    function alink() {//生成的a追加click事件
        $("#poper .second a").each(function () {
            $(this).click(function () {
                var a = $(this).html();
                $("#txtServer").val(a);
            });
        });
    }
</script>