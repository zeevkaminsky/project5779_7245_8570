﻿using BL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL_Wpf
{
    /// <summary>
    /// Interaction logic for RemoveTraineeWin.xaml
    /// </summary>
    public partial class RemoveTraineeWin : Window
    {
        private ObservableCollection<string> trainees = new ObservableCollection<string>();
        public RemoveTraineeWin()
        {
            InitializeComponent();
            try
            {
                IBl _bl = FactorySingletonBl.GetBl();
                //combo box with trainee ID's
                foreach (var item in _bl.GetTrainees())
                {
                    trainees.Add(item.ID);
                }
                IDCBox.ItemsSource = trainees;
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IDCBox.SelectedItem != null)
                {
                    IBl _bl = FactorySingletonBl.GetBl();
                    //get all tests off trainee 
                    var traineesInTest = from t in _bl.GetTests()
                                  where t.TraineeID == IDCBox.SelectedItem.ToString()
                                  select t;
                    if (traineesInTest.Any())//first need to remove the tests
                    {
                        throw new Exception("trainee can not be remove. need to remove all tests that he was the trainee");
                    }
                    MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete Trainee", System.Windows.MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        
                        string IDToRemove = IDCBox.SelectedItem as string;
                        if (_bl.RemoveTrainee(IDToRemove))
                        {
                            trainees.Remove(IDToRemove);
                            MessageBox.Show(IDToRemove + " removed successfully");
                            this.Close();
                        }
                    }
                }
                
            }
            catch (Exception m)
            {
                MessageBox.Show(m.Message);
            }
        }
    }
}
