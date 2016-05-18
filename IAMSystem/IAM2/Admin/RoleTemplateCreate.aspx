<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" CodeBehind="RoleTemplateCreate.aspx.cs" Inherits="IAM.Admin.RoleTemplateCreate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script type="text/javascript">
         $(function () {
             $("#dvloading").dialog({
                 autoOpen:false,
                 width: 320,
                 modal: true,
                 hide: true,
                 closeOnEscape:false
                 
             });
         });

         function LoadFunc() {
             $("#dvloading").dialog("open");
             //$("#<%=Button1.ClientID%>").prop("disabled", "true");
             return true;
         }
     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvloading">
        <img src="../Images/piliang_loading.gif" width="306px" height="191px" />
    </div>
    <div class="pure-control-group" style="margin-top: 10px;" id="dangediv" runat="server">
        <label for="email" style="width: 70px; padding-left: 5px; text-align: left;">
            模版名称:</label>
        <asp:TextBox ID="txtTemplateName" runat="server" Width="351px"></asp:TextBox>&nbsp;&nbsp;
        
        <asp:Button ID="btncreate" runat="server" Text=" 确 定 "
            CssClass="pure-button pure-button-primary" OnClick="btncreate_Click" />
       
    </div>
    <div class="pure-control-group" style="margin-top: 10px;" id="piliangdiv" runat="server">
        <label for="email" style="width: 100px; padding-left: 5px; text-align: left;">
            上传Excel文件:</label>
        <asp:FileUpload ID="fileupload1" runat="server" />&nbsp;&nbsp;
        
        <asp:Button ID="Button1" runat="server" Text=" 确 定 "
            CssClass="pure-button pure-button-primary" OnClientClick="javascript:return LoadFunc();" OnClick="Button1_Click" />
           
    </div>
</asp:Content>
