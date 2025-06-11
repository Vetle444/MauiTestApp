using System.Windows.Input;

namespace MauiTestApp;

public class CommandExtension : IMarkupExtension<ICommand>
{
    public Type? ContentPageType { get; set; }

    public ICommand ProvideValue(IServiceProvider serviceProvider)
    {
        return new Command(() =>
        {
            if (ContentPageType == null)
            {
                return;
            }

            var activatedObject = Activator.CreateInstance(ContentPageType);
            if (activatedObject is not Page page) return;
            Shell.Current.Navigation.PushModalAsync(new NavigationPage(page));
        });
    }

    object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
    {
        return ProvideValue(serviceProvider);
    }
}