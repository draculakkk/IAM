<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" CodeBehind="SAP_ParametersAdd.aspx.cs" Inherits="IAM.Admin.SAP_ParametersAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            jQuery("#<%=btnYes.ClientID%>").click(function () {
                var _value = "";
                _value = jQuery("#inputID").val() + "^" + jQuery("#inputName").val() + "^" + jQuery("#inputwenben").val();
                window.returnValue = _value;
                window.close(true);
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style=" width: 650px; height: auto;">
        <fieldset style="border: 1px solid #efefef;">
            <legend style="font-size: 14px; color: #3C72DF">角色委派</legend>
            <div class="pure-control-group" style="margin-top: 10px;">
                <table>
                    <tr>
                        <td class="td-title-width">
                            参数ID:
                        </td>
                        <td class="td-context-width">
                        <input type="text" id="inputID" />
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="td-title-width">
                            参数值:
                        </td>
                        <td class="td-context-width">
                          <input type="text" id="inputName" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td-title-width">
                            短文本:
                        </td>
                        <td class="td-context-width">
                           <textarea id="inputwenben" cols="30" rows="5"></textarea>
                        </td>
                        
                        
                    </tr>
                   
                </table>
            </div>
             <div style="margin-top: 10px; text-align: center;" runat="server" id="nav_top">
        <input type="button" id="btnYes" runat="server" value=" 确 定 " class="pure-button pure-button-primary" />&nbsp;
         <input type="button" value=" 取 消 " onclick="javascript: window.close(true);" class="pure-button pure-button-primary" />
    </div>
            
        </fieldset>
    </div>
</asp:Content>
