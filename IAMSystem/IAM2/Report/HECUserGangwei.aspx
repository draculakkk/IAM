<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" CodeBehind="HECUserGangwei.aspx.cs" Inherits="IAM2.Report.HECUserGangwei" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {

            var windowheight = $(window).height();
            var filterheight = $("#tablefilter").css("height");
            var tabletitle = $("#divtitle").css("height");
            var contextheight = windowheight - parseInt(filterheight.replace("px", "")) - parseInt(tabletitle.replace("px", ""));
            contextheight = contextheight - 30;
            $("#divcontext").css("height", contextheight + "px");
            $("#btnExcelExport").bind("click", function () {
                var _gonghao = $("#<%=txtgonghao.ClientID%>").val();
                var _bumen = $("#<%=txtbumen.ClientID%>").val();
                var _xingming = $("#<%=txtxingming.ClientID%>").val();
                var _hrgangwei = $("#<%=txthrgangwei.ClientID%>").val();
                var _heczhanghao = $("#<%=txtzhanghao.ClientID%>").val();
                var _zhanghaoleixing = $("#<%=dplzhanghaoleixing.ClientID%>").val();
                var _gongsi = $("#<%=txtgongsi.ClientID%>").val();
                var _hecbumen = $("#<%=txthecbumen.ClientID%>").val();
                var _hecgangwei = $("#<%=txthecgangwei.ClientID%>").val();
                var _shifouzhuagangwei = $("#<%=ddlshifouzhugangwei.ClientID%>").val();
                var _shifouqiyong = $("#<%=dplshifouqiyong.ClientID%>").val();
                var _isuser = "<%=Request.QueryString["user"]%>";
                $.post("../ExcelExportAjax.ashx?type=hecgangwei", {
                    gonghao: _gonghao,
                    bumen: _bumen,
                    xingming: _xingming,
                    hrgangwei: _hrgangwei,
                    heczhanghao: _heczhanghao,
                    zhanghaoleixing: _zhanghaoleixing,
                    gongsi: _gongsi,
                    hecbumen: _hecbumen,
                    hecgangwei: _hecgangwei,
                    shifouzhuagangwei: _shifouzhuagangwei,
                    shifouqiyong: _shifouqiyong,
                    isuser: _isuser
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
                    <asp:TextBox ID="txtgonghao" runat="server"></asp:TextBox>
                </td>
                <td class="td-title-width">姓名:
                </td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtxingming" runat="server"></asp:TextBox>
                </td>
                <td class="td-title-width">部门:
                </td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtbumen" runat="server"></asp:TextBox>
                </td>
                <td class="td-title-width">岗位: 
                </td>
                <td class="td-context-width">
                    <asp:TextBox ID="txthrgangwei" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td-title-width">HEC账号: 
                </td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtzhanghao" runat="server"></asp:TextBox>
                </td>
                <td class="td-title-width">类型:
                </td>
                <td class="td-context-width">
                    <asp:DropDownList ID="dplzhanghaoleixing" Width="150px" runat="server">
                        <asp:ListItem Value="" Text="全部"></asp:ListItem>
                        <asp:ListItem Value="员工" Text="员工"></asp:ListItem>
                        <asp:ListItem Value="其他" Text="其他"></asp:ListItem>
                        <asp:ListItem Value="系统" Text="系统"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="td-title-width">公司名称(代码):
                </td>

                <td class="td-context-width">
                    <asp:TextBox ID="txtgongsi" runat="server"></asp:TextBox>
                </td>
                <td class="td-title-width">部门名称(代码):
                </td>
                <td class="td-context-width">
                    <asp:TextBox ID="txthecbumen" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>


                <td class="td-title-width">岗位名称(代码):
                </td>
                <td class="td-context-width">
                    <asp:TextBox ID="txthecgangwei" runat="server"></asp:TextBox>
                </td>
                <td class="td-title-width">是否主岗位:</td>
                <td class="td-context-width">
                    <asp:DropDownList ID="ddlshifouzhugangwei" Width="150px" runat="server">
                        <asp:ListItem Value="" Text="全部"></asp:ListItem>
                        <asp:ListItem Value="Y" Text="是"></asp:ListItem>
                        <asp:ListItem Value="N" Text="否"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="td-title-width">是否启用: 
                </td>
                <td class="td-context-width">
                    <asp:DropDownList ID="dplshifouqiyong" Width="150px" runat="server">
                        <asp:ListItem Value="" Text="全部"></asp:ListItem>
                        <asp:ListItem Value="Y" Text="是"></asp:ListItem>
                        <asp:ListItem Value="N" Text="否"></asp:ListItem>
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
                <%if (Request.QueryString["user"] == "1")
                  { %>
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
                    <th style="width: 150px">公司名称(代码)
                    </th>
                    <th style="width: 250px">部门名称(代码)
                    </th>
                    <th style="width: 150px">岗位名称(代码)
                    </th>
                    <th style="width: 150px">是否主岗位
                    </th>
                    <th style="width: 150px">是否启用
                    </th>
                </tr>
                <%} %>
                <%else
                  { %>
                <tr align="center">
                    <th style="width: 150px">公司名称(代码)
                    </th>
                    <th style="width: 250px">部门名称(代码)
                    </th>
                    <th style="width: 150px">岗位名称(代码)
                    </th>
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

                    <th style="width: 150px">是否主岗位
                    </th>
                    <th style="width: 150px">是否启用
                    </th>
                </tr>
                <%} %>
            </thead>

        </table>
    </div>

    <div id="divcontext" style="width: 1702px; margin: 0px; overflow-x: hidden; overflow-y: auto; height: 400px;">
        <table id="testfff" class="pure-table pure-table-bordered" style="text-align: center; text-decoration: none; font-size: 12px; width: 1450px; table-layout: fixed;">
            <tbody>
                <asp:Repeater ID="repeater1HECUserrole" runat="server">
                    <ItemTemplate>
                        <%if (Request.QueryString["user"] == "1")
                          {%>
                        <tr>
                            <td style="width: 75px"><a href="#" onclick="javascript:OpenEmployeeinfo('<%#Eval("gonghao") %>');"><%#Eval("gonghao") %></a></td>
                            <td style="width: 150px"><%#Eval("bumen") %></td>
                            <td style="width: 150px">
                                <%#Eval("xingming")%>
                            </td>
                            <td style="width: 150px">
                                <%#Eval("zhanghao")%>
                            </td>
                            <td style="width: 75px">
                                <%#Eval("zhanghaoleixing")%>
                            </td>
                            <td style="width: 150px">
                                <%#Eval("COMPANY_NAME")%>(<%#Eval("COMPANY_CODE") %>)
                            </td>
                            <td style="width: 250px">
                                <%#Eval("UNIT_NAME")%>(<%#Eval("UNIT_CODE") %>)
                            </td>
                            <td style="width: 150px">
                                <%#Eval("POSITION_NAME")%>(<%#Eval("POSITION_CODE") %>)
                            </td>
                            <td style="width: 150px">
                                <%#Eval("PRIMARY_POSITION_FLAG").ToString().Equals("Y")?"是":"否" %>
                            </td>
                            <td style="width: 150px">
                                <%#Eval("ENABLED_FLAG").ToString().Equals("Y")?"是":"否" %>
                            </td>
                        </tr>
                        <%}
                          else
                          { %>
                        <tr>
                            <td style="width: 150px">
                                <%#Eval("COMPANY_NAME")%>(<%#Eval("COMPANY_CODE") %>)
                            </td>
                            <td style="width: 250px">
                                <%#Eval("UNIT_NAME")%>(<%#Eval("UNIT_CODE") %>)
                            </td>
                            <td style="width: 150px">
                                <%#Eval("POSITION_NAME")%>(<%#Eval("POSITION_CODE") %>)
                            </td>
                            <td style="width: 75px"><a href="#" onclick="javascript:OpenEmployeeinfo('<%#Eval("gonghao") %>');"><%#Eval("gonghao") %></a></td>
                            <td style="width: 150px"><%#Eval("bumen") %></td>
                            <td style="width: 150px">
                                <%#Eval("xingming")%>
                            </td>
                            <td style="width: 150px">
                                <%#Eval("zhanghao")%>
                            </td>
                            <td style="width: 75px">
                                <%#Eval("zhanghaoleixing")%>
                            </td>
                            <td style="width: 150px">
                                <%#Eval("PRIMARY_POSITION_FLAG").ToString().Equals("Y")?"是":"否" %>
                            </td>
                            <td style="width: 150px">
                                <%#Eval("ENABLED_FLAG").ToString().Equals("Y")?"是":"否" %>
                            </td>
                        </tr>
                        <%} %>
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
