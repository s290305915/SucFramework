﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestLists.aspx.cs" Inherits="YanDaoMSF.Admin.TestLists" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <title>后台管理系统</title>
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <!--[if lt IE 9]>
<script src="js/html5.js"></script>
<![endif]-->
    <link href="layer/skin/layer.ext.css" rel="stylesheet" />
    <link href="layer/skin/layer.css" rel="stylesheet" />
    <script src="../js/jquery.js"></script>
    <script src="../js/jquery.mCustomScrollbar.concat.min.js"></script>
    <script src="../layer/layer.js"></script>
    <script src="../js/Base.js"></script>
    <script>
        (function ($) {
            $(window).load(function () {
                $.ajax
                $("a[rel='load-content']").click(function (e) {
                    e.preventDefault();
                    var url = $(this).attr("href");
                    $.get(url, function (data) {
                        $(".content .mCSB_container").append(data); //load new content inside .mCSB_container
                        //scroll-to appended content 
                        $(".content").mCustomScrollbar("scrollTo", "h2:last");
                    });
                });

                $(".content").delegate("a[href='top']", "click", function (e) {
                    e.preventDefault();
                    $(".content").mCustomScrollbar("scrollTo", $(this).attr("href"));
                });
                $(".quit_icon").click(function () {
                    debugger;
                    layer.confirm("您确定要退出本系统吗？", {
                        btt:['确定', '取消']
                    }, function () {
                        clearUser('../Login.aspx/ClearLogin');
                    },
                    function () {
                    });
                    return false;
                });
                $(".set_icon").click(function () {
                    layer.alert("暂无此功能！", { icon: 0 });
                });
            });
        })(jQuery);


        $(document).ready(function () {
            debugger;
            $(".mymenu li dl").next("dd").hide();
            $(".mymenu li dt").click(function () {
                $(this).nextAll("dd").toggle();
            });
            $(".admin_icon").text("<%=username %>");
        });


    </script>
</head>
<body>

    <!--header-->
    <header>
        <h1>
            <img src="../images/admin_logo.png" /></h1>
        <ul class="rt_nav">
            <li><a href="#" class="admin_icon"></a></li>
            <li><a href="#" class="set_icon">账号设置</a></li>
            <li><a href="#" class="quit_icon">安全退出</a></li>
        </ul>
    </header>

    <!--aside nav-->
    <aside class="lt_aside_nav content mCustomScrollbar">
        <h2><a href="#">起始页</a></h2>
        <ul class="mymenu">
           <%-- <li>
                <dl>
                    <dt>芒果基本信息</dt>
                    <dd><a href="../UIBaseMangoPur/MangoPur.aspx">芒果库存列表</a></dd>
                    <dd><a href="../UIBasisMangoClass/MangoList.aspx">芒果分类列表</a></dd>
                    <dd><a href="../UIBasisMangoClass/MangoTyepIn.aspx">新增分类</a></dd>
                </dl>
            </li>
            <li>
                <dl>
                    <dt>采购管理</dt>
                    <dd><a href="../UIBaseMango/Mango.aspx">芒果采购列表</a></dd>
                    <dd><a href="../UIBaseMango/MangoIn.aspx">新增芒果</a></dd>
                </dl>
            </li>
            <li>
                <dl>
                    <dt>销售管理</dt>
                    <dd><a href="../UIBaseMangoSale/MangoSaleIn.aspx">芒果销售列表</a></dd>
                    <dd><a href="../UIBaseMangoSale/MangoSaleIn.aspx">新增销售</a></dd>
                </dl>
            </li>
            <li>
                <dl>
                    <dt>报损管理</dt>
                    <dd><a href="../UIBaseMangoReport/MangoReport.aspx">报损历史记录</a></dd>
                    <dd><a href="../UIBaseMangoReport/MangoReportIn.aspx">新增报损</a></dd>
                </dl>
            </li>
            <li>
                <dl>
                    <dt>销售分析</dt>
                    <dd><a href="../UIBaseCharts/SaleChat.aspx">芒果销售统计</a></dd>
                    <dd><a href="../UIBaseCharts/MangoChat.aspx">芒果采购统计</a></dd>
                </dl>
            </li>
            <li>
                <dl>
                    <dt>系统管理</dt>
                    <dd><a href="#" class="active">账户列表</a></dd>
                    <dd><a href="UsersIn.aspx">新增账户</a></dd>
                    <dd><a href="#" class="quit_icon">退出系统</a></dd>
                </dl>
            </li>--%>
        </ul>
    </aside>

    <section class="rt_wrap content mCustomScrollbar">
        <div class="rt_content">
            <!--开始：以下内容则可删除，仅为素材引用参考-->
            <section>
                <div class="page_title">
                    <h2 class="fl">用户列表</h2>
                </div>
                <div>

                </div>

            </section>
        </div>
    </section>

</body>
</html>