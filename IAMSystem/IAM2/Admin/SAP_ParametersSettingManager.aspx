<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" CodeBehind="SAP_ParametersSettingManager.aspx.cs" Inherits="IAM.Admin.SAP_ParametersSettingManager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Yes(){
            window.location.href=window.location;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <span style="font-size:14px;font-weight:bold;color:blue;font-family:微软雅黑">SAP 用户默认参数设置</span>
    </div>
<div style="margin-top: 10px;width:550px;">
        <table class="pure-table pure-table-bordered" style="width:550px;table-layout:fixed;text-align: center; text-decoration: none; font-size: 14px;">
            <thead>
                <tr align="center">
                    <th style="width: 200px">参数ID
                    </th>
                     <th style="width:100px;">是否禁用</th>
                    <th style="width:100px;">排序</th>
                    <th align="center" style="width: 150px">操作 &nbsp;<a href="#" onclick="OpenPage1('SAP_ParametersSettingAdd.aspx');">新建</a>
                    </th>
                </tr>
               <asp:Repeater ID="repeaterSetting" runat="server" OnItemCommand="repeaterSetting_ItemCommand">
                   <ItemTemplate>
                       <tr>
                           <td style="background:#efefef;">
                              <%#Eval("ParameterId") %>
                           </td>
                           <td style="background:#efefef;">
                               <%#Eval("isdr").ToString().Equals("1")?"禁用":"启用" %>
                           </td>
                           <td style="background:#efefef;"><%#Eval("OrderColumn") %></td>
                           <td align="right" style="background:#efefef;">
                               <asp:Button ID="btnup" runat="server" Text="" CssClass="btnup" CommandArgument='<%#Eval("id") %>' CommandName="up" />
                                <asp:Button ID="btndown" runat="server" Text="" CssClass="btndown" CommandArgument='<%#Eval("id") %>' CommandName="down"/>
                             <input type="button" value="编辑" onclick="OpenPage1('SAP_ParametersSettingAdd.aspx?id=<%#Eval("id")%>');" />
                           </td>
                       </tr>
                   </ItemTemplate>
                   <AlternatingItemTemplate>
                       <tr>
                           <td style="background:#fff;">
                             <%#Eval("ParameterId") %>
                           </td>
                           <td style="background:#fff;">
                               <%#Eval("isdr").ToString().Equals("1")?"禁用":"启用" %>
                           </td>
                           <td style="background:#fff;"><%#Eval("OrderColumn") %></td>
                           <td align="right" style="background:#fff;">
                               <asp:Button ID="btnup" runat="server" Text="" CssClass="btnup" CommandArgument='<%#Eval("id") %>' CommandName="up" />
                                <asp:Button ID="btndown" runat="server" Text="" CssClass="btndown" CommandArgument='<%#Eval("id") %>' CommandName="down"/>
                             <input type="button" value="编辑" onclick="OpenPage1('SAP_ParametersSettingAdd.aspx?id=<%#Eval("id")%>');" />
                           </td>
                       </tr>
                   </AlternatingItemTemplate>
               </asp:Repeater>
            </thead>
            
        </table>
    </div>
</asp:Content>
