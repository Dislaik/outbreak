$(document).ready(function(){

    function DisplayIdentity(bool) {
        if (bool) {
            $("#Identity").show();
        } else {
            $("#Identity").hide();
        }
    }
    DisplayIdentity(false);

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
        if (item.Type === "Identity") {
            if (item.Display == true) {
                DisplayIdentity(true);
            } else {
                DisplayIdentity(false);
            }
        }

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

        /*if (item.locale == "EN") {
            $("#name").html("Hello World");
        }*/
    })

    $("#IdentitySubmit").click(function () {
        let EntryFirstName = $("#FirstName").val()
        let EntryLastName = $("#LastName").val()
        let EntryDateOfBirth = $("#DateOfBirth").val()
        var Male = $("#MaleCheckbox").prop('checked')
        var Female = $("#FemaleCheckbox").prop('checked')
        var EntrySex;
        if (Male) {
            EntrySex = "Male";
        } else if (Female) {
            EntrySex = "Female";
        }
        
        $.post('http://outbreak/Identity:Submit', JSON.stringify({
            FirstName: EntryFirstName,
            LastName: EntryLastName,
            DateOfBirth: EntryDateOfBirth,
            Sex: EntrySex
        }));
        return
    })

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
    })
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
    })

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
    })
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
    })

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
    })
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
    })

    /*$(document).on ("click", ".Slots", function () {
        SetSlots(dictionary);
        $.post('http://outbreak/Inventory:SelectItem', JSON.stringify({
                Item: this.id
        }));
        return
    });*/

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
})