<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HREmployeeCreate.aspx.cs"
    Inherits="IAM.Admin.HREmployeeCreate" MasterPageFile="~/Admin/master.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Content/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        var usertype = "NaN";
        jQuery(document).ready(function () {

            var isadmin = "<%=IsAdmin.ToString()%>";
            if (isadmin != "True") {
                jQuery("input[type=button]").prop("disabled", true);
            }

            var request = new GetRequest();
            if (request["id"] != undefined) {
                jQuery.ControllerUserType("<%=dplEmployeeType.ClientID%>");
            }
            else {
                jQuery("#<%=dplEmployeeType.ClientID%>").bind("change", function () {
                    var obj = jQuery("#<%=btnQuery.ClientID%>");
                    obj.click();
                });
            }

            if (request["id"] != "" && request["id"] != undefined) {
                usertype = jQuery("#<%=dplEmployeeType.ClientID%>").val();
            }

            if (usertype == "") {
                usertype = "NaN";
            }

            
            
            jQuery("#<%=txtEpolyeeEncode.ClientID%>").blur(function () {
                CompareUserNameOnly();
            });
        });

      

        function UnLockControl() {
            jQuery("#<%=txtEpolyeeEncode.ClientID%>,#<%=txtEmployeeName.ClientID%>").prop("ReadOnly", false);
            return true;
        }

        function OpenModal() {
            if (jQuery("#<%=txtEpolyeeEncode.ClientID %>").val() == "") {
                alert("请先行输入用户编码\n例如:yangjian");
                return;
            }
            var _value = window.showModalDialog("HRRoleList.aspx", null, "dialogWidth=950px;dialogHeight=670px;");
            jQuery("#<%=txtUserInfo.ClientID %>").val(_value);

            var objReturnBind = document.getElementById("<%=btnOnLoadRoleInfo.ClientID%>");
            objReturnBind.click();
        }

        function CompareUserNameOnly() {
            var request = new GetRequest();
            var isupdate = request["id"] ==undefined ? 0 : 1;
            var url = "../../ValidateLoginName.ashx?sys=HR&isupdate=" + isupdate;
            var UserName = jQuery("#<%=txtEpolyeeEncode.ClientID%>").val();
            jQuery.ajax({
                url: url,
                type: "post",
                dataType: "json",
                data: { login: UserName },
                success: function (redata) {
                    if (redata["status"] != UserName) {
                        alert(UserName + "用户编码已存在!");
                        jQuery("#<%=txtEpolyeeEncode.ClientID%>").val(redata["status"]);
                    }
                }
            });
    }

    function GetRequest() {

        var url = location.search; //获取url中"?"符后的字串
        var theRequest = new Object();
        if (url.indexOf("?") != -1) {
            var str = url.substr(1);
            strs = str.split("&");
            for (var i = 0; i < strs.length; i++) {
                theRequest[strs[i].split("=")[0]] = unescape(strs[i].split("=")[1]);
            }
        }
        return theRequest;
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="pure-control-group" style="margin-top: 10px; height: 650px; overflow: auto;">

        <fieldset style="border: 1px solid #efefef;">
            <legend style="font-size: 14px; color: #3C72DF">HR 信息</legend>
            <div class="pure-control-group" style="margin-top: 10px;">
                <table>
                    <tr>
                        <td class="td-title-width">账号类别:
                        </td>
                        <td class="td-context-width">
                            <asp:DropDownList ID="dplEmployeeType" runat="server">

                                <asp:ListItem Value="员工" Text="员工" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="其他" Text="其他"></asp:ListItem>
                                <asp:ListItem Value="系统" Text="系统"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="td-title-width"><font style="color:red">*</font>工号:
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtEmployeeNumber" runat="server"></asp:TextBox>
                        </td>
                        <td colspan="4">
                            <asp:Button ID="btnQuery" runat="server" Text=" 查 询 "
                                CssClass="pure-button pure-button-primary" OnClick="btnQuery_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td-title-width">姓名:
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                        </td>
                        <td class="td-title-width">岗位:
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtGangwei" runat="server"></asp:TextBox>
                        </td>
                        <td class="td-title-width">所在部门:
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtPartment" runat="server"></asp:TextBox>
                        </td>
                        <td class="td-title-width">上级部门:
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtPrePartment" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-title-width">手机:
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtPhoneNumber" runat="server"></asp:TextBox>
                        </td>
                        <td class="td-title-width">到职日期:
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtComeDate" runat="server"></asp:TextBox>
                        </td>
                        <td class="td-title-width">离职日期:
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtOutDate" runat="server"></asp:TextBox>
                        </td>
                        <td class="td-title-width"><%--部门撤销日期:--%>
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtPartmentOutDate" runat="server" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2"><%--部门撤销标记--%>
                            <asp:CheckBox ID="chkPartmentOut" Visible="false" runat="server" />
                        </td>
                        <td colspan="6"><%--部门封存标记--%><asp:CheckBox Visible="false" ID="chkPartmentClose" runat="server" />
                        </td>
                    </tr>
                </table>

            </div>
        </fieldset>


        <fieldset style="border: 1px solid #efefef;">
            <legend style="font-size: 14px; color: #3C72DF">操作员信息</legend>
            <div class="pure-control-group" style="margin-top: 10px;">
                <table>

                    <tr>
                        <td class="td-title-width"><font style="color:red">*</font>用户编码:
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtEpolyeeEncode" EnableViewState="true" runat="server"></asp:TextBox>
                        </td>
                        <td class="td-title-width"><font style="color:red">*</font>用户名称:
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtEmployeeName" runat="server"></asp:TextBox>
                        </td>
                        <td class="td-title-width">用户描述:
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtEmpDescription" runat="server"></asp:TextBox>
                        </td>
                        <td class="td-title-width"><font style="color:red">*</font>密码级别:
                        </td>
                        <td class="td-context-width">
                            <asp:DropDownList ID="ddlPasswordLevel" runat="server">
                                <asp:ListItem Value="" Text=""></asp:ListItem>
                                <asp:ListItem Value="senior" Text="管理级"></asp:ListItem>                                
                                <asp:ListItem Value="junior" Text="普通级"></asp:ListItem>
                                <asp:ListItem Value="update" Text="预置级"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-title-width"><font style="color:red">*</font>密码:
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
                        </td>

                        <td class="td-title-width"><font style="color:red">*</font>生效日期:
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtEntryDate" onClick="WdatePicker()" runat="server" class="Wdate"></asp:TextBox>
                        </td>
                        <td class="td-title-width">失效日期:
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtFuilure" onClick="WdatePicker()" runat="server" class="Wdate"></asp:TextBox>
                        </td>
                        <td class="td-title-width"><font style="color:red">*</font>登录认证方式:
                        </td>
                        <td class="td-context-width">
                            <asp:DropDownList ID="dplLoginType" runat="server">
                                <asp:ListItem Value="ncca" Text="CA认证"></asp:ListItem>
                                <asp:ListItem Value="staticpwd" Text="静态密码认证" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-title-width">锁定标志:
                        </td>
                        <td class="td-context-width">
                            <asp:CheckBox ID="chkIsSuoding" runat="server" />
                        </td>
                        <td class="td-title-width"></td>
                        <td class="td-context-width" colspan="5"></td>
                    </tr>
                    <tr>
                        <td class="td-title-width">备注:
                        </td>
                        <td class="td-context-width" colspan="7">
                            <asp:TextBox ID="txtMemo" runat="server" TextMode="MultiLine" Width="90%" Height="50"></asp:TextBox>
                        </td>


                    </tr>
                </table>
            </div>
        </fieldset>
        <asp:ScriptManager ID="scriptmanager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="update1" runat="server">
            <ContentTemplate>


                <fieldset style="border: 1px solid #efefef;">
                    <legend style="font-size: 14px; color: #3C72DF">角色信息</legend>
                    <div class="pure-control-group" style="margin-top: 10px;">
                        <span style="display: inline-block; width: 600px; text-align: right;">
                            <input type="button" runat="server" id="btnOnLoadRoleInfo" onserverclick="btnOnLoadRoleInfo_click" style="display: none;" />
                            <input type="button" onclick="javascript: OpenModal();" runat="server" id="inputweipai" value="委派角色" class="pure-button  pure-button-primary" style="margin-right: 20px;" />
                        </span>
                        <table class="pure-table pure-table-bordered" style="width: 600px; margin-top: 20px">
                            <thead>
                                <tr>
                                    <th>角色编码
                                    </th>
                                    <th>角色名称
                                    </th>
                                    <th>公司名称
                                    </th>
                                    <th>操作
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="repeaterHRRole" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td style="background: #efefef">
                                                <%#Eval("rRole_code")%>
                                                <asp:HiddenField ID="hiddenRolePK" runat="server" Value='<%#Eval("rPk_role") %>' />
                                                <asp:HiddenField ID="hiddenCompanyKey" runat="server" Value='<%#Eval("cPk_corp") %>' />
                                            </td>
                                            <td style="background: #efefef">
                                                <%#Eval("rRole_Name")%>
                                            </td>
                                            <td style="background: #efefef">
                                                <%#Eval("cUNTTNAME")%>
                                            </td>
                                            <td style="background: #efefef">
                                                <asp:LinkButton ID="lbtnDelete" Enabled="<%#base.ReturnUserRole.Admin %>" CommandArgument='<%#Eval("urPk_user_role") %>' OnCommand="lbtnDelete_Command" runat="server" Text="删除"></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr>
                                            <td style="background: white">
                                                <%#Eval("rRole_code")%>
                                                <asp:HiddenField ID="hiddenRolePK" runat="server" Value='<%#Eval("rPk_role") %>' />
                                                <asp:HiddenField ID="hiddenCompanyKey" runat="server" Value='<%#Eval("cPk_corp") %>' />
                                            </td>
                                            <td style="background: white">
                                                <%#Eval("rRole_Name")%>
                                            </td>
                                            <td style="background: white">
                                                <%#Eval("cUNTTNAME")%>
                                            </td>
                                            <td style="background: white">
                                                <asp:LinkButton ID="lbtnDelete" Enabled="<%#base.ReturnUserRole.Admin %>" CommandArgument='<%#Eval("urPk_user_role") %>' OnCommand="lbtnDelete_Command" runat="server" Text="删除"></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                    <asp:TextBox ID="txtUserInfo" runat="server" Style="display: none" Width="900"></asp:TextBox>
                </fieldset>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div style="margin-top: 10px; text-align: center;">
            <asp:Button ID="btnSave" runat="server" Text=" 确认 "
                CssClass="pure-button pure-button-primary" OnClick="Button1_Click" OnClientClick="UnLockControl();" />&nbsp;&nbsp;
        <input type="button" id="btnCancel" value=" 取消 " onclick="javascript: ClosePage();" class="pure-button pure-button-primary" />
        </div>
    </div>
</asp:Content>
