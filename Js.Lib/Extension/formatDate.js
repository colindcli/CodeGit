//日期时间格式化
var formatDate = function(dateTime, format) {
    format = format.replace("yyyy", dateTime.getFullYear());
    format = format.replace("yy", dateTime.getFullYear().toString().substr(2));
    format = format.replace("MM", ("0" + (dateTime.getMonth() + 1).toString()).slice(-2));
    format = format.replace("dd", ("0" + (dateTime.getDate()).toString()).slice(-2));
    format = format.replace("hh", ("0" + (dateTime.getHours()).toString()).slice(-2));
    format = format.replace("mm", ("0" + (dateTime.getMinutes()).toString()).slice(-2));
    format = format.replace("ss", ("0" + (dateTime.getSeconds()).toString()).slice(-2));
    format = format.replace("ms", ("0" + (dateTime.getMilliseconds()).toString()).slice(-2));
    return format;
};
String.prototype.ToDate = function() {
    var dateTime = new Date(parseInt(this.substring(6, this.length - 2)));
    return formatDate(dateTime, "yyyy-MM-dd");
}
String.prototype.ToDatetime = function() {
    var dateTime = new Date(parseInt(this.substring(6, this.length - 2)));
    return formatDate(dateTime, "yyyy-MM-dd HH:mm");
}