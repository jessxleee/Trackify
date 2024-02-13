const progress = document.getElementById('prg');
const one = document.getElementById('1');
const two = document.getElementById('2');
const three = document.getElementById('3');
const four = document.getElementById('4');

const msg1 = document.getElementById('msg1')
const msg2 = document.getElementById('msg2')
const msg3 = document.getElementById('msg3')

const circles = document.querySelectorAll('.circle');

let currentActive = 1;

function transition() {
    const actives = document.querySelectorAll('.active')
    progress.style.width = (actives.length - 1) / (circles.length - 1) * 100 + '%'
}

two.addEventListener('click', () => {
    currentActive = 2

    /*if(currentActive > circles.length){
      currentActive = circles.length
    }*/



    update()
})

three.addEventListener('click', () => {
    currentActive = 3

    /*if(currentActive > circles.length){
      currentActive = circles.length
    }*/
    update()
})

/*four.addEventListener('click', ()=>{
  currentActive = 4

  /*if(currentActive > circles.length){
    currentActive = circles.length
  }*/
/*update()
})*/

one.addEventListener('click', () => {
    currentActive = 1

    /*if(currentActive < 1){
      currentActive = 1
    }*/
    update()
})


function errmsg() {
    document.getElementById("msgDisplay").innerHTML = "ERROR";
    four.disabled = true;
    update()
}

function msg1Display() {
    document.getElementById("msgDisplay").innerHTML = "Account Progress: Compiling";
}

function msg2Display() {
    document.getElementById("msgDisplay").innerHTML = "Account Progress: Reviewing";
}

function msg3Display() {
    document.getElementById("msgDisplay").innerHTML = "Account Progress: Created";
}

function update() {
    circles.forEach((circle, index) => {
        if (index < currentActive) {
            circle.classList.add('active')
          //circle.id = 'circle active'
        } else {
            circle.classList.remove('active')
          //circle.id = 'circle'
        }
    })

    //const actives = document.querySelectorAll('.active')
    //progress.style.width = (actives.length - 1) / (circles.length - 1) * 100 + '%'

    if (currentActive === 1 && four.disabled == true) {
        one.disabled = false
        two.disabled = false
        three.disabled = false
        four.disabled = false
      //document.getElementById("circle active").style.borderColor = "red";
        circles.forEach((circle, index) => {
            if (index < currentActive) {
                circle.classList.add('red')
                //circle.id = 'circle active'
            } else {
                circle.classList.remove('red')
                //circle.id = 'circle'
            }
        })
      //document.getElementsByClassName("circle active").style.borderColor = "red";

    } else if (currentActive === 1) {
        one.disabled = true
        two.disabled = false
        three.disabled = false
        four.disabled = false
      //document.getElementById("circle active").style.borderColor = "#028A2F";
        $(".circle.active.red").css("border-color", "#028A2F");
      //document.getElementsByClassName("circle active").style.borderColor = "#028A2F";

    } else if (currentActive === 2 && four.disabled == true) {
        one.disabled = false
        two.disabled = false
        three.disabled = false
        four.disabled = false
      //document.getElementById("circle active").style.borderColor = "red";
        circles.forEach((circle, index) => {
            if (index < currentActive) {
                circle.classList.add('red')
                //circle.id = 'circle active'
            } else {
                circle.classList.remove('red')
                //circle.id = 'circle'
            }
        })
      //document.getElementsByClassName("circle active").style.borderColor = "red";

    } else if (currentActive === 2) {
        two.disabled = true
        one.disabled = false
        three.disabled = false
        four.disabled = false
        //document.getElementById("circle active").style.borderColor = "#028A2F";
        $(".circle.active.red").css("border-color", "#028A2F");
      //document.getElementsByClassName("circle active").style.borderColor = "#028A2F";

    } else if (currentActive === 3 && four.disabled == true) {
        one.disabled = false
        two.disabled = false
        three.disabled = false
        four.disabled = false
      //document.getElementById("circle active").style.borderColor = "red";
        circles.forEach((circle, index) => {
            if (index < currentActive) {
                circle.classList.add('red')
                //circle.id = 'circle active'
            } else {
                circle.classList.remove('red')
                //circle.id = 'circle'
            }
        })
      //document.getElementsByClassName("circle active").style.borderColor = "red";

    } else if (currentActive === 3) {
        two.disabled = false
        one.disabled = false
        three.disabled = true
        four.disabled = false
      //document.getElementById("circle active").style.borderColor = "#028A2F";
        $(".circle.active.red").css("border-color", "#028A2F");
      //document.getElementsByClassName("circle active").style.borderColor = "#028A2F";
    }

   
    }















