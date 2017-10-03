<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Lucence.Net._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="Scripts/jquery-1.7.1.js" type="text/javascript"></script>
    <style type="text/css">
        .page
        {
            padding: 10px 45px 30px;
        }

            .page::after
            {
                clear: both;
                content: "";
                display: block;
                height: 0;
                visibility: hidden;
            }

            .page span
            {
                border: 1px solid #ccc;
                float: left;
                margin: 0 2px;
                position: relative;
            }

                .page span a
                {
                    cursor: pointer;
                    padding: 2px 5px;
                    color: blue;
                }
    </style>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server" method="get" action="Default.aspx">

        <div>
            <div>
                <a href="Default.aspx" style="text-decoration: none">
                    <img title="首页" alt="首页" src="//www.baidu.com/img/baidu_jgylogo3.gif" />
                </a>
                <input type="text" name="SearchKey" value="" style="width: 540px; height: 28px; font-size: 18px; font-family: Gill Sans, Gill Sans MT,Myriad Pro,DejaVu Sans Condensed,Helvetica,Arial,sans-serif;" />
                <input type="submit" name="btnSearch" value="搜索" style="left: 547px; width: 100px; height: 36px; background: #3385ff; color: #FFFFFF; font-size: 14px; border: 1px solid #4791ff;" />
                <input type="submit" name="btnSelf" value="试试手气" />
                <input type="submit" name="btnCreate" value="创建索引" />
            </div>
            <div>
                <ul style="width: 60%;">
                    <asp:Repeater ID="rp1" runat="server">
                        <HeaderTemplate>
                            <span style="color: #999; font-size: 12px; height: 42px; line-height: 42px;">&nbsp;为您找到相关结果约<%=modResult.Count %>个</span>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li style="list-style-type: none"><span style="color: blue; line-height: 1.54; font-size: medium; font-weight: normal; margin-bottom: 1px; list-style: outside none none; padding: 0;">
                                <a href='<%#Eval("pandaWebUrl") %>' target="_blank"><%# Eval("TITLE") %></a></span></li>
                            <li style="list-style-type: none"><span style="font-size: 13px; list-style: outside none none; border-collapse: collapse; line-height: 1.54; word-break: break-all; font-family: arial; color: #333; word-wrap: break-word;">&nbsp;&nbsp;&nbsp;&nbsp;
                                <%# Eval("CONTENT").ToString().Length>500?Eval("CONTENT").ToString().Substring(0,500)+"...":Eval("CONTENT") %></span></li>
                            <li style="list-style-type: none"><span style="color: pink; font-size: 11px">[<%#Eval("ID") %>]</span><span style="color: green; font-size: 10px;">
                                <a href='<%#Eval("pandaWebUrl") %>' target="_blank" style="text-decoration: none; color: green;"><%# Eval("pandaWebUrl") %></a></span></li>
                            <br />
                        </ItemTemplate>
                        <FooterTemplate>
                            <%=rp1.Items.Count==0?"<h1 style='color:red'>未查询到任何数据</h1>":"" %>
                        </FooterTemplate>
                    </asp:Repeater>
                </ul>
            </div>
            <div class="page">
                <span><a>上一页</a></span>
                <span><a>1</a></span>
                <span><a>2</a></span>
                <span><a>3</a></span>
                <span><a>4</a></span>
                <span><a>5</a></span>
                <span><a>5</a></span>
                <span><a>6</a></span>
                <span><a>7</a></span>
                <span><a>8</a></span>
                <span><a>9</a></span>
                <span><a>下一页</a></span>
            </div>
            <div style="padding: 5px 50px;">
                <input type="text" name="SearchKey" value="" style="width: 540px; height: 28px; font-size: 18px; font-family: Gill Sans, Gill Sans MT,Myriad Pro,DejaVu Sans Condensed,Helvetica,Arial,sans-serif;" />
                <input type="submit" name="btnSearch" value="搜索" style="left: 547px; width: 100px; height: 36px; background: #3385ff; color: #FFFFFF; font-size: 14px; border: 1px solid #4791ff;" />
            </div>
        </div>

    </form>
</body>
</html>
