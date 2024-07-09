using AreYouDumb.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;

namespace AreYouDumb.Views;

public sealed partial class MainPage : Page
{
    private readonly Random Random = new();

    public MainViewModel ViewModel
    {
        get;
    }

    public MainPage()
    {
        ViewModel = App.GetService<MainViewModel>();
        InitializeComponent();
    }

    private async void Yes_ClickAsync(object sender, RoutedEventArgs e)
    {
        var dialog = new ContentDialog
        {
            XamlRoot = XamlRoot,
            Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style,
            Title = "You are dumb",
            Content = "I knew it :)",
            PrimaryButtonText = "Okay",
            DefaultButton = ContentDialogButton.Primary,
        };
        _ = await dialog.ShowAsync();
    }

    private void NotDumb_PointerEntered(object sender, PointerRoutedEventArgs e)
    {
        var x = -Random.Next(0, 16) * 10;
        var y = -Random.Next(0, 20) * 10;

        if (e.GetIntermediatePoints(NotDumb).Last().Position.Y - NotDumb.Translation.Y + y is >= -100 and <= 100) y -= 100;

        NotDumb.Translation = new(x, y, 0);
    }
}
