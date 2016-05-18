<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" CodeBehind="ADComputerReport.aspx.cs" Inherits="IAM.Report.ADComputerReport" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        jQuery(document).ready(function () {

            var windowheight = $(window).height();
            var filterheight = $("#tablefilter").css("height");
            var tabletitle = $("#divtitle").css("height");
            var contextheight = windowheight - parseInt(filterheight.replace("px", "")) - parseInt(tabletitle.replace("px", ""));
            contextheight = contextheight - 30;
            $("#divcontext").css("height", contextheight + "px");

            mergeTable("testfff", 2, 0);
            jQuery("#btnExcelExport").bind("click", function () {
                var _gonghao = jQuery("#<%=txtgonghao.ClientID%>").val();
                var _name = jQuery("#<%=txtuname.ClientID%>").val();
                var _gangwei = jQuery("#<%=txtgangwei.ClientID%>").val();
                var _dept = jQuery("#<%=txtDepartment.ClientID%>").val();
                var _computername = jQuery("#<%=txtName.ClientID%>").val();
                var _leixing = jQuery("#<%=dplType.ClientID%>").val();
                var _jinyong = jQuery("#<%=dpljinyong.ClientID%>").val();
                var _workgroup = jQuery("#<%=txtWorkgroup.ClientID%>").val();

                jQuery.post("../ExcelExportAjax.ashx?type=<%=Request.QueryString["type"]%>",
                    {
                        gonghao: _gonghao,
                        name: _name,
                        gangwei: _gangwei,
                        leixing: _leixing,
                        jinyong:_jinyong,
                        computername: _computername,
                        workgroup: _workgroup,
                        department: _dept
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

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pure-control-group" style="margin-top: 10px;">
        <table id="tablefilter">
            <tr>
                <td class="td-title-width">工号:</td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtgonghao" runat="server"></asp:TextBox></td>
                <td class="td-title-width">姓名:</td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtuname" runat="server"></asp:TextBox></td>
                <td class="td-title-width">部门:</td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtDepartment" runat="server" CssClass="Wdate"></asp:TextBox></td>
                <td class="td-title-width">岗位:</td>
                <td class="td-context-width" colspan="2">
                    <asp:TextBox ID="txtgangwei" runat="server" CssClass="Wdate"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="td-title-width">账号:</td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtName" runat="server" CssClass="Wdate"></asp:TextBox>
                </td>
                <td class="td-title-width">类型:</td>
                <td class="td-context-width">
                    <asp:DropDownList ID="dplType" Width="150px" runat="server">
                        <asp:ListItem Value="" Text="全部"></asp:ListItem>
                        <asp:ListItem Value="员工" Text="员工"></asp:ListItem>
                        <asp:ListItem Value="其他" Text="其他"></asp:ListItem>
                        <asp:ListItem Value="系统" Text="系统"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="td-title-width">是否禁用:</td>
                <td class="td-context-width">
                    <asp:DropDownList ID="dpljinyong" Width="150px" runat="server">
                        <asp:ListItem Value="" Text="全部"></asp:ListItem>
                        <asp:ListItem Value="1" Text="启用"></asp:ListItem>
                        <asp:ListItem Value="0" Text="禁用"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="td-title-width">工作组:</td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtWorkgroup" runat="server" CssClass="Wdate"></asp:TextBox>
                </td>
                <td>
                    
                </td>
            </tr>
            <tr>
                <td colspan="8" style="height:35px;vertical-align:middle;padding-left:5px;">
                    <asp:Button ID="btnQuery" runat="server" Text=" 查 询 " CssClass="pure-button pure-button-primary"
                        OnClick="btnQuery_Click" />&nbsp;
                    <input type="button" id="btnExcelExport" value=" 导 出 " class="pure-button pure-button-primary" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divtitle" class="pure-control-group" style="margin-top: 10px; margin-bottom: 0px;">
        <table class="pure-table pure-table-bordered" style="width: 1150px; table-layout: fixed; text-align: center; text-decoration: none; font-size: 14px;">
            <thead>
                <%if (pagetype == "adcomputer")
                  {%>
                <tr align="center">
                    <th style="width: 75px">工号
                    </th>
                    <th style="width:75px;">姓名</th>
                    <th style="width: 200px">部门  
                    </th>
                    <th style="width: 150px">计算机名
                    </th>
                    <th style="width: 75px">类型
                    </th>
                    <th style="width:75px;">是否禁用</th>
                    <th style="width: 150px">描述
                    </th>
                    <th style="width: 200px">工作组
                    </th>
                    <th style="width: 150px">最后登录时间
                    </th>
                </tr>
                <%} %>
                <%if (pagetype == "workgroup")
                  {%>
                <tr align="center">
                    <th style="width: 200px">工作组
                    </th>
                    <th style="width: 75px">工号
                    </th>
                    <th style="width:75px;">姓名</th>
                    <th style="width: 200px">部门  
                    </th>
                    <th style="width: 150px">计算机名
                    </th>
                    <th style="width: 75px">类型
                    </th>
                    <th style="width:75px;">是否禁用</th>
                    <th style="width: 150px">描述
                    </th>

                    <th style="width: 150px">最后登录时间
                    </th>
                </tr>
                <%} %>
            </thead>

        </table>
    </div>
    <div id="divcontext" style="margin: 0px; width: 1376px; overflow-x: hidden; overflow-y: auto; height: 455px;">
        <table class="pure-table pure-table-bordered" id="tablelist" style="text-align: center; text-decoration: none; font-size: 14px; width: 1150px; table-layout: fixed">
            <tbody>
                <asp:Repeater ID="repeater1AdComputerInfo" runat="server">
                    <ItemTemplate>
                        <%if (Request.QueryString["type"] == "adcomputer")
                          { %>
                        <tr>
                            <td style="width: 75px">
                                <a href="#" onclick="javascript:OpenEmployeeinfo('<%#Eval("bgonghao")%>');"><%#Eval("bgonghao")%></a>
                            </td>
                            <td style="width:75px;"><%#Eval("ename") %></td>
                            <td style="width: 200px">
                                <%#Eval("pname")%>
                            </td>
                            <td style="width: 150px">
                                <%#Eval("aName")%>
                            </td>
                            <td style="width: 75px">
                                <%#Eval("bUserType")%>
                            </td>
                            <td style="width:75px;"><%#Eval("aenable").ToString().Equals("1")?"启用":"禁用" %></td>
                            <td style="width: 150px; cursor: pointer" title="<%#Eval("aDESCRIPTION") %>">
                                <%#Eval("aDESCRIPTION")==null?"":IAM.BLL.Untityone.SubString(Eval("aDESCRIPTION").ToString(),0,15)%>
                            </td>
                            <td style="width: 200px">
                                <%#Eval("wworkgroup") %>
                            </td>
                            <td style="width: 150px">
                                <%#Eval("aExpiryDate") == null ? "" : Convert.ToDateTime(Eval("aExpiryDate").ToString()).ToString("yyyy-MM-dd")%>
                            </td>
                        </tr>
                        <%} %>
                        <%if (Request.QueryString["type"] == "workgroup")
                          { %>
                        <tr>
                            <td style="width: 200px">
                                <%#Eval("wworkgroup") %>
                            </td>
                            <td style="width: 75px">
                                <a href="#" onclick="javascript:OpenEmployeeinfo('<%#Eval("bgonghao")%>');"><%#Eval("bgonghao")%></a>
                            </td>
                             <td style="width:75px;"><%#Eval("ename") %></td>
                            <td style="width: 200px">
                                <%#Eval("pname")%>
                            </td>
                            <td style="width: 150px">
                                <%#Eval("aName")%>
                            </td>
                            <td style="width: 75px">
                                <%#Eval("bUserType")%>
                            </td>
                            <td style="width:75px;"><%#Eval("aenable").ToString().Equals("1")?"启用":"禁用" %></td>
                            <td style="width: 150px; cursor: pointer" title="<%#Eval("aDESCRIPTION") %>">
                                <%#Eval("aDESCRIPTION")==null?"":IAM.BLL.Untityone.SubString(Eval("aDESCRIPTION").ToString(),0,15)%>
                            </td>

                            <td style="width: 150px">
                                <%#Eval("aExpiryDate") == null ? "" : Convert.ToDateTime(Eval("aExpiryDate").ToString()).ToString("yyyy-MM-dd")%>
                            </td>
                        </tr>
                        <%} %>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <%if (Request.QueryString["type"] == "adcomputer")
                          { %>
                        <tr>
                            <td style="width: 75px">
                                <a href="#" onclick="javascript:OpenEmployeeinfo('<%#Eval("bgonghao")%>');"><%#Eval("bgonghao")%></a>
                            </td>
                            <td style="width:75px;"><%#Eval("ename") %></td>
                            <td style="width: 200px">
                                <%#Eval("pname")%>
                            </td>
                            <td style="width: 150px">
                                <%#Eval("aName")%>
                            </td>
                            <td style="width: 75px">
                                <%#Eval("bUserType") %>
                            </td>
                            <td style="width:75px;"><%#Eval("aenable").ToString().Equals("1")?"启用":"禁用" %></td>
                            <td style="width: 150px; cursor: pointer" title="<%#Eval("aDESCRIPTION") %>">
                                <%#Eval("aDESCRIPTION")==null?"":IAM.BLL.Untityone.SubString(Eval("aDESCRIPTION").ToString(),0,15)%>
                            </td>
                            <td style="width: 200px">
                                <%#Eval("wworkgroup") %>
                            </td>
                            <td style="width: 150px">
                                <%#Eval("aExpiryDate") == null ? "" : Convert.ToDateTime(Eval("aExpiryDate").ToString()).ToString("yyyy-MM-dd")%>
                            </td>
                        </tr>
                        <%} %>
                        <%if (Request.QueryString["type"] == "workgroup")
                          { %>
                        <tr>
                            <td style="width: 200px">
                                <%#Eval("wworkgroup") %>
                            </td>
                            <td style="width: 75px">
                                <a href="#" onclick="javascript:OpenEmployeeinfo('<%#Eval("bgonghao")%>');"><%#Eval("bgonghao")%></a>
                            </td>
                             <td style="width:75px;"><%#Eval("ename") %></td>
                            <td style="width: 200px">
                                <%#Eval("pname")%>
                            </td>
                            <td style="width: 150px">
                                <%#Eval("aName")%>
                            </td>
                            <td style="width: 75px;">
                                <%#Eval("bUserType") %>
                            </td>
                            <td style="width:75px;"><%#Eval("aenable").ToString().Equals("1")?"启用":"禁用" %></td>
                            <td style="width: 150px; cursor: pointer" title="<%#Eval("aDESCRIPTION") %>">
                                <%#Eval("aDESCRIPTION")==null?"":IAM.BLL.Untityone.SubString(Eval("aDESCRIPTION").ToString(),0,15)%>
                            </td>

                            <td style="width: 150px">
                                <%#Eval("aExpiryDate") == null ? "" : Convert.ToDateTime(Eval("aExpiryDate").ToString()).ToString("yyyy-MM-dd")%>
                            </td>
                        </tr>
                        <%} %>
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
