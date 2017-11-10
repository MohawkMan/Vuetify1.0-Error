import "./Common";

$(document).ready(() => {
    console.log("Fading...");
    $(".preloader")
        .css("display", "flex")
        .removeClass("d-flex")
        .fadeOut("slow", () => {
        console.log("Done Fading.")
        $(".headline-underline").addClass("animate");
    });

    let reportSize = () => {
        let xl = window.matchMedia("(min-width: 1200px)"),
            lg = window.matchMedia("(min-width: 992px)"),
            md = window.matchMedia("(min-width: 768px)"),
            sm = window.matchMedia("(min-width: 544px)");

        var x = window.innerWidth;
        var y = "";
        if (xl.matches) {
            y = "Xl";
        } else if (lg.matches) {
            y = "LG";
        } else if (md.matches) {
            y = "MD";
        } else if (sm.matches) {
            y = "SM";
        } else {
            y = "XS";
        }

        $("#bootSize").html("<b>" + y + "</b> " + x);
    };
    $(window).on("resize", reportSize);
    reportSize();
});