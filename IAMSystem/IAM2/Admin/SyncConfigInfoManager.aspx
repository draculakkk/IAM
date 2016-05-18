<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SyncConfigInfoManager.aspx.cs" Inherits="IAM.Admin.SyncConfigInfo" MasterPageFile="~/Admin/master.Master" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
<script type="text/javascript">
    jQuery(document).ready(function () {
        jQuery("#<%=txtName.ClientID %>,#<%=txtTime.ClientID %>").prop("readonly", true);

        jQuery("#<%=btnEidt.ClientID %>").bind("click", function () {
            jQuery("#<%=btnUpdate.ClientID %>,#<%=btnCancel.ClientID %>").css("display", "");
            jQuery("#<%=txtTime.ClientID %>").prop("readonly", false);
            jQuery(this).css("display", "none");
            return false;
        });

        jQuery("#<%=btnCancel.ClientID %>").bind("click", function () {
            jQuery("#<%=btnEidt.ClientID %>").css("display", "");
            jQuery("#<%=btnCancel.ClientID %>,#<%=btnUpdate.ClientID %>").css("display", "none");
            jQuery("#<%=txtTime.ClientID %>").val(jQuery("#<%=hiddentime.ClientID %>").val()).prop("readonly", true);
            return false;
        });
    });
</script>
</asp:Content>

<asp:Content ID="body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<div class="pure-control-group" style="margin-top:10px;">
       <div class="pure-control-group" style="margin-top:10px;">
            <label for="email" >
                同步任务名称:</label>
           <asp:TextBox ID="txtName" runat="server"></asp:TextBox>

           <label for="email" style="width:100px">执行时间:</label>
           <asp:TextBox ID="txtTime" runat="server"></asp:TextBox>
           <asp:HiddenField ID="hiddentime" runat="server" />
           <asp:Button ID="btnEidt" runat="server"  Text="编辑"  CssClass="pure-button pure-button-primary"/>
           <asp:Button ID="btnUpdate" runat="server" Text="更新" OnClick="btnUpdate_Click" style="display:none" CssClass="pure-button pure-button-primary" />
            <asp:Button ID="btnCancel" runat="server" Text="取消" style="display:none" CssClass="pure-button pure-button-primary" />
        </div>
    
        </div>
</asp:Content>