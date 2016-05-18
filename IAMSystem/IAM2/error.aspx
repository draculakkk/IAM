<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="error.aspx.cs" Inherits="IAM.error" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/main.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">

        <div id="main">
            <header id="header">
      <h1><span class="icon">!</span>500<span class="sub">系统错误</span></h1>
    </header>
            <div id="content" style="font-family:'Microsoft YaHei UI'; font-size:14px; text-align:center">
                <h2>对不起，系统内部发生错误</h2>
                <p>请将错误ID提供给系统管理员
                <br/>错误ID：<asp:Label ID="lblMessage" runat="server"></asp:Label></p>
            </div>
        </div>
    </form>
</body>
</html>
