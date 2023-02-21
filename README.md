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

This tool is called "Modular Tool Manager" it is a plugin based GUI script executer. The default tool does deliver basic plugins for binary and script execution on Windows. This is the main view you are getting presented with.

![Main view of the program][image-main-view]
![Main view of the program in light theme][image-main-view-light]

The tool is getting tested on Windows and Linux Mint, since it is based on [AvaloniaUI][avaloniaui] it should run on mac as well. I will not provide a build for it.

> :warning: **Important:** I can only test it for Windows 10 so please check if the Avalonia variant is running on older/newer systems as well.

You can extend the functionality of the tool by writing your own plugins. Please take a look at the [Wiki] to find out how to do that.
The application does support the following languages for now, feel free to extend it via Pull Request

* German
* English

To download the tool please head over to the [download page][downloadPage].
Either use the latest develop build which is unstable or the latest tagged build.

If you found a bug or need any help with the application please head over to the [issue][issuePage] area. As an alternative there is a menu entry in the application which will open the issue page as well.

## Installation

Right now there is only a zip file you can extract somewhere to your disc. Just follow the instructions for the nighty build.

I will try to add a windows installer later on.

### Latest nightly build

The latest nightly build are self contained application builds which are getting packed into a zip folder.
To install the application get the build for your operation system and extract the files to a folder on your disc.
Start the `ModularToolManager.exe` or if you on a linux machine the "ModularToolManager" binary.

## Downloads

Click [here][downloadPage] to get to the download section of this project on GitHub. Please follow the installation instructions above.

## Found a bug or got a question?

Please head over to the  [issue page][issuePage].

## Plugins

Plugins are getting installed by moving the dll files into the "plugins" folder inside of the root application. 
If you freshly install the program you will see there is a defaultPlugins.dll. This will provide you with some basic functionality.
To develop a plugin on your own please take a look at the [wiki] page.

### :no_entry_sign: Warning :no_entry_sign:

Using plugins from any source can harm your machine. Always check if you trust the author of the plugin.
Technically the plugin will run with the user rights you did start the application with allowing any plugin to execute harmful code.

## Additional Screenshots

### Dark

![screenshot-1-dark]  
![screenshot-2-dark]
![screenshot-3-dark]

### Light

![screenshot-1-light]
![screenshot-2-light]
![screenshot-3-light]


## License
[![FOSSA Status](https://app.fossa.com/api/projects/git%2Bgithub.com%2FXanatosX%2FModularToolManager.svg?type=large)](https://app.fossa.com/projects/git%2Bgithub.com%2FXanatosX%2FModularToolManager?ref=badge_large)

[wiki]: https://github.com/XanatosX/ModularToolManager/wiki
[dotnet6]: https://dotnet.microsoft.com/en-us/download/dotnet/6.0
[avaloniaui]: https://avaloniaui.net/
[image-main-view]: https://imgur.com/oswayay.png
[image-main-view-light]: https://i.imgur.com/lqmlo5U.png
[downloadPage]: https://bitbucket.org/XanatosX/modulartoolmanager/downloads/
[issuePage]: https://bitbucket.org/XanatosX/modulartoolmanager/issues
[screenshot-1-dark]: https://imgur.com/mr3Folx.png
[screenshot-1-light]: https://i.imgur.com/Fxgu18M.png
[screenshot-2-dark]: https://i.imgur.com/LhMGcEx.png
[screenshot-2-light]: https://i.imgur.com/5sukgHg.png
[screenshot-3-dark]: https://i.imgur.com/UPURC5u.png
[screenshot-3-light]: https://i.imgur.com/rye7J9U.png
