//根据本地/服务器时间获取指定时区时间 new Date指定时区时间
function getTimeByTimeZone(timeZone) {
    var d = new Date();
    localTime = d.getTime(),
        localOffset = d.getTimezoneOffset() * 60000, //获得当地时间偏移的毫秒数,这里可能是负数  
        utc = localTime + localOffset, //utc即GMT时间  
        offset = timeZone, //时区，北京市+8  美国华盛顿为 -5  
        localSecondTime = utc + (3600000 * offset); //本地对应的毫秒数  
    var date = new Date(localSecondTime);
    console.log("根据本地时间得知" + timeZone + "时区的时间是 " + date.toLocaleString());
    console.log("系统默认展示时间方式是：" + date)
    return date;
}

getTimeByTimeZone(8)