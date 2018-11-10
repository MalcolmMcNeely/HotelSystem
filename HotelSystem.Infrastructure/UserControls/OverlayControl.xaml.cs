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
            DependencyProperty.Register("OverlayContent", typeof(ScrollViewer), typeof(OverlayControl));

        public double OverlayWidth
        {
            get { return (double)GetValue(OverlayWidthProperty); }
            set { SetValue(OverlayWidthProperty, value); }
        }

        public static readonly DependencyProperty OverlayWidthProperty =
            DependencyProperty.Register("OverlayWidth", typeof(double), typeof(OverlayControl),
                new PropertyMetadata(200d));

        public bool IsOverlayVisible
        {
            get { return (bool)GetValue(IsOverlayVisibleProperty); }
            set { SetValue(IsOverlayVisibleProperty, value); }
        }

        public static readonly DependencyProperty IsOverlayVisibleProperty =
            DependencyProperty.Register("IsOverlayVisible", typeof(bool), typeof(OverlayControl),
                new FrameworkPropertyMetadata(false, 
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, 
                    OnIsOverlayVisibleChanged));

        private static void OnIsOverlayVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var overlay = d as OverlayControl;

            if(d != null)
            {
                if((bool)e.NewValue)
                {
                    overlay.Show();
                }
                else
                {
                    overlay.Hide();
                }
            }
        }

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
            if (HorizontalAlignment == HorizontalAlignment.Left)
            {
                RenderTransform = new TranslateTransform(-OverlayWidth - HiddenMargin, 0);
                OverlayContentPresenter.SetValue(DockPanel.DockProperty, Dock.Left);
            }
            else
            {
                RenderTransform = new TranslateTransform(OverlayWidth + HiddenMargin, 0);
                OverlayContentPresenter.SetValue(DockPanel.DockProperty, Dock.Right);
            }

            OverlayContentPresenter.Width = OverlayWidth;
            
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
                To = GetHidePositionX(),
                Duration = TimeSpan.FromMilliseconds(AnimationTime)
            };

            RenderTransform.BeginAnimation(TranslateTransform.XProperty, animation);
            ShadowColumn.Width = new GridLength(0);

            IsOverlayVisible = false;
            _isShown = false;
        }

        private double GetHidePositionX()
        {
            if (HorizontalAlignment == HorizontalAlignment.Left)
            {
                var xLocation = TranslatePoint(new Point(0, 0), (UIElement)VisualParent).X;
                return xLocation - OverlayWidth - HiddenMargin;
            }
            else
            {
                var parentWidth = ((FrameworkElement)VisualParent).ActualWidth;
                return parentWidth + OverlayWidth + HiddenMargin;
            }
        }

        private void ShadowMouseDown(object sender, MouseButtonEventArgs e)
        {
            Hide();
        }
    }
}
