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
using Gtk;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using frugalirc;
using Meebey.SmartIrc4net;

public partial class MainWindow : Gtk.Window
{
	 // make an instance of the high-level API
    public static IrcClient irc = new IrcClient();
	private Thread T;
	ListStore UpdateListUsers = new Gtk.ListStore (typeof (string));
	private bool _initOk = false ;
	private int MyRandom()
	{
		Random rndNumbers = new Random();
        int rndNumber = 0;
        rndNumber = rndNumbers.Next(20);
        return rndNumber;
    
	}

	public MainWindow () : base(Gtk.WindowType.Toplevel)
	{
		Build ();
		
		SAI_User.Text+=MyRandom().ToString();
		//update package list
		// Create a column for the package name
		Gtk.TreeViewColumn Column = new Gtk.TreeViewColumn ();
		Column.Title = "Users";
		Gtk.CellRendererText NameCell = new Gtk.CellRendererText ();
		// Add the cell to the column
		Column.PackStart (NameCell, true);
		TREE_Users.AppendColumn (Column);
		Column.AddAttribute (NameCell, "text", 0);
		TREE_Users.Model=UpdateListUsers;
		if (MainClass.ChanParameter!="")
		{
			SAI_Chan.Text=MainClass.ChanParameter;
		}
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		try
		{
			Application.Quit ();
			T.Abort();
			a.RetVal = true;
		}
		catch{}
	}
	 // this method we will use to analyse queries (also known as private messages)
    public  void OnQueryMessage(object sender, IrcEventArgs e)
    {
        switch (e.Data.MessageArray[0]) {
            // debug stuff
            case "dump_channel":
                string requested_channel = e.Data.MessageArray[1];
                // getting the channel (via channel sync feature)
                Channel channel = irc.GetChannel(requested_channel);
                
                // here we send messages
                irc.SendMessage(SendType.Message, e.Data.Nick, "<channel '"+requested_channel+"'>");
                
                irc.SendMessage(SendType.Message, e.Data.Nick, "Name: '"+channel.Name+"'");
                irc.SendMessage(SendType.Message, e.Data.Nick, "Topic: '"+channel.Topic+"'");
                irc.SendMessage(SendType.Message, e.Data.Nick, "Mode: '"+channel.Mode+"'");
                irc.SendMessage(SendType.Message, e.Data.Nick, "Key: '"+channel.Key+"'");
                irc.SendMessage(SendType.Message, e.Data.Nick, "UserLimit: '"+channel.UserLimit+"'");
                
                // here we go through all users of the channel and show their
                // hashtable key and nickname 
                string nickname_list = "";
                nickname_list += "Users: ";
                foreach (DictionaryEntry de in channel.Users) {
                    string      key         = (string)de.Key;
                    ChannelUser channeluser = (ChannelUser)de.Value;
                    nickname_list += "(";
                    if (channeluser.IsOp) {
                        nickname_list += "@";
                    }
                    if (channeluser.IsVoice) {
                        nickname_list += "+";
                    }
                    nickname_list += ")"+key+" => "+channeluser.Nick+", ";
					//UpdateListUsers.AppendValues(channeluser.Nick);
                }
                irc.SendMessage(SendType.Message, e.Data.Nick, nickname_list);

                irc.SendMessage(SendType.Message, e.Data.Nick, "</channel>");
            break;
            case "gc":
                GC.Collect();
            break;
            // typical commands
            case "join":
                irc.RfcJoin(e.Data.MessageArray[1]);
            break;
            case "part":
                irc.RfcPart(e.Data.MessageArray[1]);
            break;
            case "die":
                Exit();
            break;
        }
    }

	public void Exit()
    {
        // we are done, lets exit...
        System.Console.WriteLine("Exiting...");
        System.Environment.Exit(0);
    }
	 
    // this method handles when we receive "ERROR" from the IRC server
    public  void OnError(object sender, ErrorEventArgs e)
    {
        System.Console.WriteLine("Error: "+e.ErrorMessage);
        Exit();
    }
    public  void ReadCommands()
    {
        // here we read the commands from the stdin and send it to the IRC API
        // WARNING, it uses WriteLine() means you need to enter RFC commands
        // like "JOIN #test" and then "PRIVMSG #test :hello to you"
        while (true) {
            string cmd = System.Console.ReadLine();
            if (cmd.StartsWith("/list")) {
                int pos = cmd.IndexOf(" ");
                string channel = null;
                if (pos != -1) {
                    channel = cmd.Substring(pos + 1);
                }
                
                IList<ChannelInfo> channelInfos = irc.GetChannelList(channel);
                Console.WriteLine("channel count: {0}", channelInfos.Count);
				
                foreach (ChannelInfo channelInfo in channelInfos) {
                    Console.WriteLine("channel: {0} user count: {1} topic: {2}",
                                      channelInfo.Channel,
                                      channelInfo.UserCount,
                                      channelInfo.Topic);
                }
            } else {
                irc.WriteLine(cmd);
            }
        }
    }
    

    // this method will get all IRC messages
    public  void OnRawMessage(object sender, IrcEventArgs e)
    {
		if(!_initOk) return ;
       //AppendText("Received: "+e.Data.RawMessage);
		switch(e.Data.Type)
		{
			case ReceiveType.ChannelMessage:
				AppendText(e.Data.Nick+" : "+e.Data.Message);
				break;
			case ReceiveType.Join:
				UpdateListUsers.AppendValues(e.Data.Nick);
				break;
			case ReceiveType.Quit:
				_userToTreeview();
				break;
			case ReceiveType.Part:
				_userToTreeview();
				break;
			case ReceiveType.Login:
				_userToTreeview();
				break;
			case ReceiveType.ChannelNotice:
				AppendText("NOTIFICATION: "+e.Data.Message);
				break;
			default:
				Console.WriteLine("Received: "+e.Data.RawMessage);
				break;
		}
		if(e.Data.ReplyCode==ReplyCode.List)
		{
			_userToTreeview();
		}
    }
	
 string[] GetUserList(string channel)
      {
        if (channel == null || irc.GetChannel(channel) == null)
          return new string[0];
        Hashtable users = irc.GetChannel(channel).Users;
        string[] userlist = new string[users.Count];
		
        int i = 0;
        foreach (ChannelUser user in users.Values)
          userlist[i++] = String.Concat(user.IsOp ? "@" : String.Empty, user.Nick);
        return userlist;
      }    
	
	private void _userToTreeview()
	{
		try
		{
			UpdateListUsers.Clear();
			string [] users =GetUserList(SAI_Chan.Text);
			foreach(string pseudo in users)
			{
				UpdateListUsers.AppendValues(pseudo);
			}
		}
		catch{}
	}
	protected virtual void OnBTNConnectClicked (object sender, System.EventArgs e)
	{
		Connection();
	}
	public void Connection()
	{
		BTN_Connect.Visible=false;
		T = new Thread(Connect);
		T.IsBackground=true;
		T.SetApartmentState(ApartmentState.STA);
		T.Start();
		
	}
	private void Connect()
	{
		Thread.CurrentThread.Name = "Main";
        
        // UTF-8 test
        irc.Encoding = System.Text.Encoding.UTF8;
        
        // wait time between messages, we can set this lower on own irc servers
        irc.SendDelay = 200;
        
        // we use channel sync, means we can use irc.GetChannel() and so on
        irc.ActiveChannelSyncing = true;
        
        // here we connect the events of the API to our written methods
        // most have own event handler types, because they ship different data
        irc.OnQueryMessage += new IrcEventHandler(OnQueryMessage);
        irc.OnError += new ErrorEventHandler(OnError);
        irc.OnRawMessage += new IrcEventHandler(OnRawMessage);

		string[] serverlist;
        // the server we want to connect to, could be also a simple string
        serverlist = new string[] {SAI_Serveur.Text};
        int port = int.Parse(SAI_Port.Text);
        string channel = SAI_Chan.Text;
        try {
            // here we try to connect to the server and exceptions get handled
            irc.Connect(serverlist, port);
        } catch (ConnectionException exe) {
            // something went wrong, the reason will be shown
            System.Console.WriteLine("couldn't connect! Reason: "+exe.Message);
            Exit();
        }
		
        try {
            // here we logon and register our nickname and so on 
            irc.Login(SAI_User.Text,SAI_User.Text);
            // join the channel
            irc.RfcJoin(channel);
           
			// testing the delay and flood protection (messagebuffer work)
			irc.RfcList(SAI_Chan.Text);
			irc.SendMessage(SendType.Message, SAI_Chan.Text, "Hello");
	
			_initOk=true;
			/*
			irc.RfcList(channel);
			Gtk.Application.Invoke (delegate {
				BTN_Send.Visible=true;
			});*/
			
            // spawn a new thread to read the stdin of the console, this we use
            // for reading IRC commands from the keyboard while the IRC connection
            // stays in its own thread
            //new Thread(new ThreadStart(ReadCommands)).Start();

			
			
            // here we tell the IRC API to go into a receive mode, all events
            // will be triggered by _this_ thread (main thread in this case)
            // Listen() blocks by default, you can also use ListenOnce() if you
            // need that does one IRC operation and then returns, so you need then 
            // an own loop 
            irc.Listen();

            // when Listen() returns our IRC session is over, to be sure we call
            // disconnect manually
            irc.Disconnect();
        } catch (ConnectionException) {
            // this exception is handled because Disconnect() can throw a not
            // connected exception
            Exit();
        } catch (Exception exe) {
            // this should not happen by just in case we handle it nicely
            System.Console.WriteLine("Error occurred! Message: "+exe.Message);
            System.Console.WriteLine("Exception: "+exe.StackTrace);
            Exit();
        }

	}
	
	private void AppendText(string text)
	{
		
    Gtk.Application.Invoke (delegate {
        TextIter mIter = TXT_Messages.Buffer.EndIter;
        TXT_Messages.Buffer.Insert(ref mIter, text+"\n");
        TXT_Messages.ScrollToIter(TXT_Messages.Buffer.EndIter, 0, false, 0, 0);
        });

		
		Console.WriteLine(text);
	}
	
	protected virtual void OnBTNSendClicked (object sender, System.EventArgs e)
	{
		if(!_initOk) return;
		if(SAI_Envoi.Text=="") return;
		irc.SendMessage(SendType.Message, SAI_Chan.Text, SAI_Envoi.Text);
		AppendText(SAI_User.Text+" : "+SAI_Envoi.Text);
		SAI_Envoi.Text="";
	}
	
	protected virtual void OnSAIEnvoiKeyPressEvent (object o, Gtk.KeyPressEventArgs args)
	{
		Console.WriteLine("Passe");
	}
	
	
	
}

