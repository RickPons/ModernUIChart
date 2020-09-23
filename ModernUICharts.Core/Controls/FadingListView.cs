﻿using System;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace ModernUICharts
{
    public class FadingListView : ItemsControl
    {
        public static readonly DependencyProperty RealWidthProperty =
            DependencyProperty.Register("RealWidth", typeof(double), typeof(FadingListView),
            new PropertyMetadata(0.0));
        public static readonly DependencyProperty RealHeightProperty =
            DependencyProperty.Register("RealHeight", typeof(double), typeof(FadingListView),
            new PropertyMetadata(0.0));

        static FadingListView()
        {
        }

        public FadingListView()
        {
            this.SizeChanged += FadingListView_SizeChanged;
           
        }

        public double RealWidth
        {
            get
            {
                return (double)GetValue(RealWidthProperty);
            }
            set
            {
                SetValue(RealWidthProperty, value);
            }
        }

        public double RealHeight
        {
            get
            {
                return (double)GetValue(RealHeightProperty);
            }
            set
            {
                SetValue(RealHeightProperty, value);
            }
        }

        void FadingListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RealWidth = this.ActualWidth;
            RealHeight = this.ActualHeight;
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            if (this.Items != null)
            {
                if (this.Items.Count < 100)
                {
                    int index = this.ItemContainerGenerator.IndexFromContainer(element);
                    var lb = (ContentPresenter)element;

                    TimeSpan waitTime = TimeSpan.FromMilliseconds(index * (500.0 / this.Items.Count));

                    lb.Opacity = 0.0;
                    DoubleAnimation anm = new DoubleAnimation()
                    {
                        From = 0,
                        To = 1,
                        Duration = TimeSpan.FromMilliseconds(250),
                        BeginTime = waitTime
                    };
                    Storyboard storyda = new Storyboard();
                    storyda.Children.Add(anm);
                    Storyboard.SetTarget(storyda, lb);
#if NETFX_CORE
                    Storyboard.SetTargetProperty(storyda, "Opacity");
#else
                    Storyboard.SetTargetProperty(storyda, new PropertyPath(OpacityProperty));
#endif
                    storyda.Begin();
                }
            }

            base.PrepareContainerForItemOverride(element, item);
        }
    }
}