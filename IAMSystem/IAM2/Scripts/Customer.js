var _oldvalue;
var flag = 0;
jQuery.ControllerUserType = function (_ControlID) {
    var _id = "#" + _ControlID;

    jQuery("#" + _ControlID).bind("click", function () {
        if (flag == 0) {
            _oldvalue = jQuery("#" + _ControlID).val();
            if (_oldvalue != "") {
                flag++;
            }
        }
        
    });

    jQuery("#" + _ControlID).bind("change", function () {
        var newValue = jQuery("#" + _ControlID).val();
       
        if (newValue == "") {
            alert("账号类型不能为空");
            jQuery("#" + _ControlID).val(_oldvalue);
            return;
        }
        else {
            if (_oldvalue != "") {

                var old = ReturnInt(_oldvalue);
                var _new = ReturnInt(newValue);

                if (old < _new) {
                    alert("\"其他\",\"系统\"两种类型账号不能向\"员工\"类型转移");
                    jQuery("#" + _ControlID).val(_oldvalue);
                    return;
                }
            }
            
        }

    });
}


function ReturnInt(_value)
{
    switch (_value)
    {
        case "员工": return 3;
        case "系统": return 2;
        case "其他": return 2;
        default: return 0;
    }
}