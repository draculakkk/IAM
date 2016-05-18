<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true"
    CodeBehind="ADInfoManager.aspx.cs" Inherits="IAM.Admin.ADInfo" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Content/My97DatePicker/WdatePicker.js" type="text/javascript"></script>

    <script type="text/javascript">
        jQuery(document).ready(function () {
            //jQuery.ControllerUserType("");
            jQuery("input").prop("disabled", true);
            jQuery("#usertype").change(function () {
                var _value = jQuery(this).val();
                
                switch (_value)
                {
                    case "user": window.location.href = "./aduser/UserCreate.aspx?ut="+_value; break
                    case "other": window.location.href = "./aduser/OtherCreate.aspx?ut=" + _value; break;
                    case "system": window.location.href = "./aduser/SystemCreate.aspx?ut=" + _value; break;
                }
            });
        });

        function IsDelete()
        {
            if (confirm("数据删除后将无法恢复\n确认删除？"))
            {
                return true;
            }
            return false;
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
                       <select  id="usertype">
                           <option value="" selected="selected"></option>
                           <option value="user">员工</option>
                           <option value="other">其他</option>
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
                    <td class="td-title-width">CN:
                    </td>
                    <td class="td-context-width">
                        <asp:TextBox ID="txtChineseName" runat="server"></asp:TextBox>
                    </td>
                    <td class="td-title-width">显示名:
                    </td>
                    <td class="td-context-width">
                        <asp:TextBox ID="txtDisplayName" runat="server"></asp:TextBox>
                    </td>
                    <td class="td-title-width">登录名:
                    </td>
                    <td class="td-context-width">
                        <asp:TextBox ID="txtLoginName" runat="server"></asp:TextBox>
                    </td>
                    <td class="td-title-width">密码:
                    </td>
                    <td class="td-context-width">
                        <asp:TextBox ID="txtPassword" TextMode="Password" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="td-title-width"><%--人员类别:--%>
                      
                        手机:
                    </td>
                    <td class="td-context-width">
                       <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>  
                    </td>
                    <td class="td-title-width" colspan="2">
                        Enable  <asp:CheckBox ID="chkEnable" Checked="true" runat="server" />
                    </td>
                    <td class="td-title-width">工作电话:
                    </td>
                    <td class="td-context-width">
                        <asp:TextBox ID="txtMobleNumber" runat="server"></asp:TextBox>
                    </td>
                    <td class="td-title-width">有效期至:
                    </td>
                    <td class="td-context-width">
                         <asp:TextBox ID="txtEnableDate" runat="server" onClick="WdatePicker()" class="Wdate"></asp:TextBox>
                    </td>
                </tr>
                
                <tr>
                    <td class="td-title-width">描述:
                    </td>
                    <td class="td-context-width">
                        <asp:TextBox ID="txtDescription" runat="server"></asp:TextBox>
                    </td>
                    <td class="td-title-width">部门:
                    </td>
                    <td class="td-context-width">
                        <asp:TextBox ID="txtDepartMent" runat="server"></asp:TextBox>
                        <asp:HiddenField ID="hiddenDepartMentId" runat="server" />
                        <asp:HiddenField ID="hiddenPreMentId" runat="server" />
                    </td>
                    <td class="td-title-width">职务级别:
                    </td>
                    <td class="td-context-width" colspan="3">
                        <%--<asp:TextBox ID="txtPostLevel" runat="server"></asp:TextBox>--%>
                        <asp:DropDownList ID="dplZhiji" runat="server" AutoPostBack="true"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="td-title-width">
                        <asp:CheckBox ID="chkDisk" runat="server" />
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
                    <td class="td-title-width">
                       
                        备注:
                    </td>
                    <td class="td-context-width" colspan="7">
                        <asp:TextBox ID="txtMemo" runat="server" TextMode="MultiLine" Width="90%" Height="50"></asp:TextBox>
                    </td>
                    
                   
                </tr>
            </table>

        </div>
    </fieldset>
    <fieldset style="border: 1px solid #efefef;">
        <legend style="font-size: 14px; color: #3C72DF">Email&Lync</legend>
        <div class="pure-control-group" style="margin-top: 10px;">
            <table>
                <tr>
                    <td class="td-title-width">E-Mail:
                    </td>
                    <td class="td-context-width">
                        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                    </td>
                    <td class="td-context-width">邮箱数据库:
                    </td>
                    <td class="td-context-width">
                        <asp:TextBox ID="txtMailDataBase" runat="server"></asp:TextBox>
                    </td>
                    <td class="td-title-width">Lync账号:
                    </td>
                    <td class="td-context-width" colspan="3">
                        <asp:TextBox ID="txtLyncNumber" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
    </fieldset>
    <div style="width: 938px;">
        <div style="float: right; width: 430px; height: auto;">
            <fieldset style="border: 1px solid #efefef;">
                <legend style="font-size: 14px; color: #3C72DF">组信息</legend>
                <label for="email" style="width: 100px">
                    组名:</label>
                <asp:DropDownList ID="ddlWorkGroup" runat="server" Width="240"></asp:DropDownList>

                <asp:Button ID="btnCreateGroup" runat="server" Text=" 添加 "
                    CssClass="pure-button pure-button-primary" OnClick="btnCreateGroup_Click" />
                <table class="pure-table pure-table-bordered" style="text-align: center; text-decoration: none; font-size: 14px; width: 100%">
                    <thead>
                        <tr align="center">
                            <th style="width: 40%">组名
                            </th>
                            <th style="width: 35%">概述
                            </th>

                            <th style="width: 25%">操作
                            </th>

                        </tr>
                    </thead>
                     <tbody>
                    
                         </tbody>
                </table>
            </fieldset>
        </div>
        <div style="float: left; width: 500px; height: auto;">
            <fieldset style="border: 1px solid #efefef;">
                <legend style="font-size: 14px; color: #3C72DF">计算机信息</legend>
                 <input type="button" runat="server" id="btnComputer" value=" 添加 " class="pure-button pure-button-primary" onclick="OpenPage1('AD_ComputerCreate.aspx');" />
                <%--<asp:Button ID="btnCreateComputer" runat="server" Text=" 添加 "
                    CssClass="pure-button pure-button-primary" OnClick="btnCreateComputer_Click" />--%>
                <table class="pure-table pure-table-bordered" style="text-align: center; text-decoration: none; font-size: 14px; width: 100%">
                    <thead>
                        <tr>
                            <th style="width: 20%">计算机名
                            </th>
                            <th style="width: 35%">描述
                            </th>
                            <th style="width: 20%">有效期
                            </th>
                            <th style="width: 25%">操作
                            </th>

                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="repeaterComputer" runat="server">
                            <ItemTemplate>
                                <tr>

                                    <td>
                                        <%#Eval("name") %>
                                    </td>
                                    <td>
                                        <%#Eval("DESCRIPTION") %>
                                    </td>
                                    <td>
                                        <%#Eval("ExpiryDate") %>
                                    </td>

                                    <td>[<a href="#" onclick="OpenPage1('AD_ComputerCreate.aspx?id=<%#Eval("Name") %>');">编辑</a>]&nbsp;[<asp:LinkButton ID="lbtncomdel" runat="server" CommandArgument='<%#Eval("Name") %>' OnCommand="lbtncomdel_Command">删除</asp:LinkButton>]
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr>

                                    <td>
                                        <%#Eval("name") %>
                                    </td>
                                    <td>
                                        <%#Eval("DESCRIPTION") %>
                                    </td>
                                    <td>
                                        <%#Eval("ExpiryDate") %>
                                    </td>

                                    <td>[<a href="#" onclick="OpenPage1('AD_ComputerCreate.aspx?id=<%#Eval("Name") %>');">编辑</a>]&nbsp;[<asp:LinkButton ID="lbtncomdel" runat="server" CommandArgument='<%#Eval("Name") %>' OnCommand="lbtncomdel_Command">删除</asp:LinkButton>]
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </fieldset>
        </div>
        <div style="margin-top: 10px; text-align: center;">
            <asp:Button ID="Button1" runat="server" Text=" 确认 " CssClass="pure-button pure-button-primary" OnClick="Button1_Click" />&nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" Text=" 取消 " OnClientClick="javascript:window.close();" CssClass="pure-button pure-button-primary" />
        </div>


    </div>
</asp:Content>
