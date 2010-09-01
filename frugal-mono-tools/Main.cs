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



namespace frugalmonotools
{
	class MainClass
	{
		//pacman-g2 initialise
		public static PacmanG2 pacmanG2 = new PacmanG2();
		public static Configuration configuration = new Configuration();
		
		private static void checkUpdate(object source, ElapsedEventArgs e)
		{
			Console.WriteLine("check update packages.");
			check();
		}
		public static bool updatePkg = false;
		public static void checktest()
		{
			if (configuration.Get_CheckUpdate())
			{
				updatePkg=Update.CheckUpdate();
			}
			win.InitFinish();	
		}
		
		public static void check()
		{
			IconSummaryBody notif= new IconSummaryBody();		

			if (Update.CheckUpdate())
			{
				if(Debug.ModeDebug)
				{
					foreach (packageCheck pkg in Update.UpdatePkg)
					{
						Console.WriteLine(pkg.packagename+" can be updated to "+pkg.packageversion);
					}
				}
				notif.ShowMessage("Frugalware","Some update are available.");
				Console.WriteLine("Some packages can be updated.");
			}
		}
		private static splash win;
		private static StatusIcon trayIcon;
		// Create the popup menu, on right click.
			static void OnTrayIconPopup (object o, EventArgs args) {
				Menu popupMenu = new Menu();
				ImageMenuItem menuItemcc = new ImageMenuItem ("Start Control Center");
				Gtk.Image ccimg = new Gtk.Image(Stock.Execute, IconSize.Menu);
				menuItemcc.Image = ccimg;
				popupMenu.Add(menuItemcc);
			
				ImageMenuItem menuItemQuit = new ImageMenuItem ("Quit");
				Gtk.Image appimg = new Gtk.Image(Stock.Quit, IconSize.Menu);
				menuItemQuit.Image = appimg;
				popupMenu.Add(menuItemQuit);
				menuItemcc.Activated += delegate { Outils.Excecute("frugal-mono-tools","",false); };
				// Quit the application when quit has been clicked.
				menuItemQuit.Activated += delegate { Application.Quit(); };
				popupMenu.ShowAll();
				popupMenu.Popup();
			}

		public static void Main (string[] args)
		{
			
			System.Timers.Timer aTimer;
			if(args.Length==0)
			{
				Gtk.Application.Init();		
				
				if (configuration.Get_CheckUpdate()) 
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
					MainWindow Fen = new MainWindow();
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
							check();
							aTimer = new System.Timers.Timer();
		         			aTimer.Elapsed+=new ElapsedEventHandler(checkUpdate);
		        			// Set the Interval to 1 hour.
		        			aTimer.Interval=3600000;
		         			aTimer.Enabled=true;
							// Creation of the Icon
							trayIcon = new StatusIcon(new Pixbuf ("/usr/share/pixmaps/FrugalTools.png"));
							trayIcon.Visible = true;
					 		
							trayIcon.Activate += delegate { Outils.Excecute("frugal-mono-tools","",false); };

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

