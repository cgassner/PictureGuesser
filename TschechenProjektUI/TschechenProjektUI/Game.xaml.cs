using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TschechenProjektUI
{
    /// <summary>
    /// Interaktionslogik für Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        static WrapPanel[] covering;  
        static int numbercols=7;//Zeilen und Spalten werden vom Server übergeben
        static int numberrows=4;

        public Game()
        {
            InitializeComponent();
            Cover();
            //Thread thread = new Thread(Remove);
            //for (int i = 0; i < numbercols*numberrows; i++)
            //{
            //    thread.Start();
            //    Thread.Sleep(2000);
            //    thread.Join();
            //}
            

            //DispatcherTimer timer = new DispatcherTimer();
            //timer.Tick += Remove; //Zeilen und Spalten werden vom Server übergeben
            //timer.Interval = new TimeSpan(0, 0, 5);//Zeit wird vom Server übergeben
            //timer.Start();

        }
        public async void Remove()
        {
            Random r = new Random();
            int which = 0;
            do {
                which = r.Next(1, covering.Length);
            }
            while (covering[which] == null);
            covering[which] = null;
            CoverCanvas.Children.Remove(covering[which]);           //das geht nicht wirkli... :( warten auf gassner . . . 
            //for (int i = 0; i < numberrows; i++)
            //{
            //    for (int j = 0; j < numbercols; j++)
            //    {
            //        if (covering[which] != null)
            //        {
            //            CoverCanvas.Children.Add(covering[i * j]);
            //        }

            //    }
            //}
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var text = textboxguess.Text;
            MessageBox.Show("your guess: " + text);
        }
        public void Cover()
        {
            bool[,] iscovered = new bool[numberrows, numbercols];
            covering = new WrapPanel[numberrows* numbercols];
            double height = image.Height / numberrows;
            double width = image.Width / numbercols;

            for (int i = 0; i < numberrows; i++)
            {
                for (int j = 0; j < numbercols; j++)
                {
                    iscovered[i, j] = true;
                    covering[i* j] = new WrapPanel() { Height = height, Width = width, Background = new SolidColorBrush(Color.FromRgb(0, 0, 0)) };
                    Canvas.SetTop(covering[i* j], i*height);
                    Canvas.SetLeft(covering[i* j], j*width);
                    CoverCanvas.Children.Add(covering[i* j]);
                    
                }
            }
        }
    }
}
