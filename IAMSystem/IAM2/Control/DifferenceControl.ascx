<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DifferenceControl.ascx.cs" Inherits="IAM.Control.DifferenceControl" %>
  <script type="text/javascript">
      jQuery(document).ready(function () {
          jQuery("#dialog").dialog({
              autoOpen: false,
              width: 350,
              buttons: [{
                  text: "OK",
                  click: function () {
                      var _values = jQuery("#ddlselect").val();
                      $(this).dialog("close");
                      window.location.href = "../" + _values;
                  }
              }, {
                  text: "Cancel",
                  click: function () { $(this).dialog("close"); }
              }]
          });
      });

      function OpenOther() {
          $("#dialog").dialog("open");
      }
    </script>

<div id="dialog" title="请选则差异分析工具类别" style="font-weight: normal; font-size: 12px;">
        <div id="Pinfo" style="word-break: break-all; max-height: 350px;">
            <select id="ddlselect" style="width:320px;height:36px;color:#000;font-size:14px;">
                <option value="Report/DifferenceReport.aspx">用户与用户模版差异分析</option>
                <option value="Report/DifferenceReportByTemplateName.aspx">模版与模版模版差异分析</option>
                <option value="Report/DifferenceReportByUserAndTemplate.aspx">用户与模版差异分析</option>
            </select>
        </div>
    </div>