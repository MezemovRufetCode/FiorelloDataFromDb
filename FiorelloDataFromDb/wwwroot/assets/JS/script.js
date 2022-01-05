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

  //product Categoriyaya gore uygunlasdirma
  let allheadcategory = document.querySelectorAll(".category-title");
  let allposts = document.querySelectorAll(".posts-collect .all");

  for (let i = 0; i < allheadcategory.length; i++) {
    allheadcategory[i].addEventListener(
      "click",
      filterPosts.bind(this, allheadcategory[i])
    );
  }

  function filterPosts(item) {
    changeActive(item);
    for (let i = 0; i < allposts.length; i++) {
      if (allposts[i].classList.contains(item.attributes.id.value)) {
        allposts[i].style.display = "block";
      } else allposts[i].style.display = "none";
    }
  }
  function changeActive(activeitem) {
    for (let i = 0; i < allheadcategory.length; i++) {
      allheadcategory[i].classList.remove("active");
    }
    activeitem.classList.add("active");
  }
  //owl Carousel blogpost
  $("#reviews .owl-carousel").owlCarousel({
    loop: true,
    margin: 10,
    dots: false,
    nav: true,
    autoplay: true,
    autoplaySpeed: 1000,
    smartSpeed: 1500,

    responsive: {
      0: {
        items: 1,
        nav: false,
      },
      1000: {
        items: 1,
        nav: true,
      },
    },
  });
  //parallax
  // parallax;
  // $(".parallax-window").parallax({
  //   imageSrc: "./assets/images/h3-background-img.jpg",
  // });
  // Search Area
  $("header .littleservices .search span").click(function () {
    $("header .searchArea").slideToggle();
  });

  //filterarea
  $(".products .filter").click(function () {
    $(".products .filterArea").slideToggle();
  });
});
