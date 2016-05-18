<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" CodeBehind="AdUserInfoManager.aspx.cs" Inherits="IAM.Admin.AdUserInfoManager" %>

<%@ Register Src="~/Control/TransferControl.ascx" TagName="UserTransfer" TagPrefix="UC1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/ScrollTopFunction.js" type="text/javascript"></script>
    <script src="../Content/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        //window.onload = function () { menuFixed("ContentPlaceHolder1_nav_top"); }
        function OpenNew(_value,_type){
            var url="ADInfoManager.aspx?userid="+_value;
            switch(_type)
            {
                case"员工":url="aduser/usercreate.aspx?userid="+_value;break;
                case"其他":url="aduser/OtherCreate.aspx?userid="+_value;;break;
                case"系统":url="aduser/SystemCreate.aspx?userid="+_value;break;
            }
            OpenPage(url);
        }
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
    <UC1:UserTransfer ID="userTransfer1" SystemType="AD" runat="server" />
    <div id="divmain">


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
                    <td class="td-title-width">失效日期:</td>
                    <td class="td-context-width">
                        <asp:TextBox ID="txtStartDate" runat="server" class="Wdate" onClick="WdatePicker()"></asp:TextBox></td>
                    <td class="td-title-width" style="text-align: center;">至</td>
                    <td class="td-context-width">
                        <asp:TextBox ID="txtEndDate" runat="server" class="Wdate" onClick="WdatePicker()"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td-title-width">状态:</td>
                    <td class="td-context-width">
                        <asp:DropDownList ID="dplEnable" Width="150px" runat="server">
                            <asp:ListItem Value="" Text="全部"></asp:ListItem>
                            <asp:ListItem Value="1" Text="启用"></asp:ListItem>
                            <asp:ListItem Value="0" Text="禁用"></asp:ListItem>
                        </asp:DropDownList></td>
                    <td class="td-context-width" colspan="6"></td>
                </tr>
                <tr style="height: 35px;">
                    <td colspan="8" style="vertical-align: middle; padding-left: 5px;">
                        <asp:Button ID="btnQuery" runat="server" Text=" 查 询 " CssClass="pure-button pure-button-primary" OnClick="btnQuery_Click" />
                        &nbsp;
                    <input type="button" runat="server" id="btnAddNew" value="新建用户" class="pure-button pure-button-primary" onclick="javascript: OpenPage('ADInfoManager.aspx');" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="divtitle" style="margin-top: 10px; width: 950px;">
            <table class="pure-table pure-table-bordered" style="width: 950px; table-layout: fixed; text-align: center; text-decoration: none; font-size: 14px;">
                <thead>
                    <tr align="center">
                        <th style="width: 75px">工号
                        </th>


                        <th style="width: 75px">姓名
                        </th>
                        <th style="width: 200px">部门
                        </th>
                        <th style="width: 100px">岗位
                        </th>
                        <th style="width: 100px">AD账号
                        </th>
                        <th style="width: 50px;">类型</th>
                        <th style="width: 100px">是否启用
                        </th>
                        <th style="width: 100px">失效日期
                        </th>

                        <th align="center" style="width: 150px">操作
                        </th>
                    </tr>
                </thead>

            </table>
        </div>
        <div id="divcontext" style="margin: 0px; width: 1176px; overflow-x: hidden; overflow-y: auto; ">
            <table class="pure-table pure-table-bordered" style="width: 950px; table-layout: fixed; text-align: center; text-decoration: none; font-size: 12px;">
                <tbody>
                    <asp:Repeater ID="repeaterUserInfo" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td style="background: #efefef; width: 75px;">
                                    <a href="#" onclick="javascript:OpenEmployeeinfo('<%#Eval("gonghao") %>');"><%#Eval("gonghao") %></a>
                                </td>
                                <td style="background: #efefef; width: 75px;">
                                    <%#Eval("NAME") %>
                                </td>
                                <td style="background: #efefef; width: 200px;">
                                    <%#Eval("Department") %>
                                </td>
                                <td style="background: #efefef; width: 100px;">
                                    <%#Eval("Posts") %>
                                </td>
                                <td style="background: #efefef; width: 100px;">
                                    <%#Eval("UserID") %>
                                </td>
                                <td style="background: #efefef; width: 50px;"><%#Eval("bUserType") %></td>
                                <td style="background: #efefef; width: 100px;">
                                    <%#Eval("ENABLE").ToString().Equals("True")?"启用":"禁用" %>
                                </td>
                                <td style="background: #efefef; width: 100px;">
                                    <%#Eval("expiryDate") %>
                                </td>

                                <td style="background: #efefef; width: 150px; text-align: right;">
                                    <%#Eval("bUserType")==null?"<input type=\"button\" value=\"转移\" onclick='OpenTransfer(\""+Eval("bid")+"\");'/>":Eval("bUserType").Equals("员工")?"":"<input type=\"button\" value=\"转移\" onclick='OpenTransfer(\""+Eval("bid")+"\");'/>" %>
                                    <input type="button" value="编辑" onclick="OpenNew('<%#Eval("UserID")%>    ','<%#Eval("bUserType")%>');" style="margin: 0 7px;" />
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr>
                                <td style="background: white; width: 75px;">
                                    <a href="#" onclick="javascript:OpenEmployeeinfo('<%#Eval("gonghao") %>');"><%#Eval("gonghao") %></a>
                                </td>
                                <td style="background: white; width: 75px;">
                                    <%#Eval("NAME") %>
                                </td>
                                <td style="background: white; width: 200px;">
                                    <%#Eval("Department") %>
                                </td>
                                <td style="background: white; width: 100px;">
                                    <%#Eval("Posts") %>
                                </td>
                                <td style="background: white; width: 100px;">
                                    <%#Eval("UserID") %>
                                </td>
                                <td style="background: #fff; width: 50px;"><%#Eval("bUserType") %></td>
                                <td style="background: #fff; width: 100px;">
                                    <%#Eval("ENABLE").ToString().Equals("True")?"启用":"禁用" %>
                                </td>
                                <td style="background: #fff; width: 100px;">
                                    <%#Eval("expiryDate") %>
                                </td>

                                <td style="background: white; width: 150px; text-align: right;">
                                    <%#Eval("bUserType")==null?"<input type=\"button\" value=\"转移\" onclick='OpenTransfer(\""+Eval("bid")+"\");'/>":Eval("bUserType").Equals("员工")?"":"<input type=\"button\" value=\"转移\" onclick='OpenTransfer(\""+Eval("bid")+"\");'/>" %>
                                    <input type="button" value="编辑" onclick="OpenNew('<%#Eval("UserID")%>    ','<%#Eval("bUserType")%>');" style="margin: 0 7px;" />
                            </tr>
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
    </div>
</asp:Content>
