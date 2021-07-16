﻿
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
#if NETFX_CORE
using Popup = Windows.UI.Xaml.Controls.Primitives.Popup;
#else
using Popup = Windows.UI.Xaml.Controls.Popup;
#endif

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace EntregaFinal.Components
{
    public sealed partial class Loader : Windows.UI.Xaml.Controls.UserControl
    {
        private Popup _popup;

        public Loader()
        {
            InitializeComponent();
        }

        public Loader(string message)
        {
            InitializeComponent();
            SystemNavigationManager.GetForCurrentView().BackRequested += UWPHUD_BackRequested;
            Window.Current.CoreWindow.SizeChanged += CoreWindow_SizeChanged;
            msg_Txt.Text = message;
        }

        private void CoreWindow_SizeChanged(CoreWindow sender, WindowSizeChangedEventArgs args)
        {
            UpdateUI();
        }

        private void UWPHUD_BackRequested(object sender, BackRequestedEventArgs e)
        {
            Close();
        }

        private void UpdateUI()
        {
            Rect bounds = Window.Current.Bounds;
            Width = bounds.Width;
            Height = bounds.Height;
        }

        public void Show()
        {
            try
            {
                _popup = new Popup
                {
                    Child = this
                };
                progress_R.IsActive = true;
                _popup.IsOpen = true;
                UpdateUI();
            }
            catch { }
        }

        public void Close()
        {
            if (_popup.IsOpen)
            {
                progress_R.IsActive = false;
                _popup.IsOpen = false;
                SystemNavigationManager.GetForCurrentView().BackRequested -= UWPHUD_BackRequested;
                Window.Current.CoreWindow.SizeChanged -= CoreWindow_SizeChanged;
            }
        }
    }
}
