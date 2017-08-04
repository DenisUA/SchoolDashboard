var circlesObjects = [];

function processCircles() {
    $(".lesson-circle").each(function() {
        let schoolLevel = this.data("school-level");
    });
}

function initCircles() {
    $.get("/GetSchoolLevels",
        function (data) {
            $.each(data,
                function(index, el) {
                    $("#staticPanel").append("<div class='circle-wrapper'><div class=''>" +
                        el.description + "</div><canvas width='200' height='200' class='lesson-circle' data-school-level='" +
                        el.id + "' id='lessonCircle" + el.id + "'></canvas></div>");
                    let c = new LessonCircle("lessonCircle" + el.id);
                    c.isLessonsEnded = true;
                    circlesObjects.push(c);
                });
        });

    setInterval(processCircles, 30000);
}

function drawFrames() {
    $.each(circlesObjects,
        function(index, el) {
            el.drawFrame();
        });
    window.requestAnimFrame(function () {
        drawFrames();
    });
}

$(document).ready(function () {
    initCircles();
    drawFrames();
})