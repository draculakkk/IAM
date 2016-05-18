<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HrSmUserManager.aspx.cs" Inherits="IAM.Admin.HrSmUserManager" MasterPageFile="~/Admin/master.Master" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Control/TransferControl.ascx" TagName="UserTransfer" TagPrefix="UC1" %>
<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
    <script src="../Scripts/ScrollTopFunction.js" type="text/javascript"></script>
    <script type="text/javascript">
        jQuery(document).ready(function () {

            var windowheight=$(window).height();            
            var filterheight=$("#tablefilter").css("height");
            var tabletitle=$("#divtitle").css("height");
            var contextheight=windowheight-parseInt(filterheight.replace("px",""))-parseInt(tabletitle.replace("px",""));
            contextheight=contextheight-30;
            $("#divcontext").css("height",contextheight+"px");

            jQuery("#tablelist tbody>tr:odd").css("background", "#efefef");
            jQuery("#tablelist tbody>tr:even").css("background", "#fff");
            var isadmin = '<%=IsAdmin.ToString()%>';
            jQuery("input[type=button]").each(function () {
                var athis = this;               
                if ($(athis).prop("value") == "转移" && isadmin == "False")
                    $(athis).css("display","none");
            });
        });

    </script>
</asp:Content>
<asp:Content ID="body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <UC1:UserTransfer ID="userTransfer1" SystemType="HR" runat="server" />
    <div class="pure-control-group" style="margin-top: 10px;" runat="server" id="nav_top">
        <table id="tablefilter">
            <tr>
                <td class="td-title-width">工号:
                </td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtgonghao" runat="server"></asp:TextBox>
                </td>
                <td class="td-title-width">姓名:
                </td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtname" runat="server"></asp:TextBox>
                </td>
                <td class="td-title-width">部门:
                </td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtdepartment" runat="server"></asp:TextBox>
                </td>
                <td class="td-title-width">岗位:
                </td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtgangwei" runat="server"></asp:TextBox>
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="td-title-width">HR账号: 
                </td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                </td>
                <td class="td-title-width">类型:
                </td>
                <td class="td-context-width">
                    <asp:DropDownList ID="dplType" Width="150px" runat="server">
                        <asp:ListItem Value="" Text="全部"></asp:ListItem>
                        <asp:ListItem Value="员工" Text="员工"></asp:ListItem>
                        <asp:ListItem Value="其他" Text="其他"></asp:ListItem>
                        <asp:ListItem Value="系统" Text="系统"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="td-title-width">登录方式:
                </td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtLoginType" runat="server"></asp:TextBox>
                </td>

                <td class="td-title-width">是否锁定:
                </td>
                <td class="td-context-width">
                    <asp:DropDownList ID="dplLock" Width="150px" runat="server">
                        <asp:ListItem Value="" Text="全部"></asp:ListItem>
                        <asp:ListItem Value="N" Text="否"></asp:ListItem>
                        <asp:ListItem Value="Y" Text="是"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="td-context-width"></td>

            </tr>
            <tr>
                <td colspan="8" style="height: 35px; padding-left: 5px; vertical-align: middle;">
                    <asp:Button ID="btnSearch" runat="server" Text="查询"
                        CssClass="pure-button pure-button-primary" OnClick="btnSearch_Click" />&nbsp;&nbsp;
                    <input type="button" runat="server" id="inputAddNew" value="新建用户" class="pure-button pure-button-primary" onclick="javascript:OpenPage('HREmployeeCreate.aspx');" />
                </td>
            </tr>
        </table>
    </div>

    <div id="divtitle" style="margin: 0px; padding: 0px; width: 1020px;">
        <table class="pure-table pure-table-bordered" style="text-align: center; text-decoration: none; font-size: 14px; width: 1020px; table-layout: fixed">
            <thead>
                <tr align="center">
                    <th style="width: 75px;">工号
                    </th>
                    <th style="width: 100px;">姓名
                    </th>
                    <th style="width: 100px;">部门
                    </th>
                    <th style="width: 100px;">账号
                    </th>
                    <th style="width: 75px;">类型
                    </th>
                    <th style="width: 100px;">生效时间
                    </th>
                    <th style="width: 100px;">失效时间
                    </th>
                    <th style="width: 100px;">锁定标志
                    </th>
                    <th style="width: 120px;">登录认证方式
                    </th>
                    <th style="width: 150px;">操作
                    </th>
                </tr>
            </thead>

        </table>
    </div>
    <div id="divcontext" style="margin: 0px; padding: 0px; width: 1270px; height: 460px; overflow-y: auto; overflow-x: hidden">
        <table id="tablelist" class="pure-table pure-table-bordered" style="text-align: center; text-decoration: none; font-size: 12px; width: 1020px; table-layout: fixed;">
            <tbody>
                <asp:Repeater ID="repeater1" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="width: 75px;">
                                <a href="#" onclick="javascript:OpenEmployeeinfo('<%#Eval("bgonghao")%>');"><%#Eval("bgonghao")%></a>
                            </td>
                            <td style="width: 100px;">
                                <%#Eval("aUser_name") %>
                            </td>
                            <td style="width: 100px;">
                                <%#Eval("dname")%>
                            </td>
                            <td style="width: 100px;">
                                <%#Eval("aUser_code")%>
                            </td>
                            <td style="width: 75px;">
                                <%#Eval("bUserType")%>
                            </td>
                            <td style="width: 100px;">
                                <%#Eval("aAble_time")%>
                            </td>
                            <td style="width: 100px;">
                                <%#Eval("aDisable_time")%>
                            </td>
                            <td style="width: 100px;">
                                <%#Eval("aLocked_tag").ToString().Equals("Y")?"是":"否"%>
                            </td>
                            <td style="width: 120px;">
                                <%#Eval("aAuthen_type")==null?"未知":Eval("aAuthen_type").ToString().Equals("staticpwd")?"静态密码认证":Eval("aAuthen_type").ToString().Equals("ncca")?"CA认证":"未知"%>
                            </td>
                            <td style="width: 150px; text-align: right;">

                                <%#base.ReturnUserRole.Admin==false?"": Eval("bUserType")==null?"<input type=\"button\" value=\"转移\" onclick='OpenTransfer(\""+Eval("bid")+"\");'/>":Eval("bUserType").Equals("员工")?"":"<input type=\"button\" value=\"转移\" onclick='OpenTransfer(\""+Eval("bid")+"\");'/>" %>
                                <input type="button" value="<%#base.ReturnUserRole.Admin==false?"详情":"编辑" %>" onclick="javascript: OpenPage('HREmployeeCreate.aspx?id=<%#Eval("aCuserid")%>    ');" style="margin-right: 7px;" />

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
