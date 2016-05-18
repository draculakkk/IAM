//页面滚动而始终在页面顶端
function menuFixed(id) {
    var obj = document.getElementById(id);
    var _getHeight = obj.offsetTop;

    window.onscroll = function () {
        changePos(id, _getHeight);
    }
}
function changePos(id, height) {
    var obj = document.getElementById(id);
    var scrollTop = document.documentElement.scrollTop || document.body.scrollTop;
    if (parseInt(scrollTop) - 80 < height) {
        obj.style.position = 'relative';
        obj.style.background = '';
        obj.style.margin = "10px 0px 10px 0px";
    } else {
        obj.style.position = 'fixed';
        obj.style.background = 'white';
        obj.style.margin = "0px";

    }
}