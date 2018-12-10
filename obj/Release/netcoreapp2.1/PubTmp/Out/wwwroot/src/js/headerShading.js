//1
var $div1 = $('.container1').clone().html('');
$('.container1').contents().each(function () {
    var spanClass = '';

    if ($(this).is('span')) {
        spanClass = $(this).attr('class');
    }

    $textArray = $(this).text().split('');

    for (var i = 0; i < $textArray.length; i++) {
        $('<span>').addClass(spanClass).text($textArray[i]).appendTo($div1);
    }
});
$('.container1').replaceWith($div1);
//2
var $div2 = $('.container2').clone().html('');
$('.container2').contents().each(function () {
    var spanClass = '';

    if ($(this).is('span')) {
        spanClass = $(this).attr('class');
    }

    $textArray = $(this).text().split('');

    for (var i = 0; i < $textArray.length; i++) {
        $('<span>').addClass(spanClass).text($textArray[i]).appendTo($div2);
    }
});
$('.container2').replaceWith($div2);

$('.removeClass').hover(
    function () { $(this).addClass('shadeMe'); }
);
//shadow
$('.shadeMe').realshadow({
    type: 'text',
    followMouse: true
}); // options are optional