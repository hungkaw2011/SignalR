var connectionNotification = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/notification").build();

document.getElementById("sendButton").disabled = true;

connectionNotification.on("LoadNotification", function (message, counter) {
    document.getElementById("messageList").innerHTML = "";
    var notificationCounter = document.getElementById("notificationCounter");
    notificationCounter.innerHTML = "<span>(" + counter + ")</span>";
    for (let i = message.length - 1; i >= 0; i--) {
        var li = document.createElement("li");
        li.textContent = "Notification - " + message[i];
        document.getElementById("messageList").appendChild(li);
    }
});
connectionNotification.on("Notification", (message) => {
    toastr.options = {
        "closeButton": false,
        "debug": false,
        "newestOnTop": false,
        "progressBar": true,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "3000",
        "hideDuration": "1000",
        "timeOut": "15000",
        "extendedTimeOut": "30000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut",
        iconClasses: {
            error: 'fas fa-trash',
            info: 'fa fa-info',
            success: 'bi bi-envelope-open-heart-fill',
            warning: 'something',
        },
    };
    toastr.success(`New Message from ${message}</br>`);
    addSwingAnimation();
});
document.getElementById("sendButton").addEventListener("click", function (event) {
    var message = document.getElementById("notificationInput").value;
    connectionNotification.send("SendMessage", message).then(function () {
    });
    event.preventDefault();
});


connectionNotification.start().then(function () {
    connectionNotification.send("LoadMessages");
    document.getElementById("sendButton").disabled = false;
});

var swingCount = 0;
function addSwingAnimation() {
    var img = document.getElementById("notificationBell");
    img.classList.add("swing-animation");
    setTimeout(function () {
        img.classList.remove("swing-animation");
        swingCount++;
        if (swingCount < 3) {
            // Gọi lại hàm sau 0.1 giây nữa nếu chưa đạt được 3 lần rung
            setTimeout(addSwingAnimation, 100);
        } else {
            // Reset biến đếm sau khi đã rung 3 lần
            swingCount = 0;
        }
    }, 1000);
}

