// ---------------------------------------------------------------------------
// <copyright file="ServerSettings.cs" company="">
//     Copyright (c) Israel 2013. All rights reserved.
//     Author: alex
//     Date: 4-6-2013
// </copyright>
// -----------------------------------------------------------------------------
namespace SwiftSharp.Gui.ViewModel
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// SWIFT server settings
    /// </summary>
    public class ServerSettings : DependencyObject, IDataErrorInfo 
    {
        /// <summary>
        /// Using a DependencyProperty as the backing store for Address.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty AddressProperty = DependencyProperty.Register("Address", 
                                                                                                typeof(string), 
                                                                                                typeof(ServerSettings), 
                                                                                                new FrameworkPropertyMetadata(string.Empty));
        /// <summary>
        /// Using a DependencyProperty as the backing store for Username.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty UsernameProperty = DependencyProperty.Register("Username", 
                                                                                                typeof(string), 
                                                                                                typeof(ServerSettings), 
                                                                                                new FrameworkPropertyMetadata(string.Empty));

        /// <summary>
        /// Using a DependencyProperty as the backing store for Password.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty PasswordProperty = DependencyProperty.Register("Password", 
                                                                                                typeof(string), 
                                                                                                typeof(ServerSettings),
                                                                                                new FrameworkPropertyMetadata(string.Empty));

        /// <summary>
        /// The main view-model
        /// </summary>
        private MainViewModel viewModel = null;

        private ICommand commandSaveSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerSettings"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        internal ServerSettings(MainViewModel viewModel)
        {
            this.viewModel = viewModel;
            this.Address = "localhost";
        }

        /// <summary>
        /// Gets or sets the address of SWIFT server
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public string Address
        {
            get 
            { 
                return (string)GetValue(AddressProperty); 
            }
            
            set 
            { 
                SetValue(AddressProperty, value); 
            }
        }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username
        {
            get 
            { 
                return (string)GetValue(UsernameProperty); 
            }
            
            set 
            { 
                SetValue(UsernameProperty, value); 
            }
        }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password
        {
            get 
            { 
                return (string)GetValue(PasswordProperty); 
            }

            set 
            { 
                SetValue(PasswordProperty, value); 
            }
        }

        /// <summary>
        /// Gets the command save server settings.
        /// </summary>
        /// <value>
        /// The command save server settings.
        /// </value>
        public ICommand CommandSaveServerSettings
        {
            get
            {
                if (this.commandSaveSettings == null)
                {
                    this.commandSaveSettings = new CommandSetServerSettings(viewModel);
                }

                return this.commandSaveSettings;
            }
        }

        /// <summary>
        /// Gets an error message indicating what is wrong with this object.
        /// </summary>
        /// <returns>An error message indicating what is wrong with this object. The default is an empty string ("").</returns>
        public string Error
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the error message for the property with the given name.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>An error message indicating what is wrong with this object. The default is an empty string ("").</returns>
        public string this[string columnName]
        {
            get 
            {
                this.Error = string.Empty;

                switch (columnName)
                {
                    case "Address":
                        try
                        {
                            Uri uri = new Uri(this.Address);
                        }
                        catch (UriFormatException exp_format)
                        {
                            System.Diagnostics.Debug.WriteLine("Provided address could not be converted to Uri. Additional information: " + exp_format.Message);
                            this.Error = "Wrong address";
                        }
                        break;
                    case "Username":
                        //
                        // Could be anything, even empty
                        break;
                    case "Password":
                        //
                        // Could be anything, even empty
                        break;
                    default:
                        //
                        // WTF ?
                        break;
                }

                return this.Error;
            }
        }
    }
}
