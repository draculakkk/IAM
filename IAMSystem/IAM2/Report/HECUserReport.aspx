<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" CodeBehind="HECUserReport.aspx.cs" Inherits="IAM.Report.HECUserReport" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/ScrollTopFunction.js" type="text/javascript"></script>
    <script src="../Content/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        jQuery(document).ready(function () {

            var windowheight = $(window).height();
            var filterheight = $("#tablefilter").css("height");
            var tabletitle = $("#divtitle").css("height");
            var contextheight = windowheight - parseInt(filterheight.replace("px", "")) - parseInt(tabletitle.replace("px", ""));
            contextheight = contextheight - 30;
            $("#divcontext").css("height", contextheight + "px");

            menuFixed("ContentPlaceHolder1_nav_top");

            //jQuery("#testfff tbody>tr>td").css({background:"#fff",height:"25px"});

            jQuery("#btnExcelExport").bind("click", function () {
                var gonghao = jQuery("#<%=txtgonghao.ClientID%>").val();
                var name = jQuery("#<%=txtname.ClientID%>").val();
                var department = jQuery("#<%=txtdepartment.ClientID%>").val();
                var gangwei = jQuery("#<%=txtgangwei.ClientID%>").val();

                var hecname = jQuery("#<%=txtUserName.ClientID%>").val();
                var leixing = jQuery("#<%=dpltype.ClientID%>").val();
                var startdate = jQuery("#<%=txtStartDate.ClientID%>").val();
                var enddate = jQuery("#<%=txtEndDate.ClientID%>").val();

                var rolename = jQuery("#<%=txtrolename.ClientID%>").val();
                var companyname = jQuery("#<%=txtCompanyName.ClientID%>").val();
                var jinyong = jQuery("#<%=dpljinyong.ClientID%>").val();
                jQuery.post("../ExcelExportAjax.ashx?type=hecuser",
                    {
                        gonghao: gonghao,
                        name: name,
                        department: department,
                        gangwei: gangwei,
                        hecname: hecname,
                        leixing: leixing,
                        startdate: startdate,
                        enddate: enddate,
                        rolename: rolename,
                        companyname: companyname,
                        jinyong: jinyong
                    },
                    function (data) {
                        if (data.indexOf("error") == -1) {
                            window.location.href = "../downloadFile/" + data;
                        }
                        else {
                            alert(data);
                        }
                    }, "text");
            });
            // mergeTable("testfff", 0, 1);
            // mergeTableExt("testfff", 0, 1, 0);
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
                <td class="td-title-width">HEC账号: 
                </td>
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
                <td class="td-title-width">有效期从:
                </td>

                <td class="td-context-width" colspan="3">
                    <asp:TextBox ID="txtStartDate" runat="server" class="Wdate" onClick="WdatePicker()"></asp:TextBox>

                    至
               
                    <asp:TextBox ID="txtEndDate" runat="server" class="Wdate" onClick="WdatePicker()" Width="185px"></asp:TextBox>
                </td>
            </tr>
            <tr>

                <td class="td-title-width">角色名称:
                </td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtrolename" runat="server"></asp:TextBox>
                </td>
                <td class="td-title-width">公司名称:
                </td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtCompanyName" runat="server"></asp:TextBox>
                </td>
                <td class="td-title-width">是否禁用: 
                </td>
                <td class="td-context-width">
                    <asp:DropDownList ID="dpljinyong" Width="150px" runat="server">
                        <asp:ListItem Value="" Text="全部"></asp:ListItem>
                        <asp:ListItem Value="1" Text="是"></asp:ListItem>
                        <asp:ListItem Value="0" Text="否"></asp:ListItem>
                    </asp:DropDownList>
                </td>

                <td class="td-context-width" colspan="2"></td>
            </tr>
            <tr>
                <td colspan="8" style="padding-left: 5px; height: 35px; vertical-align: middle">
                    <asp:Button ID="btnQuery" runat="server" Text="查询"
                        CssClass="pure-button pure-button-primary" OnClick="btnQuery_Click" />
                    &nbsp;&nbsp;
        <input type="button" value="导出" class="pure-button pure-button-primary" id="btnExcelExport" />
                </td>
            </tr>
        </table>
    </div>



    <div id="divtitle" style="width: 1300px; margin-top: 10px;">

        <table class="pure-table pure-table-bordered" id="table3" style="text-align: center; text-decoration: none; font-size: 14px; width: 1300px; table-layout: fixed;">
            <thead>
                <tr align="center">
                    <th style="width: 75px">工号 
                    </th>
                    <th style="width: 150px">部门 
                    </th>
                    <th style="width: 150px">姓名 
                    </th>
                    <th style="width: 150px">账号
                    </th>
                    <th style="width: 75px">类型
                    </th>
                    <th style="width: 150px">角色名称
                    </th>
                    <th style="width: 250px">公司名称
                    </th>
                    <th style="width: 150px">有效期从
                    </th>
                    <th style="width: 150px">有效期至
                    </th>
                </tr>
            </thead>

        </table>
    </div>

    <div id="divcontext" style="width: 1525px; margin: 0px; overflow-x: hidden; overflow-y: auto; height: 400px;">
        <table id="testfff" class="pure-table pure-table-bordered" style="text-align: center; text-decoration: none; font-size: 12px; width: 1300px; table-layout: fixed;">
            <tbody>
                <asp:Repeater ID="repeater1HECUserrole" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="width: 75px"><a href="#" onclick="javascript:OpenEmployeeinfo('<%#Eval("code") %>');"><%#Eval("code") %></a></td>
                            <td style="width: 150px"><%#Eval("dname") %></td>
                            <td style="width: 150px">
                                <%#Eval("ename")%>
                            </td>
                            <td style="width: 150px">
                                <%#Eval("uUSERNAME")%>
                            </td>
                            <td style="width: 75px">
                                <%#Eval("mUserType")%>
                            </td>
                            <td style="width: 150px">
                                <%#Eval("rROLENAME")%>
                            </td>
                            <td style="width: 250px">
                                <%#Eval("cCOMPNYFULLNAME")%>
                            </td>
                            <td style="width: 150px">
                                <%#Eval("uROLESTARTDATE")%>
                            </td>
                            <td style="width: 150px">
                                <%#Eval("uROLEENDDATE") != null&&Eval("uROLEENDDATE").ToString()!="" ? Convert.ToDateTime(Eval("uROLEENDDATE").ToString()).ToString("yyyy-MM-dd") : ""%>
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
