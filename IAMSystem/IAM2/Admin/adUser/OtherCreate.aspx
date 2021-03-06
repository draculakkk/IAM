﻿<%@ Page Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" CodeBehind="OtherCreate.aspx.cs" Inherits="IAM.Admin.adUser.OtherCreate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../Content/My97DatePicker/WdatePicker.js" type="text/javascript"></script>

    <script type="text/javascript">
        var pageStutas = "NaN";
        jQuery(document).ready(function () {
            var request = new GetRequest();
            //if (request["userid"] != undefined) {
            //jQuery.ControllerUserType("usertype");
            //}
            jQuery("#<%=txtLoginName.ClientID%>").blur(function () {
                if (jQuery(this).val() != "") {
                    AutoAddMail();
                }
            });
           
            var isadmin = "<%=IsAdmin.ToString()%>";
            if (isadmin != "True") {
                jQuery("input[type=button]").prop("disabled", true);
            }

            jQuery("#<%=txtNumber.ClientID%>").prop("disabled", false);
            jQuery("#usertype").prop("disabled", false);
            jQuery("#usertype").change(function () {
                var _value = jQuery(this).val();
                if (request["userid"] != undefined&&_value=="user") {
                    alert("其他类不能转移为员工类");
                    jQuery("#usertype").val("other");
                    return;
                }
                if (!confirm("当前所有已填写内容将会被清空\n你确定要更改人员类型吗？")) {
                    jQuery("#usertype").val("other");
                    return;
                }
                switch (_value) {
                    case "user":
                        window.location.href = "./UserCreate.aspx?ut=" + _value;

                        break;
                    case "other":
                        window.location.href = "./OtherCreate.aspx?ut=" + _value;

                        break;
                    case "system":
                        if (request["userid"] != undefined) {
                            window.location.href = "./SystemCreate.aspx?userid=" + request["userid"];
                        }
                        else {
                            window.location.href = "./SystemCreate.aspx";
                        }
                        break;
                }
            });
        });

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


    function IsDelete() {
        if (confirm("数据删除后将无法恢复\n确认删除？")) {
            return true;
        }
        return false;
    }

    function AutoAddMail() {
        var url = "../../ValidateLoginName.ashx?sys=AD";
        var _cn = jQuery("#<%=txtChineseName.ClientID%>").val();
        var _login = jQuery("#<%=txtLoginName.ClientID%>").val();
        var _isupdate = "0";
        var request = new GetRequest();
        if (request["userid"] != undefined)
        {
            _isupdate = "1";
        }
        jQuery.ajax({
            url: url,
            type: "post",
            dataType: "json",
            data: { cn: _cn, login: _login,isupdate:_isupdate },
            success: function (redata) {
                jQuery("#<%=txtChineseName.ClientID%>").val(redata["CN"]);
                jQuery("#<%=txtDisplayName.ClientID%>").val(redata["CN"]);
                jQuery("#<%=txtLoginName.ClientID%>").val(redata["LoginName"]);
                jQuery("#<%=txtEmail.ClientID%>,#<%=txtLyncNumber.ClientID%>").val(redata["LoginName"] + "@shac.com.cn");
            }
        });
    }

    function OpenModal() {
        if (jQuery("#<%=txtLoginName.Text %>").val() == "") {
            alert("请先行输入用户登陆名");
            return;
        }
        var _value = window.showModalDialog("ADWorkGroupManger.aspx", null, "dialogWidth=950px;dialogHeight=670px;");
        $("#<%=hiddkekonggroups.ClientID%>").val(_value);
            var objReturnBind = document.getElementById("<%=btnOnLoadRoleInfo.ClientID%>");
        objReturnBind.click();
    }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <fieldset style="border: 1px solid #efefef;">
        <legend style="font-size: 14px; color: #3C72DF">HR 信息</legend>
        <div class="pure-control-group" style="margin-top: 10px;">
            <table>
                <tr>
                    <td class="td-title-width">账号类别:
                    </td>
                    <td class="td-context-width">
                        <select id="usertype">

                            <option value="user">员工</option>
                            <option value="other" selected="selected">其他</option>
                            <option value="system">系统</option>
                        </select>
                    </td>
                    <td class="td-title-width">工号:
                    </td>
                    <td class="td-context-width">
                        <asp:TextBox ID="txtNumber" runat="server"></asp:TextBox>
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
        <legend style="font-size: 14px; color: #3C72DF">AD 信息</legend>
        <div class="pure-control-group" style="margin-top: 10px;">
            <table>
                <tr>
                    <td class="td-title-width"><font style="color: red;">*</font>CN:
                    </td>
                    <td class="td-context-width">
                        <asp:TextBox ID="txtChineseName" runat="server"></asp:TextBox>
                    </td>
                    <td class="td-title-width"><font style="color: red;">*</font>显示名:
                    </td>
                    <td class="td-context-width">
                        <asp:TextBox ID="txtDisplayName" runat="server"></asp:TextBox>
                    </td>
                    <td class="td-title-width"><font style="color: red;">*</font>登录名:
                    </td>
                    <td class="td-context-width">
                        <asp:TextBox ID="txtLoginName" runat="server"></asp:TextBox>
                        <asp:HiddenField ID="hiddenid" runat="server" />
                    </td>
                    <td class="td-title-width"><font style="color: red;">*</font>密码:
                    </td>
                    <td class="td-context-width">
                        <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td-title-width"><%--人员类别:--%>
                      
                        手机:
                    </td>
                    <td class="td-context-width">
                        <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
                    </td>
                    <td class="td-title-width">工作电话:
                    </td>
                    <td class="td-context-width">
                        <asp:TextBox ID="txtMobleNumber" runat="server"></asp:TextBox>
                    </td>
                    <td class="td-context-width" colspan="2">Enable 
                        <asp:CheckBox ID="chkEnable" Checked="true" runat="server" />
                    </td>
                    
                    <td class="td-title-width"><font style="color: red;">*</font>失效时间:
                    </td>
                    <td class="td-context-width">
                        <asp:TextBox ID="txtEnableDate" runat="server" onClick="WdatePicker()" class="Wdate"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td class="td-title-width"><font style="color: red;">*</font>描述:
                    </td>
                    <td class="td-context-width">
                        <asp:TextBox ID="txtDescription" runat="server"></asp:TextBox>
                    </td>
                    <td class="td-title-width">部门:
                    </td>
                    <td class="td-context-width">
                        <asp:DropDownList ID="dplDepartment" runat="server" OnSelectedIndexChanged="dplDepartment_SelectedIndexChanged"></asp:DropDownList>
                        <asp:HiddenField ID="hiddenDepartMentId" runat="server" />
                        <asp:HiddenField ID="hiddenPreMentId" runat="server" />
                    </td>
                    <td class="td-title-width">职务级别:
                    </td>
                    <td class="td-context-width" colspan="3">
                        <%--<asp:TextBox ID="txtPostLevel" runat="server"></asp:TextBox>--%>
                        <asp:DropDownList ID="dplZhiji" runat="server">
                            <asp:ListItem Value="   " Text="" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="td-title-width">
                        <asp:CheckBox ID="chkDisk" runat="server"  Visible="false"/>
                        盘符:
                    </td>
                    <td class="td-context-width">
                        <asp:TextBox ID="txtDisk" runat="server"></asp:TextBox>
                    </td>
                    <td class="td-title-width">映射盘符:
                    </td>
                    <td class="td-context-width" colspan="5">
                        <asp:TextBox ID="txtDiskNumber" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td-title-width">备注:
                    </td>
                    <td class="td-context-width" colspan="7">
                        <asp:TextBox ID="txtMemo" runat="server" TextMode="MultiLine" Width="90%" Height="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td-title-width"><asp:Button ID="btnShengcheng" runat="server"  Text=" 生 成 " OnClick="btnShengcheng_Click"/>
                    </td>
                    <td class="td-context-width" colspan="7" style="color:blue">
                      <font style="color:red">*</font>&nbsp;此按钮用于生成账号 描述、邮件数据库、默认组、部门组、职级组信息 
                    </td>
                </tr>
            </table>

        </div>
    </fieldset>
    <fieldset style="border: 1px solid #efefef;">
        <legend style="font-size: 14px; color: #3C72DF">Email&Lync</legend>
        <div class="pure-control-group" style="margin-top: 10px;">
            <table style="width: 90%">
                <tr>
                    <td class="td-title-width"><font style="color: red;">*</font>E-Mail:
                    </td>
                    <td class="td-context-width">
                        <asp:TextBox ID="txtEmail" Width="200" runat="server"></asp:TextBox>
                    </td>
                    <td class="td-context-width">邮箱数据库:
                    </td>
                    <td class="td-context-width">
                        <asp:TextBox ID="txtMailDataBase" runat="server"></asp:TextBox>
                    </td>
                    <td class="td-title-width"><font style="color: red;">*</font>Lync账号:
                    </td>
                    <td class="td-context-width" colspan="3">
                        <asp:TextBox ID="txtLyncNumber" Width="200" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
    </fieldset>
    <div style="width: 938px;">
        <div style="float: right; width: 430px; height: auto;">

         <asp:ScriptManager ID="scriptmanager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="updatepanel1" runat="server">
                <ContentTemplate>
                    <fieldset style="border: 1px solid #efefef;">
                        <legend style="font-size: 14px; color: #3C72DF">可控组信息</legend>
                        <label for="email" style="width: 100px">
                            添加可控组:</label>

                        <input type="button" value=" 添加 " runat="server" id="btnworkground" class="pure-button pure-button-primary" onclick="OpenModal();" />
                        <input type="button" value="" runat="server" id="btnOnLoadRoleInfo" onserverclick="btnOnLoadRoleInfo_ServerClick" style="display: none;" />
                        <asp:HiddenField ID="hiddkekonggroups" runat="server" />
                        <table class="pure-table pure-table-bordered" style="text-align: center; text-decoration: none; font-size: 14px; width: 100%">
                            <thead>
                                <tr align="center">
                                    <th style="width: 40%">组名
                                    </th>
                                    <th style="width: 35%">概述
                                    </th>
                                    <th style="width: 25%">操作</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="kekongGroupsRepeater" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td style="background: #fff;"><%#Eval("GroupName") %>
                                            </td>
                                            <td style="background: #fff;"><%#Eval("GroupName") %>
                                            </td>
                                            <td style="background: #fff;">[<asp:LinkButton ID="lbtnDelete" runat="server" Text="删除" CommandArgument='<%#Eval("ID") %>' OnCommand="lbtnDelete_Command" OnClientClick="return IsDelete();"></asp:LinkButton>]</td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr>
                                            <td style="background: #efefef;"><%#Eval("GroupName") %>
                                            </td>
                                            <td style="background: #efefef;"><%#Eval("GroupName") %>
                                            </td>
                                            <td style="background: #efefef;">[<asp:LinkButton ID="lbtnDelete" runat="server" Text="删除" CommandArgument='<%#Eval("ID") %>' OnCommand="lbtnDelete_Command" OnClientClick="return IsDelete();"></asp:LinkButton>]
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </fieldset>
                </ContentTemplate>
            </asp:UpdatePanel>   

        </div>
        <div style="float: left; width: 500px; height: auto;">
            <fieldset style="border: 1px solid #efefef;">
                <legend style="font-size: 14px; color: #3C72DF">默认组信息</legend>
                <table class="pure-table pure-table-bordered" style="text-align: center; text-decoration: none; font-size: 14px; width: 100%">
                    <thead>
                        <tr align="center">
                            <th style="width: 40%">组名
                            </th>
                            <th style="width: 35%">概述
                            </th>

                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="DefaultGroupRepeater" runat="server">
                            <ItemTemplate>

                                <tr>

                                    <td style="background: #fff;"><%#Eval("GroupName") %>
                                    </td>
                                    <td style="background: #fff;"><%#Eval("GroupName") %>
                                    </td>
                                </tr>

                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr>

                                    <td style="background: #efefef;"><%#Eval("GroupName") %>

                                    </td>
                                    <td style="background: #efefef;"><%#Eval("GroupName") %>
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </fieldset>
        </div>
        <div style="margin-top: 10px; text-align: center;">
            <asp:Button ID="Button1" runat="server" Text=" 确 认 " CssClass="pure-button pure-button-primary" OnClick="Button1_Click" />&nbsp;&nbsp;
       <input id="Button2" type="button" value=" 取 消 " class="pure-button pure-button-primary" onclick ="javascript: window.close(true); return false;" />
        </div>


    </div>
</asp:Content>
