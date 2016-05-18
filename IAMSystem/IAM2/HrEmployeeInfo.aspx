<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" CodeBehind="HrEmployeeInfo.aspx.cs" Inherits="IAM.Admin.HrEmployeeInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pure-control-group" style="margin-top: 10px;">

        <fieldset style="border: 1px solid #efefef;">
            <legend style="font-size: 14px; color: #3C72DF">HR 员工详情</legend>
            <div class="pure-control-group" style="margin-top: 10px;">
                <table style="width:800px;">
                    <tr>
                        
                        <td class="td-title-width" style="width:50px;">工号:
                        </td>
                        <td class="td-context-width" colspan="7">
                           <asp:Label ID="lblgonghao" ForeColor="blue" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-title-width" style="width:50px;">姓名:
                        </td>
                        <td class="td-context-width" style="width:100px;">
                            <asp:Label ID="lblName" ForeColor="blue" runat="server"></asp:Label>
                        </td>
                        <td class="td-title-width" style="width:100px;">岗位:
                        </td>
                        <td class="td-context-width" style="width:150px;">
                           <asp:Label ID="lblGangwei" ForeColor="blue" runat="server"></asp:Label>
                        </td>
                        <td class="td-title-width" style="width:100px;">所在部门:
                        </td>
                        <td class="td-context-width" style="width:100px;">
                            <asp:Label ID="lblPartment" ForeColor="blue" runat="server"></asp:Label>
                        </td>
                        <td class="td-title-width" style="width:100px;">上级部门:
                        </td>
                        <td class="td-context-width" style="width:100px;">
                            <asp:Label ID="lblPrePartment" ForeColor="blue" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-title-width">手机:
                        </td>
                        <td class="td-context-width">
                            <asp:Label ID="lblPhone" ForeColor="blue" runat="server"></asp:Label>
                        </td>
                        <td class="td-title-width" style="width:100px;">到职日期:
                        </td>
                        <td class="td-context-width">
                            <asp:Label ID="lblComeDate" ForeColor="blue" runat="server"></asp:Label>
                        </td>
                        <td class="td-title-width">离职日期:
                        </td>
                        <td class="td-context-width">
                            <asp:Label ID="lblPostDate" ForeColor="blue" runat="server"></asp:Label>
                        </td>
                        <td class="td-title-width"><%--部门撤销日期:--%>
                        </td>
                        <td class="td-context-width">
                            
                        </td>
                    </tr>
                  
                </table>

            </div>
        </fieldset>
    </div>
</asp:Content>
