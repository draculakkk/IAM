<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true"
    CodeBehind="HrDepartmentManager.aspx.cs" Inherits="IAM.Admin.HrDepartmentManager" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pure-control-group" style="margin-top: 10px;">
        <%--<label for="email">
            公司名称:</label>
        <asp:DropDownList ID="dplCompany" runat="server">
            <asp:ListItem Value="" Text=""></asp:ListItem>
            <asp:ListItem Value="1001" Text="汇众总公司"></asp:ListItem>
            <asp:ListItem Value="1002" Text="汇众分公司"></asp:ListItem>
        </asp:DropDownList>
        <label for="email" style="width: 100px">
            用户账号:</label>
        <asp:TextBox ID="txtUserName" runat="server" class="Wdate"></asp:TextBox>
        <asp:Button ID="btnQuery" runat="server" Text=" 查 询 " CssClass="pure-button pure-button-primary"
            OnClick="btnQuery_Click" />--%>
    </div>
    <div class="pure-control-group" style="margin-top: 10px;">
        <table class="pure-table pure-table-bordered" id="tablelist" style="text-align: center;
            text-decoration: none; font-size: 14px; width: 800px;">
            <thead>
                <tr align="center">
                    <th style="width: 120px">
                        部门名称
                    </th>
                    <th style="width: 120px">
                        上级部门
                    </th>
                    <th style="width: 120px">
                        是否封存
                    </th>
                    <th>
                        是否撤销
                    </th>
                     <th>
                        撤销日期
                    </th>
                    <th>
                        是否同步
                    </th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="repeater1HrDepartment" runat="server">
                    <ItemTemplate>
                        <tr >
                            <td style="background-color:#efefef">
                                <%#Eval("name")%>
                            </td>
                            <td style="background-color:#efefef">
                                <%#Eval("shangjiName")%>
                            </td>
                            <td style="background-color:#efefef">
                            <%#(!(bool)Eval("isSealed"))?"False":"True"%>
                            </td>
                            <td style="background-color:#efefef">
                            <%#(!(bool)Eval("isRevoke")) ? "False" : "True " %>
                            </td>
                            <td style="background-color:#efefef">
                            <%# Eval("revokeDate")==null?"":Convert.ToDateTime(Eval("revokeDate").ToString()).ToString("yyyy-MM-dd") %>
                            </td>
                            <td style="background-color:#efefef">
                            <%#(!(bool)Eval("isSync")) ? "False" : "True " + "(" + Convert.ToDateTime(Eval("syncDate").ToString()).ToString("yyyy-MM-dd") + ")"%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                    <tr >
                            <td style=" background-color:white">
                                <%#Eval("name")%>
                            </td>
                            <td style=" background-color:white">
                                <%#Eval("shangjiName")%>
                            </td>
                            <td style=" background-color:white">
                            <%#(!(bool)Eval("isSealed"))?"False":"True"%>
                            </td>
                            <td style="background-color:#efefef">
                            <%#(!(bool)Eval("isRevoke")) ? "False" : "True " %>
                            </td>
                            <td style="background-color:#efefef">
                            <%# Eval("revokeDate")==null?"":Convert.ToDateTime(Eval("revokeDate").ToString()).ToString("yyyy-MM-dd") %>
                            </td>
                            <td style=" background-color:white">
                            <%#(!(bool)Eval("isSync")) ? "False" : "True " + "(" + Convert.ToDateTime(Eval("syncDate").ToString()).ToString("yyyy-MM-dd") + ")"%>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
    <div class="pure-control-group" style="margin-top: 10px;">
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" HorizontalAlign="Center" FirstPageText="<<"
            LastPageText=">>" PrevPageText="<" NextPageText=">" NumericButtonTextFormatString="[{0}]"
            ShowCustomInfoSection="Left" ShowBoxThreshold="2" PageSize="5" CurrentPageButtonClass="pure-button"
            FirstLastButtonsClass="pure-button" FirstLastButtonsStyle="pure-button" MoreButtonsClass="pure-button"
            SubmitButtonClass="pure-button" PrevNextButtonsClass="pure-button" PagingButtonsClass="pure-button"
            PageIndexBoxClass="pure-button" CustomInfoClass="pure-button pure-button-secondary pure-button-disabled"
            CustomInfoSectionWidth="150px" InputBoxClass="text2" TextAfterInputBox="" OnPageChanging="AspNetPager1_PageChanging" />
    </div>
</asp:Content>
