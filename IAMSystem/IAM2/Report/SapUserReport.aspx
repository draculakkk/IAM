<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" CodeBehind="SapUserReport.aspx.cs" Inherits="IAM.Report.SapUserReport" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Content/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">

        jQuery(document).ready(function () {

            jQuery("#testfff tbody>tr>td").css({ background: "#fff" });
            jQuery("#btnExcelExport").bind("click", function () {
                var gonghao = jQuery("#<%=txtgonghao.ClientID%>").val();
                var name = jQuery("#<%=txtname.ClientID%>").val();
                var department = jQuery("#<%=txtdepartment.ClientID%>").val();
                var gangwei = jQuery("#<%=txtgangwei.ClientID%>").val();
                var sapname = jQuery("#<%=txtusername.ClientID%>").val();
                var leixing = jQuery("#<%=dplleixing.ClientID%>").val();
                var startdates = jQuery("#<%=txtStartDates.ClientID%>").val();
                var enddates = jQuery("#<%=txtEndDatee.ClientID%>").val();
                var startdatee = jQuery("#<%=txtStartDatee.ClientID%>").val();
                var enddatee = jQuery("#<%=txtEndDates.ClientID%>").val();
                var roleid = jQuery("#<%=txtRoleID.ClientID%>").val();
                var rolename = jQuery("#<%=txtRoleName.ClientID%>").val();
                var userType = jQuery("#<%=dplUserType.ClientID%>").val();
                jQuery.post("../ExcelExportAjax.ashx?type=sapuser",
                    {
                        gonghao: gonghao,
                        name: name,
                        department: department,
                        gangwei: gangwei,
                        sapname: sapname,
                        leixing: leixing,
                        startdates: startdates,
                        enddates: enddates,
                        startdatee: startdatee,
                        enddatee: enddatee,
                        roleid: roleid,
                        rolename: rolename,
                        userType: userType
                    },
                    function (data) {
                        if (data.indexOf("error") == -1) {
                            window.location.href = "../downloadFile/" + data;
                        }
                        else
                            alert(data);
                    }, "text");
                mergeTable("testfff", 0, 0);
                mergeTableExt("testfff", 0, 1, 0);
                // menuFixed("ContentPlaceHolder1_nav_top");
            })
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pure-control-group" style="margin-top: 10px;" runat="server" id="nav_top">
        <table>
            <tr>
                <td class="td-title-width">工号:</td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtgonghao" runat="server"></asp:TextBox></td>
                <td class="td-title-width">姓名:</td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtname" runat="server"></asp:TextBox></td>
                <td class="td-title-width">部门:</td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtdepartment" runat="server"></asp:TextBox></td>
                <td class="td-title-width">岗位:</td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtgangwei" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="td-title-width">账号:</td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtusername" runat="server"></asp:TextBox></td>
                <td class="td-title-width">类型:</td>
                <td class="td-context-width">
                    <asp:DropDownList ID="dplleixing" Width="150" runat="server">
                        <asp:ListItem Value="" Text="全部"></asp:ListItem>
                        <asp:ListItem Value="员工" Text="员工"></asp:ListItem>
                        <asp:ListItem Value="其他" Text="其他"></asp:ListItem>
                        <asp:ListItem Value="系统" Text="系统"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="td-title-width">SAP用户类型:</td>
                <td class="td-context-width">
                    <asp:DropDownList ID="dplUserType" Width="150px" runat="server">
                        <asp:ListItem Value="" Text="全部"></asp:ListItem>
                        <asp:ListItem Value="A" Text="对话"></asp:ListItem>
                        <asp:ListItem Value="B" Text="系统用户(内部RFC和后台处理)"></asp:ListItem>
                        <asp:ListItem Value="C" Text="通讯用户(外部RFC)"></asp:ListItem>
                        <asp:ListItem Value="L" Text="参考用户"></asp:ListItem>
                        <asp:ListItem Value="S" Text="服务用户"></asp:ListItem>
                    </asp:DropDownList>

                </td>
                <td class="td-title-width">角色ID:</td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtRoleID" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="td-title-width">角色名称:</td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtRoleName" runat="server"></asp:TextBox></td>
                <td class="td-title-width">起始有效期从:</td>
                <td class="td-context-width" colspan="2">
                    <asp:TextBox ID="txtStartDates" Width="100" runat="server" onClick="WdatePicker()" class="Wdate"></asp:TextBox>
                    至
        <asp:TextBox ID="txtEndDates" runat="server" Width="100" onClick="WdatePicker()" class="Wdate"></asp:TextBox>
                </td>
                 <td class="td-title-width">截至有效期从:</td>
                <td class="td-context-width" colspan="2">
                    <asp:TextBox ID="txtStartDatee" Width="100" runat="server" onClick="WdatePicker()" class="Wdate"></asp:TextBox>
                    至
        <asp:TextBox ID="txtEndDatee" runat="server" Width="100" onClick="WdatePicker()" class="Wdate"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td-context-width" colspan="8">
                    <asp:Button ID="btnQuery" runat="server" Text=" 查 询 "
                        CssClass="pure-button pure-button-primary" OnClick="btnQuery_Click" />
                    &nbsp;&nbsp;
        <input type="button" class="pure-button pure-button-primary" value=" 导 出 " id="btnExcelExport" />
                </td>
            </tr>
        </table>
    </div>
    <fieldset>
        <div style="width: 900px; margin-top: 10px;">
            <table class="pure-table pure-table-bordered" style="text-align: center; text-decoration: none; font-size: 14px; width: 900px; table-layout: fixed;">
                <thead>
                    <tr align="center">

                        <th style="width: 150px">工号
                        </th>
                        <th style="width: 125px">姓名
                        </th>
                        <th style="width: 125px;">部门</th>
                        <th style="width: 125px;">SAP账号</th>
                        <th style="width: 75px;">类型</th>
                        <th style="width: 150px">角色
                        </th>
                        <th style="width: 150px">角色名
                        </th>
                        <th style="width: 150px">有效期从
                        </th>
                        <th style="width: 150px">有效期至
                        </th>
                    </tr>
                </thead>

            </table>
        </div>

        <div style="width: 1425px; margin: 0px; height: 350px; overflow-x: hidden; overflow-y: auto;">
            <table class="pure-table pure-table-bordered" id="testfff" style="text-align: center; text-decoration: none; font-size: 12px; width: 1350px; table-layout: fixed;">
                <tbody>
                    <asp:Repeater ID="repeater1SAPUserrole" runat="server">
                        <ItemTemplate>
                            <tr>

                                <td style="width: 150px">
                                    <a href="#" onclick="javascript:OpenEmployeeinfo('<%#Eval("mgonghao") %>');"><%#Eval("mgonghao") %></a>
                                </td>
                                <td style="width: 125px">
                                    <%#Eval("ename")%> 
                                </td>
                                <td style="width: 125px;"><%#Eval("dname") %></td>
                                <td style="width: 125px;"><%#Eval("uBAPIBNAME") %></td>
                                <td style="width: 75px">
                                    <%#Eval("mUserType")%> 
                                </td>
                                <td style="width: 150px">
                                    <%#Eval("urRoleID")%>
                                </td>
                                <td style="width: 150px">
                                    <%#Eval("urRoleNAME")%>
                                </td>
                                <td style="width: 150px">
                                    <%#Eval("urStartDate")%>
                                </td>
                                <td style="width: 150px">
                                    <%#Eval("urEndDate")%>
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
                CustomInfoSectionWidth="150px" InputBoxClass="text2" TextAfterInputBox="" OnPageChanging="AspNetPager1_PageChanging"/>
            </div>
        </div>
    </fieldset>
</asp:Content>
