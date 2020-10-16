/// Male ///
var MaleHatList = [8,2,4,5,6,7,12];
var MaleTopList = [15,0,7,33,57,80,85];
var MaleUndershirtList = [15,15,16,15,15,15,15];
var MaleTorsoList = [15,0,1,0,1,0,1];
var MaleLegsList = [21,1,7,15,36,75,79];
var MaleShoesList = [34,1,4,5,12,14,51];

/// Female ///
var FemaleHatList = [120,7,12,13,21,56,76];
var FemaleTopList = [22,2,3,16,50,55,73];
var FemaleUndershirtList = [14,14,14,14,14,14,14];
var FemaleTorsoList = [15,2,3,11,3,3,14];
var FemaleLegsList = [20,0,3,4,25,27,35];
var FemaleShoesList = [35,3,10,13,50,52,64];

document.getElementById('MaleAppearanceFirstClick').click();
document.getElementById('FemaleAppearanceFirstClick').click();

var MaleHat = document.getElementById('MaleHat');
var MaleHatValue = document.getElementById('MaleHatValue');
var MaleTop = document.getElementById('MaleTop');
var MaleTopValue = document.getElementById('MaleTopValue');
var MaleUndershirtValue = document.getElementById('MaleUndershirtValue');
var MaleTorsoValue = document.getElementById('MaleTorsoValue');
var MaleLegs = document.getElementById('MaleLegs');
var MaleLegsValue = document.getElementById('MaleLegsValue');
var MaleShoes = document.getElementById('MaleShoes');
var MaleShoesValue = document.getElementById('MaleShoesValue');

var FemaleHat = document.getElementById('FemaleHat');
var FemaleHatValue = document.getElementById('FemaleHatValue');
var FemaleTop = document.getElementById('FemaleTop');
var FemaleTopValue = document.getElementById('FemaleTopValue');
var FemaleUndershirtValue = document.getElementById('FemaleUndershirtValue');
var FemaleTorsoValue = document.getElementById('FemaleTorsoValue');
var FemaleLegs = document.getElementById('FemaleLegs');
var FemaleLegsValue = document.getElementById('FemaleLegsValue');
var FemaleShoes = document.getElementById('FemaleShoes');
var FemaleShoesValue = document.getElementById('FemaleShoesValue');

function LeftRange(name) { document.getElementById(name).stepDown(1); };
function RightRange(name) { document.getElementById(name).stepUp(1); };

function MaleTab(event, name) {
    var i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("MaleTabContent");
    
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("MaleTabLinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }
    document.getElementById(name).style.display = "block";
    event.currentTarget.className += " active";
};

function MaleRangeHat(step) {
    if (step == "Left") {
        MaleHat.stepDown(1);
    } else if (step == "Right") {
        MaleHat.stepUp(1);
    }
    MaleHatValue.value = MaleHatList[MaleHat.value];
};
function MaleRangeTop(step) {
    if (step == "Left") {
        MaleTop.stepDown(1);
    } else if (step == "Right") {
        MaleTop.stepUp(1);
    }
    MaleTopValue.value = MaleTopList[MaleTop.value];
    MaleUndershirtValue.value = MaleUndershirtList[MaleTop.value];
    MaleTorsoValue.value = MaleTorsoList[MaleTop.value];
};
function MaleRangeLegs(step) {
    if (step == "Left") {
        MaleLegs.stepDown(1);
    } else if (step == "Right") {
        MaleLegs.stepUp(1);
    }
    MaleLegsValue.value = MaleLegsList[MaleLegs.value];
};
function MaleRangeShoes(step) {
    if (step == "Left") {
        MaleShoes.stepDown(1);
    } else if (step == "Right") {
        MaleShoes.stepUp(1);
    }
    MaleShoesValue.value = MaleShoesList[MaleShoes.value];
};

MaleHat.oninput = function(){ 
    MaleHatValue.value = MaleHatList[this.value]; 
}; MaleHat.oninput();
MaleTop.oninput = function(){
    MaleTopValue.value = MaleTopList[this.value];
    MaleUndershirtValue.value = MaleUndershirtList[this.value]
    MaleTorsoValue.value = MaleTorsoList[this.value]
}; MaleTop.oninput();
MaleLegs.oninput = function(){
    MaleLegsValue.value = MaleLegsList[this.value];
}; MaleLegs.oninput();
MaleShoes.oninput = function(){
    //output.innerHTML = values[this.value]; Maybe i'll need this
    MaleShoesValue.value = MaleShoesList[this.value];
}; MaleShoes.oninput();


////////////////////////////////////////////////////////////////////

function FemaleTab(event, name) {
    var i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("FemaleTabContent");
    
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("FemaleTabLinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }
    document.getElementById(name).style.display = "block";
    event.currentTarget.className += " active";
};

function FemaleRangeHat(step) {
    if (step == "Left") {
        FemaleHat.stepDown(1);
    } else if (step == "Right") {
        FemaleHat.stepUp(1);
    }
    FemaleHatValue.value = FemaleHatList[FemaleHat.value];
};
function FemaleRangeTop(step) {
    if (step == "Left") {
        FemaleTop.stepDown(1);
    } else if (step == "Right") {
        FemaleTop.stepUp(1);
    }
    FemaleTopValue.value = FemaleTopList[FemaleTop.value];
    FemaleUndershirtValue.value = FemaleUndershirtList[FemaleTop.value];
    FemaleTorsoValue.value = FemaleTorsoList[FemaleTop.value];
};
function FemaleRangeLegs(step) {
    if (step == "Left") {
        FemaleLegs.stepDown(1);
    } else if (step == "Right") {
        FemaleLegs.stepUp(1);
    }
    FemaleLegsValue.value = FemaleLegsList[FemaleLegs.value];
};
function FemaleRangeShoes(step) {
    if (step == "Left") {
        FemaleShoes.stepDown(1);
    } else if (step == "Right") {
        FemaleShoes.stepUp(1);
    }
    FemaleShoesValue.value = FemaleShoesList[FemaleShoes.value];
};

FemaleHat.oninput = function(){
    FemaleHatValue.value = FemaleHatList[this.value];
}; FemaleHat.oninput();
FemaleTop.oninput = function(){
    FemaleTopValue.value = FemaleTopList[this.value];
    FemaleUndershirtValue.value = FemaleUndershirtList[this.value]
    FemaleTorsoValue.value = FemaleTorsoList[this.value]
}; FemaleTop.oninput();
FemaleLegs.oninput = function(){
    FemaleLegsValue.value = FemaleLegsList[this.value];
}; FemaleLegs.oninput();
FemaleShoes.oninput = function(){
    FemaleShoesValue.value = FemaleShoesList[this.value];
}; FemaleShoes.oninput();
