﻿using System;
using System.Collections.Generic;
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

namespace Nirvana.Views
{
    /// <summary>
    /// Класс для передачи параметров из дочернего окна в родительское
    /// </summary>
    public class DataSms
    {
        
    }

    /// <summary>
    /// Логика взаимодействия для GetSmsCode.xaml
    /// </summary>
    public partial class GetSmsCode : Window
    {
        public String smsCode { get; private set; }
        public GetSmsCode()
        {
            InitializeComponent();
        }

        private void textBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (smsBox.Text != String.Empty)
                {
                    smsCode = smsBox.Text;
                    DialogResult = true;
                }
            }
        }

        private void smsBox_MouseLeave(object sender, MouseEventArgs e)
        {
            smsBox.Select(0, 0);
            smsLabel.Focus();
        }

        //private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    //this.smsBox.Text = String.Empty;
        //    e.Cancel = true;
        //    Hide();
        //}
    }
}
