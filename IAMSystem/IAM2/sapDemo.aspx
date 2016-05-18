<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sapDemo.aspx.cs" Inherits="IAM.sapDemo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript">
        window.onload = function () {
            document.getElementById("txtbox1").value = "rea";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:GridView ID="GridView1" runat="server"></asp:GridView>
    </div>
        <asp:TextBox ID="txtbox1" runat="server"></asp:TextBox>
        <asp:Button ID="button1" runat="server" Text="读取" OnClick="button1_Click" />
    </form>
</body>
</html>
