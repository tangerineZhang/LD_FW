function getUrlParam(name) {
    // 取得url中?后面的字符
    var query = window.location.href.substring(1);
    //alert("1、"+query);
    // 把参数按&拆分成数组
    var param_str = query.substring(query.indexOf("?") + 1, query.length);
    //alert("param_str:" + param_str);
    var param_arr = param_str.split("&");

    for (var i = 0; i < param_arr.length; i++) {
        var pair = param_arr[i].split("=");
        //alert("2、" +pair[0]);
        if (pair[0] == name) {
            return pair[1];
        }
    }
    return (false);
};