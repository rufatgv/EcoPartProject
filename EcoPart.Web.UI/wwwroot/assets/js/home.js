//header
$(document).ready(function(){
    // language and currency dropdown
    $(".language").click(function(){      
        $(".dropdown-language").toggle(); 
    });

    $(".currency").click(function(){      
        $(".dropdown-currency").toggle(); 
    });
    // language and currency dropdown

    //account-dropwdown
    $(".account").click(function(){
        $(".dropdown-account").toggle();    
    })
    //account-dropwdown

    //Notification dropdown
    $(".notific").click(function(){
        $(".dropdown-noti").toggle();   
        if($(".dropdown-noti").css("display") == "block"){
            $(".not-count").css("display","none"); 
           } 
        else if($(".dropdown-noti").css("display") =="none"){
            $(".not-count").css("display","block"); 
           }
    })
    //Notification dropdown

    //waiting-basket
    $(".waiting").click(function(){
        $(".dropdown-waiting").toggle();    
    })
    //waiting-basket

    //add-basket
    $(".basket-card").click(function(){
        $(".dropdown-basket").toggle();    
    })
     //add-basket
});
//header

//body
$(document).ready(function(){
    //brand
    $(".marka").click(function(){      
        $(".dropdown-checkbox-search").toggle(); 
    });
    $("#close-btn").click(function(){   
        $(".dropdown-checkbox-search").hide(); 
    });
    //brand

    //model
    $(".model").click(function(){      
        $(".dropdown-model-logo").toggle(); 
    });
    $("#close-btn-logo").click(function(){   
        $(".dropdown-model-logo").hide(); 
    });
    //model

    //slider
    let slideIndex = 0;
    showSlides();

function showSlides() {
  let i;
  let slides = document.getElementsByClassName("mySlides");
  for (i = 0; i < slides.length; i++) {
    slides[i].style.display = "none";
  }
  slideIndex++;
  if (slideIndex > slides.length) {slideIndex = 1}
  slides[slideIndex-1].style.display = "block";
  setTimeout(showSlides, 2000); // 
}
     //slider
});
//body

var btn = document.querySelector('.mouse-cursor-gradient-tracking');

// getting the offset of the page and binding the data on the css variable --x & --y
btn.onmousemove = function (e) {
    var x = e.pageX - btn.offsetLeft
    var y = e.pageY - btn.offsetTop
    btn.style.setProperty('--x', x + 'px')
    btn.style.setProperty('--y', y + 'px')
}


//media query
$(document).ready(function () {
    $(".oneh").click(function () {
        $(".one").fadeToggle("slow");
    })
    $(".twoh").click(function () {
        $(".two").fadeToggle("slow");
    })
    $(".threeh").click(function () {
        $(".three").fadeToggle("slow");
    })
    $(".fourh").click(function () {
        $(".four").fadeToggle("slow");
    })

})


//media query

const cursor = document.querySelector("#cursor");
const cursorBorder = document.querySelector("#cursor-border");
const cursorPos = { x: 0, y: 0 };
const cursorBorderPos = { x: 0, y: 0 };

document.addEventListener("mousemove", (e) => {
    cursorPos.x = e.clientX;
    cursorPos.y = e.clientY;

    cursor.style.transform = `translate(${e.clientX}px, ${e.clientY}px)`;
});

requestAnimationFrame(function loop() {
    const easting = 8;
    cursorBorderPos.x += (cursorPos.x - cursorBorderPos.x) / easting;
    cursorBorderPos.y += (cursorPos.y - cursorBorderPos.y) / easting;

    cursorBorder.style.transform = `translate(${cursorBorderPos.x}px, ${cursorBorderPos.y}px)`;
    requestAnimationFrame(loop);
});

document.querySelectorAll("[data-cursor]").forEach((item) => {
    item.addEventListener("mouseover", (e) => {
        if (item.dataset.cursor === "pointer") {
            cursorBorder.style.backgroundColor = "rgba(255, 255, 255, .6)";
            cursorBorder.style.setProperty("--size", "30px");
        }
        if (item.dataset.cursor === "pointer2") {
            cursorBorder.style.backgroundColor = "white";
            cursorBorder.style.mixBlendMode = "difference";
            cursorBorder.style.setProperty("--size", "80px");
        }
    });
    item.addEventListener("mouseout", (e) => {
        cursorBorder.style.backgroundColor = "unset";
        cursorBorder.style.mixBlendMode = "unset";
        cursorBorder.style.setProperty("--size", "50px");
    });
});

// pointing the button
var btn = document.querySelector('.mouse-cursor-gradient-tracking');

// getting the offset of the page and binding the data on the css variable --x & --y
btn.onmousemove = function (e) {
    var x = e.pageX - btn.offsetLeft
    var y = e.pageY - btn.offsetTop
    btn.style.setProperty('--x', x + 'px')
    btn.style.setProperty('--y', y + 'px')
}
