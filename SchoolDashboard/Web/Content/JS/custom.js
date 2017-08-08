
// Circle countodws

$(".first-circle-countdown").TimeCircles({
  "animation": "smooth",
  "bg_width": 0.04,
  "fg_width": 0.08,
  "circle_bg_color": "#9E9E9E",
  "time": {
    "Days": {
      "show": false
    },
    "Hours": {
      "show": false
    },
    "Minutes": {
      "text": "Урок через",
      "color": "#4CAF50",
      "show": true
    },
    "Seconds": {
      "show": false
    }
  }
});

$(".second-circle-countdown").TimeCircles({
  "animation": "smooth",
  "bg_width": 0.04,
  "fg_width": 0.08,
  "circle_bg_color": "#9E9E9E",
  "time": {
    "Days": {
      "show": false
    },
    "Hours": {
      "show": false
    },
    "Minutes": {
      "text": "Урок через",
      "color": "#03A9F4",
      "show": true
    },
    "Seconds": {
      "show": false
    }
  }
});

$(".third-circle-countdown").TimeCircles({
  "animation": "smooth",
  "bg_width": 0.04,
  "fg_width": 0.08,
  "circle_bg_color": "#9E9E9E",
  "time": {
    "Days": {
      "show": false
    },
    "Hours": {
      "show": false
    },
    "Minutes": {
      "text": "Урок через",
      "color": "#FFC107",
      "show": true
    },
    "Seconds": {
      "show": false
    }
  }
});


// Clock

$(document).ready(function() {
  clockUpdate();
  setInterval(clockUpdate, 30000);
})

function clockUpdate() {
  var date = new Date();
  $('.js-digital-clock');
  function addZero(x) {
    if (x < 10) {
      return x = '0' + x;
    } else {
      return x;
    }
  }

  var months = [
    'Січня', 'Лютого', 'Березня', 'Квітня', 'Травня', 'Червня', 'Липня', 'Серпня', 'Веерсня', 'Жовтня', 'Листопада', 'Грудня'
  ];

  var days = [
    'Неділя', 'Понеділок', 'Вівторок', 'Середа', 'Четвер', 'П\'ятниця', 'Суббота'
  ];

  var h = addZero(date.getHours());
  var m = addZero(date.getMinutes());
  var s = addZero(date.getSeconds());

  var dayOfWeek = days[date.getDay()];
  var month = months[date.getMonth()];
  var day = date.getDate();

  //var isSecondOdd = s % 2;
  $('.js-digital-clock').text(h + ':' + m);
  $('.js-date').html(day + ' ' + month + '<br/>' + dayOfWeek);
}
