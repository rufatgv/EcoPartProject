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

    //location
    $(".location-dest").click(function(){      
        $(".dropdown-checkbox").toggle(); 
    });
    $("#close-btnn").click(function(){   
        $(".dropdown-checkbox").hide(); 
    });
    //location

    let plus=document.querySelector(".increase")
    let minus=document.querySelector(".decrease")
    let number=document.querySelector(".number")
    
    
    let count=1;
    plus.addEventListener("click",function(e){
        count++;
        e.target.previousElementSibling.innerText=count;
        console.log(e.target.previousElementSibling.innerText);
        
    })
    
    minus.addEventListener("click",function(){
        if (count!=1) {
            count--;
            number.innerText=count;
        }    
    })
    



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