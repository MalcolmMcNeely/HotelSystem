using HotelSystem.Infrastructure.Helpers;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace HotelSystem.Infrastructure.CustomControls
{
    public class IntegerTextBox : TextBox
    {
        #region Static

        public static readonly int DefaultMaxValue = 9999999;
        public static readonly int DefaultMinValue = -9999999;

        #endregion

        #region Fields

        private bool _isAllSelected;

        #endregion

        public IntegerTextBox() : base()
        {
            // Align text to the right hand side by default.
            // This can be overridden by specifying a style
            TextAlignment = TextAlignment.Right;
        }

        #region Properties

        /// <summary>
        /// Determines the appropriate string format for this control
        /// based on the format.
        /// </summary>
        private string StringFormatString
        {
            get
            {
                return "{0:" + NumberFormat + 0 + "}";
            }
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
            DependencyProperty.Register("DisplayString", typeof(string), typeof(IntegerTextBox));

        /// <summary>
        /// Desired format code of the integer in this textbox i.e. "C" or "N".
        /// The default value is "N".
        /// </summary>
        public string NumberFormat
        {
            get { return (string)GetValue(NumberFormatProperty); }
            set { SetValue(NumberFormatProperty, value); }
        }

        public static readonly DependencyProperty NumberFormatProperty =
            DependencyProperty.Register("NumberFormat", typeof(string), typeof(IntegerTextBox),
                new FrameworkPropertyMetadata("N"));

        public int MinimumValue
        {
            get { return (int)GetValue(MinimumValueProperty); }
            set { SetValue(MinimumValueProperty, value); }
        }

        public static readonly DependencyProperty MinimumValueProperty =
            DependencyProperty.Register("MinimumValue", typeof(int), typeof(IntegerTextBox),
                new PropertyMetadata(DefaultMinValue));

        public int MaximumValue
        {
            get { return (int)GetValue(MaximumValueProperty); }
            set { SetValue(MaximumValueProperty, value); }
        }

        public static readonly DependencyProperty MaximumValueProperty =
            DependencyProperty.Register("MaximumValue", typeof(int), typeof(IntegerTextBox),
                new PropertyMetadata(DefaultMaxValue));

        public bool ShowZeroValue
        {
            get { return (bool)GetValue(ShowZeroValueProperty); }
            set { SetValue(ShowZeroValueProperty, value); }
        }

        public static readonly DependencyProperty ShowZeroValueProperty =
            DependencyProperty.Register("ShowZeroValue", typeof(bool), typeof(IntegerTextBox),
                new PropertyMetadata(false));

        public int? Value
        {
            get { return (int?)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int?), typeof(IntegerTextBox),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnValuePropertyChanged));

        private static void OnValuePropertyChanged(DependencyObject d,
                                                   DependencyPropertyChangedEventArgs e)
        {
            var control = d as IntegerTextBox;
            control.SetDisplayString((int?)e.NewValue);
        }

        #endregion

        #region Events

        /// <summary>
        /// Ignore keypress if we handle it using PreviewKeyDown as they can
        /// come down as separate events.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IntegerTextBox_KeyDown(object sender, KeyEventArgs e)
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
                int newValue;

                if (_isAllSelected)
                {
                    ToggleSelectAll();
                    newValue = 0;
                }
                else
                {
                    // Convert the Textbox DisplayString into an Integer to process operations
                    string valueString = DisplayString != null ?
                                         Regex.Replace(DisplayString, ",", string.Empty) :
                                         string.Empty;

                    if (!String.IsNullOrEmpty(valueString))
                    {
                        if (!Int32.TryParse(valueString, out newValue))
                        {
                            // This should never happen as the IntegerTextBox should be
                            // setting the DisplayString property
                            throw new InvalidCastException("IntegerTextBox failed to parse Value.");
                        }
                    }
                    else
                    {
                        newValue = 0;
                    }
                }

                if (IsKeyNumeric(e.Key))
                {
                    // Insert new digit at the right-hand most position
                    if (newValue < 0)
                    {
                        newValue = (newValue * 10) - (InputHelper.GetIntegerFromKey(e.Key));
                    }
                    else
                    {
                        newValue = (newValue * 10) + (InputHelper.GetIntegerFromKey(e.Key));
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
                                newValue = newValue / 10;
                                break;
                            }
                        case Key.Delete:
                            {
                                newValue = 0;
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
            KeyDown += IntegerTextBox_KeyDown;
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

        private void SetDisplayString(int? number = null)
        {
            // If ShowZeroValue is true and number is not null
            // then always display the number
            if (number != null && (number != 0 || ShowZeroValue))
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
