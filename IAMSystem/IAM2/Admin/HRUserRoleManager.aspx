<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" CodeBehind="HRUserRoleManager.aspx.cs" 
Inherits="IAM.Admin.HRUserRoleManager" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pure-control-group" style="margin-top:10px;">
            <label for="email" >
                公司名称:</label>
                      <asp:DropDownList ID="dplCompany" runat="server">
                      <asp:ListItem Value="" Text=""></asp:ListItem>
                     <asp:ListItem Value="1001" Text="汇众总公司"></asp:ListItem>
                     <asp:ListItem Value="1002" Text="汇众分公司"></asp:ListItem>
                      </asp:DropDownList>


           <label for="email" style="width:100px">用户账号:</label>
           <asp:TextBox ID="txtUserName" runat="server"  class="Wdate"></asp:TextBox>
          
           <asp:Button ID="btnQuery" runat="server"  Text=" 查 询 "  
                CssClass="pure-button pure-button-primary" onclick="btnQuery_Click"/>
           
        </div>
    <div class="pure-control-group" style="margin-top: 10px;">
        <table class="pure-table pure-table-bordered" id="tablelist" style="text-align: center;
            text-decoration: none; font-size: 14px; width: 450px;">
            <thead>
                <tr align="center">
                    <th style="width: 150px">
                        UserName
                    </th>
                    <th style="width: 150px">
                        权限名称
                    </th>

                    <th style="width: 150px">
                        公司
                    </th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="repeater1UserRole" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%#Eval("UserName")%>
                            </td>
                            <td>
                                <%#Eval("role_name")%>
                            </td>
                           
                            <td>
                                <%#IAM.HrCompanyEntity.CompanyNameByKey(Eval("CompanyKey").ToString())%>
                            </td>
                        </tr>
                    </ItemTemplate>
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
            CustomInfoSectionWidth="150px" InputBoxClass="text2" TextAfterInputBox="" 
            OnPageChanging="AspNetPager1_PageChanging" 
            />
    </div>
</asp:Content>
