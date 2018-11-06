using HotelSystem.Infrastructure.CustomControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HotelSystem.Infrastructure.UserControls
{
    /// <summary>
    /// Interaction logic for LabelledDecimalTextBox.xaml
    /// </summary>
    public partial class LabelledDecimalTextBox : UserControl
    {
        public LabelledDecimalTextBox()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
        }

        static LabelledDecimalTextBox()
        {
            MyTextChangedEvent = TextBox.TextChangedEvent.AddOwner(typeof(LabelledDecimalTextBox));
            MyPreviewKeyDownEvent = TextBox.PreviewKeyDownEvent.AddOwner(typeof(LabelledDecimalTextBox));
        }

        #region Dependency Properties

        public decimal? Value
        {
            get { return (decimal?)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(decimal?),
                typeof(LabelledDecimalTextBox),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string LabelText
        {
            get { return (string)GetValue(LabelTextBoxProperty); }
            set { SetValue(LabelTextBoxProperty, value); }
        }

        public static readonly DependencyProperty LabelTextBoxProperty =
            DependencyProperty.Register("LabelText", typeof(string),
                typeof(LabelledDecimalTextBox));

        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register("IsReadOnly", typeof(bool),
                typeof(LabelledDecimalTextBox));

        public bool IsDecimalTextBoxFocusable
        {
            get { return (bool)GetValue(IsDecimalTextBoxFocusableProperty); }
            set { SetValue(IsDecimalTextBoxFocusableProperty, value); }
        }

        public static readonly DependencyProperty IsDecimalTextBoxFocusableProperty =
            DependencyProperty.Register("IsDecimalTextBoxFocusable", typeof(bool),
                typeof(LabelledDecimalTextBox), new FrameworkPropertyMetadata(true));

        public GridLength TextBoxGridLength
        {
            get { return (GridLength)GetValue(TextBoxGridLengthProperty); }
            set { SetValue(TextBoxGridLengthProperty, value); }
        }

        public static readonly DependencyProperty TextBoxGridLengthProperty =
            DependencyProperty.Register("TextBoxGridLength", typeof(GridLength),
                typeof(LabelledDecimalTextBox), new FrameworkPropertyMetadata(
                   new GridLength(1, GridUnitType.Star)));

        #region DecimalTextBox Configuration

        public Style DecimalTextBoxStyle
        {
            get { return (Style)GetValue(DecimalTextBoxStyleProperty); }
            set { SetValue(DecimalTextBoxStyleProperty, value); }
        }

        public static readonly DependencyProperty DecimalTextBoxStyleProperty =
            DependencyProperty.Register("DecimalTextBoxStyle", typeof(Style),
                typeof(LabelledDecimalTextBox));

        public string NumberFormat
        {
            get { return (string)GetValue(NumberFormatProperty); }
            set { SetValue(NumberFormatProperty, value); }
        }

        public static readonly DependencyProperty NumberFormatProperty =
            DependencyProperty.Register("NumberFormat", typeof(string),
                typeof(LabelledDecimalTextBox), new FrameworkPropertyMetadata("N"));

        public int DecimalPlaces
        {
            get { return (int)GetValue(DecimalPlacesProperty); }
            set { SetValue(DecimalPlacesProperty, value); }
        }

        public static readonly DependencyProperty DecimalPlacesProperty =
            DependencyProperty.Register("DecimalPlaces", typeof(int),
                typeof(LabelledDecimalTextBox), new FrameworkPropertyMetadata(2));

        public decimal MinimumValue
        {
            get { return (decimal)GetValue(MinimumValueProperty); }
            set { SetValue(MinimumValueProperty, value); }
        }

        public static readonly DependencyProperty MinimumValueProperty =
            DependencyProperty.Register("MinimumValue", typeof(decimal),
                typeof(LabelledDecimalTextBox),
                new PropertyMetadata(DecimalTextBox.DefaultMinValue));

        public decimal MaximumValue
        {
            get { return (decimal)GetValue(MaximumValueProperty); }
            set { SetValue(MaximumValueProperty, value); }
        }

        public static readonly DependencyProperty MaximumValueProperty =
            DependencyProperty.Register("MaximumValue", typeof(decimal),
                typeof(LabelledDecimalTextBox),
                new PropertyMetadata(DecimalTextBox.DefaultMaxValue));

        public bool ShowZeroValue
        {
            get { return (bool)GetValue(ShowZeroValueProperty); }
            set { SetValue(ShowZeroValueProperty, value); }
        }

        public static readonly DependencyProperty ShowZeroValueProperty =
            DependencyProperty.Register("ShowZeroValue", typeof(bool),
                typeof(LabelledDecimalTextBox), new FrameworkPropertyMetadata(true));

        public TextAlignment DecimalTextBoxTextAlignment
        {
            get { return (TextAlignment)GetValue(DecimalTextBoxTextAlignmentProperty); }
            set { SetValue(DecimalTextBoxTextAlignmentProperty, value); }
        }

        public static readonly DependencyProperty DecimalTextBoxTextAlignmentProperty =
                DependencyProperty.Register("DecimalTextBoxTextAlignment", typeof(TextAlignment),
                    typeof(LabelledDecimalTextBox), new FrameworkPropertyMetadata(TextAlignment.Left));

        #endregion

        #endregion

        #region Events

        public static readonly RoutedEvent MyTextChangedEvent;
        public static readonly RoutedEvent MyPreviewKeyDownEvent;

        public event TextChangedEventHandler MyTextChanged
        {
            add { AddHandler(MyTextChangedEvent, value); }
            remove { RemoveHandler(MyTextChangedEvent, value); }
        }

        public event KeyEventHandler MyPreviewKeyDown
        {
            add { AddHandler(MyPreviewKeyDownEvent, value); }
            remove { RemoveHandler(MyPreviewKeyDownEvent, value); }
        }

        protected virtual void OnMyTextChanged()
        {
            TextChangedEventArgs evargs = new TextChangedEventArgs(MyTextChangedEvent, UndoAction.None);
            RaiseEvent(evargs);
        }

        #endregion
    }
}
