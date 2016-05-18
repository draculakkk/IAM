<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" CodeBehind="TCUserInfoCreate.aspx.cs" Inherits="IAM.Admin.TCUserInfoCreate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function OpenModal() {
            if (jQuery("#<%=txtUserName.ClientID%>").val() == "") {
                alert("请填写用户账号");
                return;
            }
            var _value = window.showModalDialog("TCRolelist.aspx", null, "dialogWidth=400px;dialogHeight=636px;");
            jQuery("#<%=txtUserInfo.ClientID %>").val(_value);
            if (jQuery("#<%=txtgonghao.ClientID %>").val() == "" && $('#<%=dplEmployeeType.ClientID%>').val() != "系统") {
                alert("员工类型账号必须填写工号");
                jQuery("#<%=txtUserInfo.ClientID %>").val("");
                return;
            }

            var objReturnBind = document.getElementById("<%=btnOnLoadRoleInfo.ClientID%>");
            
            objReturnBind.click();
        }


        function CompareUserNameOnly() {
            var request = new GetRequest();
            var isupdate = request["mzhanghao"] == undefined ? 0 : 1;
            var url = "../../ValidateLoginName.ashx?sys=TC&isupdate=" + isupdate;
            var UserName = jQuery("#<%=txtUserName.ClientID%>").val();
            jQuery.ajax({
                url: url,
                type: "post",
                dataType: "json",
                data: { login: UserName },
                success: function (redata) {
                    if (redata["lgn"] == UserName) {
                       
                    }
                    else {
                        alert(UserName + "该用户名已存在系统中\n故系统帮你自动生成" + redata["lgn"]);
                        jQuery("#<%=txtUserName.ClientID%>").val(redata["lgn"]);
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
        var usertype = "NaN";
        jQuery(document).ready(function () {
            var request = GetRequest();

            usertype = $('#<%=dplEmployeeType.ClientID%>').val();
            if (usertype == "") {
                usertype = "NaN";
            }

            if (request["mzhanghao"] != undefined) {
                jQuery.ControllerUserType("<%=dplEmployeeType.ClientID%>");
            }
            else {
                $('#<%=dplEmployeeType.ClientID%>').change(function () {
                    var obj = document.getElementById("<%=btnQuery.ClientID%>");
                     obj.click();
                 });
            }

            var isadmin = "<%=IsAdmin.ToString()%>";
            if (isadmin != "True") {
                jQuery("input[type=button]").prop("disabled", true);
            }

            jQuery("#<%=txtUserName.ClientID%>").blur(function () {
                CompareUserNameOnly();
            });

           
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pure-control-group" style="margin-top: 10px;">

        <fieldset style="border: 1px solid #efefef;">
            <legend style="font-size: 14px; color: #3C72DF">HR 信息</legend>
            <div class="pure-control-group" style="margin-top: 10px;">
                <table>
                    <tr>
                        <td class="td-title-width">账号类别:
                        </td>
                        <td class="td-context-width">
                            <asp:DropDownList ID="dplEmployeeType" runat="server">

                                <asp:ListItem Value="员工" Selected="True" Text="员工"></asp:ListItem>
                                <asp:ListItem Value="系统" Text="系统"></asp:ListItem>
                                <asp:ListItem Value="其他" Text="其他"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:HiddenField ID="mappingId" runat="server" />
                        </td>
                        <td class="td-title-width">工号:
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtgonghao" runat="server"></asp:TextBox>

                        </td>
                        <td colspan="4">
                            <asp:Button ID="btnQuery" runat="server" Text=" 查 询 " OnClick="btnQuery_Click" />
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
            <legend style="font-size: 14px; color: #3C72DF">TC账号信息</legend>
            <div class="pure-control-group" style="margin-top: 10px;">
                <table>

                    <tr>

                        <td class="td-title-width"><font color="red">*</font>人员姓名:
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                        </td>
                        <td class="td-title-width"><font color="red">*</font>用户ID:
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtUserID" runat="server"></asp:TextBox>
                        </td>
                        <td class="td-title-width"><font color="red">*</font>操作系统名称:
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtSystemName" runat="server"></asp:TextBox>
                        </td>
                        <td class="td-title-width"><font color="red">*</font>密码:
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td class="td-title-width"><font color="red">*</font>许可级别:
                        </td>
                        <td class="td-context-width">
                            <asp:DropDownList ID="dplLevel" runat="server" Width="150px">
                                <asp:ListItem Value="0" Text="作者"></asp:ListItem>
                                <asp:ListItem Value="1" Text="客户"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="td-title-width"><font color="red">*</font>Email:
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                        </td>
                        <td class="td-title-width">默认组: 
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtDefaultGroup" ReadOnly="true" runat="server"></asp:TextBox>
                        </td>
                        <td class="td-title-width">角色:
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtDefaultRole" ReadOnly="true" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>用户状态</td>
                        <td colspan="7">
                            <asp:RadioButtonList ID="rdstatus" Style="display: normal;" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" style="display: normal;" Selected="True">活动</asp:ListItem>
                                <asp:ListItem Value="1" style="display: normal;">非活动</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-title-width">备注:
                        </td>
                        <td class="td-context-width" colspan="7">
                            <asp:TextBox ID="txtMemo" TextMode="MultiLine" Width="90%" Height="50" runat="server"></asp:TextBox>
                        </td>

                    </tr>
                </table>
            </div>
        </fieldset>
        <fieldset style="border: 1px solid #efefef;">
            <legend style="font-size: 14px; color: #3C72DF">角色信息</legend>
            <div class="pure-control-group" style="margin-top: 10px;">
                <span style="display: inline-block; width: 350px; text-align: right;">
                    <input type="button" runat="server" id="inputAdd" value="委派角色" class="pure-button  pure-button-primary" style="margin-right: 20px;"
                        onclick="javascript: OpenModal();" />
                    <input type="button" runat="server" id="btnOnLoadRoleInfo" onserverclick="btnOnLoadRoleInfo_ServerClick"
                        style="display: none;" />
                </span>
                <table class="pure-table pure-table-bordered" style="width: 600px; margin-top: 20px"
                    id="tablelist">
                    <thead>
                        <tr>
                            <th>组名称
                            </th>
                            <th>角色
                            </th>
                            <th>状态
                            </th>
                            <th>操作
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="repeaterUserRole" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td style="background: #efefef; width: 150px">
                                        <%#IAM.BLL.Untityone.GetGroupName(Eval("urmemo").ToString())%>
                                    </td>
                                    <td style="background: #efefef; width: 150px">

                                        <%#IAM.BLL.Untityone.GetRoleName(Eval("urmemo").ToString()) %>
                                    </td>
                                    <td style="background: #efefef; width: 150px">
                                        
                                       <input type="radio"  checked='<%#Eval("urGroupStatus").ToString().Equals("1")?true:false %>' name="urGroupStatus" id="inputhuodong" runat="server" value="1" /><span>活动</span>
                                        <input type="radio" name="urGroupStatus" id="Radio1" checked='<%#Eval("urGroupStatus").ToString().Equals("0")?true:false %>' runat="server" value="0" /><span>非活动</span>
                                    </td>
                                    <td style="background: #efefef;">
                                        <asp:Button ID="btnDelete" Enabled="<%#base.ReturnUserRole.Admin %>" runat="server" CommandArgument='<%#Eval("urid") %>' Text="删除" OnClientClick="javascript:return DeleteIs();" OnCommand="btnDelete_Command" />
                                        <asp:HiddenField ID="rep_hiddenid" runat="server" Value='<%#Eval("urid") %>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr>
                                    <td style="background: #fff;">
                                        <%#IAM.BLL.Untityone.GetGroupName(Eval("urmemo").ToString())%>
                                    </td>
                                    <td style="background: #fff;">
                                        <%#IAM.BLL.Untityone.GetRoleName(Eval("urmemo").ToString()) %>
                                    </td>
                                    <td style="background: #fff; width: 150px">

                                       <input type="radio"  checked='<%#Eval("urGroupStatus").ToString().Equals("1")?true:false %>' name="urGroupStatus" id="inputhuodong" runat="server" value="1" /><span>活动</span>
                                        <input type="radio" name="urGroupStatus" id="Radio1" checked='<%#Eval("urGroupStatus").ToString().Equals("0")?true:false %>' runat="server" value="0" /><span>非活动</span>
                                    </td>
                                    <td style="background: white;">
                                        <asp:Button ID="btnDelete" Enabled="<%#base.ReturnUserRole.Admin %>" runat="server" CommandArgument='<%#Eval("urid") %>' Text="删除" OnClientClick="javascript:return DeleteIs();" OnCommand="btnDelete_Command" />
                                        <asp:HiddenField ID="rep_hiddenid" runat="server" Value='<%#Eval("urid") %>' />
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
            <asp:TextBox ID="txtUserInfo" runat="server" Style="display: none"></asp:TextBox>
        </fieldset>
    </div>
    <div class="pure-control-group" style="margin-top: 10px;">
        <asp:Button ID="btnCreate" runat="server" Text="确定" CssClass="pure-button  pure-button-primary" OnClick="btnCreate_Click" />
        &nbsp;&nbsp;
        <input type="button" class="pure-button  pure-button-primary" value="取消" onclick="javascript: window.close(true);" />
    </div>
    <script type="text/javascript">
        function DeleteIs() {
            if (confirm('删除后无法再恢复,确定要删除吗?')) { return true; } return false;
        }
    </script>
</asp:Content>
