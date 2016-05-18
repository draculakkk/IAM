<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" CodeBehind="SapRoleList.aspx.cs" Inherits="IAM.rolelist.SapRoleList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script type="text/javascript">
         jQuery(document).ready(function () {
             jQuery("#tablelist tbody>tr:odd>td").css("background", "#EFEFEF");
             jQuery("#tablelist tbody>tr:even>td").css("background", "#FFF");
         });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div  style="margin-top: 10px; width: 750px;">
        <table class="pure-table pure-table-bordered" style="text-align: center; text-decoration: none; font-size: 14px; width: 750px;table-layout:fixed;">
            <thead>
                <tr align="center">
                    <th style="width: 200px">角色代码
                    </th>
                    <th style="width: 100px">角色名称
                    </th>
                    <th style="width: 100px">有效期从
                    </th>
                    <th style="width: 100px">有效期至
                    </th>
                    <th style="width: 100px">是否新增
                    </th>
                    <th style="width: 100px">新增日期
                    </th>
                  
                </tr>
            </thead>
            
        </table>
    </div>
    <div style="margin:0px;padding:0px; width: 850px;overflow-x:hidden;overflow-y:auto;height:550px;">
         <table class="pure-table pure-table-bordered" id="tablelist" style="text-align: center; text-decoration: none; font-size: 12px; width: 750px;table-layout:fixed;">
             <tbody>
                <asp:Repeater ID="repeaterSapRole" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="width: 200px;">
                                <%#Eval("ROLEID")%>
                            </td>
                            <td style="width: 100px;">
                                <%#Eval("ROLENAME")%>
                            </td>
                            <td style="width: 100px;">
                 
                                  <%#Eval("STARTDATE")%>
                            </td>
                            <td style="width: 100px;">
               <%#Eval("ENDDATE")%>
                                               
                            </td>
                              <td style="width: 100px;">
                 
                                  <%#Eval("p1")%>
                            </td>
                            <td style="width: 100px;">
               <%#Eval("p2")%>
                                               
                            </td>
                        </tr>
                    </ItemTemplate>
                    
                </asp:Repeater>
            </tbody>
             </table>
    </div>
</asp:Content>
