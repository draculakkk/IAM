<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="false" CodeBehind="DifferenceReport.aspx.cs" Inherits="IAM.Report.DifferenceReport" ValidateRequest="false" EnableEventValidation="false" %>
<%@ Register Src="~/Control/DifferenceControl.ascx" TagName="Differencecontrol" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<script src="../Scripts/ScrollTopFunction.js" type="text/javascript"></script>--%>
    <script type="text/javascript">
        //window.onload = function () { menuFixed("ContentPlaceHolder1_nav_top"); }
    </script>
    <script type="text/javascript">
        function GetHtml() {
            var _valu = jQuery("#divcontext").html();
            _valu = _valu.replace(/<input\stype="submit".+[?>]/, "");
            _valu = _valu.replace(/<input\stype="submit".+[?>]/, "");
            _valu = _valu.replace(/<input\stype="button".+[?>]/, "");
            jQuery("#<%=hidden1.ClientID%>").val(_valu);

            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:Differencecontrol ID="dif1" runat="server"></uc1:Differencecontrol>
    <div id="divcontext">
    <table class="pure-table pure-table-bordered" id="table3" style="text-align: center;
            text-decoration: none; font-size: 14px; width: 1070px;">
       
        <tr>
            <td colspan="2">
                <label for="email" style="width: 70px; padding-left: 5px; text-align: left;">
                    源工号:</label>
                <asp:TextBox ID="TextBox2" runat="server" Width="100px" Text="012345"></asp:TextBox></td>
            <td style="width: 100px;"><=></td>
            <td colspan="4">

                <label for="email" style="width: 70px; padding-left: 5px; text-align: left;">
                    目标工号:</label>
                <asp:TextBox ID="TextBox3" runat="server" Width="100px" Text="012334"></asp:TextBox>&nbsp;&nbsp;
        <asp:Button ID="Button1" runat="server" Text="比较"
            CssClass="pure-button pure-button-primary" OnClick="btnQuery_Click" />
                &nbsp;&nbsp;
                <asp:Button ID="btnOutput" runat="server"  Text="导出" OnClientClick="javascript:return GetHtml();" CssClass="pure-button pure-button-primary" OnClick="btnOutput_Click"/>
                &nbsp;<input type="button" value="进入差异分析工具" onclick="OpenOther();" class="pure-button pure-button-primary"/>
            </td>
        </tr>
        <tr>
            <td colspan="7"></td>
        </tr>
           
          <!--AD差异区域-->
       
          <tr>
            
            <th colspan="7" style="background:#ccc7f4">域控</th>
           
        </tr>
       
       
        <tr style="background-color:  #efefef">
            <td style="background-color:#efefef">账号</td>
            <td colspan="2" style="background-color:#efefef">组名</td>
            <td style="width: 30px;background-color:#efefef;"></td>
            <td style="background-color:#efefef">账号</td>
            <td colspan="2" style="background-color:#efefef">组名</td>
        </tr>
        <tr><td colspan="7" align="left">员工类</td></tr>
        <% foreach (var item in this.Models.AD)
           {
        %>
        <tr>
            <td><%=item.zhanghao %></td>
            <td colspan="2"><%=item.RoleName %></td>
            <td style="width: 30px;"><=></td>
            <td><%=item.zhanghao2 %></td>
            <td colspan="2"><%=item.RoleName2 %></td>
        </tr>
        <%
           }   
        %>
           <tr><td colspan="7" align="left">其他类</td></tr>
        <% foreach (var item in this.Models.ADother)
           {
        %>
        <tr>
            <td><%=item.zhanghao %></td>
            <td colspan="2"><%=item.RoleName %></td>
            <td style="width: 30px;"><=></td>
            <td><%=item.zhanghao2 %></td>
            <td colspan="2"><%=item.RoleName2 %></td>
        </tr>
        <%
           }   
        %>
            <tr><td colspan="7" align="left">系统类</td></tr>
        <% foreach (var item in this.Models.ADsytem)
           {
        %>
        <tr>
            <td><%=item.zhanghao %></td>
            <td colspan="2"><%=item.RoleName %></td>
            <td style="width: 30px;"><=></td>
            <td><%=item.zhanghao2 %></td>
            <td colspan="2"><%=item.RoleName2 %></td>
        </tr>
        <%
           }   
        %>


        <!--AD Computer差异区域-->
        <tr>
           
            <th colspan="7" style="background:#ccc7f4">计算机</th>
           
        </tr>
        <tr style="background-color:  #efefef">
            <td style="background-color:#efefef">账号</td>
            <td colspan="2" style="background-color:#efefef">组名</td>
            <td style="width: 30px;background-color:#efefef;"></td>
            <td style="background-color:#efefef">账号</td>
            <td colspan="2" style="background-color:#efefef">组名</td>
        </tr>
        <tr><td colspan="7" align="left">员工类</td></tr>
        <% foreach (var item in this.Models.ADComputer)
           {
        %>
        <tr>
            <td><%=item.zhanghao %></td>
            <td colspan="2"><%=item.RoleName %></td>
            <td style="width: 30px;"><=></td>
            <td><%=item.zhanghao2 %></td>
            <td colspan="2"><%=item.RoleName2 %></td>
        </tr>
        <%
           }   
        %>
           <tr><td colspan="7" align="left">其他类</td></tr>
        <% foreach (var item in this.Models.ADComputerOther)
           {
        %>
        <tr>
            <td><%=item.zhanghao %></td>
            <td colspan="2"><%=item.RoleName %></td>
            <td style="width: 30px;"><=></td>
            <td><%=item.zhanghao2 %></td>
            <td colspan="2"><%=item.RoleName2 %></td>
        </tr>
        <%
           }   
        %>
            <tr><td colspan="7" align="left">系统类</td></tr>
        <% foreach (var item in this.Models.ADComputerSystem)
           {
        %>
        <tr>
            <td><%=item.zhanghao %></td>
            <td colspan="2"><%=item.RoleName %></td>
            <td style="width: 30px;"><=></td>
            <td><%=item.zhanghao2 %></td>
            <td colspan="2"><%=item.RoleName2 %></td>
        </tr>
        <%
           }   
        %>
        <!--SAP差异区域-->
        <tr>
           
            <th colspan="7" style="background:#ccc7f4">SAP</th>
        </tr>
        <tr style="background-color:  #efefef">
            <td style="background-color:#efefef">账号</td>
            <td style="background-color:#efefef">角色名</td>
            <td colspan="2" style="width: 30px;background-color:#efefef;"></td>
            <td style="background-color:#efefef">账号</td>
            <td colspan="2" style="background-color:#efefef">角色名</td>
        </tr>
        <tr>
            <td colspan="7" align="left">员工类</td>
        </tr>
        <% foreach (var item in this.Models.SAP)
           {
        %>
        <tr>
            <td><%=item.zhanghao %></td>
            <td colspan="2"><%=item.RoleName %></td>
            <td style="width: 30px;"><=></td>
            <td colspan="2"><%=item.zhanghao2 %></td>
            <td 01-5538><%=item.RoleName2 %></td>
        </tr>
        <%
           }   
        %>
        <tr>
            <td colspan="7" align="left">其他类</td>
        </tr>
          <% foreach (var item in this.Models.SAPother)
             {
        %>
        <tr>
            <td><%=item.zhanghao %></td>
            <td colspan="2"><%=item.RoleName %></td>
            <td style="width: 30px;"><=></td>
            <td><%=item.zhanghao2 %></td>
            <td colspan="2"><%=item.RoleName2 %></td>
        </tr>
        <%
             }   
        %>

         <tr>
            <td colspan="7" align="left">系统类</td>
        </tr>
          <% foreach (var item in this.Models.SAPsytem)
             {
        %>
        <tr>
            <td><%=item.zhanghao %></td>
            <td colspan="2"><%=item.RoleName %></td>
            <td style="width: 30px;"><=></td>
            <td><%=item.zhanghao2 %></td>
            <td colspan="2"><%=item.RoleName2 %></td>
        </tr>
        <%
             }   
        %>
        <tr>
            <td colspan="7" align="left"></td>
        </tr>


        <!--HR差异区域-->
        <tr>
           
            <th colspan="7" style="background:#ccc7f4">人事管理系统</th>
        </tr>
        <tr >
            <td style="background-color:#efefef">账号</td>
            <td style="background-color:#efefef">角色名</td>
            <td style="background-color:#efefef">公司</td>
            <td style="width: 30px;background-color:#efefef;"></td>
             <td style="background-color:#efefef">账号</td>
            <td style="background-color:#efefef">角色名</td>
            <td style="background-color:#efefef">公司</td>
        </tr>
        <tr><td colspan="7" align="left">员工类</td></tr>
        <% foreach (var item in this.Models.HR)
           {
        %>
        <tr>
            <td><%=item.zhanghao %></td>
            <td><%=item.RoleName %></td>
            <td><%=item.Company %></td>
            <td style="width: 30px;"><=></td>
            <td><%=item.zhanghao2 %></td>
            <td><%=item.RoleName2 %></td>
            <td><%=item.Company2 %></td>
        </tr>
        <%
           }   
        %>
        <tr><td colspan="7" align="left">其他类</td></tr>
        <% foreach (var item in this.Models.HRother)
           {
        %>
        <tr>
            <td><%=item.zhanghao %></td>
            <td><%=item.RoleName %></td>
            <td><%=item.Company %></td>
            <td style="width: 30px;"><=></td>
            <td><%=item.zhanghao2 %></td>
            <td><%=item.RoleName2 %></td>
            <td><%=item.Company2 %></td>
        </tr>
        <%
           }   
        %>

        <tr><td colspan="7" align="left">系统类</td></tr>
        <% foreach (var item in this.Models.HRsytem)
           {
        %>
        <tr>
            <td><%=item.zhanghao %></td>
            <td><%=item.RoleName %></td>
            <td><%=item.Company %></td>
            <td style="width: 30px;"><=></td>
            <td><%=item.zhanghao2 %></td>
            <td><%=item.RoleName2 %></td>
            <td><%=item.Company2 %></td>
        </tr>
        <%
           }   
        %>

        <!--HEC差异区域 角色-->
        <tr>
           
            <th colspan="7" style="background:#ccc7f4">预算管理系统(角色)</th>
        </tr>
        <tr >
            <td style="background-color:#efefef">账号</td>
            <td style="background-color:#efefef">角色名</td>
            <td style="background-color:#efefef">公司</td>
            <td style="width: 30px;background-color:#efefef;"></td>
            <td style="background-color:#efefef">账号</td>
            <td style="background-color:#efefef">角色名</td>
            <td style="background-color:#efefef">公司</td>
        </tr>
        <tr><td colspan="7" align="left">员工类</td></tr>
        <% foreach (var item in this.Models.HEC)
           {
        %>
        <tr>
            <td><%=item.zhanghao %></td>
            <td><%=item.RoleName %></td>
            <td><%=item.Company %></td>
            <td style="width: 30px;"><=></td>
            <td><%=item.zhanghao2 %></td>
            <td><%=item.RoleName2 %></td>
            <td><%=item.Company2 %></td>
        </tr>
        <%
           }   
        %>
        <tr><td colspan="7" align="left">其他类</td></tr>
        <% foreach (var item in this.Models.HECother)
           {
        %>
        <tr>
            <td><%=item.zhanghao %></td>
            <td><%=item.RoleName %></td>
            <td><%=item.Company %></td>
            <td style="width: 30px;"><=></td>
            <td><%=item.zhanghao2 %></td>
            <td><%=item.RoleName2 %></td>
            <td><%=item.Company2 %></td>
        </tr>
        <%
           }   
        %>
         <tr><td colspan="7" align="left">系统类</td></tr>
        <% foreach (var item in this.Models.HECsytem)
           {
        %>
        <tr>
            <td><%=item.zhanghao %></td>
            <td><%=item.RoleName %></td>
            <td><%=item.Company %></td>
            <td style="width: 30px;"><=></td>
            <td><%=item.zhanghao2 %></td>
            <td><%=item.RoleName2 %></td>
            <td><%=item.Company2 %></td>
        </tr>
        <%
           }   
        %>

         <!--HEC差异区域 岗位-->
        <tr>
           
            <th colspan="7" style="background:#ccc7f4">预算管理系统(岗位)</th>
        </tr>
        <tr >
            <td style="background-color:#efefef">账号</td>
            <td style="background-color:#efefef">角色名</td>
            <td style="background-color:#efefef">公司</td>
            <td style="width: 30px;background-color:#efefef;"></td>
            <td style="background-color:#efefef">账号</td>
            <td style="background-color:#efefef">角色名</td>
            <td style="background-color:#efefef">公司</td>
        </tr>
        <tr><td colspan="7" align="left">员工类</td></tr>
        <% foreach (var item in this.Models.HEC2)
           {
        %>
        <tr>
            <td><%=item.zhanghao %></td>
            <td><%=item.RoleName %></td>
            <td><%=item.Company %></td>
            <td style="width: 30px;"><=></td>
            <td><%=item.zhanghao2 %></td>
            <td><%=item.RoleName2 %></td>
            <td><%=item.Company2 %></td>
        </tr>
        <%
           }   
        %>
        <tr><td colspan="7" align="left">其他类</td></tr>
        <% foreach (var item in this.Models.HEC2Other)
           {
        %>
        <tr>
            <td><%=item.zhanghao %></td>
            <td><%=item.RoleName %></td>
            <td><%=item.Company %></td>
            <td style="width: 30px;"><=></td>
            <td><%=item.zhanghao2 %></td>
            <td><%=item.RoleName2 %></td>
            <td><%=item.Company2 %></td>
        </tr>
        <%
           }   
        %>
         <tr><td colspan="7" align="left">系统类</td></tr>
        <% foreach (var item in this.Models.HEC2System)
           {
        %>
        <tr>
            <td><%=item.zhanghao %></td>
            <td><%=item.RoleName %></td>
            <td><%=item.Company %></td>
            <td style="width: 30px;"><=></td>
            <td><%=item.zhanghao2 %></td>
            <td><%=item.RoleName2 %></td>
            <td><%=item.Company2 %></td>
        </tr>
        <%
           }   
        %>

        <!--TC差异区域-->
        <tr>
           
            <th style="background:#ccc7f4" colspan="7">Team Center</th>
           
        </tr>
       
        <tr style="background-color:  #efefef">
            <td style="background-color:#efefef">账号</td>
            <td style="background-color:#efefef">组</td>
            <td style="background-color:#efefef">角色</td>
            <td style="width: 30px;background-color:#efefef;"></td>            
            <td style="background-color:#efefef">账号</td>
            <td style="background-color:#efefef">组</td>
            <td style="background-color:#efefef">角色</td>
        </tr>
         <tr><td colspan="7" align="left">员工类</td></tr>
        <% foreach (var item in this.Models.TC)
           {
        %>
        <tr>
           <td><%=item.zhanghao %></td>            
            <td><%=item.RoleName==null?"":IAM.BLL.Untityone.GetGroupName(item.RoleName) %></td>
            <td><%=item.RoleName==null?"":IAM.BLL.Untityone.GetRoleName(item.RoleName) %></td>
            <td style="width: 30px;"><=></td>
            <td><%=item.zhanghao2 %></td>
             <td><%=item.RoleName2==null?"":IAM.BLL.Untityone.GetGroupName(item.RoleName2) %></td>
            <td><%=item.RoleName2==null?"":IAM.BLL.Untityone.GetRoleName(item.RoleName2) %></td>
        </tr>
        <%
           }   
        %>
          <tr><td colspan="7" align="left">其他类</td></tr>
        
        <% foreach (var item in this.Models.TCother)
           {
        %>
        <tr>
            <td><%=item.zhanghao %></td>
            
            <td><%=item.RoleName==null?"":IAM.BLL.Untityone.GetGroupName(item.RoleName) %></td>
            <td><%=item.RoleName==null?"":IAM.BLL.Untityone.GetRoleName(item.RoleName) %></td>
            <td style="width: 30px;"><=></td>
            <td><%=item.zhanghao2 %></td>
             <td><%=item.RoleName2==null?"":IAM.BLL.Untityone.GetGroupName(item.RoleName2) %></td>
            <td><%=item.RoleName2==null?"":IAM.BLL.Untityone.GetRoleName(item.RoleName2) %></td>
        </tr>
        <%
           }   
        %>

          <tr><td colspan="7" align="left">系统类</td></tr>
        
        <% foreach (var item in this.Models.TCsytem)
           {
        %>
        <tr>
            <td><%=item.zhanghao %></td>
            
            <td><%=item.RoleName==null?"":IAM.BLL.Untityone.GetGroupName(item.RoleName) %></td>
            <td><%=item.RoleName==null?"":IAM.BLL.Untityone.GetRoleName(item.RoleName) %></td>
            <td style="width: 30px;"><=></td>
            <td><%=item.zhanghao2 %></td>
             <td><%=item.RoleName2==null?"":IAM.BLL.Untityone.GetGroupName(item.RoleName2) %></td>
            <td><%=item.RoleName2==null?"":IAM.BLL.Untityone.GetRoleName(item.RoleName2) %></td>
        </tr>
        <%
           }   
        %>
      
    </table>
    </div>
    <asp:HiddenField ID="hidden1" runat="server" />
</asp:Content>
