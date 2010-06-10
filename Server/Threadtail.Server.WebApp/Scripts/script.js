window.onload = sendDataToServer;

function sendDataToServer() {
    alert("boo");

    // <img src="x.jpg?x=1;y=2"/>

    var img = document.createElement("img");
    img.setAttribute("id", "hiddenImage");

    var imgSrcValue = "x.jpg?browser=" + navigator.userAgent + ";" + "url="+ document.location;
    img.setAttribute("src", imgSrcValue);
    
    img.setAttribute("width", "10");
    img.setAttribute("height", "10");

    var x = document.getElementById("myDiv");
    x.appendChild(img);

    alert("alles gut");
}