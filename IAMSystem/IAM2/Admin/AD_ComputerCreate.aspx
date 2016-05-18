<%@ Page Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" ValidateRequest="false" EnableEventValidation="false" CodeBehind="AD_ComputerCreate.aspx.cs" Inherits="IAM.Admin.AD_ComputerCreate" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript">
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
        $(function () {
            var isadmin = "<%=IsAdmin.ToString()%>";
            if (isadmin != "True") {
                jQuery("input[type=button]").prop("disabled", true);
            }
            var request=new GetRequest();
            if (request["id"] != undefined) {
                jQuery.ControllerUserType("<%=dpltype.ClientID%>");
            }

            $("#<%=txtComputerName.ClientID%>").blur(function () {
                var url = "../ValidateLoginName.ashx?sys=ADComputer";               
                var _login = jQuery("#<%=txtComputerName.ClientID%>").val();
                var request = new GetRequest();
                var _isupdate = "0";
                if (request["id"] != undefined) {
                    _isupdate = "1";
                }
                jQuery.ajax({
                    url: url,
                    type: "post",
                    dataType: "json",
                    data: { login: _login, isupdate: _isupdate },
                    success: function (redata) {
                        if (redata["lgn"] != _login)
                        {
                            alert(_login + "用户名在计算机系统已存在\n故系统自动生成用户名为:" + redata["lgn"]);
                            jQuery("#<%=txtComputerName.ClientID%>").val(redata["lgn"]);
                        }
                }
            });
            });

        });
    </script>
</asp:Content>

<asp:Content ID="body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <fieldset>
        <legend style="font-size: 14px; color: #3C72DF">AD计算机信息</legend>
        <div class="pure-control-group" style="margin-top: 10px;">
            <table width="650px">
                <tr>
                    <td class="td-title-width"><font style="color:red;">*</font>计算机名称:
                    </td>
                    <td class="td-context-width">
                        <input type="text" runat="server" id="txtComputerName" />
                    </td>


                </tr>
                    <tr>
                    <td class="td-title-width">计算机类型:
                    </td>
                    <td class="td-context-width">
                        <asp:DropDownList ID="dpltype" runat="server">
                           
                            <asp:ListItem Value="系统" Text="系统"></asp:ListItem>
                            <asp:ListItem Value="其他" Text="其他"></asp:ListItem>
                        </asp:DropDownList>
                    </td>

                         
                </tr>
                <tr>
                    <td class="td-title-width">是否启用:
                    </td>
                    <td class="td-context-width">
                        <asp:CheckBox ID="chkenable" runat="server"  Checked="true"/>
                    </td>

                </tr>
                <tr>
                    <td class="td-title-width"><font style="color:red;">*</font>计算机描述:
                    </td>
                    <td class="td-context-width">
                        <asp:TextBox ID="txtDescription" Width="400" Height="50" TextMode="MultiLine" runat="server"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td class="td-title-width">备注:
                    </td>
                    <td class="td-context-width">
                        <asp:TextBox ID="txtMemo" Width="400" Height="50" TextMode="MultiLine" runat="server"></asp:TextBox>
                    </td>

                </tr>

            </table>


        </div>
    </fieldset>

    <fieldset>
        <legend style="font-size: 14px; color: #3C72DF">计算机组信息</legend>
        <div class="pure-control-group" style="margin-top: 10px;">
            <label for="email" style="width: 100px">
                组名称:</label>
            <asp:DropDownList ID="ddlworklist" runat="server" Width="150"></asp:DropDownList> &nbsp; <asp:Button ID="btnAdd" runat="server" Text="添加" CssClass="pure-button pure-button-primary" OnClick="btnAdd_Click"/>
            &nbsp;<br /><br />
            <table class="pure-table pure-table-bordered" style="text-align: center; text-decoration: none; font-size: 14px; width: 300px">
                <thead>
                    <tr>
                        <th style="width: 150px">组名称
                        </th>

                        <th style="width: 150px">操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repeaterComputerWorkGroups" runat="server">
                        <ItemTemplate>
                            <tr>

                                <td style="background: #efefef;">
                                    <%#Eval("WorkGroup") %>
                                </td>


                                <td style="background: #efefef;">[<asp:LinkButton ID="lbtnDelete" runat="server" Text="删除" OnCommand="lbtnDelete_Command" CommandArgument='<%#Eval("ID") %>'></asp:LinkButton>]
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr>

                                <td style="background: #fff;">
                                    <%#Eval("WorkGroup") %>
                                </td>

                                <td style="background: #fff;">[<asp:LinkButton ID="lbtnDelete" runat="server" Text="删除" OnCommand="lbtnDelete_Command" CommandArgument='<%#Eval("ID") %>'></asp:LinkButton>]
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </fieldset>
    <div style="margin: 10px 200px">
        <asp:Button ID="btnYes" runat="server" Text="确定" CssClass="pure-button pure-button-primary" OnClick="btnYes_Click" />
        &nbsp;
           <input type="button" value="取消" class="pure-button pure-button-primary" onclick="javascript: window.close(true);" />
    </div>
</asp:Content>
