function CheckboxChange(box) {
    var cbs = document.getElementsByClassName("Checkbox");
    for (var i = 0; i < cbs.length; i++) {
        cbs[i].checked = false;
    }
    box.checked = true;
}

var EntryList = [document.getElementById('FirstName'), document.getElementById('LastName'), document.getElementById('DateOfBirth')];
for(element of EntryList)
{
    element.addEventListener("input", function(){
        document.getElementById('IdentitySubmit').disabled = (EntryList[0].value === '' || EntryList[1].value === '' || EntryList[2].value === '');
    })
}

$(document).ready(function(){

    function DisplayIdentity(bool) {
        if (bool) {
            $("#Identity").show();
        } else {
            $("#Identity").hide();
        }
    }
    DisplayIdentity(false);

    window.addEventListener('message', function(event) {
        var item = event.data;
        if (item.Type === "Identity") {
            if (item.Display == true) {
                DisplayIdentity(true);
            } else {
                DisplayIdentity(false);
            }
        }
    });

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
});