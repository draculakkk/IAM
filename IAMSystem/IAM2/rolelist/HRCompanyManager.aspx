<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" CodeBehind="HRCompanyManager.aspx.cs" Inherits="IAM2.rolelist.HRCompanyManager" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <script type="text/javascript">
          jQuery(document).ready(function () {
              jQuery("#tablelist tbody>tr:odd>td").css("background", "#EFEFEF");
              jQuery("#tablelist tbody>tr:even>td").css("background", "#FFF");
          });
    </script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div  style="margin-top: 10px; width: 550px;">
        <table class="pure-table pure-table-bordered" style="text-align: center; text-decoration: none; font-size: 14px; width: 550px;table-layout:fixed;" id="tablelist">
            <thead>
                <tr align="center">
                    <th style="width: 100px">公司代码
                    </th>
                    <th style="width: 150px">公司编码
                    </th>
                    <th style="width: 100px">公司名称
                    </th>
                    <th style="width: 100px">上级公司代码
                    </th>
                </tr>
            </thead>
             <tbody>
                <asp:Repeater ID="repeaterHrRole" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="width: 100px;">
                                <%#Eval("Pk_CORP")%>
                            </td>
                            <td style="width: 150px;">
                                <%#Eval("UNTTCODE")%>
                            </td>
                            <td style="width: 100px;">
                                <%#Eval("UNTTNAME")%>
                            </td>
                            <td style="width: 100px;">
                 
                                  <%#Eval("FATHERCORP")%>
                            </td>
                          
                        </tr>
                    </ItemTemplate>
                    
                </asp:Repeater>
            </tbody>
             </table>
        
    </div>
    <div class="pure-control-group" style="margin-top: 10px;">
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" HorizontalAlign="Center" CssClass="paginator" CurrentPageButtonClass="cpb" FirstPageText="首页" AlwaysShow="true"
                LastPageText="尾页" PrevPageText="上一页" NextPageText="下一页" NumericButtonTextFormatString="[{0}]"
                ShowCustomInfoSection="Left" ShowBoxThreshold="2" PageSize="5"                
                CustomInfoSectionWidth="150px" InputBoxClass="text2" TextAfterInputBox="" OnPageChanging="AspNetPager1_PageChanging" />
    </div>
</asp:Content>
