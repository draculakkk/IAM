<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" CodeBehind="HECRoleAndCompany.aspx.cs" Inherits="IAM.HECRoleAndCompany" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Content/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ReturnBack() {
            var company = jQuery("#<%=ddlCompany.ClientID %>").val();
            var role = jQuery("#<%=ddlRole.ClientID %>").val();
            var start=jQuery("#txtstart").val();
            var end = jQuery("#txtend").val();
            var _id = "<%=Guid.NewGuid()%>";
            window.returnValue = _id + "^" + role + "^" + company+"^" +start+"^" +end+" ";
            window.close();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pure-control-group" style="margin-top: 10px;">
        <fieldset style="border: 1px solid #efefef;">
            <legend style="font-size: 14px; color: #3C72DF">用户权限和公司</legend>
            <div class="pure-control-group" style="margin-top: 10px;">
                <table>
                    <tr>
                        <td class="td-title-width">公司名称:
                        </td>
                        <td class="td-context-width" colspan="2">
                            <asp:DropDownList ID="ddlCompany" runat="server"></asp:DropDownList>


                        </td>
                        <td class="td-title-width">角色名称:
                        </td>
                        <td class="td-context-width" colspan="4">
                            <asp:DropDownList ID="ddlRole" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td class="td-context-width">
                            <input type="button" value="确定" runat="server" id="inputYes" class="pure-button  pure-button-primary" onclick="javascript:ReturnBack();" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td-title-width">有效期从:
                        </td>
                        <td class="td-context-width" colspan="2">
                            <input type="text" id="txtstart" class="Wdate" onclick="WdatePicker()" />
                        </td>
                        <td class="td-title-width">有效期至:
                        </td>
                        <td class="td-context-width" colspan="4">
                            <input type="text" id="txtend" class="Wdate" onclick="WdatePicker()" />
                        </td>
                        <td class="td-context-width"></td>
                    </tr>
                </table>
            </div>
        </fieldset>
    </div>
</asp:Content>
