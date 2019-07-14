$.ajaxSetup({
    accepts: "application/json",
    contentType: "application/json;charset=UTF-8",
    error: function (jqXHR, textStatus, errorThrown) {
        console.error(jqXHR);
        console.error(textStatus);
        console.error(errorThrown);
        //switch (jqXHR.status) {
        //    case 500:
        //        layer.msg('服务器系统内部错误', {
        //            icon: 2
        //        });
        //        break;
        //    case 401:
        //        layer.error('未登录', {
        //            icon: 2
        //        });
        //        break;
        //    case 403:
        //        layer.error('无权限执行此操作', {
        //            icon: 2
        //        });
        //        break;
        //    case 408:
        //        layer.error('请求超时', {
        //            icon: 2
        //        });
        //        break;
        //    default:
        //        layer.error('未知错误,请联系管理员', {
        //            icon: 2
        //        });
        //}
    },
    cache: false
});