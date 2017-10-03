<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BaiduMap.aspx.cs" Inherits="WebApplication1.BaiduMap" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Simple Map</title>
    <style type="text/css">
        body, html
        {
            width: 100%;
            height: 100%;
            margin: 0;
            font-family: "微软雅黑";
        } 

        p
        {
            margin-left: 5px;
            font-size: 14px;
        }
    </style>
    <script type="text/javascript" src="http://api.map.baidu.com/api?ak=Yrg8XGg5iK2SR0NEGk6WwVGxKu4Gdn34&v=2.0&services=false"></script><%--ak=Yrg8XGg5iK2SR0NEGk6WwVGxKu4Gdn34 	--%>
</head>
<body style="background: #CBE1FF">
    <div id="content" class="content">
        <input type="text" value="" id="keyword" />
        <input type="button" name="" id="" value="查询" onclick="search()" />
        <div style="width: 100%; height: 600px; border: 0px solid gray" id="container"></div>
        <p id="aa"></p>
        <script type="text/javascript">
            //创建Map实例
            var map = new BMap.Map("container");
            var point = new BMap.Point(118.060576, 36.842432);
            map.centerAndZoom(point, 15);
            //添加鼠标滚动缩放
            map.enableScrollWheelZoom();

            //添加缩略图控件
            map.addControl(new BMap.OverviewMapControl({ isOpen: false, anchor: BMAP_ANCHOR_BOTTOM_RIGHT }));
            //添加缩放平移控件
            map.addControl(new BMap.NavigationControl());
            //添加比例尺控件
            map.addControl(new BMap.ScaleControl());
            //添加地图类型控件
            //map.addControl(new BMap.MapTypeControl());

            //设置标注的图标
            var icon = new BMap.Icon("img/icon.jpg", new BMap.Size(100, 100));
            //设置标注的经纬度
            var marker = new BMap.Marker(new BMap.Point(118.056156, 36.840988), { icon: icon });
            //把标注添加到地图上
            map.addOverlay(marker);
            var content = "<table>";
            content = content + "<tr><td> 编号：</td></tr>";
            content = content + "<tr><td> 地点：</td></tr>";
            content = content + "<tr><td> 时间：</td></tr>";
            content += "</table>";
            var infowindow = new BMap.InfoWindow(content);
            marker.addEventListener("click", function () {
                this.openInfoWindow(infowindow);
            });

            //点击地图，获取经纬度坐标
            map.addEventListener("click", function (e) {
                document.getElementById("aa").innerHTML = "经度坐标：" + e.point.lng + " &nbsp;纬度坐标：" + e.point.lat;
            });

            //关键字搜索
            function search() {
                var keyword = document.getElementById("keyword").value;
                var local = new BMap.LocalSearch(map, {
                    renderOptions: { map: map }
                });
                local.search(keyword);
            }
        </script>

    </div>
</body>
</html>
