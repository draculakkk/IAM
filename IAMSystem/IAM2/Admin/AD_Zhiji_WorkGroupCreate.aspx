<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" CodeBehind="AD_Zhiji_WorkGroupCreate.aspx.cs" Inherits="IAM.Admin.AD_Zhiji_WorkGroupCreate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            var isadmin = "<%=IsAdmin.ToString()%>";
           
            if (isadmin != "True") {
                
                jQuery("input[type=button]").prop("disabled", true);
                jQuery("#<%=btnYes.ClientID%>").prop("disabled",true);
            }
        });

        function ActionMessage() {
            jQuery("input[type=text]").val("");
            alert("操作成功");
            window.parent.opener.Yes();
            window.close();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <fieldset style="border:1px solid #efefef;">
        <legend style="font-size:14px;color:#3C72DF">AD职级工作组信息</legend>
        <div class="pure-control-group" style="margin-top: 10px;">
            <table width="650px">
                <tr>
                    <td class="td-title-width" style="width:150px;">
                        职级名称:
                    </td>
                    <td class="td-context-width" style="width:150px;">
                       <asp:TextBox ID="txtZhiji" runat="server"></asp:TextBox>
                    </td>
                   <td class="td-title-width" style="width:100px;">
                       工作组:
                    </td>
                    <td class="td-context-width" style="width:300px;">
                        <asp:TextBox ID="txtWorkGroup" runat="server"></asp:TextBox>&nbsp;
                        <input type="checkbox" id="chkFalse" runat="server" /><span>禁用</span>
                    </td>
                   
                </tr>
                <tr style="display:none;">
                    <td class="td-title-width">
                        
                    </td>
                    <td class="td-context-width" colspan="3">
                        <%--<asp:TextBox ID="txtAdDepartment" runat="server"></asp:TextBox>--%>
                    </td>
                   
                </tr>
               
            </table>
            <div style="margin:10px 200px">
                 <asp:Button ID="btnYes" runat="server" Text="确定" CssClass="pure-button pure-button-primary" OnClick="btnYes_Click" />
            &nbsp;
           <input type="button" value="取消" class="pure-button pure-button-primary" onclick="javascript: window.close(true);"/>
            </div>
          
        </div>
    </fieldset>
</asp:Content>
