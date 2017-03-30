using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BombingApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Infantry ukko;
        private DispatcherTimer gameTimer;
        private DispatcherTimer ukkoTimer;
        
        
        public MainPage()
        {
            this.InitializeComponent();

            // Add ukko
            ukko = new Infantry
            {
                LocationX = myCanvas.Width = 300,
                LocationY = myCanvas.Height = 100
            };

            myCanvas.Children.Add(ukko);

            // timer for moving ukko
            ukkoTimer = new DispatcherTimer();
            ukkoTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            ukkoTimer.Tick += UkkoTimer_Tick;
            ukkoTimer.Start();

            // Game loop timer
            gameTimer = new DispatcherTimer();
            gameTimer.Interval = new TimeSpan(0, 0, 0, 1000 / 60);
            gameTimer.Tick += GameTimer_Tick1;
            gameTimer.Start();
            
        }

        private void UkkoTimer_Tick(object sender, object e)
        {
            ukko.Move();
        }

        
        private void GameTimer_Tick1(object sender, object e)
        {
            
        }
        
    }
}
