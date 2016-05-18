/*
table列合并
---------说明----------------
合并table中相同值的列，使之跨行
---------依赖js--------------
jquery1.2或以上版本
---------输入参数------------
tableid：table元素的ID
bgrowindex：开始合并行的索引（2表示忽略前两行，一般忽略一行title）
collindex：要合并的列的索引（从零开始，1表示合并第二行）
---------注意事项------------
当要合并一个table中的多个列的时候，请从最大的collindex开始调用
---------实例----------------
jQuery(function () {
    mergeTable("testfffg", 2, 6);
    mergeTable("testfff", 2, 5);
    mergeTable("testfff", 2, 4);
    mergeTable("testfff", 2, 3);
    mergeTable("testfff", 2, 2); 
    mergeTable("testfff", 2, 1);
    mergeTable("testfff", 2, 0);
});




*/

function MT(id, row, col) {
    var $trs;
    if (row <= 1)
        $trs = $("#" + id + " tr");
    else
        $trs = $("#" + id + " tr:gt(" + (row - 2) + ")");

    $trs.each(function (i) {
        var $tr = $(this);
        var txt = $tr.find("td:eq(" + (col - 1) + ")").text();

        if ($.trim(txt) != "") {
            var $distr = $tr.nextAll("tr").find("td:eq(" + (col - 1) + "):contains('" + txt + "')").closest("tr");
            if ($distr.length > 0) {
                $distr.find("td:eq(" + (col - 1) + ")").remove();
                $tr.find("td:eq(" + (col - 1) + ")").attr("rowspan", $distr.length + 1);
            }
        }
    });
}


function mergeTable(tableid, bgrowindex, collindex) {
    try {
        var id = tableid;
        var rowindex = collindex;
        var beginRowIndex = bgrowindex;
        var rowtext = "";
        var beginIndex = 0;
        jQuery("#" + id + " tr").each(function (x) {
            if (x >= beginRowIndex) {
                var onex = jQuery(this);
                if (rowtext == "") {
                    rowtext = onex.find("td:eq(" + rowindex + ")").text();
                    beginIndex = x;
                } else if (onex.find("td:eq(" + rowindex + ")").text() == rowtext) {
                    if (x == jQuery("#" + id + " tr").length - 1) {
                        for (var i = beginIndex + 1; i <= x; i++) {
                            jQuery("#" + id + " tr:eq(" + i + ")").find("td:eq(" + rowindex + ")").hide();
                        }
                        jQuery("#" + id + " tr:eq(" + beginIndex + ")").find("td:eq(" + rowindex + ")").attr("rowSpan", x - beginIndex + 1);
                        rowtext = "";
                    }
                } else {
                    for (var i = beginIndex + 1; i < x; i++) {
                        jQuery("#" + id + " tr:eq(" + i + ")").find("td:eq(" + rowindex + ")").hide();
                    }
                    jQuery("#" + id + " tr:eq(" + beginIndex + ")").find("td:eq(" + rowindex + ")").attr("rowSpan", x - beginIndex);
                    rowtext = "";
                    rowtext = onex.find("td:eq(" + rowindex + ")").text();
                    beginIndex = x;
                }
            }
        });
    } catch (e) { }
}
/*
table列合并扩展
---------说明----------------
复制指定列的结构到指定的列
---------依赖js--------------
jquery1.2或以上版本
---------输入参数------------
tableid：table元素的ID
bgrowindex：开始合并行的索引（2表示忽略前两行，一般忽略一行title）
Sourcecollindex：要参照的列
collindex：要更新结构的列
---------注意事项------------
当要合并一个table中的多个列的时候，请从最大的collindex开始调用
---------实例----------------
jQuery(function () {
                mergeTable("testfff", 2, 1);
                mergeTableExt("testfff", 2, 1, 2);
                mergeTableExt("testfff", 2, 1, 0);
                mergeTableExt("testfff", 2, 1, 4);
});
*/
function mergeTableExt(tableid, bgrowindex, Sourcecollindex, collindex) {
    try {
        var id = tableid;
        var beginRowIndex = bgrowindex;
        var SourceCollIndex = Sourcecollindex;


        jQuery("#" + id + " tr").each(function (x) {
            if (x >= beginRowIndex) {
                var onex = jQuery(this);
                var rowspan = onex.find("td:eq(" + SourceCollIndex + ")").attr("rowSpan")-0;
                for (var j = x + 1; j < rowspan + x; j++) {
                    jQuery("#" + id + " tr:eq(" + j + ")").find("td:eq(" + collindex + ")").hide();
                }
                onex.find("td:eq(" + collindex + ")").attr("rowSpan", rowspan);
            }
        });
    } catch (e) {  }
}
