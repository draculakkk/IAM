<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SyncListManager.aspx.cs" MasterPageFile="~/Admin/master.Master" Inherits="IAM.Admin.SyncListManager" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
    
    <script src="../Content/My97DatePicker/WdatePicker.js"></script>
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

        //window.setTimeout(function(){
        //    window.location=window.location;
        //},5000);

</script>

</asp:Content>

<asp:Content ID="body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

<div id="dialog" title="日志详细" style="font-weight:normal;font-size:12px;">
<div id="Pinfo" style="word-break:break-all; max-height:350px;">

</div>
</div>

  <div class="pure-control-group" style="margin-top:10px;">
  <table class="pure-table pure-table-bordered" id="tablelist" style="text-align:center; text-decoration:none;font-size:14px;width:1000px;">
        <thead>
            <tr align="center">
                <th style="width:50px">
                   任务名称
                </th>
                <th style="width:50px">
                    开始日期
                </th>
                <th style="width:50px">
                    结束日期
                </th>
                <th style="width:50px">
                    运行状态
                </th>
                <th style="width:50px">
                    预计同步总数
                </th>
                <th style="width:50px">
                    已成功总数
                </th>
                <th style="width:50px">
                    计划运行时间
                </th><th style="width:50px">
                    操作
                </th>
            </tr>
        </thead>
        <tbody>
        <asp:Repeater ID="repeater1log" runat="server">
        <ItemTemplate>
        <tr>
                <td>
                   <%#Eval("name") %>
                </td>
             <td>
                    <%#((DateTime)Eval("bgtime"))==new DateTime()? "": Eval("bgtime") %>
                </td>
             <td>
                   <%# ((DateTime)Eval("edtime"))==new DateTime()? "": Eval("edtime")%>
                </td>
             <td>
                   <%#Eval("isrun") %>
                </td>
             <td>
                   <%#Eval("AllCount") %>
                </td>
             <td>
                   <%#Eval("OkCount") %>
                </td>
                <td>
                     <%# ((DateTime)Eval("runTime")).ToLongTimeString() %>
                </td>
                <td>
                    <asp:Button runat="server" ID="testrun" ToolTip='<%#Eval("name") %>' Text="运行" Enabled='<%# !((bool)Eval("isrun")) %>' OnClick="Unnamed_Click" />
                </td>
                
            </tr>
        </ItemTemplate>
        </asp:Repeater>
            
       
        </tbody>
    </table>
         
 </div>     

</asp:Content>
