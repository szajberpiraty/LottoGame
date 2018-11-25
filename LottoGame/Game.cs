using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Diagnostics;

namespace LottoGame
{
    class Game
    {
        private MainWindow mainwindow;
        private int hanySzam;
        private int szamlalo = 0;
        private int[] tippek;
        private int[] nyeroszamok;
        private int talalat=0;

        public Game(MainWindow mainwindow,int hanyszam)
        {
            this.mainwindow = mainwindow;
            this.hanySzam = hanyszam;
            tippek = new int[hanySzam];
            nyeroszamok = new int[hanySzam];
        }

        //Most nem, de szépre megoldani, hogy mennyi számmal játszunk
        public void Start()
        {
            ButtonGrid(9, 10);
            szamlalo = 0;
        }
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
                    //gomb.Content = i + j.ToString();
                    buttonNumber++;
                    gomb.Padding = new Thickness(5);
                    gomb.Margin = new Thickness(5);
                    gomb.MinWidth = 30;
                    gomb.MinHeight = 30;
                    gomb.Click += buttonClick;
                    gomb.Click += szamlaloNovel;

                    buttonGrid.Children.Add(gomb);
                    Grid.SetRow(gomb, i);
                    Grid.SetColumn(gomb,j);


                }
            }
            mainwindow.mainGrid.Children.Add(buttonGrid);
            
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
                }
                
            }
            //Csak debug!
            if (szamlalo>=hanySzam)
            {
                for (int i = 0; i < tippek.Length; i++)
                {
                    Debug.WriteLine(tippek[i]);
                }
            }
            
        }
        private void szamlaloNovel(object sender, RoutedEventArgs e)
        {
            szamlalo +=1;
            Debug.WriteLine(szamlalo);
        }
    }
}
