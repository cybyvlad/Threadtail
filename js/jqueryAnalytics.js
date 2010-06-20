//**************************
/*jslint browser: true */ /*global jQuery: true */
/**
 * jQuery Cookie plugin
 *
 * Copyright (c) 2010 Klaus Hartl (stilbuero.de)
 * Dual licensed under the MIT and GPL licenses:
 * http://www.opensource.org/licenses/mit-license.php
 * http://www.gnu.org/licenses/gpl.html
 *
 */
jQuery.cookie = function (key, value, options) {

    // key and value given, set cookie...
    if (arguments.length > 1 && (value === null || typeof value !== "object")) {
        options = jQuery.extend({}, options);

        if (value === null) {
            options.expires = -1;
        }

        if (typeof options.expires === 'number') {
            var days = options.expires, t = options.expires = new Date();
            t.setDate(t.getDate() + days);
        }

        return (document.cookie = [
            encodeURIComponent(key), '=',
            options.raw ? String(value) : encodeURIComponent(String(value)),
            options.expires ? '; expires=' + options.expires.toUTCString() : '', // use expires attribute, max-age is not supported by IE
            options.path ? '; path=' + options.path : '',
            options.domain ? '; domain=' + options.domain : '',
            options.secure ? '; secure' : ''
        ].join(''));
    }

    // key and possibly options given, get cookie...
    options = value || {};
    var result, decode = options.raw ? function (s) { return s; } : decodeURIComponent;
    return (result = new RegExp('(?:^|; )' + encodeURIComponent(key) + '=([^;]*)').exec(document.cookie)) ? decode(result[1]) : null;
};
//**************************
function createUUID() {
    // http://www.ietf.org/rfc/rfc4122.txt
    var s = [];
    var hexDigits = "0123456789ABCDEF";
    for (var i = 0; i < 32; i++) {
        s[i] = hexDigits.substr(Math.floor(Math.random() * 0x10), 1);
    }
    s[12] = "4";  // bits 12-15 of the time_hi_and_version field to 0010
    s[16] = hexDigits.substr((s[16] & 0x3) | 0x8, 1);  // bits 6-7 of the clock_seq_hi_and_reserved to 01

    var uuid = s.join("");
    return uuid;
}

//***************

var SERVICE_URL = "http://localhost:888/x.jpg";
var COOKIE_NAME = "ThreadTailCookie"
var DEFINED_STATS_QUEUE_MAX_LENGTH = 1;
var SSID = getSSID();
var statsQueue = new Array();
function addEventToQueue(eventName, eventValue) {
    var d = new Date();
	//getting the actual utc time. We can`t send localtime to the server because we would lose time-related data.
	var utcTime = d.getTime();
	statsQueue.push({en:eventName,ev:encodeURIComponent(eventValue),t:utcTime});
    if (statsQueue.length >= DEFINED_STATS_QUEUE_MAX_LENGTH)
    {
        sendDataToServer();
    }
}
function sendDataToServer() {
    var queryString = "?ssid=" + SSID;
	var i = 0;
	var queueItem;
    while (statsQueue.length > 0) {
        queueItem = statsQueue.pop();
        queryString += "&en" + i + "=" + queueItem.en;
        queryString += "&ev" + i + "=" + queueItem.ev;
        queryString += "&t" + i + "=" + queueItem.t;
        i++;
    }
	queryString = encodeURI(queryString);
    $("<img>").attr("src", SERVICE_URL + queryString);
}
function getSSID(){
	var existingCookie = $.cookie(COOKIE_NAME);
	if (existingCookie == null)
	{
		existingCookie = createUUID();
		var options = { path: '/', expires: 10 };
		$.cookie(COOKIE_NAME,existingCookie,options);
	}
	return existingCookie;
}
function W(){
	
	//URL
	addEventToQueue('load',window.location);
	//user agent
	addEventToQueue('ua',navigator.userAgent);
	//app code name
	addEventToQueue('acn',navigator.appCodeName);
	//browser name
	addEventToQueue('bn',navigator.appName);
	//browser version
	addEventToQueue('bv',navigator.appVersion);
	//Platform
	addEventToQueue('pfm',navigator.platform);
}

(function($){
   $.watch = function(argument) {
		alert(argument);
   }
})(jQuery)