# Modular Tool Manager

> :warning: This page is still WIP and will be updated slowly.

The Modular Tool Manager is a UI Tool for Linux and Windows allowing you to launch applications or scripts via an ui docked on your screen. It allows you to define buttons which do start those scripts or applications.

The applications or scripts which can be started are limited by plugins, see the list below which can be used on which platform.

## Plugins

| Name                                               | Platforms      | Application Types        | Dll Name                         | Delivered as Standard |
| -------------------------------------------------- | -------------- | ------------------------ | -------------------------------- | --------------------- |
| [Binary Execution Plugin][binary-execution-plugin] | Windows        | `exe`                    | [Default Plugin][default-plugin] | :heavy_check_mark:    |
| [Script Execution Plugin][script-execution-plugin] | Windows, Linux | `bat`, `cmd`, `ps`, `sh` | [Default Plugin][default-plugin] | :heavy_check_mark:    |


## Getting Started

- [User Manual][user-manual]
- [Developer Manual][developer-manual]


[default-plugin]: ./dlls/default-plugin.md
[binary-execution-plugin]: ./plugins/binary-execution-plugin.md
[script-execution-plugin]: ./plugins/script-execution-plugin.md
[user-manual]: ./user/user-manual.md
[developer-manual]: ./developer/developer-manual.md
