<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" CodeBehind="RoleTemplateInfoPage.aspx.cs" Inherits="IAM.Admin.RoleTemplateInfoPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $("#<%=dplSystemType.ClientID%>").on("change", function () {
                $("#hiddentype").val($("#<%=dplSystemType.ClientID%>").val());
            });
        });
        function IsDelete() {
            if (confirm("确定要删除这行数据吗？\n数据删除后将无法恢复")) {
                return true;
            }
            return false;
        }

        function OpenUserList() {
            var _SystemName = jQuery("#hiddentype").val();
            var url = "userlist.aspx?systype=" + _SystemName;
            window.open(url, '_blank', "width=800px,height=1100px,toolbar=false,menu=no");
        }

        function OpenDialogAddRole(_SystemName) {
            var url = "";
            var _width = 0;
            var _height = 0;
            var tmp = window.location.search;
            var _templateId = tmp.substring(4, tmp.length);
            var _usertype = "";
            var _username = "";
            switch (_SystemName) {
                case "HR":
                    url = "HRRoleList.aspx"; _width = 950; _height = 670;
                    _username = $("#<%=txthrusername.ClientID%>").val();
                    _usertype = $("#<%=dplhrusertype.ClientID%>").val();
                    break;
                case "HEC":
                    url = "HECRoleAndCompany.aspx"; _width = 809; _height = 236;
                    _username = $("#<%=txthecusername.ClientID%>").val();
                    _usertype = $("#<%=dplusertype.ClientID%>").val();
                    break;
                case "HEC2":
                    url = "HECGangwei.aspx"; _width = 409; _height = 436;
                    _username = $("#<%=txtgangweizhanghao.ClientID%>").val();
                    _usertype = $("#<%=ddlgangweizhanghaoleixing.ClientID%>").val();
                    break;
                case "SAP": url = "SapRoleList.aspx"; _width = 650; _height = 400;
                    _username = $("#<%=txtsapusername.ClientID%>").val();
                    _usertype = $("#<%=dplSapUserType.ClientID%>").val();
                    break;
                case "AD":
                    _username = $("#<%=txtadusername.ClientID%>").val();
                    _usertype = $("#<%=dpladusertype.ClientID%>").val();
                    url = "./aduser/ADWorkGroupManger.aspx"; _width = 950; _height = 670;
                    break;
                case "ADComputer":
                    _username = $("#<%=txtadcomputername.ClientID%>").val();
                    _usertype = $("#<%=dplcomputertype.ClientID%>").val();
                    url = "./aduser/ADWorkGroupManger.aspx?type=adcomputer"; _width = 950; _height = 470;
                    break;
                case "TC":
                    url = "TCRolelist.aspx"; _height = 636; _width = 400;
                    _username = $("#<%=txttcusername.ClientID%>").val();
                    _usertype = $("#<%=dpltcusertype.ClientID%>").val();
                    break;
            }

            if (_usertype != "员工" && _username == "") {
                alert("请填写账号！");
                return;
            }
            var _value = window.showModalDialog(url, null, "dialogWidth=" + _width + "px;dialogHeight=" + _height + "px;");
            if (_value == "" || _value == undefined) {
                return;
            }
            alert(_value);

            jQuery.post("../ExcelExportAjax.ashx?type=addTemplateInfo", { tempid: _templateId, sysname: _SystemName, rolelist: _value, username: _username, usertype: _usertype }, function (data) {
                if (data == "ok") {
                    alert("添加成功");
                    window.location.href = window.location.href;
                }
                else if (data.indexOf("error:") != -1) {
                    alert(data);
                }
            }, "text");
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="pure-control-group" style="margin-top: 10px;">
        <label for="email" style="width: auto; color: red;">
            模版名称:</label>
        <asp:Label ID="lblTemplateName" runat="server" ForeColor="Red"></asp:Label>
    </div>

    <div class="pure-control-group" style="margin-top: 10px;">
        <div class="pure-control-group" style="margin-top: 10px;">
            <table>
                <tr>
                    <th colspan="8" align="left">账号添加</th>
                </tr>
                <tr>
                    <td class="td-title-width">系统类型:
                    </td>
                    <td class="td-context-width">
                        <asp:DropDownList ID="dplSystemType" runat="server">
                            <asp:ListItem Text="AD" Value="AD"></asp:ListItem>
                            <asp:ListItem Text="ADComputer" Value="ADComputer"></asp:ListItem>
                            <asp:ListItem Text="HR" Value="HR"></asp:ListItem>
                            <asp:ListItem Text="HEC" Value="HEC"></asp:ListItem>
                            <asp:ListItem Text="SAP" Value="HEC"></asp:ListItem>
                            <asp:ListItem Text="TC" Value="HEC"></asp:ListItem>

                        </asp:DropDownList>
                        <input type="hidden" id="hiddentype" value="AD" />
                    </td>
                    <td class="td-title-width">账号类型:
                    </td>
                    <td class="td-context-width">
                        <asp:DropDownList ID="dplUserType111" runat="server">
                            <asp:ListItem Text="员工" Value="员工"></asp:ListItem>
                            <asp:ListItem Text="其他" Value="其他"></asp:ListItem>
                            <asp:ListItem Text="系统" Value="系统"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="td-title-width">账号:
                    </td>
                    <td class="td-context-width" colspan="3" style="">
                        <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
                        <input type="button" value="选择账号" onclick="OpenUserList();" class="pure-button pure-button-primary" />
                        <asp:Button ID="btnAdd" runat="server" Text=" 添 加 " CssClass="pure-button pure-button-primary" OnClick="btnAdd_Click" />
                    </td>
                </tr>
            </table>

        </div>
    </div>

    <fieldset>
        <legend>域控系统账号</legend>
        <div style="width: 1100px;">
            <input style="float: right;" type="button" value="添加" onclick="OpenDialogAddRole('AD');" />
            &nbsp;&nbsp;
            <asp:TextBox ID="txtadusername" Style="float: right;" runat="server"></asp:TextBox>
            <span style="float: right; margin: 0 10px; display: block;">账号:</span>
            <asp:DropDownList ID="dpladusertype" Style="float: right;" runat="server">
                <asp:ListItem Value="员工" Text="员工"></asp:ListItem>
                <asp:ListItem Value="其他" Text="其他"></asp:ListItem>
                <asp:ListItem Value="系统" Text="系统"></asp:ListItem>
            </asp:DropDownList><span style="float: right; margin-left: 10px;">账号类型:</span>
        </div>
        <table class="pure-table pure-table-bordered" id="table3" style="text-align: center; text-decoration: none; font-size: 14px; width: 1100px;">
            <thead>
                <tr align="center">
                    <th style="width: 150px;">账号</th>
                    <th style="width: 100px">账号类型</th>
                    <th style="width: 200px">组
                    </th>
                    <th style="width: 520px"></th>
                    <th style="width: 130px">操作
                    </th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="repeaterAD" runat="server">
                    <ItemTemplate>
                        <tr align="center">
                            <td style="background-color: #efefef"><%#Eval("p1").ToString().Equals("员工")?"":Eval("p2") %></td>
                            <td style="background-color: #efefef"><%#Eval("p1") %></td>
                            <td style="background-color: #efefef">
                                <%#Eval("RoleName")%>
                            </td>
                            <td style="background-color: #efefef"></td>
                            <td style="background-color: #efefef">[<asp:LinkButton ID="btnsapDelete" runat="server" OnClientClick="javascript: return IsDelete();" Text="删除" Style="text-decoration: none;" CommandArgument='<%#Eval("ID") %>' OnCommand="btnsapDelete_Command"></asp:LinkButton>]</td>

                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr>
                            <td style="background-color: white"><%#Eval("p1").ToString().Equals("员工")?"":Eval("p2") %></td>
                            <td style="background-color: white"><%#Eval("p1") %></td>
                            <td style="background-color: white">
                                <%#Eval("RoleName")%>
                            </td>
                            <td style="background-color: white"></td>
                            <td style="background-color: white">[<asp:LinkButton ID="btnsapDelete" runat="server" OnClientClick="javascript: return IsDelete();" Text="删除" Style="text-decoration: none;" CommandArgument='<%#Eval("ID") %>' OnCommand="btnsapDelete_Command"></asp:LinkButton>]</td>

                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </fieldset>

    <fieldset>
        <legend>计算机</legend>
        <div style="width: 1100px;">
            <input style="float: right;" type="button" value="添加" onclick="OpenDialogAddRole('ADComputer');" />
            &nbsp;&nbsp;
            <asp:TextBox ID="txtadcomputername" Style="float: right;" runat="server"></asp:TextBox>
            <span style="float: right; margin: 0 10px;">账号:</span>
            &nbsp;&nbsp;<asp:DropDownList ID="dplcomputertype" Style="float: right;" runat="server">
                <asp:ListItem Value="员工" Text="员工"></asp:ListItem>
                <asp:ListItem Value="其他" Text="其他"></asp:ListItem>
                <asp:ListItem Value="系统" Text="系统"></asp:ListItem>
            </asp:DropDownList><span style="float: right; margin: 0 10px">账号类型:</span>
        </div>
        <table class="pure-table pure-table-bordered" id="table5" style="text-align: center; text-decoration: none; font-size: 14px; width: 1100px;">
            <thead>
                <tr align="center">
                    <th style="width: 150px">计算机名称
                    </th>
                    <th style="width: 100px">账号类型</th>
                    <th style="width: 200px">组
                    </th>
                    <th style="width: 530px"></th>
                    <th style="width: 130px">操作
                    </th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="repeaterComputer" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="background-color: #efefef">
                                <%#Eval("RoleID")%>
                            </td>
                            <td style="background-color: #efefef"><%#Eval("p1") %></td>
                            <td style="background-color: #efefef">
                                <%#Eval("RoleName")%>
                            </td>
                            <td style="background-color: #efefef"></td>
                            <td style="background-color: #efefef">[<asp:LinkButton ID="btnsapDelete" runat="server" OnClientClick="javascript: return IsDelete();" Text="删除" Style="text-decoration: none;" CommandArgument='<%#Eval("ID") %>' OnCommand="btnsapDelete_Command"></asp:LinkButton>]
                            </td>

                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr>
                            <td style="background-color: white">
                                <%#Eval("RoleID")%>
                            </td>

                            <td style="background-color: white"><%#Eval("p1") %></td>
                            <td style="background-color: white">
                                <%#Eval("RoleName")%>
                            </td>
                            <td style="background-color: white"></td>
                            <td style="background-color: white">[<asp:LinkButton ID="btnsapDelete" runat="server" OnClientClick="javascript: return IsDelete();" Text="删除" Style="text-decoration: none;" CommandArgument='<%#Eval("ID") %>' OnCommand="btnsapDelete_Command"></asp:LinkButton>]
                            </td>

                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </fieldset>

    <fieldset>
        <legend>SAP系统账号</legend>
        <div style="width: 1100px;">
            <input style="float: right;" type="button" value="添加" onclick="javascript: OpenDialogAddRole('SAP');" />
            &nbsp;&nbsp;
            <asp:TextBox ID="txtsapusername" Style="float: right;" runat="server"></asp:TextBox>
            <span style="float: right;">账号:</span>
            &nbsp;&nbsp;<asp:DropDownList ID="dplSapUserType" Style="float: right;" runat="server">
                <asp:ListItem Value="员工" Text="员工"></asp:ListItem>
                <asp:ListItem Value="其他" Text="其他"></asp:ListItem>
                <asp:ListItem Value="系统" Text="系统"></asp:ListItem>

            </asp:DropDownList><span style="float: right;">账号类型:</span>
        </div>
        <table class="pure-table pure-table-bordered" id="table1" style="text-align: center; text-decoration: none; font-size: 14px; width: 1100px;">
            <thead>
                <tr align="center">
                    <th style="width: 150px">账号</th>
                    <th style="width: 100px">账号类型</th>
                    <th style="width: 180px">角色
                    </th>
                    <th style="width: 180px">角色名称
                    </th>

                    <th style="width: 180px">有效期从
                    </th>
                    <th style="width: 180px">有效期至
                    </th>
                    <th style="width: 130px">操作
                    </th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="repeater1SAPuserRole" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="background-color: #efefef"><%#Eval("p2") %></td>
                            <td style="background-color: #efefef"><%#Eval("p1") %></td>
                            <td style="background-color: #efefef">
                                <%#Eval("RoleName") %>
                            </td>
                            <td style="background-color: #efefef">
                                <%#Eval("CompanyName") %>
                            </td>
                            <td style="background-color: #efefef">
                                <%#Eval("StartDate") %>
                            </td>
                            <td style="background-color: #efefef">
                                <%#Eval("EndDate") %>
                            </td>
                            <td style="background-color: #efefef">[<asp:LinkButton ID="btnsapDelete" runat="server" OnClientClick="javascript: return IsDelete();" Text="删除" Style="text-decoration: none;" CommandArgument='<%#Eval("ID") %>' OnCommand="btnsapDelete_Command"></asp:LinkButton>] 
                            </td>

                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr>
                            <td style="background-color: white"><%#Eval("p1").ToString().Equals("员工")?"":Eval("p2") %></td>
                            <td style="background-color: white"><%#Eval("p1") %></td>
                            <td style="background-color: white"><%#Eval("RoleName") %></td>
                            <td style="background-color: white"><%#Eval("CompanyName") %></td>
                            <td style="background-color: white"><%#Eval("StartDate") %></td>
                            <td style="background-color: white"><%#Eval("EndDate") %></td>
                            <td style="background-color: white">[<asp:LinkButton ID="btnsapDelete" runat="server" OnClientClick="javascript: return IsDelete();" Text="删除" Style="text-decoration: none;" CommandArgument='<%#Eval("ID") %>' OnCommand="btnsapDelete_Command"></asp:LinkButton>]</td>
                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </fieldset>
    <fieldset>
        <legend>人事管理系统账号</legend>
        <div style="width: 1100px;">
            <input style="float: right;" type="button" value="添加" onclick="javascript: OpenDialogAddRole('HR');" />
            &nbsp;&nbsp;
            <asp:TextBox ID="txthrusername" Style="float: right;" runat="server"></asp:TextBox>
            <span style="float: right;">账号:</span>
            &nbsp;&nbsp;<asp:DropDownList ID="dplhrusertype" Style="float: right;" runat="server">
                <asp:ListItem Value="员工" Text="员工"></asp:ListItem>
                <asp:ListItem Value="其他" Text="其他"></asp:ListItem>
                <asp:ListItem Value="系统" Text="系统"></asp:ListItem>
            </asp:DropDownList><span style="float: right;">账号类型:</span>
        </div>
        <table class="pure-table pure-table-bordered" id="table2" style="text-align: center; text-decoration: none; font-size: 14px; width: 1100px;">
            <thead>
                <tr align="center">
                    <th style="width: 150px">账号</th>
                    <th style="width: 100px">账号类型</th>
                    <th style="width: 180px">角色名称
                    </th>
                    <th style="width: 180px">公司名称
                    </th>
                    <th style="width: 180px">有效期从
                    </th>
                    <th style="width: 180px">有效期至
                    </th>
                    <th style="width: 130px">操作
                    </th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="repeaterHRUserRole" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="background-color: #efefef"><%#Eval("p1").ToString().Equals("员工")?"":Eval("p2") %></td>
                            <td style="background-color: #efefef"><%#Eval("p1") %></td>

                            <td style="background-color: #efefef">
                                <%#Eval("RoleName") %>
                            </td>
                            <td style="background-color: #efefef">
                                <%#Eval("CompanyName") %>
                            </td>
                            <td style="background-color: #efefef">
                                <%#Eval("StartDate") %>
                            </td>
                            <td style="background-color: #efefef">
                                <%#Eval("EndDate") %>
                            </td>
                            <td style="background-color: #efefef">[<asp:LinkButton ID="btnsapDelete" runat="server" OnClientClick="javascript: return IsDelete();" Text="删除" Style="text-decoration: none;" CommandArgument='<%#Eval("ID") %>' OnCommand="btnsapDelete_Command"></asp:LinkButton>] 
                            </td>

                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr>
                            <td style="background-color: white"><%#Eval("p1").ToString().Equals("员工")?"":Eval("p2") %></td>
                            <td style="background-color: white"><%#Eval("p1") %></td>
                            <td style="background-color: white"><%#Eval("RoleName") %></td>
                            <td style="background-color: white"><%#Eval("CompanyName") %></td>
                            <td style="background-color: white"><%#Eval("StartDate") %></td>
                            <td style="background-color: white"><%#Eval("EndDate") %></td>
                            <td style="background-color: white">[<asp:LinkButton ID="btnsapDelete" runat="server" OnClientClick="javascript: return IsDelete();" Text="删除" Style="text-decoration: none;" CommandArgument='<%#Eval("ID") %>' OnCommand="btnsapDelete_Command"></asp:LinkButton>]</td>
                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </fieldset>
    <fieldset>
        <legend>预算管理系统账号(角色)</legend>
        <div style="width: 1100px;">
            <input style="float: right;" type="button" value="添加" onclick="javascript: OpenDialogAddRole('HEC');" />
            &nbsp;&nbsp;
            <asp:TextBox ID="txthecusername" Style="float: right;" runat="server"></asp:TextBox>
            <span style="float: right;">账号:</span>
            &nbsp;&nbsp;<asp:DropDownList ID="dplusertype" Style="float: right;" runat="server">
                <asp:ListItem Value="员工" Text="员工"></asp:ListItem>
                <asp:ListItem Value="其他" Text="其他"></asp:ListItem>
                <asp:ListItem Value="系统" Text="系统"></asp:ListItem>
            </asp:DropDownList><span style="float: right;">账号类型:</span>
        </div>
        <table class="pure-table pure-table-bordered" style="text-align: center; text-decoration: none; font-size: 14px; width: 1100px;">
            <thead>
                <tr align="center">
                    <th style="width: 150px">账号</th>
                    <th style="width: 100px">账号类型</th>

                    <th style="width: 180px">角色名称
                    </th>
                    <th style="width: 180px">公司名称
                    </th>
                    <th style="width: 180px">有效期从
                    </th>
                    <th style="width: 180px">有效期至
                    </th>
                    <th style="width: 130px">操作
                    </th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="repeater1HECUserrole" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="background-color: #efefef"><%#Eval("p1").ToString().Equals("员工")?"":Eval("p2") %></td>
                            <td style="background-color: #efefef"><%#Eval("p1") %></td>

                            <td style="background-color: #efefef">
                                <%#Eval("RoleName") %>
                            </td>
                            <td style="background-color: #efefef">
                                <%#Eval("CompanyName") %>
                            </td>
                            <td style="background-color: #efefef">
                                <%#Eval("StartDate") %>
                            </td>
                            <td style="background-color: #efefef">
                                <%#Eval("EndDate") %>
                            </td>
                            <td style="background-color: #efefef">[<asp:LinkButton ID="btnsapDelete" runat="server" OnClientClick="javascript: return IsDelete();" Text="删除" Style="text-decoration: none;" CommandArgument='<%#Eval("ID") %>' OnCommand="btnsapDelete_Command"></asp:LinkButton>] 
                            </td>

                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr>
                            <td style="background-color: white"><%#Eval("p1").ToString().Equals("员工")?"":Eval("p2") %></td>
                            <td style="background-color: white"><%#Eval("p1") %></td>
                            <td style="background-color: white"><%#Eval("RoleName") %></td>
                            <td style="background-color: white"><%#Eval("CompanyName") %></td>
                            <td style="background-color: white"><%#Eval("StartDate") %></td>
                            <td style="background-color: white"><%#Eval("EndDate") %></td>
                            <td style="background-color: white">[<asp:LinkButton ID="btnsapDelete" runat="server" OnClientClick="javascript: return IsDelete();" Text="删除" Style="text-decoration: none;" CommandArgument='<%#Eval("ID") %>' OnCommand="btnsapDelete_Command"></asp:LinkButton>]</td>
                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </fieldset>
    <fieldset>
        <legend>预算管理系统账号(岗位)</legend>
        <div style="width: 1100px;">
            <input style="float: right;" type="button" value="添加" onclick="javascript: OpenDialogAddRole('HEC2');" />
            &nbsp;&nbsp;
            <asp:TextBox ID="txtgangweizhanghao" Style="float: right;" runat="server"></asp:TextBox>
            <span style="float: right;">账号:</span>
            &nbsp;&nbsp;<asp:DropDownList ID="ddlgangweizhanghaoleixing" Style="float: right;" runat="server">
                <asp:ListItem Value="员工" Text="员工"></asp:ListItem>
                <asp:ListItem Value="其他" Text="其他"></asp:ListItem>
                <asp:ListItem Value="系统" Text="系统"></asp:ListItem>
            </asp:DropDownList><span style="float: right;">账号类型:</span>
        </div>
        <table class="pure-table pure-table-bordered" style="text-align: center; text-decoration: none; font-size: 14px; width: 1100px;">
            <thead>
                <tr align="center">
                    <th style="width: 150px">账号</th>
                    <th style="width: 100px">账号类型</th>

                    <th style="width: 180px">公司名称(代码)
                    </th>
                    <th style="width: 180px">部门名称(代码)
                    </th>
                    <th style="width: 180px">岗位名称(代码)
                    </th>

                    <th style="width: 130px">操作
                    </th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="repeater1" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="background-color: #efefef"><%#Eval("p1").ToString().Equals("员工")?"":Eval("p2") %></td>
                            <td style="background-color: #efefef"><%#Eval("p1") %></td>

                            <td style="background-color: #efefef">
                                <%#Eval("CompanyName").ToString().Split("^".ToArray(),StringSplitOptions.RemoveEmptyEntries)[1] %>(<%#Eval("CompanyName").ToString().Split("^".ToArray(),StringSplitOptions.RemoveEmptyEntries)[0] %>)
                            </td>
                            <td style="background-color: #efefef">
                                <%#Eval("RoleName").ToString().Split("^".ToArray(),StringSplitOptions.RemoveEmptyEntries)[1] %>(<%#Eval("RoleName").ToString().Split("^".ToArray(),StringSplitOptions.RemoveEmptyEntries)[0] %>)
                            </td>
                            <td style="background-color: #efefef">
                                <%#Eval("RoleName").ToString().Split("^".ToArray(),StringSplitOptions.RemoveEmptyEntries)[3] %>(<%#Eval("RoleName").ToString().Split("^".ToArray(),StringSplitOptions.RemoveEmptyEntries)[2] %>)
                            </td>

                            <td style="background-color: #efefef">[<asp:LinkButton ID="btnsapDelete" runat="server" OnClientClick="javascript: return IsDelete();" Text="删除" Style="text-decoration: none;" CommandArgument='<%#Eval("ID") %>' OnCommand="btnsapDelete_Command"></asp:LinkButton>] 
                            </td>

                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr>
                            <td style="background-color: #fff"><%#Eval("p1").ToString().Equals("员工")?"":Eval("p2") %></td>
                            <td style="background-color: #fff"><%#Eval("p1") %></td>

                            <td style="background-color: #fff">
                                <%#Eval("CompanyName").ToString().Split("^".ToArray(),StringSplitOptions.RemoveEmptyEntries)[1] %>(<%#Eval("CompanyName").ToString().Split("^".ToArray(),StringSplitOptions.RemoveEmptyEntries)[0] %>)
                            </td>
                            <td style="background-color: #fff">
                                <%#Eval("RoleName").ToString().Split("^".ToArray(),StringSplitOptions.RemoveEmptyEntries)[1] %>(<%#Eval("RoleName").ToString().Split("^".ToArray(),StringSplitOptions.RemoveEmptyEntries)[0] %>)
                            </td>
                            <td style="background-color: #fff">
                                <%#Eval("RoleName").ToString().Split("^".ToArray(),StringSplitOptions.RemoveEmptyEntries)[3] %>(<%#Eval("RoleName").ToString().Split("^".ToArray(),StringSplitOptions.RemoveEmptyEntries)[2] %>)
                            </td>

                            <td style="background-color: #fff">[<asp:LinkButton ID="btnsapDelete" runat="server" OnClientClick="javascript: return IsDelete();" Text="删除" Style="text-decoration: none;" CommandArgument='<%#Eval("ID") %>' OnCommand="btnsapDelete_Command"></asp:LinkButton>] 
                            </td>

                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </fieldset>



    <fieldset>
        <legend>TC系统账号</legend>
        <div style="width: 1100px;">
            <input style="float: right;" type="button" value="添加" onclick="javascript: OpenDialogAddRole('TC');" />
            &nbsp;&nbsp;
            <asp:TextBox ID="txttcusername" Style="float: right;" runat="server"></asp:TextBox>
            <span style="float: right; margin: 0 10px">账号:</span>
            &nbsp;&nbsp;<asp:DropDownList ID="dpltcusertype" Style="float: right;" runat="server">
                <asp:ListItem Value="员工" Text="员工"></asp:ListItem>
                <asp:ListItem Value="其他" Text="其他"></asp:ListItem>
                <asp:ListItem Value="系统" Text="系统"></asp:ListItem>
            </asp:DropDownList><span style="float: right; margin: 0 10px">账号类型:</span>
        </div>
        <table class="pure-table pure-table-bordered" id="table4" style="text-align: center; text-decoration: none; font-size: 14px; width: 1100px;">
            <thead>
                <tr align="center">
                    <th style="width: 150px;">账号</th>
                    <th style="width: 100px">账号类型</th>
                    <th style="width: 200px">组
                    </th>

                    <th style="width: 200px;">角色</th>
                    <th style="width: 370px;"></th>
                    <th style="width: 130px">操作
                    </th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="repeaterTC" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="background-color: #efefef"><%#Eval("p1").ToString().Equals("员工")?"":Eval("p2") %></td>
                            <td style="background-color: #efefef"><%#Eval("p1") %></td>
                            <td style="background-color: #efefef">
                                <%#IAM.BLL.Untityone.GetGroupName(Eval("RoleName").ToString()) %>
                            </td>
                            <td style="background-color: #efefef">
                                <%#IAM.BLL.Untityone.GetRoleName(Eval("RoleName").ToString()) %>
                            </td>
                            <td style="background-color: #efefef"></td>
                            <td style="background-color: #efefef">[<asp:LinkButton ID="btnsapDelete" runat="server" OnClientClick="javascript: return IsDelete();" Text="删除" Style="text-decoration: none;" CommandArgument='<%#Eval("ID") %>' OnCommand="btnsapDelete_Command"></asp:LinkButton>]
                            </td>

                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr>
                            <td style="background-color: #fff"><%#Eval("p1").ToString().Equals("员工")?"":Eval("p2") %></td>
                            <td style="background-color: #fff"><%#Eval("p1") %></td>
                            <td style="background: #fff;"><%#IAM.BLL.Untityone.GetGroupName(Eval("RoleName").ToString()) %></td>
                            <td style="background: #fff;"><%#IAM.BLL.Untityone.GetRoleName(Eval("RoleName").ToString()) %></td>
                            <td style="background-color: #fff"></td>
                            <td style="background-color: white">[<asp:LinkButton ID="btnsapDelete" runat="server" OnClientClick="javascript: return IsDelete();" Text="删除" Style="text-decoration: none;" CommandArgument='<%#Eval("ID") %>' OnCommand="btnsapDelete_Command"></asp:LinkButton>]
                            </td>

                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </fieldset>

</asp:Content>
