using System.Collections.Generic;
using Windows.Foundation.Collections;
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

        #region EmptyDataControlTemplate
        private static Dictionary<int, ControlTemplate> _originalTemplates = new Dictionary<int, ControlTemplate>();

        /// <summary>
        /// Attached <see cref="DependencyProperty"/> for binding a <see cref="DataTemplate"/> as an alternate row template to a <see cref="ListViewBase"/>
        /// </summary>
        public static readonly DependencyProperty EmptyDataControlTemplateProperty = DependencyProperty.RegisterAttached(
            "EmptyDataControlTemplate",
            typeof(ControlTemplate),
            typeof(ListViewBaseExtensions),
            new PropertyMetadata(null, OnEmptyDataControlTemplatePropertyChanged));

        /// <summary>
        /// Gets the <see cref="ControlTemplate"/> associated with the specified <see cref="ListViewBase"/>
        /// </summary>
        /// <param name="obj">The <see cref="ListViewBase"/> to get the associated <see cref="ControlTemplate"/> from</param>
        /// <returns>The <see cref="ControlTemplate"/> associated with the <see cref="ListViewBase"/></returns>
        public static ControlTemplate GetEmptyDataControlTemplate(ListViewBase obj)
        {
            return (ControlTemplate)obj.GetValue(EmptyDataControlTemplateProperty);
        }

        /// <summary>
        /// Sets the <see cref="ControlTemplate"/> associated with the specified <see cref="ListViewBase"/>
        /// </summary>
        /// <param name="obj">The <see cref="ListViewBase"/> to associate the <see cref="ControlTemplate"/> with</param>
        /// <param name="value">The <see cref="ControlTemplate"/> for binding to the <see cref="ListViewBase"/></param>
        public static void SetEmptyDataControlTemplate(ListViewBase obj, ControlTemplate value)
        {
            obj.SetValue(EmptyDataControlTemplateProperty, value);
        }

        private static void OnEmptyDataControlTemplatePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            ListViewBase listViewBase = sender as ListViewBase;

            if (listViewBase == null)
            {
                return;
            }

            if (EmptyDataControlTemplateProperty != null)
            {
                var listViewIdentifier = listViewBase.GetHashCode();

                listViewBase.Items.VectorChanged += (s, e) =>
                {
                    if (_originalTemplates.ContainsKey(listViewIdentifier) == false)
                    {
                        _originalTemplates.Add(listViewIdentifier, listViewBase.Template);
                    }

                    SetProperTemplate(listViewBase, s.Count, e.CollectionChange);
                };
            }
        }

        private static void SetProperTemplate(ListViewBase listViewBase, int count, CollectionChange collectionChange = CollectionChange.Reset)
        {
            if (count == 0)
            {
                SetEmptyDataTemplate(listViewBase);
            }
            else
            {
                SetOriginalControlTemplate(listViewBase, collectionChange);
            }
        }

        private static void SetEmptyDataTemplate(ListViewBase listViewBase)
        {
            var emptyTemplate = GetEmptyDataControlTemplate(listViewBase);

            if (listViewBase.Template == emptyTemplate)
            {
                return;
            }

            listViewBase.Template = emptyTemplate;
        }

        private static void SetOriginalControlTemplate(ListViewBase listViewBase, CollectionChange collectionChange)
        {
            var listViewIdentifier = listViewBase.GetHashCode();

            if (_originalTemplates.ContainsKey(listViewIdentifier) == false)
            {
                return;
            }

            if (collectionChange != CollectionChange.Reset && listViewBase.Template != _originalTemplates[listViewIdentifier])
            {
                listViewBase.Template = _originalTemplates[listViewIdentifier];
            }
        }
        #endregion
    }
}

