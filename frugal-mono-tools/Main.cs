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
using System.Timers;
using Gtk;

namespace frugalmonotools
{
	class MainClass
	{
		//pacman-g2 initialise
		public static PacmanG2 pacmanG2 = new PacmanG2();
		
		public static IconSummaryBody notif= new IconSummaryBody();
		
		private static void UpdateBDD(object source, ElapsedEventArgs e)
		{
			Console.WriteLine("update pacman-g2 bdd");
			Outils.Excecute("pacman-g2"," -Sy",false);
		}
		private static void checkUpdate(object source, ElapsedEventArgs e)
		{
			Console.WriteLine("check update packages.");
			check();
		}
		private static void check()
		{
			
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
		public static void Main (string[] args)
		{
			System.Timers.Timer aTimer;
			Application.Init ();
			if(args.Length==0)
			{
				check();
				MainWindow win = new MainWindow ();
				win.Show ();
				Application.Run ();
			}
			else
			{
				switch(args[0])
				{
					case "--daemon":
						Console.WriteLine("Daemon mode");
						if (Mono.Unix.Native.Syscall.getuid()!=0)
						{
							Console.Write("Daemon should be started with root user");
							System.Environment.Exit(0);
						}
						//update packages bdd
						aTimer = new System.Timers.Timer();
	         			aTimer.Elapsed+=new ElapsedEventHandler(UpdateBDD);
	        			// Set the Interval to 1 hour.
	        			aTimer.Interval=3600000;
	         			aTimer.Enabled=true;
						Application.Run ();
						break;
					
					case "--update":
						//check if an update is avalaible
						//started with X session
						Console.WriteLine("check update packages.");
						check();
						aTimer = new System.Timers.Timer();
	         			aTimer.Elapsed+=new ElapsedEventHandler(checkUpdate);
	        			// Set the Interval to 1 hour.
	        			aTimer.Interval=3600000;
	         			aTimer.Enabled=true;
						Application.Run ();
						break;
					
					default:
						Console.WriteLine("Bad parameters exit...");
						break;
				}
			}
		}
	}
}

