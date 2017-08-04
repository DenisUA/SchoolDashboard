window.requestAnimFrame = (function (callback) {
    return window.requestAnimationFrame || window.webkitRequestAnimationFrame || window.mozRequestAnimationFrame || window.oRequestAnimationFrame || window.msRequestAnimationFrame ||
    function (callback) {
        window.setTimeout(callback, 1000 / 60);
    };
})();

class LessonCircle {
    constructor(canvasId) {
        let canvas = document.getElementById(canvasId);
        this.ctx = canvas.getContext("2d");

        this.width = canvas.width;
        this.height = canvas.height;

        this.maxPulsarRadius = 15;
        this.pulsar1Radius = 0;
        this.pulsar2Radius = 5;
        this.progressCirclePercent = 0;
        this.lineWidth = 15;
        this.radian = 0;
        this.defaultCirclesRadius = this.width / 2 - this.lineWidth;
        this.endPointColor = "";
        this.isLessonsEnded = false;
        this.description = "";
        this.timeRemainingText = "";
        this.isBreak = false;
        this.lessonColor = "";
        this.breakColor = "rgb(255,140,0)";
        this.breakEndpointColor = "rgba(255, 177, 82, %1$s)"
    }

    draw() {
        this.ctx.clearRect(0, 0, this.width, this.height);

        var angle = this.radian * (180 / Math.PI);
        var x0 = this.width / 2;
        var y0 = this.height / 2;
        var radius = this.defaultCirclesRadius;

        //main circle
        this.ctx.beginPath();
        this.ctx.arc(x0, y0, radius, 0, 2 * Math.PI, false);
        this.ctx.lineWidth = this.lineWidth;
        this.ctx.strokeStyle = "rgb(176, 176, 176)";
        this.ctx.stroke();


        if (!this.isLessonsEnded) {
            //progress circle
            this.ctx.beginPath();
            this.ctx.arc(x0, y0, radius, 1.5 * Math.PI, this.radian + 1.5 * Math.PI, false);
            this.ctx.lineWidth = this.lineWidth;
            this.ctx.strokeStyle = this.isBreak ? this.breakColor : this.lessonColor;
            this.ctx.lineCap = 'round';
            this.ctx.stroke();

            //end point
            var rx = 0;
            var ry = radius * -1;
            var c = Math.cos(this.radian);
            var s = Math.sin(this.radian);
            var pointX = x0 + rx * c - ry * s;
            var pointY = y0 + rx * s + ry * c;

            this.ctx.beginPath();
            this.ctx.arc(pointX, pointY, this.lineWidth / 2, 0, 2 * Math.PI, false);
            this.ctx.fillStyle = this.isBreak ? sprintf(this.breakEndpointColor, 1) : sprintf(this.endPointColor, 1);
            this.ctx.fill();

            //pulsar 1
            this.ctx.beginPath();
            this.ctx.arc(pointX, pointY, this.pulsar1Radius, 0, 2 * Math.PI);
            this.ctx.lineWidth = 3;
            var t = 1 - this.pulsar1Radius / this.maxPulsarRadius;
            this.ctx.strokeStyle = this.isBreak ? sprintf(this.breakEndpointColor, t) : sprintf(this.endPointColor, t);
            this.ctx.stroke();

            //pulsar 2
            this.ctx.beginPath();
            this.ctx.arc(pointX, pointY, this.pulsar2Radius, 0, 2 * Math.PI);
            this.ctx.lineWidth = 3;
            var t1 = 1 - this.pulsar2Radius / this.maxPulsarRadius;
            this.ctx.strokeStyle = this.isBreak ? sprintf(this.breakEndpointColor, t1) : sprintf(this.endPointColor, t1);
            this.ctx.stroke();
        }

        //texts
        var textX;
        var textY;

        if (this.isLessonsEnded) {
            textX = this.width / 2;
            textY = this.height / 2;

            this.ctx.fillStyle = "rgba(0, 0, 0, 1)";
            this.ctx.font = "25pt Arial";
            this.ctx.textBaseline = "bottom";
            this.ctx.textAlign = "center";
            this.ctx.fillText("Уроки", textX, textY);

            this.ctx.fillStyle = "rgba(0, 0, 0, 1)";
            this.ctx.font = "20pt Arial";
            this.ctx.textBaseline = "top";
            this.ctx.textAlign = "center";
            this.ctx.fillText("завершено", textX, textY);
        } else {
            textX = this.width / 2;
            textY = this.height / 2 + 5;
            this.ctx.fillStyle = "rgba(0, 0, 0, 1)";
            this.ctx.font = "15pt Arial";
            this.ctx.textBaseline = "bottom";
            this.ctx.textAlign = "center";
            this.ctx.fillText(this.description, textX, textY - 23);

            this.ctx.fillStyle = "rgba(0, 0, 0, 1)";
            this.ctx.font = "33pt Arial";
            this.ctx.textBaseline = "middle";
            this.ctx.textAlign = "center";
            this.ctx.fillText(this.timeRemainingText, textX, textY);

            this.ctx.fillStyle = "rgba(0, 0, 0, 0.5)";
            this.ctx.font = "11pt Arial";
            this.ctx.textBaseline = "middle";
            this.ctx.textAlign = "top";
            this.ctx.fillText("до завершення", textX, textY + 27);
        }
    }

    update() {

        if (!this.isLessonsEnded) {
            if (this.pulsar1Radius > this.maxPulsarRadius) {
                this.pulsar1Radius = 0;
            }
            else {
                this.pulsar1Radius += 0.2;
            }

            if (this.pulsar2Radius > this.maxPulsarRadius) {
                this.pulsar2Radius = 0;
            }
            else {
                this.pulsar2Radius += 0.2;
            }
        }


        this.radian = 2 * Math.PI * this.progressCirclePercent / 100;
    }


    drawFrame() {
        this.update();
        this.draw();
    }
}

