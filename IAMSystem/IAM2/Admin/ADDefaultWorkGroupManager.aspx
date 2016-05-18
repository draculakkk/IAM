<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" CodeBehind="ADDefaultWorkGroupManager.aspx.cs" Inherits="IAM.Admin.ADDefaultWorkGroupManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        jQuery(document).ready(function () {

            var isadmin = "<%=IsAdmin.ToString()%>";
            if (isadmin != "True") {
                jQuery("input[type=button]").prop("disabled", true);
            }

            jQuery("#dialog").dialog({
                autoOpen: false,
                width: 350,
                buttons: [{
                    text: "OK",
                    click: function () {
                        var _type = jQuery("#actiontype").val();
                        CreateOrUpdate(_type);
                    }
                }, {
                    text: "Cancel",
                    click: function () { $(this).dialog("close"); }
                }]
            });
        });

        function OpenTransfer(_type, _id) {
            $("#dialog").dialog("open");
            jQuery("#actiontype").val(_type);
            if (_type == "add")
            { }
            else
            {
                Edit(_id);
            }
        }

        function CreateOrUpdate(_type) {
           
            var _name = jQuery("#txtName").val();
            var _des = jQuery("#txtDescription").val();
            var _memo = jQuery("#txtMemo").val();
            var _id=jQuery("#hiddenid").val();
            var _isdelete= "";
            _isdelete=jQuery("input[type=radio]:checked").val();  
            jQuery.post("../ExcelExportAjax.ashx?type=defaultWorkgroup", { Name: _name, Des: _des, Memo: _memo, atype: _type ,id:_id,isdelete:_isdelete},
                function (data) {
                    if (data.indexOf("error") == -1) {
                        if(_type=="add")
                        {
                            alert("添加成功");
                        }
                        else
                        {
                            alert("更新成功");
                        }
                        window.location.href = window.location;
                    }
                    else
                    {
                        alert(data);
                    }
                }, "text");
        }

        function Edit(_id) {
           
            jQuery.post("../ExcelExportAjax.ashx?type=defaultWorkgroupEdit", { id: _id }, function (data) {
                var _data = jQuery.parseJSON(data);
                jQuery("#txtName").val(_data.NAME);
                jQuery("#txtDescription").val(_data.DESCRIPTION);
                jQuery("#txtMemo").val(_data.Memo);
                jQuery("#hiddenid").val(_id);
                if(_data.IsDelete==false)
                    jQuery("#zhuantaiqiyong").prop("checked",true);
                else
                    jQuery("#zhuangtaijinyong").prop("checked",true);
               
            }, "text");
        }

        function isDelete()
        {
            if(confirm("确定要删除？"))
            {
                return true;
            }
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div id="dialog" title="AD默认组管理" style="font-weight: normal; font-size: 12px;">
        <input type="hidden" id="actiontype" />
        <div id="Pinfo" style="word-break: break-all; max-height: 350px;">
            <span>组名称:</span>
            <span>
                <input type="text" id="txtName" />
                <input type="hidden" id="hiddenid" />
            </span>
            <br />
            <br />
            <span>&nbsp;状态:</span>
            <span>
                <input type="radio" value="qiyong" id="zhuantaiqiyong" name="zhuangtai" />启用 
                <input type="radio" value="jinyong" id="zhuangtaijinyong" name="zhuangtai" />禁用
            </span>
            <br />
            <br />
            <span>描述:</span>
            <span>
                <textarea id="txtDescription" cols="30" rows="3"></textarea>
            </span>
            <br />
            <br />
            <span>备注:</span>
            <span>
                <textarea id="txtMemo" cols="30" rows="3"></textarea>
            </span>
        </div>
    </div>

    <div class="pure-control-group" style="margin-top: 10px;" runat="server" id="nav_top">
        <label for="email" style="width: auto;">
            组名称:</label>
        <asp:TextBox ID="txtGroupName" runat="server"></asp:TextBox>
        <br /><br />
        <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="pure-button pure-button-primary" OnClick="btnQuery_Click" />

        <input type="button" runat="server" id="inputAddNew" value="新建默认组" class="pure-button pure-button-primary" onclick="javascript: OpenTransfer('add','');" />
    </div>

    <div style="margin-top: 10px; width: 650px;">
        <table class="pure-table pure-table-bordered" style="width: 750px; table-layout: fixed; text-align: center; text-decoration: none; font-size: 14px;">
            <thead>
                <tr align="center">
                    <th style="width: 150px">组名称
                    </th>
                    <th style="width: 100px;">状态</th>
                    <th style="width: 175px">描述
                    </th>
                    <th style="width: 175px">备注
                    </th>
                    <th style="width: 150px">操作
                    </th>
                </tr>
            </thead>

        </table>
    </div>
    <div style="margin: 0px; width: 951px; overflow-x: hidden; overflow-y: auto; height: 440px;">
        <table class="pure-table pure-table-bordered" style="width: 650px; table-layout: fixed; text-align: center; text-decoration: none; font-size: 12px;">
            <tbody>
                <asp:Repeater ID="repeaterDefaultWork" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="background: #efefef; width: 150px;">
                                <%#Eval("NAME") %>
                            </td>
                            <td style="background: #efefef; width: 100px">
                                <%#Eval("IsDelete").ToString().Equals("True")?"禁用":"启用" %>
                            </td>
                            <td style="background: #efefef; width: 175px;">
                                <%#Eval("Description") %>
                            </td>


                            <td style="background: #efefef; width: 175px;">
                                <%#Eval("Memo") %>
                            </td>


                            <td style="background: #efefef; width: 150px;">
                                <asp:Button ID="btnDelete" runat="server" CommandArgument='<%#Eval("Id") %>' OnClientClick="return isDelete();" OnCommand="btnDelete_Command" Text="删除" />
                                <input type="button" id="inputedit" value="编辑" onclick="OpenTransfer('edit', '<%#Eval("Id") %>    ');" />
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr>
                            <td style="background: white; width: 150px;">
                                <%#Eval("NAME") %>
                            </td>
                            <td style="background: #fff; width: 100px">
                               <%#Eval("IsDelete").ToString().Equals("True")?"禁用":"启用" %>
                            </td>
                            <td style="background: white; width: 175px;">
                                <%#Eval("Description") %>
                            </td>


                            <td style="background: white; width: 176px;">
                                <%#Eval("Memo") %>
                            </td>


                            <td style="background: white; width: 150px">
                                <asp:Button ID="btnDelete" runat="server" CommandArgument='<%#Eval("Id") %>' OnClientClick="return isDelete();" OnCommand="btnDelete_Command" Text="删除" />
                                <input type="button" id="inputedit" value="编辑" onclick="OpenTransfer('edit', '<%#Eval("Id") %>    ');" />
                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>


            </tbody>
        </table>
    </div>
</asp:Content>
