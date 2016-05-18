<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" CodeBehind="AD_Zhiji_WorkGroupManager.aspx.cs" Inherits="IAM.Admin.AD_Zhiji_WorkGroupManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <script type="text/javascript">
       function Yes(){
           window.location.href=window.location;
       }
   </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pure-control-group" style="margin-top: 10px;" runat="server" id="nav_top">
        <label for="email" style="width: auto">职级:</label>
        <asp:TextBox ID="txtjobzhiji" runat="server"></asp:TextBox>
        <label for="email" style="width: 100px">工作组:</label>
        <asp:TextBox ID="txtworkgroup" runat="server"></asp:TextBox>
        &nbsp;&nbsp;
        <input type="checkbox" runat="server" id="chkFalse" /><span>是否禁用</span><br /><br />
        &nbsp;&nbsp;
        <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="pure-button pure-button-primary" OnClick="btnQuery_Click" />
        &nbsp;
        <input type="button" value="新建职级组" id="btnAddNew" runat="server" class="pure-button pure-button-primary" onclick="javascript: OpenPage1('AD_Zhiji_WorkGroupCreate.aspx');" />
    </div>
    <div style="margin-top: 10px; width: 550px;">
        <table class="pure-table pure-table-bordered" style="width: 550px; table-layout: fixed; text-align: center; text-decoration: none; font-size: 14px;">
            <thead>
                <tr align="center">
                    <th style="width: 200px">职级
                    </th>
                    <th style="width: 200px">工作组
                    </th>
                    <th style="width:50px;">状态</th>

                    <th align="center" style="width: 100px">操作
                    </th>
                </tr>
            </thead>

        </table>
    </div>
    <div style="margin: 0px; width: 726px; overflow-x: hidden; height: 500px;">
        <table class="pure-table pure-table-bordered" style="width: 550px; table-layout: fixed; text-align: center; text-decoration: none; font-size: 12px;">
            <tbody>
                <asp:Repeater ID="repeaterADzhijiWorkgroup" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="background: #efefef; width: 200px;">
                                <%#Eval("Zhiji") %>
                            </td>
                            <td style="background: #efefef; width: 200px;">
                                <%#Eval("WorkGroup") %>
                            </td>
                             <td style="width:50px;background: #efefef;">
                              <%#Eval("p1").ToString().ToUpper().Equals("FALSE")?"启用":"禁用" %>
                             </td>

                            <td style="background: #efefef; width: 100px;text-align:right">
                               <input type="button" id="inpedit" value="编辑" onclick="OpenPage1('AD_Zhiji_WorkGroupCreate.aspx?group=<%#Server.UrlEncode(Eval("WorkGroup")==null?"":Eval("WorkGroup").ToString())%>    &name=<%#Server.UrlEncode(Eval("Zhiji").ToString())%>&jinyong=<%#Eval("p1")%>    ');" />
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr>
                            <td style="background: white; width: 200px;">
                                <%#Eval("Zhiji") %>
                            </td>
                            <td style="background: white; width: 200px;">
                                <%#Eval("WorkGroup") %>
                            </td>
                             <td style="width:50px;background: white;">
                              <%#Eval("p1").ToString().ToUpper().Equals("FALSE")?"启用":"禁用" %>
                             </td>

                            <td style="background: white; width: 100px;text-align:right">
                                 <input type="button" id="inpedit" value="编辑" onclick="OpenPage1('AD_Zhiji_WorkGroupCreate.aspx?group=<%#Server.UrlEncode(Eval("WorkGroup")==null?"":Eval("WorkGroup").ToString())%>    &name=<%#Server.UrlEncode(Eval("Zhiji").ToString())%>    &jinyong=<%#Eval("p1")%>    ');" />
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>


            </tbody>
        </table>
    </div>
</asp:Content>
