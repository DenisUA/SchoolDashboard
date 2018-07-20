var tilesHandlers = [];
tilesHandlers["historyDayPanel"] = function (data, element) {
    $(element).find("h1").text(data.title);
    $(element).find("p").text(data.text);
    $(element).find("img").attr("src", data.image);
};

tilesHandlers["weatherPanel"] = function (data, element) {

};

tilesHandlers["famousBirthday"] = function (data, element) {
    let el = $(element);
    el.find("h2").text(data.name);
    el.find("p").text(data.description);
    el.find("img").attr("src", "/Images/FamousBirthdays/" + data.pictureFileName);
};

tilesHandlers["timelinePanel"] = function (data, element) {
    let innerHtml = "<div class='line median'></div>";
    let isTop = true;

    $.each(data.timelineItems,
        function (i, el) {
            innerHtml += "<div class='event'>" +
                            "<div class='event-wrapper " + (isTop ? "top" : "bot") + "'>" +
                            "<div class='event-time'>" +
                            "<div class='day-digit'>" + el.day + "</div>" +
                            "<div class='day'>" + el.mounth +
                            "</div>" +
                            "</div>" +
                            "<div class='event-description'>" +
                            (el.hasTime ? "<div class='time'><div class='event-icon'><i class='fa fa-15x fa-clock-o' aria-hidden='true'></i></div>" + el.time + "</div>" : "") +
                            "<div class='location'>" +
                            "<div class='event-icon'>" +
                            "<i class='fa fa-15x fa-map-marker' aria-hidden='true'></i>" +
                            "</div>" + el.place +
                            "</div>" + el.description +
                            "</div>" +
                            "</div>" +
                            "<div class='line circle'></div>" +
                            "</div>";

            innerHtml += i == (data.timelineItems.length - 1) ? "" : "<div class='line long'></div>";

            isTop = !isTop;
        });

    innerHtml += "<div class='line median'></div>";
    innerHtml += "<svg height='20' width='42'>" +
        "<line x1='1' y1='0' x2='1' y2='20' style='stroke:rgba(255, 255, 255, 0.25);stroke-width:2' />" +
        "<circle cx='11' cy='10' r='3' fill='rgba(255, 255, 255, 0.25)' />" +
        "<circle cx='21' cy='10' r='3' fill='rgba(255, 255, 255, 0.25)' />" +
        "<circle cx='31' cy='10' r='3' fill='rgba(255, 255, 255, 0.25)' />" +
        "<line x1='41' y1='0' x2='41' y2='20' style='stroke:rgba(255, 255, 255, 0.25);stroke-width:2' />" +
        "</svg>";

    $(element).find(".timeline").html(innerHtml);
};

tilesHandlers["birthdaysPanel"] = function (data, element) {
    let template = "<div class='person'>"+
                        "<img class='avatar {1}' src='/Images/BPhotos/{2}'>"+
                        "<h4>{3}</h4>"+
                        "<h5>{4}</h5>"+
                    "</div>";
    let res = "";
    $.each(data.items, function (i, el) {
        res += formatString(template, el.isMale ? "male" : "female", el.isMale ? "male1.png" : "female1.png", el.name, el.description);
    });

    $(element).find(".people").html(res);
};

tilesHandlers["awardsPanel"] = function (data, element) {
    let innerHtml = "";
    $.each(data.awards,
        function (i, el) {
            innerHtml += "<div class='award'><div class='badge'><img src='/Images/awards/" +
                el.imageName +
                "'></div><div class='description'><h4>" +
                el.title +
                "</h4><p>" +
                el.description +
                "</p></div></div>\n";
        });

    $(element).find(".events").html(innerHtml);
};

tilesHandlers["noticesPanel"] = function (data, element) {
    let innerHtml = "";
    $.each(data.notices, function (i, el) {
        innerHtml += "<div class=\"item" + (el.isImportant ? " important" : "") + "\">";
        innerHtml += "<h3>" + el.title + "</h3>";
        innerHtml += "<blockquote>";
        innerHtml += "<p>" + el.dateString + "</p>";
        innerHtml += el.text;
        innerHtml += "</blockquote>";
        innerHtml += "</div>\n";
    })

    $(element).find(".items-list").html(innerHtml);
}

function show(id) {
    $("#" + id).fadeIn();
}

function hide(id) {
    $("#" + id).fadeOut();
}

var currentTilesIds = [];

function processTiles() {

    $.get("/Dashboard/GetTileShowInfo", function (data) {

        if (currentTilesIds.length > 0 && currentTilesIds.every(function (el, i){return el == data[i].tileId})) {
            setTimeout(processTiles, 1000);
            return;
        }

        if (currentTilesIds.length > 0) {
            $.each(currentTilesIds,
                function (index, el) {
                    hide(el);
                });

            currentTilesIds = [];
        }

        let showTime = 0;

        setTimeout(function () {
            $.each(data, function (index, dataElement) {
                let htmlElement = $("#" + dataElement.tileId);

                tilesHandlers[dataElement.tileId](dataElement.data, htmlElement);

                if (showTime < dataElement.data.showTime) {
                    showTime = dataElement.data.showTime;
                }

                show(dataElement.tileId);
                currentTilesIds[index] = dataElement.tileId;
            });

            setTimeout(processTiles, showTime);
        }, 1000);
    });
}

$(document).ready(function () {
    $(".panel").fadeOut();

    processTiles();
});

function formatString()
{
    var args = [].slice.call(arguments);
    //if (this.toString() != '[object Object]') {
    //    args.unshift(this.toString());
    //}

    var pattern = new RegExp('{([1-' + args.length + '])}', 'g');
    return String(args[0]).replace(pattern, function (match, index) { return args[index]; });
}