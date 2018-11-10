using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace HotelSystem.Infrastructure.WPF.CustomControls
{
    public class DataGridIntegerColumn : DataGridBoundColumn
    {
        static DataGridIntegerColumn()
        {
            ElementStyleProperty.OverrideMetadata(typeof(DataGridIntegerColumn), new FrameworkPropertyMetadata(DefaultElementStyle));
            EditingElementStyleProperty.OverrideMetadata(typeof(DataGridIntegerColumn), new FrameworkPropertyMetadata(DefaultEditingElementStyle));
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

        public Brush Background
        {
            get { return (Brush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        public static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register("Background", typeof(Brush),
                typeof(DataGridIntegerColumn), new FrameworkPropertyMetadata(Brushes.Transparent));

        public bool ShowZeroValue
        {
            get { return (bool)GetValue(ShowZeroValueProperty); }
            set { SetValue(ShowZeroValueProperty, value); }
        }

        public static readonly DependencyProperty ShowZeroValueProperty =
            DependencyProperty.Register("ShowZeroValue", typeof(bool),
                typeof(DataGridIntegerColumn), new FrameworkPropertyMetadata(true));

        public string NumberFormat
        {
            get { return (string)GetValue(NumberFormatProperty); }
            set { SetValue(NumberFormatProperty, value); }
        }

        public static readonly DependencyProperty NumberFormatProperty =
            DependencyProperty.Register("NumberFormat", typeof(string),
                typeof(DataGridIntegerColumn), new FrameworkPropertyMetadata("N"));

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
                    Style style = new Style(typeof(IntegerTextBox));
                    style.Setters.Add(new Setter(IntegerTextBox.IsHitTestVisibleProperty, false));
                    style.Setters.Add(new Setter(IntegerTextBox.FocusableProperty, false));
                    style.Setters.Add(new Setter(IntegerTextBox.BorderThicknessProperty, new Thickness(0)));
                    style.Setters.Add(new Setter(IntegerTextBox.BackgroundProperty, Brushes.Transparent));
                    //style.Setters.Add(new Setter(IntegerTextBox.ShowZeroValueProperty, true));

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
                    Style style = new Style(typeof(IntegerTextBox));
                    style.Setters.Add(new Setter(IntegerTextBox.BorderThicknessProperty, new Thickness(0)));
                    style.Setters.Add(new Setter(IntegerTextBox.BackgroundProperty, Brushes.Transparent));
                    style.Setters.Add(new Setter(IntegerTextBox.ShowZeroValueProperty, true));
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
            return GenerateIntegerTextBox(false, cell);
        }

        /// <summary>
        /// Creates the visual tree for decimal based cells. 
        /// This happens every time a cell goes into edit mode.
        /// </summary>
        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            return GenerateIntegerTextBox(true, cell);
        }

        private IntegerTextBox GenerateIntegerTextBox(bool isEditing, DataGridCell cell)
        {
            IntegerTextBox integerTextBox = (cell != null) ? (cell.Content as IntegerTextBox) : null;

            if (integerTextBox == null)
            {
                integerTextBox = new IntegerTextBox();
            }

            integerTextBox.ShowZeroValue = ShowZeroValue;
            integerTextBox.NumberFormat = NumberFormat;

            ApplyStyle(isEditing, true, integerTextBox);
            ApplyBinding(Binding, integerTextBox, IntegerTextBox.ValueProperty);
            ApplyBinding(BackgroundBinding, integerTextBox, IntegerTextBox.BackgroundProperty);

            if (isEditing)
            {
                // If this decimal text box has been generated as a result
                // of going into edit mode, then focus it immediately.
                integerTextBox.Focus();
            }

            return integerTextBox;
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
