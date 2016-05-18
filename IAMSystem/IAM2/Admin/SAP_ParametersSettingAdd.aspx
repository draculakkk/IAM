<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" CodeBehind="SAP_ParametersSettingAdd.aspx.cs" Inherits="IAM.Admin.SAP_ParametersSettingAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <fieldset style="border:1px solid #efefef;">
        <legend style="font-size:14px;color:#3C72DF">AD工作组信息</legend>
        <div class="pure-control-group" style="margin-top: 10px;">
            <table width="650px">
                <tr>
                    <td class="td-title-width">
                        <font style="color:red;">*</font>参数ID:
                    </td>
                    <td class="td-context-width" colspan="3">
                        <asp:TextBox ID="txtParmeterID" runat="server"></asp:TextBox>

                    </td>
                   
                   
                </tr>
                
                <tr>
                    <td class="td-title-width">
                        <font style="color:red;">*</font>是否禁用:
                        
                    </td>
                    <td class="td-context-width" colspan="3">
                      
                        <input type="checkbox" runat="server" id="chkFalse" /><span></span>
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
