<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" CodeBehind="SelectSapUserList.aspx.cs" Inherits="IAM.Admin.SelectSapUserList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
    <script type="text/javascript">
        $(function () {
            $("#chkall").change(function () {              
                if ($(this).prop("checked")) {
                    $("#tablelist tr>td>input[type=checkbox]").prop("checked", true);
                }
                else {
                    $("#tablelist tr>td>input[type=checkbox]").prop("checked", false);
                }                
            });

            $("#inputyes").click(function () {
                var _html = "";
                $("#tablelist tr>td>input[type=checkbox]:checked").each(function () {
                    _html = $(this).prop("value") + ";" + _html;
                });
                window.parent.opener.document.getElementById("ContentPlaceHolder1_txtbapname1").value = _html;
                window.close();
                window.parent.opener.Yes();
                
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pure-control-group" style="margin-top: 10px;">
        <label for="email" style="width: auto;">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;账号:</label>
        <asp:TextBox ID="txtusername" runat="server"></asp:TextBox>

        <label for="email" style="width: 100px">姓名:</label>
        <asp:TextBox ID="txtname" runat="server"></asp:TextBox>
        <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="pure-button pure-button-primary" OnClick="btnQuery_Click" />
        &nbsp;<input type="button" id="inputyes" value="确定" class="pure-button pure-button-primary" />
        <input type="button" value="取消" onclick="javascript: window.opener = null; window.open('','_self'); window.close();" class="pure-button pure-button-primary" />
    </div>
    <div class="pure-control-group" style="margin-top: 10px; margin-bottom: 0px; width: 410px;">
        <table class="pure-table pure-table-bordered" style="text-align: center; text-decoration: none; font-size: 14px; width: 410px; table-layout: fixed;">
            <thead>
                <tr>
                    <th align="left" style="width: 10px;">
                        <input type="checkbox" id="chkall" /></th>
                    <th style="width: 200px">账号
                    </th>
                    <th style="width: 200px">姓名
                    </th>
                </tr>
            </thead>

        </table>
    </div>
    <div class="pure-control-group" style="margin: 0px; height: 450px; width: 486px; overflow-x: hidden; overflow-y: auto;">
        <table id="tablelist" class="pure-table pure-table-bordered" style="text-align: center; text-decoration: none; font-size: 12px; width: 410px; table-layout: fixed;">
            <tbody>
                <asp:Repeater ID="repeaterSapUserInfo" runat="server">
                    <ItemTemplate>
                        <tr>

                            <td style="background: #efefef; width: 10px;">
                                <input type="checkbox" value="<%#Eval("BAPIBNAME") %>" />
                            </td>
                            <td style="background: #efefef; width: 200px;">
                                <%#Eval("BAPIBNAME") %>
                            </td>
                            <td style="background: #efefef; width: 200px;">
                                <%#Eval("LASTNAME") %>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr>

                            <td style="background: #fff; width: 10px;">
                              <input type="checkbox" value="<%#Eval("BAPIBNAME") %>" />
                            </td>
                            <td style="background: #fff; width: 200px;">
                                <%#Eval("BAPIBNAME") %>
                            </td>
                            <td style="background: #fff; width: 200px;">
                                <%#Eval("LASTNAME") %>
                            </td>
                        </tr>

                    </AlternatingItemTemplate>
                </asp:Repeater>


            </tbody>
        </table>
    </div>
</asp:Content>
