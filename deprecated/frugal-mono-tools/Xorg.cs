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
	public class Xorg
	{
		public List<string>  DriversInstalled = new List<string>(); 
		public Xorg ()
		{
			_findDrivers();
		}
		private void _findDrivers()
		{
			//divers is installed into /usr/lib/xorg/modules/drivers/*_drv.so
			string ext="_drv.so";
			string dirDrivers="/usr/lib/xorg/modules/drivers/";
			string[] files= Directory.GetFiles(dirDrivers,"*"+ext);
			
            foreach (string file in files) 
            {
				string strDriver = file;
				strDriver=strDriver.Replace(dirDrivers,"");
				strDriver=strDriver.Replace(ext,"");
				DriversInstalled.Add(strDriver);
				
			}
		}
		 
	}
}

