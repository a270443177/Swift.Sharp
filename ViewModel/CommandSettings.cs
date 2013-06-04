// ---------------------------------------------------------------------------
// <copyright file="CommandSettings.cs" company="Walletex Microelectronics LTD">
//     Copyright (c) Walletex Microelectronics LTD, Israel 2011. All rights reserved.
//     Author: alex
//     Date: 26-5-2013
// </copyright>
// -----------------------------------------------------------------------------
namespace SwiftSharp.Gui.ViewModel
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// Command for 'Settings'
    /// </summary>
    public class CommandSettings : ICommand
    {
        /// <summary>
        /// View model
        /// </summary>
        private MainViewModel viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandSettings"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public CommandSettings(MainViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        public bool CanExecute(object parameter)
        {
            return this.viewModel.CanExecuteCommandSettings(parameter);
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public void Execute(object parameter)
        {
            this.viewModel.ExecuteCommandSettings(parameter);
        }
    }
}
