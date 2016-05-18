<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" CodeBehind="TCUserInfoManager.aspx.cs" Inherits="IAM.Admin.TCUserInfoManager" %>

<%@ Register Src="~/Control/TransferControl.ascx" TagName="UserTransfer" TagPrefix="UC1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .paginator {
            font: 12px Arial, Helvetica, sans-serif;
            padding: 10px 20px 10px 0;
            margin: 0px;
        }

        .paginator a {
                border: solid 1px #ccc;
                color: #0063dc;
                cursor: pointer;
                text-decoration: none;
            }

        .paginator a:visited {
                    padding: 1px 6px;
                    border: solid 1px #ddd;
                    background: #fff;
                    text-decoration: none;
                }

        .paginator .cpb {
                border: 1px solid #F50;
                font-weight: 700;
                color: #F50;
                background-color: #ffeee5;
            }

        .paginator a:hover {
                border: solid 1px #F50;
                color: #f60;
                text-decoration: none;
            }

        .paginator a, .paginator a:visited, .paginator .cpb, .paginator a:hover {
                float: left;
                height: 16px;
                line-height: 16px;
                min-width: 10px;
                _width: 10px;
                margin-right: 5px;
                text-align: center;
                white-space: nowrap;
                font-size: 12px;
                font-family: Arial,SimSun;
                padding: 0 3px;
            }
    </style>
    <script type="text/javascript">
        $(function(){

            var windowheight=$(window).height();            
            var filterheight=$("#tablefilter").css("height");
            var tabletitle=$("#divtitle").css("height");
            var contextheight=windowheight-parseInt(filterheight.replace("px",""))-parseInt(tabletitle.replace("px",""));
            contextheight=contextheight-30;
            $("#divcontext").css("height",contextheight+"px");

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
    <UC1:UserTransfer ID="userTransfer1" SystemType="TC" runat="server" />


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
                <td class="td-title-width">TC账号:
                </td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtuserName" runat="server"></asp:TextBox>
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
                    <asp:DropDownList ID="dplxukejibie" Width="150px" runat="server">
                        <asp:ListItem Value="" Text="全部"></asp:ListItem>
                        <asp:ListItem Value="0" Text="作者"></asp:ListItem>
                        <asp:ListItem Value="1" Text="客户"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="td-title-width">用户状态:
                </td>
                <td class="td-context-width">
                    <asp:DropDownList ID="dpljinyong" Width="150px" runat="server">
                        <asp:ListItem Value="" Text="全部"></asp:ListItem>
                        <asp:ListItem Value="0" Text="活动"></asp:ListItem>
                        <asp:ListItem Value="1" Text="非活动"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="8" style="height:35px;padding-left:5px;vertical-align:middle;">
                    <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="pure-button pure-button-primary" OnClick="btnQuery_Click" />

                    <input type="button" value="新建用户" runat="server" id="inputAddNew" class="pure-button pure-button-primary" onclick="javascript: OpenPage('TCUserInfoCreate.aspx');" />

                </td>
            </tr>
        </table>
    </div>
    <div id="divtitle" style="margin-top: 10px; margin-bottom: 0px; width: 1060px;">
        <table class="pure-table pure-table-bordered" style="text-align: center; text-decoration: none; font-size: 12px; width: 1060px;">
            <thead>
                <tr align="center">
                    <th style="width: 100px; font-family: Futura Bk BT,verdana,sans-serif,'微软雅黑';">工号
                    </th>
                    <th style="width: 100px; font-family: Futura Bk BT,verdana,sans-serif,'微软雅黑';">姓名
                    </th>
                    <th style="width: 100px; font-family: Futura Bk BT,verdana,sans-serif,'微软雅黑';">部门
                    </th>
                    <th style="width: 100px; font-family: Futura Bk BT,verdana,sans-serif,'微软雅黑';">TC账号
                    </th>
                    <th style="width: 75px; font-family: Futura Bk BT,verdana,sans-serif,'微软雅黑';">类型
                    </th>
                    <th style="width: 135px; font-family: Futura Bk BT,verdana,sans-serif,'微软雅黑';">许可级别
                    </th>
                    <th style="width: 100px; font-family: Futura Bk BT,verdana,sans-serif,'微软雅黑';">用户状态
                    </th>
                    <th style="width: 200px; font-family: Futura Bk BT,verdana,sans-serif,'微软雅黑';">最后登录时间
                    </th>

                    <th align="center" style="width: 150px; font-family: Futura Bk BT,verdana,sans-serif,'微软雅黑';">操作
                    </th>
                </tr>
            </thead>
        </table>
    </div>
    <div id="divcontext" style="padding: 0px; margin: 0px; overflow-x: hidden; overflow-y: auto; height: 450px; width: 1060px;">
        <table class="pure-table pure-table-bordered" style="text-align: center; text-decoration: none; font-size: 12px; width: 1060px;">
            <tbody>
                <asp:Repeater ID="repeaterTCUserInfo" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="background: #efefef; width: 100px; font-family: Futura Bk BT,verdana,sans-serif,'微软雅黑';">
                                <a href="#" onclick="javascript:OpenEmployeeinfo('<%#Eval("mgonghao") %>');"><%#Eval("mgonghao") %></a>
                            </td>
                            <td style="background: #efefef; width: 100px; font-family: Futura Bk BT,verdana,sans-serif,'微软雅黑';">
                                <%#Eval("ename") %>
                            </td>
                            <td style="background: #efefef; width: 100px; font-family: Futura Bk BT,verdana,sans-serif,'微软雅黑';">
                                <%#Eval("dname") %>
                            </td>
                            <td style="background: #efefef; width: 100px; font-family: Futura Bk BT,verdana,sans-serif,'微软雅黑';">
                                <%#Eval("uUserID") %>
                            </td>
                            <td style="background: #efefef; width: 75px; font-family: Futura Bk BT,verdana,sans-serif,'微软雅黑';">
                                <%#Eval("mUserType") %>
                            </td>
                            <td style="background: #efefef; width: 135px; font-family: Futura Bk BT,verdana,sans-serif,'微软雅黑';">
                                <%#Eval("uLicenseLevel").ToString().Equals("0")?"作者":Eval("uLicenseLevel").ToString().Equals("1")?"客户":"" %>
                            </td>
                            <td style="background: #efefef; width: 100px; font-family: Futura Bk BT,verdana,sans-serif,'微软雅黑';"><%#Eval("uUserStatus").ToString().Equals("0")?"活动":"非活动" %>
                            </td>
                            <td style="background: #efefef; width: 200px; font-family: Futura Bk BT,verdana,sans-serif,'微软雅黑';">
                                <%#Eval("uLastLoginTime") %>
                            </td>

                            <td style="background: #efefef; width: 150px; text-align: right; font-family: Futura Bk BT,verdana,sans-serif,'微软雅黑';">
                                <%#base.ReturnUserRole.Admin==false?"": Eval("mUserType")==null?"<input type=\"button\" value=\"转移\" onclick='OpenTransfer(\""+Eval("mid")+"\");'/>":Eval("mUserType").Equals("员工")?"":"<input type=\"button\" value=\"转移\" onclick='OpenTransfer(\""+Eval("mid")+"\");'/>" %>

                                <input type="button" value="<%#base.ReturnUserRole.Admin?"编辑":"详情" %>" onclick="OpenPage('TCUserInfoCreate.aspx?mzhanghao=<%#Eval("mzhanghao")%>');" style="margin:0 5px;"/>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr>
                            <td style="background: #fff; width: 100px; font-family: Futura Bk BT,verdana,sans-serif,'微软雅黑';">

                                <a href="#" onclick="javascript:OpenEmployeeinfo('<%#Eval("mgonghao") %>');"><%#Eval("mgonghao") %></a>
                            </td>
                            <td style="background: #fff; width: 100px; font-family: Futura Bk BT,verdana,sans-serif,'微软雅黑';">
                                <%#Eval("ename") %>
                            </td>
                            <td style="background: #fff; width: 100px; font-family: Futura Bk BT,verdana,sans-serif,'微软雅黑';">
                                <%#Eval("dname") %>
                            </td>
                            <td style="background: #fff; width: 100px; font-family: Futura Bk BT,verdana,sans-serif,'微软雅黑';">
                                <%#Eval("uUserID") %>
                            </td>
                            <td style="background: #fff; width: 75px; font-family: Futura Bk BT,verdana,sans-serif,'微软雅黑';">
                                <%#Eval("mUserType") %>
                            </td>
                            <td style="background: #fff; width: 135px; font-family: Futura Bk BT,verdana,sans-serif,'微软雅黑';">
                                <%#Eval("uLicenseLevel").ToString().Equals("0")?"作者":Eval("uLicenseLevel").ToString().Equals("1")?"客户":"" %>
                            </td>
                            <td style="background: #fff; width: 100px; font-family: Futura Bk BT,verdana,sans-serif,'微软雅黑';"><%#Eval("uUserStatus").ToString().Equals("0")?"活动":"非活动" %>
                            </td>
                            <td style="background: #fff; width: 200px; font-family: Futura Bk BT,verdana,sans-serif,'微软雅黑';">
                                <%#Eval("uLastLoginTime") %>
                            </td>

                            <td style="background: #fff; width: 150px; text-align: right; font-family: Futura Bk BT,verdana,sans-serif,'微软雅黑';">
                                <%#base.ReturnUserRole.Admin==false?"": Eval("mUserType")==null?"<input type=\"button\" value=\"转移\" onclick='OpenTransfer(\""+Eval("mid")+"\");'/>":Eval("mUserType").Equals("员工")?"":"<input type=\"button\" value=\"转移\" onclick='OpenTransfer(\""+Eval("mid")+"\");'/>" %>

                                <input type="button" value="<%#base.ReturnUserRole.Admin?"编辑":"详情" %>" onclick="OpenPage('TCUserInfoCreate.aspx?mzhanghao=<%#Eval("mzhanghao")%>');"  style="margin:0 5px;"/>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
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
</asp:Content>
