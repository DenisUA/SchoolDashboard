var tilesHandlers = [];
tilesHandlers["historyDayPanel"] = function (data, element) {

};
tilesHandlers["weatherPanel"] = function (data, element) {

};
tilesHandlers["timelinePanel"] = function (data, element) {

};
tilesHandlers["birthdaysPanel"] = function (data, element) {

};
tilesHandlers["awardsPanel"] = function (data, element) {
    var innerHtml = "";
    $.each(data.awards, function(i, el) {
        innerHtml += "<div class='award'><div class='badge'><img src='Images/awards/" +
            el.imageName + "'></div><div class='description'><h4>" +
            el.title + "</h4><p>" + el.description + "</p></div></div>\n";
    })

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
        $.each(currentTilesIds, function (index, el) {
            hide(el);
        })
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