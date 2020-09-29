# Zombie Outbreak V 0.4.5
Zombie Outbreak is a Zombie Surival RPG Framework, you will must survive and fight against hordes of zombies to ensure your survival.

![](https://i.imgur.com/sE2NCpr.png)

## Features
- Synchronized zombies
- Vehicles abandoned
- Safe zones
- Character creation

## Requirements
- [Visual Studio 2019](https://visualstudio.microsoft.com/es/vs/)
- [MariaDB](https://downloads.mariadb.org/)

## Installation
1. Donwload this repository
2. Put `outbreak` folder in your folder resources
3. Add the resource in your `server.cfg`

It should look like this
```cfg
ensure mapmanager
ensure spawnmanager
ensure sessionmanager
ensure chat
ensure rconlog
ensure baseevents
ensure outbreak
```
4. Add database `outbreak.sql` to your server
5. Open file `outbreak.sln` with Visual Studio
6. Establish the connection to the database in `Server/Config/Config.cs`
7. Compile (F7) the project for save the changes

## Documentation
Take a look at [Wiki](https://github.com/Dislaik/outbreak/wiki)

### Discord
https://discord.gg/5KZBHPP (Unofficial Discord)

### Patreon
https://www.patreon.com/Dislaik

## License
Copyright © 2020 Matías Salas.

Outbreak Framework is a community project, you legal permission to copy, distribute and/or modify it only if you have forked this repository, If it's not a forked repo, then the release will be taken down by DMCA request.

