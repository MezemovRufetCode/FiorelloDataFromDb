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
});
