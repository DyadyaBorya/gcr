$(function () {
    $(window).bind("beforeunload", window.GCR.ImageUpload.cancelImage);
    window.GCR.Notifier.add("saved", function () {
        $("#memberPhoto").attr("src", arguments[1]);
        $("#Photo").val(arguments[1]);
        window.GCR.ImageUpload.refreshImageDialog();
    });
    $("#Bio").charCount();
});