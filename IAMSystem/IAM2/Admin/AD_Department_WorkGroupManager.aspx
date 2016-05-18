<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" CodeBehind="AD_Department_WorkGroupManager.aspx.cs" Inherits="IAM.Admin.AD_Department_WorkGroupManager" %>
 <%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="pure-control-group" style="margin-top: 10px;" runat="server" id="nav_top">
        <label for="email" style="width: auto;">
            HR部门:</label>
        <asp:TextBox ID="txthrdepartment" runat="server"></asp:TextBox>

        <label for="email" style="width: 100px">AD部门:</label>
        <asp:TextBox ID="txtadepartment" runat="server"></asp:TextBox>
         <label for="email" style="width: 100px">中心:</label>
        <asp:TextBox ID="txtcenter" runat="server"></asp:TextBox>
        <label for="email" style="width: 100px">科室:</label>
        <asp:TextBox ID="txtkeshi" runat="server"></asp:TextBox>
         <br /><br />
         <label for="email" style="width: auto;">&nbsp;&nbsp;&nbsp;&nbsp;部门:</label>
        <asp:TextBox ID="txtDepartment" runat="server"></asp:TextBox>
         <input type="checkbox" runat="server" id="chkFalse" /><span>是否禁用</span><br /><br />
        &nbsp;&nbsp;<asp:Button ID="btnQuery" runat="server" Text="  查 询  " CssClass="pure-button pure-button-primary" OnClick="btnQuery_Click" />

        <input type="button" value="新建部门组" runat="server" id="btnCreate" class="pure-button pure-button-primary" onclick="javascript: OpenPage1('AD_Department_workgroupcreate.aspx');" />
    </div>
    <div style="margin-top: 10px;width:850px;">
        <table class="pure-table pure-table-bordered" style="width:950px;table-layout:fixed;text-align: center; text-decoration: none; font-size: 14px;">
            <thead>
                <tr align="center">
                    <th style="width: 100px">HR部门
                    </th>
                    <th style="width: 100px">Ad部门
                    </th>
                    <th style="width: 75px">中心
                    </th>
                    <th style="width: 100px">部门
                    </th>
                    <th style="width: 100px">科室
                    </th>
                    <th style="width: 100px">邮件数据库
                    </th>
                    <th style="width: 100px">状态
                    </th>
                    <th style="width:50px;">排序</th>
                    <th align="center" style="width: 150px">操作
                    </th>
                </tr>
            </thead>
            
        </table>
    </div>
    <div style="margin: 0px;width:1102px;overflow-x:hidden;overflow-y:auto;height:410px;">
         <table class="pure-table pure-table-bordered" style="width:950px;table-layout:fixed; text-align: center; text-decoration: none; font-size: 12px;">
             <tbody>
                <asp:Repeater ID="repeaterADDepartmentWorkgroup" runat="server" OnItemCommand="repeaterADDepartmentWorkgroup_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td style="background: #efefef;width:100px;">
                                <%#Eval("HrDepartment") %>
                            </td>
                            <td style="background: #efefef;width:100px;">
                                <%#Eval("AdDepartment") %>
                            </td>
                            <td style="background: #efefef;width:75px;">
                                <%#Eval("Center") %>
                            </td>
                            <td style="background: #efefef;width:100px;">
                                <%#Eval("Department") %>
                            </td>
                            <td style="background: #efefef;width:100px;">
                                <%#Eval("KeShi") %>
                            </td>
                            <td style="background: #efefef;width:100px;">
                                <%#Eval("EmailDataBase") %>
                            </td>
                             <td style="background: #efefef;width:100px;">
                                <%#Eval("p1").ToString().ToUpper().Equals("FALSE")?"启用":"禁用" %>
                            </td>
                            <td style="width:50px;background: #efefef;"><%#Eval("ordercolumn") %></td>
                            <td style="background: #efefef;width:150px;text-align:right;">
                                <asp:Button ID="btnup" runat="server" Text="" CssClass="btnup" CommandArgument='<%#Eval("ID") %>' CommandName="up" />
                                <asp:Button ID="btndown" runat="server" Text="" CssClass="btndown" CommandArgument='<%#Eval("ID") %>' CommandName="down"/>
                               <input type="button" value="编辑" style="margin-right:5px;" onclick="OpenPage1('ad_department_workgroupcreate.aspx?id=<%#Eval("ID")%>    ');"  />
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr>
                            <td style="background: white;width:100px;">
                                <%#Eval("HrDepartment") %>
                            </td>
                            <td style="background: white;width:100px;">
                                <%#Eval("AdDepartment") %>
                            </td>
                            <td style="background: white;width:75px;">
                                <%#Eval("Center") %>
                            </td>
                            <td style="background: white;width:100px;">
                                <%#Eval("Department") %>
                            </td>
                            <td style="background: white;width:100px;">
                                <%#Eval("KeShi") %>
                            </td>
                            <td style="background: white;width:100px;">
                                <%#Eval("EmailDataBase") %>
                            </td>
                             <td style="background: #fff;width:100px;">
                                <%#Eval("p1").ToString().ToUpper().Equals("FALSE")?"启用":"禁用" %>
                            </td>
                             <td style="width:50px;background: #fff;"><%#Eval("ordercolumn") %></td>
                            <td style="background: white;width:150px;text-align:right;">
                                <asp:Button ID="btnup" runat="server" Text="" CssClass="btnup" CommandArgument='<%#Eval("ID") %>' CommandName="up" />
                                <asp:Button ID="btndown" runat="server" Text="" CssClass="btndown" CommandArgument='<%#Eval("ID") %>' CommandName="down"/>
                                <input type="button" value="编辑" onclick="OpenPage1('ad_department_workgroupcreate.aspx?id=<%#Eval("ID")%>    ');" style="margin-right:5px;" /></td>
                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>


            </tbody>
             </table>
        <div class="pure-control-group" style="margin-top: 10px;">
            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" HorizontalAlign="Center" CssClass="paginator" CurrentPageButtonClass="cpb" FirstPageText="首页"
                    LastPageText="尾页" PrevPageText="上一页" NextPageText="下一页" NumericButtonTextFormatString="[{0}]"
                    ShowCustomInfoSection="Left" ShowBoxThreshold="2" PageSize="5"
                    CustomInfoSectionWidth="150px" InputBoxClass="text2" TextAfterInputBox="" OnPageChanging="AspNetPager1_PageChanging" />
        </div>
    </div>
</asp:Content>
