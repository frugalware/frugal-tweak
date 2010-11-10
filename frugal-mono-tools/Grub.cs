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
	public struct GrubEntry
	{
		public string title;
		public string options;
		
	}
	public class Grub
	{
		/*
		default=0
		timeout=5
		gfxmenu (hd0,2)/boot/grub/message
		
		title Frugalware 1.3.1340.g8af1a28 (Haven) - 2.6.35-fw2
			kernel (hd0,2)/boot/vmlinuz root=/dev/sda3 ro quiet resume=/dev/sda4
		
		title Windows Seven
		rootnoverify (hd0,0)
		savedefault
		makeactive
		chainloader +1 
		
		title Memtest86+
			kernel (hd0,2)/boot/memtest.bin*/


		private int _default=0;
		public int GetDefault()
		{
			return _default;
		}
		public void SetDefault(int value)
		{
			_default=value;
		}
		private int _timeout=0;
		public int GetTimeout()
		{
			return _timeout;
		}
		public void SetTimeOut(int value)
		{
			_timeout=value;
		}
		
		private string _gfx="";
		public string GetGfx()
		{
			return _gfx;
		}
		public void SetGfx(string value)
		{
			_gfx=value;
		}
		
		public List<GrubEntry> Entrys = new List<GrubEntry>();
		
		private const string cch_FileMenu = @"/boot/grub/menu.lst";
			
		public Grub ()
		{
			GrubEntry grubEntry = new GrubEntry();
			bool bo_entry = false;
			string str_MenuLst = Outils.ReadFile(cch_FileMenu);
			string[] lines = str_MenuLst.Split('\n');
			//search default
			foreach (string line in lines)
			{
				if (line.IndexOf("default=0")>=0)
				    {
						//default entry find
						string str_default = line.Replace("default=","");
						str_default=str_default.Trim();
						try{
								this.SetDefault(int.Parse(str_default));
						}
						catch{
								this.SetDefault(0);
						}
					}
				if (line.IndexOf("timeout=")>=0)
				    {
						string str_time = line.Replace("timeout=","");
						str_time=str_time.Trim();
						try{
								this.SetTimeOut(int.Parse(str_time));
						}
						catch{
								this.SetTimeOut(0);
						}
					}
				if (line.IndexOf("gfxmenu")>=0)
				    {
						string str_gfx = line.Replace("gfxmenu","");
						str_gfx=str_gfx.Trim();
						try{
								this.SetGfx(str_gfx);
						}
						catch{
								this.SetGfx("");
						}
					}
				if (line.IndexOf("title")>=0)
				{
					if(bo_entry)
					{
						Entrys.Add(grubEntry);
						bo_entry=false;
					}
					
					bo_entry=true;
					grubEntry = new GrubEntry();
					grubEntry.title=line.Replace("title","").Trim();
					
				}
				if (line.Trim()=="")
				{
					if(bo_entry)
						Entrys.Add(grubEntry);
					bo_entry=false;
				}
				if ((line.Trim()!="") && (line.IndexOf("title")<0) &&(bo_entry))
				{
					grubEntry.options+=line.Replace("\t","").TrimEnd()+"\n";
				}
				
			}
		}
		public void Save()
		{
			try{
			/*
			 default=0
			timeout=5
			gfxmenu (hd0,2)/boot/grub/message
			title Frugalware 1.3.3040.g42b497b (Nexon) - 2.6.36-fw1
			kernel (hd0,2)/boot/vmlinuz root=/dev/sda3 ro quiet resume=/dev/sda1
			*/
			StreamWriter MenuGrub = new StreamWriter(cch_FileMenu);
			MenuGrub.WriteLine("default="+this.GetDefault());
			MenuGrub.WriteLine("timeout="+this.GetTimeout());
			MenuGrub.WriteLine("gfxmenu "+this.GetGfx());
			 foreach (GrubEntry entry in Entrys)
	         {
				MenuGrub.WriteLine("title "+entry.title);
				MenuGrub.WriteLine(entry.options);
			}
			MenuGrub.Close();
			}
			catch{}
		}
	}
}

