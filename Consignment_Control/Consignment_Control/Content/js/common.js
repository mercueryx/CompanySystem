$(function () {
    $('.IsAllowDecimal').on('input', function (e) {
        if (/^(\d+(\.\d{0,2})?)?$/.test($(this).val())) {
            // Input is OK. Remember this value
            $(this).data('prevValue', $(this).val());
        } else {
            // Input is not OK. Restore previous value
            $(this).val($(this).data('prevValue') || '');
        }
    }).trigger('input'); // Initialise the `prevValue` data properties

    $('.IsAllowNumberOnly').on('input', function (e) {
        if (/^(\d+(\d{0,2})?)?$/.test($(this).val())) {
            // Input is OK. Remember this value
            $(this).data('prevValue', $(this).val());
        } else {
            // Input is not OK. Restore previous value
            $(this).val($(this).data('prevValue') || '');
        }
    }).trigger('input'); // Initialise the `prevValue` data properties

    $('.IsAllowCommanAndNumOnly').on('input', function (e) {
        if (/^[0-9,]*$/.test($(this).val())) {
            // Input is OK. Remember this value
            $(this).data('prevValue', $(this).val());
        } else {
            // Input is not OK. Restore previous value
            $(this).val($(this).data('prevValue') || '');
        }
    }).trigger('input'); // Initialise the `prevValue` data properties
});

// Disable When submit
$(document).on('submit', 'form', function () {
    var buttons = $(this).find('[type="submit"]');
    setTimeout(function () {
        buttons.each(function (btn) {
            $(buttons[btn]).text("Please Wait...");
            $(buttons[btn]).prop('disabled', true);
        });
    });
});
// CSRF (XSRF) security
function addAntiForgeryToken(data) {
    //if the object is undefined, create a new one.
    if (!data) {
        data = {};
    }
    //add token
    var tokenInput = $('input[name=__RequestVerificationToken]');
    if (tokenInput.length) {
        data.__RequestVerificationToken = tokenInput.val();
    }
    return data;
};
// Generate Alert Box
function returnMsg(element, lvl, msg) {
    if (lvl === 3) {
        $(element).append("<div class='alert alert-warning alert-dismissible dynamicAlert'>" +
            "<button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button>" +
            msg +
            "</div>");
        $("html, body").animate({ scrollTop: 0 }, "slow");
    }
    if (lvl === 2) {
        $(element).append("<div class='alert alert-danger alert-dismissible dynamicAlert'>" +
            "<button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button>" +
            msg +
            "</div>");
        $("html, body").animate({ scrollTop: 0 }, "slow");
    }
    if (lvl === 1) {
        $(element).append("<div class='alert alert-success alert-dismissible dynamicAlert'>" +
            "<button type='button' class='close' data-dismiss='alert' aria-hidden='true'>×</button>" +
            msg +
            "</div>");
        $("html, body").animate({ scrollTop: 0 }, "slow");
    }
};

// Generate Alert Box
function returnModalMsg(lvl, msg) {
    $('input.form').prop('disabled', true);
    $('#modal-common-body').html('');
    //var obj = document.createElement("audio");
    //obj.src = "/Content/Media/Sound/beep-01a.mp3";
    
    if (lvl === 1) {
        $('#modal-common-body').append("<p style='color: green;font-weight: bold;font-size: x-large;'>" + '<i class="far fa-check-circle"></i></i><br/>' + msg + "</p>");
    }
    if (lvl === 2) {
        //obj.play();
        $('#modal-common-body').append("<p style='color: red;font-weight: bold;font-size: x-large;'>" + '<i class="fas fa-times"></i><br/>' + msg + "</p>");
    }
    if (lvl === 3) {
        //obj.play();
       $('#modal-common-body').append("<p style='color: red; font-weight: bold;font-size: x-large;'>" + '<i class="fas fa-exclamation-triangle"></i><br/>' + msg + "</p>");
    }
    $('#modal-common-display').modal('show');
};

// Clear textbox by Dynamic Class Name
function ClearTxtbyClass(cls) {
    $("." + cls).val('');
}

// Set Menu Navigation
function setNavigation(val) {
    var path = window.location.pathname;
    path = path.replace(/\/$/, "");
    path = decodeURIComponent(path);
    var _menu = val;

    $(".main-sidebar .sidebar nav ul li a").each(function () {
        var href = $(this).attr('href');
        var setMenu = $(this).attr('data-menu');
        if (path.substring(0, href.length) === href || _menu === setMenu) {
            $(this).addClass('active');
            $(this).closest('.has-treeview').addClass('menu-open');
            $(this).closest('.nav-item ').addClass('active');
        }
    });
}

