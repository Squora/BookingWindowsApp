﻿#pragma checksum "..\..\..\PasswordRecoveryPage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "811C0AC774B43F69BAEC4B15BEE17078D3271B52"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using BookingApp;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace BookingApp {
    
    
    /// <summary>
    /// PasswordRecoveryPage
    /// </summary>
    public partial class PasswordRecoveryPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\PasswordRecoveryPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel SpRecover;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\PasswordRecoveryPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TbLogin;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\PasswordRecoveryPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel SpCodeEnter;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\PasswordRecoveryPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TbCode;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\PasswordRecoveryPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel SpChangePassword;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\PasswordRecoveryPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TbNewPassword;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\PasswordRecoveryPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TbRepeatNewPassword;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/BookingApp;component/passwordrecoverypage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\PasswordRecoveryPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.SpRecover = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 2:
            this.TbLogin = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            
            #line 16 "..\..\..\PasswordRecoveryPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BtnRecoverPassword_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.SpCodeEnter = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 5:
            this.TbCode = ((System.Windows.Controls.TextBox)(target));
            
            #line 21 "..\..\..\PasswordRecoveryPage.xaml"
            this.TbCode.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TbCode_TextChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.SpChangePassword = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 7:
            this.TbNewPassword = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.TbRepeatNewPassword = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            
            #line 29 "..\..\..\PasswordRecoveryPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BtnChangePassword_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
