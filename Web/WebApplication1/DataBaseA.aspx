<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataBaseA.aspx.cs" Inherits="WebApplication1.DataBaseA" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="Scripts/jquery-1.7.1.js"></script>
    <script>
        $(function () {
            var t = $("#tid").val();
            alert(t);
            post('pages/statisticsJsp/excel.action', { html: prnhtml, cm1: 'sdsddsd', cm2: 'haha' });
        })

        ///通过单独制作form来调用post
        function post(url, params) {
            var temp = document.createElement("form");
            temp.action = url;
            temp.method = "post";
            temp.style.display = "none";
            for (var x in params) {
                var opt = document.createElement("input");
                opt.name = x;
                opt.value = params[x];
                temp.appendChild(opt);
            }
            document.body.appendChild(temp);
            temp.submit();
            return temp;
        }

        //调用方法 如      
    </script>
    <title></title>
</head>
<body>
    <input type="hidden" runat="server" id="tid" />
    <form id="form1" runat="server">
        <div>
            <ul>
                <asp:Repeater runat="server" ID="rp_list">
                    <ItemTemplate>
                        <li><%#Eval("ID") %><a href="DataBaseB.aspx?id=<%#Eval("ID") %>"><%#Eval("NAME") %></a></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </form>
</body>
</html>
