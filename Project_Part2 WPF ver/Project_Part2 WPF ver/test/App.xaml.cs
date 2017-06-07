﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace test
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.Startup += App_StartUp;
        }

        void App_StartUp(object sender, StartupEventArgs e)
        {
            test.MainWindow.Instance.Show();
        }

    }
}