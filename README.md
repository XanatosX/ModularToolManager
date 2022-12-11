# Modular Tool Manager

[![Recent develop build](https://github.com/XanatosX/ModularToolManager/actions/workflows/create-latest-develop-build.yml/badge.svg)](https://github.com/XanatosX/ModularToolManager/actions/workflows/create-latest-develop-build.yml)
[![Latest release](https://badgen.net/github/release/Naereen/Strapdown.js)](https://github.com/XanatosX/ModularToolManager/releases)
[![Github all releases](https://img.shields.io/github/downloads/Naereen/StrapDown.js/total.svg)](https://GitHub.com/XanatosX/ModularToolManager/releases/)

[![Codacy Badge](https://api.codacy.com/project/badge/Grade/a76b14fe59a54a9ab4d3e4f6afed53dc)](https://app.codacy.com/app/simonaberle/ModularToolManager?utm_source=github.com&utm_medium=referral&utm_content=XanatosX/ModularToolManager&utm_campaign=badger)
[![FOSSA Status](https://app.fossa.com/api/projects/git%2Bgithub.com%2FXanatosX%2FModularToolManager.svg?type=shield)](https://app.fossa.com/projects/git%2Bgithub.com%2FXanatosX%2FModularToolManager?ref=badge_shield)

[![Linux](https://svgshare.com/i/Zhy.svg)](https://svgshare.com/i/Zhy.svg)
[![Windows](https://svgshare.com/i/ZhY.svg)](https://svgshare.com/i/ZhY.svg)

## Requirements

* [.NET Desktop Runtime 6.x.x][dotnet6]

## About the Project

This tool is called "Modular Tool Manager" it is a plugin based GUI script executer. The default tool does deliver basic plugins for binary and script execution on Windows.

![Main view of the rogramp][image-main-view]

The tool is getting tested on Windows and Linux Mint, since it is based on [AvaloniaUI][avaloniaui] it should run on mac as well.

**Important:** I can only test it for Windows 10 so please check if the Avalonia variant is running on older systems as well.

You can extend the functionality of the tool by writing your own plugins. Please take a look at the [Wiki] to find out how to do that.
The application does support the following languages for now, feel free to extend it via Pull Request

* German
* Englisch

To download the tool please head over to the [download page][downloadPage]. If you found a bug or need any help with the applcation please head over to the [issue][issuePage] area.



## Installation

### Latest nighlty build

The latest nighlty build are self contained application builds which are getting packed into a zip folder.
To install the application get the build for your operation system and extract the files to a folder on your disc.
Start the "ModularToolManager.exe" or if you on a linux machine the "ModularToolManager" binary.

## Downloads

Click [here][downloadPage] to get to the download section of this project on GitHub. Please follow the installation instructions above.

## Found a bug or got a question?

Please head over to the  [issue page][issuePage].

## Plugins

Plugins are getting installed by moving the dll files into the "plugins" folder inside of the root application. 
If you freshly install the program you will see there is a defaultPlugins.dll. This will provide you with some basic functionality.
To develop a plugin on your own please take a look at the [wiki] page

### Warning

Using plugins from any source can harm your machine. Always check if you trust the author of the plugin.
Technicaly the plugin will run with the user rights you did start the application with allowing any plugin to execute harmful code.

## Additional Screenshots
![screenshot-1]  
![screenshot-2]  
![screenshot-3]



## License
[![FOSSA Status](https://app.fossa.com/api/projects/git%2Bgithub.com%2FXanatosX%2FModularToolManager.svg?type=large)](https://app.fossa.com/projects/git%2Bgithub.com%2FXanatosX%2FModularToolManager?ref=badge_large)

[wiki]: https://github.com/XanatosX/ModularToolManager/wiki
[dotnet6]: https://dotnet.microsoft.com/en-us/download/dotnet/6.0
[avaloniaui]: https://avaloniaui.net/
[image-main-view]: https://imgur.com/oswayay.png
[downloadPage]: https://bitbucket.org/XanatosX/modulartoolmanager/downloads/
[issuePage]: https://bitbucket.org/XanatosX/modulartoolmanager/issues
[screenshot-1]: https://imgur.com/mr3Folx.png
[screenshot-2]: https://imgur.com/UPURC5u.png
[screenshot-3]: https://imgur.com/UKl60fA.png
