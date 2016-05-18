<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" CodeBehind="UserConflictResolution.aspx.cs" Inherits="IAM.ConflictResolution.UserConflictResolution" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        jQuery(document).ready(function () {
            jQuery("#testfff tbody>tr:even>td").css("background", "#efefef");
            jQuery("#testfff tbody>tr:odd>td").css("background", "#fff");
            jQuery("#tablehead thead>tr:eq(0)>th:eq(0)>input:checkbox").click(function () {
                if (document.getElementById("chkquanxuan").checked == true) {
                    jQuery("#testfff").find("input:checkbox").prop('checked', true);
                }
                else {
                    jQuery("#testfff").find("input:checkbox").prop('checked', false);
                }
            });
            var _type = "";
            jQuery("#btniamhtml").click(function () {
                if (confirm("冲突解决，一旦更改数据，将不可逆转\n请慎用\n是否使用冲突解决？")) {
                    var _val = jQuery("#<%=dlpsysType.ClientID%>").val();
                    if (jQuery("#testfff").find("input[type=checkbox]:checked").length > 0) {
                        jQuery("#dialog").dialog("open");
                        _type = "iam";
                    }
                    else {
                        if (_val == "2" || _val == "3") {
                            alert("以解决冲突不能在修改");
                        }
                        else {
                            alert("请先选择");
                        }
                    }
                }
                return false;
            });

            jQuery("#btnsystemhtml").click(function () {
                if (confirm("冲突解决，一旦更改数据，将不可逆转\n请慎用\n是否使用冲突解决？")) {
                    var _val = jQuery("#<%=dlpsysType.ClientID%>").val();
                    if (jQuery("#testfff").find("input[type=checkbox]:checked").length > 0) {
                        jQuery("#dialog").dialog("open");
                        _type = "system";
                    }
                    else {
                        if (_val == "2" || _val == "3") {
                            alert("以解决冲突不能在修改");
                        }
                        else {
                            alert("请先选择");
                        }
                    }
                }

            })


            jQuery("#dialog").dialog({
                autoOpen: false,
                width: 500,
                buttons: [{
                    text: "确定",
                    click: function () {
                        jQuery("#<%=hiddenMemo.ClientID%>").val(jQuery("#textareamemo").val());
                        if (_type == "iam") {
                            var _obj = document.getElementById("<%=btniam.ClientID%>");
                            _obj.click();
                        }
                        else if (_type == "system") {
                            var _obj = document.getElementById("<%=btnSystem.ClientID%>");
                                _obj.click();
                            }
                    }
                },
                {
                    text: "取消",
                    click: function () {
                        jQuery(this).dialog("close");
                    }
                }
                ]

            });

            ControllDisabled();

            $("#btnsystemhtml0").bind("click", function () {
                exportexcel();
            });

        });

        function exportexcel() {
            var _systpye = $("#<%=dplsystemtype.ClientID%>").val();
            var _type = $("#<%=dlpsysType.ClientID%>").val();
            $.post("../ExcelExportAjax.ashx?type=confiter", {systype:_systpye,state:_type}, function (data) {
                if (data.indexOf("error") == -1) {
                    window.location.href = "../downloadFile/" + data;
                }
                else {
                    alert(data);
                }
            }, "text");
        }

        function ControllDisabled() {
            var _val = jQuery("#<%=dlpsysType.ClientID%>").val();
            if (_val != "1") {
                jQuery("#testfff").find("input:checkbox").prop('disabled', true);
                jQuery("#tablehead thead>tr:eq(0)>th:eq(0)>input:checkbox").prop("disabled", true);
            }
            else {
                jQuery("#testfff").find("input:checkbox").prop('disabled', false);
                jQuery("#tablehead thead>tr:eq(0)>th:eq(0)>input:checkbox").prop("disabled", false);
            }
        }
        function openmaping() {
            OpenPage("./ConfigMaping.aspx");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dialog" title="填写更改备注" style="font-weight: normal; font-size: 12px;">
        <div id="Pinfo" style="word-break: break-all; max-height: 350px;">
            <textarea cols="30" style="width: 478px" rows="10" id="textareamemo"></textarea>
        </div>
    </div>
    <div class="pure-control-group" style="margin-top: 10px;" runat="server" id="nav_top">
        <table>
              <tr>
                  <td class="td-title-width">系统:</td>
                  <td class="td-context-width"><asp:DropDownList ID="dplsystemtype" runat="server">
            <asp:ListItem Value="" Text="" Selected="True"></asp:ListItem>
            <asp:ListItem Value="AD" Text="AD"></asp:ListItem>
            <asp:ListItem Value="ADComputer" Text="ADComputer"></asp:ListItem>
            <asp:ListItem Value="HR" Text="HR"></asp:ListItem>
            <asp:ListItem Value="TC" Text="TC"></asp:ListItem>
            <asp:ListItem Value="HEC" Text="HEC"></asp:ListItem>
            <asp:ListItem Value="SAP" Text="SAP"></asp:ListItem>
        </asp:DropDownList></td>
                  <td class="td-title-width">冲突类型:</td>
                  <td class="td-context-width"><asp:DropDownList ID="dlpsysType" runat="server">
            <asp:ListItem Value="1" Text="新冲突" Selected="True"></asp:ListItem>
            <asp:ListItem Value="2" Text="以IAM为主"></asp:ListItem>
            <asp:ListItem Value="3" Text="以原系统为主"></asp:ListItem>
        </asp:DropDownList></td>
                  <td class="td-title-width">用户名:</td>
                  <td class="td-context-width"><asp:TextBox ID="txtUserName" runat="server" CssClass="Wdate"></asp:TextBox></td>
                  <td class="td-title-width">查询:</td>
                  <td class="td-context-width"><asp:TextBox ID="txtneirong" runat="server"></asp:TextBox></td>
              </tr>
            <tr><td colspan="8" style="height:10px;"></td></tr>
            <tr>
                <td class="td-context-width" colspan="8">
<asp:Button ID="btnQuery" runat="server" Text=" 查 询 "
            CssClass="pure-button pure-button-primary" OnClick="btnQuery_Click" />
        &nbsp;
        <input type="button" id="btniamhtml" value="以IAM为主" class="pure-button pure-button-primary" />
        &nbsp;
        <input type="button" id="btnsystemhtml" value="以原系统为主" class="pure-button pure-button-primary" />
        <input type="button" runat="server" id="btniam" onserverclick="btniam_ServerClick" style="display: none;" />
        <input type="button" runat="server" id="btnSystem" style="display: none;" onserverclick="btnSystem_ServerClick" />
        <input type="button" id="btnsystemhtml0" value=" 导 出 " class="pure-button pure-button-primary" />
        <a href="UserConfilictByUser.aspx">解决IAM系统中无账号而源系统有账号</a>
        <a href="javascript:openmaping()">配置账号Mapping关系</a>
                </td>
            </tr>
            </table>
        <asp:HiddenField ID="hiddenMemo" runat="server" />
        
        
        
    </div>

    <div style="margin: 0px; padding: 0px; width: 1000px;">
        <table class="pure-table pure-table-bordered" id="tablehead" style="text-align: center; text-decoration: none; font-size: 14px; width: 1000px; table-layout: fixed;">
            <thead>
                <tr align="center">
                    <th style="width: 50px">
                        <input type="checkbox" id="chkquanxuan" />
                    </th>
                    <th style="width: 100px">系统名称
                    </th>
                    <th style="width: 100px">用户账号
                    </th>

                    <th style="width: 150px">字段名称
                    </th>
                    <th style="width: 150px">IAM系统
                    </th>
                    <th style="width: 150px">应用系统
                    </th>
                    <th style="width: 150px">创建时间
                    </th>
                    <th style="width: 150px">解决时间
                    </th>

                </tr>
            </thead>

        </table>
    </div>
    <div style="margin: 0px; padding: 0px; width: 1200px; overflow-x: hidden; overflow-y: visible; height: 450px;">
        <table class="pure-table pure-table-bordered" id="testfff" style="text-align: center; text-decoration: none; font-size: 14px; width: 1000px; table-layout: fixed;">
            <tbody>
                <asp:Repeater ID="repeaterUserDeferences" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="width: 50px">
                                <input type="checkbox" runat="server" id="repcheckbox" value='<%#Eval("ID") %>' />
                                <asp:HiddenField ID="hiddenp2" runat="server" Value='<%#Eval("p2") %>' />
                            </td>
                            <td style="width: 100px">
                                <%#Eval("SysType")%>
                            </td>
                            <td style="width: 100px; cursor: pointer"><a style="text-decoration: underline" onclick="<%#ReturnOpenLink(Eval("SysType"),Eval("UserName").ToString()) %>"><%#Eval("UserName") %></a></td>

                            <td style="width: 150px">
                                <%#Eval("CollName")%>
                            </td>
                            <td style="width: 150px">
                                <%#Eval("CollIAMValue")%>
                            </td>
                            <td style="width: 150px">
                                <%#Eval("CollSysValue") %>
                            </td>
                            <td style="width: 150px; cursor: pointer;">
                                <%#Eval("CreateTime") %>
                            </td>
                            <td style="width: 150px" title='<%#Eval("Remark") %>'><%#Eval("ApprovedTime") %></td>

                        </tr>
                    </ItemTemplate>
                </asp:Repeater>



            </tbody>
        </table>
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" HorizontalAlign="Center" CssClass="paginator" CurrentPageButtonClass="cpb" FirstPageText="首页"
            LastPageText="尾页" PrevPageText="上一页" NextPageText="下一页" NumericButtonTextFormatString="[{0}]"
            ShowCustomInfoSection="Left" ShowBoxThreshold="2" PageSize="5"
            CustomInfoSectionWidth="150px" InputBoxClass="text2" TextAfterInputBox="" OnPageChanging="AspNetPager1_PageChanging" />
    </div>
</asp:Content>
