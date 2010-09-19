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
using Gdk;
using Gtk;
using Rss;
using System.Timers;

namespace frugalmonotools
{
	public partial class Fen_Menu : Gtk.Window
	{
		private const string cch_system="System";
		private const string cch_xorg="Xorg";
		private const string cch_update="Update";
		private const string cch_updateConf="Update configuration";
		private const string cch_packages="Packages";
		private const string cch_hardware="Hardware";
		private const string cch_services="Services";
		private const string cch_network="Network";
		private const string cch_loginManager="Login Manager";
		private const string cch_support="Support";
		private const string cch_news="News";
		private const string cch_configuration="Configuration";
		private const string cch_about="About";
		
		//widget
		WID_Network fen_network ;
		WID_News fen_news ;
		WID_Pkg fen_pkg;
		WID_Xorg fen_xorg;
		WID_Update fen_update;
		WID_UpdateConf fen_updateConf;
		WID_Services fen_services;
		WID_Support fen_support;
		WID_System fen_system;
		WID_About fen_about ;
		WID_Config fen_config ;
		WID_Hardware fen_hardware ;
		WID_LoginManager fen_loginManager;
		
		string CurrentWidget="";

		protected Gtk.TreeIter iter;
		public Fen_Menu () : base(Gtk.WindowType.Toplevel)
		{
			
			this.Build ();
			//graphical debug
			if ( Debug.ModeDebug && Debug.ModeDebugGraphique)
			{
				Debug.winDebug = new FEN_Debug(); 
				Debug.winDebug.Show();
			}
			ListStore ListMenu = new Gtk.ListStore (typeof (Gdk.Pixbuf),typeof (string));
			TREE_Menu.Model=ListMenu;
			TREE_Menu.AppendColumn ("", new Gtk.CellRendererPixbuf (), "pixbuf", 0);
			// Create a column for the package name
			Gtk.TreeViewColumn ListColumn = new Gtk.TreeViewColumn ();
			ListColumn.Title = "Module";
			/*ListColumn.FixedWidth=70;
			ListColumn.MaxWidth=70;
			ListColumn.MinWidth=70;*/
			Gtk.CellRendererText ListCell = new Gtk.CellRendererText ();
			// Add the cell to the column
			ListColumn.PackStart (ListCell, true);
			TREE_Menu.AppendColumn (ListColumn);
			ListColumn.AddAttribute (ListCell, "text", 1);
			// Event on treeview
			TREE_Menu.Selection.Changed += OnSelectionEntryUpdate;
			
			
			Pixbuf icoSys = global::Gdk.Pixbuf.LoadFromResource ("frugalmonotools.Pictures.icons.system.png");
			iter = ListMenu.AppendValues(icoSys.ScaleSimple(20,20, Gdk.InterpType.Nearest),cch_system);
			this.TREE_Menu.SetCursor(ListMenu.GetPath(iter),TREE_Menu.GetColumn(1),false);
			//FIX ME select first element
			Pixbuf icoX = global::Gdk.Pixbuf.LoadFromResource ("frugalmonotools.Pictures.icons.xorg.png");
			ListMenu.AppendValues(icoX.ScaleSimple(20,20, Gdk.InterpType.Nearest), cch_xorg);
			Pixbuf icoupdate = global::Gdk.Pixbuf.LoadFromResource ("frugalmonotools.Pictures.icons.update.png");
			ListMenu.AppendValues(icoupdate.ScaleSimple(20,20, Gdk.InterpType.Nearest),cch_update);
			//TODO change icon
			Pixbuf icoupdateConf = global::Gdk.Pixbuf.LoadFromResource ("frugalmonotools.Pictures.icons.update.png");
			ListMenu.AppendValues(icoupdateConf.ScaleSimple(20,20, Gdk.InterpType.Nearest),cch_updateConf);
			
			Pixbuf icopkg = global::Gdk.Pixbuf.LoadFromResource ("frugalmonotools.Pictures.icons.packages.png");
			ListMenu.AppendValues(icopkg.ScaleSimple(20,20, Gdk.InterpType.Nearest),cch_packages);
			Pixbuf icohardware = global::Gdk.Pixbuf.LoadFromResource ("frugalmonotools.Pictures.icons.hardware.png");
			ListMenu.AppendValues(icohardware.ScaleSimple(20,20, Gdk.InterpType.Nearest),cch_hardware);
			Pixbuf icoservices = global::Gdk.Pixbuf.LoadFromResource ("frugalmonotools.Pictures.icons.services.png");
			ListMenu.AppendValues(icoservices.ScaleSimple(20,20, Gdk.InterpType.Nearest),cch_services);
			Pixbuf iconet = global::Gdk.Pixbuf.LoadFromResource ("frugalmonotools.Pictures.icons.network.png");
			ListMenu.AppendValues(iconet.ScaleSimple(20,20, Gdk.InterpType.Nearest),cch_network);
			Pixbuf icologin = global::Gdk.Pixbuf.LoadFromResource ("frugalmonotools.Pictures.icons.loginmanager.png");
			ListMenu.AppendValues(icologin.ScaleSimple(20,20, Gdk.InterpType.Nearest),cch_loginManager);
			Pixbuf icosup = global::Gdk.Pixbuf.LoadFromResource ("frugalmonotools.Pictures.icons.support.png");
			ListMenu.AppendValues(icosup.ScaleSimple(20,20, Gdk.InterpType.Nearest),cch_support);
			Pixbuf iconews = global::Gdk.Pixbuf.LoadFromResource ("frugalmonotools.Pictures.icons.news.png");
			ListMenu.AppendValues(iconews.ScaleSimple(20,20, Gdk.InterpType.Nearest),cch_news);
			Pixbuf icoconfig = global::Gdk.Pixbuf.LoadFromResource ("frugalmonotools.Pictures.icons.configurations.png");
			ListMenu.AppendValues(icoconfig.ScaleSimple(20,20, Gdk.InterpType.Nearest),cch_configuration);
			Pixbuf icoabout = global::Gdk.Pixbuf.LoadFromResource ("frugalmonotools.Pictures.icons.about.png");
			ListMenu.AppendValues(icoabout.ScaleSimple(20,20, Gdk.InterpType.Nearest),cch_about);
			
			//timer news
			System.Timers.Timer aTimer = new System.Timers.Timer();
			aTimer.Elapsed+=new ElapsedEventHandler(checkRSS);
			// Set the Interval to 1 hour.
			aTimer.Interval=3600000;
			aTimer.Enabled=true;
			_checkRss();
		}
		
		protected void OnSelectionEntryUpdate(object o, EventArgs args)
	    {
	   		try
			{
			 	TreeModel model;
				 if (((TreeSelection)o).GetSelected(out model, out iter))
		        {
					string T =(string)model.GetValue (iter, 1);
					SelectModule(T);
				}
			}
			catch(Exception exe)
			{
				Console.WriteLine(exe.Message);
			}
		}
		
		private void SelectModule(string module)
		{
					
					CurrentWidget=module;
					this.HBOX_Details.Destroy();
					this.HBOX_Details = new global::Gtk.HBox ();
					this.HBOX_Details.SetSizeRequest(680,500);
					this.HBOX_Details.Name = "HBOX_Details";
					this.HBOX_Menu.Add (this.HBOX_Details);
		            
					
					switch (module){
						case cch_packages:
							this.fen_pkg = new WID_Pkg();
							this.HBOX_Details.PackStart(fen_pkg);
							this.HBOX_Details.ShowAll();
							this.fen_pkg.InitPkg();
						break;
						
						case cch_update:
							this.fen_update = new WID_Update();
							this.HBOX_Details.PackStart(fen_update);
							this.HBOX_Details.ShowAll();
							this.fen_update.InitUpdate();
						break;
				
						case cch_updateConf:
							this.fen_updateConf = new WID_UpdateConf();
							this.HBOX_Details.PackStart(fen_updateConf);
							this.HBOX_Details.ShowAll();
							this.fen_updateConf.InitUpdateConf();
						break;
				
						case cch_about:
							this.fen_about = new WID_About();
							this.HBOX_Details.PackStart(fen_about);
							this.HBOX_Details.ShowAll();
						break;
				
						case cch_configuration:
							this.fen_config = new WID_Config();
							this.HBOX_Details.PackStart(fen_config);
							this.HBOX_Details.ShowAll();
							this.fen_config.InitConfig();
						break;

						case cch_hardware:
							this.fen_hardware = new WID_Hardware();
							this.HBOX_Details.PackStart(fen_hardware);
							this.HBOX_Details.ShowAll();
							this.fen_hardware.InitHardware();
						break;

						case cch_loginManager:
							this.fen_loginManager = new WID_LoginManager();
							this.HBOX_Details.PackStart(fen_loginManager);
							this.HBOX_Details.ShowAll();
							this.fen_loginManager.InitLoginManager();
						break;
				
						case cch_network:
							this.fen_network=new WID_Network();
							this.HBOX_Details.PackStart(fen_network);
							this.HBOX_Details.ShowAll();
							this.fen_network.InitNetworkManager();
						break;
				
						case cch_news:
							this.fen_news = new WID_News();
							this.HBOX_Details.PackStart(fen_news);
							this.HBOX_Details.ShowAll();
							this.fen_news.InitNews();
						break;

						case cch_services:
							this.fen_services = new WID_Services();
							this.HBOX_Details.PackStart(fen_services);
							this.HBOX_Details.ShowAll();
							this.fen_services.InitService();
							
						break;

						case cch_support:
							this.fen_support= new WID_Support();
							this.HBOX_Details.PackStart(fen_support);
							this.HBOX_Details.ShowAll();
						break;
				
						case cch_system:
							this.fen_system=new WID_System();
							this.HBOX_Details.PackStart(fen_system);
							this.HBOX_Details.ShowAll();
							this.fen_system.InitSystem();
						break;
				
						case cch_xorg:
							this.fen_xorg=new WID_Xorg();
							this.HBOX_Details.PackStart(fen_xorg);
							this.HBOX_Details.ShowAll();
							this.fen_xorg.InitXorg();
						break;
					}
		}
		protected virtual void OnDeleteEvent (object o, Gtk.DeleteEventArgs args)
		{
			if(MainClass.StartedAutomatic)
			{
				this.Hide();
			}
			else
			{
				Application.Quit ();
			}
			args.RetVal = true;
		}
		private void checkRSS(object source, ElapsedEventArgs e)
		{
			_checkRss();
		}
		private void _checkRss()
		{
				//RSS
		try{
			if (CurrentWidget==cch_news)
			{
					fen_news.CheckRss();
					return;
			}
			RssFeed rssFeed =RssFeed.Read(MainClass.UrlPlanet);
			RssChannel rssChannel = (RssChannel)rssFeed.Channels[0];
			string latest="";
			foreach (RssItem item in rssChannel.Items)
			{
				if(latest =="")latest=item.Link.AbsoluteUri.ToString();
				if (MainClass.cache.GetLatest()!=latest)
				{
					Outils.Inform("Frugalware","News are available.");
					//write cache	
					MainClass.cache.SetLatest(latest);
					MainClass.cache.CacheSave();
				}
				return;
			}
			
		}
			catch{}
		
		}
		
	}
}

