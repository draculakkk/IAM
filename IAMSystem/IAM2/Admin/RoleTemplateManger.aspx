<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" CodeBehind="RoleTemplateManger.aspx.cs" Inherits="IAM.Admin.RoleTemplateManger" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        jQuery(document).ready(function () {
            jQuery("#dialog").dialog({
                autoOpen: false,
                width: 350,
                buttons: [{
                    text: "OK",
                    click: function () {
                        var _values = jQuery("#ddlselect").val();
                        $(this).dialog("close");
                        window.location.href = "../"+_values;
                    }
                }, {
                    text: "Cancel",
                    click: function () { $(this).dialog("close"); }
                }]
            });
        });

        function OpenOther() {
            $("#dialog").dialog("open");
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="dialog" title="请选则差异分析工具类别" style="font-weight: normal; font-size: 12px;">
        <div id="Pinfo" style="word-break: break-all; max-height: 350px;">
            <select id="ddlselect" style="width:320px;height:36px;color:#000;font-size:14px;">
                <option value="Report/DifferenceReport.aspx">用户与用户模版差异分析</option>
                <option value="Report/DifferenceReportByTemplateName.aspx">模版与模版模版差异分析</option>
                <option value="Report/DifferenceReportByUserAndTemplate.aspx">用户与模版差异分析</option>
            </select>
        </div>
    </div>

    <div class="pure-control-group" style="margin-top: 10px;">
        <label for="email" style="width: auto;">
            &nbsp;&nbsp;岗位:</label>
        <asp:TextBox ID="txtTemplateName" runat="server" CssClass="Wdate"></asp:TextBox>
        <br /><br />
        &nbsp;&nbsp;<asp:Button ID="btnQuery" runat="server" Text=" 查 询 " CssClass="pure-button pure-button-primary"
            OnClick="btnQuery_Click" />
        &nbsp;&nbsp;
     <input type="button" value="进入差异分析工具" onclick="OpenOther();" class="pure-button pure-button-primary" />
    </div>

    <div class="pure-control-group" style="margin-top: 10px;">
        <table class="pure-table pure-table-bordered" id="tablelist" style="text-align: center; text-decoration: none; font-size: 14px; width: 1050px;">
            <thead>
                <tr align="center">
                    <th style="width: 150px">岗位
                    </th>
                    <th style="width: 150px">SAP系统角色数
                    </th>
                    <th style="width: 150px">HR系统角色数
                    </th>
                    <th style="width: 150px">HEC系统角色数
                    </th>
                    <th style="width: 150px">HEC系统岗位数</th>
                    <th style="width: 150px">TC系统角色数
                    </th>
                    <th style="width: 150px">AD系统角色数
                    </th>
                    <th style="width:150px;">AD计算机工作组</th>
                    <th style="width: 150px">操作
                    </th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="repeaterRoleTemplate" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="background: #efefef">
                                <%#Eval("TemplateName")%>
                            </td>
                            <td style="background: #efefef">
                                <%#Eval("SAP")%>
                            </td>
                            <td style="background: #efefef">
                                <%#Eval("HR")%>
                            </td>
                            <td style="background: #efefef">
                                <%#Eval("HEC") %>
                            </td>
                            <td style="background: #efefef">
                                <%#Eval("HEC2") %>
                            </td>
                            <td style="background: #efefef">
                                <%#Eval("TC") %>
                            </td>
                            <td style="background: #efefef">
                                <%#Eval("AD") %>
                            </td>  
                            <td style="background: #efefef">
                                <%#Eval("ADComputer") %>
                            </td>                         
                            <td style="background: #efefef">[<a href="javascript:OpenPage('RoleTemplateInfoPage.aspx?id=<%#Eval("ID") %>');">详情</a>]
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr>
                            <td style="background: #fff">
                                <%#Eval("TemplateName")%>
                            </td>
                            <td style="background: #fff">
                                <%#Eval("SAP")%>
                            </td>
                            <td style="background: #fff">
                                <%#Eval("HR")%>
                            </td>
                            <td style="background: #fff">
                                <%#Eval("HEC") %>
                            </td>
                            <td style="background: #fff">
                                <%#Eval("HEC2") %>
                            </td>
                            <td style="background: #fff">
                                <%#Eval("TC") %>
                            </td>
                            <td style="background: #fff">
                                <%#Eval("AD") %>
                            </td>
                            <td style="background: #fff">
                                <%#Eval("ADComputer") %>
                            </td>
                            <td style="background: #fff">[<a href="javascript:OpenPage('RoleTemplateInfoPage.aspx?id=<%#Eval("ID") %>');">详情</a>]
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>

</asp:Content>
