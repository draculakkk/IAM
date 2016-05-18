<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true"
    CodeBehind="HECUserInfoManager.aspx.cs" Inherits="IAM.Admin.HECUserInfoManager" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Control/TransferControl.ascx" TagName="UserTransfer" TagPrefix="UC1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Content/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        jQuery(document).ready(function () {

            var windowheight=$(window).height();            
            var filterheight=$("#tablefilter").css("height");
            var tabletitle=$("#divtitle").css("height");
            var contextheight=windowheight-parseInt(filterheight.replace("px",""))-parseInt(tabletitle.replace("px",""));
            contextheight=contextheight-30;
            $("#divcontext").css("height",contextheight+"px");

            jQuery("#tablelist tbody>tr:odd>td").css("background", "#EFEFEF");
            jQuery("#tablelist tbody>tr:even>td").css("background", "#FFF");
            var isadmin = '<%=IsAdmin.ToString()%>';
            jQuery("input[type=button]").each(function () {
                var athis = this;               
                if ($(athis).prop("value") == "转移" && isadmin == "False")
                    $(athis).css("display","none");
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <UC1:UserTransfer ID="userTransfer1" SystemType="HEC" runat="server" />
    <div class="pure-control-group" style="margin-top: 10px;" runat="server" id="nav_top">
        <table id="tablefilter">
            <tr>
                <td class="td-title-width">工号:</td>
                <td class="td-context-width">

                    <asp:TextBox ID="txtgonghao" runat="server"></asp:TextBox>

                </td>
                <td class="td-title-width">姓名:</td>
                <td class="td-context-width">

                    <asp:TextBox ID="txtname" runat="server"></asp:TextBox>

                </td>
                <td class="td-title-width">部门:</td>
                <td class="td-context-width">

                    <asp:TextBox ID="txtdepartment" runat="server"></asp:TextBox>

                </td>
                <td class="td-title-width">岗位:</td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtgangwei" runat="server"></asp:TextBox>
                </td>

            </tr>
            <tr>
                <td class="td-title-width">Hec账号:</td>
                <td class="td-context-width">

                    <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>

                </td>
                <td class="td-title-width">类型:
                </td>
                <td class="td-context-width">
                    <asp:DropDownList ID="dpltype" Width="150px" runat="server">
                        <asp:ListItem Value="" Text="全部"></asp:ListItem>
                        <asp:ListItem Value="员工" Text="员工"></asp:ListItem>
                        <asp:ListItem Value="其他" Text="其他"></asp:ListItem>
                        <asp:ListItem Value="系统" Text="系统"></asp:ListItem>
                    </asp:DropDownList>
                </td>

                <td class="td-title-width">禁用时间从:</td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtStartdate" runat="server" class="Wdate" onClick="WdatePicker()"></asp:TextBox>
                </td>
                <td class="td-title-width" align="center">至
                </td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtEndDate" runat="server" onClick="WdatePicker()" class="Wdate"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <%--<td class="td-title-width">是否禁用: 
                </td>--%>
                <td class="td-context-width">
                    <asp:DropDownList ID="dpljinyong" Visible="false" Width="150px" runat="server">
                        <asp:ListItem Value="" Text="全部"></asp:ListItem>
                        <asp:ListItem Value="1" Text="是"></asp:ListItem>
                        <asp:ListItem Value="0" Text="否"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="td-context-width" colspan="2"></td>
            </tr>
            <tr>
                <td colspan="8" style="height: 35px; vertical-align: middle; padding-left: 5px;">
                    <asp:Button ID="btnQuery" runat="server" Text=" 查 询 " CssClass="pure-button pure-button-primary"
                        OnClick="btnQuery_Click" />
                    &nbsp;&nbsp;
        <input type="button" runat="server" id="inputAddNew" value="新建用户" onclick="OpenPage('HECUserInfoCreate.aspx');" class="pure-button pure-button-primary" />
                </td>
            </tr>
        </table>
    </div>

    <div id="divtitle" style="margin-top: 10px; width: 950px;">
        <table class="pure-table pure-table-bordered" style="text-align: center; text-decoration: none; font-size: 14px; width: 950px; table-layout: fixed;">
            <thead>
                <tr align="center">
                    <th style="width: 100px">工号
                    </th>

                    <th style="width: 100px">姓名
                    </th>
                    <th style="width: 100px">部门
                    </th>
                    <th style="width: 100px">账号
                    </th>
                    <th style="width: 100px">账号类型
                    </th>
                    <th style="width: 100px">启用</th>
                    <th style="width: 100px">有效期从
                    </th>
                    <th style="width: 100px">有效期至
                    </th>
                    <th style="width: 150px">操作
                    </th>
                </tr>
            </thead>

        </table>
    </div>
    <div id="divcontext" style="margin: 0px; padding: 0px; width: 1176px; overflow-x: hidden; overflow-y: auto; height: 435px;">
        <table class="pure-table pure-table-bordered" id="tablelist" style="text-align: center; text-decoration: none; font-size: 12px; width: 950px; table-layout: fixed;">
            <tbody>
                <asp:Repeater ID="repeater1HECUserInfo" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="width: 100px;">
                                <a href="#" onclick="javascript:OpenEmployeeinfo('<%#Eval("gonghao") %>');"><%#Eval("gonghao") %></a>
                            </td>
                            <td style="width: 100px;">

                                <%#Eval("employeename")%>
                            </td>
                            <td style="width: 100px;">
                                <%#Eval("department")%>
                            </td>

                            <td style="width: 100px;">
                                <%#Eval("USER_CD")%>
                                               
                            </td>
                            <td style="width: 100px;"><%#Eval("bUserType") %></td>
                            <td style="width: 100px"><%#Eval("ISDISABLED").ToString().Equals("0")?"启用":"禁用" %></td>
                            <td style="width: 100px;">
                                <%#Eval("START_DATE") == null ? "" : Convert.ToDateTime(Eval("START_DATE").ToString()).ToString("yyyy-MM-dd")%>
                            </td>
                            <td style="width: 100px;">
                                <%#Eval("END_DATE") == null ? "" : Convert.ToDateTime(Eval("END_DATE").ToString()).ToString("yyyy-MM-dd")%>
                            </td>
                            <td style="width: 150px; text-align: right;">
                                <%# base.ReturnUserRole.Admin==false?"": Eval("bUserType")==null?"<input type=\"button\" value=\"转移\" onclick='OpenTransfer(\""+Eval("bid")+"\");'/>":Eval("bUserType").Equals("员工")?"":"<input type=\"button\" value=\"转移\" onclick='OpenTransfer(\""+Eval("bid")+"\");'/>" %>
                                <input type="button" value="<%#base.ReturnUserRole.Admin==false?"详情":"编辑" %>" onclick="OpenPage('HECUserInfoCreate.aspx?usercd=<%#Eval("USER_CD") %>    ');" style="margin-right: 7px;" />

                            </td>
                        </tr>
                    </ItemTemplate>

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
