using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiTestApp;

public partial class RegularPage : ContentPage
{
    public RegularPage()
    {
        InitializeComponent();
    }

    private void Button_OnClicked(object? sender, EventArgs e)
    {
        Navigation.PushAsync(new RegularPage2());
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();

        if (Handler is null)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await Task.Delay(1000);    
                _ = Shell.Current.DisplayAlert("Handler disconnected",
                    "The handler for root page has been disconnected.", "OK");
            });
        }
    }
}