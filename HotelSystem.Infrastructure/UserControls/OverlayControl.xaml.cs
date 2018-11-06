using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace HotelSystem.Infrastructure.UserControls
{
    /// <summary>
    /// Interaction logic for OverlayControl.xaml
    /// </summary>
    public partial class OverlayControl : UserControl
    {
        private bool _isShown;

        public OverlayControl()
        {
            InitializeComponent();
        }

        #region Dependency Properties

        public ScrollViewer OverlayContent
        {
            get { return (ScrollViewer)GetValue(OverlayContentProperty); }
            set { SetValue(OverlayContentProperty, value); }
        }

        public static readonly DependencyProperty OverlayContentProperty =
            DependencyProperty.Register("MenuContent", typeof(ScrollViewer), typeof(OverlayControl));

        public double OverlayWidth
        {
            get { return (double)GetValue(OverlayWidthProperty); }
            set { SetValue(OverlayWidthProperty, value); }
        }

        public static readonly DependencyProperty OverlayWidthProperty =
            DependencyProperty.Register("MenuWidth", typeof(double), typeof(OverlayControl),
                new PropertyMetadata(200d));

        public double AnimationTime
        {
            get { return (double)GetValue(AnimationTimeProperty); }
            set { SetValue(AnimationTimeProperty, value); }
        }

        public static readonly DependencyProperty AnimationTimeProperty =
            DependencyProperty.Register("AnimationTime", typeof(double), typeof(OverlayControl),
                new PropertyMetadata(100d));

        public int HiddenMargin
        {
            get { return (int)GetValue(HiddenMarginProperty); }
            set { SetValue(HiddenMarginProperty, value); }
        }

        public static readonly DependencyProperty HiddenMarginProperty =
            DependencyProperty.Register("HiddenMarginProperty", typeof(int), typeof(OverlayControl),
                new PropertyMetadata(0));

        #endregion

        public override void OnApplyTemplate()
        {
            RenderTransform = new TranslateTransform(-OverlayWidth - HiddenMargin, 0);
            MenuColumn.Width = new GridLength(OverlayWidth);
        }

        public void Toggle()
        {
            if (_isShown)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }

        public void Show()
        {
            var animation = new DoubleAnimation
            {
                From = -OverlayWidth * .95,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(AnimationTime)
            };

            RenderTransform.BeginAnimation(TranslateTransform.XProperty, animation);
            ShadowColumn.Width = new GridLength(10000);
            _isShown = true;
        }

        public void Hide()
        {
            var animation = new DoubleAnimation
            {
                To = -OverlayWidth - HiddenMargin,
                Duration = TimeSpan.FromMilliseconds(AnimationTime)
            };

            RenderTransform.BeginAnimation(TranslateTransform.XProperty, animation);
            ShadowColumn.Width = new GridLength(0);
            _isShown = false;
        }

        private void ShadowMouseDown(object sender, MouseButtonEventArgs e)
        {
            Hide();
        }
    }
}
