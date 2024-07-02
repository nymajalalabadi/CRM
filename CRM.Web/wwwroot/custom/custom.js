function fillPageId(pageId) {
    $('#CurrentPage').val(pageId);
    $('#filter-search').submit();
}

function ShowMessage(title, text, theme) {
    window.createNotification({
        closeOnClick: true,
        displayCloseButton: false,
        positionClass: 'nfc-bottom-right',
        showDuration: 5000,
        theme: theme !== '' ? theme : 'success',
    })({
        title: title !== '' ? title : 'اعلان',
        message: text
    });
}

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#imgPreview').attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]); // convert to base64 string
    }
}
$("#ImageFile").change(function () {
    readURL(this);
});

///show modals

function OpenSelectMarketerModal(orderId) {
    $.ajax({
        url: "/Order/SelectMarketerModal",
        type: "Get",
        data: {
            orderId: orderId
        },
        beforeSend: function () {

        },
        success: function (response) {
            $("#content").html(response);
            $("#basicModal").modal("show");
        },
        error: function () {

        }
    });
}

function SelectOrderMarketerDone(response) {
    if (response.status === "success") {
        ShowMessage("اعلان", "عملیات با موفقیت انجام شد", "success");
        $("#basicModal").modal("hide");
    }
    else if (response.status === "Exist") {
        ShowMessage("اعلان", "قبلا بازاریاب داشته است", "warning");
        $("#basicModal").modal("hide");
    } else {
        ShowMessage("اعلان", "عملیات با شکست مواجه شد", "errror");
    }
}

///show modals