<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" CodeBehind="HRRoleList.aspx.cs" Inherits="IAM.Admin.HRRoleList" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function menuFixed(id) {
            var obj = document.getElementById(id);
            var _getHeight = obj.offsetTop;

            window.onscroll = function () {
                changePos(id, _getHeight);
            }
        }
        function changePos(id, height) {
            var obj = document.getElementById(id);
            var scrollTop = document.documentElement.scrollTop || document.body.scrollTop;
            if (parseInt(scrollTop) - 80 < height) {
                obj.style.position = 'relative';
                obj.style.background = '';
                obj.style.margin = "10px 0px 10px 0px";
                obj.style.textAlign = "center";
            } else {
                obj.style.position = 'fixed';
                obj.style.background = 'white';
                obj.style.margin = "0px 0px 0px 800px";

            }
        }
    </script>
    <script type="text/javascript">
        //window.onload = function () {  }
        jQuery(document).ready(function () {
            menuFixed("ContentPlaceHolder1_nav_top");
            jQuery("#<%=btnYes.ClientID%>").bind("click", function () { ForEachFindValue(); });
        });

        function ForEachFindValue() {
            var _value = "";
            var checkboxlist = jQuery("input[type='checkbox']:checked");
            jQuery(checkboxlist).each(function () {
                var _this = this;
                _value += jQuery(_this).attr("value");
            });
            window.returnValue = _value;
            window.close(true);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pure-control-group" style="margin-top: 10px; text-align: center;" runat="server" id="nav_top">
        <input type="button" id="btnYes" runat ="server" value=" 确 定 " class="pure-button pure-button-primary" />&nbsp;
         <input type="button" value=" 取 消 " onclick="javascript: window.close();" class="pure-button pure-button-primary" />
    </div>
    <div style="float: left; width: 950px; height: auto;">
        <fieldset style="border: 1px solid #efefef;">
            <legend style="font-size: 14px; color: #3C72DF">角色公司信息</legend>


            <table class="pure-table pure-table-bordered" id="tablelist" style="text-align: center; text-decoration: none; font-size: 14px; width: 900px">
                <thead>
                    <tr style="text-align: center;">
                        <th style="width:100px;">角色编码</th>
                        <th style="width: 150px">角色名称
                        </th>
                        <th style="width: 150px">角色类型
                        </th>
                        <th style="width: 600px">对应公司
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repeaterRoleComputer" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td style="background: #efefef;"><%#Eval("Role_code") %></td>
                                <td style="background: #efefef;">
                                    <%#Eval("role_name") %>
                                </td>
                                <td style="background: #efefef;">
                                    <%#(IAMEntityDAL.HRsm_roleDAL.RoleResourceType)Eval("Resource_type") %>
                                </td>
                                <td style="background: #efefef; text-align: left;">
                                    <%#BindCompany(Eval("Pk_role").ToString()) %>
                                    
                                </td>

                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr>
                                 <td style="background: white;"><%#Eval("Role_code") %></td>
                                <td style="background: white;">
                                    <%#Eval("role_name") %>
                                </td>
                                <td style="background: white;">
                                    <%#(IAMEntityDAL.HRsm_roleDAL.RoleResourceType)Eval("Resource_type") %>
                                </td>
                                <td style="background: white; text-align: left;">
                                    <%#BindCompany(Eval("Pk_role").ToString()) %>
                                    
                                </td>

                            </tr>
                        </AlternatingItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </fieldset>
    </div>
</asp:Content>
