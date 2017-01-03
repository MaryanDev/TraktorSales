(function($) {



    // jQuery for page scrolling feature - requires jQuery Easing plugin
    $('.page-scroll a').bind('click', function(event) {
        var $anchor = $(this);
        $('html, body').stop().animate({
            scrollTop: ($($anchor.attr('href')).offset().top - 50)
        }, 1250, 'easeInOutExpo');
        event.preventDefault();
    });



    // Highlight the top nav as scrolling occurs
    // $('body').scrollspy({
    //     target: '.navbar-fixed-top',
    //     offset: 51
    // });

    // Closes the Responsive Menu on Menu Item Click
    $('.navbar-collapse ul li a').click(function(){ 
            $('.navbar-toggle:visible').click();
    });

    // Offset for Main Navigation
    $('#mainNav').affix({
        offset: {
            top: 10,
            //bottom: 0
        }
    })

    // Floating label headings for the contact form
    $(function() {
        $("body").on("input propertychange", ".floating-label-form-group", function(e) {
            $(this).toggleClass("floating-label-form-group-with-value", !!$(e.target).val());
        }).on("focus", ".floating-label-form-group", function() {
            $(this).addClass("floating-label-form-group-with-focus");
        }).on("blur", ".floating-label-form-group", function() {
            $(this).removeClass("floating-label-form-group-with-focus");
        });


    });

    //$('.slider-for').slick({
    //    slidesToShow: 1,
    //    slidesToScroll: 1,
    //    arrows: true,
    //    fade: true,
    //    asNavFor: '.slider-nav'
    //});
    //$('.slider-nav').slick({
    //    slidesToShow: 3,
    //    slidesToScroll: 1,
    //    asNavFor: '.slider-for',
    //    dots: false,
    //    centerMode: true,
    //    focusOnSelect: true
    //});





})(jQuery); // End of use strict

$('#list').click(function (event) { event.preventDefault(); $('#products .item').addClass('list-group-item'); });
$('#grid').click(function (event) { event.preventDefault(); $('#products .item').removeClass('list-group-item'); $('#products .item').addClass('grid-group-item'); });

