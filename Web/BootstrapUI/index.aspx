<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="BootstrapUI.index" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="Content/bootstrap.min.css">
    <link rel="stylesheet" href="Content/bootstrap-table.min.css">
    <link href="Content/reveal.css" rel="stylesheet" />

    <script src="Scripts/jquery.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="Scripts/tableExport.js"></script>
    <script src="Scripts/jquery.base64.js"></script>
    <script src="Scripts/bootstrap-table.js"></script>
    <script src="Scripts/bootstrap-table-export.js"></script>
    <script src="Scripts/jquery.reveal.js"></script>
    <script>
        $(function () {

            $("#tx_val").val("{1,2,3,4}");
            var t = "<%=getp()%>";
            BindTable();
        });

        function BindTable() {
            $('#table').bootstrapTable({
                url: 'Data/UserHandler.ashx?opt=GetUserList',
                pagination: true,
                showRefresh: true,
                showColumns: true,
                showFooter: true,
                showHeader: true,
                search: true,
                columns: [
                { field: 'ID', title: 'ID' },
                { field: 'NAME', title: '姓名', formatter: nameFormatter },
                { field: 'LOGIN_NAME', title: '登录名' },
                { field: 'CREAT_TIME', title: '创建时间' },
                { field: 'LOGIN_COUNT', title: '登录次数' },
                { field: 'REMARK', title: '备注' },
                { field: 'ROLE_ID', title: '角色' },
                { field: 'PHONENO', title: '电话号码' },
                { field: 'IP_ADDRESS', title: '注册IP' },
                { field: 'UNIT', title: '单位' },
                { field: 'IS_CHECK', title: '是否审核员' },
                {
                    field: "ID", title: "操作", width: "150",
                    formatter: function (value) {
                        return "<img src='Content/img/gear_in.png' title='修改' onclick = 'ModifybyId(" + value + ")' style='cursor:hand'></img>"
                            + "&nbsp;&nbsp;&nbsp;&nbsp;" +
                               "<img src='Content/img/cancel.png' title='删除' onclick='DeletebyId(" + value + ")'  style='cursor:hand'></img>";
                    }
                }]
            });

        }

        function DeletebyId(v) {
            alert(v);
        }
        //
        function ModifybyId(v) {
            $('#Excute').reveal();
        }

        function nameFormatter(value) {
            return "♥-" + value + "-😘";
        }

    </script>
</head>
<body>
    <div class="container">
        <table id="table">
        </table>
    </div>


    <div id="Excute" class="reveal-modal">
        <h1>弹出</h1>
        <p id="Content">
            需要修改自己做一个弹窗<br />
        </p>
        <a class="close-reveal-modal">&#215;</a>
    </div>

    <input type="hidden" runat="server" id="tx_val" />
</body>
</html>
