$.fn.extend({
    animateCss: function (animationName, isIn) {
        var animationEnd = 'webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend';
        if (isIn) {
            $(this).removeClass("hidden");
            this.addClass('animated ' + animationName).one(animationEnd,
                function() {
                    $(this).removeClass('animated ' + animationName);
                });
        } else {
            this.addClass('animated ' + animationName).one(animationEnd,
                function () {
                    $(this).removeClass('animated ' + animationName).addClass("hidden");
                });
        }

        return this;
    }
});

function show() {
    $("#historyDayPanel").animateCss("fadeInRight", true);
}

function hide() {
    $("#historyDayPanel").animateCss("fadeOutLeft", false);
}