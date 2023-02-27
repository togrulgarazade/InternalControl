// Page Loader

var loader = document.getElementById("loadertop");


const pageLoading = () => {


    let loadingDistance = -body.getBoundingClientRect().top;
    let loadingWidth = (loadingDistance / (body.getBoundingClientRect().height - document.documentElement.clientHeight)) * 100;
    let value = Math.floor(loadingWidth);

    loader.style.width = value + "%";

    if (value === 0) {
        loader.style.width = "100%";
    }



};


window.addEventListener("load", pageLoading);

const pageLoadingNone = () => setTimeout(() => {
    loader.style.display = "none";
}, 1200);

window.addEventListener("load", pageLoadingNone);

// Page Loader - END