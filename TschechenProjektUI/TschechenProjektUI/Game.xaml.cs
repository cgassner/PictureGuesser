using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
    public partial class GameObject : Window
    {
        static WrapPanel[,] covering;
        static readonly HttpClient client = new HttpClient();
        Random r = null;
        int which1 = 0, which2 = 0;
        static int numbercols=7;//Zeilen und Spalten werden vom Server übergeben
        static int numberrows=4;
        float diff;
        string cat;

        public GameObject()
        {
            InitializeComponent();
            foreach (Window item in Application.Current.Windows)
            {
                if (item.Name == "Window1")
                {
                    diff=Convert.ToSingle(((Options)item).slider1.Value);
                    cat=((Options)item).comboBox_Copy.Text;
                }
            }
            Game_ fertigesgame= CreateGameAsync(diff, cat).Result;
            Cover();
            
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Remove;
            timer.Interval = new TimeSpan(0, 0, 1);//Zeit wird vom Server übergeben
            timer.Start();
        }

        static async Task<Game_> CreateGameAsync(float difficultyScale, string category)
        {
            Game_ game = new Game_();
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                var response = await client.PostAsJsonAsync("http://77.244.251.110:81/api/games", new GameStartObject
                {
                    difficultyScale = difficultyScale,
                    category = category
                });
                var answer = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(answer);
                Difficulty difficulty = new Difficulty();

                foreach (var item in json.Children())   // macht ein richtiges Game...
                {
                    switch (item.Path)
                    {
                        case "id":
                            game.Id = (Guid)item.Last;
                            break;
                        case "pictureID":
                            game.pictureID = (Guid)item.Last;
                            break;
                        case "isFinished":
                            game.isFinished = (bool)item.Last;
                            break;
                        case "difficulty":
                            foreach (var item2 in item.Children().Children())
                            {
                                switch (item2.Path)
                                {
                                    case "difficulty.difficultyScale":
                                        difficulty.DifficultyScale = (float)item2.Last;
                                        break;
                                    case "difficulty.rows":
                                        difficulty.rows = (int)item2.Last;
                                        break;
                                    case "difficulty.cols":
                                        difficulty.cols = (int)item2.Last; 
                                        break;
                                    case "difficulty.revealDelay":
                                        difficulty.revealDelay = (float)item2.Last; 
                                        break;
                                    default:
                                        break;
                                }
                            }
                            break;
                        default:
                            MessageBox.Show("Da war was falsch...");
                            break;
                    }
                }
                game.Difficulty = difficulty;
                MessageBox.Show(Convert.ToString(game.Difficulty.revealDelay)+" "+ Convert.ToString(game.Difficulty.rows) + " " + Convert.ToString(game.Difficulty.cols) + " " + Convert.ToString(game.Difficulty.DifficultyScale) + " " + Convert.ToString(game.Id) + " " + Convert.ToString(game.isFinished) + " " + Convert.ToString(game.pictureID) + " " );
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show("\nException Caught!");
                MessageBox.Show("Message :{0} ", e.Message);
            }
            return game;
        }

        public void Remove(object sender, EventArgs e)
        {
            r = new Random();
            do {                                                //wenn alles aufgedeckt wurde, rennt das in endlosschleife! ! ! ! ! ! ! ! !
                which1 = r.Next(0, covering.GetLength(0));
                which2 = r.Next(0, covering.GetLength(1));
            }
            while (covering[which1,which2] == null);
            CoverCanvas.Children.Remove(covering[which1, which2]);
            covering[which1,which2] = null;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var text = textboxguess.Text;
            MessageBox.Show("your guess: " + text);
        }
        public void Cover()
        {
            bool[,] iscovered = new bool[numberrows, numbercols];
            covering = new WrapPanel[numberrows, numbercols];
            double height = image.Height / numberrows;
            double width = image.Width / numbercols;

            for (int i = 0; i < numberrows; i++)
            {
                for (int j = 0; j < numbercols; j++)
                {
                    iscovered[i, j] = true;
                    covering[i, j] = new WrapPanel() { Height = height, Width = width, Background = new SolidColorBrush(Color.FromRgb(0, 0, 0)) };
                    Canvas.SetTop(covering[i, j], i*height);
                    Canvas.SetLeft(covering[i, j], j*width);
                    CoverCanvas.Children.Add(covering[i, j]);
                }
            }
        }
    }
}
