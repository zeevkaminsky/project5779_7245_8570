﻿using BE;
using BL;
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
    /// Interaction logic for AddTrainee.xaml
    /// </summary>
    public partial class AddTrainee : Window
    {
        IBl _bl;
        private BE.Trainee trainee;
        
        public AddTrainee()
        {

            InitializeComponent();
            trainee = new BE.Trainee();
            _bl = FactorySingletonBl.GetBl();
            init();
            

        }
        public AddTrainee(BE.Trainee traineeToUp)//update send an existing trainee
        {
            InitializeComponent();
            trainee = traineeToUp;
            this.IDTBox.IsReadOnly = true;
            _bl = FactorySingletonBl.GetBl();
            init();
            this.AddTraineeButton.Content = "Update";
            
        }
        private void init()
        {
            this.InnerGrid.DataContext = trainee;

            GearCBox.ItemsSource = Enum.GetValues(typeof(Gear));
            CityCBox.ItemsSource = Enum.GetValues(typeof(Cities));
            GenderCBox.ItemsSource = Enum.GetValues(typeof(Gender));
            VehicleCBox.ItemsSource = Enum.GetValues(typeof(Vehicle));
        }

        private void AddTraineeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AddTraineeButton.Content.ToString() != "Update")//add for the first time
                {
                    IBl _bl = FactorySingletonBl.GetBl();
                    if (_bl.AddTrainee(trainee))
                    {
                        MessageBox.Show(trainee.ToString() + "added successfully");
                    }
                }
                else//update
                {
                    IBl _bl = FactorySingletonBl.GetBl();

                    if (_bl.UpdateTrainee(trainee))
                    {
                        MessageBox.Show(trainee.ToString() + "updated successfully");
                    }
                }
                this.Close();

            }
            catch (Exception m)
            {

                MessageBox.Show(m.Message);
            }

        }
        #region input_validations
        private void IDTBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Extra.NumbersValidate(sender, e);
        }

        private void PhoneTBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Extra.NumbersValidate(sender, e);
        }

        private void NumberTBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Extra.NumbersValidate(sender, e);
        }

        private void LessonsTBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Extra.NumbersValidate(sender, e);
        }
        #endregion
    }
}
