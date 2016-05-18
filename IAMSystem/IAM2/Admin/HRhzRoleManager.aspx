<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HRhzRoleManager.aspx.cs" Inherits="IAM.Admin.HRhzRoleManager" MasterPageFile="~/Admin/master.Master" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">

</asp:Content>

<asp:Content ID="body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
  <div class="pure-control-group" style="margin-top:10px;">
  <table class="pure-table pure-table-bordered" id="tablelist" style="text-align:center; text-decoration:none;font-size:14px;width:500px;">
        <thead>
            <tr align="center">
                <th style="width:100px">
                   角色编码
                </th>
                <th style="width:100px">
                    角色名称
                </th>
                <th style="width:100px">
                    公司名称
                </th>
                <th style="width:100px">
                    角色类型
                </th>
              
            </tr>
        </thead>
        <tbody>
        <asp:Repeater ID="repeater1Role" runat="server">
        <ItemTemplate>
        <tr>
                <td>
                   <%#Eval("Role_code")%>
                </td>
                <td>
                    <%#Eval("role_name")%>
                </td>
                <td>
                    <%#IAM.HrCompanyEntity.CompanyNameByKey(Eval("Pk_corp").ToString())%>
                </td>
                <td>
                   <%#(IAMEntityDAL.HRsm_roleDAL.RoleResourceType)Eval("Resource_type")%>
                </td>
                
                
            </tr>
        </ItemTemplate>
            <AlternatingItemTemplate>
                <tr class="pure-table-odd">
                 <td>
                   <%#Eval("Role_code")%>
                </td>
                <td>
                    <%#Eval("role_name")%>
                </td>
                <td>
                    <%#IAM.HrCompanyEntity.CompanyNameByKey(Eval("Pk_corp").ToString())%>
                </td>
                <td>
                   <%#(IAMEntityDAL.HRsm_roleDAL.RoleResourceType)Eval("Resource_type")%>
                </td>
                
                
            </tr>
            </AlternatingItemTemplate>
        </asp:Repeater>
            
       
        </tbody>
    </table>
         
 </div>     
      <div class="pure-control-group" style="margin-top:10px;">
           <Webdiyer:AspNetPager id="AspNetPager1" runat="server" HorizontalAlign="Center"  FirstPageText="<<" LastPageText=">>" PrevPageText="<" NextPageText=">" NumericButtonTextFormatString="[{0}]" 
           ShowCustomInfoSection="Left" ShowBoxThreshold="2" PageSize="5"
                CurrentPageButtonClass="pure-button" 
               FirstLastButtonsClass="pure-button" FirstLastButtonsStyle="pure-button"
                MoreButtonsClass="pure-button" SubmitButtonClass="pure-button"
                 PrevNextButtonsClass="pure-button" PagingButtonsClass="pure-button"
                PageIndexBoxClass="pure-button" CustomInfoClass="pure-button pure-button-secondary pure-button-disabled" 
                CustomInfoSectionWidth="150px"  
               InputBoxClass="text2" TextAfterInputBox="" OnPageChanging="AspNetPager1_PageChanging"  />
          </div>
</asp:Content>