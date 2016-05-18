<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TransferControl.ascx.cs" Inherits="IAM.Control.TransferControl" %>
<script type="text/javascript">
    jQuery(document).ready(function () {
        jQuery("#dialog").dialog({
            autoOpen: false,
            width: 350,
            buttons: [{
                text: "OK",
                click: function () {
                    AjaxTransfer();
                }
            }, {
                text: "Cancel",
                click: function () { $(this).dialog("close"); }
            }]
        });
    });

    function OpenTransfer(_id) {
        jQuery("#hiddenYuanlai").val(_id);
        $("#dialog").dialog("open");
    }

    function AjaxTransfer() {
        var oldId = jQuery("#hiddenYuanlai").val();
        var newValue = jQuery("#txtmubiaogonghao").val();
        var systype = jQuery("#hiddentype").val();
        
       
        if (confirm("请慎用账号转移\n确定要转移该账号吗？")) {
            jQuery.post("../ExcelExportAjax.ashx?type=Transfer", { Oldvalue: oldId, newvalue: newValue,system:systype }, function (data) {
                if (data.indexOf("error") == -1) {
                    alert("转移成功");
                    $("#dialog").dialog("close");
                }
                else {
                    alert(data);
                }
            }, "text");
        }



    }

</script>

<div id="dialog" title="账号转移" style="font-weight: normal; font-size: 12px;">
    <div id="Pinfo" style="word-break: break-all; max-height: 350px;">
        <span>目标工号:</span><span><input type="text" id="txtmubiaogonghao" /><input type="hidden" id="hiddenYuanlai" />
            <input type="hidden" id="hiddentype" value='<%=SystemType %>' />
        </span>
    </div>
</div>
