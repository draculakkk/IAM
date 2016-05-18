<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master.Master" AutoEventWireup="true" CodeBehind="HECGangwei.aspx.cs" Inherits="IAM2.Admin.HECGangwei" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../Scripts/HEC_Gangwei_JavaScript.js?time=77" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            if (typeof (JSON) == 'undefined')
            {
                $("head").append($("<script type='text/javascript' scr='../Scripts/json2.js'/>"));
            }

            HECCompany();
            var defaultString = "<option value=\"\">===请选择===</option>";
            $("#ddlCompany").on("change", function () {

                if ($("#ddlCompany").val() != "") {
                    $("#ddlParentDepartMent option").remove();
                    $("#ddlParentDepartMent").append(defaultString);
                    $("#ddlDepartMent option").remove();
                    $("#ddlDepartMent").append(defaultString);
                    $("#ddlGangwei option").remove();
                    $("#ddlGangwei").append(defaultString);
                    GetHECParentDepartMent();
                }
                else {
                    $("#ddlParentDepartMent option").remove();
                    $("#ddlParentDepartMent").append(defaultString);
                    $("#ddlDepartMent option").remove();
                    $("#ddlDepartMent").append(defaultString);
                    $("#ddlGangwei option").remove();
                    $("#ddlGangwei").append(defaultString);
                }

            });
            $("#ddlParentDepartMent").on("change", function () {
                if ($("#ddlParentDepartMent").val() != "") {
                    $("#ddlDepartMent option").remove();
                    $("#ddlDepartMent").append(defaultString);
                    $("#ddlGangwei option").remove();
                    $("#ddlGangwei").append(defaultString);
                    GetHECDepartMent();
                }
                else {
                    $("#ddlDepartMent option").remove();
                    $("#ddlDepartMent").append(defaultString);
                    $("#ddlGangwei option").remove();
                    $("#ddlGangwei").append(defaultString);
                }
            });
            $("#ddlDepartMent").on("change", function () {
                if ($("#ddlDepartMent").val() != "") {
                    $("#ddlGangwei option").remove();
                    $("#ddlGangwei").append(defaultString);
                    GetHECGangwei();
                }
                else {
                    $("#ddlGangwei option").remove();
                    $("#ddlGangwei").append(defaultString);
                }
            });
        });
        function ReturnBack() {
            var companyCode = $("#ddlCompany").val();
            var companyName = $("#ddlCompany option:selected").text();
            var departMentCode = $("#ddlDepartMent").val();
            var departMentName = $("#ddlDepartMent option:selected").text();
            var gangweiCode = $("#ddlGangwei").val();
            var gangweiName = $("#ddlGangwei option:selected").text();
            var ParmentName = $("#ddlParentDepartMent option:selected").text();
            if (companyCode == "") {
                alert("请选择公司");
                return;
            }
            if (departMentCode == ""&&gangweiCode=="") {
                alert("请选择部门");
                return;
            }
            if (gangweiCode == "") {
                alert("请选择岗位");
                return;
            }
            var FlashString = "";
            if (departMentCode == "" && gangweiCode != "")
            {
                FlashString = companyName.replace("——", "^") + "^" + ParmentName.replace("——", "^") + "^" + gangweiName.replace("——","^");
            }
            else
            {
                FlashString = companyName.replace("——", "^") + "^" + ParmentName.replace("——", "^") + "^" + gangweiName.replace("——", "^");
            }
            //alert(FlashString);
            window.returnValue = FlashString;
            window.close();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pure-control-group" style="margin-top: 10px;">
        <input type="hidden" id="hidden1" />
        <fieldset style="border: 1px solid #efefef;">
            <legend style="font-size: 14px; color: #3C72DF">添加岗位信息</legend>
            <div class="pure-control-group" style="margin-top: 10px;">
                <table>
                    <tr>
                        <td class="td-title-width">公司名称:
                        </td>
                        <td class="td-context-width" colspan="2">
                            <select id="ddlCompany" style="width: 300px">
                                <option value="">===请选择===</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-title-width">部门:
                        </td>
                        <td class="td-context-width" colspan="2">
                            <select id="ddlParentDepartMent" style="width: 300px">
                                <option value="">===请选择===</option>
                            </select>
                        </td>
                    </tr>
                    <tr style="display:none">
                        <td class="td-title-width">部门:
                        </td>
                        <td class="td-context-width" colspan="2">
                            <select id="ddlDepartMent" style="width: 300px">
                                <option value="">===请选择===</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-title-width">岗位:
                        </td>
                        <td class="td-context-width" colspan="2">
                            <select id="ddlGangwei" style="width: 300px">
                                <option value="">===请选择===</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td class="td-context-width" style="text-align: center" colspan="3">
                            <input type="button" value=" 确 定 " runat="server" id="inputYes" class="pure-button  pure-button-primary" onclick="javascript: ReturnBack();" />
                            <input type="button" value=" 取 消 " onclick="javascript: window.close();" class="pure-button  pure-button-primary" />
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>
    </div>
</asp:Content>
