<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" CodeBehind="AD_WorkGroupManager.aspx.cs" Inherits="IAM.Admin.AD_WorkGroupManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin: 0px; width: 726px; overflow-x: hidden;">
        <fieldset>
            <legend>AD可控工作组信息</legend>
        
        <table class="pure-table pure-table-bordered" style="width: 300px; table-layout: fixed; text-align: center; text-decoration: none; font-size: 12px;">
            <thead>
                <tr>
                    <th>组名称</th>
                    <th>描述</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="repeaterADWorkgroup" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="background: #efefef; width: 200px;">
                                <%#Eval("Name") %>
                            </td>
                            <td style="background: #efefef; width: 200px;">
                                <%#Eval("DESCRIPTION") %>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr>
                            <td style="background: white; width: 200px;">
                                <%#Eval("Name") %>
                            </td>
                            <td style="background: white; width: 200px;">
                                <%#Eval("DESCRIPTION") %>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>


            </tbody>
        </table>
            </fieldset>
    </div>
</asp:Content>
