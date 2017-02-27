using System.Collections.Generic;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace EvD.Uwp.UI.Extensions
{
    public static class ItemsControlExtensions
    {
        #region EmptyDataControlTemplate
        private static Dictionary<int, ControlTemplate> _originalTemplates = new Dictionary<int, ControlTemplate>();

        /// <summary>
        /// Attached <see cref="DependencyProperty"/> for binding a <see cref="ControlTemplate"/> as an alternate row template to a <see cref="ItemsControl"/>
        /// </summary>
        public static readonly DependencyProperty EmptyDataControlTemplateProperty = DependencyProperty.RegisterAttached(
            "EmptyDataControlTemplate",
            typeof(ControlTemplate),
            typeof(ItemsControlExtensions),
            new PropertyMetadata(null, OnEmptyDataControlTemplatePropertyChanged));

        /// <summary>
        /// Gets the <see cref="ControlTemplate"/> associated with the specified <see cref="ItemsControl"/>
        /// </summary>
        /// <param name="obj">The <see cref="ItemsControl"/> to get the associated <see cref="ControlTemplate"/> from</param>
        /// <returns>The <see cref="ControlTemplate"/> associated with the <see cref="ItemsControl"/></returns>
        public static ControlTemplate GetEmptyDataControlTemplate(ItemsControl obj)
        {
            return (ControlTemplate)obj.GetValue(EmptyDataControlTemplateProperty);
        }

        /// <summary>
        /// Sets the <see cref="ControlTemplate"/> associated with the specified <see cref="ItemsControl"/>
        /// </summary>
        /// <param name="obj">The <see cref="ListViewBase"/> to associate the <see cref="ControlTemplate"/> with</param>
        /// <param name="value">The <see cref="ControlTemplate"/> for binding to the <see cref="ItemsControl"/></param>
        public static void SetEmptyDataControlTemplate(ItemsControl obj, ControlTemplate value)
        {
            obj.SetValue(EmptyDataControlTemplateProperty, value);
        }

        private static void OnEmptyDataControlTemplatePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            ItemsControl itemsControl = sender as ItemsControl;

            if (itemsControl == null)
            {
                return;
            }

            if (EmptyDataControlTemplateProperty != null)
            {
                var listViewIdentifier = itemsControl.GetHashCode();

                itemsControl.Items.VectorChanged += (s, e) =>
                {
                    if (_originalTemplates.ContainsKey(listViewIdentifier) == false)
                    {
                        _originalTemplates.Add(listViewIdentifier, itemsControl.Template);
                    }

                    SetProperTemplate(itemsControl, s.Count, e.CollectionChange);
                };
            }
        }

        private static void SetProperTemplate(ItemsControl itemsControl, int count, CollectionChange collectionChange = CollectionChange.Reset)
        {
            if (count == 0)
            {
                SetEmptyDataTemplate(itemsControl);
            }
            else
            {
                SetOriginalControlTemplate(itemsControl, collectionChange);
            }
        }

        private static void SetEmptyDataTemplate(ItemsControl itemsControl)
        {
            var emptyTemplate = GetEmptyDataControlTemplate(itemsControl);

            if (itemsControl.Template == emptyTemplate)
            {
                return;
            }

            itemsControl.Template = emptyTemplate;
        }

        private static void SetOriginalControlTemplate(ItemsControl itemsControl, CollectionChange collectionChange)
        {
            var itemsControlIdentifier = itemsControl.GetHashCode();

            if (_originalTemplates.ContainsKey(itemsControlIdentifier) == false)
            {
                return;
            }

            if (collectionChange != CollectionChange.Reset && itemsControl.Template != _originalTemplates[itemsControlIdentifier])
            {
                itemsControl.Template = _originalTemplates[itemsControlIdentifier];
            }
        }
        #endregion
    }
}
