using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiTestApp;

public partial class CollectionView2Bug : ContentPage
{
    private List<string> m_items =
    [
        "Item 1",
        "Item 2",
        "Item 3",
        "Item 4",
        "Item 5",
        "Item 6",
        "Item 7",
        "Item 8",
        "Item 9",
        "Item 10",
        "Item 11",
        "Item 12",
        "Item 13",
        "Item 14",
        "Item 15",
        "Item 16",
    ];

    public CollectionView2Bug()
    {
        InitializeComponent();
    }

    public List<string> Items
    {
        get => m_items;
        set
        {
            if (Equals(value, m_items)) return;
            m_items = value;
            OnPropertyChanged();
        }
    }

    private void Button_OnClicked(object? sender, EventArgs e)
    {
        var reverseItems = new List<string>(Items);
        reverseItems.Reverse();
        Items = new List<string>(reverseItems);
    }
}