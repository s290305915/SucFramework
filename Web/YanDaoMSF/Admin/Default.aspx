<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Master/HeadPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="YanDaoMSF.Admin.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <!--内容-->
        <div class="line10"></div>
        <div class="nlist-1">
            <ul>
                <li>本次登录IP：<asp:Literal ID="litIP" runat="server" Text="-" /></li>
                <li>上次登录IP：<asp:Literal ID="litBackIP" runat="server" Text="-" /></li>
                <li>上次登录时间：<asp:Literal ID="litBackTime" runat="server" Text="-" /></li>
            </ul>
        </div>
        <div class="line10"></div>

        <div class="nlist-2">
            <h3><i></i>站点信息</h3>
            <ul>
                <li>站点名称：<%--=siteConfig.webname --%></li>
                <li>公司名称：<%--=siteConfig.webcompany --%></li>
                <li>网站域名：<%--=siteConfig.weburl --%></li>
                <li>安装目录：<%--=siteConfig.webpath --%></li>
                <li>网站管理目录：<%--=siteConfig.webmanagepath --%></li>
                <li>附件上传目录：<%--=siteConfig.filepath --%></li>
                <li>服务器名称：<%=Server.MachineName%></li>
                <li>服务器IP：<%=Request.ServerVariables["LOCAL_ADDR"] %></li>
                <li>NET框架版本：<%=Environment.Version.ToString()%></li>
                <li>操作系统：<%=Environment.OSVersion.ToString()%></li>
                <li>IIS环境：<%=Request.ServerVariables["SERVER_SOFTWARE"]%></li>
                <li>服务器端口：<%=Request.ServerVariables["SERVER_PORT"]%></li>
                <li>目录物理路径：<%=Request.ServerVariables["APPL_PHYSICAL_PATH"]%></li>
                <li>系统版本：V 1.0.0<%--=Utils.GetVersion()--%></li>
                <li>升级通知：<asp:Literal ID="LitUpgrade" runat="server" /></li>
            </ul>
        </div>
        <div class="line20"></div>

        <div class="nlist-3">
            <ul>
                <li><a onclick="" class="icon-setting" href="javascript:;"></a><span>系统设置</span></li>
                <li><a onclick="" class="icon-channel" href="javascript:;"></a><span>频道分类</span></li>
                <li><a onclick="" class="icon-templet" href="javascript:;"></a><span>模板管理</span></li>
                <li><a onclick="" class="icon-mark" href="javascript:;"></a><span>生成静态</span></li>
                <li><a onclick="" class="icon-plugin" href="javascript:;"></a><span>插件配置</span></li>
                <li><a onclick="" class="icon-user" href="javascript:;"></a><span>会员管理</span></li>
                <li><a onclick="" class="icon-manaer" href="javascript:;"></a><span>管理员</span></li>
                <li><a onclick="" class="icon-log" href="javascript:;"></a><span>系统日志</span></li>
            </ul>
        </div>

        <div class="nlist-4">
            <h3><i class="site"></i>使用帮助</h3>
            <ul>
                <li>1、进入后台管理中心，点击“系统设置”修改网站配置信息；</li>
                <li>2、点击“频道分类”进行系统频道分类、建立频道、扩展字段等信息；</li>
                <li>3、制作好网站模板，上传到站点templates目录下，点击“模板管理”生成模板；</li>
            </ul>
            <h3><i class="msg"></i>官方消息</h3>
            <ul>
                <asp:Literal ID="LitNotice" runat="server" />
            </ul>
        </div>

        <!--/内容-->
    </form>
</asp:Content>
