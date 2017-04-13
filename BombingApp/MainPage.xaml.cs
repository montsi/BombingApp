using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private List<Bomb> bombs;
        //private Bomb bomb;
        private List<Infantry> ukot;
        //private Infantry ukko;
        
        private DispatcherTimer gameTimer;
        //private DispatcherTimer ukkoTimer;
        
        
        public MainPage()
        {
            this.InitializeComponent();

            // bomb = new Bomb();
            bombs = new List<Bomb>();
            ukot = new List<Infantry>();

            // create a ukko
            Infantry ukko = new Infantry()
            {
                LocationX = myCanvas.Width / 2,
                LocationY = myCanvas.Height / 2
            };
            ukot.Add(ukko);
            myCanvas.Children.Add(ukko);
           

            // mouse listener
            Window.Current.CoreWindow.PointerPressed += CoreWindow_PointerPressed;

            /* timer for moving ukko
            ukkoTimer = new DispatcherTimer();
            ukkoTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            ukkoTimer.Tick += UkkoTimer_Tick;
            ukkoTimer.Start(); */

            // Game loop timer
            gameTimer = new DispatcherTimer();
            gameTimer.Interval = new TimeSpan(0, 0, 0, 1000 / 60);
            gameTimer.Tick += GameTimer_Tick1;
            gameTimer.Start();
            
        }

        private void CoreWindow_PointerPressed(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.PointerEventArgs args)
        {
            Bomb bomb = new Bomb();
            bomb.LocationX = args.CurrentPoint.Position.X - bomb.Width / 2;
            bomb.LocationY = args.CurrentPoint.Position.Y - bomb.Width / 2;
            // add to canvas
            myCanvas.Children.Add(bomb);
            bomb.SetLocation();
            // add bombs list
            bombs.Add(bomb);
            CheckCollision(bomb);
        }

        /*private void UkkoTimer_Tick(object sender, object e)
        {
            
            ukko.Move();
        }*/

        
        private void GameTimer_Tick1(object sender, object e)
        {
            
        }

        private void CheckCollision(Bomb bomb)
        {
            Debug.WriteLine("testii");

            // loop bomb list
            foreach (Infantry ukko in ukot)
            {
                // get rects
                Rect BRect = new Rect(
                    bomb.LocationX, bomb.LocationY, bomb.ActualWidth, bomb.ActualHeight
                    );
                Rect IRect = new Rect(
                    ukko.LocationX, ukko.LocationY, ukko.ActualWidth, ukko.ActualHeight
                    );
                // does objects intersect

                Debug.WriteLine(IRect);
                Debug.WriteLine(BRect);

                BRect.Intersect(IRect);           
                
                if (!BRect.IsEmpty)
                {
                    // collision! area not empty
                    // remove ukko from canvas
                    myCanvas.Children.Remove(ukko);
                    // remove from list
                    ukot.Remove(ukko);
                    
                    
                    // play audio
                    //mediaElement.Play();

                    break;
                }
            }
        }
    }
}
