<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" CodeBehind="TCRolelist.aspx.cs" Inherits="IAM.Admin.TCRolelist" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ReturnGroupInfo() {
            var _value = "";
            var checkboxlist = jQuery("input[type='checkbox']:checked");
            jQuery(checkboxlist).each(function () {
                var _this = this;
                _value += jQuery(_this).attr("value") + ";";
            });
            window.returnValue = _value;
            window.close(true);
            
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <fieldset style="border: 1px solid #efefef;">
            <legend style="font-size: 14px; color: #3C72DF">角色组信息</legend>


            <table class="pure-table pure-table-bordered" id="tablelist" style="text-align: left; text-decoration: none; font-size: 14px; width: 300px">
                <thead>
                    <tr style="text-align: center;">
                        <th style="width: 150px">组
                        </th>
                        <th style="width: 150px">角色
                        </th>
                    </tr>
                </thead>
            </table>
        <div style="height:500px;overflow-y:auto;width:300px;overflow-x:hidden;">
 <table class="pure-table pure-table-bordered" id="tablelist1" style="text-align: left; text-decoration: none; font-size: 14px; width: 300px">
                <tbody>
                    <asp:Repeater ID="repeaterRole" runat="server">
                        <ItemTemplate>
                            <tr>

                                <td style="background: #efefef;width:150px;">
                                    <%#"<input type='checkbox' value='"+Eval("RoleID").ToString()+"^"+IAM.BLL.Untityone.GetRoleName(Eval("RoleName").ToString())+"^"+IAM.BLL.Untityone.GetGroupName(Eval("RoleName").ToString())+"'"+"<span style=\"margin-left:5px;\">"+IAM.BLL.Untityone.GetGroupName(Eval("RoleName").ToString())+"</span>" %>
                                </td>
                                <td style="background: #efefef;width:150px;">
                                    <%#IAM.BLL.Untityone.GetRoleName(Eval("RoleName").ToString()) %>
                                </td>
                                

                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr>

                                <td style="background: white;">
                                    <%#"<input type='checkbox' value='"+Eval("RoleID").ToString()+"^"+IAM.BLL.Untityone.GetRoleName(Eval("RoleName").ToString())+"^"+IAM.BLL.Untityone.GetGroupName(Eval("RoleName").ToString())+"'"+"<span style=\"margin-left:5px;\">"+IAM.BLL.Untityone.GetGroupName(Eval("RoleName").ToString())+"</span>" %>
                                </td>
                                <td style="background: white;">
                                     <%#IAM.BLL.Untityone.GetRoleName(Eval("RoleName").ToString()) %>
                                </td>
                               

                            </tr>
                        </AlternatingItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
        </fieldset>
    <div class="pure-control-group" style="margin-top: 10px; text-align: left;" runat="server" id="nav_top">
        <input type="button" id="btnYes" runat="server" value=" 确 定 " onclick="javascript: ReturnGroupInfo();" class="pure-button pure-button-primary" />&nbsp;
         <input type="button" value=" 取 消 " onclick="javascript: window.close(true);" class="pure-button pure-button-primary" />
    </div>
</asp:Content>
