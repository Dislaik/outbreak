var EmptySlots = document.getElementById("SlotsContainer")
var EmptyWeight = document.getElementById("TotalWeight")
var Inventory;
var Loot;
var TotalWeight;
var WeaponMeleeList = ["weapon_dagger", "weapon_bat", "weapon_bottle", "weapon_crowbar", "weapon_flashlight", "weapon_golfclub", "weapon_hammer", "weapon_hatchet", "weapon_knuckle", "weapon_knife",
"weapon_machete", "weapon_switchblade", "weapon_nightstick", "weapon_wrench", "weapon_battleaxe", "weapon_poolcue", "weapon_stone_hatchet"]
var WeaponList = ["weapon_dagger", "weapon_bat", "weapon_bottle", "weapon_crowbar", "weapon_flashlight", "weapon_golfclub", "weapon_hammer", "weapon_hatchet", "weapon_knuckle", "weapon_knife",
"weapon_machete", "weapon_switchblade", "weapon_nightstick", "weapon_wrench", "weapon_battleaxe", "weapon_poolcue", "weapon_stone_hatchet", "weapon_pistol", "weapon_pistol_mk2", "weapon_combatpistol",
"weapon_appistol", "weapon_stungun", "weapon_pistol50", "weapon_snspistol", "weapon_snspistol_mk2", "weapon_heavypistol", "weapon_vintagepistol", "weapon_flaregun", "weapon_marksmanpistol", "weapon_revolver",
"weapon_revolver_mk2", "weapon_doubleaction", "weapon_raypistol", "weapon_ceramicpistol", "weapon_navyrevolver", "weapon_microsmg", "weapon_smg", "weapon_smg_mk2", "weapon_assaultsmg", "weapon_combatpdw",
"weapon_machinepistol", "weapon_minismg", "weapon_raycarbine", "weapon_pumpshotgun", "weapon_pumpshotgun_mk2", "weapon_sawnoffshotgun", "weapon_assaultshotgun", "weapon_bullpupshotgun", "weapon_musket",
"weapon_heavyshotgun", "weapon_dbshotgun", "weapon_autoshotgun", "weapon_assaultrifle", "weapon_assaultrifle_mk2", "weapon_carbinerifle", "weapon_carbinerifle_mk2", "weapon_advancedrifle", "weapon_specialcarbine",
"weapon_specialcarbine_mk2", "weapon_bullpuprifle", "weapon_bullpuprifle_mk2", "weapon_compactrifle", "weapon_mg", "weapon_combatmg", "weapon_combatmg_mk2", "weapon_gusenberg", "weapon_sniperrifle", 
"weapon_heavysniper", "weapon_heavysniper_mk2", "weapon_marksmanrifle", "weapon_marksmanrifle_mk2", "weapon_rpg", "weapon_grenadelauncher", "weapon_grenadelauncher_smoke", "weapon_minigun", "weapon_firework",
"weapon_railgun", "weapon_hominglauncher", "weapon_compactlauncher", "weapon_rayminigun", "weapon_grenade", "weapon_bzgas", "weapon_molotov", "weapon_stickybomb", "weapon_proxmine", "weapon_snowball",
"weapon_pipebomb", "weapon_ball", "weapon_smokegrenade", "weapon_flare", "weapon_petrolcan", "gadget_parachute", "weapon_fireextinguisher", "weapon_hazardcan"]

function SetSlots(Items) {
    var Div = "";
    var LimitItem;
    TotalWeight = 0;
    var WeightItem;
    for (var Item in Items) {
        if (typeof(Items[Item].Limit) == "undefined") 
        {
            if ( WeaponMeleeList.indexOf(Item) > -1 )
            {
                LimitItem = "";
            }
            else
            {
                LimitItem = Items[Item].Ammo;
            }
            WeightItem = Items[Item].Weight * 1;
        } 
        else 
        {
            LimitItem = Items[Item].Amount + "/" + Items[Item].Limit;
            WeightItem = Items[Item].Weight * Items[Item].Amount;
        }

        Div += `<div class="col-3">
                    <div id="${Item}" class="Slots">
                        <p class="Weight">${WeightItem} KG</p>
                        <p class="Limit">${LimitItem}</p>
                        <p class="Label">${Items[Item].Label}</p>
                        <img src="Assets/Images/Inventory/${Item}.png" width="100%" height="100%">
                    </div>
                    <div class="hide">
                        <p class="title">${Items[Item].Label}</p>
                        <img class="ImageDescription" src="Assets/Images/Inventory/${Item}.png">
                        <p class="Description">${Items[Item].Description}</p>
                    </div>
                </div>`;
        TotalWeight += (WeightItem);
    }
    Div = `<div class="row">` + Div + `</div>`;
    EmptySlots.innerHTML = Div;
    EmptyWeight.innerHTML = parseFloat(TotalWeight).toFixed(1) + " KG";
}

function SetSlotsLoot(Items) {
    var Div = "";
    var LimitItem;
    var WeightItem;
    for (var Item in Items) {
        if (typeof(Items[Item].Limit) == "undefined") 
        {
            if ( WeaponMeleeList.indexOf(Items[Item].Name) > -1 )
            {
                LimitItem = "";
            }
            else
            {
                LimitItem = Items[Item].Ammo;
            }
            WeightItem = Items[Item].Weight * 1;
        } 
        else 
        {
            LimitItem = Items[Item].Amount;
            WeightItem = Items[Item].Weight;
        }
        Div += `<div class="col-3">
                    <div id="${Item}" class="SlotsLoot">
                        <p class="Weight">${WeightItem} KG</p>
                        <p class="Limit">${LimitItem}</p>
                        <p class="Label">${Items[Item].Label}</p>
                        <img src="Assets/Images/Inventory/${Items[Item].Name}.png" width="100%" height="100%">
                    </div>
                </div>`;
    }
    Div = `<div class="row">` + Div + `</div>`;
    document.getElementById("SlotsContainerLoot").innerHTML = Div;
}

function InputNumber(ClassName)
{
    document.getElementById(ClassName).addEventListener('keydown', function(e) {
        var key   = e.keyCode ? e.keyCode : e.which;
    
        if (!( [8, 9, 13, 27, 46, 110, 190].indexOf(key) !== -1 ||
             (key == 65 && ( e.ctrlKey || e.metaKey  ) ) || 
             (key >= 35 && key <= 40) ||
             (key >= 48 && key <= 57 && !(e.shiftKey || e.altKey)) ||
             (key >= 96 && key <= 105)
           )) e.preventDefault();
    });
}
InputNumber('GiveInput');
InputNumber('ThrowInput');


let InventoryMouse = false;
const content = document.getElementById('Inventory');
const content2 = document.getElementById('Loot');

content.addEventListener('mouseenter', () => {
    InventoryMouse = true;
})
content2.addEventListener('mouseenter', () => {
    InventoryMouse = true;
})

content.addEventListener('mouseleave', () => {
    InventoryMouse = false;
})
content2.addEventListener('mouseleave', () => {
    InventoryMouse = false;
})

$(document).ready(function(){
    
    function DisplayInventory(bool) {
        if (bool) {
            $('#Inventory').fadeIn(200);
        } else {
            $('#Inventory').fadeOut(200);
        }
    }
    DisplayInventory(false);
    function DisplayQuickSlots(bool) {
        if (bool) {
            $("#QuickSlots").show();
        } else {
            $("#QuickSlots").hide();
        }
    }
    DisplayQuickSlots(false);
    function DisplayLoot(bool) {
        if (bool) {
            $('#Loot').fadeIn(200);
        } else {
            $('#Loot').fadeOut(200);
        }
    }
    DisplayLoot(false);
    window.addEventListener('message', function(event) {
        var item = event.data;

        if (item.Type == "Inventory") {
            if (item.Display == true) {
                DisplayInventory(true);
                Inventory = item.Items;
                SetSlots(Inventory);
                $('.Slots').draggable({
                    helper: function (event) {
                        var ret = $(this).clone();
                        $(this).toggleClass("ghost");
                        return ret;
                    },
                    zIndex: 99999,
                    revert: 'invalid',
                    containment: "#Inventory",
                    drag: function (event, ui) {
                        if ( WeaponList.indexOf($(this).attr("id")) > -1 )
                        {
                            $('#Use').children('a').html("Equip/Unequip");
                        }
                    },
                    stop: function (event, ui) {
                        $(this).toggleClass("ghost");
                        $('#Use').children('a').html("Use");
                    }
                });
            } else {
                DisplayInventory(false);
            }

        }
        if (item.Type == "UpdateInventory") {
            Inventory = item.Items;
            SetSlots(Inventory);
            $('.Slots').draggable({
                helper: function (event) {
                    var ret = $(this).clone();
                    $(this).toggleClass("ghost");
                    return ret;
                },
                zIndex: 99999,
                revert: 'invalid',
                containment: "#Inventory",
                drag: function (event, ui) {
                    if ( WeaponList.indexOf($(this).attr("id")) > -1 )
                    {
                        $('#Use').children('a').html("Equip/Unequip");
                    }
                },
                stop: function (event, ui) {
                    $(this).toggleClass("ghost");
                    $('#Use').children('a').html("Use");
                }
            });
            RepetDraggables();
        }

        if (item.Type == "Loot") {
            if (item.Display == true) {
                DisplayLoot(true);
                $('.SlotsLoot').draggable({
                    helper: function (event) {
                        var ret = $(this).clone();
                        $(this).toggleClass("ghost");
                        return ret;
                    },
                    zIndex: 99999,
                    revert: 'invalid',
                    containment: "#Loot",
                    stop: function (event, ui) {
                        $(this).toggleClass("ghost");
                    }
                });
                $( "#SlotsContainer" ).droppable({
                    accept: ".SlotsLoot",
                    drop: function(event, ui) 
                    { 
                        $.post('http://outbreak/Loot:Lootable', JSON.stringify({
                            Item: ui.draggable.attr("id")
                        }));
                    }
                });
            } else {
                DisplayLoot(false);
            }
        }

        if (item.Type == "UpdateLoot") {
            Loot = item.Items;
            SetSlotsLoot(Loot);
                $('.SlotsLoot').draggable({
                    helper: function (event) {
                        var ret = $(this).clone();
                        $(this).toggleClass("ghost");
                        return ret;
                    },
                    zIndex: 99999,
                    revert: 'invalid',
                    containment: "#Loot",
                    stop: function (event, ui) {
                        $(this).toggleClass("ghost");
                    }
                });
                $( "#SlotsContainer" ).droppable({
                    accept: ".SlotsLoot",
                    drop: function(event, ui) 
                    { 
                        $.post('http://outbreak/Loot:Lootable', JSON.stringify({
                            Item: ui.draggable.attr("id")
                        }));
                    }
                });
            
        }

        if (item.Type == "QuickSlots") {
            if (item.Display == true) {
                DisplayQuickSlots(true);
            } else {
                DisplayQuickSlots(false);
            }
        }
    });
    
    
    $(document).on('keydown', function(event) {
        if (InventoryMouse && (event.which == 27 || event.which == 113)) {
                $.post('http://outbreak/Inventory:Close', JSON.stringify({
                }));
                $.post('http://outbreak/Loot:Close', JSON.stringify({
                }));
            return
        }
    });

    function RepetDraggables() {
        $('.Slots').draggable({
            helper: function (event) {
                var ret = $(this).clone();
                $(this).toggleClass("ghost");
                return ret;
            },
            zIndex: 99999,
            revert: 'invalid',
            containment: "#Inventory",
            drag: function (event, ui) {
                if ( WeaponList.indexOf($(this).attr("id")) > -1 )
                {
                    $('#Use').children('a').html("Equip/Unequip");
                }
            },
            stop: function (event, ui) {
                $(this).toggleClass("ghost");
                $('#Use').children('a').html("Use");
            }
        });
        
        $('#Use').droppable({
            accept: ".Slots",
            drop: function(event, ui) 
            { 
                SetSlots(Inventory);
                $.post('http://outbreak/Inventory:Use', JSON.stringify({
                        Item: ui.draggable.attr("id"),
                        Ammo: Inventory[ui.draggable.attr("id")].Ammo
                }));
                RepetDraggables();
                return
            }
        });
        $('#Give').droppable({
            accept: ".Slots",
            drop: function(event, ui) {
            SetSlots(Inventory);
            $.post('http://outbreak/Inventory:Give', JSON.stringify({
                    Item: ui.draggable.attr("id"),
                    Amount: $("#GiveInput").val(),
                    Limit: Inventory[ui.draggable.attr("id")].Amount,
                    Ammo: Inventory[ui.draggable.attr("id")].Ammo
            }));
            RepetDraggables();
            return
        }});
        $('#Throw').droppable({
            accept: ".Slots",
            drop: function(event, ui) {
            SetSlots(Inventory);
            $.post('http://outbreak/Inventory:Throw', JSON.stringify({
                    Item: ui.draggable.attr("id"),
                    Ammo: Inventory[ui.draggable.attr("id")].Ammo,
                    Amount: $("#ThrowInput").val(),
                    Limit: Inventory[ui.draggable.attr("id")].Limit
            }));
            RepetDraggables();
            return
        }});
    }
    RepetDraggables();
});

