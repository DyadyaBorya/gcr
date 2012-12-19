if (window.GCR.ImageUpload === undefined) {
    window.GCR.ImageUpload = {};
}


window.GCR.ImageUpload.saveImage = function () {
    $("input[value=save]", $("iframe")[0].contentDocument).click();
}

window.GCR.ImageUpload.cancelImage = function () {
    $("input[value=cancel]", $("iframe")[0].contentDocument).click();
}

window.GCR.ImageUpload.refreshImageDialog = function () {
    $("iframe").attr("src", $("iframe")[0].src);
}

window.GCR.ImageUpload.showImageDialog = function () {
    $('.image-uploader').dialog({
        modal: true, width: '860px', resizable: false, buttons: {
            Ok: function () {
                window.GCR.ImageUpload.saveImage();
                $(this).dialog("close");
            },
            Cancel: function () {
                window.GCR.ImageUpload.cancelImage();
                $(this).dialog("close");
                window.GCR.ImageUpload.refreshImageDialog();
            }
        }
    });
}

if (window.GCR.Notifier === undefined) {

    window.GCR.Notifier = new function Notifier() {
        var self = this;
        this.notify = function () {
            var newArgs = [];
            for (var i = 1; i < arguments.length; i++) {
                newArgs.push(arguments[i]);
            }

            $(self).trigger(arguments[0], newArgs);
        };
        this.add = function (eventName, handler) {
            $(self).bind(eventName, handler);
        };
        this.remove = function (eventName, handler) {
            $(self).unbind(eventName, handler);
        };
    };
}

