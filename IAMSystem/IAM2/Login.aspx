<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="IAM.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>MaterialMatrix-Login</title>
   
    <link href="style/css/style.css" rel="stylesheet" type="text/css" />
   
<style type="text/css">
.error
{  color:Yellow
    }
</style>
<script type="text/javascript">
    if (window.location != window.parent.location) {
        window.parent.location = "login.aspx";
    }
</script>
</head>

<body>
<form id="form1" runat="server" defaultbutton="btnButton">
<div class="login">
	<div class="loginbox">
<div class="lin">
<%  if (isDebug)
   { %>
<asp:TextBox CssClass="logoname" Text="haiboax" runat="server" ID="loginname" />

<%}
   else
   { %>
<span class="logoname"><%=Request.LogonUserIdentity.Name%></span>
<%} %>
<br />
<asp:Label ID="errorMess" CssClass="error" runat="server" ></asp:Label>
</div>
<div class="lin"><select name="" id="languagetype" runat="server">
<option value="zh-cn">简体中文</option>
</select></div>
<div class="btnlin">

<asp:Button ID="btnButton" runat="server" CssClass="btn" 
        onclick="btnButton_Click" />
</div>
  </div>  
</div>
</form>
</body>
</html>
