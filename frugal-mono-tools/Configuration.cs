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
using System.Text.RegularExpressions;
using System.IO;
namespace frugalmonotools
{
	public class Configuration
	{
	
		private const string confFile=@"/.config/FrugalTools.conf";
		private bool _checkUpdate = false;
		public string GetConfFile(){
			return Environment.GetFolderPath(System.Environment.SpecialFolder
.Personal)+confFile;
		}
		public bool Get_CheckUpdate()
		{
			return _checkUpdate;
		}
		public void Set_CheckUpdate(bool valeur)
		{
			_checkUpdate=valeur;
		}
		
		public Configuration ()
		{
			//read value
			try{
			string filedesc = GetConfFile();
			string content = Outils.ReadFile(filedesc);
			string[] lines = content.Split('\n');	
			foreach (string line in lines) 
			{
				 if (Regex.Matches(line, "checkupdate").Count>0)
					{
						Set_CheckUpdate(true);
					}
			}
			}
			catch
			{
				//default value
			}
			
			
		}
		public void ConfSave()
		{
			try
			{
				StreamWriter FileConf = new StreamWriter(GetConfFile());
				if (Get_CheckUpdate()) FileConf.WriteLine("checkupdate");
				FileConf.Close();
			}
			catch(Exception exe)
			{
				Console.WriteLine(exe.Message);
			}
		}
	}
}

