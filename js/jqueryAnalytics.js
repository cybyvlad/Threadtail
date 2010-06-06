var SERVICE_URL = "http://localhost";

var DEFINED_STATS_QUEUE_MAX_LENGTH = 3;
var statsQueue = new Array();
function addEventToQueue(eventName, eventValue) {
    statsQueue.push({en:eventName,ev:eventValue,t:new Date().getTime()});
    if (statsQueue.length >= DEFINED_STATS_QUEUE_MAX_LENGTH)
    {
        sendDataToServer();
    }
}
function sendDataToServer() {
    var queryString = "";
    var queueItem = statsQueue.pop();
    var i = 0;
    queryString += "?en" + i + "=" + queueItem.en;
    queryString += "&ev" + i + "=" + queueItem.ev;
    queryString += "&t" + i + "=" + queueItem.t;
	    i++;
    while (statsQueue.length > 0) {
        queueItem = statsQueue.pop();
        queryString += "&en" + i + "=" + queueItem.en;
        queryString += "&ev" + i + "=" + queueItem.ev;
        queryString += "&t" + i + "=" + queueItem.t;
        i++;
    }
    $("<img>").attr("src", SERVICE_URL + queryString);
}

(function($){
   $.watch = function(argument) {
		alert(argument);
   }
})(jQuery)