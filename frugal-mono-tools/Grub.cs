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

namespace frugalmonotools
{
	public struct GrubEntry
	{
		public string tittle;
		public string root;
		public string kernel;
		public string initrd;
		
	}
	public class Grub
	{
		private int _default=0;
		public int GetDefault()
		{
			return _default;
		}
		public void SetDefault(int value)
		{
			_default=value;
		}
		public List<string> Entrys = new List<string>();
		
		private const string cch_FileMenu = @"/boot/grub/menu.lst";
			
		public Grub ()
		{
			string str_MenuLst = Outils.ReadFile(cch_FileMenu);
			string[] lines = str_MenuLst.Split('\n');
			//search default
			foreach (string line in lines)
			{
				if (line.IndexOf("default")==0)
				    {
						//default entry find
						string str_default = line.Replace("default","");
						str_default=str_default.Trim();
						try{
								this.SetDefault(int.Parse(str_default));
						}
						catch{
								this.SetDefault(0);
						}
					}
			}
		}
	}
}

