# Injectable Services

There are multiple service you can inject inside of your plugin. Those are listed and explained in this document.

## How to inject a service

To inject the services inside of your plugin just add the interfaces as arguments to your constructor.
This behavior is identical to normal dependency injection, keep in mind only the available services down bellow will work.

`public MyPluginClass(IPluginTranslationService translationService, IPluginLoggerService<MyPluginClass> loggingService)`

The matching objects should be injected.

## Available services

The following services can be used inside of your plugin.

- [IPluginLoggerService][plugin-logger-service]
- [IPluginTranslationService][plugin-translation-service]

## Developer Manual

Go [back][developer-manuel] to developer manual

[developer-manuel]: ./developer-manual.md
[plugin-logger-service]: ./plugin-logger-service.md
[plugin-translation-service]: ./plugin-translation-service.md
