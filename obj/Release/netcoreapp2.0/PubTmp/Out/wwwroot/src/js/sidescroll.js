//<----------JQ----------->
//scroll distance from bottom of the project index
$(window).scroll(function () {
    if ($(window).scrollTop() + $(window).height() > $(document).height() - 300) {
        if ($('.sidenav').hasClass('visible') == false) {
            $('.sidenav').stop().animate({
                right: '0px'
            }, function () {
                $('.sidenav').addClass('visible');
            });
        }
    }
    if ($(window).scrollTop() + $(window).height() < $(document).height() - 600) {
        if ($('.sidenav').hasClass('visible') == true) {
            $('.sidenav').stop().animate({
                right: '-350px'
            }, function () {
                $('.sidenav').removeClass('visible');
            });
        }
    }
});



//scroll distance from top of the aboutme index
$(window).scroll(function () {
    if (($(this).scrollTop() >= 150) || ($(window).scrollTop() + $(window).height() < $(document).height() - 10)) {
        $('.aboutDialogReactContainer').css({ opacity: 0.0, visibility: "visible" }).animate({ opacity: 1.0 });
    } 
});

