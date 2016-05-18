﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" CodeBehind="HRChayi.aspx.cs" Inherits="IAM2.ConflictResolution.HRChayi" %>
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
                        //提交操作
                        //return true;
                        var _obj = document.getElementById("<%=btniam.ClientID%>");
                        _obj.click();
                    }
                    else {
                        if (_val == "2" || _val == "3") {
                            alert("已解决冲突不能在修改");
                        }
                        else {
                            alert("请先选择");
                        }
                    }
                }
                return false;
            });


             ControllDisabled();


        });

        function ControllDisabled() {
            var _val = jQuery("#<%=dlpsysType.ClientID%>").val();
            if (_val != "未处理") {
                jQuery("#testfff").find("input:checkbox").prop('disabled', true);
                jQuery("#tablehead thead>tr:eq(0)>th:eq(0)>input:checkbox").prop("disabled", true);
            }
            else {
                jQuery("#testfff").find("input:checkbox").prop('disabled', false);
                jQuery("#tablehead thead>tr:eq(0)>th:eq(0)>input:checkbox").prop("disabled", false);
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pure-control-group" style="margin-top: 10px;" runat="server" id="nav_top">
        
        <label for="email" style="width: 70px; padding-left: 5px; text-align: left;">
            状态:</label>
        <asp:DropDownList ID="dlpsysType" runat="server">
            <asp:ListItem Value="" Text="全部"></asp:ListItem>
            <asp:ListItem Value="未处理" Text="未处理"  Selected="True"></asp:ListItem>
            <asp:ListItem Value="已处理" Text="已处理"></asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="btnQuery" runat="server" Text="查询"
            CssClass="pure-button pure-button-primary" OnClick="btnQuery_Click" />
        &nbsp;&nbsp;
        <asp:Button  id="btnsystemhtml" Text="导出" CssClass="pure-button pure-button-primary"  runat="server" OnClick="btnsystemhtml_Click1"  />
        &nbsp;&nbsp;
        <input type="button" id="btniamhtml" value="解决" class="pure-button pure-button-primary" />
        &nbsp;&nbsp;
        
        <input type="button" runat="server" id="btniam" onserverclick="btniam_ServerClick"  style="display:none;"/>
    </div>
      <div style="margin: 0px; padding: 0px; width: 1000px;">
        <table class="pure-table pure-table-bordered" id="tablehead" style="text-align: center; text-decoration: none; font-size: 14px; width: 1150px; table-layout: fixed;">
            <thead>
                <tr align="center">
                    <th style="width: 50px">
                        <input type="checkbox" id="chkquanxuan" />
                    </th>
                     <th style="width: 100px">工号
                    </th>
                     <th style="width: 100px">姓名
                    </th>
                   
                    <th style="width: 150px">所在部门
                    </th>
                    <th style="width: 150px">岗位
                    </th>
                    <th style="width: 150px">手机
                    </th>
                    <th style="width: 150px">产生时间
                    </th>
                    <th style="width: 150px">处理时间
                    </th>
                    <th style="width: 150px">状态
                    </th>
                   
                </tr>
            </thead>

        </table>
    </div>
    <div style="margin: 0px; padding: 0px; width: 1375px; overflow-x: hidden; overflow-y: visible; height: 450px;">
        <table class="pure-table pure-table-bordered" id="testfff" style="text-align: center; text-decoration: none; font-size: 14px; width: 1150px; table-layout: fixed;">
            <tbody>
                <asp:Repeater ID="repeaterUserDeferences" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="width: 50px">
                                <input type="checkbox" runat="server"  id="repcheckbox" value='<%#Eval("ID") %>' />
                            </td>
                             <td style="width: 100px">
                                <%#Eval("code")%>
                            </td>
                            <td style="width: 100px;cursor:pointer">
                                 <%#Eval("name")%>
                            </td>
                           
                            <td style="width: 150px">
                                <%#Eval("deptname")%>
                            </td>
                            <td style="width: 150px">
                                <%#Eval("posts")%>
                            </td>
                            <td style="width: 150px">
                                <%#Eval("moblePhone") %>
                            </td>
                            <td style="width: 150px;">
                                <%#Eval("syncDate") %>
                            </td>
                            <td style="width: 150px">
                                <%#Eval("p3") %>
                            </td>
                            <td style="width: 150px">
                                <%#Eval("p2") %>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>



            </tbody>
        </table>
    </div>
    <div>
    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" HorizontalAlign="Center" CssClass="paginator" CurrentPageButtonClass="cpb" FirstPageText="首页"
            LastPageText="尾页" PrevPageText="上一页" NextPageText="下一页" NumericButtonTextFormatString="[{0}]"
            ShowCustomInfoSection="Left" ShowBoxThreshold="2" PageSize="5"
            CustomInfoSectionWidth="150px" InputBoxClass="text2" TextAfterInputBox="" OnPageChanging="AspNetPager1_PageChanging" />
    </div>
</asp:Content>
