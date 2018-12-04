﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestWCFService.SVCPersonInfo {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Person", Namespace="http://schemas.datacontract.org/2004/07/WCFService")]
    [System.SerializableAttribute()]
    public partial class Person : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SVCPersonInfo.IPersonInfo")]
    public interface IPersonInfo {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersonInfo/PostPersonInfo", ReplyAction="http://tempuri.org/IPersonInfo/PostPersonInfoResponse")]
        void PostPersonInfo(string personinfo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersonInfo/PostPersonInfo", ReplyAction="http://tempuri.org/IPersonInfo/PostPersonInfoResponse")]
        System.Threading.Tasks.Task PostPersonInfoAsync(string personinfo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersonInfo/GetPersonInfo", ReplyAction="http://tempuri.org/IPersonInfo/GetPersonInfoResponse")]
        TestWCFService.SVCPersonInfo.Person GetPersonInfo(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersonInfo/GetPersonInfo", ReplyAction="http://tempuri.org/IPersonInfo/GetPersonInfoResponse")]
        System.Threading.Tasks.Task<TestWCFService.SVCPersonInfo.Person> GetPersonInfoAsync(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersonInfo/postMostStr", ReplyAction="http://tempuri.org/IPersonInfo/postMostStrResponse")]
        void postMostStr(System.IO.Stream getStream);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersonInfo/postMostStr", ReplyAction="http://tempuri.org/IPersonInfo/postMostStrResponse")]
        System.Threading.Tasks.Task postMostStrAsync(System.IO.Stream getStream);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersonInfo/DownLoadFile", ReplyAction="http://tempuri.org/IPersonInfo/DownLoadFileResponse")]
        System.IO.Stream DownLoadFile(string downfile);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersonInfo/DownLoadFile", ReplyAction="http://tempuri.org/IPersonInfo/DownLoadFileResponse")]
        System.Threading.Tasks.Task<System.IO.Stream> DownLoadFileAsync(string downfile);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersonInfo/postText", ReplyAction="http://tempuri.org/IPersonInfo/postTextResponse")]
        string postText();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersonInfo/postText", ReplyAction="http://tempuri.org/IPersonInfo/postTextResponse")]
        System.Threading.Tasks.Task<string> postTextAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IPersonInfoChannel : TestWCFService.SVCPersonInfo.IPersonInfo, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PersonInfoClient : System.ServiceModel.ClientBase<TestWCFService.SVCPersonInfo.IPersonInfo>, TestWCFService.SVCPersonInfo.IPersonInfo {
        
        public PersonInfoClient() {
        }
        
        public PersonInfoClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public PersonInfoClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PersonInfoClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PersonInfoClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void PostPersonInfo(string personinfo) {
            base.Channel.PostPersonInfo(personinfo);
        }
        
        public System.Threading.Tasks.Task PostPersonInfoAsync(string personinfo) {
            return base.Channel.PostPersonInfoAsync(personinfo);
        }
        
        public TestWCFService.SVCPersonInfo.Person GetPersonInfo(string id) {
            return base.Channel.GetPersonInfo(id);
        }
        
        public System.Threading.Tasks.Task<TestWCFService.SVCPersonInfo.Person> GetPersonInfoAsync(string id) {
            return base.Channel.GetPersonInfoAsync(id);
        }
        
        public void postMostStr(System.IO.Stream getStream) {
            base.Channel.postMostStr(getStream);
        }
        
        public System.Threading.Tasks.Task postMostStrAsync(System.IO.Stream getStream) {
            return base.Channel.postMostStrAsync(getStream);
        }
        
        public System.IO.Stream DownLoadFile(string downfile) {
            return base.Channel.DownLoadFile(downfile);
        }
        
        public System.Threading.Tasks.Task<System.IO.Stream> DownLoadFileAsync(string downfile) {
            return base.Channel.DownLoadFileAsync(downfile);
        }
        
        public string postText() {
            return base.Channel.postText();
        }
        
        public System.Threading.Tasks.Task<string> postTextAsync() {
            return base.Channel.postTextAsync();
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SVCPersonInfo.IWebSocketsServer")]
    public interface IWebSocketsServer {
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IWebSocketsServerChannel : TestWCFService.SVCPersonInfo.IWebSocketsServer, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WebSocketsServerClient : System.ServiceModel.ClientBase<TestWCFService.SVCPersonInfo.IWebSocketsServer>, TestWCFService.SVCPersonInfo.IWebSocketsServer {
        
        public WebSocketsServerClient() {
        }
        
        public WebSocketsServerClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WebSocketsServerClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WebSocketsServerClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WebSocketsServerClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
    }
}
