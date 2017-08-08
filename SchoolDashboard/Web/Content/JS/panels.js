﻿var tilesHandlers = [];
tilesHandlers["historyDayPanel"] = function (data, element) {

};
tilesHandlers["weatherPanel"] = function (data, element) {

};
tilesHandlers["timelinePanel"] = function (data, element) {
    let innerHtml = "<div class='line median'></div>";
    let isTop = true;

    $.each(data.timelineItems,
        function(i, el) {
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

};
tilesHandlers["awardsPanel"] = function (data, element) {
    let innerHtml = "";
    $.each(data.awards,
        function(i, el) {
            innerHtml += "<div class='award'><div class='badge'><img src='Images/awards/" +
                el.imageName +
                "'></div><div class='description'><h4>" +
                el.title +
                "</h4><p>" +
                el.description +
                "</p></div></div>\n";
        });

    $(element).find(".events").html(innerHtml);
};

function show(id) {
    $("#" + id).fadeIn();
}

function hide(id) {
    $("#" + id).fadeOut();
}

var currentTilesIds = [];

function processTiles() {
    if (currentTilesIds.length > 0) {
        $.each(currentTilesIds,
            function(index, el) {
                hide(el);
            });
    }
    setTimeout(function () {
        $.get("/GetTileShowInfo", function (data) {
            let showTime = 0;

            $.each(data, function (index, dataElement) {
                let htmlElement = $("#" + dataElement.tileId);
                
                tilesHandlers[dataElement.tileId](dataElement.data, htmlElement);

                if (showTime < dataElement.data.showTime) {
                    showTime = dataElement.data.showTime;
                }

                show(dataElement.tileId);
                currentTilesIds.push(dataElement.tileId);
            });

            setTimeout(processTiles, showTime);
        });
    }, 1000);

}

$(document).ready(function () {
    $(".panel").fadeOut();

    processTiles();
});