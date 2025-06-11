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
        var pageToRemove = Navigation.NavigationStack.FirstOrDefault();
        if(pageToRemove is null) // Root page is null for some reason in shell
            pageToRemove = Navigation.NavigationStack[1];
        Navigation.RemovePage(pageToRemove);
    }

    private void Button_OnClicked2(object? sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }
}