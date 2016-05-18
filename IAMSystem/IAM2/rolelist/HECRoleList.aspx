<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" CodeBehind="HECRoleList.aspx.cs" Inherits="IAM.rolelist.HECRoleList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script type="text/javascript">
         jQuery(document).ready(function () {
             jQuery("#tablelist tbody>tr:odd>td").css("background", "#EFEFEF");
             jQuery("#tablelist tbody>tr:even>td").css("background", "#FFF");
         });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div  style="margin-top: 10px; width: 550px;">
        <table class="pure-table pure-table-bordered" style="text-align: center; text-decoration: none; font-size: 14px; width: 550px;table-layout:fixed;">
            <thead>
                <tr align="center">
                    <th style="width: 100px">角色代码
                    </th>
                    <th style="width: 100px">角色名称
                    </th>
                    <th style="width: 150px">描述
                    </th>
                    <th style="width: 100px">有效期从
                    </th>
                    <th style="width: 100px">有效期至
                    </th>
                   
                  
                </tr>
            </thead>
            
        </table>
    </div>
    <div style="margin:0px;padding:0px; width: 675px;overflow-x:hidden;overflow-y:auto;height:540px;">
         <table class="pure-table pure-table-bordered" id="tablelist" style="text-align: center; text-decoration: none; font-size: 12px; width: 550px;table-layout:fixed;">
             <tbody>
                <asp:Repeater ID="repeaterHecRole" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="width: 100px;">
                                <%#Eval("ROLE_CODE")%>
                            </td>
                            <td style="width: 100px;">
                                <%#Eval("ROLE_NAME")%>
                            </td>
                            <td style="width: 150px;">
                                <%#Eval("DESCRIPTION")%>
                            </td>
                            <td style="width: 100px;">
                 
                                  <%#Eval("START_DATE")%>
                            </td>
                            <td style="width: 100px;">
               <%#Eval("END_DATE")%>
                                               
                            </td>
                          
                        </tr>
                    </ItemTemplate>
                    
                </asp:Repeater>
            </tbody>
             </table>
    </div>
</asp:Content>
