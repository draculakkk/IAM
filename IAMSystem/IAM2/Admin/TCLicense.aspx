<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" CodeBehind="TCLicense.aspx.cs" Inherits="IAM2.Admin.TCLicense" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <script type="text/javascript">
        $(function () {
            $("#tablelist tbody").find("input[type=button]").bind("click", function () {
                var athis = this;
                var id = $(athis).siblings("input[type=hidden]").val();
                var _name = $(athis).parent("td").siblings("td:eq(0)").html();
                var _value = $(athis).parent("td").siblings("td:eq(1)").html();
                $("#inputvalue").val(_value);
                $("#lblname").text(_name);
                $("#<%=hiddenID.ClientID%>").val(id);
                $("#dialog").dialog("open");
            });


            $("#dialog").dialog({
                autoOpen: false,
                width: 350,
                buttons: [{
                    text: "OK",
                    click: function () {
                        $("#<%=hiddenvalue.ClientID%>").val($("#inputvalue").val());
                        var obj = $("#<%=btnAdd.ClientID%>");
                        obj.click();

                    }
                }, {
                    text: "Cancel",
                    click: function () { $("#dialog").dialog("close"); }
                }]
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dialog" title="编辑 TC License" style="font-weight: normal; font-size: 12px;">
        <div id="Pinfo" style="word-break: break-all; max-height: 350px;">
            <span>名称:</span><span id="lblname">
            </span>
            <br />
            <span>License 数量:</span><span><input type="text" id="inputvalue" /></span>

        </div>
        </div>
        <div style="margin-top: 10px; width: 550px;">
            <asp:HiddenField runat="server" ID="hiddenID" />
            <asp:HiddenField runat="server" ID="hiddenvalue" />
            <input type="button" runat="server" id="btnAdd" style="display: none" onserverclick="btnAdd_ServerClick" />
            <table class="pure-table pure-table-bordered" id="tablelist" style="width: 550px; table-layout: fixed; text-align: center; text-decoration: none; font-size: 14px;">
                <thead>
                    <tr align="center">

                        <th style="width: 200px">名称
                        </th>
                        <th style="width: 50px;">属性值</th>

                        <th align="center" style="width: 100px">操作
                        </th>
                    </tr>
                </thead>
                <tbody>

                    <asp:Repeater ID="repeaterTCLicense" runat="server">
                        <ItemTemplate>
                            <tr align="center">
                                <td><%#Eval("KEY") %></td>
                                <td><%#Eval("Value") %></td>
                                <td>
                                    <input type="button" value="编辑" />
                                    <input type="hidden" value='<%#Eval("ID") %>' />
                                </td>
                            </tr>
                        </ItemTemplate>

                    </asp:Repeater>

                </tbody>
            </table>
        </div>
</asp:Content>
