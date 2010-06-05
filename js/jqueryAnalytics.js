var SERVICE_URL = "http://localhost";

var DEFINED_STATS_QUEUE_MAX_LENGTH = 1;
var statsQueue = new Array();
function addEventToQueue(eventName, eventValue) {
    statsQueue.push({en:eventName,ev:eventValue});
    if (statsQueue.length >= DEFINED_STATS_QUEUE_MAX_LENGTH)
    {
        sendDataToServer();
    }
}

function sendDataToServer() {
    var queryString = "";
    var queItem = statsQueue.pop();
    var i = 0;
    queryString += "?en" + i + "=" + queItem.en;
    queryString += "&ev" + i + "=" + queItem.ev;
    i++;
    while (statsQueue.length > 0) {
        queItem = statsQueue.pop();
        queryString += "?en" + i + "=" + queItem.en;
        queryString += "&ev" + i + "=" + queItem.ev;
        i++;
    }
    $("<img>").attr("src", SERVICE_URL + queryString);
}
