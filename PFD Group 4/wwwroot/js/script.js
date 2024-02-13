const progressBar = document.getElementById("progress-bar");
//const progressNext = document.getElementById("progress-next");
//const progressPrev = document.getElementById("progress-prev");
//const rb1 = document.getElementById("rb1");
//const rb2 = document.getElementById("rb2");
const upbtn = document.getElementById("upbtn")
const steps = document.querySelectorAll(".step");
let active = 2;

/*progressNext.addEventListener("click", () => {
    active++;
    if (active > steps.length) {
        active = steps.length;
    }
    updateProgress();
});


progressPrev.addEventListener("click", () => {
    active--;
    if (active < 1) {
        active = 1;
    }
    updateProgress();
});*/

const updateProgress = () => {
    // toggle active class on list items
    //steps.forEach((step, i) => {
    //    if (i < active) {
    //        step.classList.add("active");
    //    } else {
    //        step.classList.remove("active");
    //    }
    //});
    // set progress bar width  
    progressBar.style.width = 67 %
    // ((active - 1) / (steps.length - 1)) * 67 + "%";
    // enable disable prev and next buttons
    /*if (active === 1) {
        progressPrev.disabled = true;
    } else if (active === steps.length) {
        progressNext.disabled = true;
    } else {
        progressPrev.disabled = false;
        progressNext.disabled = false;
    }*/
}




function checking() {
    active++;
    if (active > steps.length) {
        active = steps.length;
    }
    updateProgress();
}

//rb2.onclick() = checking();

//document.getElementById('rb').onclick = checking();


