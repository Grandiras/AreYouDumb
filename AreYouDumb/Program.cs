using AreYouDumb;
using AreYouDumb.Pages;
using Microsoft.UI.Xaml.Controls;
using System.Diagnostics;
using WinSharp;
using WinSharp.Pages;
using WinSharp.Pages.Components.Settings;
using WinSharp.Services;
using WinSharp.Windows;

new AppBuilder()

.AddNavigationWindow(window => _ = window
    .AddMenuPage<MainPage>(Resources.MainPage_Title, Symbol.Home, false)
    .AddFooterPage<SettingsPage>(Resources.SettingsPage_Title, Symbol.Setting, true))

.Configure<TitleBar>(titleBar => titleBar.Title = Resources.Title)

.AddTransient<MainPage>()
.Configure<SettingsPage>(settings => settings
    .AddComponent<ThemeSelector>()
    .AddComponent<AboutSection>())

.Configure<AboutSection>(section =>
{
    section.AppName = Resources.Title;
    section.Publisher = "Darkymos";
    section.Version = "1.0.0.0";

    section.Links.Add(new("Repository", "https://github.com/Grandiras/AreYouDumb"));
})

.Configure<EventBinding>(events => events.ExceptionThrown += (sender, e) => Debugger.Break())
.Configure<LocalizationService>(localization => localization.ResourceManager = Resources.ResourceManager)

.Build();