$(document).ready(function () {
    $("#loginForm").on("submit", function (e) {
        e.preventDefault();
         
        var data = {
            username: $("#Username").val(),
            password: $("#Password").val()
        };

        $.ajax({
            url: "/Account/login",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(data),
            success: function (response) {
                if (response.success) {
                 
                    toastr.success("Giriş başarılı, yönlendiriliyorsunuz...");
                     
                    setTimeout(function () {
                        window.location.href = "/";
                    }, 1000);
                } else {
                    toastr.error(response.message || "Giriş başarısız!");
                }
            },
            error: function (xhr) {
                toastr.error("Giriş yapılırken bir hata oluştu!");
            }
        });
    });
});