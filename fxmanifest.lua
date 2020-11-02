fx_version "adamant"
games { "gta5" }

author "Dislaik"
description "Zombie Survival RPG Framework"
version "0.6.5"

client_script "Client/Release/Outbreak.Client.net.dll"
server_script "Server/Release/Outbreak.Server.net.dll"

ui_page "Client/UI/HTML/NUI.html"

files {
    "Client/UI/HTML/NUI.html",
    "Client/UI/HTML/Assets/CSS/*.css",
    "Client/UI/HTML/Assets/JS/*.js",
    "Client/UI/HTML/Assets/Images/**/*.jpg",
    "Client/UI/HTML/Assets/Images/**/*.png",
    "Client/UI/HTML/Assets/Fonts/**/*.ttf"
}