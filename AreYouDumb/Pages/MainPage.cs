using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media.Animation;
using WinSharp.BindingExtensions;
using WinSharp.Styles;
using Grid = WinSharp.Controls.Grid;

namespace AreYouDumb.Pages;
internal sealed class MainPage : Page
{
    private readonly Button NoButton;


    public MainPage() => Content = new Grid
    {
        new TextBlock
        {
            Text = AreYouDumb.Resources.MainPage_Heading,
            HorizontalAlignment = HorizontalAlignment.Center,
            FontSize = 36,
            FontWeight = FontWeights.Bold,
        },

        new Button
        {
            Content = AreYouDumb.Resources.MainPage_Yes,
            Width = 200,
            Height = 40,
            Margin = new(30, 0, 0, 10),
            VerticalAlignment = VerticalAlignment.Bottom,
        }
        .OnClick(async () =>
        {
            var dialog = new ContentDialog
            {
                XamlRoot = XamlRoot,
                Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style,
                Title = AreYouDumb.Resources.MainPage_YesDialog_Title,
                Content = AreYouDumb.Resources.MainPage_YesDialog_Content,
                PrimaryButtonText = AreYouDumb.Resources.MainPage_YesDialog_Button,
                DefaultButton = ContentDialogButton.Primary,
            };
            _ = await dialog.ShowAsync();
        }),

        new Button
        {
            Content = AreYouDumb.Resources.MainPage_No,
            Width = 200,
            Height = 40,
            Margin = new(0, 0, 30, 10),
            VerticalAlignment = VerticalAlignment.Bottom,
            HorizontalAlignment = HorizontalAlignment.Right,
            TranslationTransition = new Vector3Transition { Duration = TimeSpan.FromMilliseconds(200)  }
        }
        .BindSelf(out NoButton)
        .SetCustomEventHandler<Button, PointerEventHandler>("PointerEntered", (object sender, PointerRoutedEventArgs e) =>
        {
            var x = -Random.Shared.Next(0, 16) * 10;
            var y = -Random.Shared.Next(0, 20) * 10;

            if (e.GetCurrentPoint(NoButton).Position.Y - NoButton.Translation.Y + y is >= -100 and <= 100) y -= 100;

            NoButton.Translation = new(x, y, 0);
        })
    }.SetProperties(grid => grid.Transitions = [new EntranceThemeTransition { FromVerticalOffset = 50 }]);
}
