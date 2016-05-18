<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" CodeBehind="SapRoleList.aspx.cs" Inherits="IAM.Admin.SapRoleList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script src="../Content/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            jQuery("#ddlRoleList").change(function () {
                var _value = jQuery("#ddlRoleList").val();
                if (_value.indexOf("^") != -1)
                {
                    jQuery("#<%=txtRoleName.ClientID%>").val(_value.split('^')[1]);
                }
            });

            jQuery("#<%=btnYes.ClientID%>").click(function () {
                var _value = "";
                _value = jQuery("#ddlRoleList").val() + "^" + jQuery("#<%=txtStartDate.ClientID%>").val() + "^" + jQuery("#<%=txtEndDate.ClientID%>").val();
                window.returnValue = _value;
                window.close(true);
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style=" width: 650px; height: auto;">
        <fieldset style="border: 1px solid #efefef;">
            <legend style="font-size: 14px; color: #3C72DF">角色委派</legend>
            <div class="pure-control-group" style="margin-top: 10px;">
                <table>
                    <tr>
                        <td class="td-title-width">
                            角色Id:
                        </td>
                        <td class="td-context-width">
                          <%=fdajfdlajfd %>
                        </td>
                        <td class="td-title-width">
                            角色名称:
                        </td>
                        <td class="td-context-width">
                           <asp:TextBox ID="txtRoleName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-title-width">
                            有效期从:
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtStartDate" runat="server" onClick="WdatePicker()" 
class="Wdate"></asp:TextBox>
                        </td>
                        <td class="td-title-width">
                           有效期至:
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtEndDate" runat="server" onClick="WdatePicker()" 
class="Wdate"></asp:TextBox>
                        </td>
                        
                    </tr>
                   
                </table>
            </div>

            
        </fieldset>
    </div>
    <div style="margin-top: 10px; text-align: center;" runat="server" id="nav_top">
        <input type="button" id="btnYes" runat="server" value=" 确 定 " class="pure-button pure-button-primary" />&nbsp;
         <input type="button" value=" 取 消 " onclick="javascript: window.close(true);" class="pure-button pure-button-primary" />
    </div>
</asp:Content>
