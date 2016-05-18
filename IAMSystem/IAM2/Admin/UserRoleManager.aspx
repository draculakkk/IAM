<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserRoleManager.aspx.cs"
    Inherits="IAM.Admin.UserRoleManager" MasterPageFile="~/Admin/master.Master" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript">
        jQuery(document).ready(function () {
            jQuery("#tablelist tbody>tr:odd").css("background", "#efefef");
            jQuery("#tablelist tbody>tr:even").css("background", "#fff");
        });


        function IsDelete() {
            if (confirm("是否确定要删除该条权限？")) {
                return true;
            }
            return false;
        }
    </script>


</asp:Content>
<asp:Content ID="body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="pure-control-group" style="margin-top:10px;">
            <label for="email" style="margin-left:5px;width:auto;">
                权限名称:</label>
                      <asp:DropDownList ID="dplType" runat="server">
                      <asp:ListItem Value="" Text=""></asp:ListItem>
                      <asp:ListItem Value="EndUser" Text="EndUser"></asp:ListItem>
                      <asp:ListItem Value="Admin" Text="Admin"></asp:ListItem>
                      <asp:ListItem Value="TC" Text="TC"></asp:ListItem>
                      <asp:ListItem Value="EHR" Text="EHR"></asp:ListItem>
                      <asp:ListItem Value="HEC" Text="HEC"></asp:ListItem>
                      <asp:ListItem Value="AD" Text="AD"></asp:ListItem>
                      <asp:ListItem Value="SAP" Text="SAP"></asp:ListItem>
                          <asp:ListItem Value="Leader" Text="Leader"></asp:ListItem>
                      </asp:DropDownList>


           <label for="email" style="width:100px">AD账号:</label>
           <asp:TextBox ID="txtADname" runat="server"  class="Wdate"></asp:TextBox>
          <br /><br />
           &nbsp;&nbsp;<asp:Button ID="btnQuery" runat="server"  Text=" 查 询 "  
                CssClass="pure-button pure-button-primary" onclick="btnQuery_Click"/>
           <input type="button" runat="server" value=" 新 建 " onclick="javascript:OpenPage1('UserRoleAdd.aspx');" class="pure-button pure-button-primary" />
        </div>
    <div class="pure-control-group" style="margin-top: 10px;">
        <table class="pure-table pure-table-bordered" id="tablelist" style="text-align: center;
            text-decoration: none; font-size: 14px; width: 750px;">
            <thead>
                <tr align="center">
                    <th style="width: 200px">
                        ADName
                    </th>
                    <th style="width: 350px">
                        权  限
                    </th>

                    <th style="width: 200px">
                        操作
                    </th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="repeater1UserRole" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%#Eval("adname") %>
                            </td>
                            <td>
                                <%#Eval("roles")%>
                            </td>
                           
                            <td>
                                <input type="button" value=" 编 辑 " onclick="javascript:OpenPage1('UserRoleAdd.aspx?adname=<%#Eval("adname") %>')"/>
                               
                                <asp:Button ID="btndelete" runat="server" Text=" 删 除 "  OnCommand="btndelete_Command1" OnClientClick="return IsDelete();" CommandArgument='<%#Eval("adname") %>'/>
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
