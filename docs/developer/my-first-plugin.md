# My First Plugin

How to write your first plugin for the modular tool manager? This guide will provide the basic information

## Initial setup

First of all let's create a new project to do so use the following commands. The plugin will be called `MyFirstPlugin` but you can name it as you want.

>:information_source: Does require the [unlisted nuget package][nuget-package] `ModularToolManagerPlugin` which is also part of this repository.

```
mkdir MyFirstPlugin
cd MyFirstPlugin
dotnet new sln
dotnet new classlib -o src/MyFirstPlugin
dotnet sln add ./src/MyFirstPlugin
cd ./src/MyFirstPlugin
dotnet add package ModularToolManagerPlugin --version 1.0.0
cd ../..
dotnet restore
dotnet build
```

>:warning: Make sure that your plugin does target `.net8.0`

Open the plugin with a code editor like [vscode][vscode].

Rename the 'Class1.cs' file to the name of your plugin, in this case "MyFirstBatPlugin" since it should be used to start batch files.

Extend the `AbstractFunctionPlugin` class. You could also use the `IFunctionPlugin` but you will need to add more code on your own. This tutorial will use the abstract function plugin.

Add all the missing methods as provided by your ide.

You should end up with something like this

```csharp
using ModularToolManagerPlugin.Models;
using ModularToolManagerPlugin.Plugin;

namespace MyFirstPlugin;

public class MyFirstBatPlugin : AbstractFunctionPlugin
{
    public override void Dispose()
    {
        throw new NotImplementedException();
    }

    public override bool Execute(string parameters, string path)
    {
        throw new NotImplementedException();
    }

    public override IEnumerable<FileExtension> GetAllowedFileEndings()
    {
        throw new NotImplementedException();
    }

    public override string GetDisplayName()
    {
        throw new NotImplementedException();
    }

    public override PluginInformation GetPluginInformation()
    {
        throw new NotImplementedException();
    }

    public override bool IsOperationSystemValid()
    {
        throw new NotImplementedException();
    }

    public override void ResetSettings()
    {
        throw new NotImplementedException();
    }
}
```

For this plugin we won't use any [injectable services][injectable-services] or plugin settings. This will allow use to remove the code inside of the `ResetSettings()` and `Dispose()` method.

> :information_source: The `ResetSettings()` method is used to reset the plugin settings before each call. This is required that the manager can set the user settings correctly and there are no left overs from previous executions. Since we will not used settings, this is not relevant.

The methods do look like this now.

```csharp
public override void Dispose() { }
public override void ResetSettings() { }
```

The next step will be to define the plugin information via the `GetPluginInformation()` method. To do so return the following object.

```csharp
    public override PluginInformation GetPluginInformation()
    {
        return new PluginInformation
        {
            Authors = new List<string> { "XanatosX " }.AsReadOnly(),
            Description = "Example plugin to run bat files on a windows machine",
            License = "MIT",
            ProjectUrl = "https://github.com/XanatosX/ModularToolManager",
            Version = new Version("1.0.0.0")
        };
    }
```

You can change the information to match your situation. Keep in mind that the description can use Markdown to format it to your desire.

Next let's change the display name, this is used to show the plugin in the plugins selection of the tool.

Change the `GetDisplayName()` like this

```csharp
    public override string GetDisplayName()
    {
        return "My First Bat Plugin (Tutorial)";
    }
```

Now we will validate if the plugin is suitable for the operation system we are working on. To do so edit the `IsOperationSystemValid()` method.

```csharp
    public override bool IsOperationSystemValid()
    {
        return OperatingSystem.IsWindows();
    }
```

>:information_source: If you want to target linux use `OperatingSystem.IsLinux()` instead. If you want to support both return true for both systems.

Let's configure the allowed file endings next. To do so change the `GetAllowedFileEndings()` method to something like this.

```csharp
    public override IEnumerable<FileExtension> GetAllowedFileEndings()
    {
        return new List<FileExtension>
        {
            new FileExtension("Batch-File", "bat"),
        };
    }
```

Each entry in the list will be shown as a possibility to select files for the plugin. The first parameter for the `FileExtension` is the name displayed ion the dialog for the file type. The second one is the extension.

The last step is the `Execute(string parameters, string path)` method. This method does get you two parameters. The first one are the parameters provided inside the function to run the target path. The second string is the path to the file which should be executed.

First of all make sure the path does exist, and the ending is indeed supported. Next make sure to run the bat file with a process starter. For this tutorial I do use the following code.

```csharp
    public override bool Execute(string parameters, string path)
    {
        if (!File.Exists(path))
        {
            return false;
        }
        var info = new FileInfo(path);
        if (!GetAllowedFileEndings().Any(extension => extension.Extension == info.Extension.Replace(".", string.Empty)))
        {
            return false;
        }

        var processStarter = new ProcessStartInfo
        {
            FileName = path,
            Arguments = parameters
        };
        Process.Start(processStarter);
        return true;
    }
```

To see the complete code of the tutorial class take a look at my [this gist][complete-class].

Build your plugin for testing purpose via `dotnet build`. Go into the `bin/Debug/net8.0` directory and search for the `MyFirstPlugin.dll`. Copy the file and to the directory described in the next part.

## Add the plugin to the manager

Close the manager to make sure every plugin is unloaded. Go to the installation directory of the manager inside of the `plugins` folder. Copy your plugin to this directory.

Go one directory up and start the application `ModularToolManager.exe`. Open the `New Function` dialog `File->New Function`. If you click on the dropdown you should see your plugin with the provided display name.

![custom-plugin]

Select your plugin and [add a new function][add-a-function] to test it.

Next validate the meta information for your plugin, to do so go to `Help->Plugins`

![plugin-meta-information]

Check if everything is as expected. Well done you wrote your first plugin for the modular tool manager.

## How to continue

Use the [injectable services][injectable-services] to get some translation started or log errors for an easier debugging experience.

Define settings for your plugin to allow the user some customization. There is no manual for this right now!

[vscode]: https://code.visualstudio.com/
[nuget-package]: https://www.nuget.org/packages/ModularToolManagerPlugin/
[injectable-services]: ./injectable-services.md
[complete-class]: https://gist.github.com/XanatosX/34ac6a3e60c96250396f5565e7bab1d0
[custom-plugin]: https://i.imgur.com/9duZk1O.png
[add-a-function]: ../user/add-new-function.md
[plugin-meta-information]: https://i.imgur.com/OmCQX5b.png