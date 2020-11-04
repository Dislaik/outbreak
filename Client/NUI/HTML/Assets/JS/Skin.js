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
    MaleShoesValue.value = MaleShoesList[this.value];
}; MaleShoes.oninput();

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

$(document).ready(function(){

    function DisplayMaleSkin(bool) {
        if (bool) {
            $("#MaleSkin").show();
        } else {
            $("#MaleSkin").hide();
        }
    }
    function DisplayFemaleSkin(bool) {
        if (bool) {
            $("#FemaleSkin").show();
        } else {
            $("#FemaleSkin").hide();
        }
    }
    DisplayMaleSkin(false);
    DisplayFemaleSkin(false);

    window.addEventListener('message', function(event) {
        var item = event.data;
        if (item.Type == "Skin") {
            if (item.Sex == "Male") {
                if (item.Display == true) {
                    DisplayMaleSkin(true);
                } else {
                    DisplayMaleSkin(false);
                }
            }
            else if (item.Sex == "Female") {
                if (item.Display == true) {
                    DisplayFemaleSkin(true);
                } else {
                    DisplayFemaleSkin(false);
                }
            }
        }
    });

    $("#MaleSubmit").click(function () {
        $.post('http://outbreak/Skin:Submit', JSON.stringify({
            Skin: $('input[name=skin]:checked', '#MaleSkinForm').val(),
            Face: $('input[name=face]:checked', '#MaleSkinForm').val(),
            Hair: $('input[name=hair]:checked', '#MaleSkinForm').val(),
            HairColor: $('input[name=haircolor]:checked', '#MaleSkinForm').val(),
            EyesColor: $('input[name=eyescolor]:checked', '#MaleSkinForm').val(),
            Blemishes: $('input[name=blemishes]', '#MaleSkinForm').val(),
            Beard: $('input[name=beard]', '#MaleSkinForm').val(),
            Eyebrows: $('input[name=eyebrows]', '#MaleSkinForm').val(),
            Wrinkles: $('input[name=wrinkles]', '#MaleSkinForm').val(),
            Complexion: $('input[name=complexion]', '#MaleSkinForm').val(),
            SunDamage: $('input[name=sundamage]', '#MaleSkinForm').val(),
            Freckles: $('input[name=freckles]', '#MaleSkinForm').val(),
            ChestHair: $('input[name=chesthair]', '#MaleSkinForm').val(),
            BodyBlemishes: $('input[name=bodyblemishes]', '#MaleSkinForm').val(),
            Hat: $('#MaleHatValue').val(),
            Top: $('#MaleTopValue').val(),
            Undershirt: $('#MaleUndershirtValue').val(),
            Torso: $('#MaleTorsoValue').val(),
            Legs: $('#MaleLegsValue').val(),
            Shoes: $('#MaleShoesValue').val()
        }));
        return
    });
    $("#FemaleSubmit").click(function () {
        $.post('http://outbreak/Skin:Submit', JSON.stringify({
            Skin: $('input[name=skin]:checked', '#FemaleSkinForm').val(),
            Face: $('input[name=face]:checked', '#FemaleSkinForm').val(),
            Hair: $('input[name=hair]:checked', '#FemaleSkinForm').val(),
            HairColor: $('input[name=haircolor]:checked', '#FemaleSkinForm').val(),
            EyesColor: $('input[name=eyescolor]:checked', '#FemaleSkinForm').val(),
            Blemishes: $('input[name=blemishes]', '#FemaleSkinForm').val(),
            Beard: "-1",
            Eyebrows: $('input[name=eyebrows]', '#FemaleSkinForm').val(),
            Wrinkles: $('input[name=wrinkles]', '#FemaleSkinForm').val(),
            Complexion: $('input[name=complexion]', '#FemaleSkinForm').val(),
            SunDamage: $('input[name=sundamage]', '#FemaleSkinForm').val(),
            Freckles: $('input[name=freckles]', '#FemaleSkinForm').val(),
            ChestHair: "-1",
            BodyBlemishes: $('input[name=bodyblemishes]', '#FemaleSkinForm').val(),
            Hat: $('#FemaleHatValue').val(),
            Top: $('#FemaleTopValue').val(),
            Undershirt: $('#FemaleUndershirtValue').val(),
            Torso: $('#FemaleTorsoValue').val(),
            Legs: $('#FemaleLegsValue').val(),
            Shoes: $('#FemaleShoesValue').val()
        }));
        return
    });

    $('#MaleSkinForm').on('input',function(){
        $.post('http://outbreak/Skin:Update', JSON.stringify({
            Skin: $('input[name=skin]:checked', '#MaleSkinForm').val(),
            Face: $('input[name=face]:checked', '#MaleSkinForm').val(),
            Hair: $('input[name=hair]:checked', '#MaleSkinForm').val(),
            HairColor: $('input[name=haircolor]:checked', '#MaleSkinForm').val(),
            EyesColor: $('input[name=eyescolor]:checked', '#MaleSkinForm').val(),
            Blemishes: $('input[name=blemishes]', '#MaleSkinForm').val(),
            Beard: $('input[name=beard]', '#MaleSkinForm').val(),
            Eyebrows: $('input[name=eyebrows]', '#MaleSkinForm').val(),
            Wrinkles: $('input[name=wrinkles]', '#MaleSkinForm').val(),
            Complexion: $('input[name=complexion]', '#MaleSkinForm').val(),
            SunDamage: $('input[name=sundamage]', '#MaleSkinForm').val(),
            Freckles: $('input[name=freckles]', '#MaleSkinForm').val(),
            ChestHair: $('input[name=chesthair]', '#MaleSkinForm').val(),
            BodyBlemishes: $('input[name=bodyblemishes]', '#MaleSkinForm').val(),
            Hat: $('#MaleHatValue').val(),
            Top: $('#MaleTopValue').val(),
            Undershirt: $('#MaleUndershirtValue').val(),
            Torso: $('#MaleTorsoValue').val(),
            Legs: $('#MaleLegsValue').val(),
            Shoes: $('#MaleShoesValue').val()
        }));
        return
    });
    $('.MaleRangeButton').click(function(){
        $.post('http://outbreak/Skin:Update', JSON.stringify({
            Skin: $('input[name=skin]:checked', '#MaleSkinForm').val(),
            Face: $('input[name=face]:checked', '#MaleSkinForm').val(),
            Hair: $('input[name=hair]:checked', '#MaleSkinForm').val(),
            HairColor: $('input[name=haircolor]:checked', '#MaleSkinForm').val(),
            EyesColor: $('input[name=eyescolor]:checked', '#MaleSkinForm').val(),
            Blemishes: $('input[name=blemishes]', '#MaleSkinForm').val(),
            Beard: $('input[name=beard]', '#MaleSkinForm').val(),
            Eyebrows: $('input[name=eyebrows]', '#MaleSkinForm').val(),
            Wrinkles: $('input[name=wrinkles]', '#MaleSkinForm').val(),
            Complexion: $('input[name=complexion]', '#MaleSkinForm').val(),
            SunDamage: $('input[name=sundamage]', '#MaleSkinForm').val(),
            Freckles: $('input[name=freckles]', '#MaleSkinForm').val(),
            ChestHair: $('input[name=chesthair]', '#MaleSkinForm').val(),
            BodyBlemishes: $('input[name=bodyblemishes]', '#MaleSkinForm').val(),
            Hat: $('#MaleHatValue').val(),
            Top: $('#MaleTopValue').val(),
            Undershirt: $('#MaleUndershirtValue').val(),
            Torso: $('#MaleTorsoValue').val(),
            Legs: $('#MaleLegsValue').val(),
            Shoes: $('#MaleShoesValue').val()
        }));
        return
    });

    $('#FemaleSkinForm').on('input',function(){
        $.post('http://outbreak/Skin:Update', JSON.stringify({
            Skin: $('input[name=skin]:checked', '#FemaleSkinForm').val(),
            Face: $('input[name=face]:checked', '#FemaleSkinForm').val(),
            Hair: $('input[name=hair]:checked', '#FemaleSkinForm').val(),
            HairColor: $('input[name=haircolor]:checked', '#FemaleSkinForm').val(),
            EyesColor: $('input[name=eyescolor]:checked', '#FemaleSkinForm').val(),
            Blemishes: $('input[name=blemishes]', '#FemaleSkinForm').val(),
            Beard: "-1",
            Eyebrows: $('input[name=eyebrows]', '#FemaleSkinForm').val(),
            Wrinkles: $('input[name=wrinkles]', '#FemaleSkinForm').val(),
            Complexion: $('input[name=complexion]', '#FemaleSkinForm').val(),
            SunDamage: $('input[name=sundamage]', '#FemaleSkinForm').val(),
            Freckles: $('input[name=freckles]', '#FemaleSkinForm').val(),
            ChestHair: "-1",
            BodyBlemishes: $('input[name=bodyblemishes]', '#FemaleSkinForm').val(),
            Hat: $('#FemaleHatValue').val(),
            Top: $('#FemaleTopValue').val(),
            Undershirt: $('#FemaleUndershirtValue').val(),
            Torso: $('#FemaleTorsoValue').val(),
            Legs: $('#FemaleLegsValue').val(),
            Shoes: $('#FemaleShoesValue').val()
        }));
        return
    });
    $('.FemaleRangeButton').click(function(){
        $.post('http://outbreak/Skin:Update', JSON.stringify({
            Skin: $('input[name=skin]:checked', '#FemaleSkinForm').val(),
            Face: $('input[name=face]:checked', '#FemaleSkinForm').val(),
            Hair: $('input[name=hair]:checked', '#FemaleSkinForm').val(),
            HairColor: $('input[name=haircolor]:checked', '#FemaleSkinForm').val(),
            EyesColor: $('input[name=eyescolor]:checked', '#FemaleSkinForm').val(),
            Blemishes: $('input[name=blemishes]', '#FemaleSkinForm').val(),
            Beard: "-1",
            Eyebrows: $('input[name=eyebrows]', '#FemaleSkinForm').val(),
            Wrinkles: $('input[name=wrinkles]', '#FemaleSkinForm').val(),
            Complexion: $('input[name=complexion]', '#FemaleSkinForm').val(),
            SunDamage: $('input[name=sundamage]', '#FemaleSkinForm').val(),
            Freckles: $('input[name=freckles]', '#FemaleSkinForm').val(),
            ChestHair: "-1",
            BodyBlemishes: $('input[name=bodyblemishes]', '#FemaleSkinForm').val(),
            Hat: $('#FemaleHatValue').val(),
            Top: $('#FemaleTopValue').val(),
            Undershirt: $('#FemaleUndershirtValue').val(),
            Torso: $('#FemaleTorsoValue').val(),
            Legs: $('#FemaleLegsValue').val(),
            Shoes: $('#FemaleShoesValue').val()
        }));
        return
    });

    $(document).on('keydown', function(event) {
        if (event.which == 65) {
            $.post('http://outbreak/Skin:RotateLeft', JSON.stringify({
                Left: 10
            }));
            return
        }
        if (event.which == 68) {
            $.post('http://outbreak/Skin:RotateRight', JSON.stringify({
                Right: 10
            }));
            return
        }
        if (event.which == 9) {
            $.post('http://outbreak/Skin:View', JSON.stringify({
            }));
            return
        }
    });

});