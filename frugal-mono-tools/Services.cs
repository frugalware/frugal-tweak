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
using System.Collections;
using System.Collections.Generic;
using System.IO;
namespace frugalmonotools
{
	public static class ServicesRc
	{
		public static List<Service> Services = new List<Service>();
		private static bool _started=false;
		
		public static void CheckList(){
			if (_started) return;
			_started=false;
			Services.Clear();
			string rcFile="/etc/rc.d/";
			string[] files= Directory.GetFiles(rcFile,"rc.*",SearchOption.TopDirectoryOnly);
			
			foreach(string file in files)
			{
				string rcName=file.Replace(rcFile+"rc.","");
				if ((rcName!="halt") &&
					(rcName!="0")&&
					(rcName!="1")&& 
					(rcName!="2")&&
					(rcName!="3")&&
					(rcName!="4")&&
					(rcName!="5")&&
					(rcName!="6")&&
					(rcName!="K")&&
					(rcName!="M")&&
					(rcName!="S")&&
				    (rcName!="zz-splash")&&
				    (rcName!="rmount")&&
				    (rcName!="fsck")&&
				    (rcName!="splash")&&
					(rcName!="swap")&&
					(rcName!="sysctl")&&
					(rcName!="sysvinit")&&
					(rcName!="time")&&
					(rcName!="udev")&&
				    (rcName!="functions")&&
				    (rcName!="bootclean")&&
					(rcName!="console")&&
					(rcName!="font")&&
					(rcName!="frugalware")&&
					(rcName!="hostname")&&
					(rcName!="local")&&
					(rcName!="modules")&&
					(rcName!="mount")&&
					(rcName!="serial")&&
					(rcName!="single")&&
				    (rcName!="postgresql")&& //rc status crash should check it!!
				    (rcName!="reboot"))
				{
					try{
						if(Debug.ModeDebug) 
							Console.WriteLine(rcName);
						Service service = new Service(rcName);
					  	Services.Add(service);
					}
					catch(Exception exe)
					{
						Console.WriteLine(rcName+" is ignored");
						Console.WriteLine(exe.Message);
					}
				}
				
			}
			Console.WriteLine("Service initialisation finish");
			_started=false;
		}
	}
}

