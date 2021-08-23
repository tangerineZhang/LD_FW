namespace WebUtility
{
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    using System.Configuration;


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "ThirdPartyJobSoap", Namespace = "http://tempuri.org/")]
    public partial class ThirdPartyJob : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        private System.Threading.SendOrPostCallback TodoOperationCompleted;

        private System.Threading.SendOrPostCallback DoneOperationCompleted;

        private System.Threading.SendOrPostCallback DeleteTodoOperationCompleted;

        /// <remarks/>
        public ThirdPartyJob()
        {
            string urlSetting = System.Configuration.ConfigurationManager.AppSettings["WebServiceBaseUrlThird"];
            //string urlSetting = "http://bpmtest.lvdu-dc.com:84";
            if ((urlSetting != null))
            {
                this.Url = string.Concat(urlSetting, "ThirdPartyJob.asmx");
            }
            else
            {
                this.Url = "http://api.lvdu-dc.com/ThirdPartyJob.asmx";
            }
        }

        /// <remarks/>
        public event TodoCompletedEventHandler TodoCompleted;

        /// <remarks/>
        public event DoneCompletedEventHandler DoneCompleted;

        /// <remarks/>
        public event DeleteTodoCompletedEventHandler DeleteTodoCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Todo", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string Todo(string appId, string appSecret, string procInstId, string topic, string originator, string originatorName, string startDate, string jobUserId, string jobStepName, string webUrl, string appUrl)
        {
            object[] results = this.Invoke("Todo", new object[] {
                        appId,
                        appSecret,
                        procInstId,
                        topic,
                        originator,
                        originatorName,
                        startDate,
                        jobUserId,
                        jobStepName,
                        webUrl,
                        appUrl});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginTodo(string appId, string appSecret, string procInstId, string topic, string originator, string originatorName, string startDate, string jobUserId, string jobStepName, string webUrl, string appUrl, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("Todo", new object[] {
                        appId,
                        appSecret,
                        procInstId,
                        topic,
                        originator,
                        originatorName,
                        startDate,
                        jobUserId,
                        jobStepName,
                        webUrl,
                        appUrl}, callback, asyncState);
        }

        /// <remarks/>
        public string EndTodo(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void TodoAsync(string appId, string appSecret, string procInstId, string topic, string originator, string originatorName, string startDate, string jobUserId, string jobStepName, string webUrl, string appUrl)
        {
            this.TodoAsync(appId, appSecret, procInstId, topic, originator, originatorName, startDate, jobUserId, jobStepName, webUrl, appUrl, null);
        }

        /// <remarks/>
        public void TodoAsync(string appId, string appSecret, string procInstId, string topic, string originator, string originatorName, string startDate, string jobUserId, string jobStepName, string webUrl, string appUrl, object userState)
        {
            if ((this.TodoOperationCompleted == null))
            {
                this.TodoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnTodoOperationCompleted);
            }
            this.InvokeAsync("Todo", new object[] {
                        appId,
                        appSecret,
                        procInstId,
                        topic,
                        originator,
                        originatorName,
                        startDate,
                        jobUserId,
                        jobStepName,
                        webUrl,
                        appUrl}, this.TodoOperationCompleted, userState);
        }

        private void OnTodoOperationCompleted(object arg)
        {
            if ((this.TodoCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.TodoCompleted(this, new TodoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Done", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string Done(string appId, string appSecret, string procInstId, string jobUserId, string webUrl, string appUrl)
        {
            object[] results = this.Invoke("Done", new object[] {
                        appId,
                        appSecret,
                        procInstId,
                        jobUserId,
                        webUrl,
                        appUrl});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginDone(string appId, string appSecret, string procInstId, string jobUserId, string webUrl, string appUrl, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("Done", new object[] {
                        appId,
                        appSecret,
                        procInstId,
                        jobUserId,
                        webUrl,
                        appUrl}, callback, asyncState);
        }

        /// <remarks/>
        public string EndDone(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void DoneAsync(string appId, string appSecret, string procInstId, string jobUserId, string webUrl, string appUrl)
        {
            this.DoneAsync(appId, appSecret, procInstId, jobUserId, webUrl, appUrl, null);
        }

        /// <remarks/>
        public void DoneAsync(string appId, string appSecret, string procInstId, string jobUserId, string webUrl, string appUrl, object userState)
        {
            if ((this.DoneOperationCompleted == null))
            {
                this.DoneOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDoneOperationCompleted);
            }
            this.InvokeAsync("Done", new object[] {
                        appId,
                        appSecret,
                        procInstId,
                        jobUserId,
                        webUrl,
                        appUrl}, this.DoneOperationCompleted, userState);
        }

        private void OnDoneOperationCompleted(object arg)
        {
            if ((this.DoneCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DoneCompleted(this, new DoneCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/DeleteTodo", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string DeleteTodo(string appId, string appSecret, string procInstId, string jobUserId)
        {
            object[] results = this.Invoke("DeleteTodo", new object[] {
                        appId,
                        appSecret,
                        procInstId,
                        jobUserId});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginDeleteTodo(string appId, string appSecret, string procInstId, string jobUserId, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("DeleteTodo", new object[] {
                        appId,
                        appSecret,
                        procInstId,
                        jobUserId}, callback, asyncState);
        }

        /// <remarks/>
        public string EndDeleteTodo(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void DeleteTodoAsync(string appId, string appSecret, string procInstId, string jobUserId)
        {
            this.DeleteTodoAsync(appId, appSecret, procInstId, jobUserId, null);
        }

        /// <remarks/>
        public void DeleteTodoAsync(string appId, string appSecret, string procInstId, string jobUserId, object userState)
        {
            if ((this.DeleteTodoOperationCompleted == null))
            {
                this.DeleteTodoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDeleteTodoOperationCompleted);
            }
            this.InvokeAsync("DeleteTodo", new object[] {
                        appId,
                        appSecret,
                        procInstId,
                        jobUserId}, this.DeleteTodoOperationCompleted, userState);
        }

        private void OnDeleteTodoOperationCompleted(object arg)
        {
            if ((this.DeleteTodoCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DeleteTodoCompleted(this, new DeleteTodoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
    public delegate void TodoCompletedEventHandler(object sender, TodoCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class TodoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal TodoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
    public delegate void DoneCompletedEventHandler(object sender, DoneCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DoneCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal DoneCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
    public delegate void DeleteTodoCompletedEventHandler(object sender, DeleteTodoCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DeleteTodoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal DeleteTodoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

