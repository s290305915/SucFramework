<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SliderLayout.aspx.cs" Inherits="WebApplication1.SliderLayout" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="Scripts/Jquery-easyui-1.4/themes/default/easyui.css" rel="stylesheet" />
    <link href="Scripts/Jquery-easyui-1.4/themes/color.css" rel="stylesheet" />
    <link href="Scripts/Jquery-easyui-1.4/themes/icon.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.8.3.min.js"></script>
    <script src="Scripts/Jquery-easyui-1.4/jquery.easyui.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <h2>基本布局</h2>
        <p>上下左右可拖动的布局.</p>
        <div class="easyui-layout" style="width: 700px; height: 350px;">
            <div data-options="region:'north',split:true" style="height: 50px">
                上边，可以不要
            </div>
            <div data-options="region:'south',split:true" style="height: 50px;">
                下面 也可以不要
            </div>
            <div data-options="region:'east',split:true" title="右" style="width: 100px;">
                左边，内容随便写
            </div>
            <div data-options="region:'west',split:true" title="左" style="width: 100px;">
                右边 可要可不要
            </div>
            <div data-options="region:'center',iconCls:'icon-ok'" title="中">
                中间
                <embed type="application/x-shockwave-flash" src="http://uiste.com/content/templates/default/images/xfish.swf" wmode="opaque" bgcolor="F6F6F6" flashvars="up_numFish=6&amp;up_fishColor4=#FFFFFF&amp;up_backgroundColor=F6F6F6&amp;up_fishColor1=F4A61C&amp;up_fishColor7=F45540&amp;up_fishColor6=F45540&amp;up_fishColor8=F45540&amp;up_fishColor2=C4C4C4&amp;up_fishColor9=F45540&amp;up_fishColor3=#600000&amp;up_fishName=Fish&amp;up_fishColor5=F45540&amp;up_fishColor10=F45540&amp;up_backgroundImage=http://&amp;up_foodColor=FCB347&amp;" width="324" height="250" />
            </div>
        </div>
    </form>
</body>
</html>
