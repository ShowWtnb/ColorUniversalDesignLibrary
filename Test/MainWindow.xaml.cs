using System;
using System.Windows;

namespace Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ColorUniversalDesignLibrary.ColorBlindnessSimulator.CUD_ColorBlindnessSimulator _cbs;
        public MainWindow()
        {
            InitializeComponent();
                        
            _cbs = new ColorUniversalDesignLibrary.ColorBlindnessSimulator.CUD_ColorBlindnessSimulator();
            _cbs.StartMirroring(this);
            Closed += MainWindow_Closed;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            _cbs.StopMirroring();
        }
    }
}
