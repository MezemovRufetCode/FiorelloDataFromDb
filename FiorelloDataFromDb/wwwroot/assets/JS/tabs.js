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

  // Search Area
  $("header .littleservices .search span").click(function () {
    $("header .searchArea").slideToggle();
  });
  //Categoriyaya gore uygunlasdirma

  let category = document.querySelectorAll(".tab1 .category");
  let catContent = document.querySelectorAll(".tab1 .content .all");

  for (let i = 0; i < category.length; i++) {
    category[i].addEventListener(
      "click",
      filterContents.bind(this, category[i])
    );
  }

  function filterContents(item) {
    changeActive(item);
    for (let i = 0; i < catContent.length; i++) {
      if (catContent[i].classList.contains(item.attributes.id.value)) {
        catContent[i].style.display = "block";
      } else catContent[i].style.display = "none";
    }
  }
  function changeActive(activeitem) {
    for (let i = 0; i < category.length; i++) {
      category[i].classList.remove("active2");
    }
    activeitem.classList.add("active2");
  }

  //-----------------------------------------
  let category1 = document.querySelectorAll(".tab2 .category");
  let catContent1 = document.querySelectorAll(".tab2 .content .all");

  for (let i = 0; i < category1.length; i++) {
    category1[i].addEventListener(
      "click",
      filterContents1.bind(this, category1[i])
    );
  }

  function filterContents1(item) {
    changeActive1(item);
    for (let i = 0; i < catContent1.length; i++) {
      if (catContent1[i].classList.contains(item.attributes.id.value)) {
        catContent1[i].style.display = "block";
      } else catContent1[i].style.display = "none";
    }
  }
  function changeActive1(activeitem1) {
    for (let i = 0; i < category.length; i++) {
      category1[i].classList.remove("active2");
    }
    activeitem1.classList.add("active2");
  }

  //----------------------------------------
  let category2 = document.querySelectorAll(".tab3 .category");
  let catContent2 = document.querySelectorAll(".tab3 .content .all");

  for (let i = 0; i < category2.length; i++) {
    category2[i].addEventListener(
      "click",
      filterContents2.bind(this, category2[i])
    );
  }

  function filterContents2(item) {
    changeActive2(item);
    for (let i = 0; i < catContent2.length; i++) {
      if (catContent2[i].classList.contains(item.attributes.id.value)) {
        catContent2[i].style.display = "block";
      } else catContent2[i].style.display = "none";
    }
  }
  function changeActive2(activeitem2) {
    for (let i = 0; i < category.length; i++) {
      category2[i].classList.remove("active2");
    }
    activeitem2.classList.add("active2");
  }
  //----------------------------------------
  let category3 = document.querySelectorAll(".tab4 .category");
  let catContent3 = document.querySelectorAll(".tab4 .content .all");

  for (let i = 0; i < category3.length; i++) {
    category3[i].addEventListener(
      "click",
      filterContents3.bind(this, category3[i])
    );
  }

  function filterContents3(item) {
    changeActive3(item);
    for (let i = 0; i < catContent3.length; i++) {
      if (catContent3[i].classList.contains(item.attributes.id.value)) {
        catContent3[i].style.display = "block";
      } else catContent3[i].style.display = "none";
    }
  }
  function changeActive3(activeitem3) {
    for (let i = 0; i < category.length; i++) {
      category3[i].classList.remove("active2");
    }
    activeitem3.classList.add("active2");
  }
});
