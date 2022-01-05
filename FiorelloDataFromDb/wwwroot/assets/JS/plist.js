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

  //product Categoriyaya gore uygunlasdirma

  let category = document.querySelectorAll(".category");
  let catContent = document.querySelectorAll(".tab .all");

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

  //quantity
  var valueCount;
  document.querySelector(".minus-btn").setAttribute("disabled", "disabled");
  document.querySelector(".plus-btn").addEventListener("click", function () {
    valueCount = document.getElementById("quantity").value;
    valueCount++;
    document.getElementById("quantity").value = valueCount;
    if (valueCount > 0) {
      document.querySelector(".minus-btn").removeAttribute("disabled");
      document.querySelector(".minus-btn").classList.remove("disabled");
    }
  });
  document.querySelector(".minus-btn").addEventListener("click", function () {
    valueCount = document.getElementById("quantity").value;
    valueCount--;
    document.getElementById("quantity").value = valueCount;
    if (valueCount == 0) {
      document.querySelector(".minus-btn").setAttribute("disabled", "disabled");
    }
  });
  //image deyisme
  let images = Array.from(document.querySelectorAll(".images .little .img"));
  images.forEach((img) => {
    img.onclick = function () {
      var src = img.getAttribute("src");

      document
        .querySelector(".images .mainimage .mimg")
        .setAttribute("src", src);
      document.querySelector(".images .mainimage img").style.objectFit =
        "contain";
      //  .style.objetcFit =
      //     "cover";
    };
  });
});
