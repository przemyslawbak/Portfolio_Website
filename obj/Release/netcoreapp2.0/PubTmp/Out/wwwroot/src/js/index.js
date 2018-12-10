import React, { Component } from 'react';
import ReactDOM from 'react-dom';
import $ from 'jquery';
import uuid from 'uuid';
import App from '../js/components/App';
import '../../dist/site.css';

//JQUERY
//<----------JQ----------->
//navi bar
$(document).ready(function () {
    // grab the initial top offset of the navigation 
    var stickyNavTop = $('.mainNavbar').offset().top;
    // our function that decides weather the navigation bar should have "fixed" css position or not.
    var stickyNav = function () {
        var scrollTop = $(window).scrollTop(); // our current vertical position from the top
        // if we've scrolled more than the navigation, change its position to fixed to stick to top,
        // otherwise change it back to relative
        if (window.pageYOffset >= stickyNavTop) {
            $('.stickyNavbar').addClass('sticky');
            $('.mainNavbar').addClass('hidden');
        } else {
            $('.stickyNavbar').removeClass('sticky');
            $('.mainNavbar').removeClass('hidden');
        }
    };
    stickyNav();
    // and run it again every time you scroll
    $(window).scroll(function () {
        stickyNav();
    });

    //<----------JQ----------->
    //nav bar transition buttons
    $(".projects, .techologies, .about, .contact, .socialIcon").hover(function () {
        $(this).toggleClass("hoverBtn");
    });

    //pic zoom in for mouse on

    $(".innerContainer").mouseenter(function () {
        $(this).find(".projectPictures img").animate({ 'height': '+=5', 'width': '+=5' }, 'fast')
    });
    $(".innerContainer").mouseleave(function () {
        $(this).find(".projectPictures img").animate({ 'height': "-=5", 'width': '-=5' }, 'fast')
    });

    //<----------JQ----------->
    //header reveal spotlight
    // Global variables
    var spotlightDiameter = 400;
    // Verify that the mouse event wasn't triggered by a descendant.
    function verifyMouseEvent(e, elem) {
        e = e || window.event;
        var related = e.relatedTarget || e.toElement;

        while ((related != undefined) &&
            (related != elem) &&
            (related.nodeName != 'BODY')) {
            related = related.parentNode;
        }
        return (related != elem);
    }
    // Create the spotlight
    function createSpotlight() {
        $('.spotlight').width(spotlightDiameter + 'px')
            .height(spotlightDiameter + 'px')
            .css({ boxShadow: '0px 0px 100px 160px rgba(85, 125, 168, 1.0) inset' });
    }
    // Mousemove
    $('.mainHeader').on('mousemove', function (e) {
        var center = {
            x: e.pageX - this.offsetLeft,
            y: e.pageY - this.offsetTop
        };
        var x = center.x - (spotlightDiameter >> 1);
        var y = center.y - (spotlightDiameter >> 1);

        $('.spotlight').css({
            left: x + 'px', top: y + 'px',
            backgroundPosition: -x + 'px ' + -y + 'px'
        }).show();
    });
    // Mouseover
    $('.mainHeader').on('mouseout', function (e) {
        if (!verifyMouseEvent(e, this)) return;
        $('.spotlight').hide();
    });
    $(document).ready(function () {
        createSpotlight();
    });

});

//REACT

//<----------REACT----------->

ReactDOM.render(
    <App/>,
    document.getElementById('dialogsReact'));