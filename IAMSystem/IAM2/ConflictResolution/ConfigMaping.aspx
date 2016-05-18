<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" CodeBehind="ConfigMaping.aspx.cs" Inherits="IAM.ConflictResolution.ConfigMaping" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            jQuery("#tablehead thead>tr:eq(0)>th:eq(0)>input:checkbox").click(function () {
                if (document.getElementById("chkquanxuan").checked == true) {
                    jQuery("#tablehead tbody").find("input[type=checkbox]").prop('checked', true);
                }
                else {
                    jQuery("#tablehead tbody").find("input[type=checkbox]").prop('checked', false);
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pure-control-group" style="margin-top: 10px;" runat="server" id="nav_top">
        <span style="color:blue;font-weight:bold;">配置新增账号Mapping关系</span>
        <asp:Button ID="btnupdate" runat="server" Text=" 确 定 " CssClass="pure-button pure-button-primary" OnClick="btnupdate_Click" />
        &nbsp;<input type="button" value=" 取 消 " class="pure-button pure-button-primary" onclick="javascript: window.close(true);" />
    </div>
    <div style="margin: 0px; padding: 0px; width: 1000px;">
        <table class="pure-table pure-table-bordered" id="tablehead" style="text-align: center; text-decoration: none; font-size: 14px; width: 800px; table-layout: fixed;">
            <thead>
                <tr align="center">
                    <th style="width: 50px">
                        <input type="checkbox" id="chkquanxuan" />
                    </th>
                    <th style="width: 70px">系统名称
                    </th>
                    <th style="width: 200px">用户账号
                    </th>
                    <th style="width: 240px">工号</th>
                    <th style="width: 240px">账号类型</th>

                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="repeater1" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <input type="checkbox" runat="server" id="repcheckbox" value='<%#Eval("id") %>' /></td>
                            <td><%#Eval("type") %></td>
                            <td><%#Eval("zhanghao") %>
                                <asp:HiddenField ID="hiddenzhanghao" Value='<%#Eval("zhanghao") %>' runat="server" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtgonghao" runat="server" Width="90%"></asp:TextBox></td>
                            <td>
                                <asp:DropDownList ID="dplusertype" runat="server" Width="90%">
                                    <asp:ListItem Value="" Text="请选择账号类型"></asp:ListItem>
                                    <asp:ListItem Value="员工" Text="员工"></asp:ListItem>
                                    <asp:ListItem Value="其他" Text="其他"></asp:ListItem>
                                    <asp:ListItem Value="系统" Text="系统"></asp:ListItem>
                                   
                                </asp:DropDownList></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>

            </tbody>
        </table>
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" HorizontalAlign="Center" CssClass="paginator" CurrentPageButtonClass="cpb" FirstPageText="首页"
            LastPageText="尾页" PrevPageText="上一页" NextPageText="下一页" NumericButtonTextFormatString="[{0}]"
            ShowCustomInfoSection="Left" ShowBoxThreshold="2" PageSize="5"
            CustomInfoSectionWidth="150px" InputBoxClass="text2" TextAfterInputBox="" OnPageChanging="AspNetPager1_PageChanging" />
    </div>
</asp:Content>
