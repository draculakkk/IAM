<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" CodeBehind="AD_Department_WorkGroupCreate.aspx.cs" Inherits="IAM.Admin.AD_Department_WorkGroupCreate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        //jQuery().ready(function () {
        //    jQuery("table tr>td:eq(0)").css("width", "150px")
        //    jQuery("table tr:eq(0)>td:eq(2)").css("width", "150px")
        //    jQuery("table tr>td:eq(1)").css("width", "500px")
        //    jQuery("table tr:eq(0)>td:eq(3)").css("width", "175px")
        //    jQuery("table tr:eq(0)>td:eq(1)").css("width", "175px")
        //});
    </script>
    <style type="text/css">
        .pure-button-primary {
            height: 21px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <fieldset style="border:1px solid #efefef;">
        <legend style="font-size:14px;color:#3C72DF">AD工作组信息</legend>
        <div class="pure-control-group" style="margin-top: 10px;">
            <table width="650px">
                <tr>
                    <td class="td-title-width">
                        <font style="color:red;">*</font>HR部门:
                    </td>
                    <td class="td-context-width" colspan="3">
                        <asp:TextBox ID="txtHRDepartment" runat="server"></asp:TextBox>

                    </td>
                   
                   
                </tr>
                <tr>
                    <td class="td-title-width">
                        <font style="color:red;">*</font>AD部门组:
                    </td>
                    <td class="td-context-width" colspan="3">
                        <asp:TextBox ID="txtAdDepartment" runat="server"></asp:TextBox>
                    </td>
                   
                </tr>
                <tr>
                    <td class="td-title-width">
                        中心:
                    </td>
                    <td class="td-context-width" colspan="3">
                        <asp:TextBox ID="txtCenter" runat="server"></asp:TextBox>
                    </td>
                    
                </tr>
                <tr>
                    <td class="td-title-width" >
                        部门:
                        
                    </td>
                    <td class="td-context-width" colspan="3">
                        <asp:TextBox ID="txtDepartment" runat="server"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td class="td-title-width">
                        科室:
                        
                    </td>
                    <td class="td-context-width" colspan="3">
                        <asp:TextBox ID="txtkeshi" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td-title-width">
                        <font style="color:red;">*</font>邮件数据库:
                        
                    </td>
                    <td class="td-context-width" colspan="3">
                        <asp:TextBox ID="txtEmailDatabase" runat="server"></asp:TextBox>
                        &nbsp;
                        <input type="checkbox" runat="server" id="chkFalse" /><span>是否禁用</span>
                    </td>
                </tr>
                <tr>
                 <td class="td-title-width">默认排序:</td>
                    <td colspan="3" class="td-context-width">
                        <font style="color:blue">第</font><asp:Label ID="lblOrder" runat="server"></asp:Label><font style="color:blue">位</font>
                    </td>
                </tr>
            </table>
            <div style="margin:10px 200px">
                 <asp:Button ID="btnYes" runat="server" Text="确定" CssClass="pure-button pure-button-primary" OnClick="btnYes_Click" />
            &nbsp;
           <input type="button" value="取消" class="pure-button pure-button-primary" onclick="javascript: window.close(true);"/>
            </div>
          
        </div>
    </fieldset>
</asp:Content>
