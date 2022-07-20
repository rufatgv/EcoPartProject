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


//main
let plus=document.querySelector(".increase")
let minus=document.querySelector(".decrease")
let number=document.querySelector(".number")
console.log(number);

let count=1;
plus.addEventListener("click",function(){
    count++;
    number.innerText=count;
    
})

minus.addEventListener("click",function(){
    if (count!=1) {
        count--;
        number.innerText=count;
    }    
})
//main


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