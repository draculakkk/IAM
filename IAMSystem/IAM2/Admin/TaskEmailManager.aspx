<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" CodeBehind="TaskEmailManager.aspx.cs" Inherits="IAM.Admin.TaskEmailManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        jQuery(document).ready(function () {
            jQuery("#dialog").dialog({
                autoOpen: false,
                width: 500,
                buttons: [{
                    text: "OK",
                    click: function () {
                        ajaxupdate();
                    }
                }, {
                    text: "Cancel",
                    click: function () { $(this).dialog("close"); }
                }]
            });
        });

        function OpenOther(_sysname,_mail,_quan) {
            $("#dialog").dialog("open");
            jQuery("#txtsystemname").val(_sysname);
            jQuery("#txtMail").val(_mail);
            jQuery("#txtquanxian").val(_quan);
        }

        function ajaxupdate(){
            var _sys= jQuery("#txtsystemname").val();
            var _mail=jQuery("#txtMail").val();
            var _quanxian=jQuery("#txtquanxian").val();
            jQuery.post("../ExcelExportAjax.ashx?type=taskmail",{sys:_sys,mail:_mail,quan:_quanxian},function(data){
                if(data!="")
                {
                    if(data.indexOf("error")==-1)
                    {
                        alert("更新成功");
                        window.location.href=window.location.href;
                    }
                    else
                    {
                        alert(data);
                    }
                }
            },"text");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="dialog" title="系统邮件配置" style="font-weight: normal; font-size: 12px;">
        <div id="Pinfo" class="pure-control-group" style="word-break: break-all; max-height: 550px;">
            <label for="email" >系统名称:</label>
            <input type="text" id="txtsystemname" style="width: 260px;" /><br />
            <br />
            <label for="email" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;邮箱:</label>
            <textarea id="txtMail" cols="50" rows="5"></textarea><br /><br />
            <label for="email" >权限审核:</label>
            <textarea id="txtquanxian" cols="50" rows="5"></textarea>
        </div>
    </div>

    <div class="pure-control-group" style="margin-top: 10px;" runat="server" id="nav_top">
        <label for="email" style="width: auto;margin-left:5px;">系统:</label>
        <asp:DropDownList ID="ddlName" runat="server" Width="200">
            <asp:ListItem Value="SAP" Text="SAP">
            </asp:ListItem>
            <asp:ListItem Value="TC" Text="TC"></asp:ListItem>
            <asp:ListItem Value="AD" Text="AD">
            </asp:ListItem>
            <asp:ListItem Value="EHR" Text="EHR"></asp:ListItem>
            <asp:ListItem Value="HEC" Text="HEC"></asp:ListItem>
            <asp:ListItem Value="" Text="" Selected="True"></asp:ListItem>
        </asp:DropDownList><br /><br />&nbsp;
        <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="pure-button pure-button-primary" OnClick="btnQuery_Click" />


    </div>
    <div style="margin-top: 10px; width: 750px;">
        <table class="pure-table pure-table-bordered" style="width: 750px; table-layout: fixed; text-align: center; text-decoration: none; font-size: 14px;">
            <thead>
                <tr align="center">
                    <th style="width: 200px">系统
                    </th>
                    <th style="width: 200px">邮箱
                    </th>
                    <th style="width: 200px">
                        权限审核
                    </th>

                    <th align="center" style="width: 150px">操作
                    </th>
                </tr>
            </thead>

        </table>
    </div>
    <div style="margin: 0px; width: 926px; overflow-x: hidden; height: 500px;">
        <table class="pure-table pure-table-bordered" style="width: 750px; table-layout: fixed; text-align: center; text-decoration: none; font-size: 12px;">
            <tbody>
                <asp:Repeater ID="repeaterTaskEmail" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="background: #efefef; width: 200px;">
                                <%#Eval("SystemName") %>
                            </td>
                            <td style="background: #efefef; width: 200px;">
                                <%#Eval("EmailAddress") %>
                            </td>
                            <td style="background: #efefef; width: 200px;">
                                <%#Eval("p1") %>
                            </td>

                            <td style="background: #efefef; width: 150px;">
                                <input type="button" value="编辑" onclick="OpenOther('<%#Eval("SystemName") %>    ','<%#Eval("EmailAddress")%>    ','<%#Eval("p1") %>    ');" />
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr>
                            <td style="background: white; width: 200px;">
                                <%#Eval("SystemName") %>
                            </td>
                            <td style="background: white; width: 200px;">
                                <%#Eval("EmailAddress") %>
                            </td>
                             <td style="background: white; width: 200px;">
                                <%#Eval("p1") %>
                            </td>

                            <td style="background: white; width: 150px;">
                                <input type="button" value="编辑" onclick="OpenOther('<%#Eval("SystemName") %>    ','<%#Eval("EmailAddress")%>    ','<%#Eval("p1") %>    ');" />
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>


            </tbody>
        </table>
    </div>
</asp:Content>
