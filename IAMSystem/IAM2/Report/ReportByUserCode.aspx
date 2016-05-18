<%@ Page Title="从员工角度出发的角色" Language="C#" MasterPageFile="~/Admin/master.Master"
    AutoEventWireup="true" CodeBehind="ReportByUserCode.aspx.cs" Inherits="IAM.Report.ReportByUserCode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Open() {
            if ($("#<%=txtNumber.ClientID%>").val() == "") {
                alert("必须填写工号");
                return;
            }
            OpenPageSmall("../admin/RoleTemplateCreate.aspx?templatename=" + jQuery("#<%=txtNumber.ClientID%>").val());
        }

        function Open1() {
           
             OpenPageSmall("../admin/RoleTemplateCreate.aspx");
         }

        function ChenckValue() {
            if ($("#<%=txtNumber.ClientID%>").val() == "") {
                alert("请填写工号");
                return false;
            }
            else
                return true;
        }
    </script>
    <script type="text/javascript">
        //window.onload = function () { menuFixed("ContentPlaceHolder1_nav_top"); }
        function OpenNew(_value, _type) {
            var url = "ADInfoManager.aspx?userid=" + _value;
            switch (_type) {
                case "员工": url = "../admin/aduser/usercreate.aspx?userid=" + _value; break;
                case "其他": url = "../admin/aduser/othercreate.aspx?userid=" + _value;; break;
                case "系统": url = "../admin/aduser/systemcreate.aspx?userid=" + _value;; break;
            }
            OpenPage(url);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pure-control-group" style="margin-top: 10px;">
        <label for="email" style="width: 50px; padding-left: 5px; text-align: left;">
            工号:</label>
        <asp:TextBox ID="txtNumber" runat="server" Width="351px"></asp:TextBox><br /><br />&nbsp;&nbsp;
        <asp:Button ID="btnQuery" OnClientClick="javascript:return ChenckValue();" runat="server" Text="查询"
            CssClass="pure-button pure-button-primary" OnClick="btnQuery_Click" />
        &nbsp;&nbsp;
        <input type="button" class="pure-button pure-button-primary" value="添加为模版" onclick="javascript: Open()" />
        &nbsp;&nbsp;<input type="button" class="pure-button pure-button-primary" value="批量添加为模版" onclick="javascript: Open1()" />
    </div>
    <fieldset style="width: 1300px;">
        <legend>员工基本信息 </legend>
        <table class="pure-table pure-table-bordered" id="tablelist" style="text-align: center; text-decoration: none; font-size: 14px; width: 1300px;">
            <thead>
                <tr align="center">
                    <th style="width: 150px">工号
                    </th>
                    <th style="width: 150px">姓名
                    </th>
                    <th style="width: 400px">部门
                    </th>
                    <th style="width: 125px">岗位
                    </th>
                    <th style="width: 100px">手机
                    </th>
                    <th style="width: 125px">到职日期
                    </th>
                    <th style="width: 125px">离职日期
                    </th>
                    <th style="width: 100px">人员归属范围
                    </th>

                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="repeater1HrEmployee" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="background-color: #efefef">
                                <a href="#" onclick="javascript:OpenEmployeeinfo('<%#Eval("code")%>');"><%#Eval("code")%></a>
                            </td>
                            <td style="background-color: #efefef">
                                <%#Eval("name")%>
                            </td>
                            <td style="background-color: #efefef">
                                <%#Eval("deptname")%>
                            </td>
                            <td style="background-color: #efefef">
                                <%#Eval("posts")%>
                            </td>


                            <td style="background-color: #efefef">
                                <%#Eval("moblephone")%>
                            </td>
                            <td style="background-color: #efefef">
                                <%#Eval("topostDate")!=null?Convert.ToDateTime(Eval("topostDate").ToString()).ToString("yyyy-MM-dd"):""%>
                            </td>
                            <td style="background-color: #efefef">
                                <%#Eval("leavePostsDate")!=null?Convert.ToDateTime(Eval("leavePostsDate").ToString()).ToString("yyyy-MM-dd"):""%>
                            </td>
                            <td style="background-color: #efefef">
                                <%#Eval("userScope")%>
                            </td>

                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr>
                            <td style="background-color: white">
                                <%#Eval("code")%>
                            </td>
                            <td style="background-color: white">
                                <%#Eval("name")%>
                            </td>
                            <td style="background-color: white">
                                <%#Eval("deptname")%>
                            </td>
                            <td style="background-color: white">
                                <%#Eval("posts")%>
                            </td>


                            <td style="background-color: white">
                                <%#Eval("moblephone")%>
                            </td>
                            <td style="background-color: white">
                                <%#Eval("topostDate")!=null?Convert.ToDateTime(Eval("topostDate").ToString()).ToString("yyyy-MM-dd"):""%>
                            </td>
                            <td style="background-color: white">
                                <%#Eval("leavePostsDate")!=null?Convert.ToDateTime(Eval("leavePostsDate").ToString()).ToString("yyyy-MM-dd"):""%>
                            </td>
                            <td style="background-color: white">
                                <%#Eval("userScope")%>
                            </td>

                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </fieldset>
    <fieldset style="width: 1300px;">
        <legend>域控系统账号</legend>
        <table class="pure-table pure-table-bordered" style="text-align: center; text-decoration: none; font-size: 14px; width: 1300px;">
            <thead>
                <tr align="center">
                    <th style="width: 150px;">AD账号
                    </th>
                    <th style="width: 150px;">账号类型
                    </th>
                    <th style="width: 180px">账号状态
                    </th>
                    <th style="width: 300px">组
                    </th>
                    <th style="width: 540px;"></th>

                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="repeaterAD" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="background-color: #efefef">
                                <a href="#" onclick="OpenNew('<%#Eval("uAccountname") %>','<%#Eval("mUserType") %>')"><%#Eval("uAccountname")%>  </a>
                            </td>
                            <td style="background-color: #efefef;">
                                <%#Eval("mUserType")%> 
                            </td>
                            <td style="background-color: #efefef">
                                <%#Eval("uENABLE")==null?"不启用":Eval("uENABLE").ToString().Trim().Equals("True")?"启用":"不启用"%>
                            </td>
                            <td style="background-color: #efefef">
                                <%#Eval("uwGroupName")%>
                            </td>
                            <td colspan="5" style="background-color: #efefef"></td>

                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr>
                            <td style="background-color: white">
                                <a href="#" onclick="OpenNew('<%#Eval("uAccountname") %>','<%#Eval("mUserType") %>')"><%#Eval("uAccountname")%>  </a>
                            </td>
                            <td style="background-color: white">
                                <%#Eval("mUserType")%> 
                            </td>
                            <td style="background-color: white">
                                <%#Eval("uENABLE")==null?"不启用":Eval("uENABLE").ToString().Trim().Equals("True")?"启用":"不启用"%>
                            </td>
                            <td style="background-color: white">
                                <%#Eval("uwGroupName")%>
                            </td>
                            <td colspan="5" style="background-color: white"></td>

                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </fieldset>

    <fieldset style="width: 1300px;">
        <legend>计算机</legend>
        <table class="pure-table pure-table-bordered" id="table4" style="text-align: center; text-decoration: none; font-size: 14px; width: 1300px;">
            <thead>
                <tr align="center">
                    <th style="width: 150px">计算机名称
                    </th>
                    <th style="width: 150px">计算机状态
                    </th>
                    <th style="width: 240px">工作组
                    </th>
                    <th style="width: 760px"></th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="repeaterADComputer" runat="server">
                    <ItemTemplate>
                        <tr>

                            <td style="background-color: #efefef">
                                <a href="#" onclick="OpenPage('../admin/AD_ComputerCreate.aspx?gonghao=<%#Eval("bgonghao") %>&id=<%#Eval("aName") %>')"><%#Eval("aName")%> </a>
                            </td>

                            <td style="background-color: #efefef">
                                <%#Eval("aEnable")==null?"启用":Eval("aEnable").ToString().Trim()=="0"?"禁用":"启用"%>
                               
                            </td>
                            <td style="background-color: #efefef">
                                <%#Eval("wworkgroup")%>
                            </td>
                            <td style="background-color: #efefef"></td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr>

                            <td style="background-color: #fff">
                                <a href="#" onclick="OpenPage('../admin/AD_ComputerCreate.aspx?gonghao=<%#Eval("bgonghao") %>&id=<%#Eval("aName") %>')"><%#Eval("aName")%> </a>
                            </td>

                            <td style="background-color: #fff">
                                <%#Eval("aEnable")==null?"启用":Eval("aEnable").ToString().Trim()=="0"?"禁用":"启用"%>
                                
                            </td>
                            <td style="background-color: white">
                                <%#Eval("wworkgroup")%>
                            </td>
                            <td style="background-color: white"></td>
                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </fieldset>
    <fieldset style="width: 1300px;">
        <legend>SAP账号</legend>
        <table class="pure-table pure-table-bordered" id="table1" style="text-align: center; text-decoration: none; font-size: 14px; width: 1300px;">
            <thead>
                <tr align="center">
                    <th style="width: 150px">账号
                    </th>
                    <th style="width: 100px">账号类型</th>
                    <th style="width: 150px">账号有效期从
                    </th>
                    <th style="width: 150px">账号有效期至
                    </th>
                    <th style="width: 70px">用户类型
                    </th>
                    <th style="width: 150px">角色
                    </th>
                    <th style="width: 150px">角色名称
                    </th>

                    <th style="width: 150px">有效期从
                    </th>
                    <th style="width: 150px">有效期至
                    </th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="repeater1SAPuserRole" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="background-color: #efefef">
                                <a href="#" onclick="OpenPage('../admin/sapUserCreate.aspx?uid=<%#Eval("uBAPIBNAME") %>');"><%#Eval("uBAPIBNAME") %></a>
                            </td>
                            <td style="background-color: #efefef;"><%#Eval("mUserType") %></td>
                            <td style="background-color: #efefef">
                                <%#Eval("uSTART_DATE") %>
                            </td>
                            <td style="background-color: #efefef">
                                <%#Eval("uEND_DATE") %>
                            </td>
                            <td style="background-color: #efefef">
                                <%#Eval("uUSERTYPE") %>
                            </td>
                            <td style="background-color: #efefef">
                                <%#Eval("urRoleID") %>
                            </td>
                            <td style="background-color: #efefef">
                                <%#Eval("urRoleNAME") %>
                            </td>

                            <td style="background-color: #efefef">
                                <%#Eval("urStartDate") %>
                            </td>
                            <td style="background-color: #efefef">
                                <%#Eval("urEndDate") %>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr>
                            <td style="background-color: white"><a href="#" onclick="OpenPage('../admin/sapUserCreate.aspx?uid=<%#Eval("uBAPIBNAME") %>');"><%#Eval("uBAPIBNAME") %></a>  </td>
                            <td style="background-color: white;"><%#Eval("mUserType") %></td>
                            <td style="background-color: white"><%#Eval("uSTART_DATE") %></td>
                            <td style="background-color: white"><%#Eval("uEND_DATE") %></td>
                            <td style="background-color: white"><%#Eval("uUSERTYPE") %></td>
                            <td style="background-color: white"><%#Eval("urRoleID") %></td>
                            <td style="background-color: white"><%#Eval("urRoleNAME") %></td>


                            <td style="background-color: #fff">
                                <%#Eval("urStartDate") %>
                            </td>
                            <td style="background-color: #fff">
                                <%#Eval("urEndDate") %>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </fieldset>
    <fieldset style="width: 1300px;">
        <legend>人事管理系统账号</legend>
        <table class="pure-table pure-table-bordered" id="table2" style="text-align: center; text-decoration: none; font-size: 14px; width: 1300px;">
            <thead>
                <tr align="center">
                    <th style="width: 150px">账号
                    </th>
                    <th style="width: 150px">账号类型
                    </th>
                    <th style="width: 150px">是否锁定
                    </th>
                    <th style="width: 200px">禁用时间
                    </th>
                    <th style="width: 200px">角色
                    </th>
                    <th style="width: 200px">角色名称
                    </th>
                    <th style="width: 250px">公司名称
                    </th>

                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="repeaterHRUserRole" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="background-color: #efefef">
                                <a href="#" onclick="OpenPage('../admin/HREmployeeCreate.aspx?id=<%#Eval("Cuserid") %>');"><%#Eval("hrusUser_code")%></a>
                            </td>
                            <td style="background-color: #efefef"><%#Eval("mUserType") %></td>
                            <td style="background-color: #efefef"><%#Eval("Locked_tag") %></td>
                            <td style="background-color: #efefef"><%#Eval("Disable_time") %></td>
                            <td style="background-color: #efefef">
                                <%#Eval("hrrRoleCode")%>
                            </td>
                            <td style="background-color: #efefef">
                                <%#Eval("role_name")%>
                            </td>
                            <td style="background-color: #efefef">
                                <%#Eval("CompanyKey")%>
                            </td>

                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr>
                            <td style="background-color: white">
                                <a href="#" onclick="OpenPage('../admin/HREmployeeCreate.aspx?id=<%#Eval("Cuserid") %>');"><%#Eval("hrusUser_code")%></a>
                            </td>
                            <td style="background-color: #fff"><%#Eval("mUserType") %></td>
                            <td style="background-color: #fff"><%#Eval("Locked_tag") %></td>
                            <td style="background-color: #fff"><%#Eval("Disable_time") %></td>
                            <td style="background-color: white">
                                <%#Eval("hrrRoleCode")%>
                            </td>
                            <td style="background-color: white">
                                <%#Eval("role_name")%>
                            </td>
                            <td style="background-color: white">
                                <%#Eval("CompanyKey")%>
                            </td>

                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </fieldset>
    <fieldset style="width: 1300px;">
        <legend>预算管理系统账号(角色)</legend>
        <table class="pure-table pure-table-bordered" id="table3" style="text-align: center; text-decoration: none; font-size: 14px; width: 1300px;">
            <thead>
                <tr align="center">
                    <th style="width: 150px">账号
                    </th>
                    <th style="width: 150px">账号类型
                    </th>
                    <th style="width: 150px">账号有效期从</th>
                    <th style="width: 150px">账号有效期至</th>
                    <th style="width: 100px">角色
                    </th>
                    <th style="width: 150px">角色名称
                    </th>
                    <th style="width: 150px">公司名称
                    </th>
                    <th style="width: 150px">角色有效期从
                    </th>
                    <th style="width: 150px">角色有效期至
                    </th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="repeater1HECUserrole" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="background-color: #efefef">
                                <a href="#" onclick="OpenPage('../admin/HECUserInfoCreate.aspx?usercd=<%#Eval("uUSERNAME")%>');"><%#Eval("uUSERNAME")%>   </a>
                            </td>
                            <td style="background-color: #efefef">
                                <%#Eval("mUserType")%>
                            </td>
                            <td style="background-color: #efefef"><%#Eval("uSTARTDATE") %></td>
                            <td style="background-color: #efefef"><%#Eval("uENDDATE") %></td>

                            <td style="background-color: #efefef">
                                <%#Eval("rROLECODE")%>
                            </td>
                            <td style="background-color: #efefef">
                                <%#Eval("rROLENAME")%>
                            </td>
                            <td style="background-color: #efefef">
                                <%#Eval("cCOMPNYFULLNAME")%>
                            </td>
                            <td style="background-color: #efefef">
                                <%#Eval("uROLESTARTDATE")%>
                            </td>
                            <td style="background-color: #efefef">
                                <%--<%#Eval("uROLEENDDATE") != null ?Eval("uROLEENDDATE").ToString().Trim()!=string.Empty?"": Convert.ToDateTime(Eval("uROLEENDDATE").ToString()).ToString("yyyy-MM-dd") : ""%>--%><%#Eval("uROLEENDDATE") %>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr>
                            <td style="background-color:white">
                                <a href="#" onclick="OpenPage('../admin/HECUserInfoCreate.aspx?usercd=<%#Eval("uUSERNAME")%>');"><%#Eval("uUSERNAME")%>   </a>
                            </td>
                            <td style="background-color:white">
                                <%#Eval("mUserType")%>
                            </td>
                            <td style="background-color:white"><%#Eval("uSTARTDATE") %></td>
                            <td style="background-color:white"><%#Eval("uENDDATE") %></td>
                            <td style="background-color:white">
                                <%#Eval("rROLECODE")%>
                            </td>
                            <td style="background-color:white">
                                <%#Eval("rROLENAME")%>
                            </td>
                            <td style="background-color:white">
                                <%#Eval("cCOMPNYFULLNAME")%>
                            </td>
                            <td style="background-color: white">
                                <%#Eval("uROLESTARTDATE")%>
                            </td>
                            <td style="background-color: white">
                                <%--<%#Eval("uROLEENDDATE") != null ?Eval("uROLEENDDATE").ToString().Trim()!=string.Empty?"": Convert.ToDateTime(Eval("uROLEENDDATE").ToString()).ToString("yyyy-MM-dd") : ""%>--%>
                                <%#Eval("uROLEENDDATE") %>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </fieldset>
    <fieldset style="width: 1300px;">
        <legend>预算管理系统账号(岗位)</legend>
        <table class="pure-table pure-table-bordered" id="table5" style="text-align: center; text-decoration: none; font-size: 14px; width: 1300px;">
            <thead>
                <tr align="center">
                    <th style="width: 150px">账号
                    </th>
                    <th style="width: 150px">账号类型
                    </th>

                    <th style="width: 150px">账号有效期</th>
                    <th style="width: 100px">公司代码
                    </th>
                    <th style="width: 150px">公司名称
                    </th>
                    <th style="width: 150px">部门代码
                    </th>
                    <th style="width: 150px">部门名称
                    </th>
                    <th style="width: 150px">岗位代码
                    </th>
                    <th style="width: 150px">岗位名称</th>
                    <th style="width: 150px">主岗位/启用</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="repeater1" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="background-color: #efefef">
                                <a href="#" onclick="OpenPage('../admin/HECUserInfoCreate.aspx?usercd=<%#Eval("zhanghao")%>');"><%#Eval("zhanghao")%>   </a>
                            </td>
                            <td style="background-color: #efefef">
                                <%#Eval("zhanghaoleixing")%>
                            </td>
                            <td style="background-color: #efefef"><%#Eval("START_DATE") %>至<%#Eval("END_DATE") %></td>


                            <td style="background-color: #efefef">
                                <%#Eval("COMPANY_CODE")%>
                            </td>
                            <td style="background-color: #efefef">
                                <%#Eval("COMPANY_NAME")%>
                            </td>
                            <td style="background-color: #efefef">
                                <%#Eval("UNIT_CODE")%>
                            </td>
                            <td style="background-color: #efefef">
                                <%#Eval("UNIT_NAME")%>
                            </td>
                            <td style="background-color: #efefef">
                                <%#Eval("POSITION_CODE") %>
                            </td>
                            <td style="background-color: #efefef"><%#Eval("POSITION_NAME") %></td>
                            <td style="background-color: #efefef"><%#Eval("PRIMARY_POSITION_FLAG").ToString().Equals("Y")?"是":"否" %>/<%#Eval("ENABLED_FLAG").ToString().Equals("Y")?"是":"否" %></td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr>
                            <td style="background-color:white;">
                                <a href="#" onclick="OpenPage('../admin/HECUserInfoCreate.aspx?usercd=<%#Eval("zhanghao")%>');"><%#Eval("zhanghao")%>   </a>
                            </td>
                            <td style="background-color:white">
                                <%#Eval("zhanghaoleixing")%>
                            </td>
                            <td style="background-color: white"><%#Eval("START_DATE") %>至<%#Eval("END_DATE") %></td>


                            <td style="background-color: white">
                                <%#Eval("COMPANY_CODE")%>
                            </td>
                            <td style="background-color: white">
                                <%#Eval("COMPANY_NAME")%>
                            </td>
                            <td style="background-color: white">
                                <%#Eval("UNIT_CODE")%>
                            </td>
                            <td style="background-color: white">
                                <%#Eval("UNIT_NAME")%>
                            </td>
                            <td style="background-color: white">
                                <%#Eval("POSITION_CODE") %>
                            </td>
                            <td style="background-color: white"><%#Eval("POSITION_NAME") %></td>
                            <td style="background-color: white"><%#Eval("PRIMARY_POSITION_FLAG").ToString().Equals("Y")?"是":"否" %>/<%#Eval("ENABLED_FLAG").ToString().Equals("Y")?"是":"否" %></td>
                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </fieldset>


    <fieldset style="width: 1300px;">
        <legend>Team Center系统账号</legend>
        <table class="pure-table pure-table-bordered" border="0" cellspacing="1" cellpadding="1" style="text-align: center; text-decoration: none; font-size: 14px; width: 1300px;">
            <thead>
                <tr align="center">


                    <th style="width: 150px">用户ID
                    </th>
                    <th style="width: 150px">账号类型
                    </th>
                    <th style="width: 150px">账号状态
                    </th>
                    <th style="width: 200px">组名称</th>
                    <th style="width: 200px">角色名称</th>
                    <th style="width: 150px">角色状态</th>
                    <th style="width: 350px"></th>


                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="repeaterTCUserInfo" runat="server">
                    <ItemTemplate>
                        <tr align="center">


                            <td style="background: #efefef;">
                                <a href="#" onclick="OpenPage('../admin/TCUserInfoCreate.aspx?mzhanghao=<%#Eval("uUserID") %>');"><%#Eval("uUserID") %> </a>
                            </td>
                            <td style="background: #efefef;">
                                <%#Eval("mUserType") %>
                            </td>
                            <td style="background: #efefef;">
                                <%#Eval("uUserStatus").ToString().Equals("1")?"非活动":"活动" %>
                            </td>
                            <td style="background: #efefef;"><%#Eval("urmemo")==null?"":IAM.BLL.Untityone.GetGroupName(Eval("urmemo").ToString()) %></td>
                            <td style="background: #efefef;"><%#Eval("urmemo")==null?"":IAM.BLL.Untityone.GetRoleName(Eval("urmemo").ToString()) %></td>

                            <td style="background: #efefef;"><%#Eval("urGroupStatus").ToString().Equals("1")?"活动":"非活动" %></td>
<td style="background:#efefef;"></td>

                        </tr>

                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr align="center">


                            <td style="background: #fff;">
                                <a href="#" onclick="OpenPage('../admin/TCUserInfoCreate.aspx?mzhanghao=<%#Eval("uUserID") %>');"><%#Eval("uUserID") %> </a>
                            </td>
                            <td style="background: #fff;">
                                <%#Eval("mUserType") %>
                            </td>
                            <td style="background: #fff;">
                                <%#Eval("uUserStatus").ToString().Equals("1")?"非活动":"活动" %>
                            </td>
                            <td style="background: #fff;"><%#Eval("urmemo")==null?"":IAM.BLL.Untityone.GetGroupName(Eval("urmemo").ToString()) %></td>
                            <td style="background: #fff;"><%#Eval("urmemo")==null?"":IAM.BLL.Untityone.GetRoleName(Eval("urmemo").ToString()) %></td>
                           
                            <td style="background: #fff;"><%#Eval("urGroupStatus").ToString().Equals("1")?"活动":"非活动" %></td>
                            <td style="background:#fff;"></td>

                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </fieldset>
</asp:Content>
