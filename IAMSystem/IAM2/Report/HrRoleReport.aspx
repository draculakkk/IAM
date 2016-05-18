<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="false" CodeBehind="HrRoleReport.aspx.cs" ValidateRequest="false" Inherits="IAM.Report.HrRoleReport" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/ScrollTopFunction.js" type="text/javascript"></script>
    <script type="text/javascript">
        jQuery(document).ready(function () {

            var windowheight = $(window).height();
            var filterheight = $("#tablefilter").css("height");
            var tabletitle = $("#divtitle").css("height");
            var contextheight = windowheight - parseInt(filterheight.replace("px", "")) - parseInt(tabletitle.replace("px", ""));
            contextheight = contextheight - 30;
            $("#divcontext").css("height", contextheight + "px");

            jQuery("#testfff tbody>tr>td").css({ background: "#fff", height: "25px" });

            var _gonghao = jQuery("#<%=txtgonghao.ClientID%>").val();
            var _name = jQuery("#<%=txtname.ClientID%>").val();
            var _dept = jQuery("#<%=txtdepartment.ClientID%>").val();
            var _hrusername = jQuery("#<%=txtLoginType.ClientID%>").val();
            var _rolename = jQuery("#<%=txtrolename.ClientID%>").val();
            var _companyname = jQuery("#<%=txtCompanyName.ClientID%>").val();
            var _logintype = jQuery("#<%=txtLoginType.ClientID%>").val();
            var _islock = jQuery("#<%=dplLock.ClientID%>").val();
            var _gangwei = jQuery("#<%=txtgangwei.ClientID%>").val();
            var _leixing = jQuery("#<%=dplType.ClientID%>").val();
            jQuery("#btnExcelExport").bind("click", function () {
                jQuery.post("../ExcelExportAjax.ashx?type=hrrole",
                    {
                        gonghao: _gonghao, name: _name, department: _dept,
                        hrusername: _hrusername, logintype: _logintype, rolename: _rolename,
                        companyname: _companyname, islock: _islock, gangwei: _gangwei, leixing: _leixing
                    }, function (data) {
                    if (data.indexOf("error") == -1) {
                        window.location.href = "../downloadFile/" + data;
                    }
                    else {
                        alert(data);
                    }
                }, "text");
            });

            //mergeTable("testfff", 0, 1);
            //mergeTableExt("testfff", 0, 1, 0);

            //menuFixed("ContentPlaceHolder1_nav_top");

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                
            </tr>
            <tr>
                <td class="td-title-width">HR账号: 
                </td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                </td>
                <td class="td-title-width">
                    类型:
                </td>
                <td class="td-context-width">
                    <asp:DropDownList ID="dplType" Width="150px"  runat="server">
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
                <td class="td-title-width">角色名称:
                </td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtrolename" runat="server"></asp:TextBox>
                </td>
                
                

            </tr>
            <tr>
                <td class="td-title-width">公司名称:
                </td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtCompanyName" runat="server"></asp:TextBox>
                </td>
                <td class="td-title-width">
                    锁定标志
                </td>
                <td class="td-context-width">
                     <asp:DropDownList ID="dplLock" Width="150px" runat="server">
                        <asp:ListItem Value="" Text="全部"></asp:ListItem>
                        <asp:ListItem Value="N" Text="否"></asp:ListItem>
                        <asp:ListItem Value="Y" Text="是"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="td-context-width" colspan="4">
                    
                </td>
            </tr>
            <tr>
                <td colspan="8" style="padding-left:5px; vertical-align:middle;height:35px;">
<asp:Button ID="btnQuery" runat="server" Text=" 查 询 "
                        CssClass="pure-button pure-button-primary" OnClick="btnQuery_Click" />
                    &nbsp;&nbsp;
        <input type="button" class="pure-button pure-button-primary" value=" 导 出 " id="btnExcelExport" />
                </td>
            </tr>
        </table>


    </div>


    <div id="divtitle" style="margin: 0px; padding: 0px; width: 1000px;">
        <table class="pure-table pure-table-bordered" style="text-align: center; text-decoration: none; font-size: 14px; width: 1600px; table-layout: fixed;">
            <thead>
                <tr align="center">
                    <th style="width: 75px">工号</th>
                    
                    <th style="width: 150px">姓名</th>
                    <th style="width: 150px">部门</th>
                    <th style="width: 150px">角色
                    </th>
                    <th style="width: 150px">角色名称
                    </th>
                    <th style="width: 150px">账号
                    </th>
                    <th style="width: 75px">类型
                    </th>
                    <th style="width: 250px">公司名称
                    </th>
                    <th style="width: 150px">锁定标记
                    </th>
                    <th style="width: 150px">登录方式
                    </th>
                </tr>
            </thead>

        </table>
    </div>
    <div id="divcontext" style="margin: 0px; padding: 0px; width: 1700px; overflow-x: hidden; overflow-y: auto; height: 400px;">
        <table class="pure-table pure-table-bordered" id="testfff" style="text-align: center; text-decoration: none; font-size: 14px; width: 1600px; table-layout: fixed;">
            <tbody>
                <asp:Repeater ID="repeaterHRUserRole" runat="server">
                    <ItemTemplate>
                        <tr>
                             <td style="width: 75px"><a href="#" onclick="javascript:OpenEmployeeinfo('<%#Eval("mgonghao") %>');"><%#Eval("mgonghao") %></a></td>
                            
                            <td style="width: 150px"><%#Eval("ename") %></td>
                            <td style="width: 150px"><%#Eval("dname") %></td>
                            <td style="width: 150px">
                                <%#Eval("hrrRoleCode")%>
                            </td>


                            <td style="width: 150px">
                                <%#Eval("role_name")%>
                            </td>
                            <td style="width: 150px">
                                <%#Eval("hrusUser_code")%>
                            </td>
                            <td style="width: 75px">
                                <%#Eval("mUserType")%>
                            </td>
                            <td style="width: 250px">
                                <%#Eval("CompanyName")%>
                            </td>
                            <td style="width: 150px"><%#Eval("Locked_tag").ToString().Equals("N")?"否":"是" %></td>
                            <td style="width: 150px"><%#Eval("Authen_type") %></td>
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
