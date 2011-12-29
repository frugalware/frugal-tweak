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
	public static class DirTweak
	{
		public static string GetHome()
		{
			return Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
		}
		public static string GetDirFrugalTweak()
		{
			string dirTweak= GetHome()+"/.frugalware-tweak";
			if(!Directory.Exists(dirTweak))
				Directory.CreateDirectory(dirTweak);
			return dirTweak;
			
		}
	}
	//only uses by rss for now
	public class Cache
	{
		private string oldconfFile = DirTweak.GetHome()+@"/.cache/FrugalTools";
		private const string confFile =  "/cache";
		private string _latest="";
		public Cache()
		{
			//read value
			try{
				string filedesc = GetConfFile();
				string content = Outils.ReadFile(filedesc);
				string[] lines = content.Split('\n');	
				foreach (string line in lines) 
				{
					 if (Regex.Matches(line, "last").Count>0)
						{
							string last = line.Split('"')[1];
							this.SetLatest(last);
						}
				}
			}
			catch{	}
		}
		public void CacheSave()
		{
			try
			{
				StreamWriter FileConf = new StreamWriter(GetConfFile());
				FileConf.WriteLine("last=\""+this.GetLatest()+"\"");
				FileConf.Close();
			}
			catch(Exception exe)
			{
				Console.WriteLine(exe.Message);
			}
		}
		public string GetConfFile(){
			if(File.Exists(oldconfFile))
			   File.Move(oldconfFile,DirTweak.GetDirFrugalTweak()+confFile);
			return DirTweak.GetDirFrugalTweak()+confFile;
		}
		
		public string GetLatest()
		{
			return _latest;
		}
		public void SetLatest(string valeur)
		{
			_latest=valeur;
		}
	}
	
	public class Configuration
	{
	
		private string oldconfFile=DirTweak.GetHome()+@"/.config/FrugalTools.conf";
		private const string confFile= "/fwtweak.conf";
		private bool _checkUpdate = false;
		private bool _startWithX = true;
		private bool _showNotif = true;
		private bool _showSplash = true;
		private string _noteLogin ="Login";
		private string _notePass="";
		
		public string GetConfFile(){
			if(File.Exists(oldconfFile))
			   File.Move(oldconfFile,DirTweak.GetDirFrugalTweak()+confFile);
			return DirTweak.GetDirFrugalTweak()+confFile;
		}
		public bool Get_CheckUpdate()
		{
			return _checkUpdate;
		}
		public void Set_CheckUpdate(bool valeur)
		{
			_checkUpdate=valeur;
		}
		public bool Get_StartWithX()
		{
			return _startWithX;
		}
		public void Set_StartWithX(bool valeur)
		{
			_startWithX=valeur;
		}
		public void Set_ShowNotif(bool valeur)
		{
			_showNotif=valeur;
		}
		public bool Get_ShowNotif()
		{
			return _showNotif;
		}
		public void Set_ShowSplash(bool valeur)
		{
			 _showSplash=valeur;
		}
		public bool Get_ShowSplash()
		{
			return _showSplash;
		}
		public void Set_NoteLogin(string login)
		{
			_noteLogin=login;
		}
		public string Get_NoteLogin()
		{
			return _noteLogin;
		}
		public void Set_NotePass(string pass)
		{
			_notePass=pass;
		}
		public string Get_NotePass()
		{
			return _notePass;
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
					if (Regex.Matches(line, "dontstartwithx").Count>0)
						{
							Set_StartWithX(false);
						}
					if (Regex.Matches(line, "dontshownotif").Count>0)
						{
							Set_ShowNotif(false);
						}
					if (Regex.Matches(line, "dontshowsplash").Count>0)
						{
							Set_ShowSplash(false);
						}
					if (Regex.Matches(line, "notelogin=").Count>0)
						{
							Set_NoteLogin(line.Split('=')[1]);
						}
					if (Regex.Matches(line, "notepass=").Count>0)
						{
							Set_NotePass(line.Split('=')[1]);
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
				if (!Get_StartWithX()) FileConf.WriteLine("dontstartwithx");
				if (!Get_ShowNotif()) FileConf.WriteLine("dontshownotif");
				if (!Get_ShowSplash()) FileConf.WriteLine("dontshowsplash");
				FileConf.WriteLine("notelogin="+this.Get_NoteLogin());
				FileConf.WriteLine("notepass="+this.Get_NotePass());
				FileConf.Close();
			}
			catch(Exception exe)
			{
				Console.WriteLine(exe.Message);
			}
		}
	}
}

