if (window.GCR === undefined)
{
    window.GCR = {};
}

window.GCR.WebRequest = function (url, data, successCallback, failedCallback) {
    /// <summary>
    /// Creates an ajax post to the server to send/receive information.
    /// </summary>
    $.ajax({
        url: url,
        type: "POST",
        data: data,
        contentType: 'application/json',
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

window.GCR.PostJson = function (url, jsObject, successCallback, failedCallback) {
    /// <summary>
    /// Creates an ajax post to the server to send/receive information.
    /// </summary>

    window.GCR._WebRequest(url, JSON.stringify(jsObject), "POST", 'application/json', successCallback, failedCallback);
};

window.GCR.WebRequest = function (url, data, successCallback, failedCallback) {
    /// <summary>
    /// Creates an ajax post to the server to send/receive information.
    /// </summary>
    window.GCR._WebRequest(url, data, "POST", null, successCallback, failedCallback);
};

window.GCR._WebRequest = function (url, data, method, contentType, successCallback, failedCallback) {
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

