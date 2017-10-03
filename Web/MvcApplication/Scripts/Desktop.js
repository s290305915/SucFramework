function resizeLayout() {
    // 主操作区域高度
    var wWidth = (window.document.documentElement.clientWidth || window.document.body.clientWidth || window.innerHeight);
    var wHeight = (window.document.documentElement.clientHeight || window.document.body.clientHeight || window.innerHeight);
    var nHeight = $('#north').is(':visible') ? $('#north').outerHeight() : 0;
    var fHeight = $('#funcbar').is(':visible') ? $('#funcbar').outerHeight() : 0;
    var cHeight = wHeight - nHeight - fHeight - $('#south').outerHeight() - $('#hero_bar').outerHeight() - $('#taskbar').outerHeight();
    $('#ContentPannel').height(cHeight);
    $('#Content').height(cHeight);
    //一级标签宽度
    var width = wWidth - $('#taskbar_left').outerWidth() - $('#taskbar_right').outerWidth();
    $('#tabs_container').width(width - $('#tabs_left_scroll').outerWidth() - $('#tabs_right_scroll').outerWidth() - 2);
    $('#taskbar_center').width(width - 1);   //-1是为了兼容iPad
    $('#tabs_container').triggerHandler('_resize');
};
function initStartMenu() {
    //点击页面，隐藏各级菜单面板，并清除二级和三级菜单的active状态
    $('#overlay_startmenu').click(function () {
        if ($('#start_menu_panel:visible').length) {
            $('#overlay_startmenu').hide();
            $('#start_menu_panel').slideUp(300);
            $('#start_menu').removeClass('active');
        }
    });
    //鼠标点击导航图标按钮弹出菜单面板
    $('#start_menu').bind('click', function () {
        if ($('#start_menu_panel:visible').length) {
            $('#overlay_startmenu').hide();
            $('#start_menu_panel').slideUp(300);
            $(this).removeClass('active');
        }
        //设置导航图标为active状态
        $(this).addClass('active');
        //遮罩层位置和显示
        $('#overlay_startmenu').show();
        //菜单面板位置
        var top = $('#start_menu').offset().top + $('#start_menu').outerHeight() - 6;
        $('#start_menu_panel').css({ top: top });
        $('#start_menu_panel').slideDown('fast');
    });
}
//窗口resize事件
$(window).resize(function () { resizeLayout(); });
/*设置桌面图标*/
function setDeskresize() {
    resizeU();
    $(window).resize(resizeU);
    function resizeU() {
        $('.desktop .desk').each(function () {
            var deskpic = $(this).find('li');
            if (deskpic.length > 0) {
                var picHeight = deskpic[0].offsetHeight;
                var t = 20, l = 20, top = 40, left = 20, count = 1;
                var clientHeight = $("#Content").height();
                for (var i = 0, len = deskpic.length; i < len; i++) {
                    if (top >= (clientHeight - picHeight)) {
                        top = 40;
                        left += (l + picHeight);
                    }
                    deskpic[i].style.top = top + "px";
                    deskpic[i].style.left = left + "px";
                    top += (t + picHeight);
                }
            }
        })
    }
}
$(document).ready(function ($) {
    //调整窗口大小
    resizeLayout();
    //开始菜单
    initStartMenu();
    //设置桌面图标
    setDeskresize();
});
/**安全退出**/
function IndexOut() {
    top.showConfirmMsg('亲：您是否确认退出通用基本权限系统？', function (r) {
        if (r) {
            getAjax("/Frame/Frame.ashx", "action=Outlogin", function (data) {
                location.href = ''; //登录页
            })
        }
    });
}
//最大化
function Maximize() {
    $("#north").hide();
    $("#hide_topbar").attr('onclick', 'Fullrestore()');
    //调整窗口大小
    resizeLayout();
    //设置桌面图标
    setDeskresize();
}
//还原
function Fullrestore() {
    $("#north").show();
    $("#hide_topbar").attr('onclick', 'Maximize()');
    //调整窗口大小
    resizeLayout();
    //设置桌面图标
    setDeskresize();
}