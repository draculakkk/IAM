﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18449
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码是由 Microsoft.VSDesigner 4.0.30319.18449 版自动生成。
// 
#pragma warning disable 1591

namespace IAM.WebReference2 {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="user.cux_user_ws_query.svc_binding", Namespace="http://www.aurora-framework.org/schema")]
    public partial class auto_service : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback executeOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public auto_service() {
            this.Url = global::IAM.Properties.Settings.Default.IAM_WebReference2_auto_service;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event executeCompletedEventHandler executeCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("execute", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("model", Namespace="http://www.aurora-framework.org/schema")]
        public model execute([System.Xml.Serialization.XmlElementAttribute(Namespace="http://www.aurora-framework.org/schema")] parameter parameter) {
            object[] results = this.Invoke("execute", new object[] {
                        parameter});
            return ((model)(results[0]));
        }
        
        /// <remarks/>
        public void executeAsync(parameter parameter) {
            this.executeAsync(parameter, null);
        }
        
        /// <remarks/>
        public void executeAsync(parameter parameter, object userState) {
            if ((this.executeOperationCompleted == null)) {
                this.executeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnexecuteOperationCompleted);
            }
            this.InvokeAsync("execute", new object[] {
                        parameter}, this.executeOperationCompleted, userState);
        }
        
        private void OnexecuteOperationCompleted(object arg) {
            if ((this.executeCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.executeCompleted(this, new executeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18408")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.aurora-framework.org/schema")]
    public partial class parameter {
        
        private string frozen_flagField;
        
        private string company_codeField;
        
        private string role_codeField;
        
        private string ws_passwordField;
        
        private string start_numField;
        
        private string employee_codeField;
        
        private string password_lifespan_accessField;
        
        private string employee_nameField;
        
        private string ws_user_nameField;
        
        private string user_nameField;
        
        private string frozen_dateField;
        
        private string role_end_dateField;
        
        private string end_dateField;
        
        private string end_numField;
        
        private string password_lifespan_daysField;
        
        private string descriptionField;
        
        private string role_start_dateField;
        
        private string start_dateField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string frozen_flag {
            get {
                return this.frozen_flagField;
            }
            set {
                this.frozen_flagField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string company_code {
            get {
                return this.company_codeField;
            }
            set {
                this.company_codeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string role_code {
            get {
                return this.role_codeField;
            }
            set {
                this.role_codeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ws_password {
            get {
                return this.ws_passwordField;
            }
            set {
                this.ws_passwordField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string start_num {
            get {
                return this.start_numField;
            }
            set {
                this.start_numField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string employee_code {
            get {
                return this.employee_codeField;
            }
            set {
                this.employee_codeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string password_lifespan_access {
            get {
                return this.password_lifespan_accessField;
            }
            set {
                this.password_lifespan_accessField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string employee_name {
            get {
                return this.employee_nameField;
            }
            set {
                this.employee_nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ws_user_name {
            get {
                return this.ws_user_nameField;
            }
            set {
                this.ws_user_nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string user_name {
            get {
                return this.user_nameField;
            }
            set {
                this.user_nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string frozen_date {
            get {
                return this.frozen_dateField;
            }
            set {
                this.frozen_dateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string role_end_date {
            get {
                return this.role_end_dateField;
            }
            set {
                this.role_end_dateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string end_date {
            get {
                return this.end_dateField;
            }
            set {
                this.end_dateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string end_num {
            get {
                return this.end_numField;
            }
            set {
                this.end_numField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string password_lifespan_days {
            get {
                return this.password_lifespan_daysField;
            }
            set {
                this.password_lifespan_daysField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string description {
            get {
                return this.descriptionField;
            }
            set {
                this.descriptionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string role_start_date {
            get {
                return this.role_start_dateField;
            }
            set {
                this.role_start_dateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string start_date {
            get {
                return this.start_dateField;
            }
            set {
                this.start_dateField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18408")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.aurora-framework.org/schema")]
    public partial class model {
        
        private modelRecord[] user_recordsField;
        
        private string user_sumField;
        
        private string successField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("record", IsNullable=false)]
        public modelRecord[] user_records {
            get {
                return this.user_recordsField;
            }
            set {
                this.user_recordsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string user_sum {
            get {
                return this.user_sumField;
            }
            set {
                this.user_sumField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string success {
            get {
                return this.successField;
            }
            set {
                this.successField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18408")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.aurora-framework.org/schema")]
    public partial class modelRecord {
        
        private string frozen_flagField;
        
        private string company_codeField;
        
        private string role_codeField;
        
        private string employee_codeField;
        
        private string password_lifespan_accessField;
        
        private string employee_nameField;
        
        private string user_nameField;
        
        private string frozen_dateField;
        
        private string role_end_dateField;
        
        private string end_dateField;
        
        private string password_lifespan_daysField;
        
        private string descriptionField;
        
        private string row_numField;
        
        private string role_start_dateField;
        
        private string start_dateField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string frozen_flag {
            get {
                return this.frozen_flagField;
            }
            set {
                this.frozen_flagField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string company_code {
            get {
                return this.company_codeField;
            }
            set {
                this.company_codeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string role_code {
            get {
                return this.role_codeField;
            }
            set {
                this.role_codeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string employee_code {
            get {
                return this.employee_codeField;
            }
            set {
                this.employee_codeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string password_lifespan_access {
            get {
                return this.password_lifespan_accessField;
            }
            set {
                this.password_lifespan_accessField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string employee_name {
            get {
                return this.employee_nameField;
            }
            set {
                this.employee_nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string user_name {
            get {
                return this.user_nameField;
            }
            set {
                this.user_nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string frozen_date {
            get {
                return this.frozen_dateField;
            }
            set {
                this.frozen_dateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string role_end_date {
            get {
                return this.role_end_dateField;
            }
            set {
                this.role_end_dateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string end_date {
            get {
                return this.end_dateField;
            }
            set {
                this.end_dateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string password_lifespan_days {
            get {
                return this.password_lifespan_daysField;
            }
            set {
                this.password_lifespan_daysField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string description {
            get {
                return this.descriptionField;
            }
            set {
                this.descriptionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string row_num {
            get {
                return this.row_numField;
            }
            set {
                this.row_numField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string role_start_date {
            get {
                return this.role_start_dateField;
            }
            set {
                this.role_start_dateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string start_date {
            get {
                return this.start_dateField;
            }
            set {
                this.start_dateField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    public delegate void executeCompletedEventHandler(object sender, executeCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.18408")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class executeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal executeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public model Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((model)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591