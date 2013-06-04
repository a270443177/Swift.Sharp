// ---------------------------------------------------------------------------
// <copyright file="CommandSetServerSettings.cs" company="Walletex Microelectronics LTD">
//     Copyright (c) Walletex Microelectronics LTD, Israel 2011. All rights reserved.
//     Author: alex
//     Date: 27-5-2013
// </copyright>
// -----------------------------------------------------------------------------
namespace SwiftSharp.Gui.ViewModel
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// Command for 'Save SWIFT server settings'
    /// </summary>
    public class CommandSetServerSettings : ICommand
    {
        /// <summary>
        /// Main view-model
        /// </summary>
        private MainViewModel viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandSetServerSettings"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public CommandSetServerSettings(MainViewModel viewModel)
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
            return this.viewModel.CanExecuteCommandSaveServerSettings(parameter);
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public void Execute(object parameter)
        {
            this.viewModel.ExecuteCommandSaveServerSettings(parameter);
        }
    }
}
