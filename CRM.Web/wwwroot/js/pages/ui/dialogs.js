$(function () {
    $('.js-sweetalert button').on('click', function () {
        var type = $(this).data('type');
        if (type === 'basic') {
            showBasicMessage();
        }
        else if (type === 'with-title') {
            showWithTitleMessage();
        }
        else if (type === 'success') {
            showSuccessMessage();
        }
        else if (type === 'confirm') {
            showConfirmMessage();
        }
        else if (type === 'cancel') {
            showCancelMessage();
        }
        else if (type === 'with-custom-icon') {
            showWithCustomIconMessage();
        }
        else if (type === 'html-message') {
            showHtmlMessage();
        }
        else if (type === 'autoclose-timer') {
            showAutoCloseTimerMessage();
        }
        else if (type === 'prompt') {
            showPromptMessage();
        }
        else if (type === 'ajax-loader') {
            showAjaxLoaderMessage();
        }
    });
});

//These codes takes from http://t4t5.github.io/sweetalert/
function showBasicMessage() {
    swal("این یک پیام است !");
}

function showWithTitleMessage() {
    swal("این یک پیام است !", "این بسیار قشنگ است ، اینطور نیست ؟");
}

function showSuccessMessage() {
    swal("کار عالی !", "شما بر روی دکمه کلیک کرده اید !", "success");
}

function showConfirmMessage() {
    swal({
        title: "شما مطمئن هستید؟",
        text: "شما قادر به بازیابی این پرونده تخیلی نخواهید بود!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "بله ، آن را حذف کنید!",
        closeOnConfirm: false
    }, function () {
        swal("حذف شده!", "پرونده تخیلی شما حذف شده است.", "success");
    });
}

function showCancelMessage() {
    swal({
        title: "شما مطمئن هستید؟",
        text: "شما قادر به بازیابی این پرونده تخیلی نخواهید بود!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "بله ، آن را حذف کنید!",
        cancelButtonText: "نه ، لغو لطفا !",
        closeOnConfirm: false,
        closeOnCancel: false
    }, function (isConfirm) {
        if (isConfirm) {
            swal("حذف شده!", "پرونده تخیلی شما حذف شده است.", "success");
        } else {
            swal("لغو شد", "پرونده تخیلی شما ایمن است :)", "error");
        }
    });
}

function showWithCustomIconMessage() {
    swal({
        title: "شیرین!",
        text: "این یک تصویر سفارشی است.",
        imageUrl: "/images/sm/avatar2.jpg"
    });
}

function showHtmlMessage() {
    swal({
        title: "HTML <small> عنوان </small>!",
        text: "یک پیام  <span style=\"color: #CC0000\">html<span> سفارشی.",
        html: true
    });
}

function showAutoCloseTimerMessage() {
    swal({
        title: "هشدار نزدیک خودکار!",
        text: "در عرض 2 ثانیه می بندم.",
        timer: 2000,
        showConfirmButton: false
    });
}

function showPromptMessage() {
    swal({
        title: "ورودی!",
        text: "چیزی جالب بنویسید:",
        type: "input",
        showCancelButton: true,
        closeOnConfirm: false,
        animation: "slide-from-top",
        inputPlaceholder: "چیزی بنویسید"
    }, function (inputValue) {
        if (inputValue === false) return false;
        if (inputValue === "") {
            swal.showInputError("شما باید چیزی بنویسید!"); return false
        }
        swal("خوب!", "تو نوشتی:" + inputValue, "success");
    });
}

function showAjaxLoaderMessage() {
    swal({
        title: "مثال درخواست آژاکس",
        text: "برای اجرای درخواست آژاکس ارسال کنید",
        type: "info",
        showCancelButton: true,
        closeOnConfirm: false,
        showLoaderOnConfirm: true,
    }, function () {
        setTimeout(function () {
            swal("درخواست آژاکس به پایان رسید!");
        }, 2000);
    });
}