# Script execution plugin

This plugin does allow you to run simple script for your operation system. Those scripts do represent the default scripting languages for the given system.

This plugin is part of the [Default Plugin][default-plugin] dll.

## File types and availability

| Extension | Windows            | Linux              |
| --------- | ------------------ | ------------------ |
| `bat`     | :heavy_check_mark: | :x:                |
| `cmd`     | :heavy_check_mark: | :x:                |
| `ps`      | :heavy_check_mark: | :x:                |
| `sh`      | :x:                | :heavy_check_mark: |

## Settings

To get more information about the scopes of a setting read the [scope of a setting][scope-of-a-setting] documentation.

| Name          | Scope  | Description                                                   |
| ------------- | ------ | ------------------------------------------------------------- |
| Hide Terminal | Global | Does hide the terminal where the script is getting started in |

[default-plugin]: ../dlls/default-plugin.md
[scope-of-a-setting]: ../user/scope-of-a-setting.md