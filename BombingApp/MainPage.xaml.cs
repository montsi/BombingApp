using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
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
        private List<Infantry> ukot;
        private List<Tank> tanks;
        
        private DispatcherTimer gameTimer;
        private DispatcherTimer ukkoTimer;
        private DispatcherTimer tankTimer;
        
        
        public MainPage()
        {
            this.InitializeComponent();

            ApplicationView.PreferredLaunchViewSize = new Size(1280, 720);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            // 
            bombs = new List<Bomb>();
            ukot = new List<Infantry>();
            tanks = new List<Tank>();

            // create a ukko
            createInfantry();

            createTank();

            // mouse listener
            Window.Current.CoreWindow.PointerPressed += CoreWindow_PointerPressed;

            // timer for  ukko
            ukkoTimer = new DispatcherTimer();
            ukkoTimer.Interval = new TimeSpan(0, 0, 0, 1);
            ukkoTimer.Tick += UkkoTimer_Tick;
            ukkoTimer.Start();

            // timer for tank
            tankTimer = new DispatcherTimer();
            tankTimer.Interval = new TimeSpan(0, 0, 0, 4);
            tankTimer.Tick += TankTimer_Tick1;
            tankTimer.Start();

            // Game loop timer
            gameTimer = new DispatcherTimer();
            gameTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000 / 60);
            gameTimer.Tick += GameTimer_Tick1;
            gameTimer.Start();
            
        }

        private void TankTimer_Tick1(object sender, object e)
        {
            createTank();
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

        private void UkkoTimer_Tick(object sender, object e)
        {
            createInfantry();        
        }
        private void createInfantry()
        {
            Infantry ukko = new Infantry()
            {
                LocationY = myCanvas.Height / 2
            };
            Random random = new Random();
            ukko.LocationY = random.Next(50, 720 - 50);
            ukko.SetLocation();
            ukot.Add(ukko);
            myCanvas.Children.Add(ukko);
        }

        private void createTank()
        {
            Tank tank = new Tank()
            {
                LocationY = myCanvas.Height / 2
            };
            Random random = new Random();
            tank.LocationY = random.Next(50, 720 - 50);
            tank.SetLocation();
            tanks.Add(tank);
            myCanvas.Children.Add(tank);
        }


        private void GameTimer_Tick1(object sender, object e)
        {

            List<Infantry> remove = new List<Infantry>();

            // move ukko
            foreach (Infantry ukko in ukot)
            {
                ukko.Move();
                if (ukko.LocationX >= 1000)
                {
                    remove.Add(ukko);
                }
            }
            // remove all
            foreach (Infantry ukko in remove)
            {
                ukot.Remove(ukko);
                myCanvas.Children.Remove(ukko);
                Debug.WriteLine("poistuu");
            }
            remove.Clear();

            List<Tank> removeT = new List<Tank>();
            // move ukko
            foreach (Tank tank in tanks)
            {
                tank.Move();
                if (tank.LocationX >= 1000)
                {
                    removeT.Add(tank);
                }
            }
            // remove all
            foreach (Tank tank in removeT)
            {
                tanks.Remove(tank);
                myCanvas.Children.Remove(tank);
                Debug.WriteLine("poistuu tämäki");
            }
            removeT.Clear();

            /*List<Bomb> removeB = new List<Bomb>();
 
            foreach (Bomb bomb in bombs)
            {                
                if ()
                {
                    removeB.Add(bomb);
                }
            }
            // remove bomb
            foreach (Bomb bomb in removeB)
            {
                bombs.Remove(bomb);
                myCanvas.Children.Remove(bomb);
            }
            removeB.Clear();*/

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

            foreach (Tank tank in tanks)
            {
                // get rects
                Rect BRect = new Rect(
                    bomb.LocationX, bomb.LocationY, bomb.ActualWidth, bomb.ActualHeight
                    );
                Rect TRect = new Rect(
                    tank.LocationX, tank.LocationY, tank.ActualWidth, tank.ActualHeight
                    );
                // does objects intersect

                Debug.WriteLine(TRect);
                Debug.WriteLine(BRect);

                BRect.Intersect(TRect);

                if (!BRect.IsEmpty)
                {
                    // collision! area not empty
                    // remove ukko from canvas
                    myCanvas.Children.Remove(tank);
                    // remove from list
                    tanks.Remove(tank);

                    break;
                }
            }
        }
    }
}
