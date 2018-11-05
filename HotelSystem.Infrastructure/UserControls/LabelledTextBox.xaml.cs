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
   /// Interaction logic for LabelledTextBox.xaml
   /// </summary>
   public partial class LabelledTextBox : UserControl
   {
      public LabelledTextBox()
      {
         InitializeComponent();
         LayoutRoot.DataContext = this;
      }

      static LabelledTextBox()
      {
         MyTextChangedEvent = TextBox.TextChangedEvent.AddOwner(typeof(LabelledTextBox));
         MyPreviewKeyDownEvent = TextBox.PreviewKeyDownEvent.AddOwner(typeof(LabelledTextBox));
      }

      #region Dependency Properties

      public string Text
      {
         get { return (string)GetValue(TextProperty); }
         set { SetValue(TextProperty, value); }
      }

      public static readonly DependencyProperty TextProperty =
         DependencyProperty.Register("Text", typeof(string), typeof(LabelledTextBox),
            new FrameworkPropertyMetadata(string.Empty,
               FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

      public string LabelText
      {
         get { return (string)GetValue(LabelTextBoxProperty); }
         set { SetValue(LabelTextBoxProperty, value); }
      }

      public static readonly DependencyProperty LabelTextBoxProperty =
          DependencyProperty.Register("LabelText", typeof(string), typeof(LabelledTextBox));

      public bool IsReadOnly
      {
         get { return (bool)GetValue(IsReadOnlyProperty); }
         set { SetValue(IsReadOnlyProperty, value); }
      }

      public static readonly DependencyProperty IsReadOnlyProperty =
          DependencyProperty.Register("IsReadOnly", typeof(bool),
              typeof(LabelledTextBox));

      public GridLength TextBoxGridLength
      {
         get { return (GridLength)GetValue(TextBoxGridLengthProperty); }
         set { SetValue(TextBoxGridLengthProperty, value); }
      }

      public static readonly DependencyProperty TextBoxGridLengthProperty =
          DependencyProperty.Register("TextBoxGridLength", typeof(GridLength),
              typeof(LabelledTextBox), new FrameworkPropertyMetadata(
                 new GridLength(1, GridUnitType.Star)));

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
