using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace EvD.Uwp.UI.Extensions
{
    public static class ListViewBaseExtensions
    {
        #region AlternateRowColor
        /// <summary>
        /// Attached <see cref="DependencyProperty"/> for binding a <see cref="Brush"/> as an alternate row background color to a <see cref="ListViewBase"/>
        /// </summary>
        public static readonly DependencyProperty AlternateRowColorProperty = DependencyProperty.RegisterAttached(
            "AlternateRowColor",
            typeof(Brush),
            typeof(ListViewBaseExtensions),
            new PropertyMetadata(null, OnAlternateRowColorPropertyChanged));

        /// <summary>
        /// Gets the <see cref="Brush"/> associated with the specified <see cref="ListViewBase"/>
        /// </summary>
        /// <param name="obj">The <see cref="ListViewBase"/> to get the associated <see cref="Brush"/> from</param>
        /// <returns>The <see cref="Brush"/> associated with the <see cref="ListViewBase"/></returns>
        public static Brush GetAlternateRowColor(ListViewBase obj)
        {
            return (Brush)obj.GetValue(AlternateRowColorProperty);
        }

        /// <summary>
        /// Sets the <see cref="Brush"/> associated with the specified <see cref="DependencyObject"/>
        /// </summary>
        /// <param name="obj">The <see cref="ListViewBase"/> to associate the <see cref="Brush"/> with</param>
        /// <param name="value">The <see cref="Brush"/> for binding to the <see cref="ListViewBase"/></param>
        public static void SetAlternateRowColor(ListViewBase obj, Brush value)
        {
            obj.SetValue(AlternateRowColorProperty, value);
        }

        private static void OnAlternateRowColorPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            ListViewBase listViewBase = sender as ListViewBase;

            if (listViewBase == null)
            {
                return;
            }

            listViewBase.ContainerContentChanging -= ColorContainerContentChanging;

            if (AlternateRowColorProperty != null)
            {
                listViewBase.ContainerContentChanging += ColorContainerContentChanging;
            }
        }

        private static void ColorContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            var itemContainer = args.ItemContainer as ListViewItem;
            var itemIndex = sender.IndexFromContainer(itemContainer);

            if (itemIndex % 2 == 0)
            {
                itemContainer.Background = GetAlternateRowColor(sender);
            }
            else
            {
                itemContainer.Background = null;
            }
        }
        #endregion

        #region AlternateRowItemTemplate
        /// <summary>
        /// Attached <see cref="DependencyProperty"/> for binding a <see cref="DataTemplate"/> as an alternate row template to a <see cref="ListViewBase"/>
        /// </summary>
        public static readonly DependencyProperty AlternateRowItemTemplateProperty = DependencyProperty.RegisterAttached(
            "AlternateRowItemTemplate",
            typeof(DataTemplate),
            typeof(ListViewBaseExtensions),
            new PropertyMetadata(null, OnAlternateRowItemTemplatePropertyChanged));

        /// <summary>
        /// Gets the <see cref="DataTemplate"/> associated with the specified <see cref="ListViewBase"/>
        /// </summary>
        /// <param name="obj">The <see cref="ListViewBase"/> to get the associated <see cref="DataTemplate"/> from</param>
        /// <returns>The <see cref="DataTemplate"/> associated with the <see cref="ListViewBase"/></returns>
        public static DataTemplate GetAlternateRowItemTemplate(ListViewBase obj)
        {
            return (DataTemplate)obj.GetValue(AlternateRowItemTemplateProperty);
        }

        /// <summary>
        /// Sets the <see cref="DataTemplate"/> associated with the specified <see cref="ListViewBase"/>
        /// </summary>
        /// <param name="obj">The <see cref="ListViewBase"/> to associate the <see cref="DataTemplate"/> with</param>
        /// <param name="value">The <see cref="DataTemplate"/> for binding to the <see cref="ListViewBase"/></param>
        public static void SetAlternateRowItemTemplate(ListViewBase obj, DataTemplate value)
        {
            obj.SetValue(AlternateRowItemTemplateProperty, value);
        }

        private static void OnAlternateRowItemTemplatePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            ListViewBase listViewBase = sender as ListViewBase;

            if (listViewBase == null)
            {
                return;
            }

            listViewBase.ContainerContentChanging -= ItemTemplateContainerContentChanging;

            if (AlternateRowItemTemplateProperty != null)
            {
                listViewBase.ContainerContentChanging += ItemTemplateContainerContentChanging;
            }
        }

        private static void ItemTemplateContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            var itemContainer = args.ItemContainer as ListViewItem;
            var itemIndex = sender.IndexFromContainer(itemContainer);

            if (itemIndex % 2 == 0)
            {
                itemContainer.ContentTemplate = GetAlternateRowItemTemplate(sender);
            }
            else
            {
                itemContainer.ContentTemplate = sender.ItemTemplate;
            }
        }
        #endregion
    }
}

