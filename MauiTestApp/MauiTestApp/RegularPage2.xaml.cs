using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiTestApp;

public partial class RegularPage2 : ContentPage
{
    public RegularPage2()
    {
        InitializeComponent();
    }

    private void Button_OnClicked(object? sender, EventArgs e)
    {
        var navigationPage =
            Shell.Current.Navigation.ModalStack.FirstOrDefault(p => p is NavigationPage) as
                NavigationPage;
        var stack = navigationPage.Navigation.NavigationStack;
        var pages = stack.Take(stack.Count - 1).ToList();
        foreach (var page in pages)
        {
            navigationPage.Navigation.RemovePage(page); // remove all pages except the one currently showing
        }
        Navigation.PopModalAsync();
    }

    private void Button_OnClicked2(object? sender, EventArgs e)
    {
        var pages = Navigation.NavigationStack.Take(Navigation.NavigationStack.Count - 1).ToList();
        foreach (var page in pages)
        {
            Navigation.RemovePage(page); // remove all pages except the one currently showing
        }
        Navigation.PopModalAsync();
    }
}