// ---------------------------------------------------------------------------
// <copyright file="MainViewModel.cs" company="">
//     Copyright (c) Israel 2013. All rights reserved.
//     Author: alex
//     Date: 4-6-2013
// </copyright>
// -----------------------------------------------------------------------------
namespace SwiftSharp.Gui.ViewModel
{
    using MahApps.Metro;
    using MahApps.Metro.Controls;
    using SwiftSharp.Gui.Model;
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Input;

    /// <summary>
    /// Main 'View-Model' for app
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Model to use
        /// </summary>
        private MainModel model;

        /// <summary>
        /// SWIFT server settings
        /// </summary>
        private ServerSettings serverSettings;

        /// <summary>
        /// Command that execute show/hide 'settings' window
        /// </summary>
        private ICommand commandSettings = null;

        /// <summary>
        /// The view model visual state
        /// </summary>
        private string viewModelVisualState = string.Empty;

        private const string VM_NORMAL = "NormalState";

        private const string VM_BUSY = "BusyState";

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        internal MainViewModel(MainModel model)
        {
            this.model = model;
            this.serverSettings = new ServerSettings(this);

            this.viewModelVisualState = VM_NORMAL;
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the settings command 
        /// </summary>
        /// <value>
        /// The settings command 
        /// </value>
        public ICommand CommandSettings
        {
            get
            {
                if (this.commandSettings == null)
                {
                    this.commandSettings = new CommandSettings(this);
                }

                return this.commandSettings;
            }
        }

        /// <summary>
        /// Determines whether this instance [can execute command settings] the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        ///   <c>true</c> if this instance [can execute command settings] the specified parameter; otherwise, <c>false</c>.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "parameter", Justification = "By design")]
        internal bool CanExecuteCommandSettings(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Executes the command settings.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "parameter", Justification = "By design")]
        internal void ExecuteCommandSettings(object parameter)
        {
            if (this.Flyouts == null)
            {
                throw new System.NotImplementedException(SwiftSharp.Gui.Properties.Resources.CRIT_ERROR_NO_FALYOUTS);
            }

            HideShowSettingsWindow(true);
        }

        /// <summary>
        /// Hides the show 'settings window'.
        /// </summary>
        /// <param name="visible">if set to <c>true</c> [visible].</param>
        private void HideShowSettingsWindow(bool visible)
        {
            var bottomFlayout = this.Flyouts.Where(f => f.Name.Equals("flConnection", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (bottomFlayout != null)
            {
                bottomFlayout.IsOpen = visible;
            }
        }

        /// <summary>
        /// Gets or sets the flyouts from MainWindow
        /// </summary>
        /// <value>
        /// The flyouts.
        /// </value>
        internal ObservableCollection<Flyout> Flyouts
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the state of the view model visual.
        /// </summary>
        /// <value>
        /// The state of the view model visual.
        /// </value>
        public string ViewModelVisualState
        {
            get
            {
                return viewModelVisualState;
            }

            set
            {
                viewModelVisualState = value;
                RaisePropertyChanged("ViewModelVisualState");
            }
        }

        /// <summary>
        /// Main 'window' class (used in Accent changes)
        /// </summary>
        public System.Windows.Window Window
        {
            get;
            set;
        }

        /// <summary>
        /// Raises the property changed.
        /// </summary>
        /// <param name="propName">Name of the prop.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate", Justification = "By design")]
        protected void RaisePropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        /// <summary>
        /// Gets or sets the server settings.
        /// </summary>
        /// <value>
        /// The server settings.
        /// </value>
        public ServerSettings ServerSettings
        {
            get 
            { 
                return this.serverSettings; 
            }

            set
            {
                this.serverSettings = value;
                this.RaisePropertyChanged("ServerSettings");
            }
        }

        /// <summary>
        /// Determines whether this instance [can execute command save server settings] the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        ///   <c>true</c> if this instance [can execute command save server settings] the specified parameter; otherwise, <c>false</c>.
        /// </returns>
        internal bool CanExecuteCommandSaveServerSettings(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Executes the command save server settings.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        internal void ExecuteCommandSaveServerSettings(object parameter)
        {
            System.Windows.Controls.PasswordBox pswdBox = parameter as System.Windows.Controls.PasswordBox;
            if (pswdBox == null)
            {
                throw new ApplicationException("Parameter is incorrect!");
            }

            System.Diagnostics.Debug.WriteLine("Parameter is: " + parameter.ToString());
            ChangeAccent("Orange");
            HideShowSettingsWindow(false);
            ViewModelVisualState = VM_BUSY;
        }

        /// <summary>
        /// Changes the accent (window border color)
        /// </summary>
        /// <param name="accentName">Name of the accent.</param>
        private void ChangeAccent(string accentName)
        {
            Accent currentAccent = ThemeManager.DefaultAccents.First(x => x.Name == accentName);

            if (this.Window != null)
            {
                ThemeManager.ChangeTheme(this.Window, currentAccent, Theme.Light);
            }
        }
    }
}
