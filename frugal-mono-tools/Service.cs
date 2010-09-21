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
		public string GetDescription()
		{
			string filedesc = @"/etc/rc.d/rc."+this.Get_Name();
			string content = Outils.ReadFile(filedesc);
			string[] lines = content.Split('\n');
			foreach (string line in lines)
            {
				if (line.IndexOf("description:")>0)
				{
					if (Debug.ModeDebug) Console.WriteLine(line);
					return line.Replace("# description: ","");
				}
			}
			return "";
		}
		public bool IsStarted()
		{
			if(Debug.ModeDebug) Console.WriteLine(this.Get_Name());
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
				if(strSatus.Trim()=="")
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
			if(MainClass.boRoot)
				Outils.Excecute("/sbin/service",this.Get_Name()+" start",true);
			else
				Outils.ExcecuteAsRoot("/sbin/service "+this.Get_Name()+" start",true);
		}
		public void Stop()
		{
			if(MainClass.boRoot)
				Outils.Excecute("/sbin/service",this.Get_Name()+" stop",true);
			else
				Outils.ExcecuteAsRoot("/sbin/service "+this.Get_Name()+" stop",true);
		}
		public void EnableDisableOnBoot(bool enableit)
		{
			if(enableit)
				if(MainClass.boRoot)
					Outils.Excecute("/sbin/service",this.Get_Name()+" add",true);
				else
					Outils.ExcecuteAsRoot("/sbin/service "+this.Get_Name()+" add",true);
			else
				if(MainClass.boRoot)
					Outils.Excecute("/sbin/service",this.Get_Name()+" del",true);
				else
					Outils.ExcecuteAsRoot("/sbin/service "+this.Get_Name()+" del",true);

		}
	}
}

