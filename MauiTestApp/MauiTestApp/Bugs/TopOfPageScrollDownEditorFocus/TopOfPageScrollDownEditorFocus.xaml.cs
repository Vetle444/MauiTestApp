using Microsoft.Maui.Platform;
using UIKit;

namespace MauiTestApp.Bugs.TopOfPageScrollDownEditorFocus;

public partial class TopOfPageScrollDownEditorFocus 
{
    public TopOfPageScrollDownEditorFocus()
    {
        InitializeComponent();
    }

    private void Element_OnHandlerChanged(object? sender, EventArgs e)
    {
        if (sender is not Editor editor) 
            return;
#if __IOS__
        if (editor.Handler?.PlatformView is MauiTextView mauiTextView)
        {
            mauiTextView.TextContainerInset = UIEdgeInsets.Zero;
        }
#endif
    }
}