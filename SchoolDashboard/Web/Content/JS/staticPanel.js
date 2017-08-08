var circlesObjects = [];
var circlesColors = [
    ["rgb(50,205,50)", "rgba(111, 220, 111, %1$s)"],
    ["rgb(32,178,170)", "rgba(36, 204, 195, %1$s)"],
    ["rgb(0,0,205)", "rgba(66, 66, 255, %1$s)"]
];

function processCircles() {
    $(".lesson-circle").each(function () {
        let schoolLevel = $(this).data("school-level");
        $.get("/GetLessonsInfo?SchoolLevel=" + schoolLevel, function (data) {
            let circle = circlesObjects[schoolLevel];
            if (data.lessonsEnded) {
                circle.isLessonsEnded = true;
                circle.isBreak = false;
                return;
            }

            circle.isLessonsEnded = false;
            circle.description = data.description;
            circle.timeRemainingText = data.minutesRemaining + " хв";
            circle.progressCirclePercent = data.percantage;

            if (data.isLesson) {
                circle.isBreak = false;
            }
            else {
                circle.isBreak = true;
            }
        });
    });
}

function initCircles() {
    $.get("/GetSchoolLevels",
        function (data) {
            $.each(data,
                function (index, el) {
                    $("#staticPanel").append("<div class='circle-wrapper'><div class='circle-title'>" +
                        el.description + "</div><canvas width='200' height='200' class='lesson-circle' data-school-level='" +
                        el.id + "' id='lessonCircle" + el.id + "'></canvas></div>");
                    let circle = new LessonCircle("lessonCircle" + el.id);
                    let colors = circlesColors.pop();
                    circle.lessonColor = colors[0];
                    circle.endPointColor = colors[1];
                    circlesObjects[el.id] = circle;
                });

            processCircles();
            setInterval(processCircles, 30000);
        });
}

function drawFrames() {
    $.each(circlesObjects,
        function (index, el) {
            if (el == null)
                return;
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