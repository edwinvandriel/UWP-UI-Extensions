using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;

namespace EvD.Uwp.UI.Extensions
{
    public static class UIElementExtensions
    {
        #region ShowAccessKeyHint
        public static readonly DependencyProperty ShowAccessKeyHintProperty = DependencyProperty.RegisterAttached(
            "ShowAccessKeyHint",
            typeof(bool),
            typeof(UIElementExtensions),
            new PropertyMetadata(null, OnShowAccessKeyHintPropertyChanged));

        public static bool GetShowAccessKeyHint(UIElement obj)
        {
            return (bool)obj.GetValue(ShowAccessKeyHintProperty);
        }

        public static void SetShowAccessKeyHint(UIElement obj, bool value)
        {
            obj.SetValue(ShowAccessKeyHintProperty, value);
        }

        private static void OnShowAccessKeyHintPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            UIElement element = sender as UIElement;

            if (element == null)
            {
                return;
            }

            element.AccessKeyDisplayDismissed -= Element_AccessKeyDisplayDismissed;
            element.AccessKeyDisplayRequested -= Element_AccessKeyDisplayRequested;

            if (ShowAccessKeyHintProperty != null)
            {
                element.AccessKeyDisplayDismissed += Element_AccessKeyDisplayDismissed;
                element.AccessKeyDisplayRequested += Element_AccessKeyDisplayRequested;
            }
        }

        private static void Element_AccessKeyDisplayRequested(UIElement sender, Windows.UI.Xaml.Input.AccessKeyDisplayRequestedEventArgs args)
        {
            var tooltip = ToolTipService.GetToolTip(sender) as ToolTip;

            if (tooltip == null)
            {
                tooltip = new ToolTip()
                {
                    Background = new SolidColorBrush(Windows.UI.Colors.Black),
                    Foreground = new SolidColorBrush(Windows.UI.Colors.White),
                    Padding = new Thickness(4, 4, 4, 4),
                    VerticalOffset = -20,
                    Placement = PlacementMode.Bottom
                };
                ToolTipService.SetToolTip(sender, tooltip);
            }

            if (string.IsNullOrEmpty(args.PressedKeys))
            {
                tooltip.Content = sender.AccessKey;
            }
            else
            {
                tooltip.Content = sender.AccessKey.Remove(0, args.PressedKeys.Length);
            }

            tooltip.IsOpen = true;
        }

        private static void Element_AccessKeyDisplayDismissed(UIElement sender, Windows.UI.Xaml.Input.AccessKeyDisplayDismissedEventArgs args)
        {
            var tooltip = ToolTipService.GetToolTip(sender) as ToolTip;
            if (tooltip != null)
            {
                tooltip.IsOpen = false;
                //Fix to avoid show tooltip with mouse
                ToolTipService.SetToolTip(sender, null);
            }
        }
        #endregion
    }
}
