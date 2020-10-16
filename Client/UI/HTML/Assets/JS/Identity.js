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
