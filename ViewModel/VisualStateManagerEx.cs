// ---------------------------------------------------------------------------
// <copyright file="VisualStateManagerEx.cs" company="Walletex Microelectronics LTD">
//     Copyright (c) Walletex Microelectronics LTD, Israel 2011. All rights reserved.
//     Author: alex
//     Date: 27-5-2013
// </copyright>
// -----------------------------------------------------------------------------
namespace SwiftSharp.Gui.ViewModel
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Class will implement attached property to switch Window to Visual State
    /// </summary>
    public static class VisualStateManagerEx
    {
        /// <summary>
        /// Gets the state of the visual.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public static string GetVisualState(DependencyObject obj)
        {
            return (string)obj.GetValue(VisualStateProperty);
        }

        /// <summary>
        /// Sets the state of the visual.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">The value.</param>
        public static void SetVisualState(DependencyObject obj, string value)
        {
            obj.SetValue(VisualStateProperty, value);
        }

        /// <summary>
        /// DP for 'VisualState'
        /// </summary>
        public static readonly DependencyProperty VisualStateProperty =
            DependencyProperty.RegisterAttached(
                "VisualState", 
                typeof(string), 
                typeof(VisualStateManagerEx), 
                new PropertyMetadata(
                                null,
                                new PropertyChangedCallback((dependencyObject, evenArgs) =>
                                {
                                        //Control changeStateControl = o as Control;
                                        //if (changeStateControl == null)
                                        //{
                                        //    throw (new Exception("VisualState works only on Controls"));
                                        //}
                                    
                                        //VisualStateManager.GoToState(changeStateControl, e.NewValue.ToString(), true);

                                        var frameworkElement = dependencyObject as FrameworkElement;
                                        if (frameworkElement == null)
                                        {
                                            return;
                                        }
                                        
                                        VisualStateManager.GoToElementState(frameworkElement, (string)evenArgs.NewValue, true);

                                        System.Diagnostics.Trace.WriteLine("Visual state changed to " + evenArgs.NewValue.ToString(), "[VisualStateManagerEx]");
                                    })
                        )
            );
    }
}
