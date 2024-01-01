# Plugin translation service

This service does allow you to get translated text's for your plugin. To use this functionality you need to structure the plugin in a specific way. The interfaces does look like this.

```csharp
public interface IPluginLoggerService
{
    CultureInfo GetFallbackLanguage();
    List<string> GetLanguages();
    List<TranslationModel> GetAllTranslations();
    List<TranslationModel> GetAllTranslations(Assembly assemblyToUse);
    string? GetTranslationByKey(Assembly assembly, string key);
    List<string> GetKeys();
    List<string> GetKeys(Assembly assembly);
}
```
>See the [interface][interface-class] for more information

## How to use it


### Preparation  

This plugin does need some setup. First of all you will need a json file inside of your plugin. Those file need to be placed inside of a `Translations` folder. The json files should be named like `de-DE.json` and `en-EN.json`.

This should result in something like this

```
PluginRoot/
├─ Translations/
│  ├─ de-DE.json
│  ├─ en-EN.json
PluginProjectFile.csproj
```
> Generated with [ASCII Tree Generator][ascii-tree-generator]

The json files are structured like this.

```json
[
    {
        "key": "item-key",
        "value": "translation for item"
    },
    {
        "key": "item-key-2",
        "value": "translation for item 2"
    }
]
```
>Take a look at the default plugin translation as an [example][json-example]

Add the following configuration to the csproj file of your plugin:

```xml
  <ItemGroup>
    <None Remove="Translations\de-DE.json" />
    <None Remove="Translations\en-EN.json" />
  </ItemGroup>

  <ItemGroup>
    <EmbeddedResource Include="Translations\de-DE.json" />
    <EmbeddedResource Include="Translations\en-EN.json" />
  </ItemGroup>
```

### Usage

After [injecting the service][how-to-inject-a-service] you can use it like this.

>:information_source: Service was injected as `translationService`

```csharp
translationService?.GetTranslationByKey("item-key") ?? "My fallback value if something went wrong";
```

This should return you the string "translation for item" as defined in our translation json.


## Other services

Also check out the other [injectable services][injectabe-services]

[injectabe-services]: ./injectable-services.md
[how-to-inject-a-service]: ./injectable-services.md#how-to-inject-a-service
[interface-class]: https://github.com/XanatosX/ModularToolManager/blob/main/src/ModularToolManagerPlugin/Services/IPluginTranslationService.cs
[ascii-tree-generator]: https://ascii-tree-generator.com/
[json-example]: https://github.com/XanatosX/ModularToolManager/blob/main/src/DefaultPlugins/Translations/de-DE.json