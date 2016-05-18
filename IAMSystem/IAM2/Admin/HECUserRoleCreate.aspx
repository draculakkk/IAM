<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" CodeBehind="HECUserRoleCreate.aspx.cs" Inherits="IAM.Admin.HECUserRoleCreate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<fieldset style="border: 1px solid #efefef;">
            <legend style="font-size: 14px; color: #3C72DF">角色委派</legend>
            <div class="pure-control-group" style="margin-top: 10px;">
                <table>
                    <tr>
                        <td class="td-title-width">
                            角色代码:
                        </td>
                        <td class="td-context-width">
                            <asp:DropDownList ID="dplRoleCD" runat="server"></asp:DropDownList>
                        </td>
                        <td class="td-title-width">
                            角色名称:
                        </td>
                        <td class="td-context-width">
                        <asp:TextBox ID="txtRoleName" runat="server"></asp:TextBox>
                        </td>
                        <td class="td-title-width">
                            公司代码:
                        </td>
                        <td class="td-context-width">
                        <asp:TextBox ID="txtCompanyNumber" runat="server"></asp:TextBox>
                        </td>
                       <td class="td-title-width">
                            公司简称:
                        </td>
                        <td class="td-context-width">
                        <asp:TextBox ID="txtCompanyName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    
                    <tr>
                        <td class="td-title-width">
                            有效期自:
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtStartDate" onClick="WdatePicker()" runat="server"></asp:TextBox>
                        </td>
                        <td class="td-title-width">
                            有效期至:
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtEndDate" onClick="WdatePicker()" runat="server"></asp:TextBox>
                        </td>
                        <td class="td-title-width">
                            
                        </td>
                        <td class="td-context-width" colspan="3">
                            
                        </td>
                        
                    </tr>
                    
                </table>
            </div>
        </fieldset>
</asp:Content>
