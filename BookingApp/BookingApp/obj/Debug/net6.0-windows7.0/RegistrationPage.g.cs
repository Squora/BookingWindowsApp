﻿#pragma checksum "..\..\..\RegistrationPage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6736163B73F4E6E792217BA1E4A71583F1AE9378"
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
    /// RegistrationPage
    /// </summary>
    public partial class RegistrationPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 27 "..\..\..\RegistrationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TbFirstName;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\RegistrationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TbLastName;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\RegistrationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TbMiddleName;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\RegistrationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TbPassportDetails;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\RegistrationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TbPhoneNumber;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\RegistrationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TbEmail;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\RegistrationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LblCheckCode;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\RegistrationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TbCheckCode;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\RegistrationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox PasswordBox;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\RegistrationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LblPasswordStrength;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.1.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/BookingApp;component/registrationpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\RegistrationPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.1.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 19 "..\..\..\RegistrationPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BtnBack_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.TbFirstName = ((System.Windows.Controls.TextBox)(target));
            
            #line 27 "..\..\..\RegistrationPage.xaml"
            this.TbFirstName.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.TbFirstName_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 3:
            this.TbLastName = ((System.Windows.Controls.TextBox)(target));
            
            #line 30 "..\..\..\RegistrationPage.xaml"
            this.TbLastName.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.TbLastName_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 4:
            this.TbMiddleName = ((System.Windows.Controls.TextBox)(target));
            
            #line 33 "..\..\..\RegistrationPage.xaml"
            this.TbMiddleName.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.TbMiddleName_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 5:
            this.TbPassportDetails = ((System.Windows.Controls.TextBox)(target));
            
            #line 36 "..\..\..\RegistrationPage.xaml"
            this.TbPassportDetails.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.TbPassportDetails_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 6:
            this.TbPhoneNumber = ((System.Windows.Controls.TextBox)(target));
            
            #line 39 "..\..\..\RegistrationPage.xaml"
            this.TbPhoneNumber.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.TbPhoneNumber_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 7:
            this.TbEmail = ((System.Windows.Controls.TextBox)(target));
            
            #line 42 "..\..\..\RegistrationPage.xaml"
            this.TbEmail.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.TbEmail_PreviewTextInput);
            
            #line default
            #line hidden
            
            #line 42 "..\..\..\RegistrationPage.xaml"
            this.TbEmail.LostFocus += new System.Windows.RoutedEventHandler(this.TbEmail_LostFocus);
            
            #line default
            #line hidden
            return;
            case 8:
            this.LblCheckCode = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.TbCheckCode = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.PasswordBox = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 47 "..\..\..\RegistrationPage.xaml"
            this.PasswordBox.PasswordChanged += new System.Windows.RoutedEventHandler(this.PasswordBox_PasswordChanged);
            
            #line default
            #line hidden
            return;
            case 11:
            this.LblPasswordStrength = ((System.Windows.Controls.Label)(target));
            return;
            case 12:
            
            #line 50 "..\..\..\RegistrationPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BtnRegister_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
