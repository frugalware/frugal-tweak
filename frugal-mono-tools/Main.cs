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
		private static void UpdateBDD(object source, ElapsedEventArgs e)
		{
			Console.WriteLine("update pacman-g2 bdd");
			Outils.Excecute("pacman-g2"," -Sy",false);
			
		}
		
		public static void Main (string[] args)
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
					System.Timers.Timer aTimer = new System.Timers.Timer();
         			aTimer.Elapsed+=new ElapsedEventHandler(UpdateBDD);
        			// Set the Interval to 1 hour.
        			aTimer.Interval=3600000;
         			aTimer.Enabled=true;
					while(true){}
					
				case "--update":
					//check if an update is avalaible
					//started with X session
					break;
				default:
					Console.WriteLine(args[0]);
					Application.Init ();
					MainWindow win = new MainWindow ();
					win.Show ();
					Application.Run ();
					break;
			}
		}
	}
}

