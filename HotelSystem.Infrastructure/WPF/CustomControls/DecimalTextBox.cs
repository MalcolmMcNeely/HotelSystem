using HotelSystem.Infrastructure.Helpers;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;


namespace HotelSystem.Infrastructure.WPF.CustomControls
{
    /// <summary>
    /// A textbox exclusively purposed for handling decimal numbers with any number
    /// of decimal points. By default it is configured to work with 2 decimal points
    /// and will hide its text when its Value is 0. NumberFormat can be specified with
    /// a .NET standard numeric format string including percentages:
    /// https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings
    /// </summary>
    public class DecimalTextBox : TextBox
    {
        #region Static

        // Most Decimals are stored as DECIMAL(9,2) in the Database
        public static readonly decimal DefaultMaxValue = 9999999.99M;
        public static readonly decimal DefaultMinValue = -9999999.99M;

        #endregion

        #region Fields

        private bool _isAllSelected;

        #endregion

        public DecimalTextBox() : base()
        {
            // Align text to the right hand side by default.
            // This can be overridden by specifying a style
            TextAlignment = TextAlignment.Right;
        }

        #region Properties

        /// <summary>
        /// Determines the appropriate string format for this control
        /// based on the format and number of decimal places specified.
        /// </summary>
        private string StringFormatString
        {
            get
            {
                return "{0:" + NumberFormat + DecimalPlaces + "}";
            }
        }

        /// <summary>
        /// This variable will be used when calculating the position
        /// and resulting number of a new digit entered by the user.
        /// </summary>
        private decimal _addNumberConversionExponent = 100M;
        private decimal AddNumberConversionExponent
        {
            get { return _addNumberConversionExponent; }
            set { _addNumberConversionExponent = value; }
        }

        /// <summary>
        /// This variable will be used to calculate the resulting number
        /// after the user has deleted the right-most digit using backspace.
        /// </summary>
        private decimal _removeNumberConversionExponent = 0.1M;
        private decimal RemoveNumberConversionExponent
        {
            get { return _removeNumberConversionExponent; }
            set { _removeNumberConversionExponent = value; }
        }

        #endregion

        #region DependencyProperties

        /// <summary>
        /// The string that is displayed by the textbox. This should never be
        /// set by the developer as this textbox is responsible for any
        /// permissable form of input.
        /// </summary>
        public string DisplayString
        {
            get { return (string)GetValue(DisplayStringProperty); }
            set { SetValue(DisplayStringProperty, value); }
        }

        public static readonly DependencyProperty DisplayStringProperty =
            DependencyProperty.Register("DisplayString", typeof(string), typeof(DecimalTextBox));

        /// <summary>
        /// Desired format code of the decimal in this textbox i.e. "C" or "N", 
        /// without number of decimal places specified. The default value is "N".
        /// </summary>
        public string NumberFormat
        {
            get { return (string)GetValue(NumberFormatProperty); }
            set { SetValue(NumberFormatProperty, value); }
        }

        public static readonly DependencyProperty NumberFormatProperty =
            DependencyProperty.Register("NumberFormat", typeof(string), typeof(DecimalTextBox),
                new FrameworkPropertyMetadata("N"));

        /// <summary>
        /// Number of decimal places that this textbox should be using. The default value is 2.
        /// </summary>
        public int DecimalPlaces
        {
            get { return (int)GetValue(DecimalPlacesProperty); }
            set { SetValue(DecimalPlacesProperty, value); }
        }

        public static readonly DependencyProperty DecimalPlacesProperty =
            DependencyProperty.Register("DecimalPlaces", typeof(int), typeof(DecimalTextBox),
                new PropertyMetadata(2, OnDecimalPlacesPropertyChanged));

        private static void OnDecimalPlacesPropertyChanged(DependencyObject d,
                                                           DependencyPropertyChangedEventArgs e)
        {
            var control = d as DecimalTextBox;
            control.CalculateExponents((int)e.NewValue);
        }

        public decimal MinimumValue
        {
            get { return (decimal)GetValue(MinimumValueProperty); }
            set { SetValue(MinimumValueProperty, value); }
        }

        public static readonly DependencyProperty MinimumValueProperty =
            DependencyProperty.Register("MinimumValue", typeof(decimal), typeof(DecimalTextBox),
                new PropertyMetadata(DefaultMinValue));

        public decimal MaximumValue
        {
            get { return (decimal)GetValue(MaximumValueProperty); }
            set { SetValue(MaximumValueProperty, value); }
        }

        public static readonly DependencyProperty MaximumValueProperty =
            DependencyProperty.Register("MaximumValue", typeof(decimal), typeof(DecimalTextBox),
                new PropertyMetadata(DefaultMaxValue));

        public bool ShowZeroValue
        {
            get { return (bool)GetValue(ShowZeroValueProperty); }
            set { SetValue(ShowZeroValueProperty, value); }
        }

        public static readonly DependencyProperty ShowZeroValueProperty =
            DependencyProperty.Register("ShowZeroValue", typeof(bool), typeof(DecimalTextBox),
                new PropertyMetadata(false));

        public decimal? Value
        {
            get { return (decimal?)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(decimal?), typeof(DecimalTextBox),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnValuePropertyChanged));

        private static void OnValuePropertyChanged(DependencyObject d,
                                                   DependencyPropertyChangedEventArgs e)
        {
            var control = d as DecimalTextBox;
            control.SetDisplayString((decimal?)e.NewValue);
        }

        #endregion

        #region Events

        /// <summary>
        /// Ignore keypress if we handle it using PreviewKeyDown as they can
        /// come down as separate events.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DecimalTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (IsKeyHandled(e.Key))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// This function will be called after OnPreviewKeyDown.
        /// </summary>
        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!IsReadOnly && IsKeyHandled(e.Key))
            {
                decimal newValue;

                if (_isAllSelected)
                {
                    ToggleSelectAll();
                    newValue = 0M;
                }
                else
                {
                    // Convert the Textbox DisplayString into a Decimal to process operations
                    string valueString = DisplayString != null ?
                                         Regex.Replace(DisplayString, CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol, string.Empty) :
                                         string.Empty;

                    if (!String.IsNullOrEmpty(valueString))
                    {
                        if (!Decimal.TryParse(valueString, out newValue))
                        {
                            // This should never happen as the DecimalTextBox should be
                            // setting the DisplayString property
                            throw new InvalidCastException("DecimalTextBox failed to parse Value.");
                        }
                    }
                    else
                    {
                        newValue = 0M;
                    }
                }

                if (IsKeyNumeric(e.Key))
                {
                    // Insert new digit at the right-hand most position
                    if (newValue < 0)
                    {
                        newValue = (newValue * 10M) - (InputHelper.GetDecimalFromKey(e.Key) /
                            AddNumberConversionExponent);

                    }
                    else
                    {
                        newValue = (newValue * 10M) + (InputHelper.GetDecimalFromKey(e.Key) /
                            AddNumberConversionExponent);
                    }

                    e.Handled = true;
                }
                else
                {
                    switch (e.Key)
                    {
                        case Key.Back:
                            {
                                // Remove the digit furtherest on the right hand side
                                newValue = (newValue - (newValue %
                                    RemoveNumberConversionExponent)) / 10M;
                                break;
                            }
                        case Key.Delete:
                            {
                                newValue = 0M;
                                break;
                            }
                        case Key.Subtract:
                        case Key.OemMinus:
                            {
                                newValue *= -1;
                                break;
                            }
                        default:
                            break;
                    }

                    e.Handled = true;
                }

                if (newValue < 0)
                {
                    newValue = Math.Max(newValue, MinimumValue);
                }
                else
                {
                    newValue = Math.Min(newValue, MaximumValue);
                }

                e.Handled = true;
                Value = newValue;
            }
        }

        private void TextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Keep the caret at the end of the number
            CaretIndex = Text.Length;
            (sender as TextBox).Focus();
        }

        private void TextBox_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            // Keep the caret at the end of the number
            CaretIndex = Text.Length;
            (sender as TextBox).Focus();
            ToggleSelectAll(true);
        }

        #endregion

        #region Overrides

        /// <summary>
        /// This function is called after the control has been initialised by the WPF framework.
        /// </summary>
        protected override void OnInitialized(EventArgs e)
        {
            // Bind Text to Number with the specified StringFormat
            var textBinding = new Binding();
            textBinding.Path = new PropertyPath("DisplayString");
            textBinding.RelativeSource = new RelativeSource(RelativeSourceMode.Self);
            textBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            BindingOperations.SetBinding(this, TextBox.TextProperty, textBinding);

            // Attach events
            CaretIndex = Text.Length;
            PreviewKeyDown += TextBox_PreviewKeyDown;
            PreviewMouseDown += TextBox_PreviewMouseDown;
            PreviewMouseUp += TextBox_PreviewMouseUp;
            KeyDown += DecimalTextBox_KeyDown;
            ContextMenu = null;

            // Show Zero value if specified
            if (ShowZeroValue)
            {
                DisplayString = String.Format(StringFormatString, 0M);
            }

            base.OnInitialized(e);
        }

        /// <summary>
        /// This function will intercept keypresses before the textbox has
        /// the opportunity to handle the keypress, so we ignore irrelevant 
        /// keys here.
        /// </summary>
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            // Do nothing if we ignore this key
            if (IsKeyIgnored(e.Key))
            {
                e.Handled = true;
            }

            base.OnPreviewKeyDown(e);
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            CaretIndex = Text.Length;
            ToggleSelectAll(true);

            base.OnGotFocus(e);
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            // Keep the caret at the end of the number
            CaretIndex = Text.Length;
        }

        /// <summary>
        /// It is possible to assign/bind value before template/style options have been applied.
        /// Therefore ensure that the value matches the template/style when it has been applied.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            SetDisplayString(Value);
        }

        #endregion

        private void SetDisplayString(decimal? number = null)
        {
            // If ShowZeroValue is true and number is not null
            // then always display the number
            if (number != null && (number != 0M || ShowZeroValue))
            {
                DisplayString = String.Format(StringFormatString, number);
            }
            else
            {
                DisplayString = string.Empty;
            }
        }

        /// <summary>
        /// Toggles full selection of text content in this control.
        /// </summary>
        /// <param name="forceSelection">Enforce selection or deselection of text content</param>
        private void ToggleSelectAll(bool? forceSelection = null)
        {
            if ((forceSelection.HasValue && forceSelection.Value) || !_isAllSelected)
            {
                SelectAll();
                _isAllSelected = true;
            }
            else if ((forceSelection.HasValue && !forceSelection.Value) || _isAllSelected)
            {
                CaretIndex = Text.Length;
                _isAllSelected = false;
            }
        }

        /// <summary>
        /// Caches the exponents needed to calculate the position of newly entered digits 
        /// and how by how much the number should be shortened upon deleting the most 
        /// recent digit with backspace.
        /// </summary>
        /// <param name="numberOfDecimalPlaces">Number of decimal places this control 
        /// will use</param>
        private void CalculateExponents(int numberOfDecimalPlaces)
        {
            try
            {
                AddNumberConversionExponent = Convert.ToDecimal(Math.Pow(10,
                    numberOfDecimalPlaces));
                RemoveNumberConversionExponent = Convert.ToDecimal(1 / Math.Pow(10,
                    numberOfDecimalPlaces - 1));
            }
            catch (OverflowException e)
            {
                // This should never happen as the default min/max values
                // are far below the decimal limits.
                throw new OverflowException("The min or max value for DecimalTextBox " +
                    "has been set too high.", e);
            }
        }

        /// <summary>
        /// Determines if the specified key is numeric.
        /// </summary>
        /// <param name="key">Key entered</param>
        /// <returns></returns>
        private bool IsKeyNumeric(Key key)
        {
            return InputHelper.IsKeyNumeric(key);
        }

        /// <summary>
        /// Determines if the specified key is ignored by this control.
        /// </summary>
        /// <param name="key">Key entered</param>
        private bool IsKeyIgnored(Key key)
        {
            return InputHelper.IsKeyIgnored(key, InputConfiguration.Numeric | InputConfiguration.CustomTextBox);
        }

        /// <summary>
        /// Determines if the specified key is handled by this control
        /// </summary>
        /// <param name="key">Key entered</param>
        private bool IsKeyHandled(Key key)
        {
            return InputHelper.IsKeyHandled(key, InputConfiguration.Numeric | InputConfiguration.CustomTextBox);
        }
    }
}
