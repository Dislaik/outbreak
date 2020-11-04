fx_version "adamant"
games { "gta5" }

author "Dislaik"
description "Zombie Survival RPG Framework"
version "0.6.5"

client_script "Client/Release/Outbreak.Client.net.dll"
server_script "Server/Release/Outbreak.Server.net.dll"

ui_page "Client/NUI/HTML/NUI.html"

files {
    "Client/NUI/HTML/NUI.html",
    "Client/NUI/HTML/Assets/CSS/*.css",
    "Client/NUI/HTML/Assets/JS/*.js",
    "Client/NUI/HTML/Assets/Images/**/*.jpg",
    "Client/NUI/HTML/Assets/Images/**/*.png",
    "Client/NUI/HTML/Assets/Fonts/**/*.ttf"
}