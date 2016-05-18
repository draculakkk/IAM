<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" CodeBehind="AD_ComputerManager.aspx.cs" Inherits="IAM.Admin.AD_ComputerManager" %>
<%@ Register Src="~/Control/TransferControl.ascx" TagName="UserTransfer" TagPrefix="UC1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function AddComputer(_name,_gonghao) {
            if (_name != "") {
                window.open('./AD_ComputerCreate.aspx?isupdate=1&gonghao=' + _gonghao + "&id=" + _name, "_blank", "width=800px;height=900px;toolbar=no;loction=no;");
            }
            else if (_name == "" && _gonghao == "")
            {
                window.open("./AD_ComputerCreate.aspx", "_blank", "width=800px;height=900px;toolbar=no;loction=no;");
            }
        }

        $(function () {

            var windowheight = $(window).height();
            var filterheight = $("#tablefilter").css("height");
            var tabletitle = $("#divtitle").css("height");
            var contextheight = windowheight - parseInt(filterheight.replace("px", "")) - parseInt(tabletitle.replace("px", ""));
            contextheight = contextheight - 30;
            $("#divcontext").css("height", contextheight + "px");

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
    <UC1:UserTransfer ID="userTransfer1" SystemType="ADComputer" runat="server" />
    
      <div class="pure-control-group" style="margin-top: 10px;">
          <table id="tablefilter">
              <tr>
                  <td class="td-title-width">工号:</td>
                  <td class="td-context-width"><asp:TextBox ID="txtgonghao" runat="server"></asp:TextBox></td>
                  <td class="td-title-width">姓名:</td>
                  <td class="td-context-width"><asp:TextBox ID="txtuname" runat="server"></asp:TextBox></td>
                  <td class="td-title-width">部门:</td>
                  <td class="td-context-width"><asp:TextBox ID="txtDepartment" runat="server" CssClass="Wdate"></asp:TextBox></td>
                  <td class="td-title-width">岗位:</td>
                  <td class="td-context-width"><asp:TextBox ID="txtgongwei" runat="server"></asp:TextBox></td>
              </tr>
              <tr>
                  <td class="td-title-width">账号:</td>
                  <td class="td-context-width">
                      <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
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
                  <td class="td-context-width">
                     
                  </td>
              </tr>
              <tr>
                  <td colspan="8" style="vertical-align:middle;height:35px;padding-left:5px;">
<asp:Button ID="btnQuery" runat="server" Text=" 查 询 " CssClass="pure-button pure-button-primary"
            OnClick="btnQuery_Click" /> &nbsp;
                      <input type="button" value=" 新 建 " onclick="AddComputer('', '');" class="pure-button pure-button-primary" />
                  </td>
              </tr>
          </table>
         
        
      
    </div>
    <div id="divtitle" class="pure-control-group" style="margin-top: 10px;margin-bottom:0px;">
        <table class="pure-table pure-table-bordered" style="width:1100px;table-layout:fixed;text-align: center; text-decoration: none; font-size: 14px;">
            <thead>
                <tr align="center">
                    <th style="width: 75px">
                        工号
                    </th>
                    <th style="width:75px">姓名</th>
                    <th style="width: 200px">
                       部门  
                    </th>
                    <th style="width: 150px">
                        计算机名
                    </th>
                    <th style="width: 75px">
                        类型
                    </th>
                    <th style="width: 150px">
                        描述
                    </th>
                    <th style="width:75px;">是否禁用</th>
                    <th style="width: 150px;display:none;">
                        最后登陆时间
                    </th>
                    <th style="width: 150px">
                        操作
                    </th>
                </tr>
            </thead>
            
        </table>
    </div>
    <div id="divcontext" style="margin: 0px;width:1151px;overflow-x:hidden;overflow-y:auto;height:450px;">
        <table class="pure-table pure-table-bordered" id="tablelist" style="text-align: center;
            text-decoration: none; font-size: 14px; width: 950px;table-layout:fixed">
            <tbody>
                <asp:Repeater ID="repeater1AdComputerInfo" runat="server">
                    <ItemTemplate>
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
                            <td style="width: 150px">
                                <%#Eval("aDESCRIPTION")%>
                            </td>
                            <td style="width:75px;"><%#Eval("aenable").ToString().Equals("1")?"启用":"禁用" %></td>
                            <td style="width: 150px;display:none;">
                                <%#Eval("aExpiryDate") == null ? "" : Convert.ToDateTime(Eval("aExpiryDate").ToString()).ToString("yyyy-MM-dd")%>
                            </td>
                            <td style="width:150px;text-align:right"><%#Eval("bUserType")==null?"<input type=\"button\" value=\"转移\" onclick='OpenTransfer(\""+Eval("bid")+"\");'/>":Eval("bUserType").Equals("员工")?"":"<input type=\"button\" value=\"转移\" onclick='OpenTransfer(\""+Eval("bid")+"\");'/>" %>
                               <%#EidtString(Eval("aName"),Eval("bgonghao")) %>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
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
                            <td style="width: 150px">
                                <%#Eval("aDESCRIPTION")%>
                            </td>
                            <td style="width:75px;"><%#Eval("aenable").ToString().Equals("1")?"启用":"禁用" %></td>
                            <td style="width: 150px;display:none;">
                                <%#Eval("aExpiryDate") == null ? "" : Convert.ToDateTime(Eval("aExpiryDate").ToString()).ToString("yyyy-MM-dd")%>
                            </td>
                             <td style="width:150px;text-align:right"><%#Eval("bUserType")==null?"<input type=\"button\" value=\"转移\" onclick='OpenTransfer(\""+Eval("bid")+"\");'/>":Eval("bUserType").Equals("员工")?"":"<input type=\"button\" value=\"转移\" onclick='OpenTransfer(\""+Eval("bid")+"\");'/>" %>
                                 <%#EidtString(Eval("aName"),Eval("bgonghao")) %>
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
                CustomInfoSectionWidth="150px" InputBoxClass="text2" TextAfterInputBox="" OnPageChanging="AspNetPager1_PageChanging" />
    </div>
    </div>
</asp:Content>
