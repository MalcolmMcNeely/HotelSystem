using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace HotelSystem.Infrastructure.CustomControls
{
    /// <summary>
    /// A columns that displays a DecimalTextBox.
    /// </summary>
    public class DataGridDecimalColumn : DataGridBoundColumn
    {
        static DataGridDecimalColumn()
        {
            ElementStyleProperty.OverrideMetadata(typeof(DataGridDecimalColumn), new FrameworkPropertyMetadata(DefaultElementStyle));
            EditingElementStyleProperty.OverrideMetadata(typeof(DataGridDecimalColumn), new FrameworkPropertyMetadata(DefaultEditingElementStyle));
        }

        #region Properties

        /// <summary>
        /// The binding that will be applied to the background property of the generated DecimalTextBox.
        /// </summary>
        /// <remarks>
        /// This isn't a DP because if it were getting the value would evaluate the binding.
        /// </remarks>
        public BindingBase BackgroundBinding
        {
            get { return _backgroundBinding; }
            set
            {
                if (_backgroundBinding != value)
                {
                    _backgroundBinding = value;
                    NotifyPropertyChanged(nameof(BackgroundBinding));
                }
            }
        }

        private BindingBase _backgroundBinding;

        #endregion

        #region Dependency Properties

        public bool ShowZeroValue
        {
            get { return (bool)GetValue(ShowZeroValueProperty); }
            set { SetValue(ShowZeroValueProperty, value); }
        }

        public static readonly DependencyProperty ShowZeroValueProperty =
            DependencyProperty.Register("ShowZeroValue", typeof(bool),
                typeof(DataGridDecimalColumn), new FrameworkPropertyMetadata(true));

        public string NumberFormat
        {
            get { return (string)GetValue(NumberFormatProperty); }
            set { SetValue(NumberFormatProperty, value); }
        }

        public static readonly DependencyProperty NumberFormatProperty =
            DependencyProperty.Register("NumberFormat", typeof(string),
                typeof(DataGridDecimalColumn), new FrameworkPropertyMetadata("N"));

        public int DecimalPlaces
        {
            get { return (int)GetValue(DecimalPlacesProperty); }
            set { SetValue(DecimalPlacesProperty, value); }
        }

        public static readonly DependencyProperty DecimalPlacesProperty =
            DependencyProperty.Register("DecimalPlaces", typeof(int),
                typeof(DataGridDecimalColumn), new PropertyMetadata(2));

        #endregion

        #region Styles

        private static Style _defaultElementStyle;
        /// <summary>
        ///     The default value of the ElementStyle property.
        ///     This value can be used as the BasedOn for new styles.
        /// </summary>
        public static Style DefaultElementStyle
        {
            get
            {
                if (_defaultElementStyle == null)
                {
                    // When not in edit mode, the end-user should not be able to toggle the state
                    Style style = new Style(typeof(DecimalTextBox));
                    style.Setters.Add(new Setter(DecimalTextBox.IsHitTestVisibleProperty, false));
                    style.Setters.Add(new Setter(DecimalTextBox.FocusableProperty, false));
                    style.Setters.Add(new Setter(DecimalTextBox.BorderThicknessProperty, new Thickness(0)));
                    style.Setters.Add(new Setter(DecimalTextBox.BackgroundProperty, Brushes.Transparent));
                    //style.Setters.Add(new Setter(DecimalTextBox.ShowZeroValueProperty, (bool)GetValue(ShowZeroValueProperty)));
                    style.Seal();

                    _defaultElementStyle = style;
                }

                return _defaultElementStyle;
            }
        }

        private static Style _defaultEditingElementStyle;
        /// <summary>
        ///     The default value of the EditingElementStyle property.
        ///     This value can be used as the BasedOn for new styles.
        /// </summary>
        public static Style DefaultEditingElementStyle
        {
            get
            {
                if (_defaultEditingElementStyle == null)
                {
                    Style style = new Style(typeof(DecimalTextBox));
                    style.Setters.Add(new Setter(DecimalTextBox.BorderThicknessProperty, new Thickness(0)));
                    style.Setters.Add(new Setter(DecimalTextBox.BackgroundProperty, Brushes.Transparent));
                    //style.Setters.Add(new Setter(DecimalTextBox.ShowZeroValueProperty, ShowZeroValue));

                    style.Seal();
                    _defaultEditingElementStyle = style;
                }

                return _defaultEditingElementStyle;
            }
        }

        /// <summary>
        /// Assigns the ElementStyle to the desired property on the given element.
        /// </summary>
        private void ApplyStyle(bool isEditing, bool defaultToElementStyle, FrameworkElement element)
        {
            Style style = PickStyle(isEditing, defaultToElementStyle);

            if (style != null)
            {
                element.Style = style;
            }
        }

        private Style PickStyle(bool isEditing, bool defaultToElementStyle)
        {
            Style style = isEditing ? EditingElementStyle : ElementStyle;

            if (isEditing && defaultToElementStyle && (style == null))
            {
                style = ElementStyle;
            }

            return style;
        }

        #endregion

        #region Element Generation

        /// <summary>
        /// Creates the visual tree for decimal based cells.
        /// </summary>
        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            return GenerateDecimalTextBox(false, cell);
        }

        /// <summary>
        /// Creates the visual tree for decimal based cells. 
        /// This happens every time a cell goes into edit mode.
        /// </summary>
        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            return GenerateDecimalTextBox(true, cell);
        }

        private DecimalTextBox GenerateDecimalTextBox(bool isEditing, DataGridCell cell)
        {
            DecimalTextBox decimalTextBox = (cell != null) ? (cell.Content as DecimalTextBox) : null;

            if (decimalTextBox == null)
            {
                decimalTextBox = new DecimalTextBox();
            }

            decimalTextBox.ShowZeroValue = ShowZeroValue;
            decimalTextBox.NumberFormat = NumberFormat;
            decimalTextBox.DecimalPlaces = DecimalPlaces;
            decimalTextBox.IsReadOnly = IsReadOnly;

            ApplyStyle(isEditing, true, decimalTextBox);
            ApplyBinding(Binding, decimalTextBox, DecimalTextBox.ValueProperty);
            ApplyBinding(BackgroundBinding, decimalTextBox, DecimalTextBox.BackgroundProperty);

            if (isEditing)
            {
                // If this decimal text box has been generated as a result
                // of going into edit mode, then focus it immediately.
                decimalTextBox.Focus();
            }

            return decimalTextBox;
        }

        /// <summary>
        /// Assigns the specified binding to the desired property on the target object.
        /// </summary>
        internal void ApplyBinding(BindingBase binding, DependencyObject target, DependencyProperty property)
        {
            if (binding != null)
            {
                BindingOperations.SetBinding(target, property, binding);
            }
            else
            {
                BindingOperations.ClearBinding(target, property);
            }
        }

        #endregion
    }
}
