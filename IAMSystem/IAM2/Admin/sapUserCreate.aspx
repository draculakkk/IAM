<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" CodeBehind="sapUserCreate.aspx.cs" Inherits="IAM.Admin.sapUserCreate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Content/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        var usertype = "NaN";
        jQuery(document).ready(function () {
            var request = new GetRequest();

            var isadmin = "<%=IsAdmin.ToString()%>";
            if (isadmin != "True") {
                jQuery("input[type=button]").prop("disabled", true);
            }

            if (request["uid"] != undefined) {
                usertype = jQuery("#<%=dplEmployeeType.ClientID%>").val();
                jQuery.ControllerUserType("<%=dplEmployeeType.ClientID%>");
            } else {

                jQuery("#<%=dplEmployeeType.ClientID%>").bind("change", function () {
                    var _val = jQuery(this).val();

                    var obj = document.getElementById("<%=btnQuery.ClientID%>");
                    obj.click();

                });
            }

            jQuery("#<%=txtUserName.ClientID%>").blur(function () {
                var _isupdate = "0";
                var request = new GetRequest();
                if (request["uid"] != undefined)
                    _isupdate = "1";
                var url = "../../ValidateLoginName.ashx?sys=SAP";
                var _login = jQuery("#<%=txtUserName.ClientID%>").val();
                jQuery.ajax({
                    url: url,
                    type: "post",
                    dataType: "json",
                    data: { isupdate: _isupdate, login: _login },
                    success: function (redata) {
                        if (_login != redata["lgn"]) {
                            alert(_login + "用户名在SAP系统已存在\n故系统自动生成用户名为:" + redata["lgn"]);
                            jQuery("#<%=txtUserName.ClientID%>").val(redata["lgn"]);
                        }
                    }
                });
            });

        });

        function DeleteIs() {
            if (confirm("是否要确认删除?")) {
                return true;
            }
            return false;
        }


        function OpenModal() {
            if (jQuery("#<%=txtUserName.ClientID%>").val() == "") {
                alert("请先行输入用户名\n例如:工号");
                return;
            }
            var _value = window.showModalDialog("saprolelist.aspx", null, "dialogWidth=650px;dialogHeight=400px;");
            jQuery("#<%=txtUserInfo.ClientID %>").val(_value);
            var objReturnBind = document.getElementById("<%=btnOnLoadRoleInfo.ClientID%>");
            objReturnBind.click();
        }

        function OpenModal1() {
            if (jQuery("#<%=txtUserName.ClientID%>").val() == "") {
                alert("请先行输入用户名\n例如:工号");
                return;
            }
            var _value = window.showModalDialog("SAP_ParametersAdd.aspx", null, "dialogWidth=650px;dialogHeight=400px;");
            jQuery("#<%=TextBox1.ClientID %>").val(_value);
            var objReturnBind = document.getElementById("<%=Button2.ClientID%>");
            objReturnBind.click();
        }

        function GetRequest() {
            var url = location.search; //获取url中"?"符后的字串
            var theRequest = new Object();
            if (url.indexOf("?") != -1) {
                var str = url.substr(1);
                strs = str.split("&");
                for (var i = 0; i < strs.length; i++) {
                    theRequest[strs[i].split("=")[0]] = unescape(strs[i].split("=")[1]);
                }
            }
            return theRequest;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="scriptmanager1" runat="server"></asp:ScriptManager>
    <div class="pure-control-group" style="margin-top: 10px;">
        <fieldset style="border: 1px solid #efefef;">
            <legend style="font-size: 14px; color: #3C72DF">HR 信息</legend>
            <div class="pure-control-group" style="margin-top: 10px;">
                <table>
                    <tr>
                        <td class="td-title-width">账号类别:
                        </td>
                        <td class="td-context-width">

                            <asp:DropDownList ID="dplEmployeeType" runat="server">

                                <asp:ListItem Value="员工" Text="员工" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="系统" Text="系统"></asp:ListItem>
                                <asp:ListItem Value="其他" Text="其他"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:HiddenField ID="mappingId" runat="server" />
                        </td>
                        <td class="td-title-width"><font style="color: red;">*</font>工号:
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtGongHao" runat="server"></asp:TextBox>

                        </td>
                        <td colspan="4">
                            <asp:Button ID="btnQuery" runat="server" Text=" 查 询 " OnClick="btnQuery_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td-title-width">姓名:
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                        </td>
                        <td class="td-title-width">岗位:
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtGangwei" runat="server"></asp:TextBox>
                        </td>
                        <td class="td-title-width">所在部门:
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtPartment" runat="server"></asp:TextBox>
                        </td>
                        <td class="td-title-width">上级部门:
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtPrePartment" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-title-width">手机:
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtPhoneNumber" runat="server"></asp:TextBox>
                        </td>
                        <td class="td-title-width">到职日期:
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtComeDate" runat="server"></asp:TextBox>
                        </td>
                        <td class="td-title-width">离职日期:
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtOutDate" runat="server"></asp:TextBox>
                        </td>
                        <td class="td-title-width"><%--部门撤销日期:--%>
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtPartmentOutDate" runat="server" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2"><%--部门撤销标记--%>
                            <asp:CheckBox ID="chkPartmentOut" Visible="false" runat="server" />
                        </td>
                        <td colspan="6"><%--部门封存标记--%><asp:CheckBox Visible="false" ID="chkPartmentClose" runat="server" />
                        </td>
                    </tr>
                </table>

            </div>
        </fieldset>
        <fieldset style="border: 1px solid #efefef;">
            <legend style="font-size: 14px; color: #3C72DF">新建账号</legend>
            <div class="pure-control-group" style="margin-top: 10px;">
                <table>
                    <tr>
                        <td class="td-title-width"><font style="color: red;">*</font>用户名:
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                        </td>
                        <td class="td-title-width"><font style="color: red;">*</font>姓(名):
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtLastAndFirstName" runat="server"></asp:TextBox>
                        </td>
                        <td class="td-title-width"><font style="color: red;">*</font>部门:
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtDempartMent" runat="server"></asp:TextBox>
                        </td>
                        <td class="td-title-width">语言:
                        </td>
                        <td class="td-context-width">
                            <asp:DropDownList ID="ddlUserLanguage" Width="150px" runat="server">
                                <asp:ListItem Value="1" Text="Zh-Cn"></asp:ListItem>
                                <asp:ListItem Value="E" Text="En"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-title-width">移动电话:
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtMoblePhoneNumber" runat="server"></asp:TextBox>
                        </td>
                        <td class="td-title-width">Email:
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                        </td>
                        <td class="td-title-width">用户类型:
                        </td>
                        <td class="td-context-width">
                            <asp:DropDownList ID="ddlUserType" Width="150px" runat="server">

                                
                                <asp:ListItem Value="A" Text="对话"></asp:ListItem>
                                <asp:ListItem Value="B" Text="系统用户(内部RFC和后台处理)"></asp:ListItem>
                                <asp:ListItem Value="C" Text="通讯用户(外部RFC)"></asp:ListItem>
                                <asp:ListItem Value="L" Text="参考用户"></asp:ListItem>
                                <asp:ListItem Value="S" Text="服务用户"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="td-title-width">许可证Type Id:</td>
                        <td class="td-context-width">
                            <asp:DropDownList ID="ddlTypeId" Width="150px" runat="server">
                                <asp:ListItem Value="6" Text="ABAP 工作台"></asp:ListItem>
                                <asp:ListItem Value="81" Text="IS-USER TYP 81"></asp:ListItem>
                                <asp:ListItem Value="82" Text="IS-USER TYP 82"></asp:ListItem>
                                <asp:ListItem Value="83" Text="IS-USER TYP 83"></asp:ListItem>
                                <asp:ListItem Value="84" Text="IS-USER TYP 84"></asp:ListItem>
                                <asp:ListItem Value="85" Text="IS-USER TYP 85"></asp:ListItem>
                                <asp:ListItem Value="23" Text="SAP APO-COLL"></asp:ListItem>
                                <asp:ListItem Value="20" Text="SAP APO-G.ATP"></asp:ListItem>
                                <asp:ListItem Value="21" Text="SAP APO-PP/DS"></asp:ListItem>
                                <asp:ListItem Value="22" Text="SAP APO-SCP"></asp:ListItem>
                                <asp:ListItem Value="24" Text="SAP APO-TP"></asp:ListItem>
                                <asp:ListItem Value="30" Text="SAP BW"></asp:ListItem>
                                <asp:ListItem Value="42" Text="SAP CFM: CR (incl. CFM TM)"></asp:ListItem>
                                <asp:ListItem Value="45" Text="SAP CFM: LP (incl. OP)"></asp:ListItem>
                                <asp:ListItem Value="41" Text="SAP CFM: MR (incl. CFM TM)"></asp:ListItem>
                                <asp:ListItem Value="44" Text="SAP CFM: PA (incl. CFM TM)"></asp:ListItem>
                                <asp:ListItem Value="40" Text="SAP CFM: TM (incl. OP)"></asp:ListItem>
                                <asp:ListItem Value="34" Text="SAP CRM Category II"></asp:ListItem>
                                <asp:ListItem Value="35" Text="SAP CRM Category III"></asp:ListItem>
                                <asp:ListItem Value="25" Text="SAP EBP Purchaser"></asp:ListItem>
                                <asp:ListItem Value="26" Text="SAP EBP Requisitioner"></asp:ListItem>
                                <asp:ListItem Value="63" Text="SAP NetWeaver Developer"></asp:ListItem>
                                <asp:ListItem Value="64" Text="SAP NetWeaver Professional"></asp:ListItem>
                                <asp:ListItem Value="65" Text="SAP NetWeaver User"></asp:ListItem>
                                <asp:ListItem Value="48" Text="SAP PLM"></asp:ListItem>
                                <asp:ListItem Value="38" Text="SAP SEM"></asp:ListItem>
                                <asp:ListItem Value="71" Text="SPECIAL MODULE TYPE 1"></asp:ListItem>
                                <asp:ListItem Value="72" Text="SPECIAL MODULE TYPE 2"></asp:ListItem>
                                <asp:ListItem Value="73" Text="SPECIAL MODULE TYPE 3"></asp:ListItem>
                                <asp:ListItem Value="74" Text="SPECIAL MODULE TYPE 4"></asp:ListItem>
                                <asp:ListItem Value="75" Text="SPECIAL MODULE TYPE 5"></asp:ListItem>
                                <asp:ListItem Value="91" Text="测试"></asp:ListItem>
                                <asp:ListItem Value="5" Text="基础"></asp:ListItem>
                                <asp:ListItem Value="1" Text="可操作"></asp:ListItem>
                                <asp:ListItem Value="7" Text="企业 HR"></asp:ListItem>
                                <asp:ListItem Value="3" Text="询问/响应"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td class="td-title-width"><font style="color: red;">*</font>有效期从:
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtStartDate" runat="server" onClick="WdatePicker()"
                                class="Wdate"></asp:TextBox>
                        </td>
                        <td class="td-title-width">有效期至:</td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtEndDate" runat="server" onClick="WdatePicker()"
                                class="Wdate"></asp:TextBox>
                        </td>
                        <td class="td-title-width">
                            <%--初始口令:--%>
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox Visible="false" ID="txtPassword1" TextMode="Password" runat="server"></asp:TextBox>
                        </td>
                        <td class="td-title-width">
                            <%--重复口令:--%>
                        </td>
                        <td class="td-context-width">
                            <asp:TextBox Visible="false" ID="txtPassword2" TextMode="Password" runat="server"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td class="td-title-width">小数点格式:
                        </td>
                        <td class="td-context-width">
                            <asp:DropDownList ID="dplDECIMAL_POINT_FORMAT" Width="150px" runat="server">
                                <asp:ListItem Value="" Text="小数点是逗号:N.NNN,NN"></asp:ListItem>
                                <asp:ListItem Value="X" Text="小数点是句号:N,NNN.NN"></asp:ListItem>
                                <asp:ListItem Value="Y" Text="小数点是 N NNN NNN,NN"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="td-title-width">日期格式:
                        </td>
                        <td class="td-context-width">
                            <asp:DropDownList ID="dplDATE_FORMAT" Width="150px" runat="server">
                                <asp:ListItem Value="1" Text="DD.MM.YYYY"></asp:ListItem>
                                <asp:ListItem Value="2" Text="MM/DD/YYYY"></asp:ListItem>
                                <asp:ListItem Value="4" Text="YYYY.MM.DD"></asp:ListItem>
                                <asp:ListItem Value="5" Text="YYYY/MM/DD"></asp:ListItem>
                                <asp:ListItem Value="3" Text="MM-DD-YYYY"></asp:ListItem>
                                <asp:ListItem Value="6" Text="YYYY-MM-DD"></asp:ListItem>
                                <asp:ListItem Value="7" Text="GYY.MM.DD(Japanese Date)"></asp:ListItem>
                                <asp:ListItem Value="8" Text="GYY/MM/DD(Japanese Date)"></asp:ListItem>
                                <asp:ListItem Value="9" Text="GYY-MM-DD(Japanese Date)"></asp:ListItem>
                                <asp:ListItem Value="A" Text="YYYY/MM/DD(Islamic Date 1)"></asp:ListItem>
                                <asp:ListItem Value="B" Text="YYYY/MM/DD(Islamic Date 2)"></asp:ListItem>
                                <asp:ListItem Value="C" Text="YYYY/MM/DD(Iranian Date)"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="td-title-width">时间格式:
                        </td>
                        <td class="td-context-width">
                            <asp:DropDownList ID="dplTIME_FORMAT" Width="150px" runat="server">
                                <asp:ListItem Value="0" Text="24小时格式(例如:12:08:10)"></asp:ListItem>
                                <asp:ListItem Value="1" Text="12小时格式(例如:12:05:10 PM)"></asp:ListItem>
                                <asp:ListItem Value="2" Text="12小时格式(例如:12:05:10 pm)"></asp:ListItem>
                                <asp:ListItem Value="3" Text="从0到11的小时(例如:00:05:10 PM)"></asp:ListItem>
                                <asp:ListItem Value="4" Text="从0到11的小时(例如:00:05:10 pm)"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="td-title-width">输出设备:</td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtOUTPUT_EQUIMENT" Text="LP01" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-title-width">立即输出:
                        </td>
                        <td class="td-context-width">
                            <asp:DropDownList ID="dplNOWTIME_EQUIMENT" runat="server">
                                <asp:ListItem Value="N" Text="N"></asp:ListItem>
                                <asp:ListItem Value="Y" Selected="True" Text="Y"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="td-title-width">输出后删除:
                        </td>
                        <td class="td-context-width">
                            <asp:DropDownList ID="dplOUTPUTED_DELETE" runat="server">
                                <asp:ListItem Value="N" Text="N"></asp:ListItem>
                                <asp:ListItem Value="Y" Selected="True" Text="Y"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="td-title-width"><font style="color: red;">*</font>用户时区:
                        </td>
                        <td class="td-context-width">
                            <%--<asp:TextBox ID="txtUSER_TIMEZONE" Text="UTC+8" runat="server"></asp:TextBox>--%>
                            <asp:DropDownList ID="dplUSER_TIMEZONE" Width="150px" runat="server">
                                <asp:ListItem Value="AFGHAN" Text="(AFGHAN)阿富汗 +  4 1/2 小时 非夏时制"></asp:ListItem>
                                <asp:ListItem Value="ALA" Text="(ALA)阿拉斯加 (Anchorage) -  9 小时 美国"></asp:ListItem>
                                <asp:ListItem Value="ALAW" Text="(ALAW)阿拉斯加 (阿留申) - 10 小时 美国"></asp:ListItem>
                                <asp:ListItem Value="AST" Text="(AST)大西洋时间 (Halifax) -  4 小时 美国"></asp:ListItem>
                                <asp:ListItem Value="AUSACT" Text="(AUSACT)澳大利亚首都地域 + 10 小时 澳大利亚"></asp:ListItem>
                                <asp:ListItem Value="AUSLHI" Text="(AUSLHI)澳大利亚 Lord Howe 岛 + 10 1/2 小时 澳大利亚"></asp:ListItem>
                                <asp:ListItem Value="AUSNSW" Text="(AUSNSW)澳大利亚新南威尔士 + 10 小时 澳大利亚"></asp:ListItem>
                                <asp:ListItem Value="AUSNT" Text="(AUSNT)澳大利亚北部领土 +  9 1/2 小时 非夏时制"></asp:ListItem>
                                <asp:ListItem Value="AUSQLD" Text="(AUSQLD)澳大利亚女王岛 + 10 小时 非夏时制"></asp:ListItem>
                                <asp:ListItem Value="AUSSA" Text="(AUSSA)澳洲南澳大利亚 +  9 1/2 小时 澳大利亚"></asp:ListItem>
                                <asp:ListItem Value="AUSTAS" Text="(AUSTAS)澳大利亚塔斯马尼亚 + 10 小时 澳大利亚"></asp:ListItem>
                                <asp:ListItem Value="AUSVIC" Text="(AUSVIC)澳大利亚维多利亚 + 10 小时 澳大利亚"></asp:ListItem>
                                <asp:ListItem Value="AUSWA" Text="(AUSWA)澳洲西澳大利亚 +  8 小时 非夏时制"></asp:ListItem>
                                <asp:ListItem Value="AZOREN" Text="(AZOREN)欧洲亚述尔群岛 -  1 小时 欧洲"></asp:ListItem>
                                <asp:ListItem Value="BRAZIL" Text="(BRAZIL)巴西 -  3 小时 巴西"></asp:ListItem>
                                <asp:ListItem Value="BRZLAN" Text="(BRZLAN)巴西安第斯山脉 -  5 小时 巴西"></asp:ListItem>
                                <asp:ListItem Value="BRZLEA" Text="(BRZLEA)巴西东部 -  3 小时 巴西"></asp:ListItem>
                                <asp:ListItem Value="BRZLWE" Text="(BRZLWE)巴西西部 -  4 小时 巴西"></asp:ListItem>
                                <asp:ListItem Value="CAT" Text="(CAT)中非 +  2 小时 非夏时制"></asp:ListItem>
                                <asp:ListItem Value="CET" Text="(CET)中欧 +  1 小时 欧洲"></asp:ListItem>
                                <asp:ListItem Value="CHILE" Text="(CHILE)智利 -  4 小时 智利"></asp:ListItem>
                                <asp:ListItem Value="CHILEE" Text="(CHILEE)智利复活岛 -  6 小时 智利"></asp:ListItem>
                                <asp:ListItem Value="CST" Text="(CST)中央时间 (Dallas) -  6 小时 美国"></asp:ListItem>
                                <asp:ListItem Value="CSTNO" Text="(CSTNO)中央时间非夏时制 -  6 小时 非夏时制"></asp:ListItem>
                                <asp:ListItem Value="CYPRUS" Text="(CYPRUS)塞浦路斯 +  2 小时 塞浦路斯"></asp:ListItem>
                                <asp:ListItem Value="EET" Text="(EET)东欧 +  2 小时 欧洲"></asp:ListItem>
                                <asp:ListItem Value="EGYPT" Text="(EGYPT)埃及 +  2 小时 埃及"></asp:ListItem>
                                <asp:ListItem Value="EST" Text="(EST)东部时间 (纽约) -  5 小时 美国"></asp:ListItem>
                                <asp:ListItem Value="EST_" Text="(EST_)东部时间 (Montr#al) -  5 小时 美国"></asp:ListItem>
                                <asp:ListItem Value="ESTNO" Text="(ESTNO)东部时间 (Indianapolis) -  5 小时 非夏时制"></asp:ListItem>
                                <asp:ListItem Value="FLKLND" Text="(FLKLND)福克兰群岛 -  4 小时 智利"></asp:ListItem>
                                <asp:ListItem Value="GMTUK" Text="(GMTUK)英国格林威治 (DST) +/- 0  = UTC/GMT 欧洲"></asp:ListItem>
                                <asp:ListItem Value="GST" Text="(GST)格陵兰岛 -  3 小时 欧洲"></asp:ListItem>
                                <asp:ListItem Value="GSTE" Text="(GSTE)东格陵兰 -  1 小时 欧洲"></asp:ListItem>
                                <asp:ListItem Value="GSTW" Text="(GSTW)西格陵兰 -  4 小时 欧洲"></asp:ListItem>
                                <asp:ListItem Value="HAW" Text="(HAW)夏威夷 - 10 小时 美国"></asp:ListItem>
                                <asp:ListItem Value="INDIA" Text="(INDIA)印度 +  5 1/2 小时 非夏时制"></asp:ListItem>
                                <asp:ListItem Value="IRAN" Text="(IRAN)伊朗 +  3 1/2 小时 伊朗"></asp:ListItem>
                                <asp:ListItem Value="IRAQ" Text="(IRAQ)伊拉克 +  3 小时 伊拉克 "></asp:ListItem>
                                <asp:ListItem Value="ISRAEL" Text="(ISRAEL)以色列 +  2 小时 以色列 "></asp:ListItem>
                                <asp:ListItem Value="JAPAN" Text="(JAPAN)日本 +  9 小时 非夏时制"></asp:ListItem>
                                <asp:ListItem Value="JORDAN" Text="(JORDAN)约旦 +  2 小时 约旦"></asp:ListItem>
                                <asp:ListItem Value="LBANON" Text="(LBANON)黎巴嫩 +  2 小时 黎巴嫩 "></asp:ListItem>
                                <asp:ListItem Value="MST" Text="(MST)山地时间 (丹佛) -  7 小时 美国"></asp:ListItem>
                                <asp:ListItem Value="MSTNO" Text="(MSTNO)山地时间 (菲尼克斯) -  7 小时 非夏时制"></asp:ListItem>
                                <asp:ListItem Value="NEPAL" Text="(NEPAL)尼泊尔 + 5 3/4 时数 非夏时制"></asp:ListItem>
                                <asp:ListItem Value="NFDL" Text="(NFDL)纽芬兰 -  3 1/2 小时 美国"></asp:ListItem>
                                <asp:ListItem Value="NORFLK" Text="(NORFLK)诺福克岛 + 11 1/2 小时 非夏时制"></asp:ListItem>
                                <asp:ListItem Value="NST" Text="(NST)纽芬兰 -  3 1/2 小时 美国"></asp:ListItem>
                                <asp:ListItem Value="NZCHA" Text="(NZCHA)新西兰 Chatham 岛 + 12 3/4 小时 新西兰 "></asp:ListItem>
                                <asp:ListItem Value="NZST" Text="(NZST)新西兰 + 12 小时 新西兰 "></asp:ListItem>
                                <asp:ListItem Value="PARAGY" Text="(PARAGY)巴拉圭 -  4 小时 非夏时制"></asp:ListItem>
                                <asp:ListItem Value="PIERRE" Text="(PIERRE)St Pierre 和 Miquelon -  3 小时 美国"></asp:ListItem>
                                <asp:ListItem Value="PST" Text="(PST)太平洋时间 (洛杉矶) -  8 小时 美国"></asp:ListItem>
                                <asp:ListItem Value="RUS03" Text="(RUS03)俄罗斯 (莫斯科) (UTC+03) +  3 小时 欧洲"></asp:ListItem>
                                <asp:ListItem Value="RUS04" Text="(RUS04)俄罗斯 (UTC+04) +  4 小时 欧洲"></asp:ListItem>
                                <asp:ListItem Value="RUS05" Text="(RUS05)俄罗斯 (UTC+05) +  5 小时 欧洲"></asp:ListItem>
                                <asp:ListItem Value="RUS06" Text="(RUS06)俄罗斯 (UTC+06) +  6 小时 欧洲"></asp:ListItem>
                                <asp:ListItem Value="RUS07" Text="(RUS07)俄罗斯 (UTC+07) +  7 小时 欧洲"></asp:ListItem>
                                <asp:ListItem Value="RUS08" Text="(RUS08)俄罗斯 (UTC+08) +  8 小时 欧洲"></asp:ListItem>
                                <asp:ListItem Value="RUS09" Text="(RUS09)俄罗斯 (UTC+09) +  9 小时 欧洲"></asp:ListItem>
                                <asp:ListItem Value="RUS10" Text="(RUS10)俄罗斯 (UTC+10) + 10 小时 欧洲"></asp:ListItem>
                                <asp:ListItem Value="RUS11" Text="(RUS11)俄罗斯 (UTC+11) + 11 小时 欧洲"></asp:ListItem>
                                <asp:ListItem Value="RUS12" Text="(RUS12)俄罗斯 (UTC+12) + 12 小时 欧洲"></asp:ListItem>
                                <asp:ListItem Value="RUS13" Text="(RUS13)俄罗斯 (UTC+13) + 13 小时 欧洲"></asp:ListItem>
                                <asp:ListItem Value="RUS14" Text="(RUS14)俄罗斯 (UTC+14) + 14 小时 欧洲"></asp:ListItem>
                                <asp:ListItem Value="SYRIA" Text="(SYRIA)叙利亚 +  2 小时 叙利亚 "></asp:ListItem>
                                <asp:ListItem Value="UK" Text="(UK)英格兰、爱尔兰、苏格兰 +/- 0  = UTC/GMT 欧洲"></asp:ListItem>
                                <asp:ListItem Value="UTC UTC+0" Text="(UTC UTC+0)+/- 0 #NAME? 非夏时制"></asp:ListItem>
                                <asp:ListItem Value="UTC+1" Text="(UTC+1)UTC+01 +  1 小时 非夏时制"></asp:ListItem>
                                <asp:ListItem Value="UTC+10" Text="(UTC+10)UTC+10 + 10 小时 非夏时制"></asp:ListItem>
                                <asp:ListItem Value="UTC+11" Text="(UTC+11)UTC+11 + 11 小时 非夏时制"></asp:ListItem>
                                <asp:ListItem Value="UTC+12" Text="(UTC+12)UTC+12 + 12 小时 非夏时制"></asp:ListItem>
                                <asp:ListItem Value="UTC+13" Text="(UTC+13)UTC+13 + 13 小时 非夏时制"></asp:ListItem>
                                <asp:ListItem Value="UTC+14" Text="(UTC+14)UTC+14 + 14 小时 非夏时制"></asp:ListItem>
                                <asp:ListItem Value="UTC+2" Text="(UTC+2)UTC+02 +  2 小时 非夏时制"></asp:ListItem>
                                <asp:ListItem Value="UTC+3" Text="(UTC+3)UTC+03 +  3 小时 非夏时制"></asp:ListItem>
                                <asp:ListItem Value="UTC+4" Text="(UTC+4)UTC+04 +  4 小时 非夏时制"></asp:ListItem>
                                <asp:ListItem Value="UTC+5" Text="(UTC+5)UTC+05 +  5 小时 非夏时制"></asp:ListItem>
                                <asp:ListItem Value="UTC+53" Text="(UTC+53)UTC+05:30 +  5 1/2 小时 非夏时制"></asp:ListItem>
                                <asp:ListItem Value="UTC+6" Text="(UTC+6)UTC+06 +  6 小时 非夏时制"></asp:ListItem>
                                <asp:ListItem Value="UTC+63" Text="(UTC+63)UTC+06:30 +  6 1/2 小时 非夏时制"></asp:ListItem>
                                <asp:ListItem Value="UTC+7" Text="(UTC+7)UTC+07 +  7 小时 非夏时制"></asp:ListItem>
                                <asp:ListItem Value="UTC+8" Text="(UTC+8)UTC+08 +  8 小时 非夏时制"></asp:ListItem>
                                <asp:ListItem Value="UTC+9" Text="(UTC+9)UTC+09 +  9 小时 非夏时制"></asp:ListItem>
                                <asp:ListItem Value="UTC-1" Text="(UTC-1)UTC-01 -  1 小时 非夏时制"></asp:ListItem>
                                <asp:ListItem Value="UTC-10" Text="(UTC-10)UTC-10 - 10 小时 非夏时制"></asp:ListItem>
                                <asp:ListItem Value="UTC-11" Text="(UTC-11)UTC-11 - 11 小时 非夏时制"></asp:ListItem>
                                <asp:ListItem Value="UTC-12" Text="(UTC-12)UTC-12 - 12 小时 非夏时制"></asp:ListItem>
                                <asp:ListItem Value="UTC-2" Text="(UTC-2)UTC-02 -  2 小时 非夏时制"></asp:ListItem>
                                <asp:ListItem Value="UTC-3" Text="(UTC-3)UTC-03 -  3 小时 非夏时制"></asp:ListItem>
                                <asp:ListItem Value="UTC-4" Text="(UTC-4)UTC-04 -  4 小时 非夏时制"></asp:ListItem>
                                <asp:ListItem Value="UTC-5" Text="(UTC-5)UTC-05 -  5 小时 非夏时制"></asp:ListItem>
                                <asp:ListItem Value="UTC-6" Text="(UTC-6)UTC-06 -  6 小时 非夏时制"></asp:ListItem>
                                <asp:ListItem Value="UTC-7" Text="(UTC-7)UTC-07 -  7 小时 非夏时制"></asp:ListItem>
                                <asp:ListItem Value="UTC-8" Text="(UTC-8)UTC-08 -  8 小时 非夏时制"></asp:ListItem>
                                <asp:ListItem Value="UTC-83" Text="(UTC-83)UTC-8:30 -  8 1/2 小时 非夏时制"></asp:ListItem>
                                <asp:ListItem Value="UTC-9" Text="(UTC-9)UTC-09 -  9 小时 非夏时制"></asp:ListItem>
                                <asp:ListItem Value="WAT" Text="(WAT)西非 +  1 小时 非夏时制"></asp:ListItem>
                                <asp:ListItem Value="WDFT" Text="(WDFT)Walldorf 时间 +  1 小时 欧洲"></asp:ListItem>
                                <asp:ListItem Value="WET" Text="(WET)西欧 +/- 0  = UTC/GMT 欧洲"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="td-title-width">系统时区:</td>
                        <td class="td-context-width">
                            <asp:TextBox ID="txtSYSTEM_TIMEZONE" Enabled="false" runat="server"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td class="td-title-width">备注:
                        </td>
                        <td class="td-context-width" colspan="7">
                            <asp:TextBox ID="txtMemo" TextMode="MultiLine" Height="50" Width="90%" runat="server"></asp:TextBox>
                        </td>

                    </tr>
                </table>
            </div>
        </fieldset>

        <fieldset style="border: 1px solid #efefef;">
            <legend style="font-size: 14px; color: #3C72DF">参数设置</legend>

            <asp:UpdatePanel ID="updatepanel2" runat="server">
                <ContentTemplate>


                    <div class="pure-control-group" style="margin-top: 10px;">
                        <span style="display: inline-block; width: 1000px; text-align: right;">
                            <input type="button" runat="server" id="Button1" value="添加参数" class="pure-button  pure-button-primary" style="margin-right: 20px;"
                                onclick="javascript: OpenModal1();" />
                            <input type="button" runat="server" id="Button2" onserverclick="Button2_ServerClick"
                                style="display: none;" />
                        </span>
                        <table class="pure-table pure-table-bordered" style="width: 800px; margin-top: 20px"
                            id="table1">
                            <thead>
                                <tr>
                                    <th style="width: 200px;">参数ID
                                    </th>
                                    <th style="width: 200px;">参数值
                                    </th>


                                    <th style="width: 300px;">短文本
                                    </th>

                                    <th style="width: 100px;">操作
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rptParameters" runat="server">
                                    <ItemTemplate>
                                        <tr>

                                            <td style="background: #efefef;">
                                                <asp:TextBox ID="txtparametersID" runat="server" Text='<%#Eval("PARAMENTERID") %>'></asp:TextBox>
                                            </td>
                                            <td style="background: #efefef;">
                                                <asp:TextBox ID="txtparametersvalue" runat="server" Text='<%#Eval("PARAMENTERVALUE") %>'></asp:TextBox>
                                            </td>


                                            <td style="background: #efefef;">
                                                <asp:TextBox ID="txtwenben" TextMode="MultiLine" Width="90%" runat="server" Text='<%#Eval("PARAMETERTEXT") %>'></asp:TextBox>
                                            </td>
                                            <td style="background: #efefef;">
                                                <!--OnCommand="btnDelete_click" -->
                                                <asp:Button ID="btnDelete" Enabled='<%#base.ReturnUserRole.Admin %>' runat="server" Text="删除" CommandName="par" CommandArgument='<%#Eval("id") %>' OnCommand="btnDelete_Command"
                                                    OnClientClick="javascript:return DeleteIs();" />
                                                <asp:HiddenField ID="hiddenid" runat="server" Value='<%#Eval("id") %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr>


                                            <td style="background: #fff;">
                                                <asp:TextBox ID="txtparametersID" runat="server" Text='<%#Eval("PARAMENTERID") %>'></asp:TextBox>
                                            </td>
                                            <td style="background: #fff;">
                                                <asp:TextBox ID="txtparametersvalue" runat="server" Text='<%#Eval("PARAMENTERVALUE") %>'></asp:TextBox>
                                            </td>

                                            <td style="background: white;">
                                                <asp:TextBox ID="txtwenben" TextMode="MultiLine" Width="90%" runat="server" Text='<%#Eval("PARAMETERTEXT") %>'></asp:TextBox>
                                            </td>
                                            <td style="background: white;">
                                                <!--OnCommand="btnDelete_click"-->
                                                <asp:Button ID="btnDelete" Enabled='<%#base.ReturnUserRole.Admin %>' runat="server" Text="删除" CommandName="par" CommandArgument='<%#Eval("ID") %>' OnCommand="btnDelete_Command"
                                                    OnClientClick="javascript:return DeleteIs();" />
                                                <asp:HiddenField ID="hiddenid" runat="server" Value='<%#Eval("id") %>' />
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:TextBox ID="TextBox1" runat="server" Width="816px" Style="display: none"></asp:TextBox>
        </fieldset>


        <fieldset style="border: 1px solid #efefef;">
            <legend style="font-size: 14px; color: #3C72DF">角色信息</legend>

            <asp:UpdatePanel ID="updatepanel1" runat="server">
                <ContentTemplate>


                    <div class="pure-control-group" style="margin-top: 10px;">
                        <span style="display: inline-block; width: 1000px; text-align: right;">
                            <input type="button" runat="server" id="inputNew" value="委派角色" class="pure-button  pure-button-primary" style="margin-right: 20px;"
                                onclick="javascript: OpenModal();" />
                            <input type="button" runat="server" id="btnOnLoadRoleInfo" onserverclick="btnOnLoadRoleInfo_ServerClick"
                                style="display: none;" />
                        </span>
                        <table class="pure-table pure-table-bordered" style="width: 1000px; margin-top: 20px"
                            id="tablelist">
                            <thead>
                                <tr>
                                    <th>角色ID
                                    </th>
                                    <th>角色名称
                                    </th>


                                    <th>有效期自
                                    </th>
                                    <th>有效期至
                                    </th>
                                    <th>操作
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="repeaterUserRole" runat="server">
                                    <ItemTemplate>
                                        <tr>

                                            <td style="background: #efefef;">
                                                <%#Eval("ROLEID")%>
                                            </td>
                                            <td style="background: #efefef;">
                                                <%#Eval("ROLENAME")%>
                                            </td>

                                            <td style="background: white;">
                                                <asp:TextBox ID="rep_txtstartdate" runat="server" Text='<%#Eval("START_DATE")%>' onClick="WdatePicker()" class="Wdate"></asp:TextBox>
                                                <asp:HiddenField ID="rep_hiddenid" runat="server" Value='<%#Eval("ID") %>' />
                                            </td>
                                            <td style="background: white;">
                                                <asp:TextBox ID="rep_enddate" runat="server" Text='<%#Eval("END_DATE")%>' onClick="WdatePicker()" class="Wdate"></asp:TextBox>
                                            </td>
                                            <td style="background: #efefef;">
                                                <!--OnCommand="btnDelete_click" -->
                                                <asp:Button ID="btnDelete" Enabled='<%#base.ReturnUserRole.Admin %>' CommandName="role" runat="server" Text="删除" CommandArgument='<%#Eval("ID") %>' OnCommand="btnDelete_Command"
                                                    OnClientClick="javascript:return DeleteIs();" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr>


                                            <td style="background: white;">
                                                <%#Eval("ROLEID")%>
                                            </td>
                                            <td style="background: white;">
                                                <%#Eval("ROLENAME")%>
                                            </td>
                                            <td style="background: white;">
                                                <asp:TextBox ID="rep_txtstartdate" runat="server" Text='<%#Eval("START_DATE")%>' onClick="WdatePicker()" class="Wdate"></asp:TextBox>
                                                <asp:HiddenField ID="rep_hiddenid" runat="server" Value='<%#Eval("ID") %>' />
                                            </td>
                                            <td style="background: white;">
                                                <asp:TextBox ID="rep_enddate" runat="server" Text='<%#Eval("END_DATE")%>' onClick="WdatePicker()" class="Wdate"></asp:TextBox>
                                            </td>
                                            <td style="background: white;">
                                                <!--OnCommand="btnDelete_click"-->
                                                <asp:Button ID="btnDelete" Enabled='<%#base.ReturnUserRole.Admin %>' runat="server" CommandName="role" Text="删除" CommandArgument='<%#Eval("ID") %>' OnCommand="btnDelete_Command"
                                                    OnClientClick="javascript:return DeleteIs();" />
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:TextBox ID="txtUserInfo" runat="server" Width="816px" Style="display: none"></asp:TextBox>
        </fieldset>
        <div style="margin-top: 10px; text-align: center;">
            <asp:Button ID="btnSave" runat="server" Text=" 确认 " CssClass="pure-button pure-button-primary"
                OnClick="Button1_Click" />&nbsp;&nbsp;
            <input type="button" id="btnCancel" value=" 取消 " onclick="javascript: ClosePage();"
                class="pure-button pure-button-primary" />
        </div>
    </div>
</asp:Content>
