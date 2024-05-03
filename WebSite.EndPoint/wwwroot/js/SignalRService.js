var chatBox = $("#ChatBox");

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/siteChathub")
    .build();

connection.start();


//connection.invoke('SendNewMessage', "بازدید کننده", "سلام این پیام از سمت کلاینت ارسال شده است");

//نمایش چت باکس برای کاربر
function showChatDialog() {
    connection.invoke('ShowChatBox', sessionStorage.getItem("roomId"));

    let today = new Date();
    let time = today.getHours() + ':' + today.getMinutes() + ':' + today.getSeconds();
    getMessage("پشتیبانی سایت", "سلام وقت بخیر 👋 . چطور میتونم کمکتون کنم؟", " " + time);
    chatBox.css("display", "block");
}

function Init() {

    setTimeout(showChatDialog, 1000);


    // هر زمان که دکمه ارسال در چت باکس کلیک شور کد های زیر اجرا می شود
    $("#NewMessageForm").on("submit", function (e) {

        e.preventDefault();
        var message = e.target[0].value;
        e.target[0].value = '';
        sendMessage(message);
    });

}

//ارسال پیام به سرور
function sendMessage(text) {
    connection.invoke('SendNewMessage', " بازدید کننده ", text);
}

//درسافت پیام از سرور
connection.on('getNewMessage', getMessage);

connection.on('ReceiveRoomId', setSession);

function getMessage(sender, message, time) {
    if (sender == "پشتیبانی سایت") {
        $(".Messages").append("<li id='admin' class='chatbox'><div class='chatbox'><span class='name'>" + sender + "</span><span class='time'>" + time + "</span></div><div class='message'>" + message + "</div></li>")
    }
    else {
        $(".Messages").append("<li id='user' class='chatbox'><div class='chatbox'><span class='name'>" + sender + "</span><span class='time'>" + time + "</span></div><div class='message'>" + message + "</div></li>")
    }
};

function setSession(roomId) {
    sessionStorage.setItem("roomId", roomId);
}

$(document).ready(function () {
    Init();
    $(".chatbox-open").on("click", function () {
        $(".chatbox-popup, .chatbox-close").fadeIn();
    });

    $(".chatbox-close").on("click", function () {
        $(".chatbox-popup, .chatbox-close").fadeOut()
    });

    $(".chatbox-maximize").on("click", function () {
        $(".chatbox-popup, .chatbox-open, .chatbox-close").fadeOut();
        $(".Messages").css("height", "700px");
        $(".chatbox-panel").fadeIn();
        $(".chatbox-panel").css({ display: "flex" });
    });

    $(".chatbox-minimize").on("click", function () {
        $(".chatbox-panel").fadeOut();
        $(".Messages").css("height", "200px");
        $(".chatbox-popup, .chatbox-open, .chatbox-close").fadeIn();
    });
    $(".chatbox-panel-close").on("click", function () {
        $(".chatbox-panel").fadeOut();
        $(".chatbox-open").fadeIn();
    });
});