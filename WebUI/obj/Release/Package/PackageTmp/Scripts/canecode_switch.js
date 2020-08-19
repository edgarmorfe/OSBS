var Main_canecode_switch = {
    unistarter_switch_enable_switch:function(switch_selector, off_selector="", on_selector=""){
        $('div.canecode_select[data-switch-id="'+ switch_selector +'"]').addClass("is_on");
        $(switch_selector).prop('checked', true);
        Main_canecode_switch.unistarter_switch_show_elements(on_selector);
        Main_canecode_switch.unistarter_switch_hide_elements(off_selector);
    },
    unistarter_switch_disable_switch:function(switch_selector, off_selector="", on_selector=""){
        $('div.canecode_select[data-switch-id="'+ switch_selector +'"]').removeClass("is_on");
        $(switch_selector).prop('checked', false);
        Main_canecode_switch.unistarter_switch_show_elements(off_selector);
        Main_canecode_switch.unistarter_switch_hide_elements(on_selector);
    },
    unistarter_switch_show_elements:function(off_selector){
        if(off_selector != ""){

            if($.isArray(off_selector)){

                $.each(off_selector, function(index, value){

                    $(value).each(function(){

                        var $_this = $(this);
                        if($_this.attr("data-stored-value") != null){
                            $(this).val($_this.attr("data-stored-value"));
                        }
                        $(this).addClass("canecode_active").removeClass("canecode_hidden");

                    });

                });

            }else{

                $(off_selector).each(function(){

                    var $_this = $(this);
                    if($_this.attr("data-stored-value") != null){
                        $(this).val($_this.attr("data-stored-value"));
                    }
                    $(this).addClass("canecode_active").removeClass("canecode_hidden");

                });
            }

        }
    },
    unistarter_switch_hide_elements:function(what_to_hide){
        if(what_to_hide != ""){

            if($.isArray(what_to_hide)){

                $.each(what_to_hide, function(index, arr_value){

                    $(arr_value).each(function(){

                        var $_this = $(this),
                            value = $(this).val();

                        $_this.addClass("canecode_hidden").removeClass("canecode_active");
                        if(value != ""){
                            $_this.attr("data-stored-value", value);
                            $(this).val("");
                        }

                    });
                });

            }else{

                $(what_to_hide).each(function(){

                    var $_this = $(this),
                        value = $(this).val();

                    $_this.addClass("canecode_hidden").removeClass("canecode_active");
                    if(value != ""){
                        $_this.attr("data-stored-value", value);
                        $(this).val("");
                    }
                });
            }

        }
    }
};
function canecode_switch(switch_selector, off_selector="", on_selector=""){
    if($(switch_selector).length > 0){
        $(switch_selector).addClass("canecode_hidden");
        $(switch_selector).after('<div class="canecode_select" id="unistarter_switch" data-switch-id="'+switch_selector+'"><span></span></div>');

        if($(switch_selector + ":checked").length > 0){
            Main_canecode_switch.unistarter_switch_enable_switch(switch_selector, off_selector, on_selector );
        }else{
            Main_canecode_switch.unistarter_switch_disable_switch(switch_selector, off_selector, on_selector );
        }

        $(".canecode_select").click( function(){
            if($(switch_selector + ":checked").length == 0){
                Main_canecode_switch.unistarter_switch_enable_switch(switch_selector, off_selector, on_selector );
            }else{
                Main_canecode_switch.unistarter_switch_disable_switch(switch_selector, off_selector, on_selector );
            }
        });
    }
}