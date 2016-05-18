<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" CodeBehind="sapUserManger.aspx.cs" Inherits="IAM.Admin.sapUserManger" %>

<%@ Register Src="~/Control/TransferControl.ascx" TagName="UserTransfer" TagPrefix="UC1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Content/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function(){
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

    <UC1:UserTransfer ID="userTransfer1" SystemType="SAP" runat="server" />
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
                    <asp:DropDownList ID="dplleixing" Width="150px" runat="server">
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
                <td class="td-title-width"></td>
                <td class="td-context-width"></td>
            </tr>
            <tr>
                <td class="td-title-width">起始有效期从:</td>
                <td class="td-context-width" colspan="3">
                    <asp:TextBox ID="txtStartDate" Width="170" runat="server" onClick="WdatePicker()" class="Wdate"></asp:TextBox>
                    至
        <asp:TextBox ID="txtEndDate" Width="170" runat="server" onClick="WdatePicker()" class="Wdate"></asp:TextBox>
                </td>
                <td class="td-title-width">截至有效期从:</td>
                <td class="td-context-width" colspan="3">
                    <asp:TextBox ID="TextBox1" Width="170" runat="server" onClick="WdatePicker()" class="Wdate"></asp:TextBox>
                    至
        <asp:TextBox ID="TextBox2" Width="170" runat="server" onClick="WdatePicker()" class="Wdate"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td class="td-context-width" colspan="8">
                    <asp:Button ID="btnQuery" runat="server" Text=" 查 询 " CssClass="pure-button pure-button-primary" OnClick="btnQuery_Click" />
                    &nbsp;
        <input type="button" runat="server" id="inputAddNew" value="新建用户" class="pure-button pure-button-primary" onclick="javascript: OpenPage('sapUserCreate.aspx');" />

                </td>
            </tr>
        </table>


    </div>

    <div class="pure-control-group" style="margin-top: 10px; margin-bottom: 0px; width: 920px;">
        <table class="pure-table pure-table-bordered" style="text-align: center; text-decoration: none; font-size: 14px; width: 920px; table-layout: fixed;">
            <thead>
                <tr>
                    <th style="width: 100px">工号
                    </th>
                    <th style="width: 100px">姓(名)
                    </th>
                    <th style="width: 100px">部门
                    </th>
                    <th style="width: 100px">用户名
                    </th>
                    <th style="width: 100px">类型
                    </th>
                    <th style="width: 100px">用户类型
                    </th>
                    <th style="width: 100px">有效期从
                    </th>
                    <th style="width: 100px">有效期至
                    </th>


                    <th align="center" style="width: 120px">操作
                    </th>
                </tr>
            </thead>

        </table>
    </div>
    <div class="pure-control-group" style="margin: 0px; height: 350px; width: 1145px; overflow-x: hidden; overflow-y: auto;">
        <table class="pure-table pure-table-bordered" style="text-align: center; text-decoration: none; font-size: 12px; width: 920px; table-layout: fixed;">
            <tbody>
                <asp:Repeater ID="repeaterSapUserInfo" runat="server">
                    <ItemTemplate>
                        <tr>

                            <td style="background: #efefef; width: 100px;">

                                <a href="#" onclick="javascript:OpenEmployeeinfo('<%#Eval("bGONGHAO") %>');"><%#Eval("bGONGHAO") %></a>
                            </td>
                            <td style="background: #efefef; width: 100px;">
                                <%#Eval("eNAME") %>
                            </td>
                            <td style="background: #efefef; width: 100px;">
                                <%#Eval("dNAME") %>
                            </td>
                            <td style="background: #efefef; width: 100px;">
                                <%#Eval("aBAPIBNAME") %>
                            </td>
                            <td style="background: #efefef; width: 100px;">
                                <%#Eval("bUserType") %>
                            </td>
                            <td style="background: #efefef; width: 100px;">
                                <%#Eval("aUSERTYPE") %>
                            </td>
                            <td style="background: #efefef; width: 100px;">
                                <%#Eval("aSTART_DATE") %>
                            </td>
                            <td style="background: #efefef; width: 100px;">
                                <%#Eval("aEND_DATE") %>
                            </td>

                            <td style="background: #efefef; width: 120px; text-align: right;">
                                <%#base.ReturnUserRole.Admin==false?"": Eval("bUserType")==null?"<input type=\"button\" value=\"转移\" onclick='OpenTransfer(\""+Eval("bid")+"\");'/>":Eval("bUserType").Equals("员工")?"":"<input type=\"button\" value=\"转移\" onclick='OpenTransfer(\""+Eval("bid")+"\");'/>" %>
                                <input type="button" style="margin-right: 10px;" value='<%#base.ReturnUserRole.Admin?"编辑":"详情" %>' onclick="OpenPage('sapusercreate.aspx?uid=<%#Eval("aBAPIBNAME")%>    ');" />
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr>

                            <td style="background: #fff; width: 100px;">
                                <a href="#" onclick="javascript:OpenEmployeeinfo('<%#Eval("bGONGHAO") %>');"><%#Eval("bGONGHAO") %></a>
                            </td>
                            <td style="background: #fff; width: 100px;">
                                <%#Eval("eNAME") %>
                            </td>
                            <td style="background: #fff; width: 100px;">
                                <%#Eval("dNAME") %>
                            </td>
                            <td style="background: #fff; width: 100px;">
                                <%#Eval("aBAPIBNAME") %>
                            </td>
                            <td style="background: #fff; width: 100px;">
                                <%#Eval("bUserType") %>
                            </td>
                            <td style="background: #fff; width: 100px;">
                                <%#Eval("aUSERTYPE") %>
                            </td>
                            <td style="background: #fff; width: 100px;">
                                <%#Eval("aSTART_DATE") %>
                            </td>
                            <td style="background: #fff; width: 100px;">
                                <%#Eval("aEND_DATE") %>
                            </td>

                            <td style="background: #fff; width: 120px; text-align: right;">
                                <%#base.ReturnUserRole.Admin==false?"": Eval("bUserType")==null?"<input type=\"button\" value=\"转移\" onclick='OpenTransfer(\""+Eval("bid")+"\");'/>":Eval("bUserType").Equals("员工")?"":"<input type=\"button\" value=\"转移\" onclick='OpenTransfer(\""+Eval("bid")+"\");'/>" %>
                                <input type="button" style="margin-right: 10px;" value='<%#base.ReturnUserRole.Admin?"编辑":"详情" %>' onclick="OpenPage('sapusercreate.aspx?uid=<%#Eval("aBAPIBNAME")%>    ');" />
                        </tr>

                    </AlternatingItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
    <div class="pure-control-group" style="margin-top: 10px;">
            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" HorizontalAlign="Center" CssClass="paginator" CurrentPageButtonClass="cpb" FirstPageText="首页"
                LastPageText="尾页" PrevPageText="上一页" NextPageText="下一页" NumericButtonTextFormatString="[{0}]"
                ShowCustomInfoSection="Left" ShowBoxThreshold="2" PageSize="5"                
                CustomInfoSectionWidth="150px" InputBoxClass="text2" TextAfterInputBox="" OnPageChanging="AspNetPager1_PageChanging" />
        </div>
</asp:Content>
