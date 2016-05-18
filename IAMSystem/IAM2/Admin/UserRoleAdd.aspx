<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" CodeBehind="UserRoleAdd.aspx.cs" Inherits="IAM.Admin.UserRoleAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pure-control-group" style="margin-top: 10px;">
        <fieldset style="border: 1px solid #efefef;">
            <legend style="font-size: 14px; color: #3C72DF">操作员信息</legend>
            <div class="pure-control-group" style="margin-top: 10px;">
                <table style="width:500px">
                    <tr>
                        <td class="td-title-width" style="width:100px;">
                            AD账号:
                        </td>
                        <td class="td-context-width" style="width:175px;">
                            <asp:TextBox ID="txtADname" runat="server"></asp:TextBox>
                        </td>
                        <td class="td-title-width" style="width:50px">
                           
                        </td>
                        <td class="td-context-width" style="width:175px;">
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="td-title-width" valign="middle" style="height:150px;">
                            <span style="display:block;height:20px;margin:65px 0px;">权限名称:</span>
                        </td>
                        <td class="td-context-width" >
                            <input type="checkbox" id="chxEndUser" runat="server" /><span style="margin-left:10px">End User</span><br />
                            <input type="checkbox" id="chxAdmin" runat="server" /><span style="margin-left:10px">Admin</span><br /> 
                            <input type="checkbox" id="chkLeader" runat="server" /><span style="margin-left:10px">Leader</span> 
                        </td>
                        <td class="td-title-width" valign="middle" style="height:150px;">
                            <span style="display:block;height:20px;margin:65px 0px;">Manager:</span>
                        </td>
                        <td class="td-context-width" colspan="5">
                           <input type="checkbox" id="chxTC" runat="server" /><span style="margin-left:10px;margin-right:50px;">TC</span><br />
                           <input type="checkbox" id="chxEHR" runat="server" /><span style="margin-left:10px;margin-right:50px;">EHR</span><br />
                           <input type="checkbox" id="chxHEC" runat="server" /><span style="margin-left:10px;margin-right:50px;">HEC</span> <br />
                           <input type="checkbox" id="chxAD" runat="server" /><span style="margin-left:10px;margin-right:50px;">AD</span><br />
                           <input type="checkbox" id="chxSAP" runat="server" /><span style="margin-left:10px;margin-right:50px;">SAP</span>
                        </td>
                       
                        
                    </tr>
                  
                    
                </table>
            </div>
        </fieldset>
        
                <div style="margin-top:10px;text-align:center;">
        <asp:Button ID="btnSave" runat="server" Text=" 确认 " 
                        CssClass="pure-button pure-button-primary" onclick="Button1_Click" />
                        <asp:Button ID="btnUpdate" runat="server" Text="更新" 
                        CssClass="pure-button pure-button-primary" onclick="btnUpdate_Click" />
                        &nbsp;&nbsp;
        <input type="button" ID="btnCancel" value=" 取消 " onclick="javascript:ClosePage();" class="pure-button pure-button-primary" />
        </div>
    </div>
</asp:Content>
