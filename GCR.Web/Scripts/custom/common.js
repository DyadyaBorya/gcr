if (window.GCR === undefined)
{
    window.GCR = {};
}

window.GCR.PostJson = function (url, jsObject, successCallback, failedCallback) {
    /// <summary>
    /// Creates an ajax post to the server to send/receive information.
    /// </summary>

    window.GCR._ServerRequest(url, JSON.stringify(jsObject), "POST", 'application/json', successCallback, failedCallback);
};

window.GCR.Post = function (url, data, successCallback, failedCallback) {
    /// <summary>
    /// Creates an ajax post to the server to send/receive information.
    /// </summary>
    window.GCR._ServerRequest(url, data, "POST", null, successCallback, failedCallback);
};

window.GCR._ServerRequest = function (url, data, method, contentType, successCallback, failedCallback) {
    /// <summary>
    /// Creates an ajax post to the server to send/receive information.
    /// </summary>
    $.ajax({
        url: url,
        type: method,
        data: data,
        contentType: contentType == null ? "application/x-www-form-urlencoded; charset=UTF-8" : contentType,
        success: function (result, textStatus, jqXHR) {
            if (typeof (successCallback) === "function") {
                successCallback(result, textStatus, jqXHR);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            if (typeof (failedCallback) === "function") {
                failedCallback(jqXHR, textStatus, errorThrown);
            }
        }
    });
};

window.GCR.StatusMessage = function (message, status) {
    var container = $('#top-message');
    var div = null;

    if (message.status) {
        div = $("<div>" + message.message + "</div>");
        container.removeClass().addClass(message.status);
    }
    else {
        div = $("<div>" + message + "</div>");
        container.removeClass().addClass(status || "success");
    }
    container.html(div);

    var triggerClick = function () {
        container.trigger('click');
    };

    var clickHandler = function () {
        container.css("border-width", "0");
        window.clearTimeout(alertTimer);
        container.animate({ height: '0' }, 400);
    };

    var alertTimer = window.setTimeout(triggerClick, 4000);

    container.click(clickHandler);
    container.css("border-width", "2px");
    container.animate({ height: div[0].offsetHeight }, 300)
};

window.GCR.QueryString = function (key, defaultValue) {
    if (window.GCR._QueryString === undefined) {
        window.GCR._QueryString = {};
        var pageURL = window.location.search.substring(1);
        var variables = pageURL.split('&');
        for (var i = 0; i < variables.length; i++) {
            var parts = variables[i].split('=');
            window.GCR._QueryString[parts[0]] = parts[1];
        }
    }

    var qs = window.GCR._QueryString[key];
    if (qs) {
        return qs;
    }
    else {
        return defaultValue;
    }
};