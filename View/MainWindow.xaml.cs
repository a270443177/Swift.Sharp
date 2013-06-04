// ---------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="">
//     Copyright (c) Israel 2013. All rights reserved.
//     Author: alex
//     Date: 4-6-2013
// </copyright>
// -----------------------------------------------------------------------------
namespace SwiftSharp.Gui.View
{
    using MahApps.Metro.Controls;
    using SwiftSharp.Gui.ViewModel;
    using System.ComponentModel;
    using System.Windows;
    using System;

    /// <summary>
    /// Interaction logic for MainWindow
    /// </summary>
    public partial class MainWindow // : MetroWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        /// <param name="viewModel">View model object</param>
        public MainWindow(MainViewModel viewModel) //: base()
        {
            //
            // Set Falyouts
            if (viewModel != null)
            {
                viewModel.Flyouts = this.Flyouts;
                viewModel.Window = this;
            }
            

            this.DataContext = this.ViewModel = viewModel;

            InitializeComponent();
        }

        /// <summary>
        /// Gets the view model.
        /// </summary>
        /// <value>
        /// The view model.
        /// </value>
        public MainViewModel ViewModel
        {
            get;
            set;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //VisualStateManager.GoToState(grid, "BusyState", true);
            //VisualStateManager.GoToElementState(grid, "BusyState", true);

            this.ViewModel.ViewModelVisualState = "BusyState";
        }
    }
}
