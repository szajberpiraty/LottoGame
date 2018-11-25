using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace LottoGame
{
    class Game
    {
        private MainWindow mainwindow;
        private int szamlalo = 0;

        public Game(MainWindow mainwindow)
        {
            this.mainwindow = mainwindow;
        }

        //Most nem, de szépre megoldani, hogy mennyi számmal játszunk
        public void Start()
        {
            ButtonGrid(9, 10);
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
                    buttonGrid.Children.Add(gomb);
                    Grid.SetRow(gomb, i);
                    Grid.SetColumn(gomb,j);


                }
            }
            mainwindow.mainGrid.Children.Add(buttonGrid);
            
        }
    }
}
