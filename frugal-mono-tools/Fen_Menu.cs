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
using System.Timers;

namespace frugalmonotools
{
	public partial class Fen_Menu : Gtk.Window
	{
		private const string cch_system="System";
		private const string cch_xorg="Xorg";
		private const string cch_update="Update";
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
		WID_Network fen_network = new WID_Network();
		WID_News fen_news ;
		WID_Pkg fen_pkg= new WID_Pkg();
		WID_Xorg fen_xorg= new WID_Xorg();
		WID_Update fen_update= new WID_Update();
		WID_Services fen_services = new WID_Services();
		WID_Support fen_support = new WID_Support();
		WID_System fen_system = new WID_System();
		WID_About fen_about = new WID_About();
		WID_Config fen_config = new WID_Config();
		WID_Hardware fen_hardware = new WID_Hardware();
		WID_LoginManager fen_loginManager = new WID_LoginManager();

		protected Gtk.TreeIter iter;
		public Fen_Menu () : base(Gtk.WindowType.Toplevel)
		{
			
			this.Build ();
			ListStore ListMenu = new Gtk.ListStore (typeof (string));
			TREE_Menu.Model=ListMenu;
			// Create a column for the package name
			Gtk.TreeViewColumn ListColumn = new Gtk.TreeViewColumn ();
			ListColumn.Title = "Module";
			Gtk.CellRendererText ListCell = new Gtk.CellRendererText ();
			// Add the cell to the column
			ListColumn.PackStart (ListCell, true);
			TREE_Menu.AppendColumn (ListColumn);
			ListColumn.AddAttribute (ListCell, "text", 0);
			// Event on treeview
			TREE_Menu.Selection.Changed += OnSelectionEntryUpdate;
			
			ListMenu.AppendValues(cch_system);
			ListMenu.AppendValues(cch_xorg);
			ListMenu.AppendValues(cch_update);
			ListMenu.AppendValues(cch_packages);
			ListMenu.AppendValues(cch_hardware);
			ListMenu.AppendValues(cch_services);
			ListMenu.AppendValues(cch_network);
			ListMenu.AppendValues(cch_loginManager);
			ListMenu.AppendValues(cch_support);
			ListMenu.AppendValues(cch_news);
			ListMenu.AppendValues(cch_configuration);
			ListMenu.AppendValues(cch_about);
			
			//see module system
			SelectModule(cch_system);
			
			//timer news
			System.Timers.Timer aTimer = new System.Timers.Timer();
			aTimer.Elapsed+=new ElapsedEventHandler(checkRSS);
			// Set the Interval to 1 hour.
			aTimer.Interval=3600000;
			aTimer.Enabled=true;
		}
		
		protected void OnSelectionEntryUpdate(object o, EventArgs args)
	    {
	   		try
			{
			 	TreeModel model;
				 if (((TreeSelection)o).GetSelected(out model, out iter))
		        {
					string T =(string)model.GetValue (iter, 0);
					SelectModule(T);
				}
			}
			catch{}
		}
		
		private void SelectModule(string module)
		{
					
					
					this.HBOX_Details.Destroy();
					this.HBOX_Details = new global::Gtk.HBox ();
					this.HBOX_Details.Name = "HBOX_Details";
					this.HBOX_Menu.Add (this.HBOX_Details);
		            
					
					switch (module){
						case cch_packages:
							this.fen_pkg = new WID_Pkg();
							this.HBOX_Details.PackStart(fen_pkg);
							this.HBOX_Details.ShowAll();
						break;
						
						case cch_update:
							this.fen_update = new WID_Update();
							this.HBOX_Details.PackStart(fen_update);
							this.HBOX_Details.ShowAll();
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
						break;

						case cch_hardware:
							this.fen_hardware = new WID_Hardware();
							this.HBOX_Details.PackStart(fen_hardware);
							this.HBOX_Details.ShowAll();
						break;

						case cch_loginManager:
							this.fen_loginManager = new WID_LoginManager();
							this.HBOX_Details.PackStart(fen_loginManager);
							this.HBOX_Details.ShowAll();
						break;
				
						case cch_network:
							this.fen_network=new WID_Network();
							this.HBOX_Details.PackStart(fen_network);
							this.HBOX_Details.ShowAll();
						break;
				
						case cch_news:
							this.fen_news = new WID_News();
							this.HBOX_Details.PackStart(fen_news);
							this.HBOX_Details.ShowAll();
						break;

						case cch_services:
							this.fen_services = new WID_Services();
							this.HBOX_Details.PackStart(fen_services);
							this.HBOX_Details.ShowAll();
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
						break;
				
						case cch_xorg:
							this.fen_xorg=new WID_Xorg();
							this.HBOX_Details.PackStart(fen_xorg);
							this.HBOX_Details.ShowAll();
						break;
					}
		}
		protected virtual void OnDeleteEvent (object o, Gtk.DeleteEventArgs args)
		{
				if(MainClass.StartedAutomatic)
					this.Hide();
				else
					Application.Quit ();
		
			args.RetVal = true;
		}
		private void checkRSS(object source, ElapsedEventArgs e)
		{
			
			/*
		//RSS
		try{
			modelFlux.Clear();
			rssFeed =RssFeed.Read(UrlPlanet);
			RssChannel rssChannel = (RssChannel)rssFeed.Channels[0];

			int i = 0;
			string latest="";
			foreach (RssItem item in rssChannel.Items)
			{
				string titre=item.Title;
				if(latest =="")latest=item.Link.AbsoluteUri.ToString();
				modelFlux.AppendValues(titre,i);
				i++;
			}
			InformNewFlux(latest);
		}
		catch{}		*/
		}
	
		
	}
}

