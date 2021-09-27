﻿function deleteItem(url, errorTitle, errorText) {
    if (errorTitle == null || errorTitle == "undefined") {
        errorTitle = "عملیات ناموفق";
    }
    if (errorText == null || errorText == "undefined") {
        errorText = "";
    }
    Swal.fire({
        title: "هشدار !!",
        text: "آیا از حذف اطمینان دارید ؟",
        icon: "warning",
        confirmButtonText: "بله",
        showCancelButton: true,
        cancelButtonText: "خیر",
        preConfirm: () => {
            $.ajax({
                url: url,
                type: "get",
                beforeSend: function () {
                    $(".loading").show();
                },
                complete: function () {
                    $(".loading").hide();
                },
                error: function (data) {
                    ErrorAlert("مشکلی در اعملیات رخ داده", "لطفا در زمان دیگری امتحان کنید");
                }
            }).done(function (data) {
                data = JSON.parse(data);
                if (data.Status === 200) {
                    Swal.fire({
                        title: data.Title,
                        text: data.Message == null ? "اعملیات با موفقیت انجام شد" : data.Message,
                        icon: "success",
                        confirmButtonText: "باشه",
                    }).then(function (res) {
                        if (data.IsReloadPage === true) {
                            location.reload();
                        }
                    });
                } else if (data.Status === 10) {
                    ErrorAlert(data.Title, data.Message);
                } else if (data.Status === 404) {
                    Warning(data.Title, data.Message);
                }
            });
        }
    });
}
function Question(url, QuestionTitle, QuestionText, successText, callBack) {
    if (QuestionTitle == null || QuestionTitle == "undefined") {
        QuestionTitle = "آیا از انجام عملیات اطمینان دارید؟";
    }
    if (QuestionText == null || QuestionText == "undefined") {
        QuestionText = "";
    }

    Swal.fire({
        title: QuestionTitle,
        text: QuestionText,
        icon: "question",
        confirmButtonText: "بله",
        showCancelButton: true,
        cancelButtonText: "خیر",
        preConfirm: () => {
            $.ajax({
                url: url,
                type: "get",
                beforeSend: function () {
                    $(".loading").show();
                },
                complete: function () {
                    $(".loading").hide();
                },
                error: function (data) {
                    ErrorAlert("مشکلی در اعملیات رخ داده", "لطفا در زمان دیگری امتحان کنید");
                }
            }).done(function (data) {
                try {
                    data = JSON.parse(data);
                    if (data.Status === 200) {
                        Swal.fire({
                            title: data.Title,
                            text: successText == null ? data.Message : successText,
                            icon: "success",
                            confirmButtonText: "باشه",
                        }).then(function (res) {
                            if (data.IsReloadPage === true) {
                                location.reload();
                            } else {
                                if (callBack) {
                                    callBack(data.Status);
                                }
                            }
                        });
                    } else if (data.Status === 10) {
                        ErrorAlert(data.Title, data.Message);
                    } else if (data.Status === 404) {
                        Warning(data.Title, data.Message);
                    }
                } catch (ex) {
                    ErrorAlert();
                }
            });


        }
    });
}

function Success(Title, description, isReload = false) {
    if (Title == null || Title == "undefined") {
        Title = "عملیات با موفقیت انجام شد";
    }
    if (description == null || description == "undefined") {
        description = "";
    }
    Swal.fire({
        title: Title,
        text: description,
        icon: "success",
        confirmButtonText: "باشه",
    }).then((result) => {
        if (isReload === true) {
            location.reload();
        }
    });
}
function Info(Title, description) {
    if (Title == null || Title == "undefined") {
        Title = "توجه";
    }
    if (description == null || description == "undefined") {
        description = "";
    }
    Swal.fire({
        title: Title,
        text: description,
        icon: "info",
        confirmButtonText: "باشه"
    });
}
function ErrorAlert(Title, description, isReload = false) {
    if (Title == null || Title == "undefined") {
        Title = "مشکلی در عملیات رخ داده است";
    }
    if (description == null || description == "undefined") {
        description = "";
    }
    Swal.fire({
        title: Title,
        text: description,
        icon: "error",
        confirmButtonText: "باشه"
    }).then((result) => {
        if (isReload === true) {
            location.reload();
        }
    });
}
function Warning(Title, description, isReload = false) {
    if (Title == null || Title == "undefined") {
        Title = "مشکلی در عملیات رخ داده است";
    }
    if (description == null || description == "undefined") {
        description = "";
    }
    Swal.fire({
        title: Title,
        text: description,
        icon: "warning",
        confirmButtonText: "باشه"
    }).then((result) => {
        if (isReload === true) {
            location.reload();
        }
    });
}

function getCookie(cname) {
    let name = cname + "=";
    let decodedCookie = decodeURIComponent(document.cookie);
    let ca = decodedCookie.split(';');
    for (let i = 0; i < ca.length; i++) {
        let c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return decodeURIComponent(c.substring(name.length, c.length));
        }
    }
    return "";
}

function deleteCookie(cookieName) {
    document.cookie = `${cookieName}=;expires=Thu, 01 Jan 1970;path=/`;
}
$(document).ready(function () {
    loadCkeditor5();
    loadCkeditor4();
    loadDataTable();
    var result = getCookie("SystemAlert");
    if (result) {
        result = JSON.parse(result);
        if (result.Status === 200) {
            Success(result.Title, result.Message, result.isReloadPage);
        } else if (result.Status === 10) {
            ErrorAlert(result.Title, result.Message);
        } else if (result.Status === 404) {
            Warning(result.Title, result.Message);
        }
        deleteCookie("SystemAlert");
    }

    $(document).on("submit",
        'form[data-ajax="true"]',
        function (e) {
            e.preventDefault();
            var form = $(this);
            const method = form.attr("method").toLocaleLowerCase();
            const url = form.attr("action");
            var action = form.attr("data-action");
            if (method === "get") {
                const data = form.serializeArray();
                $.get(url,
                    data,
                    function (data) {
                        CallBackHandler(data);
                    });
            } else {
                var formData = new FormData(this);
                $.ajax({
                    url: url,
                    type: "post",
                    data: formData,
                    enctype: "multipart/form-data",
                    dataType: "json",
                    processData: false,
                    contentType: false,
                    beforeSend: function () {
                        $(".loading").show();
                    },
                    complete: function () {
                        $(".loading").hide();
                    },
                    success: function (data) {
                        CallBackHandler(data);
                    },
                    error: function (data) {
                        ErrorAlert();
                    }
                });
            }
            return false;
        });

    if (document.getElementById("number_input")) {
        setInputFilter(document.getElementById("number_input"),
            function (value) {
                return /^\d*\.?\d*$/.test(value);
            });
    }
    if (document.getElementById("number_input1")) {
        setInputFilter(document.getElementById("number_input1"),
            function (value) {
                return /^\d*\.?\d*$/.test(value);
            });
    }
});

function setInputFilter(textbox, inputFilter) {
    ["input", "keydown", "keyup", "mousedown", "mouseup", "select", "contextmenu", "drop"].forEach(function (event) {
        textbox.addEventListener(event, function () {
            if (inputFilter(this.value)) {
                this.oldValue = this.value;
                this.oldSelectionStart = this.selectionStart;
                this.oldSelectionEnd = this.selectionEnd;
            } else if (this.hasOwnProperty("oldValue")) {
                this.value = this.oldValue;
                this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd);
            } else {
                this.value = "";
            }
        });
    });
};

function CallBackHandler(data) {
    switch (data.Status) {
        case 200:
            Success(data.Title, data.Message, data.IsReloadPage);
            break;
        case 10:
            ErrorAlert(data.Title, data.Message, data.IsReloadPage);
            break;
        case 404:
            Warning(data.Title, data.Message, data.IsReloadPage);
            break;
        default:
    }
}

//modalSize:lg,sm,
function OpenModal(url, name, title) {
    var modalSize = 'modal-lg';
    var that = this;
    $('#' + name + ' .modal-body').html('');

    $.ajax({
        url: url,
        type: "get",
        beforeSend: function () {
            $(".loading").show();
        },
        complete: function () {
            $(".loading").hide();
        },
    }).done(function (result) {
        if (result) {
            $('#' + name + ' .modal-body').html(result);
            $('#' + name + ' .modal-title').html(title);

            $('#' + name).modal({
                backdrop: 'static',
                keyboard: true
            },
                'show');

            $('#' + name + ' .modal-dialog').removeClass('modal-lg modal-xl modal-sm modal-full');
            $('#' + name + ' .modal-dialog').addClass(modalSize);

            const form = $("#" + name + ' form');
            if (form) {
                $.validator.unobtrusive.parse(form);
            }
        }
    });

}

function OpenStaticModal(content, name, title) {

    $('#' + name + ' .modal-body').html('');///important ,so that content of modal does not be cached next times

    $('#' + name + ' .modal-body').html(content);
    $('#' + name + ' .modal-title').html(title);
    $('#' + name).modal({
        backdrop: 'static',
        keyboard: false,
        show: 'true'
    });
    if (isModal) {
        $('#' + name + ' .modal-dialog').removeClass('fullScreenModalDialog');
        $('#' + name + ' .modal-content').removeClass('fullScreenModalContent');
    } else {
        $('#' + name + ' .modal-dialog').addClass('fullScreenModalDialog');
        $('#' + name + ' .modal-content').addClass('fullScreenModalContent');
    }
}
function loadCalender() {
    if ($(".dateSelect ")) {
        $('.dateSelect').pDatepicker({
            format: 'YYYY/MM/D',
            initialValue: false
        });
    }

}
function loadCkeditor5() {
    if (!document.querySelector('.ckeditor5'))
        return;
    $("body").prepend(`<script src="/dashboard/ckeditor5/build/ckeditor.js"></script>`);

    ClassicEditor
        .create(document.querySelector('.ckeditor5'), {
            simpleUpload: {
                uploadUrl: '/Upload/Article'
            },
            toolbar: {
                items: [
                    'highlight',
                    'removeFormat',
                    '|',
                    'bold',
                    'italic',
                    'underline',
                    'alignment',
                    'link',
                    '|',
                    'bulletedList',
                    'numberedList',
                    'indent',
                    'outdent',
                    '|',
                    'fontBackgroundColor',
                    'fontFamily',
                    'fontColor',
                    'fontSize',
                    '|',
                    'htmlEmbed',
                    'imageUpload',
                    'imageInsert',
                    'mediaEmbed',
                    '|',
                    'blockQuote',
                    'insertTable',
                    'specialCharacters',
                    'horizontalLine',
                    '|',
                    'undo',
                    'redo',
                    '|',
                    'code',
                    'codeBlock',
                    'exportWord'
                ]
            },
            language: 'fa',
            image: {
                toolbar: [
                    'imageTextAlternative',
                    'imageStyle:full',
                    'imageStyle:side',
                    'linkImage'
                ]
            },
            table: {
                contentToolbar: [
                    'tableColumn',
                    'tableRow',
                    'mergeTableCells',
                    'tableProperties'
                ]
            },
            licenseKey: '',

        })
        .then(editor => {
            window.editor = editor;
        })
        .catch(error => {
            console.error(error);
        });
}

function loadCkeditor4() {
    if (!document.getElementById("ckeditor4"))
        return;

    $("body").prepend(`<script src="/dashboard/ckeditor4/ckeditor/ckeditor.js"></script>`);
    setTimeout(() => {
        CKEDITOR.replace('ckeditor4', {
            customConfig: '/dashboard/ckeditor4/ckeditor/config.js'
        });
    }, 500);
}

function loadSelect2() {

}
function loadDataTable() {
    try {
        if ($(".data-table")) {
            $(".data-table").DataTable();
        }
    } catch (ex) {

    }
}