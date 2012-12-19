$(function ()
{
    CreateCommands();

    var _createSuccess = function (result) {
        if (result.status) {
            window.GCR.StatusMessage(result);
        }
        else
        {
            $("#HomepagePhotoContainer").append(result);
            window.GCR.StatusMessage("New Photo Saved Successfully!", "success");
        }
    };
    var _createFailure = function () {
        window.GCR.StatusMessage("New Photo could not be Saved!", "error");
    };
    var _saveSuccess = function (result) {
        if (result.returnUrl) {
            window.location = result.returnUrl;
            return;
        }
        if (result.status) {
            window.GCR.StatusMessage(result);
        }
    };
    var _saveFailure = function () {
        window.GCR.StatusMessage("Order could not be Saved!", "error");
    };
    var _create = function (path) {
        window.GCR.Post(_createActionUrl, { PhotoPath: path }, _createSuccess, _createFailure);
    };

    var _saveOrder = function (jsObject) {
        window.GCR.PostJson(_saveActionUrl, jsObject, _saveSuccess, _saveFailure);
    };

    $(window).bind("beforeunload", window.GCR.ImageUpload.cancelImage);

    $("#saveOrder").bind("click", function () {
        var photos = [];
        $("#HomepagePhotoContainer").children().each(
            function (index, ele) {
                var id = parseInt($(ele).attr("data-id"));
                photos.push({ Id: id, Order: index + 1 });
            });
        _saveOrder(photos);
    });

    window.GCR.Notifier.add("saved", function () {
        _create(arguments[1]);
        window.GCR.ImageUpload.refreshImageDialog();
    });

    $("#HomepagePhotoContainer").sortable
        ({
            items: "li",
            distance: 10
        })
        .bind("sortupdate", function (evt, ui) {
            window.GCR.HomePagePhoto.reorder();
        }).disableSelection();
});

function CreateCommands()
{
    window.GCR.HomePagePhoto = {};

    window.GCR.HomePagePhoto.reorder = function () {
        $("#HomepagePhotoContainer").children().each(
            function (index, ele) {
                $(".display-order", ele).html(index + 1);
        });
    };

    window.GCR.HomePagePhoto.moveUp = function (ele) {
        var li = $(ele).parents("li:first")
        var prev = li.prev();
        if (prev.length > 0) {
            li.insertBefore(prev);
            window.GCR.HomePagePhoto.reorder();
        }
    };

    window.GCR.HomePagePhoto.moveDown = function (ele) {
        var li = $(ele).parents("li:first")
        var next = li.next();
        if (next.length > 0) {
            li.insertAfter(next);
            window.GCR.HomePagePhoto.reorder();
        }

    };
}