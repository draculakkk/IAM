function TableChangeClick() {
    jQuery("#table1 td").bind("click", function () {

        var athis = this;
        jQuery("#table1 td").each(function () {
            if (athis != this) {
                jQuery(this).css("backgroundColor", "").siblings().css("backgroundColor", "");

            }
            jQuery(athis).css("backgroundColor", "#FFFF00").siblings().css("backgroundColor", "#FFFF00");
        });
    });
}




function TableChangeClickTick(_tableID, _TrColor, _TdColor) {
    var sameColor;
    if (_TrColor == "")
    { _TrColor = "#efefef"; }
    if (_TrColor=="")
    {_TdColor = "#333699";}
    jQuery("#" + _tableID + " tr").bind("mouseover", function () { sameColor = jQuery(this).css("backgroundColor"); jQuery(this).css("backgroundColor", _TrColor); });//给table 的tr 绑定一个mouseover事件
    jQuery("#" + _tableID + " tr").bind("mouseout", function () { jQuery(this).css("backgroundColor", sameColor); }); //给table de tr 绑定一个mouseout 事件
    jQuery("#" + _tableID + " td").bind("click", function () {

        var athis = this;
        jQuery("#" + _tableID + " td").each(function () {
            if (athis != this) {
                jQuery(this).css("backgroundColor", "").siblings().css("backgroundColor", "");

            }
            jQuery(athis).css("backgroundColor", _TdColor).siblings().css("backgroundColor", _TdColor);
        });
    });//给table 的所有td 绑定一个click 选中事情
}