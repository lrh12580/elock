﻿#pragma checksum "..\..\CMessageBoxWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "A903C27D573FF27070B00109711E3A36"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace DesktopApplication {
    
    
    /// <summary>
    /// CMessageBoxWindow
    /// </summary>
    public partial class CMessageBoxWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 1 "..\..\CMessageBoxWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DesktopApplication.CMessageBoxWindow window;
        
        #line default
        #line hidden
        
        
        #line 105 "..\..\CMessageBoxWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid grid;
        
        #line default
        #line hidden
        
        
        #line 158 "..\..\CMessageBoxWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button YesButton;
        
        #line default
        #line hidden
        
        
        #line 161 "..\..\CMessageBoxWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button NoButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/DesktopApplication;component/cmessageboxwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\CMessageBoxWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.window = ((DesktopApplication.CMessageBoxWindow)(target));
            
            #line 7 "..\..\CMessageBoxWindow.xaml"
            this.window.Loaded += new System.Windows.RoutedEventHandler(this.Win_Loaded);
            
            #line default
            #line hidden
            
            #line 7 "..\..\CMessageBoxWindow.xaml"
            this.window.Closed += new System.EventHandler(this.Win_Closed);
            
            #line default
            #line hidden
            return;
            case 2:
            this.grid = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            
            #line 119 "..\..\CMessageBoxWindow.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.OnMouseLeftButtonDownAtTitlee);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 131 "..\..\CMessageBoxWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.MinWindowButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 135 "..\..\CMessageBoxWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CloseWindowButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.YesButton = ((System.Windows.Controls.Button)(target));
            
            #line 158 "..\..\CMessageBoxWindow.xaml"
            this.YesButton.Click += new System.Windows.RoutedEventHandler(this.YesButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.NoButton = ((System.Windows.Controls.Button)(target));
            
            #line 161 "..\..\CMessageBoxWindow.xaml"
            this.NoButton.Click += new System.Windows.RoutedEventHandler(this.NoButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

