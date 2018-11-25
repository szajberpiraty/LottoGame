using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Diagnostics;
using System.Threading;

namespace LottoGame
{
    class Game
    {
        private MainWindow mainwindow;
        private int hanySzam;
        private int szamlalo = 0;
        private int[] tippek;
        private int[] nyeroszamok;
        private int sorSzam;
        private int oszlopSzam;
        private int talalat=0;
        private Random nyeroszam = new Random();

        public Game(MainWindow mainwindow,int hanyszam,int sor,int oszlop)
        {
            this.mainwindow = mainwindow;
            this.hanySzam = hanyszam;
            this.sorSzam = sor;
            this.oszlopSzam = oszlop;

            tippek = new int[hanySzam];
            nyeroszamok = new int[hanySzam];
            mainwindow.buttonUjJatek.Click += UjJatek;
            mainwindow.buttonSorsolas.Click += sorsolasClick;
        }

        //Most nem, de szépre megoldani, hogy mennyi számmal játszunk
        public void Start()
        {
            ButtonGrid(9, 5);
            szamlalo = 0;
            mainwindow.buttonSorsolas.IsEnabled = false;
            mainwindow.buttonUjJatek.IsEnabled = false;
           
        }
        //Annyi rácsból áll a grid, amennyi gomb van
        public void ButtonGrid(int sor,int oszlop)
        {
            Grid buttonGrid = new Grid();
            //Oszlopok hozzáadása
        
            for (int i = 0; i < oszlop; i++)
            {
                ColumnDefinition aktcol = new ColumnDefinition();
                buttonGrid.ColumnDefinitions.Add(aktcol);
            }
            //Sorok hozzáadása
            for (int i = 0; i < sor; i++)
            {
                RowDefinition aktsor = new RowDefinition();
                buttonGrid.RowDefinitions.Add(aktsor);
            }
            //Gombok létrehozása, hozzáadása
            var buttonNumber = 1;
            for (int i = 0; i < sor; i++)
            {
                for (int j = 0; j < oszlop; j++)
                {
                    Button gomb = new Button();
                    gomb.Content = buttonNumber;
                    
                    buttonNumber++;
                    gomb.Padding = new Thickness(5);
                    gomb.Margin = new Thickness(5);
                    gomb.MinWidth = 30;
                    gomb.MinHeight = 30;
                    gomb.Click += buttonClick;
                    

                    buttonGrid.Children.Add(gomb);
                    Grid.SetRow(gomb, i);
                    Grid.SetColumn(gomb,j);


                }
            }
            mainwindow.tippGombok.Children.Add(buttonGrid);
            
        }
        private void buttonClick(object sender,RoutedEventArgs e)
        {
            Button aktButton = (Button)sender;
            if (szamlalo<hanySzam)
            {
                aktButton.Foreground = Brushes.Red;
                aktButton.Background = Brushes.Gold;
                if (Array.IndexOf(tippek,Convert.ToInt32(aktButton.Content))==-1)
                {
                    tippek[szamlalo] = Convert.ToInt32(aktButton.Content);
                    szamlalo++;
                }
                
            }
            //Csak debug!
            if (szamlalo>=hanySzam)
            {
                mainwindow.buttonSorsolas.IsEnabled = true;
                for (int i = 0; i < tippek.Length; i++)
                {
                    Debug.WriteLine(tippek[i]);
                   
                }
                Debug.WriteLine("Szamlalo:{0}", szamlalo);
            }
            
        }
        private void sorsolasClick(object sender,RoutedEventArgs e)
        {
            Sorsolas();
            EredmenyKiemeles();
            mainwindow.buttonSorsolas.IsEnabled = false;
        }

        private void Sorsolas()
        {

            for (int i = 0; i < hanySzam; i++)
            {

                var temp = nyeroszam.Next(1, (sorSzam*oszlopSzam)+1);

                //figyelni, hogy ne legyen ugyanaz a nyerőszám
                while (Array.IndexOf(nyeroszamok, temp) > -1)
                {
                    temp = nyeroszam.Next(1, (sorSzam * oszlopSzam) + 1);
                }
                nyeroszamok[i] = temp;



            }
            Array.Sort(nyeroszamok);

            //Gombokat csinálunk a nyerőszámokból, és a wrappanel-re tesszük
            for (int i = 0; i < nyeroszamok.Length; i++)
            {
                Button nyeroGomb = new Button();
                nyeroGomb.Content = nyeroszamok[i].ToString();
                nyeroGomb.Padding = new Thickness(5);
                nyeroGomb.Margin = new Thickness(5);
                nyeroGomb.MinHeight = 40;
                nyeroGomb.MinWidth = 40;
                mainwindow.nyeroSzamok.Children.Add(nyeroGomb);
                
            }

            //Csak debug
            for (int i = 0; i < nyeroszamok.Length; i++)
            {
                Debug.WriteLine(nyeroszamok[i]);
            }
        }
        private void EredmenyKiemeles()

        {


            foreach (Grid gr in mainwindow.tippGombok.Children)
            {

                foreach (Button ch in gr.Children)
                {
                    if (Array.IndexOf(nyeroszamok, Convert.ToInt32(ch.Content)) > -1)
                    {

                        ch.Foreground = Brushes.DarkBlue;
                        ch.Background = Brushes.Green;

                    }
                    if (Array.IndexOf(tippek, Convert.ToInt32(ch.Content)) > -1 && Array.IndexOf(nyeroszamok, Convert.ToInt32(ch.Content)) > -1)
                    {

                        ch.Foreground = Brushes.DarkBlue;
                        ch.Background = Brushes.Red;

                    }
                }

               
            }
            mainwindow.buttonUjJatek.IsEnabled = true;
           
        }

        private void UjJatek(object sender, RoutedEventArgs e)
        {
            mainwindow.tippGombok.Children.Clear();
            mainwindow.nyeroSzamok.Children.Clear();
            
            tippek = new int[hanySzam];
            
            nyeroszamok = new int[hanySzam];
            
            Start();


        }
        

       
    }
}
