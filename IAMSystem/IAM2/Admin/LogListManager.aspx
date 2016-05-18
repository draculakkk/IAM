<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogListManager.aspx.cs" MasterPageFile="~/Admin/master.Master" Inherits="IAM.Admin.LogList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">

    <script src="../Content/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            jQuery("#tablelist tbody>tr:odd").css("background", "#efefef");
            jQuery("#tablelist tbody>tr:even").css("background", "#fff");

        <%--jQuery("#<%=txtTimeStart.ClientID %>,#<%=txtTimeEnd.ClientID %>").datepicker({ inline: true, dateFormat: "yy-mm-dd" });--%>


            jQuery("#dialog").dialog({
                autoOpen: false,
                width: 700,
                buttons: [
                    {
                        text: "Ok",
                        click: function () {
                            $(this).dialog("close");
                        }
                    }
                ]
            });


        });

        function FindMessage(_id) {
       
            var _valu="读取数据失败";
            jQuery.post("../ExcelExportAjax.ashx?type=log",{id:_id},function(data){
               
                jQuery("#Pinfo").html(data);
               
            },"text");

            
            $("#dialog").dialog("open");
           
       
        }

    

    </script>

</asp:Content>

<asp:Content ID="body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <div id="dialog" title="日志详细" style="font-weight: normal; font-size: 12px;">
        <div id="Pinfo" style="word-break: break-all; max-height: 800px;">
        </div>
    </div>

    <div class="pure-control-group" style="margin-top: 10px;">
        <table>
            <tr>
                <td class="td-title-width">日志类型:</td>
                <td class="td-context-width">
                    <asp:DropDownList ID="dplType" runat="server">
                        <asp:ListItem Value="" Text="ALL"></asp:ListItem>
                        <asp:ListItem Value="0" Text="系统错误日志"></asp:ListItem>
                        <asp:ListItem Value="1" Text="普通操作日志"></asp:ListItem>
                        <asp:ListItem Value="2" Text="邮件发送日志"></asp:ListItem>
                        <asp:ListItem Value="3" Text="数据同步日志"></asp:ListItem>
                        <asp:ListItem Value="4" Text="主数据变更日志"></asp:ListItem>
                    </asp:DropDownList></td>
                <td class="td-title-width">开始时间:</td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtTimeStart" runat="server" onClick="WdatePicker()" class="Wdate"></asp:TextBox></td>
                <td class="td-title-width">结束时间:</td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtTimeEnd" runat="server" onClick="WdatePicker()" class="Wdate"></asp:TextBox></td>

            </tr>
            <tr>
                <td class="td-title-width">日志内容:</td>
                <td class="td-context-width">
                    <asp:TextBox ID="txtmess" runat="server"></asp:TextBox>
                </td>
                <td class="td-title-width" colspan="4">
                    
            </tr>
            <tr>
                <td colspan="6" style="padding-left:5px;">
                    <asp:Button ID="btnQuery" runat="server" Text=" 查 询 "
                        CssClass="pure-button pure-button-primary" OnClick="btnQuery_Click" /></td>
                </td>

            </tr>
            </table>
    </div>

    <div class="pure-control-group" style="margin-top: 10px;">
        <table class="pure-table pure-table-bordered" id="tablelist" style="text-align: center; text-decoration: none; font-size: 14px; width: 1000px;">
            <thead>
                <tr align="center">
                    <th style="width: 100px">日志类型
                    </th>
                    <th style="width: 700px">日志详细
                    </th>
                    <th style="width: 150px">创建时间
                    </th>
                    <th style="width: 50px">详细
                    </th>

                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="repeater1log" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%#(IAMEntityDAL.LogDAL.LogType)Eval("type") %>
                            </td>
                            <td>
                                <%#IAM.BLL.Untityone.SubString(Eval("mess").ToString().Replace("<br/>",""), 0, 35)%>
                   
                            </td>
                            <td>
                                <%#Eval("createDate") %>
                            </td>
                            <td>
                                <input type="button" value=" 详 细 " onclick="javascript:FindMessage('<%#Eval("id") %>    ');" />
                            </td>


                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="pure-table-odd">
                            <td>
                                <%#(IAMEntityDAL.LogDAL.LogType)Eval("type") %>
                            </td>
                            <td>
                                <%#IAM.BLL.Untityone.SubString(Eval("mess").ToString().Replace("<br/>",""), 0, 35)%>
                    
                            </td>
                            <td>
                                <%#Eval("createDate") %>
                            </td>
                            <td>
                                <input type="button" value=" 详 细 " onclick="javascript:FindMessage('<%#Eval("id") %>    ');" />
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
