(function($){
  "use strict";

  var $window = $(window);

  $window.on('load', function() {
    $window.trigger("resize");
  });

  // Preloader
  $('.loader').fadeOut();
  $('.loader-mask').delay(350).fadeOut('slow');


  // Init
  initOwlCarousel();
  initFlickity();
  

  $window.on('resize', function() {
    hideSidenav();
    megaMenu();
  });


  /* Detect Browser Size
  -------------------------------------------------------*/
  var minWidth;
  if (Modernizr.mq('(min-width: 0px)')) {
    // Browsers that support media queries
    minWidth = function (width) {
      return Modernizr.mq('(min-width: ' + width + 'px)');
    };
  }
  else {
    // Fallback for browsers that does not support media queries
    minWidth = function (width) {
      return $window.width() >= width;
    };
  }


  /* Mobile Detect
  -------------------------------------------------------*/
  if (/Android|iPhone|iPad|iPod|BlackBerry|Windows Phone/i.test(navigator.userAgent || navigator.vendor || window.opera)) {
     $("html").addClass("mobile");
     $('.dropdown-toggle').attr('data-toggle', 'dropdown');
  }
  else {
    $("html").removeClass("mobile");
  }

  /* IE Detect
  -------------------------------------------------------*/
  if(Function('/*@cc_on return document.documentMode===10@*/')()){ $("html").addClass("ie"); }


  /* Sticky Navigation
  -------------------------------------------------------*/
  $window.scroll(function(){

    scrollToTop();
    var $stickyNav = $('.nav--sticky');

    if ($(window).scrollTop() > 190) {
      $stickyNav.addClass('sticky');
    } else {
      $stickyNav.removeClass('sticky');
    }

    if ($(window).scrollTop() > 200) {
      $stickyNav.addClass('offset');
    } else {
      $stickyNav.removeClass('offset');
    }

    if ($(window).scrollTop() > 500) {
      $stickyNav.addClass('scrolling');
    } else {
      $stickyNav.removeClass('scrolling');
    }
    
  });


  /* Mobile Navigation
  -------------------------------------------------------*/
  var $sidenav = $('#sidenav'),
      $mainContainer = $('#main-container'),
      $navIconToggle = $('.nav-icon-toggle'),
      $navHolder = $('.nav__holder'),
      $contentOverlay = $('.content-overlay'),
      $htmlContainer = $('html'),
      $sidenavCloseButton = $('#sidenav__close-button');


  $navIconToggle.on('click', function(e) {
    e.stopPropagation();
    $(this).toggleClass('nav-icon-toggle--is-open');
    $sidenav.toggleClass('sidenav--is-open');   
    $contentOverlay.toggleClass('content-overlay--is-visible');
    // $htmlContainer.toggleClass('oh');
  });

  function resetNav() {
    $navIconToggle.removeClass('nav-icon-toggle--is-open');
    $sidenav.removeClass('sidenav--is-open');
    $contentOverlay.removeClass('content-overlay--is-visible');
    // $htmlContainer.removeClass('oh');
  }

  function hideSidenav() {
    if( minWidth(992) ) {
      resetNav();
      setTimeout( megaMenu, 500 );
    }
  }

  $contentOverlay.on('click', function() {
    resetNav();
  });

  $sidenavCloseButton.on('click', function() {
    resetNav();
  });
  

  var $dropdownTrigger = $('.nav__dropdown-trigger'),
      $navDropdownMenu = $('.nav__dropdown-menu'),
      $navDropdown = $('.nav__dropdown');


  if ( $('html').hasClass('mobile') ) {

    $('body').on('click',function() {
      $navDropdownMenu.addClass('hide-dropdown');
    });

    $navDropdown.on('click', '> a', function(e) {
      e.preventDefault();
    });

    $navDropdown.on('click',function(e) {
      e.stopPropagation();
      $navDropdownMenu.removeClass('hide-dropdown');
    });
  }


  /* Sidenav Menu
  -------------------------------------------------------*/
  $('.sidenav__menu-toggle').on('click', function(e) {
    e.preventDefault();
    
    var $this = $(this);
    
    $this.parent().siblings().removeClass('sidenav__menu--is-open');
    $this.parent().siblings().find('li').removeClass('sidenav__menu--is-open');
    $this.parent().find('li').removeClass('sidenav__menu--is-open');
    $this.parent().toggleClass('sidenav__menu--is-open');       
    
    if ($this.next().hasClass('show')) {
      $this.next().removeClass('show').slideUp(350);    
    } else {
      $this.parent().parent().find('li .sidenav__menu-dropdown').removeClass('show').slideUp(350);
      $this.next().toggleClass('show').slideToggle(350);
    }
  });


  /* Nav Seacrh
  -------------------------------------------------------*/
  (function() {
    var navSearchTrigger = $('.nav__search-trigger'),
        navSearchTriggerIcon = navSearchTrigger.find('i'),
        navSearchBox = $('.nav__search-box'), 
        navSearchInput = $('.nav__search-input'); 

    navSearchTrigger.on('click', function(e){
      e.preventDefault();
      navSearchTriggerIcon.toggleClass('ui-close');
      navSearchBox.slideToggle();
      // navSearchInput.focus();
    });
  })();


  /* Mega Menu
  -------------------------------------------------------*/
  function megaMenu(){
    $('.nav__megamenu').each(function () {
      var $this = $(this);

      $this.css('width', $('.container').width());
      var offset = $this.closest('.nav__dropdown').offset();
      offset = offset.left;
      var containerOffset = $(window).width() - $('.container').outerWidth();
      containerOffset = containerOffset /2;
      offset = offset - containerOffset - 15;
      $this.css('left', -offset);
    });
  }
  

  /* Twitter Feed
  -------------------------------------------------------*/
  if($('#tweets').length) {
    function initTwitter() {
      var config1 = {
        "id": '594366594521804800',
        "domId": 'tweets',
        "showUser": false,
        "showInteraction": false,
        "showPermalinks": false,
        "showTime": true,
        "maxTweets": 2,
        "enableLinks": true
      };
      twitterFetcher.fetch(config1);
    }
    initTwitter();
  }


  /* YouTube Video Playlist
  -------------------------------------------------------*/
  (function(){ 
    var videoPlaylistListItem = $('.video-playlist__list-item'),
        videoPlaylistContentVideo = $('.video-playlist__content-video');     

    videoPlaylistListItem.on('click', function(e){
      e.preventDefault();
      var $this = $(this);
      var thumbVideoUrl = $this.attr('href');

      videoPlaylistContentVideo.attr('src', thumbVideoUrl);

      $this.siblings().removeClass('video-playlist__list-item--active');
      $this.addClass('video-playlist__list-item--active');

    });

  })();



  /* News Ticker
  -------------------------------------------------------*/
  var $newsTicker = $('.newsticker__list');

  if($newsTicker.length) {
    $newsTicker.newsTicker({
      row_height: 34,
      max_rows: 1,
      prevButton: $('#newsticker-button--prev'),
      nextButton: $('#newsticker-button--next')
    });
  }  
  

  /* Tabs
  -------------------------------------------------------*/
  $('.tabs__trigger').on('click', function(e) {
    var currentAttrValue = $(this).attr('href');
    $('.tabs__content-trigger ' + currentAttrValue).stop().fadeIn(1000).siblings().hide();
    $(this).parent('li').addClass('tabs__item--active').siblings().removeClass('tabs__item--active');
    e.preventDefault();
  });
  

  /* Owl Carousel
  -------------------------------------------------------*/
  function initOwlCarousel(){

    // Hero Slider
    $("#owl-hero-slider").owlCarousel({
      rtl: true,
      center: true,
      items: 1,
      loop: true,
      nav: true,
      dots: false,
      margin: 8,
      lazyLoad: true,
      navSpeed: 500,
      navText: ['<i class="ui-arrow-left">','<i class="ui-arrow-right">'],
      responsive:{
        1200: {
          items:4
        },
        768:{
          items:2
        },
        540:{
          items:2
        }
      }
    });

    // Posts Carousel
    $("#owl-posts").owlCarousel({
      rtl: true,
      center: false,
      items: 1,
      loop: true,
      nav: true,
      dots: false,
      margin: 30,
      lazyLoad: true,
      navSpeed: 500,
      navText: ['<i class="ui-arrow-right">','<i class="ui-arrow-left">'],
      responsive:{
        768:{
          items:4
        },
        540:{
          items:3
        }
      }
    });

    // Related Posts
    $("#owl-posts-3-items").owlCarousel({
      rtl: true,
      center: false,
      items: 1,
      loop: true,
      nav: true,
      dots: false,
      margin: 20,
      lazyLoad: true,
      navSpeed: 500,
      navText: ['<i class="ui-arrow-right">','<i class="ui-arrow-left">'],
      responsive:{
        768:{
          items:3
        },
        540:{
          items:2
        }
      }
    });

    // Headlines
    $("#owl-headlines").owlCarousel({
      rtl: true,
      items: 1,
      loop: true,
      nav: false,
      dots: false,
      lazyLoad: true,
      navSpeed: 500,
      navText: ['<i class="ui-arrow-left">','<i class="ui-arrow-right">']
    });

    // Single Image
    $("#owl-single").owlCarousel({
      rtl: true,
      items: 1,
      loop: true,
      nav: true,
      dots: false,
      lazyLoad: true,
      navSpeed: 500,
      navText: ['<i class="ui-arrow-left">','<i class="ui-arrow-right">']
    });

    // Single Post Gallery
    $("#owl-single-post-gallery").owlCarousel({
      rtl: true,
      items: 1,
      loop: true,
      nav: true,
      dots: true,
      lazyLoad: true,
      navSpeed: 500,
      navText: ['<i class="ui-arrow-left">','<i class="ui-arrow-right">']
    });

    // Custom nav
    var owlCustomNav = $('#owl-headlines').owlCarousel();

    $(".owl-custom-nav__btn--prev").on('click', function () {
        owlCustomNav.trigger('prev.owl.carousel');
    });

    $(".owl-custom-nav__btn--next").on('click', function () {
        owlCustomNav.trigger('next.owl.carousel');
    });
  };


  /* Flickity Slider
  -------------------------------------------------------*/
  function initFlickity() {

    // 1st carousel, main
    $('#flickity-hero').flickity({
      cellAlign: 'left',
      contain: true,
      pageDots: false,
      prevNextButtons: false,
      draggable: false
    });

    // 2nd carousel, navigation
    $('#flickity-thumbs').flickity({
      cellAlign: 'left',
      asNavFor: '#flickity-hero',
      contain: true,
      pageDots: false,
      prevNextButtons: false,
      draggable: false
    });
  }


  /* Sticky Socials
  -------------------------------------------------------*/
  (function() {
    var $stickyCol = $('.sticky-col');
    if($stickyCol.length > 0) {
      $stickyCol.stick_in_parent({
        offset_top: 80
      });
    }
  })();


  /* ---------------------------------------------------------------------- */
  /*  Contact Form
  /* ---------------------------------------------------------------------- */

  var submitContact = $('#submit-message'),
    message = $('#msg');

  submitContact.on('click', function(e){
    e.preventDefault();

    var $this = $(this);
    
    $.ajax({
      type: "POST",
      url: 'contact.php',
      dataType: 'json',
      cache: false,
      data: $('#contact-form').serialize(),
      success: function(data) {

        if(data.info !== 'error'){
          $this.parents('form').find('input[type=text],input[type=email],textarea,select').filter(':visible').val('');
          message.hide().removeClass('success').removeClass('error').addClass('success').html(data.msg).fadeIn('slow').delay(5000).fadeOut('slow');
        } else {
          message.hide().removeClass('success').removeClass('error').addClass('error').html(data.msg).fadeIn('slow').delay(5000).fadeOut('slow');
        }
      }
    });
  });


  /* Scroll to Top
  -------------------------------------------------------*/
  function scrollToTop() {
    var scroll = $window.scrollTop();
    var $backToTop = $("#back-to-top");
    if (scroll >= 50) {
      $backToTop.addClass("show");
    } else {
      $backToTop.removeClass("show");
    }
  }

  $('a[href="#top"]').on('click',function(){
    $('html, body').animate({scrollTop: 0}, 1000, "easeInOutQuint");
    return false;
  });

})(jQuery);