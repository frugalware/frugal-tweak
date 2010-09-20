/*
 *  Copyright (C) 2010 by Gaetan Gourdin <bouleetbil@frogdev.info>
 *
 *  This program is free software; you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation; either version 2 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program; if not, write to the Free Software
 *  Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA 02111-1307, USA.
 */
using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Timers;
using Gdk;
using Gtk;
using Rss;


namespace frugalmonotools
{
	class MainClass
	{
		public static string UrlPlanet="http://planet.frugalware.org/feed.php?type=rss";
		//pacman-g2 initialise
		public static PacmanG2 pacmanG2 = new PacmanG2();
		public static Configuration configuration = new Configuration();
		public static ConfSystem  confSystem = new ConfSystem();
		public static Cache cache = new Cache();
		public static  bool boRoot = false;
		
		private static void checkUpdate(object source, ElapsedEventArgs e)
		{
			Console.WriteLine("check update packages.");
			//start it in a new thread
			Thread th = new Thread(new ThreadStart(check));
			th.IsBackground=true;
			th.SetApartmentState(ApartmentState.STA);
			th.Start();

		}
		public static bool updatePkg = false;
		public static void checktest()
		{
			Console.WriteLine("Thread started");
			if (configuration.Get_CheckUpdate())
			{
				updatePkg=Update.CheckUpdate();
			}
			win.InitFinish();	
		}
		
		public static void check()
		{
			try
			{
			RssFeed rssFeed =RssFeed.Read(UrlPlanet);
			RssChannel rssChannel = (RssChannel)rssFeed.Channels[0];
			string latest="";
			foreach (RssItem item in rssChannel.Items)
				{
					if(latest =="")latest=item.Link.AbsoluteUri.ToString();
					if (cache.GetLatest()!=latest)
					{
						Outils.Inform("Frugalware","News are available.");
						//write cache	
						cache.SetLatest(latest);
						cache.CacheSave();
					}
				
				}
			}
			catch{}
			if (Update.CheckUpdate())
			{
				Pixbuf ico = global::Gdk.Pixbuf.LoadFromResource ("frugalmonotools.Pictures.systrayupdate.png");
				Gtk.Application.Invoke (delegate{trayIcon.Pixbuf=ico;});
							
				if(Debug.ModeDebug)
				{
					foreach (packageCheck pkg in Update.UpdatePkg)
					{
						Console.WriteLine(pkg.packagename+" can be updated to "+pkg.packageversion);
					}
				}

				Outils.Inform("Frugalware","Some update are available.");
				//Console.WriteLine("Some packages can be updated.");
				
			}
			else
			{
				Pixbuf ico = global::Gdk.Pixbuf.LoadFromResource ("frugalmonotools.Pictures.systray.png");
				Gtk.Application.Invoke (delegate{trayIcon.Pixbuf=ico;});
			}
		}
		private static splash win;
		public static StatusIcon trayIcon = null ;
		// Create the popup menu, on right click.
			static void OnTrayIconPopup (object o, EventArgs args) {
				Menu popupMenu = new Menu();
				ImageMenuItem menuItemcc = new ImageMenuItem ("Start Control Center");
				Gtk.Image ccimg = new Gtk.Image(Stock.Execute, IconSize.Menu);
				menuItemcc.Image = ccimg;
				popupMenu.Add(menuItemcc);
				
				ImageMenuItem menuItemccRoot = new ImageMenuItem ("Start Control Center as root");
				Gtk.Image ccimgRoot = new Gtk.Image(Stock.Execute, IconSize.Menu);
				menuItemccRoot.Image = ccimgRoot;
				popupMenu.Add(menuItemccRoot);
				
				ImageMenuItem menuItemForum = new ImageMenuItem ("Forums Frugalware");
				Gtk.Image Forumimg = new Gtk.Image(Stock.Help, IconSize.Menu);
				menuItemForum.Image = Forumimg;
				popupMenu.Add(menuItemForum);
			
				ImageMenuItem menuItemWiki = new ImageMenuItem ("Wiki Frugalware");
				Gtk.Image Wikiimg = new Gtk.Image(Stock.Help, IconSize.Menu);
				menuItemWiki.Image = Wikiimg;
				popupMenu.Add(menuItemWiki);
			
				ImageMenuItem menuItemQuit = new ImageMenuItem ("Quit");
				Gtk.Image appimg = new Gtk.Image(Stock.Quit, IconSize.Menu);
				menuItemQuit.Image = appimg;
				popupMenu.Add(menuItemQuit);
			
				menuItemForum.Activated += delegate { 
													WebkitBrowser browser = new WebkitBrowser("http://forums.frugalware.org");
													browser.Show(); 
													};
				menuItemWiki.Activated += delegate { 
													WebkitBrowser browser = new WebkitBrowser("http://wiki.frugalware.org");
													browser.Show(); 
													};
				menuItemcc.Activated += delegate {  
													if(Fen==null)
													{
														Fen = new Fen_Menu();
														Fen.Show();
													}
													else
													{
														Fen.Destroy();
														Fen.Dispose();
														Fen=null;
													}
											};

				menuItemccRoot.Activated += delegate { Outils.Excecute("sucontrolcenter","",false); };
				
				// Quit the application when quit has been clicked.
				menuItemQuit.Activated += delegate { Application.Quit(); };
				popupMenu.ShowAll();
				popupMenu.Popup();
			}
		
		public static Fen_Menu Fen ;
		public static bool StartedAutomatic=false;
		public static Xorg xorg = new Xorg();
		
		
		public static void Main (string[] args)
		{
			//root options
			if (Mono.Unix.Native.Syscall.getuid()!=0)
				boRoot=false;
			else
				boRoot=true;
			
			System.Timers.Timer aTimer;
			if(args.Length==0)
			{
				Gtk.Application.Init();		
				
				if (configuration.Get_ShowSplash()) 
				{
					win = new splash ();
					win.Show ();
					Thread th = new Thread(new ThreadStart(checktest));
					th.IsBackground=true;
					th.SetApartmentState(ApartmentState.STA);
					th.Start();
				}
				else
				{
					System.Threading.Thread.Sleep(1000);
					Fen = new Fen_Menu();
					Fen.Show();
				}
				Gtk.Application.Run ();
			}
			else
			{
				Console.WriteLine(args[0]);
				switch(args[0])
				{		
					case "--update":
						if(configuration.Get_StartWithX())
						{
							//check if an update is avalaible
							//started with X session
							Gtk.Application.Init();
							Console.WriteLine("check update packages.");
							// Creation of the Icon
							Pixbuf ico = global::Gdk.Pixbuf.LoadFromResource ("frugalmonotools.Pictures.systray.png");
	
							trayIcon = new StatusIcon(ico);
							trayIcon.Visible = true;
							check();
							StartedAutomatic=true;
							aTimer = new System.Timers.Timer();
		         			aTimer.Elapsed+=new ElapsedEventHandler(checkUpdate);
		        			// Set the Interval to 1 hour.
		        			aTimer.Interval=3600000;
		         			aTimer.Enabled=true;
							
					 		
							trayIcon.Activate += delegate {  
								if(Fen==null)
								{
									Fen = new Fen_Menu();
									Fen.Show();
								}
								else
								{
									Fen.Destroy();
									Fen.Dispose();
									Fen=null;
								}
							
							};

							// Show a pop up menu when the icon has been right clicked.
							trayIcon.PopupMenu += OnTrayIconPopup;
					 
							// A Tooltip for the Icon
							trayIcon.Tooltip = "Frugalware Control Center";
							Gtk.Application.Run ();
						}
						break;
					
					default:
						Console.WriteLine("Bad parameters exit...");
						break;
				}
			}
		}
	}
}

