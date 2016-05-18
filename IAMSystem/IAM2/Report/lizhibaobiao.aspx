<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" CodeBehind="lizhibaobiao.aspx.cs" Inherits="IAM2.Report.lizhibaobiao" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pure-control-group" style="margin-top: 10px;">
        <asp:DropDownList ID="dplreporttype" runat="server">
            <asp:ListItem Value="0" Text="离职"></asp:ListItem>
            <asp:ListItem Value="1" Text="在职"></asp:ListItem>
        </asp:DropDownList>
        
        <asp:Button ID="btnQuery" runat="server" Text=" 查 询 " CssClass="pure-button pure-button-primary"
            OnClick="btnQuery_Click" />
        &nbsp;&nbsp;
    <asp:Button ID="btnOutExpr" runat="server" Text=" 导 出 " OnClick="btnOutExpr_Click" CssClass="pure-button pure-button-primary" />
    </div>
    <div class="pure-control-group" style="margin-top: 10px;">
        <table class="pure-table pure-table-bordered" id="tablelist" style="text-align: center; text-decoration: none; font-size: 14px; width: 1050px;">
            <thead>
                <tr align="center">
                    <th style="width: 150px">工号
                    </th>
                    <th style="width: 150px">姓名
                    </th>
                    <th style="width: 150px;">部门</th>
                    <th style="width: 100px">AD
                    </th>
                    <th style="width: 100px">ADComputer
                    </th>
                    <th style="width: 100px">HEC
                    </th>
                    <th style="width: 100px">SAP
                    </th>
                    <th style="width: 100px;">TC</th>
                    <th style="width: 100px;">HR</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="RptReport" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="width: 150px; background: #efefef;"><a href="javascript:OpenPage('ReportByUserCode.aspx?gonghao=<%#Eval("code") %>');"><%#Eval("code") %></a>
                            </td>
                            <td style="width: 150px; background: #efefef;"><%#Eval("name") %>
                            </td>
                            <td style="width: 150px; background: #efefef;"><%#Eval("DepartName") %></td>
                            <td style="width: 100px; background: #efefef;"><%#Eval("ad") %>
                            </td>
                            <td style="width: 100px; background: #efefef;"><%#Eval("adcomputer") %>
                            </td>
                            <td style="width: 100px; background: #efefef;"><%#Eval("hec") %>
                            </td>
                            <td style="width: 100px; background: #efefef;"><%#Eval("sap") %>
                            </td>
                            <td style="width: 100px; background: #efefef;"><%#Eval("tc") %></td>
                            <td style="width: 100px; background: #efefef;"><%#Eval("hr") %></td>

                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr>
                            <td style="width: 150px; background: #fff;">
                                <a href="javascript:OpenPage('ReportByUserCode.aspx?gonghao=<%#Eval("code") %>');"><%#Eval("code") %></a>
                            </td>
                            <td style="width: 150px; background: #fff;"><%#Eval("name") %>
                            </td>
                            <td style="width: 150px; background: #fff;"><%#Eval("DepartName") %></td>
                            <td style="width: 100px; background: #fff;"><%#Eval("ad") %>
                            </td>
                            <td style="width: 100px; background: #fff;"><%#Eval("adcomputer") %>
                            </td>
                            <td style="width: 100px; background: #fff;"><%#Eval("hec") %>
                            </td>
                            <td style="width: 100px; background: #fff;"><%#Eval("sap") %>
                            </td>
                            <td style="width: 100px; background: #fff;"><%#Eval("tc") %></td>
                            <td style="width: 100px; background: #fff;"><%#Eval("hr") %></td>

                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" HorizontalAlign="Center" CssClass="paginator" CurrentPageButtonClass="cpb" FirstPageText="首页"
            LastPageText="尾页" PrevPageText="上一页" NextPageText="下一页" NumericButtonTextFormatString="[{0}]"
            ShowCustomInfoSection="Left" ShowBoxThreshold="2" PageSize="5"
            CustomInfoSectionWidth="150px" InputBoxClass="text2" TextAfterInputBox="" OnPageChanging="AspNetPager1_PageChanging" />
    </div>
</asp:Content>
