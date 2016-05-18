<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true"
    CodeBehind="HREmployeeManager.aspx.cs" Inherits="IAM.Admin.HREmployeeManager" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Content/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("#btnexportexcel").click(function () {
                var _gonghao = $("#<%=txtgonghao.ClientID%>").val();
                var _name = $("#<%=txtname.ClientID%>").val();
                var _department = $("#<%=txtdepartment.ClientID%>").val();
                var _gangwei = $("#<%=txtgangwei.ClientID%>").val();
                var _lizhiriqi = $("#<%=txtlizhidate.ClientID%>").val();
                var _lizhiriqi2 = $("#<%=txtlizhidate2.ClientID%>").val();
                var _shifoulizhi = $("#<%=dpllizhi.ClientID%>").val();
                if (_lizhiriqi == "" && _lizhiriqi2 != "")
                {
                    alert('必须填写离职日期从值');
                    return;
                }
                if (_lizhiriqi != "" && _lizhiriqi2 == "")
                {
                    alert('必须填写离职日期至值');
                    return;
                }
                jQuery.post("../ExcelExportAjax.ashx?type=hrEmployee",
                        {
                            gonghao: _gonghao,
                            name: _name,
                            department: _department,
                            gangwei: _gangwei,
                            lizhiriqi: _lizhiriqi,
                            lizhiriqi2: _lizhiriqi2,
                            shifoulizhi:_shifoulizhi
                        },
                        function (data) {
                            if (data.indexOf("error") == -1) {
                                window.location.href = "../downloadFile/" + data;
                            }
                            else {
                                alert(data);
                            }
                        }, "text");
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pure-control-group" style="margin-top: 10px;" runat="server" id="nav_top">
        <table>
            <tr>
                <td class="td-title-width">工号:
                </td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtgonghao" runat="server"></asp:TextBox>
                </td>
                <td class="td-title-width">姓名:
                </td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtname" runat="server"></asp:TextBox>
                </td>
                <td class="td-title-width">
                     部门:
                </td>
                 <td class="td-context-width">
                    <asp:TextBox ID="txtdepartment" runat="server"></asp:TextBox>
                </td>
                <td class="td-title-width">
                     岗位:
                </td>
                 <td class="td-context-width">
                    <asp:TextBox ID="txtgangwei" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>   是否离职:

                </td>
                <td>
                    <asp:DropDownList ID="dpllizhi" Width="150px" runat="server">
                        <asp:ListItem Value="" Text="全部"></asp:ListItem>
                        <asp:ListItem Value="1" Text="离职"></asp:ListItem>
                        <asp:ListItem Value="0" Text="在职"></asp:ListItem>
                    </asp:DropDownList>
                </td>
             <td class="td-title-width">离职时间从:
                </td>
                <td class="td-context-width" colspan="2">
                    <asp:TextBox ID="txtlizhidate" runat="server" onClick="WdatePicker()" class="Wdate"></asp:TextBox>至
                    <asp:TextBox ID="txtlizhidate2" runat="server" onClick="WdatePicker()" class="Wdate"></asp:TextBox>
                </td>
                <td class="td-context-width" colspan="4">
                    
                </td>
            </tr>
            <tr style="height:35px;">
                
                <td colspan="8" style="vertical-align:middle;text-align:left;padding-left:5px;">
                    <asp:Button ID="btnSearch" runat="server" Text=" 查 询 "
                        CssClass="pure-button pure-button-primary" OnClick="btnSearch_Click" />&nbsp;&nbsp;
                   <input type="button" class="pure-button pure-button-primary" id="btnexportexcel" value=" 导 出 " />
                </td>
            </tr>
        </table>
    </div>
    <div class="pure-control-group" style="margin-top: 10px;">

        <table class="pure-table pure-table-bordered" id="tablelist" style="text-align: center;
            text-decoration: none; font-size: 14px; width: 1000px;">
            <thead>
                <tr align="center">
                    <th style="width: 100px;height:25px;">
                        工号
                    </th>
                   
                    <th style="width: 100px;height:25px;">
                        姓  名
                    </th>
                    <th style="width: 100px;height:25px;">
                        所在部门
                    </th>
                     <th style="width: 100px;height:25px;">
                        岗位
                    </th>
                    <th style="width: 100px;height:25px;">
                        手机
                    </th>
                    <th style="width: 100px;height:25px;">
                        到职日期
                    </th>
                    <th style="width: 100px;height:25px;">
                        离职日期
                    </th>
                    <th style="width: 100px;height:25px;">
                        人员归属范围
                    </th>
                    
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="repeater1HrEmployee" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="background-color:#efefef;height:25px;">
                               <a href="#" onclick="javascript:OpenEmployeeinfo('<%#Eval("code") %>');"> <%#Eval("code")%> </a>
                            </td>
                            
                            <td style="background-color:#efefef;height:25px;">
                                <%#Eval("name")%>
                            </td>
                            <td style="background-color:#efefef;height:25px;">
                                <%#Eval("deptname")%>
                            </td>
                            <td style="background-color:#efefef;height:25px;">
                                <%#Eval("posts")%>
                            </td>
                            <td style="background-color:#efefef;height:25px;">
                                <%#Eval("moblephone")%>
                            </td>
                            <td style="background-color:#efefef;height:25px;">
                                <%#Eval("topostDate")!=null?Convert.ToDateTime(Eval("topostDate").ToString()).ToString("yyyy-MM-dd"):""%>
                            </td>
                            <td style="background-color:#efefef;height:25px;">
                                <%#Eval("leavePostsDate")!=null?Convert.ToDateTime(Eval("leavePostsDate").ToString()).ToString("yyyy-MM-dd"):""%>
                            </td>
                            <td style="background-color:#efefef;height:25px;">
                                <%#Eval("userScope")%>
                            </td>
                            
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr>
                            <td style="background-color:white;height:25px;">
                                <a href="#" onclick="javascript:OpenEmployeeinfo('<%#Eval("code") %>');"> <%#Eval("code")%> </a>
                            </td>
                            
                            <td style="background-color:white;height:25px;">
                                <%#Eval("name")%>
                            </td>
                            <td style="background-color:white;height:25px;">
                                <%#Eval("deptname")%>
                            </td>
                            <td style="background-color:white;height:25px;">
                                <%#Eval("posts")%>
                            </td>
                            <td style="background-color:white;height:25px;">
                                <%#Eval("moblephone")%>
                            </td>
                             <td style="background-color:white;height:25px;">
                                <%#Eval("topostDate")!=null?Convert.ToDateTime(Eval("topostDate").ToString()).ToString("yyyy-MM-dd"):""%>
                            </td>
                            <td style="background-color:white;height:25px;">
                                <%#Eval("leavePostsDate")!=null?Convert.ToDateTime(Eval("leavePostsDate").ToString()).ToString("yyyy-MM-dd"):""%>
                            </td>
                            <td style="background-color:white;height:25px;">
                                <%#Eval("userScope")%>
                            </td>
                            
                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
    <div class="pure-control-group" style="margin-top: 10px;">
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" HorizontalAlign="Center" CssClass="paginator" CurrentPageButtonClass="cpb" FirstPageText="首页"
                LastPageText="尾页" PrevPageText="上一页" NextPageText="下一页" NumericButtonTextFormatString="[{0}]"
                ShowCustomInfoSection="Left" ShowBoxThreshold="2" PageSize="5"                
                CustomInfoSectionWidth="150px" InputBoxClass="text2" TextAfterInputBox="" OnPageChanging="AspNetPager1_PageChanging" />
    </div>
</asp:Content>
