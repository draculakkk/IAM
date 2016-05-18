<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" CodeBehind="ADGroupReport.aspx.cs" Inherits="IAM.Report.ADGroupReport" %>

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

            jQuery("#btnExcelExport").bind("click", function () {
                var gonghao = jQuery("#<%=txtgonghao.ClientID%>").val();
                var department = jQuery("#<%=txtPartment.ClientID%>").val();
                var name = jQuery("#<%=txtName.ClientID%>").val();
                var gangwei = jQuery("#<%=txtgangwei.ClientID%>").val();
                var adusername = jQuery("#<%=txtUserName.ClientID%>").val();
                var leixing = jQuery("#<%=dlptype.ClientID%>").val();
                var StartDate = jQuery("#<%=txtStartDate.ClientID%>").val();
                var EndDate = jQuery("#<%=txtEndDate.ClientID%>").val();
                var qiyong = jQuery("#<%=dplEnable.ClientID%>").val();
                var workgroupName = jQuery("#<%=txtgroupname.ClientID%>").val();
                if (StartDate == "" && EndDate != "") {
                    alert('必须填写失效日期从字段值'); return;
                }
                if (StartDate != "" && EndDate == "") {
                    alert('必须填写失效日期至字段值'); return;
                }
                jQuery.post("../ExcelExportAjax.ashx?type=adgroup",
                    {
                        gonghao: gonghao,
                        department: department,
                        name: name,
                        gangwei: gangwei,
                        adusername: adusername,
                        leixing: leixing,
                        StartDate: StartDate,
                        EndDate: EndDate,
                        qiyong: qiyong,
                        workgroupName: workgroupName
                    }
                    , function (data) {
                        if (data.indexOf("error") == -1) {
                            window.location.href = "../downloadFile/" + data;
                        }
                        else {
                            alert(data);
                        }
                    }, "text");
            });
            jQuery("#testfff tbody>tr:even>td").css({ background: "#fff" });
            jQuery("#testfff tbody>tr:odd>td").css({ background: "#efefef" });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pure-control-group" style="margin-top: 10px;" runat="server" id="nav_top">
        <table id="tablefilter">
            <tr>
                <td class="td-title-width">工号:</td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtgonghao" runat="server"></asp:TextBox></td>
                <td class="td-title-width">姓名:</td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox></td>
                <td class="td-title-width">部门:</td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtPartment" runat="server"></asp:TextBox></td>
                <td class="td-title-width">岗位:</td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtgangwei" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="td-title-width">账号:</td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox></td>
                <td class="td-title-width">类型:</td>
                <td class="td-context-width">
                    <asp:DropDownList ID="dlptype" Width="150px" runat="server">
                        <asp:ListItem Value="" Text="全部"></asp:ListItem>
                        <asp:ListItem Value="员工" Text="员工"></asp:ListItem>
                        <asp:ListItem Value="其他" Text="其他"></asp:ListItem>
                        <asp:ListItem Value="系统" Text="系统"></asp:ListItem>
                    </asp:DropDownList>

                </td>
                <td class="td-title-width">失效日期从:</td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtStartDate" runat="server" class="Wdate" onClick="WdatePicker()"></asp:TextBox>

                </td>
                <td class="td-title-width" style="text-align: center;">至</td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtEndDate" runat="server" class="Wdate" onClick="WdatePicker()"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="td-title-width">是否启用:</td>
                <td class="td-context-width">
                    <asp:DropDownList ID="dplEnable" Width="150px" runat="server">
                        <asp:ListItem Value="" Text="全部"></asp:ListItem>
                        <asp:ListItem Value="1" Text="启用"></asp:ListItem>
                        <asp:ListItem Value="0" Text="禁用"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="td-title-width">名称组:</td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtgroupname" runat="server"></asp:TextBox></td>
                <td class="td-context-width" colspan="4"></td>
            </tr>
            <tr>
                <td colspan="8" style="vertical-align: middle; height: 35px; padding-left: 5px;">
                    <asp:Button ID="btnQuery" runat="server" Text=" 查 询 "
                        CssClass="pure-button pure-button-primary" OnClick="btnQuery_Click" />
                    &nbsp;&nbsp;
        <input type="button" value=" 导 出 " id="btnExcelExport" class="pure-button pure-button-primary" />
                </td>
            </tr>
        </table>
    </div>

    <div id="divtitle" style="margin: 0px; padding: 0px; width: 850px;">
        <table class="pure-table pure-table-bordered" style="table-layout: fixed; text-align: center; text-decoration: none; font-size: 14px; width: 850px;">
            <thead>
                <tr align="center">
                    <th style="width: 200px">组名称
                    </th>
                    <th style="width: 75px">工号 
                    </th>

                    <th style="width: 150px;">姓名</th>
                    <th style="width: 200px">部门 
                    </th>
                    <th style="width: 150px">账号
                    </th>
                    <th style="width: 75px">类型
                    </th>
                    <th style="width: 150px">状态
                    </th>
                </tr>
            </thead>

        </table>
    </div>
    <div id="divcontext" style="margin: 0px; padding: 0px; width: 1177px; overflow-x: hidden; overflow-y: auto;">
        <table class="pure-table pure-table-bordered" id="testfff" style="table-layout: fixed; text-align: center; text-decoration: none; font-size: 14px; width: 850px;">
            <tbody>
                <asp:Repeater ID="repeater1ADUserGrounp" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="width: 200px">
                                <%#Eval("uwGroupName")%>
                            </td>
                            <td style="width: 75px">
                                <a href="#" onclick="javascript:OpenEmployeeinfo('<%#Eval("mgonghao")%>');"><%#Eval("mgonghao")%></a>
                            </td>

                            <td style="width: 150px">
                                <%#Eval("ename")%>
                            </td>
                            <td style="width: 200px">
                                <%#Eval("dname")%>
                            </td>
                            <td style="width: 150px">
                                <%#Eval("uUserID")%>
                            </td>
                            <td style="width: 75px">
                                <%#Eval("mUserType")%>
                            </td>
                            <td style="width: 150px">
                                <%#Eval("uENABLE")!=null?Eval("uENABLE").ToString().Equals("True")?"启用":"禁用":"" %>

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
