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

///show modals selected marketer

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

function SubmitForm() {
    var sendData = $('#SelectedMarketerForm').serializeArray().reduce(function (obj, item) {
        obj[item.name] = item.value;
        return obj;
    },
        {});


    var form_data = new FormData();

    for (var key in sendData) {
        form_data.append(key, sendData[key]);
    }

    $.ajax({
        url: "/Order/SelectMarketerModal",
        type: "POST",
        data: form_data,
        processData: false,
        contentType: false,
        beforeSend: function () {

        },
        success: function (response) {
            SelectOrderMarketerDone(response);
        },
        error: function () {

        }
    });
}

function SelectOrderMarketerDone(response) {
    if (response.status === "Success") {
        ShowMessage("اعلان", "عملیات با موفقیت انجام شد", "success");
        $("#basicModal").modal("hide");
    }
    else if (response.status === "Exist") {
        ShowMessage("اعلان", "قبلا بازاریاب داشته است", "warning");
        $("#basicModal").modal("hide");
    } else {
        ShowMessage("اعلان", "عملیات با شکست مواجه شد", "error");
    }
}

///show modals

function SelectCompanyModal(userId) {
    $.ajax({
        url: "/user/SelectComponyModal",
        type: "Get",
        data: {
            userId: userId
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

function SubmitForm() {
    var sendData = $('#SelectedCompanyForm').serializeArray().reduce(function (obj, item) {
        obj[item.name] = item.value;
        return obj;
    },
        {});


    var form_data = new FormData();

    for (var key in sendData) {
        form_data.append(key, sendData[key]);
    }

    $.ajax({
        url: "/user/SelectComponyModal",
        type: "POST",
        data: form_data,
        processData: false,
        contentType: false,
        beforeSend: function () {

        },
        success: function (response) {
            SelectCustomerCompanyDone(response);
        },
        error: function () {

        }
    });
}
function SelectCustomerCompanyDone(response) {
    if (response.status === "Success") {
        ShowMessage("اعلان", "عملیات با موفقیت انجام شد", "success");
        $("#basicModal").modal("hide");
    }
    else if (response.status === "Exist") {
        ShowMessage("اعلان", "قبلا بازاریاب داشته است", "warning");
        $("#basicModal").modal("hide");
    } else {
        ShowMessage("اعلان", "عملیات با شکست مواجه شد", "error");
    }
}

//////////////////

function SelectedMarketerForLead(leadId) {
    $.ajax({
        url: "/lead/SetLeadToMarketer",
        type: "Get",
        data: {
            leadId: leadId
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

function SubmitForm() {
    var sendData = $('#SelectedLeadToMarketerModal').serializeArray().reduce(function (obj, item) {
        obj[item.name] = item.value;
        return obj;
    },
        {});


    var form_data = new FormData();

    for (var key in sendData) {
        form_data.append(key, sendData[key]);
    }

    $.ajax({
        url: "/lead/SetLeadToMarketer",
        type: "POST",
        data: form_data,
        processData: false,
        contentType: false,
        beforeSend: function () {

        },
        success: function (response) {
            SelectleadMarketerDone(response);
        },
        error: function () {

        }
    });
}

function SelectleadMarketerDone(response) {
    if (response.status === "Success") {
        ShowMessage("اعلان", "عملیات با موفقیت انجام شد", "success");
        $("#basicModal").modal("hide");
    }
    else if (response.status === "Exist") {
        ShowMessage("اعلان", "قبلا بازاریاب داشته است", "warning");
        $("#basicModal").modal("hide");
    } else {
        ShowMessage("اعلان", "عملیات با شکست مواجه شد", "error");
    }
}

//////////////////
