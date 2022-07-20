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

//media query
$(document).ready(function(){
    $(".oneh").click(function(){
       $(".one").fadeToggle("slow");
    })
    $(".twoh").click(function(){
        $(".two").fadeToggle("slow");
     })
     $(".threeh").click(function(){
        $(".three").fadeToggle("slow");
     })
     $(".fourh").click(function(){
        $(".four").fadeToggle("slow");
     })

})
//media query
