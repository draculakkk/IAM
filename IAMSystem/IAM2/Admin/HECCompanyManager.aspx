<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HECCompanyManager.aspx.cs"
    Inherits="IAM.Admin.HECCompanyManager" MasterPageFile="~/Admin/master.Master" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
 <script type="text/javascript">
     jQuery(document).ready(function () {
         jQuery("#tablelist tbody>tr:odd>td").css("background", "#EFEFEF");
         jQuery("#tablelist tbody>tr:even>td").css("background", "#FFF");
     });
</script>
</asp:Content>
<asp:Content ID="body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="pure-control-group" style="margin-top: 10px;">
        <label for="email" style="width: auto;">
            公司名称:</label>
        <asp:TextBox ID="txtCompanyName" runat="server" CssClass="Wdate"></asp:TextBox><br /><br />
        <asp:Button ID="btnQuery" runat="server" Text=" 查 询 " CssClass="pure-button pure-button-primary"
            OnClick="btnQuery_Click" />
        &nbsp;&nbsp;
    </div>
    <div>
        <table class="pure-table pure-table-bordered" id="table1" style="text-align: center; text-decoration: none; font-size: 14px; width: 750px; table-layout: fixed;">
            <thead>
                <tr align="center">
                    <th style="width: 150px">
                        公司代码
                    </th>
                    <th style="width: 150px">
                        公司简称
                    </th>
                    <th style="width: 150px">
                        公司全名
                    </th>
                    <th style="width: 150px">
                        有效期至
                    </th>
                    <th style="width: 150px">
                        员工代码
                    </th>
                </tr>
            </thead>
            </table>
    </div>
    <div style="margin:0px;padding:0px; width: 876px;overflow-x:hidden;overflow-y:auto;height:480px;">
        

            <table class="pure-table pure-table-bordered" id="tablelist" style="text-align: center; text-decoration: none; font-size: 14px; width: 750px; table-layout: fixed;">
            <tbody>
                <asp:Repeater ID="repeater1HECCompanyInfo" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="width:150px;">
                                <%#Eval("COMPANY_CODE")%>
                            </td>
                            <td style="width:150px;">
                                <%#Eval("COMPANY_SHORT_NAME")%>
                            </td>
                            <td style="width:150px;">
                                <%#Eval("COMPANY_FULL_NAME")%>
                            </td>
                            <td style="width:150px;">
                                <%#Eval("START_DATE") == null ? "" : Convert.ToDateTime(Eval("START_DATE").ToString()).ToString("yyyy-MM-dd")%>
                            </td>
                            <td style="width:150px;">
                                <%#Eval("END_DATE")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    
                </asp:Repeater>
            </tbody>
        </table>
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" HorizontalAlign="Center" CssClass="paginator" CurrentPageButtonClass="cpb" FirstPageText="首页" AlwaysShow="true"
                LastPageText="尾页" PrevPageText="上一页" NextPageText="下一页" NumericButtonTextFormatString="[{0}]"
                ShowCustomInfoSection="Left" ShowBoxThreshold="2" PageSize="5"                
                CustomInfoSectionWidth="150px" InputBoxClass="text2" TextAfterInputBox="" OnPageChanging="AspNetPager1_PageChanging" />
    </div>
    
</asp:Content>
