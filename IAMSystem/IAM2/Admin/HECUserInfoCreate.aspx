<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true"
    CodeBehind="HECUserInfoCreate.aspx.cs" Inherits="IAM.Admin.HECUserInfoCreate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Content/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        function OpenModal() {
            if (jQuery("#<%=txtgonghao.ClientID%>").val() == "" && jQuery("#<%=dplEmployeeType.ClientID%>").val() != "系统") {
                alert("请填写工号");
                return;
            }
            var _value = window.showModalDialog("HECRoleAndCompany.aspx", null, "dialogWidth=809px;dialogHeight=350px;");

            jQuery("#<%=txtUserInfo.ClientID %>").val(_value);
            if (jQuery("#<%=txtEmployeeNumber.ClientID %>").val() == "") {
                jQuery("#<%=txtUserInfo.ClientID %>").val("");
                return;
            }
            var objReturnBind = document.getElementById("<%=btnOnLoadRoleInfo.ClientID%>");

            objReturnBind.click();
        }

        function OpenModal2() {
            if (jQuery("#<%=txtgonghao.ClientID%>").val() == "" && jQuery("#<%=dplEmployeeType.ClientID%>").val() != "系统") {
                alert("请填写工号");
                return;
            }
            var _value = window.showModalDialog("HECgangwei.aspx", null, "dialogWidth=400px;dialogHeight=436px;");

            jQuery("#<%=txthecgangwei.ClientID %>").val(_value);
            if (jQuery("#<%=txtEmployeeNumber.ClientID %>").val() == "") {
                jQuery("#<%=txthecgangwei.ClientID %>").val("");
                return;
            }
            var objReturnBind = document.getElementById("<%=btnhecGangwei.ClientID%>");
            objReturnBind.click();
        }

        //检查用户名是否在系统中存在
        function CheckUserNameOnly() {
            var url = "../../ValidateLoginName.ashx?sys=HEC";
            var _login = jQuery("#<%=txtEmployeeNumber.ClientID%>").val();
            jQuery.ajax({
                url: url,
                type: "post",
                dataType: "json",
                data: { isupdate: "0", login: _login },
                success: function (redata) {
                    if (redata["status"] == "no") {
                        alert(_login + "用户名在HEC系统中已存在\n请重新填写");
                        jQuery("#<%=txtEmployeeNumber.ClientID%>").val("");
                    }

                }
            });
        }


        // *****js 获取QueryString
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
            var request = new GetRequest();
            if (request["usercd"] != undefined) {
                jQuery.ControllerUserType("<%=dplEmployeeType.ClientID%>");
                
            }
            else {
                $("#<%=dplEmployeeType.ClientID%>").bind("change", function () {
                    var _obj = document.getElementById("<%=btnQuery.ClientID%>");
                    _obj.click();
                });
            }

            if (request["usercd"] != undefined) {
                usertype = jQuery("#<%=dplEmployeeType.ClientID%>").val();
            }
            if (usertype == "") {
                usertype = "NaN";
            }

            var isadmin = "<%=IsAdmin.ToString()%>";
            if (isadmin != "True") {
                jQuery("input[type=button]").prop("disabled", true);
            }


            jQuery("#<%=txtEmployeeNumber.ClientID%>").blur(function () {
                CheckUserNameOnly();
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

                                <asp:ListItem Value="员工" Text="员工" Selected="True"></asp:ListItem>
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
            <legend style="font-size: 14px; color: #3C72DF">HEC账号</legend>
            <div class="pure-control-group" style="margin-top: 10px;">
                <table>
                    <tr>
                        <td class="td-title-width"><font color="red">*</font>账号:
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtEmployeeNumber" runat="server"></asp:TextBox>
                            <%--<asp:Button ID="btnQuery" runat="server" Text=" 查 询 " OnClick="btnQuery_Click" />--%>
                        </td>
                        <td class="td-title-width"><font color="red">*</font>员工工号:</td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtEmployeeCode" runat="server"></asp:TextBox></td>
                        <td class="td-title-width"><font color="red">*</font>姓名:
                        </td>
                        <td class="td-context-width" colspan="3">
                            <asp:TextBox ID="txtUserChinesename" runat="server"></asp:TextBox>
                        </td>


                    </tr>
                    <tr>
                        <td class="td-title-width">描述:
                        </td>
                        <td class="td-context-width" colspan="5">
                            <asp:TextBox ID="txtUserDescritpion" TextMode="MultiLine" runat="server" Width="99%" Height="36px"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td class="td-title-width"><font color="red">*</font>有效期从:
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtStartDate" onClick="WdatePicker()" runat="server" class="Wdate"></asp:TextBox>
                        </td>
                        <td class="td-title-width">有效期至:
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtEndDate" onClick="WdatePicker()" runat="server" class="Wdate"></asp:TextBox>
                        </td>
                        <td class="td-title-width"></td>
                        <td class="td-context-width" colspan="3"></td>
                    </tr>
                    <tr>
                        <td class="td-title-width">冻结:
                        </td>
                        <td class="td-context-width">
                            <asp:CheckBox ID="chkIsSuoding" runat="server" />
                        </td>
                        <td class="td-title-width">冻结时间:
                        </td>
                        <td class="td-context-width" colspan="5">
                            <asp:TextBox ID="txtDongjieTime" runat="server" onClick="WdatePicker()" class="Wdate"></asp:TextBox>
                        </td>
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

        <fieldset style="border: 1px solid #efefef;">
            <legend style="font-size: 14px; color: #3C72DF">角色信息</legend>
            <div class="pure-control-group" style="margin-top: 10px;">
                <span style="display: inline-block; width: 1000px; text-align: right;">
                    <input type="button" runat="server" id="inputAddNewRole" value="委派角色" class="pure-button  pure-button-primary" style="margin-right: 20px;"
                        onclick="javascript: OpenModal();" />

                </span>
                <asp:UpdatePanel ID="updatepanel1" runat="server">
                    <ContentTemplate>
                        <input type="button" runat="server" id="btnOnLoadRoleInfo" onserverclick="btnOnLoadRoleInfo_ServerClick"
                            style="display: none;" />

                        <table class="pure-table pure-table-bordered" style="width: 1000px; margin-top: 20px"
                            id="tablelist">
                            <thead>
                                <tr>
                                    <th>角色代码
                                    </th>
                                    <th>角色名称
                                    </th>
                                    <th>公司代码
                                    </th>
                                    <th>公司简称
                                    </th>
                                    <th>有效期自
                                    </th>
                                    <th>有效期至
                                    </th>
                                    <th>操作
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="repeaterUserInfo" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td style="background: #efefef;">
                                                <asp:HiddenField ID="hiddenID" runat="server" Value='<%Eval("uID") %>' />
                                                <%#Eval("rROLECODE")%>
                                            </td>
                                            <td style="background: #efefef;">
                                                <%#Eval("rROLENAME")%>
                                            </td>
                                            <td style="background: #efefef;">
                                                <%#Eval("cCOMPANYCODE")%>
                                            </td>
                                            <td style="background: #efefef;">
                                                <%#Eval("cCOMPNYFULLNAME")%>
                                            </td>
                                            <td style="background: white;">
                                                <asp:TextBox ID="rep_txtstartdate" runat="server" Text='<%#Eval("uROLESTARTDATE")%>' onClick="WdatePicker()" class="Wdate"></asp:TextBox>
                                                <asp:HiddenField ID="rep_hiddenid" runat="server" Value='<%#Eval("uID") %>' />
                                            </td>
                                            <td style="background: white;">
                                                <asp:TextBox ID="rep_enddate" runat="server" Text='<%#Eval("uROLEENDDATE")%>' onClick="WdatePicker()" class="Wdate"></asp:TextBox>
                                            </td>
                                            <td style="background: #efefef;">
                                                <asp:Button ID="btnDelete" runat="server" Text="删除" CommandArgument='<%#Eval("uID") %>'
                                                    OnCommand="btnDelete_click" OnClientClick="javascript:return DeleteIs();" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr>
                                            <td style="background: white;">
                                                <asp:HiddenField ID="hiddenID" runat="server" Value='<%Eval("uID") %>' />
                                                <%#Eval("rROLECODE")%>
                                            </td>
                                            <td style="background: white;">
                                                <%#Eval("rROLENAME")%>
                                            </td>
                                            <td style="background: white;">
                                                <%#Eval("cCOMPANYCODE")%>
                                            </td>
                                            <td style="background: white;">
                                                <%#Eval("cCOMPNYFULLNAME")%>
                                            </td>
                                            <td style="background: white;">
                                                <asp:TextBox ID="rep_txtstartdate" runat="server" Text='<%#Eval("uROLESTARTDATE")%>' onClick="WdatePicker()" class="Wdate"></asp:TextBox>
                                                <asp:HiddenField ID="rep_hiddenid" runat="server" Value='<%#Eval("uID") %>' />
                                            </td>
                                            <td style="background: white;">
                                                <asp:TextBox ID="rep_enddate" runat="server" Text='<%#Eval("uROLEENDDATE")%>' onClick="WdatePicker()" class="Wdate"></asp:TextBox>
                                            </td>
                                            <td style="background: white;">
                                                <asp:Button ID="btnDelete" runat="server" Text="删除" CommandArgument='<%#Eval("uID") %>'
                                                    OnCommand="btnDelete_click" OnClientClick="javascript:return DeleteIs();" />
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <asp:TextBox ID="txtUserInfo" runat="server" Style="display: none;"></asp:TextBox>
        </fieldset>
        <fieldset>
            <legend>岗位信息</legend>
            <div class="pure-control-group" style="margin-top: 10px;">
                <span style="display: inline-block; width: 1000px; text-align: right;">
                    <input type="button" runat="server" id="Button1" value="委派岗位" class="pure-button  pure-button-primary" style="margin-right: 20px;"
                        onclick="javascript: OpenModal2();" />
                    <asp:TextBox ID="txthecgangwei" runat="server" Style="display: none;"></asp:TextBox>
                    <asp:UpdatePanel ID="updatepanel2" runat="server">
                        <ContentTemplate>
                            <input type="button" runat="server" id="btnhecGangwei" onserverclick="btnhecGangwei_ServerClick"
                                style="display: none;" />
                            <table class="pure-table pure-table-bordered" style="width: 1000px; margin-top: 20px" id="tablegangwei">
                                <thead>
                                    <tr>
                                        <th>公司代码</th>
                                        <th>公司名称</th>
                                        <th>部门代码</th>
                                        <th>部门名称</th>
                                        <th>岗位代码</th>
                                        <th>岗位名称</th>
                                        <th>是否启用</th>
                                        <th>是否主岗位</th>
                                        <th>操作</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="repeatergangwei" runat="server">
                                        <ItemTemplate>
                                            <tr style="background: #efefef; text-align: center;">
                                                <td><%#Eval("COMPANY_CODE") %>
                                                    <asp:HiddenField ID ="hhdgangweiid" runat="server" Value='<%#Eval("ID") %>' />
                                                </td>
                                                <td><%#Eval("COMPANY_NAME") %></td>
                                                <td><%#Eval("UNIT_CODE") %></td>
                                                <td><%#Eval("UNIT_NAME") %></td>
                                                <td><%#Eval("POSITION_CODE") %></td>
                                                <td><%#Eval("POSITION_NAME") %></td>
                                                <td>
                                                    <input type="radio" name="enabled" id="enabledY" runat="server" value="Y" checked='<%#Eval("ENABLED_FLAG").ToString().Equals("Y")?true:false %>' />
                                                    <span>是</span>
                                                    <input type="radio" name="enabled" id="enabledN" runat="server" checked='<%#Eval("ENABLED_FLAG").ToString().Equals("N")?true:false %>' value="N" />
                                                    <span>否</span>
                                                </td>
                                                <td>
                                                    <input type="radio" id="primarykeyY" runat="server" name="primary" value="Y" checked='<%#Eval("PRIMARY_POSITION_FLAG").ToString().Equals("Y")?true:false %>' />
                                                    <span>是</span>
                                                    <input type="radio" id="primarykeyN" runat="server" name="primary" checked='<%#Eval("PRIMARY_POSITION_FLAG").ToString().Equals("N")?true:false %>' value="N" />
                                                    <span>否</span>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btngangweidel" CommandArgument='<%#Eval("ID") %>' runat="server" Text="删除" OnCommand="btngangweidel_Command" /></td>
                                            </tr>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                            <tr style="background: #fff; text-align: center;">
                                               <td><%#Eval("COMPANY_CODE") %>
                                                   <asp:HiddenField ID ="hhdgangweiid" runat="server" Value='<%#Eval("ID") %>' />
                                               </td>
                                                <td><%#Eval("COMPANY_NAME") %></td>
                                                <td><%#Eval("UNIT_CODE") %></td>
                                                <td><%#Eval("UNIT_NAME") %></td>
                                                <td><%#Eval("POSITION_CODE") %></td>
                                                <td><%#Eval("POSITION_NAME") %></td>
                                                <td>
                                                    <input type="radio" name="enabled" id="enabledY" runat="server" value="Y" checked='<%#Eval("ENABLED_FLAG").ToString().Equals("Y")?true:false %>' />
                                                    <span>是</span>
                                                    <input type="radio" name="enabled" id="enabledN" runat="server" checked='<%#Eval("ENABLED_FLAG").ToString().Equals("N")?true:false %>' value="N" />
                                                    <span>否</span>
                                                </td>
                                                <td>
                                                    <input type="radio" id="primarykeyY" runat="server" name="primary" value="Y" checked='<%#Eval("PRIMARY_POSITION_FLAG").ToString().Equals("Y")?true:false %>' />
                                                    <span>是</span>
                                                    <input type="radio" id="primarykeyN" runat="server" name="primary" checked='<%#Eval("PRIMARY_POSITION_FLAG").ToString().Equals("N")?true:false %>' value="N" />
                                                    <span>否</span>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btngangweidel" CommandArgument='<%#Eval("ID") %>' runat="server" Text="删除" OnCommand="btngangweidel_Command" /></td>
                                            </tr>
                                        </AlternatingItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </span>
            </div>
        </fieldset>
        <div style="margin-top: 10px; text-align: center;">
            <asp:Button ID="btnSave" runat="server" Text=" 确认 " CssClass="pure-button pure-button-primary"
                OnClick="Button1_Click" />&nbsp;&nbsp;
            <input type="button" id="btnCancel" value=" 取消 " onclick="javascript: ClosePage();"
                class="pure-button pure-button-primary" />
        </div>
    </div>
    <script type="text/javascript">
        function DeleteIs() {
            if (confirm('删除后无法再恢复,确定要删除吗?')) { return true; } return false;
        }
    </script>
</asp:Content>
