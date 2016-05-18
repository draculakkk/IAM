var URL = "HECGangweiInfo.ashx?type=";
function HECCompany() {
    $.post(URL + "Company", function (data) {
        var _val = data["mess"];
        var ojb = JSON.parse(_val);
        $(ojb).each(function (index, obj) {
            var obj = ojb[index];
            var _html = "<option value=\"" + obj.COMPANY_CODE + "\">" + obj.COMPANY_CODE + "——" + obj.COMPANY_SHORT_NAME + "</option>";
            $("#ddlCompany").append(_html);
        });
    }, "json");
}

function GetHECParentDepartMent() {
   
    var compangcode = $("#ddlCompany").val();
    $.post(URL + "ParentDepartMent", { PARENT_UNIT_CODE: "", COMPANY_CODE: compangcode },
        function (data) {
            var _val = data["mess"];
           
            var ojb = JSON.parse(_val);
            
            if (ojb.length > 0) {

                $(ojb).each(function (index, obj) {
                    var _html = "<option value=\"" + obj.UNIT_CODE + "\">" + obj.UNIT_CODE + "——" + obj.UNIT_NAME + "</option>";
                    $("#ddlParentDepartMent").append(_html);
                });
            }

        }, "json");
}

function GetHECDepartMent() {
    var compangcode = $("#ddlCompany").val();
    var parent = $("#ddlParentDepartMent").val();
    var ojb="";
    $.post(URL + "DepartMent", { PARENT_UNIT_CODE: parent, COMPANY_CODE: compangcode }, function (data) {
        var _val = data["mess"];
         ojb = JSON.parse(_val);
        
    }, "json");

    if (ojb.count > 0) {
        $(ojb).each(function (index, obj) {
            var _html = "<option value=\"" + obj.UNIT_CODE + "\">" + obj.UNIT_CODE + "——" + obj.UNIT_NAME + "</option>";
            $("#ddlDepartMent").append(_html);
        });
    }
    else {
        var _depart = $("#ddlParentDepartMent").val();
        var _companycode = $("#ddlCompany").val();
        $.post(URL + "Gangwei", { UNIT_CODE: _depart, CompanyCode: _companycode }, function (data) {
            var _val = data["mess"];
            ojb = JSON.parse(_val);
            if (ojb.length == 0)
                alert("无任何岗位供你选择");
            else {
                $(ojb).each(function (index, obj) {
                    var _html = "<option value=\"" + obj.POSTITION_CODE + "\">" + obj.POSTITION_CODE + "——" + obj.POSITION_NAME + "</option>";
                    $("#ddlGangwei").append(_html);

                });
            }
        }, "json");
        //alert("无下级部门，请直接选择岗位");
    }

    

}

function GetHECGangwei() {
    var _depart = $("#ddlDepartMent").val();
    var _companycode = $("#ddlCompany").val();
    alert(_companycode);
    $.post(URL + "Gangwei", { UNIT_CODE: _depart,CompanyCode:_companycode }, function (data) {
        var _val = data["mess"];
        var ojb = JSON.parse(_val);
        $(ojb).each(function (index, obj) {
            var _html = "<option value=\"" + obj.POSTITION_CODE + "\">" + obj.POSTITION_CODE + "^" + obj.POSITION_NAME + "</option>";
            $("#ddlGangwei").append(_html);
        });
    }, "json");
}