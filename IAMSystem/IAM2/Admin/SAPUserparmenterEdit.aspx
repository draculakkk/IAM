<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" CodeBehind="SAPUserparmenterEdit.aspx.cs" Inherits="IAM.Admin.SAPUserparmenterEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery-1.8.2.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#inputopen").click(function () {
                OpenPage1("SelectSapUserList.aspx");
            });
            if ($("#<%=txtbapname1.ClientID%>").val() == "")
                $("#divuserlist").css("display", "none");

            jQuery("#tmptablelist").find("input[type=button]").live("click", function () {
                $(this).parent("td").parent("tr").remove();

            });
        });

        function DeleteIs() {
            if (confirm("是否要确认删除?")) {
                return true;
            }
            return false;
        }

        function GetBapName() {
            var _value="";
            $("#tmptablelist tbody>tr").each(function () {
                _value = _value + ";" +$(this).children("td:eq(0)").html();
            });
            $("#<%=txtbapname1.ClientID%>").val(_value);
            return true;
        }

        function Yes() {
            var BapName = $("#<%=txtbapname1.ClientID%>").val();
            var old = $("#<%=old.ClientID%>").val();
            if (BapName == "")
                return;
            var BapNameArr = new Array();
            BapNameArr = BapName.split(';');
            var _HTML = "";
            for (var x = 0; x < BapNameArr.length; x++) {
                if (BapNameArr[x] == "")
                    continue;
                if ($("#tmptablelist tbody>tr").length == 0) {
                    _HTML += "<tr><td>" + BapNameArr[x] + "</td><td><input type=\"button\" value=\"Remove\"/></td></tr>";
                }
                else {
                    var isok = false;
                    $("#tmptablelist tbody>tr").each(function () {

                        if (BapNameArr[x] == $(this).children("td:eq(0)").html()) {

                            isok = true;
                        }
                    });
                    if (!isok)
                        _HTML += "<tr><td>" + BapNameArr[x] + "</td><td><input type=\"button\" value=\"Remove\"/></td></tr>";
                }
            }
            $("#tmptablelist").append(_HTML);
            if (BapName != "")
                $("#divuserlist").css("display", "");
            else
                $("#divuserlist").css("display", "none");
        }
        function OpenModal1() {
            if ($("#tmptablelist tbody>tr").length == 0) {
                alert("请先行输入用户名\n");
                return;
            }
            var _value = window.showModalDialog("SAP_ParametersAdd.aspx", null, "dialogWidth=650px;dialogHeight=400px;");
            jQuery("#<%=TextBox1.ClientID %>").val(_value);
            var objReturnBind = document.getElementById("<%=Button2.ClientID%>");
            objReturnBind.click();
        }

        function OpenModal() {
            if ($("#tmptablelist tbody>tr").length == 0) {
                alert("请先行输入用户名");
                return;
            }
            var _value = window.showModalDialog("saprolelist.aspx", null, "dialogWidth=650px;dialogHeight=400px;");
            jQuery("#<%=txtUserInfo.ClientID %>").val(_value);
            var objReturnBind = document.getElementById("<%=btnOnLoadRoleInfo.ClientID%>");
            objReturnBind.click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="script1" runat="server"></asp:ScriptManager>
    <div class="pure-control-group" style="margin-top: 10px;" runat="server" id="nav_top">
        <label for="email" style="width: auto;">
            &nbsp;账号:</label>
        <asp:TextBox ID="old" Style="display: none" Enabled="false" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtbapname1" Style="display: none" Enabled="false" runat="server"></asp:TextBox><input id="inputopen" class="pure-button pure-button-primary" type="button" value="选择账号" />
        <input type="button" value="确定" onclick="GetBapName();" class="pure-button pure-button-primary" runat="server" onserverclick="Unnamed_ServerClick" />
        <input type="button" value="取消" onclick="javascript: window.opener = null; window.location.href = './hremployeemanager.aspx';" class="pure-button pure-button-primary" />

    </div>
    <div id="divuserlist">
        <table class="pure-table pure-table-bordered" id="tmptablelist" style="width: 300px; margin-top: 20px">
            <thead>
                <tr>
                    <th style="width: 200px">账号名</th>
                    <th style="width: 100px">操作</th>
                </tr>
            </thead>
            <tbody>
                <%--<tr>
               <td>fjdasl</td>
               <td><input type="button" value="remove" /></td>
           </tr>--%>
            </tbody>
        </table>
    </div>
    <fieldset>
        <legend>参数设置</legend>
        <asp:UpdatePanel ID="updatepanel2" runat="server">
            <ContentTemplate>
                <div class="pure-control-group" style="margin-top: 10px;">
                    <span style="display: inline-block; width: 1000px; text-align: right;">
                        <input type="button" runat="server" id="Button1" value="添加参数" class="pure-button  pure-button-primary" style="margin-right: 20px;"
                            onclick="javascript: OpenModal1();" />
                        <input type="button" runat="server" id="Button2" onserverclick="Button2_ServerClick"
                            style="display: none;" />
                    </span>
                    <table class="pure-table pure-table-bordered" style="width: 950px; margin-top: 20px"
                        id="table1">
                        <thead>
                            <tr>
                                <th style="width: 200px;">参数ID
                                </th>
                                <th style="width: 200px;">参数值
                                </th>


                                <th style="width: 300px;">短文本
                                </th>
                                <th style="width:150px;">操作类型</th>
                                <th style="width: 100px;">操作
                                </th>
                            </tr>
                        </thead>
                        <tbody>

                            <asp:Repeater ID="rptParameters" runat="server">
                                <ItemTemplate>
                                    <tr>

                                        <td style="background: #efefef;">
                                            <asp:TextBox ID="txtparametersID" runat="server" Text='<%#Eval("PARAMENTERID") %>'></asp:TextBox>
                                        </td>
                                        <td style="background: #efefef;">
                                            <asp:TextBox ID="txtparametersvalue" runat="server" Text='<%#Eval("PARAMENTERVALUE") %>'></asp:TextBox>
                                        </td>
                                        <td style="background: #efefef;">
                                            <asp:TextBox ID="txtwenben" TextMode="MultiLine" Width="90%" runat="server" Text='<%#Eval("PARAMETERTEXT") %>'></asp:TextBox>
                                        </td>
                                        <td style="background: #efefef;">
                                            <input type="radio" runat="server" name="type" id="rdcreate" /><span>添加</span>
                                            <input type="radio" runat="server" name="type" id="rddelete" /><span>删除</span>
                                        </td>
                                        <td style="background: #efefef;">
                                            <!--OnCommand="btnDelete_click" -->
                                            <asp:Button ID="btnDelete" runat="server" Text="删除" CommandName="par" CommandArgument='<%#Eval("id") %>' OnCommand="btnDelete_Command"
                                                OnClientClick="javascript:return DeleteIs();" />
                                            <asp:HiddenField ID="hiddenid" runat="server" Value='<%#Eval("id") %>' />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <tr>
                                        <td style="background: #fff;">
                                            <asp:TextBox ID="txtparametersID" runat="server" Text='<%#Eval("PARAMENTERID") %>'></asp:TextBox>
                                        </td>
                                        <td style="background: #fff;">
                                            <asp:TextBox ID="txtparametersvalue" runat="server" Text='<%#Eval("PARAMENTERVALUE") %>'></asp:TextBox>
                                        </td>

                                        <td style="background: white;">
                                            <asp:TextBox ID="txtwenben" TextMode="MultiLine" Width="90%" runat="server" Text='<%#Eval("PARAMETERTEXT") %>'></asp:TextBox>
                                        </td>
                                        <td style="background: #fff;">
                                            <input type="radio" runat="server" name="type" id="rdcreate" /><span>添加</span>
                                            <input type="radio" runat="server" name="type" id="rddelete" /><span>删除</span>
                                        </td>
                                        <td style="background: white;">
                                            <!--OnCommand="btnDelete_click"-->
                                            <asp:Button ID="btnDelete" runat="server" Text="删除" CommandName="par" CommandArgument='<%#Eval("ID") %>' OnCommand="btnDelete_Command"
                                                OnClientClick="javascript:return DeleteIs();" />
                                            <asp:HiddenField ID="hiddenid" runat="server" Value='<%#Eval("id") %>' />
                                        </td>
                                    </tr>
                                </AlternatingItemTemplate>
                            </asp:Repeater>

                        </tbody>
                    </table>
                    <asp:TextBox ID="TextBox1" runat="server" Width="816px" Style="display: none"></asp:TextBox>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </fieldset>
    <fieldset>
        <legend>角色设置</legend>
        <asp:UpdatePanel ID="updatepanel1" runat="server">
            <ContentTemplate>
                <div class="pure-control-group" style="margin-top: 10px;">
                    <span style="display: inline-block; width: 1000px; text-align: right;">
                        <input type="button" runat="server" id="inputNew" value="委派角色" class="pure-button  pure-button-primary" style="margin-right: 20px;"
                            onclick="javascript: OpenModal();" />
                        <input type="button" runat="server" id="btnOnLoadRoleInfo" onserverclick="btnOnLoadRoleInfo_ServerClick"
                            style="display: none;" />
                    </span>
                    <table class="pure-table pure-table-bordered" style="width: 1000px; margin-top: 20px"
                        id="tablelist">
                        <thead>
                            <tr>
                                <th>角色ID
                                </th>
                                <th>角色名称
                                </th>
                                <th>有效期自
                                </th>
                                <th>有效期至
                                </th>
                                <th>操作类型
                                </th>
                                <th>操作
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="repeaterUserRole" runat="server">
                                <ItemTemplate>
                                    <tr>

                                        <td style="background: #efefef;">
                                            <%#Eval("ROLEID")%>
                                        </td>
                                        <td style="background: #efefef;">
                                            <%#Eval("ROLENAME")%>
                                        </td>

                                        <td style="background: #efefef;">
                                            <asp:TextBox ID="rep_txtstartdate" runat="server" Text='<%#Eval("START_DATE")%>' onClick="WdatePicker()" class="Wdate"></asp:TextBox>
                                            <asp:HiddenField ID="rep_hiddenid" runat="server" Value='<%#Eval("ID") %>' />
                                        </td>
                                        <td style="background: #efefef;">
                                            <asp:TextBox ID="rep_enddate" runat="server" Text='<%#Eval("END_DATE")%>' onClick="WdatePicker()" class="Wdate"></asp:TextBox>
                                        </td>
                                        <td style="background: #efefef;">
                                            <input type="radio" runat="server" name="type" id="rdcreate" /><span>添加</span>
                                            <input type="radio" runat="server" name="type" id="rddelete" /><span>删除</span>
                                        </td>
                                        <td style="background: #efefef;">
                                            <!--OnCommand="btnDelete_click" -->
                                            <asp:Button ID="btnDelete" CommandName="role" runat="server" Text="删除" CommandArgument='<%#Eval("ID") %>' OnCommand="btnDelete_Command"
                                                OnClientClick="javascript:return DeleteIs();" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <tr>


                                        <td style="background: white;">
                                            <%#Eval("ROLEID")%>
                                        </td>
                                        <td style="background: white;">
                                            <%#Eval("ROLENAME")%>
                                        </td>
                                        <td style="background: white;">
                                            <asp:TextBox ID="rep_txtstartdate" runat="server" Text='<%#Eval("START_DATE")%>' onClick="WdatePicker()" class="Wdate"></asp:TextBox>
                                            <asp:HiddenField ID="rep_hiddenid" runat="server" Value='<%#Eval("ID") %>' />
                                        </td>
                                        <td style="background: white;">
                                            <asp:TextBox ID="rep_enddate" runat="server" Text='<%#Eval("END_DATE")%>' onClick="WdatePicker()" class="Wdate"></asp:TextBox>
                                        </td>
                                        <td style="background: white;">
                                            <input type="radio" runat="server" name="type" id="rdcreate" /><span>添加</span>
                                            <input type="radio" runat="server" name="type" id="rddelete" /><span>删除</span>
                                        </td>
                                        <td style="background: white;">
                                            <!--OnCommand="btnDelete_click"-->
                                            <asp:Button ID="btnDelete" runat="server" CommandName="role" Text="删除" CommandArgument='<%#Eval("ID") %>' OnCommand="btnDelete_Command"
                                                OnClientClick="javascript:return DeleteIs();" />
                                        </td>
                                    </tr>
                                </AlternatingItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                    <asp:TextBox ID="txtUserInfo" runat="server" Width="816px" Style="display: none"></asp:TextBox>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </fieldset>
    
</asp:Content>
