<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LayoutBase.aspx.cs" Inherits="IAM.LayoutBase" EnableEventValidation="false" %>

<%@ Register Src="~/Control/LeftMenu.ascx" TagName="LeftMenu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>IAM管理系统</title>
    <link rel="shortcut icon" href="favicon.ico" />
    <link href="Content/style.css" rel="stylesheet" type="text/css" />
    <link href="Content/module.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.10.2.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">
        if (window != top)
            top.location.href = location.href;
    </script>
    <script type="text/javascript">
        function windowChange() {
            var windowWidth = document.body.cilentWidth;
            var windowHeight = jQuery(window).height();

            var amiisHeight = 5;
            windowHeight = parseInt(windowHeight) - (parseInt(amiisHeight) + 80);

            jQuery(".main_help,#switch,.continer,#leftheid,.coned").css({ height: windowHeight });
            jQuery("#contentiframe").height(windowHeight);
            jQuery(".sidebar").css("height", windowHeight).css("overflow", "auto");
        }
    </script>
    <script type="text/javascript">
        jQuery(function () {
            //窗口改变大小 Start

            jQuery(window).resize(function () {
                windowChange();
            });
            jQuery(window).load(function () {
                windowChange();
            });
            //窗口改变大小 end


            jQuery(".sidebar, #switch, #contentiframe").css("height", "500px");

            //左侧控制
            jQuery("#switch").toggle(
            function () {
                jQuery("#leftcontent").hide();
                jQuery("#leftheid").removeClass("con");
                jQuery("#leftheid").addClass("coned");
                windowChange();
            }, function () {
                jQuery("#leftcontent").show();
                jQuery("#leftheid").addClass("con");
                jQuery("#leftheid").removeClass("coned");
                windowChange();
            });

        });

        //session维持机制
        var time3 = setInterval(function () {
            jQuery.post("keepsession.ashx", function (data, retuslt) { }, "text");
        }, 1000 * 60 * 5);

    </script>


    <!--[if IE 6]> 
<style type="text/css">

* html, * html body{background-image:url(about:blank);background-attachment:fixed;}
* html .qstion{position:absolute;bottom:auto;
top:expression(eval(document.documentElement.scrollTop+document.documentElement.clientHeight-this.offsetHeight-(parseInt(this.currentStyle.marginTop, 10)||0)-(parseInt(this.currentStyle.marginBottom, 10)||0)));}
</style>
<![endif]-->
</head>
<body>
    <form id="form1" runat="server">
        <!--页头 start-->
        <div class="head hedhelp">
            <div class="helplogo">
            </div>
            <div class="rtlogo">
            </div>
            <div class="toplink conttop" style="margin-top: 22px;">
                <a href="#" target="_blank" style="color: white">欢迎您,
                <asp:Label ID="username" Text="未登陆" ForeColor="White" runat="server"></asp:Label></a>
                <asp:LinkButton ID="lbtnLoginOut" runat="server" Text="注销"
                    OnClick="lbtnLoginOut_Click" ForeColor="White"></asp:LinkButton>
                <a href="Account/ChangePassword.aspx" style="display: none;">更改密码</a>
            </div>
        </div>
        <div class="menu" style="height: 10px"></div>
        <!--页头 end-->
        <div class="continer">
            <!--  左边样式 -->
            <uc1:LeftMenu ID="LeftMenu1" runat="server" />
            <!-- end 左边样式 -->
            <div id="leftheid" class="con" style="overflow-y:hidden;">
                <div id="switch">
                    <img src="images/lft_arrow.gif" alt="隐藏左侧导航栏" class="imgarrow" />
                </div>
                <iframe id="contentiframe" src="admin/HREmployeeManager.aspx"
                    style="border: 0;overflow-y:hidden" />
            </div>
        </div>
        <div class="clear">
        </div>
        <div class="footer">
            <center>
                &copy; Copyright Alcatel-Lucent shanghai all rights reserved</center>
        </div>
    </form>
</body>
</html>
