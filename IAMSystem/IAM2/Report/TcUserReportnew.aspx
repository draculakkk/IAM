<%@ Page Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" CodeBehind="TcUserReportnew.aspx.cs" Inherits="IAM.Report.TcUserReportnew" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    
     <script src="../Content/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
       <script type="text/javascript">
           jQuery(document).ready(function () {

               var windowheight = $(window).height();
               var filterheight = $("#tablefilter").css("height");
               var tabletitle = $("#divtitle").css("height");
               var contextheight = windowheight - parseInt(filterheight.replace("px", "")) - parseInt(tabletitle.replace("px", ""));
               contextheight = contextheight - 30;
               $("#divcontext").css("height", contextheight + "px");

               jQuery("#btnExcelExport").bind("click", function () {
                   var gonghao = jQuery("#<%=txtgonghao.ClientID%>").val();
                   var Name = jQuery("#<%=txtname.ClientID%>").val();
                   var department = jQuery("#<%=txtdepartment.ClientID%>").val();
                   var UserName = jQuery("#<%=txtTCusername.ClientID%>").val();
                   var xukejibie = jQuery("#<%=ddlxukejibi.ClientID%>").val();
                   var userStatus = jQuery("#<%=ddlUserStatus.ClientID%>").val();
                   var leixing = jQuery("#<%=dpltype.ClientID%>").val();
                   var groupname = jQuery("#<%=txtGroupName.ClientID%>").val();
                   var rolename = jQuery("#<%=txtRoleName.ClientID%>").val();
                   var gangwei = jQuery("#<%=txtgangwei.ClientID%>").val();
                   var juesejinyong = jQuery("<%=groupjinyong.ClientID%>").val();
                   jQuery.post("../ExcelExportAjax.ashx?type=tcuser", {
                       gonghao: gonghao,
                       Name: Name,
                       department: department,
                       gangwei: gangwei,
                       UserName: UserName,
                       xukejibie: xukejibie,
                       userStatus: userStatus,
                       leixing: leixing,
                       groupname: groupname,
                       rolename: rolename,
                       juesejinyong: juesejinyong
                   }, function (data) {
                    if (data.indexOf("error") == -1) {
                        window.location.href = "../downloadFile/" + data;
                    }
                    else {
                        alert(data);
                    }
                }, "text");
            });
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
                    <asp:TextBox ID="txtgonghao" Width="150px" runat="server"></asp:TextBox>
                </td>
                <td class="td-title-width">姓名:
                </td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtname" runat="server" Width="150px"></asp:TextBox>
                </td>
                <td class="td-title-width">部门:
                </td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtdepartment" Width="150px" runat="server"></asp:TextBox>
                </td>
                <td class="td-title-width">岗位:
                </td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtgangwei" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td-title-width">TC账号:
                </td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtTCusername" runat="server" Width="150px"></asp:TextBox>
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
                <td class="td-title-width">许可级别:
                </td>
                <td class="td-context-width">
                    <asp:DropDownList ID="ddlxukejibi" Width="150px" runat="server">
                        <asp:ListItem Text="客户" Value="0"></asp:ListItem>
                        <asp:ListItem Text="作者" Value="1"></asp:ListItem>
                        <asp:ListItem Text="全部" Value="" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="td-title-width">用户状态: 
                </td>
                <td class="td-context-width">
                    <asp:DropDownList ID="ddlUserStatus" Width="150px" runat="server">
                        <asp:ListItem Text="非活动" Value="1"></asp:ListItem>
                        <asp:ListItem Text="活动" Value="0"></asp:ListItem>
                        <asp:ListItem Text="全部" Value="" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="td-title-width">组名称:
                </td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtGroupName" Width="150px" runat="server"></asp:TextBox>
                </td>
                <td class="td-title-width">角色名称:
                </td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtRoleName" Width="150px" runat="server"></asp:TextBox>
                </td>
                <td class="td-title-width">角色状态: 
                </td>
                <td class="td-context-width">
                    <asp:DropDownList ID="groupjinyong" Width="150px" runat="server">
                        <asp:ListItem Text="非活动" Value="0"></asp:ListItem>
                        <asp:ListItem Text="活动" Value="1"></asp:ListItem>
                        <asp:ListItem Text="全部" Value="" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="3" class="td-context-width">
                    
                </td>
            </tr>
            <tr>
                <td colspan="8" style="height:35px;vertical-align:middle;padding-left:5px;">
<asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="pure-button pure-button-primary" OnClick="btnQuery_Click" />
                    &nbsp;
        <input type="button" value="导出" id="btnExcelExport" class="pure-button pure-button-primary" />
                </td>
            </tr>
        </table>
    </div>

        <div id="divtitle" style="margin-top: 10px; margin-bottom: 0px;width:1180px;">
        <table border="0" class="pure-table pure-table-bordered" cellspacing="1" cellpadding="1" style="text-align: center; text-decoration: none; font-size: 13px; width: 1180px;table-layout:fixed">
            <thead>
                <tr align="center">
                    
                    <th style="width: 75px">工号
                    </th>
                    <th style="width: 100px">姓名
                    </th>
                     <th style="width: 225px">部门
                    </th>
                    <th style="width: 100px">TC账号
                    </th>
                    <th style="width: 75px">类型
                    </th>
                   
                    
                    <th style="width: 75px">许可级别
                    </th>
                    <th style="width: 70px">用户状态
                    </th>
                    <th style="width: 130px">最后登录时间
                    </th>
                    <th style="width: 125px">组名称</th>
                    <th style="width: 125px">角色名称</th>
                    <th style="width:70px;">角色状态</th>
                </tr>
            </thead>
        </table>
        </div>
        <div id="divcontext"  style="padding: 0px; margin: 0px; overflow-x: hidden; overflow-y: auto; height: 400px; width: 1447px;">
        <table class="pure-table pure-table-bordered" border="0" cellspacing="1" cellpadding="1" style="text-align: center; width: 1180px;font-size:13px; table-layout:fixed ">
           
                <asp:Repeater ID="repeaterTCUserInfo" runat="server">
                    <ItemTemplate>
                                                <tr>
                            
                            <td style="width: 75px;">
                               <a href="#" onclick="javascript:OpenEmployeeinfo('<%#Eval("mgonghao") %>');"><%#Eval("mgonghao") %></a> 
                            </td>
                            <td style="width: 100px;">
                                <%#Eval("uUserName") %>
                            </td>
                                                    <td style="width:225px;">
                            <%#Eval("dname") %>
                            </td>
                            <td style="width: 100px;">
                                <%#Eval("uUserID") %>
                            </td>
                            <td style="width: 75px"><%#Eval("mUserType") %>
                    </td>
                                
                            
                            <td style="width: 75px;"><%#Eval("uLicenseLevel").ToString().Equals("1")?"作者":Eval("uLicenseLevel").ToString().Equals("0")?"客户":"其他" %>
                            </td>
                            <td style="width: 70px;">
                                    <%#Eval("uUserStatus").ToString().Equals("0")?"活动":"非活动" %>
                                </td>
                                <td style="width: 130px;">
                                   <%#Eval("uLastLoginTime") %>
                                </td>
<td style="width: 125px; "><%#IAM.BLL.Untityone.GetGroupName(Eval("urMemo").ToString()) %></td>
                            <td style="width: 125px;"><%#IAM.BLL.Untityone.GetRoleName(Eval("urMemo").ToString()) %></td>
                                                    <td style="width: 70px;">
<%#Eval("urGroupStatus").ToString().Equals("0")?"非活动":"活动" %>
                                                    </td>
                        </tr>

                        
                    </ItemTemplate>                     
                </asp:Repeater> 
        </table>
             <div class="pure-control-group" style="margin-top: 10px;">
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" HorizontalAlign="Center" CssClass="paginator" CurrentPageButtonClass="cpb" FirstPageText="首页"
                LastPageText="尾页" PrevPageText="上一页" NextPageText="下一页" NumericButtonTextFormatString="[{0}]"
                ShowCustomInfoSection="Left" ShowBoxThreshold="2" PageSize="5"                
                CustomInfoSectionWidth="150px" InputBoxClass="text2" TextAfterInputBox="" OnPageChanging="AspNetPager1_PageChanging" />
    </div>
    </div>
</asp:Content>

