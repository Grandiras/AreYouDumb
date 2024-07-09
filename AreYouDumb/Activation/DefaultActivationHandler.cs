using AreYouDumb.Contracts.Services;
using AreYouDumb.ViewModels;

using Microsoft.UI.Xaml;

namespace AreYouDumb.Activation;

public class DefaultActivationHandler : ActivationHandler<LaunchActivatedEventArgs>
{
    private readonly INavigationService _navigationService;

    public DefaultActivationHandler(INavigationService navigationService) => _navigationService = navigationService;

    protected override bool CanHandleInternal(LaunchActivatedEventArgs args) =>
        // None of the ActivationHandlers has handled the activation.
        _navigationService.Frame?.Content == null;

    protected override async Task HandleInternalAsync(LaunchActivatedEventArgs args)
    {
        _ = _navigationService.NavigateTo(typeof(MainViewModel).FullName!, args.Arguments);

        await Task.CompletedTask;
    }
}
