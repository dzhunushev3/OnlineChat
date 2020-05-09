
"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/Chat").build();

//Disable send button until connection is established  
/*document.getElementById("sendBtn").disabled = true;*/

connection.on("ReceiveMessage", function (idGroup, user, message,dateTime) {
    var dt = dateTime;
    var date = dt.split("T");
    var time = date[1].split(".");
    var dt2 = "  " + date[0] + " " + time[0];
    //var msg = data.messag.replace(/&/g, "&").replace(/</g, "<").replace(/>/g, ">");
    var li = document.createElement("li");
    li.className = "d-flex justify-content-between mb-4";
    var div1 = document.createElement("div");
    div1.className = "chat-body white p-3 ml-2 z-depth-1";
    var div1Child = document.createElement("div");
    div1Child.className = "header";
    var userNameStrong = document.createElement("strong");
    userNameStrong.className = "primary-font";
    userNameStrong.innerHTML = user;
    var smallTime = document.createElement("small");
    smallTime.className = " text-muted";
    smallTime.innerHTML = dt2;
    var timeIcon = document.createElement("i");
    timeIcon.className = "far fa-clock";
    var hr = document.createElement("hr");
    hr.className = "w-100";
    var pMessage = document.createElement("p");
    pMessage.className = " mb-0";
    pMessage.innerHTML = message;
    li.appendChild(div1);
    div1.appendChild(div1Child);
    div1.appendChild(hr);
    div1.appendChild(pMessage);
    div1Child.appendChild(userNameStrong);
    div1Child.appendChild(smallTime);
    smallTime.appendChild(timeIcon);
    document.getElementById(idGroup).appendChild(li);
});
connection.on("UpdateGroup", function () {
    GetGroups();
});
connection.on( "OnlineUsers", function (data) {
    // Update list of users
    $('#onlineUsers').empty();
    for (let i =0; i<data.length;i++){
        var ul = document.getElementById("onlineUsers");
        var li = document.createElement("li");
        li.appendChild(document.createTextNode(data[i]));
        li.setAttribute("id", data[i]); // added line
        ul.appendChild(li);
    }
});

connection.start().then(function () {
}).catch(function (err) {
    return console.error(err.toString());
});

