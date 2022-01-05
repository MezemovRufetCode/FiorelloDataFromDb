$(document).ready(function () {
  // open close menu side
  $(".menuIcon i").click(function () {
    $(".menu").addClass("active");
  });
  $(".close i").click(function () {
    $(".menu").removeClass("active");
  });

  //dropdown slide
  $(function () {
    $(".nav-links li").hover(
      function () {
        $(">ul.sub:not(:animated)", this).slideDown(350);
      },
      function () {
        $(">ul.sub", this).slideUp(70);
      }
    );
  });

  //dropdown icon rotate
  $(".menu-links .sub").hide();
  $(".mlist").click(function () {
    $(this).find(".rightIcn").toggleClass("ndeg udeg");
    $(this).next(".sub").slideToggle(200);
  });
  // Search Area
  $("header .littleservices .search span").click(function () {
    $("header .searchArea").slideToggle();
  });
  //To top
  $(".goUp").hide();
  $(window).scroll(function () {
    if ($(this).scrollTop() > 500) {
      $(".goUp").show(200);
    } else {
      $(".goUp").hide(100);
    }
  });
  $(".goUp").click(function () {
    $("html, body").animate({ scrollTop: 0 }, 50);
    return false;
  });

  //accordion

  $(".accordion .first .allrowFirst .heading").on("click", function () {
    if ($(this).hasClass("active")) {
      $(this).removeClass("active");
      $(this).siblings(".allrowFirst .content").slideUp(200);
      $(".first .allrowFirst .heading i")
        .removeClass("fa-minus")
        .addClass("fa-plus");
    } else {
      $(".first .allrowFirst .heading i")
        .removeClass("fa-minus")
        .addClass("fa-plus");
      $(this).find("i").removeClass("fa-plus").addClass("fa-minus");
      $(".first .allrowFirst .heading").removeClass("active");
      $(this).addClass("active");
      $(".first .allrowFirst .content").slideUp(200);
      $(this).siblings(".allrowFirst .content").slideDown(200);
    }
  });

  // let allHeading = document.querySelectorAll(".accordion .first .heading");
  // allHeading.forEach((head) => {
  //   if (!head.classList.contains("active")) {
  //     head.onmouseenter = function () {
  //       this.querySelector("span").innerHTML = "<i class='fas fa-minus'></i>";
  //     };
  //     head.onmouseleave = function () {
  //       this.querySelector("span").innerHTML = "<i class='fas fa-plus'></i>";
  //     };
  //   }
  // });

  $(".accordion .second .allrowSecond .heading").on("click", function () {
    if ($(this).hasClass("active")) {
      $(this).removeClass("active");
      $(this).siblings(".allrowSecond .content").slideUp(200);
      $(".second .allrowSecond .heading i")
        .removeClass("fa-minus")
        .addClass("fa-plus");
    } else {
      $(".second .allrowSecond .heading i")
        .removeClass("fa-minus")
        .addClass("fa-plus");
      $(this).find("i").removeClass("fa-plus").addClass("fa-minus");
      $(".second .allrowSecond .heading").removeClass("active");
      $(this).addClass("active");
      $(".second .allrowSecond .content").slideUp(200);
      $(this).siblings(".allrowSecond .content").slideDown(200);
    }
  });

  $(".accordionSec .first .allrowThird .heading").on("click", function () {
    if ($(this).hasClass("active")) {
      $(this).removeClass("active");
      $(this).siblings(".allrowThird .content").slideUp(200);
      $(".first .allrowThird .heading i")
        .removeClass("fa-minus")
        .addClass("fa-plus");
    } else {
      $(".first .allrowThird .heading i")
        .removeClass("fa-minus")
        .addClass("fa-plus");
      $(this).find("i").removeClass("fa-plus").addClass("fa-minus");
      $(".first .allrowThird .heading").removeClass("active");
      $(this).addClass("active");
      $(".first .allrowThird .content").slideUp(200);
      $(this).siblings(".allrowThird .content").slideDown(200);
    }
  });

  $(".accordionSec .second .allrowFourth .heading").on("click", function () {
    if ($(this).hasClass("active")) {
      $(this).removeClass("active");
      $(this).siblings(".allrowFourth .content").slideUp(200);
      $(".second .allrowFourth .heading i")
        .removeClass("fa-minus")
        .addClass("fa-plus");
    } else {
      $(".second .allrowFourth .heading i")
        .removeClass("fa-minus")
        .addClass("fa-plus");
      $(this).find("i").removeClass("fa-plus").addClass("fa-minus");
      $(".second .allrowFourth .heading").removeClass("active");
      $(this).addClass("active");
      $(".second .allrowFourth .content").slideUp(200);
      $(this).siblings(".allrowFourth .content").slideDown(200);
    }
  });
});
