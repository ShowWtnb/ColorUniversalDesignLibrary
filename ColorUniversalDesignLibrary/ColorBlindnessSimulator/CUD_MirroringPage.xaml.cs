using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ColorUniversalDesignLibrary.ColorBlindnessSimulator
{
    /// <summary>
    /// Core_UI_CUD_MirroringPage.xaml の相互作用ロジック
    /// </summary>
    public partial class MirroringPage : Page, INotifyPropertyChanged
    {
        public MirroringPage()
        {
            InitializeComponent();
            MainGrid.DataContext = this;

            OriginalButtonStyle = MaterialDesignThemes.Wpf.PackIconKind.MagnifyPlusOutline;
            ProtanopiaButtonStyle = MaterialDesignThemes.Wpf.PackIconKind.MagnifyPlusOutline;
            TritanopiaButtonStyle = MaterialDesignThemes.Wpf.PackIconKind.MagnifyPlusOutline;
            DeuteranopiaButtonStyle = MaterialDesignThemes.Wpf.PackIconKind.MagnifyPlusOutline;
        }
        #region ButtonStyleProperty
        private MaterialDesignThemes.Wpf.PackIconKind _OriginalButtonStyle = MaterialDesignThemes.Wpf.PackIconKind.None;
        public MaterialDesignThemes.Wpf.PackIconKind OriginalButtonStyle
        {
            get { return _OriginalButtonStyle; }
            set
            {
                _OriginalButtonStyle = value;
                OnPropertyChanged(nameof(OriginalButtonStyle));
            }
        }

        private MaterialDesignThemes.Wpf.PackIconKind _ProtanopiaButtonStyle = MaterialDesignThemes.Wpf.PackIconKind.None;
        public MaterialDesignThemes.Wpf.PackIconKind ProtanopiaButtonStyle
        {
            get { return _ProtanopiaButtonStyle; }
            set
            {
                _ProtanopiaButtonStyle = value;
                OnPropertyChanged(nameof(ProtanopiaButtonStyle));
            }
        }
        private MaterialDesignThemes.Wpf.PackIconKind _TritanopiaButtonStyle = MaterialDesignThemes.Wpf.PackIconKind.None;
        public MaterialDesignThemes.Wpf.PackIconKind TritanopiaButtonStyle
        {
            get { return _TritanopiaButtonStyle; }
            set
            {
                _TritanopiaButtonStyle = value;
                OnPropertyChanged(nameof(TritanopiaButtonStyle));
            }
        }
        private MaterialDesignThemes.Wpf.PackIconKind _DeuteranopiaButtonStyle = MaterialDesignThemes.Wpf.PackIconKind.None;
        public MaterialDesignThemes.Wpf.PackIconKind DeuteranopiaButtonStyle
        {
            get { return _DeuteranopiaButtonStyle; }
            set
            {
                _DeuteranopiaButtonStyle = value;
                OnPropertyChanged(nameof(DeuteranopiaButtonStyle));
            }
        }
        #endregion ButtonStyleProperty

        private bool IsFullScreen { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region ButtonClickedEvent
        private void OriginalButton_Click(object sender, RoutedEventArgs e)
        {
            Grid target = OriginalGrid;
            if (IsFullScreen)
            {
                Grid.SetRow(target, 0);
                Grid.SetColumn(target, 0);
                Grid.SetRowSpan(target, 1);
                Grid.SetColumnSpan(target, 1);

                OriginalButtonStyle = MaterialDesignThemes.Wpf.PackIconKind.MagnifyPlusOutline;

                ShowAllGrids();

                IsFullScreen = false;
            }
            else
            {
                Grid.SetRow(target, 0);
                Grid.SetColumn(target, 0);
                Grid.SetRowSpan(target, 2);
                Grid.SetColumnSpan(target, 2);

                OriginalButtonStyle = MaterialDesignThemes.Wpf.PackIconKind.MagnifyMinus;

                HideAllGrids();
                target.Visibility = Visibility.Visible;

                IsFullScreen = true;
            }
        }
        private void ProtanopiaButton_Click(object sender, RoutedEventArgs e)
        {
            var target = ProtanopiaGrid;
            if (IsFullScreen)
            {
                Grid.SetRow(target, 0);
                Grid.SetColumn(target, 1);
                Grid.SetRowSpan(target, 1);
                Grid.SetColumnSpan(target, 1);

                ProtanopiaButtonStyle = MaterialDesignThemes.Wpf.PackIconKind.MagnifyPlusOutline;

                ShowAllGrids();

                IsFullScreen = false;
            }
            else
            {
                Grid.SetRow(target, 0);
                Grid.SetColumn(target, 0);
                Grid.SetRowSpan(target, 2);
                Grid.SetColumnSpan(target, 2);

                ProtanopiaButtonStyle = MaterialDesignThemes.Wpf.PackIconKind.MagnifyMinus;

                HideAllGrids();
                target.Visibility = Visibility.Visible;

                IsFullScreen = true;
            }

        }
        private void TritanopiaButton_Click(object sender, RoutedEventArgs e)
        {
            var target = TritanopiaGrid;
            if (IsFullScreen)
            {
                Grid.SetRow(target, 1);
                Grid.SetColumn(target, 0);
                Grid.SetRowSpan(target, 1);
                Grid.SetColumnSpan(target, 1);

                TritanopiaButtonStyle = MaterialDesignThemes.Wpf.PackIconKind.MagnifyPlusOutline;

                ShowAllGrids();

                IsFullScreen = false;
            }
            else
            {
                Grid.SetRow(target, 0);
                Grid.SetColumn(target, 0);
                Grid.SetRowSpan(target, 2);
                Grid.SetColumnSpan(target, 2);

                TritanopiaButtonStyle = MaterialDesignThemes.Wpf.PackIconKind.MagnifyMinus;

                HideAllGrids();
                target.Visibility = Visibility.Visible;

                IsFullScreen = true;
            }

        }
        private void DeuteranopiaButton_Click(object sender, RoutedEventArgs e)
        {
            var target = DeuteranopiaGrid;
            if (IsFullScreen)
            {
                Grid.SetRow(target, 1);
                Grid.SetColumn(target, 1);
                Grid.SetRowSpan(target, 1);
                Grid.SetColumnSpan(target, 1);

                DeuteranopiaButtonStyle = MaterialDesignThemes.Wpf.PackIconKind.MagnifyPlusOutline;

                ShowAllGrids();

                IsFullScreen = false;
            }
            else
            {
                Grid.SetRow(target, 0);
                Grid.SetColumn(target, 0);
                Grid.SetRowSpan(target, 2);
                Grid.SetColumnSpan(target, 2);

                DeuteranopiaButtonStyle = MaterialDesignThemes.Wpf.PackIconKind.MagnifyMinus;

                HideAllGrids();
                target.Visibility = Visibility.Visible;

                IsFullScreen = true;
            }

        }
        #endregion ButtonClickedEvent

        private void ShowAllGrids()
        {
            OriginalGrid.Visibility = Visibility.Visible;
            ProtanopiaGrid.Visibility = Visibility.Visible;
            TritanopiaGrid.Visibility = Visibility.Visible;
            DeuteranopiaGrid.Visibility = Visibility.Visible;
        }
        private void HideAllGrids()
        {
            OriginalGrid.Visibility = Visibility.Collapsed;
            ProtanopiaGrid.Visibility = Visibility.Collapsed;
            TritanopiaGrid.Visibility = Visibility.Collapsed;
            DeuteranopiaGrid.Visibility = Visibility.Collapsed;
        }


        internal void SetImages(BitmapSource original, BitmapSource protanopia, BitmapSource tritanopia, BitmapSource deuteranopia)
        {
            Dispatcher.Invoke(() =>
            {
                Image1.Source = original;
                Image2.Source = protanopia;
                Image3.Source = tritanopia;
                Image4.Source = deuteranopia;
            });
        }
    }
}
