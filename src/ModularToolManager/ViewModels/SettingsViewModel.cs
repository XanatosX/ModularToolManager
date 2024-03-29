﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using ModularToolManager.Models;
using ModularToolManager.Models.Messages;
using ModularToolManager.Services.Settings;
using ModularToolManager.Services.Ui;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ModularToolManager.ViewModels;

/// <summary>
/// View model for the settings of the application
/// </summary>
internal partial class SettingsViewModel : ObservableObject
{
    /// <summary>
    /// The settings service to use
    /// </summary>
    private readonly ISettingsService settingsService;

    /// <summary>
    /// Baking field for the top most checkbox
    /// </summary>
    [ObservableProperty]
    private bool topMost;

    /// <summary>
    /// Baking field if the app should minimize if a function was pressed
    /// </summary>
    [ObservableProperty]
    private bool closeOnFunctionExecute;

    /// <summary>
    /// Baking field if the search should be cleard if a function was executed
    /// </summary>
    [ObservableProperty]
    private bool clearSearchAfterFunctionExecute;

    /// <summary>
    /// Baking field if should be started minimitzed
    /// </summary>
    [ObservableProperty]
    private bool startMinimized;

    /// <summary>
    /// Baking field if should be shown in taskbar
    /// </summary>
    [ObservableProperty]
    private bool showInTaskbar;

    /// <summary>
    /// All the available themes for the application
    /// </summary>
    [ObservableProperty]
    private List<ApplicationStyleViewModel> availableThemes;

    /// <summary>
    /// The currently selected theme
    /// </summary>
    [ObservableProperty]
    private ApplicationStyleViewModel? selectedTheme;

    /// <summary>
    /// Enable autocomplete for the function search
    /// </summary>
    [ObservableProperty]
    private bool enableAutocompleteForFunctionSearch;

    /// <summary>
    /// Create a new instance of this class
    /// </summary>
    /// <param name="settingsService">The settings service to use</param>
    public SettingsViewModel(ISettingsService settingsService, IThemeService themeService)
    {
        this.settingsService = settingsService;
        ApplicationSettings appSettings = settingsService.GetApplicationSettings();
        TopMost = appSettings.AlwaysOnTop;
        CloseOnFunctionExecute = appSettings.MinimizeOnFunctionExecute;
        StartMinimized = appSettings.StartMinimized;
        ClearSearchAfterFunctionExecute = appSettings.ClearSearchAfterFunctionExecute;
        ShowInTaskbar = appSettings.ShowInTaskbar;
        AvailableThemes = themeService.GetAllStyles()
                                .OrderBy(style => style.Name)
                                .Where(style => !string.IsNullOrEmpty(style.Name))
                                .Select(style => new ApplicationStyleViewModel(style))
                                .ToList();
        SelectedTheme = AvailableThemes.Where(theme => theme.Id == appSettings.SelectedThemeId).FirstOrDefault() ?? AvailableThemes.FirstOrDefault();
        EnableAutocompleteForFunctionSearch = appSettings.EnableAutocompleteForFunctionSearch;

        PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == nameof(SelectedTheme) && selectedTheme is not null)
            {
                WeakReferenceMessenger.Default.Send(new ApplicationThemeUpdated(selectedTheme.Id));
            }
        };
    }

    /// <summary>
    /// The ok button to save and confirm the changes
    /// </summary>
    [RelayCommand]
    private void Ok()
    {
        var changeResult = settingsService.ChangeSettings(settings =>
        {
            settings.StartMinimized = StartMinimized;
            settings.ShowInTaskbar = ShowInTaskbar;
            settings.AlwaysOnTop = TopMost;
            settings.MinimizeOnFunctionExecute = CloseOnFunctionExecute;
            settings.ClearSearchAfterFunctionExecute = ClearSearchAfterFunctionExecute;
            settings.SelectedThemeId = SelectedTheme?.Id ?? 0;
            settings.EnableAutocompleteForFunctionSearch = EnableAutocompleteForFunctionSearch;
        });
        if (changeResult)
        {
            WeakReferenceMessenger.Default.Send(new ValueChangedMessage<ApplicationSettings>(settingsService.GetApplicationSettings()));
            Abort();
        }
    }

    /// <summary>
    /// The abort button to discard the changes and close the modal
    /// </summary>
    [RelayCommand]
    private void Abort()
    {
        WeakReferenceMessenger.Default.Send(new CloseModalMessage(this));
        AvailableThemes.Clear();
        SelectedTheme = null;
    }
}
