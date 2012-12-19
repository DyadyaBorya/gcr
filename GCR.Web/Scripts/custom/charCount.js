(function ($) {

    $.fn.charCount = function (options) {

        var defaults = {
            counterText: 'characters left: '
        };

        var options = $.extend(defaults, options);

        function calculate(textArea) {

            var count = $(textArea).val().length;
            var available = options.allowed - count;

            if (available < 0) {
                $(textArea).next().addClass("exceeded");
            }
            else {
                $(textArea).next().removeClass("exceeded");
            }
            updateText(textArea, options.counterText + available);
        };

        function updateText(textArea, text) {

            $(textArea).next().html(text);
        };

        function setAllowed(textArea, defaultValue) {

            if (options.allowed === undefined || options.allowed == null)
            { 
                var val = $(textArea).attr("data-val-length-max");
                if (val !== undefined && val != null) {
                    options.allowed = parseInt(val);
                }
                else {
                    options.allowed = defaultValue;
                }
            }
        };

        this.each(function () {

            var textArea = this;
            var $textArea = $(textArea);

            var container = $('<div class="char-counter"></div>');
            var counter = $('<span class="counter"></span>');

            $textArea.before(container)
            container.append(textArea);
            container.append(counter);

            setAllowed(textArea, 200);
            calculate(textArea);

            $textArea.click(function () { calculate(textArea) });
            $textArea.keyup(function () { calculate(textArea) });
            $textArea.change(function () { calculate(textArea) });
            
        });
    };
})(jQuery);