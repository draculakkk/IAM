<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" CodeBehind="HrRoleList.aspx.cs" Inherits="IAM.rolelist.HrRoleList" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
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
                    <th style="width: 150px">角色名称
                    </th>
                    <th style="width: 100px">是否删除
                    </th>
                    <th style="width: 100px">是否新增
                    </th>
                    <th style="width: 100px">新值日期
                    </th>
                   
                  
                </tr>
            </thead>
            
        </table>
    </div>
    <div style="margin:0px;padding:0px; width: 675px;overflow-x:hidden;overflow-y:auto;height:480px;">
         <table class="pure-table pure-table-bordered" id="tablelist" style="text-align: center; text-decoration: none; font-size: 12px; width: 450px;table-layout:fixed;">
             <tbody>
                <asp:Repeater ID="repeaterHrRole" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="width: 100px;">
                                <%#Eval("Role_code")%>
                            </td>
                            <td style="width: 150px;">
                                <%#Eval("role_name")%>
                            </td>
                            <td style="width: 100px;">
                                <%#Eval("Dr")!=null&&Eval("Dr").ToString().Equals("0")?"否":"是"%>
                            </td>
                            <td style="width: 100px;">
                 
                                  <%#Eval("p1")==null?"否":"是"%>
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
    <div class="pure-control-group" style="margin-top: 10px;">
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" HorizontalAlign="Center" CssClass="paginator" CurrentPageButtonClass="cpb" FirstPageText="首页" AlwaysShow="true"
                LastPageText="尾页" PrevPageText="上一页" NextPageText="下一页" NumericButtonTextFormatString="[{0}]"
                ShowCustomInfoSection="Left" ShowBoxThreshold="2" PageSize="5"                
                CustomInfoSectionWidth="150px" InputBoxClass="text2" TextAfterInputBox="" OnPageChanging="AspNetPager1_PageChanging" />
    </div>
</asp:Content>
