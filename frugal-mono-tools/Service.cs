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
using System.IO;
using System.Text.RegularExpressions;
namespace frugalmonotools
{
	public class Service
	{
		private string _name;
		public string Get_Name()
		{
			return _name;
		}
		public Service (string name)
		{
			_name=name;
		}
		public bool IsStarted()
		{
			try
			{
				string strSatus=Outils.getoutput("/sbin/service "+this.Get_Name()+" status");
				if(Regex.Matches(strSatus,"  ON ").Count>0)
				{
					return true;
				}
				if(Regex.Matches(strSatus,"  OFF").Count>0)
				{
					return false;
				}
				Console.WriteLine(this.Get_Name()+" don't use status this service should be fixed");
				return false;
			}
			catch{return false;}
					
		}
		private bool _findRunlevel(int runlevel)
		{
			try
			{
			string rcFile="/etc/rc.d/rc"+runlevel.ToString()+".d/";
			string[] files= Directory.GetFiles(rcFile,"*rc."+this.Get_Name());
			if (files.Length==1)
				return true;
			else
				return false;
			}
			catch(Exception exe)
			{
				Console.WriteLine(exe.Message);
				return false;
			}
		}
		public bool IsStartedOnBoot()
		{
			//check on all runlevel
			int i =0;
			while(i<=6)
			{
				if (_findRunlevel(i)) return true;
				i++;
			}
			return false;
		}
		public void Start()
		{
			Outils.Excecute("/sbin/service",this.Get_Name()+" start",false);
		}
		public void Stop()
		{
			Outils.Excecute("/sbin/service",this.Get_Name()+" stop",false);
		}
		public void EnableDisableOnBoot(bool enableit)
		{
			if(enableit)
				Outils.Excecute("/sbin/service",this.Get_Name()+" add",false);
			else
				Outils.Excecute("/sbin/service",this.Get_Name()+" del",false);
		}
	}
}

