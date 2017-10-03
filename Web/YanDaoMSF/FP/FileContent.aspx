<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileContent.aspx.cs" Inherits="YanDaoMSF.FP.FileContent" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>成都市盐道街外语学校</title>
    <link href="../Video/css/video-js.min.css" rel="stylesheet" />
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
    <!----下拉菜单---->
    <script type="text/javascript" src="../FP/js/flipmenu-min.js"></script>
    <!----下拉Js---->
    <script type="text/javascript" src="../FP/js/jquery-1.3.2.min.js"></script>
    <script src="../Video/js/video.min.js"></script>
    <!----下拉Js---->
    <script type="text/javascript">
        videojs.options.flash.swf = "../../video/js/video-js.swf";
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
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            var menu1 = new Flipmenu("flip_menu1");
            var menu2 = new Flipmenu("flip_menu2");
        });

    </script>
    <!-----下拉菜单Js------>
    <script type="text/javascript">
        $(document).ready(function () {
            var menu1 = new Flipmenu("flip_menu3");
            var menu2 = new Flipmenu("flip_menu4");
        });
    </script>
    <!-----下拉菜单Js------>
    <script type="text/javascript">
        $(document).ready(function () {
            var menu1 = new Flipmenu("flip_menu5");
            var menu2 = new Flipmenu("flip_menu6");
        });
    </script>
    <!-----下拉菜单Js------>
    <script type="text/javascript">
        $(document).ready(function () {
            var menu1 = new Flipmenu("flip_menu7");
            var menu2 = new Flipmenu("flip_menu8");
        });
    </script>
    <!-----下拉菜单Js------>
    <script type="text/javascript">
        $(document).ready(function () {
            var menu1 = new Flipmenu("flip_menu9");
            var menu2 = new Flipmenu("flip_menu10");
        });
    </script>
    <!-----下拉菜单Js------>
    <script type="text/javascript">
        $(document).ready(function () {
            var menu1 = new Flipmenu("flip_menu11");
            var menu2 = new Flipmenu("flip_menu12");
        });
    </script>
    <!-----下拉菜单Js------>
    <script type="text/javascript">
        $(document).ready(function () {
            var menu1 = new Flipmenu("flip_menu13");
            var menu2 = new Flipmenu("flip_menu14");
        });
    </script>
    <!-----下拉菜单Js------>
    <script type="text/javascript">
        $(document).ready(function () {
            var menu1 = new Flipmenu("flip_menu15");
            var menu2 = new Flipmenu("flip_menu16");
        });
    </script>
    <!-----下拉菜单Js------>
</head>

<body>
    <form runat="server" id="form1">
        <div id="top">
            <div id="top1">
                <div id="top2">
                    <div id="apDiv1">
                        <h4 style="line-height: 24px;">欢迎进入！&nbsp;&nbsp;&nbsp;</h4>
                        <h4>
                            <asp:LinkButton ID="lk_loginstate" runat="server" OnClick="lk_loginstate_Click" Style="text-decoration: none" Text="请登录"></asp:LinkButton>
                             <asp:LinkButton ID="lk_quitlogin" runat="server" OnClick="lk_quitlogin_Click" Style="text-decoration:none" Text="退出登录" Visible="false"></asp:LinkButton>
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
                        <li style="display:none"><a href="#">关于我们</a>
                            <ul id="wrap">
                                <li id="flip_menu1" style="background-image: none; background-image: none !important; _background-image: none;">
                                    <a href="#" style="border-bottom: 2px #FFF solid;">学校简介</a>
                                </li>
                            </ul>
                        </li>

                    </ul>
                </div>
            </div>
            <!---------------焦点图片-------------------------->
            <!--<div id="xxx">
     <script>
     var box =new PPTBox();
     box.width = 1000; //宽度
     box.height = 237;//高度
     box.autoplayer = 3;//自动播放间隔时间

     //box.add({"url":"图片地址","title":"悬浮标题","href":"链接地址"})
     box.add({"url":"../images/index-3_1.png","href":"http://www.lanrentuku.com/","title":"悬浮提示标题1"})
     box.add({"url":"../images/index-3_2.png","href":"http://www.lanrentuku.com/","title":"悬浮提示标题2"})
     box.add({"url":"../images/index-3_1.png","href":"http://www.lanrentuku.com/","title":"悬浮提示标题3"})
     box.add({"url":"../images/index-3_2.png","href":"http://www.lanrentuku.com/","title":"悬浮提示标题4"})
     box.show();
    </script>
</div>-->
            <!---------------主体-------------------------->
            <div style="width: 1008px; height: auto;">
                <!--<div style="width:1008px; height:200px; margin-bottom:10px;"></div>-->
                <!--<div style="width:inherit; height:30px; line-height:30px; text-align:left; font-size:14px; font-family:'微软雅黑', '黑体'; color:#333;;">首页&nbsp;>&nbsp;课程资源&nbsp;>&nbsp;如何做...</div>-->
                <div style="width: inherit; height: auto; margin-top: 10px;">
                    
                    <div style="width: inherit; height: auto; min-height: 37px; background-color: #E1FAFD;">
                        <div style="width: 700px; height: 35px; margin-left: 10px; font-family: '微软雅黑', '黑体'; text-align: left; margin-top: 2px;">
                            <div style="font-weight: bold; font-size: 16px; line-height: 35px; float: left; width: 460px;"><%=Name %></div>
                            <div style="font-size: 12px; line-height: 20px; float: right; width: 240px; color: #333; margin-top: 10px;">
                                <div style="width: 60px; float: left;">浏览:<%=BrowNum %></div>
                                <div style="width: 70px; float: left;">下载:<%=DownloadNum %>次</div>
                                <div style="width: 110px; float: left;">发布于<%=PublishDate %></div>
                            </div>
                        </div>
                        <div style="width: 720px; height: 2px; border-top: #CCC 1px dashed; margin-left: 1px;"></div>
                    </div>
                    <div style="width: 720px; height: auto; margin-left: 1px; font-family: '微软雅黑', '黑体';">
                        <div style="width: 700px; height: 490px; float: left; margin-left: 10px; margin-right: 14px; margin-top: 5px;">
                            <div style="width: inherit; height: 480px; background-color: #FFF;" id="showContennt">
                                <!--视频-->
                                <%=loadmedia %>
                                <%--<video id="example_video_1" class="video-js vjs-default-skin" controls preload="none" width="710" height="420"
                                poster="http://video-js.zencoder.com/oceans-clip.png"
                                data-setup="{}">
                               <source src="../<%=FilePath %>" type="video/mp4"/>
                                <track kind="captions" src="demo.captions.vtt" srclang="en" label="English"/>
                                <track kind="subtitles" src="demo.captions.vtt" srclang="en" label="English"/>
                            </video>--%>
                                <!--ppt/word-->
                                <object id="rppt" class="FlashReaderObject" classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0" height="500" width="100%">
                                    <param name="Movie" value="<%=FilePath %>">
                                    <param name="wmode" value="opaque">
                                    <param name="allowFullScreen" value="true">
                                    <param name="quality" value="high">
                                    <param name="SCALE" value="exactfit">
                                    <embed src="../<%=FilePath %>" id="rppt_embed" allowfullscreen="true" quality="high" pluginspage="http://www.macromedia.com/go/getflashplayer" wmode="opaque" type="application/x-shockwave-flash" scale="exactfit" height="5" width="100%">
                                </object><%----%>
                            </div>

                        </div>
                    </div>
                </div>
                <div style="width: 275px; height: 232px; border: #06F 1px solid; float: right;">
                    <div style="width: inherit; height: 32px; background-image: url(../images/index-04.jpg); background-repeat: no-repeat;">
                        <div style="width: auto; height: 32px; float: left; margin-left: 70px; line-height: 34px; font-family: '微软雅黑', '黑体'; font-size: 16px; color: #000; text-align: center;">资源属性</div>
                        <!--<div style="width:88px; height:30px; background-color:#0CC; float:right; margin-right:3px; line-height:28px; font-family:'微软雅黑', '黑体'; font-size:14px; text-align:center;"><a href="#" style=" color:#FFF; text-decoration:none;">登录上传</a></div>-->
                    </div>
                    <div style="width: inherit; height: 200px; margin-top: 5px; color: #1B1B1B; font-size: 12px; line-height: 28px; color: #333;">
                        <table width="275" border="0" style="margin-top: 5px; text-indent: 5px;" id="ziyuan1">
                     
                            <%=fileinfo %>
                        </table>

                    </div>
                </div>
                <div style="width: 275px; height: 232px; border: #06F 1px solid; float: right; margin-top: 10px;">
                    <div style="width: inherit; height: 32px; background-image: url(../images/index-04.jpg); background-repeat: no-repeat;">
                        <div style="width: auto; height: 32px; float: left; margin-left: 70px; line-height: 34px; font-family: '微软雅黑', '黑体'; font-size: 16px; color: #000; text-align: center;">资源下载</div>
                        <div style="width: 88px; height: 30px; background-color: #0CC; float: right; margin-right: 3px; line-height: 28px; font-family: '微软雅黑', '黑体'; font-size: 14px; text-align: center;">
                            <asp:LinkButton runat="server" ID="lk_upload" Text="登录上传" Style="text-decoration: none" OnClientClick="document.getElementById('form1').target='_blank'" OnClick="lk_upload_Click"></asp:LinkButton>
                        </div>
                    </div>
                    <div style="width: inherit; height: 200px; margin-top: 5px; color: #1B1B1B; font-size: 13px; line-height: 30px; color: #333;">
                        <table width="275" border="0" style="margin-top: 5px; text-indent: 5px;" id="ziyuan2">
                            <tr>
                                <td colspan="2">
                                    <img src="../js/Jquery-easyui-1.4/themes/default/images/combo_arrow.png" />
                                    <%--<a href="<%=(FilePath) %>"><%=Name %></a>--%>
                                    <asp:LinkButton runat="server" ID="btn_download" OnClick="btn_download_Click"><%=Name %></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
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
                    <td>联系电话: 028- 85961001 <br />
                        网站备案：蜀ICP备12031164号 成都市机场路土桥段76号<br />
                        版权所有   2014 成都市盐道街外语学校<br />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>

</html>
<script type="text/javascript">
    var myPlayer = videojs('example_video_1');
    videojs("example_video_1").ready(function () {
        var myPlayer = this;
        myPlayer.play();
    });
</script>
