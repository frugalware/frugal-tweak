// /*
//  *  Copyright (C) 2010 by Gaetan Gourdin <bouleetbil@frogdev.info>
//  *
//  *  This program is free software; you can redistribute it and/or modify
//  *  it under the terms of the GNU General Public License as published by
//  *  the Free Software Foundation; either version 2 of the License, or
//  *  (at your option) any later version.
//  *
//  *  This program is distributed in the hope that it will be useful,
//  *  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  *  GNU General Public License for more details.
//  *
//  *  You should have received a copy of the GNU General Public License
//  *  along with this program; if not, write to the Free Software
//  *  Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA 02111-1307, USA.
//  */
using System;
namespace frugalmonotools
{

	[System.Web.Services.WebServiceBinding(Name="FrugalNotesSoap", Namespace="http://tempuri.org/")]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class FrugalNotes : System.Web.Services.Protocols.SoapHttpClientProtocol {
    
    private System.Threading.SendOrPostCallback SendNoteOperationCompleted;
    
    private System.Threading.SendOrPostCallback GetNoteOperationCompleted;
    
    public FrugalNotes() {
        this.Url = "http://dors.frugalware.org/webservices/FrugalNotes.asmx";
    }
    
    public event SendNoteCompletedEventHandler SendNoteCompleted;
    
    public event GetNoteCompletedEventHandler GetNoteCompleted;
    
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SendNote", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped, Use=System.Web.Services.Description.SoapBindingUse.Literal)]
    public bool SendNote(string user, string pass, string note) {
        object[] results = this.Invoke("SendNote", new object[] {
                    user,
                    pass,
                    note});
        return ((bool)(results[0]));
    }
    
    public System.IAsyncResult BeginSendNote(string user, string pass, string note, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("SendNote", new object[] {
                    user,
                    pass,
                    note}, callback, asyncState);
    }
    
    public bool EndSendNote(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((bool)(results[0]));
    }
    
    public void SendNoteAsync(string user, string pass, string note) {
        this.SendNoteAsync(user, pass, note, null);
    }
    
    public void SendNoteAsync(string user, string pass, string note, object userState) {
        if ((this.SendNoteOperationCompleted == null)) {
            this.SendNoteOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSendNoteCompleted);
        }
        this.InvokeAsync("SendNote", new object[] {
                    user,
                    pass,
                    note}, this.SendNoteOperationCompleted, userState);
    }
    
    private void OnSendNoteCompleted(object arg) {
        if ((this.SendNoteCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.SendNoteCompleted(this, new SendNoteCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
    
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetNote", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped, Use=System.Web.Services.Description.SoapBindingUse.Literal)]
    public string GetNote(string user, string pass) {
        object[] results = this.Invoke("GetNote", new object[] {
                    user,
                    pass});
        return ((string)(results[0]));
    }
    
    public System.IAsyncResult BeginGetNote(string user, string pass, System.AsyncCallback callback, object asyncState) {
        return this.BeginInvoke("GetNote", new object[] {
                    user,
                    pass}, callback, asyncState);
    }
    
    public string EndGetNote(System.IAsyncResult asyncResult) {
        object[] results = this.EndInvoke(asyncResult);
        return ((string)(results[0]));
    }
    
    public void GetNoteAsync(string user, string pass) {
        this.GetNoteAsync(user, pass, null);
    }
    
    public void GetNoteAsync(string user, string pass, object userState) {
        if ((this.GetNoteOperationCompleted == null)) {
            this.GetNoteOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetNoteCompleted);
        }
        this.InvokeAsync("GetNote", new object[] {
                    user,
                    pass}, this.GetNoteOperationCompleted, userState);
    }
    
    private void OnGetNoteCompleted(object arg) {
        if ((this.GetNoteCompleted != null)) {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.GetNoteCompleted(this, new GetNoteCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }
}

public partial class SendNoteCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal SendNoteCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState) {
        this.results = results;
    }
    
    public bool Result {
        get {
            this.RaiseExceptionIfNecessary();
            return ((bool)(this.results[0]));
        }
    }
}

public delegate void SendNoteCompletedEventHandler(object sender, SendNoteCompletedEventArgs args);

public partial class GetNoteCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
    
    private object[] results;
    
    internal GetNoteCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState) {
        this.results = results;
    }
    
    public string Result {
        get {
            this.RaiseExceptionIfNecessary();
            return ((string)(this.results[0]));
        }
    }
}

public delegate void GetNoteCompletedEventHandler(object sender, GetNoteCompletedEventArgs args);

}

