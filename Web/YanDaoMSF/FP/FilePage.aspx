<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FilePage.aspx.cs" Inherits="YanDaoMSF.FP.HTML.FilePage" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>成都市盐道街外语学校</title>
    <!------头尾部css样式-------->
    <link rel="stylesheet" type="text/css" href="../FP/css/muBanYe.css" />
    <link rel="stylesheet" type="text/css" href="../FP/css/index.css" />
    <!--大焦点图片-->
    <script type="text/javascript" src="../FP/js/pptBox.js"></script>
    <script type="text/javascript" src="../FP/js/jquery-1.10.2.min.js"></script>
    <!--小焦点图片
<link rel="stylesheet" type="text/css" href="../css/xixi.css"/>
<script type="text/javascript" src="../js/lrtk.js"></script>
<script type="text/javascript" src="../js/jquery.js"></script>-->
    <script type="text/javascript" src="../FP/js/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="../FP/js/simplefoucs.js"></script>
    <link rel="stylesheet" type="text/css" href="../FP/css/simplefoucs_lrtk.css" />
    <link rel="stylesheet" type="text/css" href="../FP/css/flipmenu.css" />
    <script type="text/javascript" src="../FP/js/flipmenu-min.js"></script>
    <script type="text/javascript" src="../FP/js/jquery-1.3.2.min.js"></script>
    <script src="../js/Jquery-easyui-1.4/extendJS.js"></script>
    <script src="../js/Jquery-easyui-1.4/jquery.min.js"></script>
    <script src="../js/Jquery-easyui-1.4/jquery.easyui.min.js"></script>
    <script src="../js/Jquery-easyui-1.4/locale/easyui-lang-zh_CN.js"></script>
    <link href="../js/Jquery-easyui-1.4/themes/metro/easyui.css" rel="stylesheet" />
    <link href="../js/Jquery-easyui-1.4/themes/icon.css" rel="stylesheet" />
    <script src="../js/formatDatebox.js"></script>
    <style>
        .drop
        {
            width: 100px;
            height: 20px;
            float: left;
            margin-top: 8px;
            line-height: 20px;
            font-family: '微软雅黑', '黑体';
            font-size: 12px;
            color: #000;
            text-align: center;
            background-color: #FFF;
        }
    </style>
    <!----下拉Js---->
    <script type="text/javascript">
        $(document).ready(function () {
            var menu1 = new Flipmenu("flip_menu1");
            var menu2 = new Flipmenu("flip_menu2");
            var menu1 = new Flipmenu("flip_menu3");
            var menu2 = new Flipmenu("flip_menu4");
            var menu1 = new Flipmenu("flip_menu5");
            var menu2 = new Flipmenu("flip_menu6");
            var menu1 = new Flipmenu("flip_menu7");
            var menu2 = new Flipmenu("flip_menu8");
            var menu1 = new Flipmenu("flip_menu9");
            var menu2 = new Flipmenu("flip_menu10");
            var menu1 = new Flipmenu("flip_menu11");
            var menu2 = new Flipmenu("flip_menu12");
            var menu1 = new Flipmenu("flip_menu13");
            var menu2 = new Flipmenu("flip_menu14");
            var menu1 = new Flipmenu("flip_menu15");
            var menu2 = new Flipmenu("flip_menu16");
            //$("#divMList").datagrid({
            //    url: "Handler/FileHandler.ashx?opt=list&mid=1&ftype=0&stype=99&key="
            //});

            $('#divMList').datagrid({
                url: "Handler/FileHandler.ashx?opt=list&mid=1&ftype=0&stype=99&key=",
                idField: "ID",
                singleSelect: true,
                border: false,
                sortable: true,
                loadMsg: '数据加载中请稍后……',
                fitColumns: true,
                rownumbers: true, //是否加行号 
                pagination: true, //是否显式分页 
                pageSize: 20, //页容量，必须和pageList对应起来，否则会报错 
                //pageNumber: 1, //默认显示第几页 
                pageList: [20, 30, 50, ],//分页中下拉选项的数值 
                columns: [[
                    {
                        title: '标题', field: 'ID', width: 300, formatter: function (value, data, index) {
                            return "<a title='点击查看详情' style='text-decoration: none'  href='FileContent.aspx?id=" + value + "'>" + data.NAME + "</a>";
                        }
                    },
                    { title: '作者', field: 'USER_NAME', width: 80 },
                    { title: '单位', field: 'UNIT', width: 80 },
                    { title: '格式', field: 'TYPE', width: 50 },
                    { title: '浏览', field: 'BROWNUM', width: 50 },
                    {
                        title: '发布时间', field: 'PUBLISH_DATE', width: 136, align: 'center', formatter: function (value, data, index) {
                            return jsonDateFormat(value, "year");//, "year"
                        }
                    }
                ]]
            });
            //var data = $('#divMList').datagrid('getData');
            $('#divTree').tree({
                url: 'Handler/FileHandler.ashx?opt=treelazyList',//treeList
                border: false,
                toolbar: "#tb",
                idField: "id",
                loadMsg: '数据加载中请稍后……',
                treeField: 'text',
                rownumbers: true,
                autoRowHeight: true,
                //cascadeCheck: true,
                //loadFilter: myLoadFilter,
                columns: [[
                    { title: '模块名称', field: 'text', width: 180 }
                ]],
                onBeforeExpand: function (node, param) {
                    debugger;
                    $('#divTree').tree('options').url = "Handler/FileHandler.ashx?opt=treelazyListChild?pid=" + node.id;
                },//},
                onSelect: function (node) {
                    $("#nodeid").val(node.id);
                    $("#divMList").datagrid({
                        url: "Handler/FileHandler.ashx?opt=list&ftype=0&key=&stype=99&mid=" + node.id
                    });
                }
            });
            $("#btn_search").click(function () {
                var key = $("#search_text").val();
                var stype = '<%=schType %>'
                $("#divMList").datagrid({
                    url: "Handler/FileHandler.ashx?opt=list&stype=" + stype + "&ftype=0&key=" + key + "&mid=" + $("#nodeid").val()
                });
            });
            $("#a0").click(function () {
                $("#divMList").datagrid({
                    url: "Handler/FileHandler.ashx?opt=list&ftype=0&key=&stype=99&mid=" + $("#nodeid").val()
                });
            });
            $("#a1").click(function () {
                $("#divMList").datagrid({
                    url: "Handler/FileHandler.ashx?opt=list&ftype=1&key=&stype=99&mid=" + $("#nodeid").val()
                });
            });
            $("#a2").click(function () {
                $("#divMList").datagrid({
                    url: "Handler/FileHandler.ashx?opt=list&ftype=2&key=&stype=99&mid=" + $("#nodeid").val()
                });
            });
            $("#a3").click(function () {
                $("#divMList").datagrid({
                    url: "Handler/FileHandler.ashx?opt=list&ftype=3&key=&stype=99&mid=" + $("#nodeid").val()
                });
            });
            $("#a4").click(function () {
                $("#divMList").datagrid({
                    url: "Handler/FileHandler.ashx?opt=list&ftype=4&key=&stype=99&mid=" + $("#nodeid").val()
                });
            });
            $("#a5").click(function () {
                $("#divMList").datagrid({
                    url: "Handler/FileHandler.ashx?opt=list&ftype=5&key=&stype=99&mid=" + $("#nodeid").val()
                });
            });
            $("#a6").click(function () {
                $("#divMList").datagrid({
                    url: "Handler/FileHandler.ashx?opt=list&ftype=6&key=&stype=99&mid=" + $("#nodeid").val()
                });
            });
            $("#a7").click(function () {
                $("#divMList").datagrid({
                    url: "Handler/FileHandler.ashx?opt=list&ftype=7&key=&stype=99&mid=" + $("#nodeid").val()
                });
            });
            $("#a8").click(function () {
                $("#divMList").datagrid({
                    url: "Handler/FileHandler.ashx?opt=list&ftype=8&key=&stype=99&mid=" + $("#nodeid").val()
                });
            });
            $("#a9").click(function () {
                $("#divMList").datagrid({
                    url: "Handler/FileHandler.ashx?opt=list&ftype=9&key=&stype=99&mid=" + $("#nodeid").val()
                });
            });
            $("#a10").click(function () {
                $("#divMList").datagrid({
                    url: "Handler/FileHandler.ashx?opt=list&ftype=10&key=&stype=99&mid=" + $("#nodeid").val()
                });
            });
            $("#a11").click(function () {
                $("#divMList").datagrid({
                    url: "Handler/FileHandler.ashx?opt=list&ftype=11&key=&stype=99&mid=" + $("#nodeid").val()
                });
            });

        });

        function jsonDateFormat(jsonDate, year) {//json日期格式转换为正常格式
            try {
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
            } catch (ex) {
                return "";
            }
        }

        function setTat(name, cursel, n) {
            for (i = 1; i <= n; i++) {
                var menu = document.getElementById(name + i);
                if (menu != undefined) {
                    var con = document.getElementById("con_" + name + "_" + i);
                    menu.className = i == cursel ? "hover" : "";
                    con.style.display = i == cursel ? "block" : "none"
                }
            }
        }//视频的JS代
        function setTad(name, cursel, n) {
            for (i = 1; i <= n; i++) {
                var menu = document.getElementById(name + i);
                if (menu != undefined) {
                    var con = document.getElementById("con_" + name + "_" + i);
                    menu.className = i == cursel ? "hover" : "";
                    con.style.display = i == cursel ? "block" : "none"
                }
            }
        }//最新产品的JS代
        function setTab(name, cursel, n) {
            for (i = 1; i <= n; i++) {
                var menu = document.getElementById(name + i);
                if (menu != undefined) {
                    var con = document.getElementById("con_" + name + "_" + i);
                    menu.className = i == cursel ? "hover" : "";
                    con.style.display = i == cursel ? "block" : "none"
                }
            }
        }//更新、新闻动态的JS代

        function myLoadFilter(data, parentId) {
            function setData() {
                var todo = [];
                for (var i = 0; i < data.length; i++) {
                    todo.push(data[i]);
                }
                while (todo.length) {
                    var node = todo.shift();
                    if (node.children) {
                        node.state = 'closed';
                        node.children1 = node.children;
                        node.children = undefined;
                        todo = todo.concat(node.children1);
                    }
                }
            }

            setData(data);
            var tg = $(this);
            var opts = tg.treegrid('options');
            opts.onBeforeExpand = function (row) {
                if (row.children1) {
                    tg.treegrid('append', {
                        parent: row[opts.idField],
                        data: row.children1
                    });
                    row.children1 = undefined;
                    tg.treegrid('expand', row[opts.idField]);
                }
                return row.children1 == undefined;
            };
            return data;
        }
    </script>

</head>

<body>
    <form runat="server" target="_blank">
        <input id="nodeid" type="hidden" />
        <div id="top">
            <div id="top1">
                <div id="top2">
                    <div id="apDiv1">
                        <h4 style="line-height: 24px;">欢迎进入！&nbsp;&nbsp;&nbsp;</h4>
                        <h4>
                            <asp:LinkButton ID="lk_loginstate" runat="server" OnClick="lk_loginstate_Click" Style="text-decoration: none" Text="请登录"></asp:LinkButton>
                            <asp:LinkButton ID="lk_quitlogin" runat="server" OnClick="lk_quitlogin_Click" Style="text-decoration: none" Text="退出登录" Visible="false"></asp:LinkButton>
                            <asp:LinkButton ID="lk_modifypwd" runat="server" OnClick="lk_modifypwd_Click" Style="text-decoration: none" Text="修改密码" Visible="false"></asp:LinkButton>
                        </h4>
                    </div>
                </div>
            </div>
            <!---------------导航-------------------------->
            <div id="daohang">
                <div id="asd">
                    <ul>
                        <li><a href="FilePage.aspx">首页</a></li>
                        <li style="display: none"><a href="#">关于我们</a>
                            <ul id="wrap">
                                <li id="flip_menu1" style="background-image: none; background-image: none !important; _background-image: none;">
                                    <a href="#" style="border-bottom: 2px #FFF solid;">学校简介</a>
                                </li>
                            </ul>
                        </li>
                    </ul>
                    <%--<div id="asd1">
                        <table>
                            <tr>
                                <td style="width: 180px; _width: 148px;">
                                    <input type="text" style="width: 148px; _width: 148px; height: 18px" /></td>
                                <td>
                                    <input type="button" value="搜索" style="width: 45px; height: 25px; cursor: pointer; _width: 32px;" /></td>
                            </tr>
                        </table>
                    </div>--%>
                </div>
            </div>
            <!---------------焦点图片-------------------------->
            <%-- <div id="xxx">
            <script>
                var box = new PPTBox();
                box.width = 1000; //宽度
                box.height = 237;//高度
                box.autoplayer = 3;//自动播放间隔时间

                //box.add({"url":"图片地址","title":"悬浮标题","href":"链接地址"})
                box.add({ "url": "../images/index-3_1.png", "href": "http://www.lanrentuku.com/", "title": "悬浮提示标题1" })
                box.add({ "url": "../images/index-3_2.png", "href": "http://www.lanrentuku.com/", "title": "悬浮提示标题2" })
                box.add({ "url": "../images/index-3_1.png", "href": "http://www.lanrentuku.com/", "title": "悬浮提示标题3" })
                box.add({ "url": "../images/index-3_2.png", "href": "http://www.lanrentuku.com/", "title": "悬浮提示标题4" })
                box.show();
            </script>
        </div>--%>
            <!---------------主体-------------------------->
            <div style="width: 1008px; height: auto;">
                <div style="width: 1008px; height: 0px; margin-bottom: 10px;"></div>
                <%--可以放图片--%>
                <div style="width: inherit; height: 30px; line-height: 30px; text-align: left; font-size: 14px; font-family: '微软雅黑', '黑体'; color: #333;">首页&nbsp;>&nbsp;课程资源</div>
                <div style="width: inherit; height: auto;">
                    <div style="width: 275px; height: 562px; min-height: 520px; border: #06F 1px solid; float: left;">
                        <div style="width: inherit; height: 32px; background-image: url(../images/index-04.jpg); background-repeat: no-repeat;">
                            <div style="width: auto; height: 34px; float: left; margin-left: 70px; line-height: 34px; font-family: '微软雅黑', '黑体'; font-size: 16px; color: #000; text-align: center;">资源目录</div>
                            <div style="width: 88px; height: 30px; background-color: #0CC; float: right; margin-right: 3px; line-height: 28px; font-family: '微软雅黑', '黑体'; font-size: 14px; text-align: center;">
                                <asp:LinkButton runat="server" OnClientClick="document.getElementById('form1').target='_blank'" ID="lk_upload" Text="登录上传" Style="text-decoration: none" OnClick="lk_upload_Click"></asp:LinkButton>
                            </div>
                        </div>
                        <div style="width: inherit; height: 520px; margin-top: 5px; overflow: auto; color: #1B1B1B;">
                            <div id="divTree" style="height: auto"></div>
                        </div>
                    </div>
                    <div style="width: 722px; height: auto; min-height: 320px; border: #06F 1px solid; float: right;">
                        <div style="width: inherit; height: 32px; background-image: url(../images/index-05.jpg); background-repeat: no-repeat;">
                            <div style="width: auto; height: 32px; float: left; margin-left: 70px; line-height: 32px; font-family: '微软雅黑', '黑体'; font-size: 16px; color: #000; text-align: center;">课程资源</div>
                            <div style="width: auto; height: 32px; float: left; margin-left: 30px; line-height: 32px; font-family: '微软雅黑', '黑体'; font-size: 12px; color: #000; text-align: center;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%--共 <span id="count"></span>条资源--%></div>
                            <div style="width: 55px; height: 20px; float: left; margin-left: 30px; margin-top: 8px; line-height: 20px; font-family: '微软雅黑', '黑体'; font-size: 12px; color: #000; text-align: center; background-color: #FFF;">
                                <%--<select name="select" id="select">
                            </select>--%>
                                <asp:DropDownList OnSelectedIndexChanged="dr_searchtype_SelectedIndexChanged" runat="server" ID="dr_searchtype">
                                </asp:DropDownList>
                            </div>
                            <%--<div style="width: 160px; height: 22px; float: left; margin-left: 5px; margin-top: 6px; line-height: 22px; font-family: '微软雅黑', '黑体'; font-size: 12px; color: #000; text-align: center; background-color: #FFF;"></div>--%>
                            <input type="text" id="search_text" style="width: 250px; height: 22px; float: left; margin-left: 5px; margin-top: 6px; line-height: 22px; font-family: '微软雅黑', '黑体'; font-size: 12px; color: #000; text-align: center; background-color: #FFF;" />
                            <%--<div style="width: 40px; height: 22px; float: left; margin-left: 5px; margin-top: 6px; line-height: 22px; font-family: '微软雅黑', '黑体'; font-size: 12px; color: #000; text-align: center; background-color: #FFF;"></div>--%>
                            <input type="button" id="btn_search" title="搜索" value="搜索" style="width: 45px; height: 25px; float: left; margin-left: 5px; margin-top: 6px; line-height: 22px; font-family: '微软雅黑', '黑体'; font-size: 12px; text-align: center;" />
                            <div style="width: auto; height: 22px; float: left; margin-left: 12px; margin-top: 6px; line-height: 22px; font-family: '微软雅黑', '黑体'; font-size: 12px; color: #000; text-align: center;">&nbsp;&nbsp;&nbsp;&nbsp;<%--排序：--%></div>
                            <%--<div style="width: 90px; height: 20px; float: left; margin-top: 8px; line-height: 20px; font-family: '微软雅黑', '黑体'; font-size: 12px; color: #000; text-align: center; background-color: #FFF;"></div>--%>
                            <asp:DropDownList Visible="false" runat="server" ID="dr_searchsort" CssClass="drop">
                            </asp:DropDownList>
                            <div style="width: 24px; height: 22px; float: left; margin-left: 12px; margin-top: 6px; line-height: 22px; font-family: '微软雅黑', '黑体'; font-size: 12px; color: #000; text-align: center; background-color: #FFF;"></div>
                            <%-- <div style="width: 24px; height: 22px; float: left; margin-left: 5px; margin-top: 6px; line-height: 22px; font-family: '微软雅黑', '黑体'; font-size: 12px; color: #000; text-align: center; background-color: #FFF;"></div>--%>
                        </div>
                        <div style="width: inherit; height: auto; min-height: 45px; background-color: #E6E6E6;">
                            <div style="width: 705px; height: 35px; line-height: 35px; margin-left: 10px; font-size: 14px; font-family: '微软雅黑', '黑体'; text-align: left;">
                                类型导航：&nbsp;&nbsp;
                               <a href="javascript://" style="text-decoration: none" id="a0">全部&nbsp;&nbsp;</a>
                                <a href="javascript://" style="text-decoration: none" id="a1">教案&nbsp;&nbsp;</a>
                                <a href="javascript://" style="text-decoration: none" id="a2">课件&nbsp;&nbsp;</a>
                                <a href="javascript://" style="text-decoration: none" id="a3">课例&nbsp;&nbsp;</a>
                                <a href="javascript://" style="text-decoration: none" id="a4">微课&nbsp;&nbsp;</a>
                                <a href="javascript://" style="text-decoration: none" id="a5">试题&nbsp;&nbsp;</a>
                                <a href="javascript://" style="text-decoration: none" id="a6">学案&nbsp;&nbsp;</a>
                                <a href="javascript://" style="text-decoration: none" id="a7">素材&nbsp;&nbsp;</a>
                                <a href="javascript://" style="text-decoration: none" id="a8">文献&nbsp;&nbsp;</a>
                                <a href="javascript://" style="text-decoration: none" id="a9">工具&nbsp;&nbsp;</a>
                                <a href="javascript://" style="text-decoration: none" id="a10">仿真&nbsp;&nbsp;</a>
                                <a href="javascript://" style="text-decoration: none" id="a11">其它</a>
                            </div>
                            <div style="width: 720px; height: 10px; border-top: #CCC 1px dashed; margin-left: 1px;"></div>
                        </div>
                        <div style="width: 720px; height: auto; min-height: 300px; font-family: '微软雅黑', '黑体';">
                            <table width="720" border="0">
                                <tr style="background-color: #CCC; height: 30px; line-height: 30px; font-size: 14px; font-weight: bold;">
                                    <th scope="col" width="323">标题</th>
                                    <th scope="col" widtj="83">作者</th>
                                    <th scope="col" width="80">单位</th>
                                    <th scope="col" width="50">格式</th>
                                    <th scope="col" width="50">浏览</th>
                                    <th scope="col" style="font-size: 12px;">发布时间</th>
                                </tr>
                                <%--<asp:Repeater runat="server" ID="rp_filelist">
                                    <ItemTemplate>
                                        <tr style="font-size: 12px; text-align: left; line-height: 26px; text-align: center; color: #1B1B1B;">
                                            <td style="text-align: left; text-indent: 3px; border-bottom: #CCC 1px dashed;">
                                                <a style="text-decoration: none" href="FileContent.aspx?id=<%# Eval("ID") %>">
                                                    <%# Eval("NAME") %></a>
                                            </td>
                                            <td style="border-bottom: #CCC 1px dashed;"><%# Eval("USER_NAME") %></td>
                                            <td style="border-bottom: #CCC 1px dashed;"><%# Eval("UNIT") %></td>
                                            <td style="border-bottom: #CCC 1px dashed;"><%# Eval("BROWNUM") %></td>
                                            <td style="border-bottom: #CCC 1px dashed;"><%# Convert.ToDateTime( Eval("PUBLISH_DATE")).ToShortDateString() %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>--%>
                            </table>
                            <div id="divMList"></div>
                        </div>
                        <div id="divMapList" style="display: none; width: 720px; height: auto; min-height: 300px; margin-left: 1px; font-family: '微软雅黑', '黑体';">
                            <div style="width: 155px; height: 180px; float: left; margin-left: 11px; margin-right: 14px; margin-top: 5px; margin-bottom: 5px;">
                                <div style="width: inherit; height: 110px; background-color: #096;"></div>
                                <div style="width: inherit; height: 70px; font-size: 14px; line-height: 22px; text-align: center;">
                                    <table width="155" border="0">
                                        <tr>
                                            <td><a href="FileContent.aspx" style="color: #06C; text-decoration: none;">标题机刷卡缴费机</a></td>
                                        </tr>
                                        <tr>
                                            <td><a href="#" style="color: #000; text-decoration: none;">某某中学</a></td>
                                        </tr>
                                        <tr>
                                            <td><a href="#" style="color: #000; text-decoration: none;">上传名</a></td>
                                        </tr>
                                    </table>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <!---------------------页脚部分-------------------->
            <div class="foot">
                <table width="1008" style="text-align: center;">
                    <tr>
                        <td>
                            <div class="yiJian"><a href="#">意见征询</a> | <a href="#">联系我们</a> | <a href="#">帮助</a></div>
                        </td>
                    </tr>
                    <tr class="fenGX">
                        <td></td>
                    </tr>
                    <tr height="12">
                        <td></td>
                    </tr>
                    <tr>
                        <td>联系电话: 028 - 85961001
                            <br />
                            网站备案：蜀ICP备12031164号 成都市机场路土桥段76号<br />
                            版权所有   2014 成都市盐道街外语学校<br />
                        </td>
                    </tr>
                </table>
            </div>
    </form>
</body>
</html>
