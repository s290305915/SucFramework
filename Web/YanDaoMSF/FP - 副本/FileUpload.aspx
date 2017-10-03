<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileUpload.aspx.cs" Inherits="YanDaoMSF.FP.FileUpload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <style>
        .fu_list
        {
            width: 600px;
            height: 230px;
            background: #ebebeb;
            font-size: 12px;
        }

            .fu_list td
            {
                padding: 5px;
                line-height: 20px;
                background-color: #fff;
            }

            .fu_list table
            {
                width: 100%;
                border: 1px solid #ebebeb;
            }

            .fu_list thead td
            {
                background-color: #f4f4f4;
            }

            .fu_list b
            {
                font-size: 14px;
            }
        /*file容器样式*/
        a.files
        {
            width: 90px;
            height: 30px;
            overflow: hidden;
            display: block;
            border: 1px solid #BEBEBE;
            background: url(Images//fu_btn.gif) left top no-repeat;
            text-decoration: none;
        }

            a.files:hover
            {
                background-color: #FFFFEE;
                background-position: 0 -30px;
            }
            /*file设为透明，并覆盖整个触发面*/
            a.files input
            {
                margin-left: -350px;
                font-size: 30px;
                cursor: pointer;
                filter: alpha(opacity=0);
                opacity: 0;
            }
            /*取消点击时的虚线框*/
            a.files, a.files input
            {
                outline: none; /*ff*/
                hide-focus: expression(this.hideFocus=true); /*ie*/
            }

        .cent
        {
            width: 600px;
            margin-left: auto;
            margin-right: auto;
            height: 300px;
            margin-top: 100px;
        }
    </style>
    <%--<script type="text/javascript">

        var isIE = (document.all) ? true : false;

        var $ = function (id) {
            return "string" == typeof id ? document.getElementById(id) : id;
        };

        var Class = {
            create: function () {
                return function () {
                    this.initialize.apply(this, arguments);
                }
            }
        }

        var Extend = function (destination, source) {
            for (var property in source) {
                destination[property] = source[property];
            }
        }

        var Bind = function (object, fun) {
            return function () {
                return fun.apply(object, arguments);
            }
        }

        var Each = function (list, fun) {
            for (var i = 0, len = list.length; i < len; i++) { fun(list[i], i); }
        };

        //文件上传类
        var FileUpload = Class.create();
        FileUpload.prototype = {
            //表单对象，文件控件存放空间
            initialize: function (form, folder, options) {

                this.Form = $(form);//表单
                this.Folder = $(folder);//文件控件存放空间
                this.Files = [];//文件集合

                this.SetOptions(options);

                this.FileName = this.options.FileName;
                this._FrameName = this.options.FrameName;
                this.Limit = this.options.Limit;
                this.Distinct = !!this.options.Distinct;
                this.ExtIn = this.options.ExtIn;
                this.ExtOut = this.options.ExtOut;

                this.onIniFile = this.options.onIniFile;
                this.onEmpty = this.options.onEmpty;
                this.onNotExtIn = this.options.onNotExtIn;
                this.onExtOut = this.options.onExtOut;
                this.onLimite = this.options.onLimite;
                this.onSame = this.options.onSame;
                this.onFail = this.options.onFail;
                this.onIni = this.options.onIni;

                if (!this._FrameName) {
                    //为每个实例创建不同的iframe
                    this._FrameName = "uploadFrame_" + Math.floor(Math.random() * 1000);
                    //ie不能修改iframe的name
                    var oFrame = isIE ? document.createElement("<iframe name=\"" + this._FrameName + "\">") : document.createElement("iframe");
                    //为ff设置name
                    oFrame.name = this._FrameName;
                    oFrame.style.display = "none";
                    //在ie文档未加载完用appendChild会报错
                    document.body.insertBefore(oFrame, document.body.childNodes[0]);
                }

                //设置form属性，关键是target要指向iframe
                this.Form.target = this._FrameName;
                this.Form.method = "post";
                //注意ie的form没有enctype属性，要用encoding
                this.Form.encoding = "multipart/form-data";

                //整理一次
                this.Ini();
            },
            //设置默认属性
            SetOptions: function (options) {
                this.options = {//默认值
                    FileName: "file_upload",//文件上传控件的name，配合后台使用
                    FrameName: "",//iframe的name，要自定义iframe的话这里设置name
                    onIniFile: function () { },//整理文件时执行(其中参数是file对象)
                    onEmpty: function () { },//文件空值时执行
                    Limit: 0,//文件数限制，0为不限制
                    onLimite: function () { },//超过文件数限制时执行
                    Distinct: true,//是否不允许相同文件
                    onSame: function () { },//有相同文件时执行
                    ExtIn: [],//允许后缀名
                    onNotExtIn: function () { },//不是允许后缀名时执行
                    ExtOut: [],//禁止后缀名，当设置了ExtIn则ExtOut无效
                    onExtOut: function () { },//是禁止后缀名时执行
                    onFail: function () { },//文件不通过检测时执行(其中参数是file对象)
                    onIni: function () { }//重置时执行
                };
                Extend(this.options, options || {});
            },
            //整理空间
            Ini: function () {
                //整理文件集合
                this.Files = [];
                //整理文件空间，把有值的file放入文件集合
                Each(this.Folder.getElementsByTagName("input"), Bind(this, function (o) {
                    if (o.type == "file") { o.value && this.Files.push(o); this.onIniFile(o); }
                }))
                //插入一个新的file
                var file = document.createElement("input");
                file.name = this.FileName; file.type = "file"; file.onchange = Bind(this, function () { this.Check(file); this.Ini(); });
                this.Folder.appendChild(file);
                //执行附加程序
                this.onIni();
            },
            //检测file对象
            Check: function (file) {
                //检测变量
                var bCheck = true;
                //空值、文件数限制、后缀名、相同文件检测
                if (!file.value) {
                    bCheck = false; this.onEmpty();
                } else if (this.Limit && this.Files.length >= this.Limit) {
                    bCheck = false; this.onLimite();
                } else if (!!this.ExtIn.length && !RegExp("\.(" + this.ExtIn.join("|") + ")$", "i").test(file.value)) {
                    //检测是否允许后缀名
                    bCheck = false; this.onNotExtIn();
                } else if (!!this.ExtOut.length && RegExp("\.(" + this.ExtOut.join("|") + ")$", "i").test(file.value)) {
                    //检测是否禁止后缀名
                    bCheck = false; this.onExtOut();
                } else if (!!this.Distinct) {
                    Each(this.Files, function (o) { if (o.value == file.value) { bCheck = false; } })
                    if (!bCheck) { this.onSame(); }
                }
                //没有通过检测
                !bCheck && this.onFail(file);
            },
            //删除指定file
            Delete: function (file) {
                //移除指定file
                this.Folder.removeChild(file); this.Ini();
            },
            //删除全部file
            Clear: function () {
                //清空文件空间
                Each(this.Files, Bind(this, function (o) { this.Folder.removeChild(o); })); this.Ini();
            }
        }

        var fu = new FileUpload("uploadForm", "idFile", {
            Limit: 1, ExtIn: ["xls", "xlsx"],
            onIniFile: function (file) { file.value ? file.style.display = "none" : this.Folder.removeChild(file); },
            onEmpty: function () { alert("请选择一个文件"); },
            onLimite: function () { alert("超过上传限制"); },
            onSame: function () { alert("已经有相同文件"); },
            onNotExtIn: function () { alert("只允许上传" + this.ExtIn.join("，") + "文件"); },
            onFail: function (file) { this.Folder.removeChild(file); },
            onIni: function () {
                //显示文件列表
                var arrRows = [];
                if (this.Files.length) {
                    var oThis = this;
                    Each(this.Files, function (o) {
                        var a = document.createElement("a"); a.innerHTML = "取消"; a.href = "javascript:void(0);";
                        a.onclick = function () { oThis.Delete(o); return false; };
                        arrRows.push([o.value, a]);
                    });
                } else { arrRows.push(["<font color='gray'>没有添加文件</font>", "&nbsp;"]); }
                AddList(arrRows);
                //设置按钮
                $("idBtnupload").disabled = $("idBtndel").disabled = this.Files.length <= 0;
            }
        });

        $("idBtnupload").onclick = function () {
            //显示文件列表
            var arrRows = [];
            Each(fu.Files, function (o) { arrRows.push([o.value, "&nbsp;"]); });
            AddList(arrRows);

            fu.Folder.style.display = "none";
            $("idProcess").style.display = "";
            $("idMsg").innerHTML = "正在导入数据到数据库中，请稍候……<br />有可能因为网络问题，出现程序长时间无响应，请点击“<a href='?'><font color='red'>取消</font></a>”重新上传文件";

            fu.Form.submit();
        }

        //用来添加文件列表的函数
        function AddList(rows) {
            //根据数组来添加列表
            var FileList = $("idFileList"), oFragment = document.createDocumentFragment();
            //用文档碎片保存列表
            Each(rows, function (cells) {
                var row = document.createElement("tr");
                Each(cells, function (o) {
                    var cell = document.createElement("td");
                    if (typeof o == "string") { cell.innerHTML = o; } else { cell.appendChild(o); }
                    row.appendChild(cell);
                });
                oFragment.appendChild(row);
            })
            //ie的table不支持innerHTML所以这样清空table
            while (FileList.hasChildNodes()) { FileList.removeChild(FileList.firstChild); }
            FileList.appendChild(oFragment);
        }



        $("idBtndel").onclick = function () { fu.Clear(); }
        function ShowPercent(percent) {
            $("lblPercent").innerHTML = percent;
        }
        //在后台通过window.parent来访问主页面的函数
        function Finish(msg) {
            alert(msg);
            location.href = location.href;
        }

    </script>--%>
    <title>文件上传</title>
</head>
<body>
    <div class="cent">
        <form id="uploadForm" runat="server" style="height: 230px;">
            <table border="0" cellspacing="1" class="fu_list">
                <thead>
                    <tr>
                        <td colspan="3"><b>上传文件</b></td>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td align="right" width="15%" style="line-height: 35px;">添加文件：</td>
                        <td>
                            <%--<a href="javascript:void(0);" class="files" id="idFile"></a>--%>
                            <asp:FileUpload runat="server" ID="file_open" Width="300px" />
                            <img id="idProcess" style="display: none;" src="Images/loading.gif" /></td>
                        <td>
                            <label id="lblPercent"></label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <table border="0" cellspacing="0">
                                <thead>
                                    <tr>
                                        <td>文件路径&nbsp;&nbsp;</td>
                                        <td width="100">
                                            <asp:Label runat="server" ID="lb_filepath"></asp:Label></td>
                                    </tr>
                                </thead>
                                <tbody id="idFileList"></tbody>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="color: gray">
                            <br />
                            1、只允许上传固定后缀名文件(视频，文档，幻灯片)。
                        <br />
                            2、视频固定格式为.mp4或wmv、文档固定格式为.doc、幻灯片固定格式为.ppt。<br />
                            (<b style="color: red;">视频格式可用视频工厂进行转换，文档幻灯片格式请在保存类型下拉列表中选择97-2003文件类型。</b>)
                    <br />
                            3、360浏览器如果点击添加文件无响应，请使用360极速模式或者更换浏览器。
                        <br />
                            4、文件上传成功后，可在几分钟后根据文件列表查看。  </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="center" id="idMsg">
                            <asp:Button runat="server" Text="开始上传" ID="btn_upload" OnClick="btn_upload_Click" />
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button runat="server" Text="全部取消" ID="btn_cancel" Enabled="false" OnClick="btn_cancel_Click" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </form>
    </div>
</body>
</html>
