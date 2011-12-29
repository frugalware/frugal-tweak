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
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using Mono.Unix.Native;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;

namespace frugalmonotools
{
	
	public class Package
	{
			private string _contentDesc="";
			private string _repo;
			public string GetRepo()
			{
				return _repo;
			}
			public void SetRepo(string repo)
			{
				_repo=repo;
			}
			private string _pkgname;
			public string GetPkgname()
			{
				return _pkgname;
			}
			public void SetPkgname(string pkgname)
			{
				_pkgname=pkgname;
			}
			private string _pkgversion;
			public string GetPkgversion(){
				return _pkgversion;
			}
			public void SetPkgverion(string version)
			{
				_pkgversion=version;
			}
			
		
			public Package(){
			}
			
			private void _setContent()
			{
				if(_contentDesc!="") return;
				string filedesc = PacmanG2.ROOT_PATH+PacmanG2.PACMANG2_BDD+"/"+this.GetRepo()+"/"+this.GetPkgname()+
										"-"+this.GetPkgversion()+"/desc";
				if (File.Exists(filedesc))
				{
					_contentDesc=Outils.ReadFile(filedesc);
				}
				else
				{
					filedesc = PacmanG2.ROOT_PATH+PacmanG2.PACMANG2_BDD+"/"+this.GetRepo()+"/"+this.GetPkgname()+"/desc";
					_contentDesc=Outils.ReadFile(filedesc);
				}
			}
			public string GetDescription()
			{
				_setContent();
				if(_contentDesc=="") return "";
				string content="";
				string[] lines = _contentDesc.Split('\n');
				bool FindDescr = false;
	            foreach (string line in lines)
	            {
					if(FindDescr)
					{
						content=line;
						break;
					}
					if (line=="%DESC%") 
						FindDescr=true;
					
				}
				return content;	
			}
		
		public bool ShouldForce()
		{
			_setContent();
			if(_contentDesc=="") return false;
			string[] lines = _contentDesc.Split('\n');
			foreach (string line in lines)
            {
				if (line=="%FORCE%") 
					return true;
			}
			return false;
		}
		
		public string GetGroup()
		{
			_setContent();
			string content="";
			if(_contentDesc=="") return "";
			string[] lines = _contentDesc.Split('\n');
			bool FindDescr = false;
            foreach (string line in lines)
            {
				if(FindDescr)
				{
					content=line;
					break;
				}
				if (line=="%GROUPS%") 
					FindDescr=true;
				
			}
			return content;
			
		}
	}
	public class PacmanG2
	{
		//const
		public static  string ROOT_PATH="/";
		public static string PACMANG2_BDD="var/lib/pacman-g2/";
		private const string PACMANG2_LOCAL="local/";
		private const int pm_errno = -1;
		private const string cch_pacmanconf ="/etc/pacman-g2.conf";
		
		//long maxPathLen = Syscall.pathconf("/", PathconfName._PC_PATH_MAX);
		
		[DllImport("libpacman.so")]
		private static extern int pacman_initialize(string root);
		[DllImport("libpacman.so")]
		private static extern int pacman_parse_config(string file, EnumRepoProcDelegate callback,string this_section);
		private delegate void EnumRepoProcDelegate(string section, string lParam);
		[DllImport("libpacman.so")]
		private static extern void pacman_db_register(string treename);
	
		//public
		public string repoSelected="";
		public List<string> fwRepo = new List<string>();
		public const string repoInstalled= "Installed";
		
		private List<string> ignorePkg = new List<string>();
		public List<string> GetignorePkg ()
		{
			_ReadIgnorePkg();
			return ignorePkg;
		}
		private void EnumRepoProc(string section, string lParam)
		{
			if(Debug.ModeDebug) Console.WriteLine(section);
			fwRepo.Add(section);
		}
		
		public PacmanG2 ()
		{
			try{
				//load ignore pkg
				EnumRepoProcDelegate _cd_db = new EnumRepoProcDelegate(EnumRepoProc);
				if (pacman_initialize(ROOT_PATH)!=pm_errno)
				{
					pacman_parse_config(cch_pacmanconf,_cd_db,"");
				}
				fwRepo.Add("local");
				
			}
			catch{}
		}
		public void SetIgnorePkg(string packagename,bool updateAllIgnore )
		{
			try
			{					
				string strPacmanConf =Outils.ReadFile(cch_pacmanconf);
				string[] lines = strPacmanConf.Split('\n');	
				string[] result= new string[lines.Length];
				string lineResult;
				int i = 0;
					foreach (string line in lines) 
		            {
						lineResult=line;
						if (System.Text.RegularExpressions.Regex.Matches(line, "IgnorePkg").Count>0)
						{
							//find it :p
							if(updateAllIgnore)
							{
								lineResult="IgnorePkg = "+ packagename;
							}
							else
							{
								lineResult=lineResult.Replace("=","= "+packagename+" ");
								lineResult=lineResult.Replace("#","");
								this.ignorePkg.Add(packagename);
							}
						}
						result[i]=lineResult;
						i++;
					}
				StreamWriter File = new StreamWriter(cch_pacmanconf);
				foreach (string line in result) 
		        {
					File.WriteLine(line);
				}
				File.Close();
				
			}
			catch(Exception exe)
			{
				Console.WriteLine("Can't update pacman-g2.conf");
				Console.WriteLine(exe.Message);
			}
			if(updateAllIgnore)
			{
				_ReadIgnorePkg();
			}
		}
		private void _ReadIgnorePkg(){
			try{
				ignorePkg.Clear();
				string filedesc = cch_pacmanconf;
				string content = Outils.ReadFile(filedesc);
				string[] lines = content.Split('\n');	
				foreach (string line in lines) 
	            {
					if (Regex.Matches(line, "IgnorePkg").Count>0)
					{
						string []contentspkg =line.Split('=');
						if (contentspkg.Length==0 )return;
						string pkgs= contentspkg[1];
						string []pkgsIgnore =pkgs.Split(' ');
						foreach (string pkgIgnore in pkgsIgnore) 
	            		{
							ignorePkg.Add(pkgIgnore.Trim());
						}
						break;
					}
					
				}
			}
			catch{}
		}
		
		public void SelectRepo(string repo)
		{
			if (repo==repoInstalled) repo ="local";
			repoSelected=repo;
		}
		
		public List<Package> Search(string strSearch,string repo,bool readInfo)
		{
			if (repo==repoInstalled) repo ="local";
			//don't use pacman more quickly to use .net directly
			List<Package> packages = new List<Package>();
			string dirpkg=ROOT_PATH+PACMANG2_BDD+repo+"/";
			string[] dirs= Directory.GetDirectories(dirpkg, "*"+strSearch+"*");
			if (repo=="") return null;
		
            foreach (string dir in dirs) 
            {
				Package package = new Package();
				
				string tmpname=dir.Replace(dirpkg,"");
				package.SetPkgname(extractNamePackage(tmpname));
				package.SetPkgverion(extractVersionPackage(tmpname));
				package.SetRepo(repo);
				/*package.pkgdescription=_getDescription(package.pkgname+"-"+package.pkgversion,repo);
				package.pkggroup=_getGroup(package.pkgname+"-"+package.pkgversion,repo);
				package.force=ShouldPackageForce(package.pkgname+"-"+package.pkgversion,repo);*/
				packages.Add(package);
            }
			return packages;
		}
		public static bool ShouldPackageForce(string Package,string repo)
		{
			string filedesc = ROOT_PATH+PACMANG2_BDD+"/"+repo+"/"+Package+"/desc";
			string content = Outils.ReadFile(filedesc);
			string[] lines = content.Split('\n');
			foreach (string line in lines)
            {
				if (line=="%FORCE%") 
					return true;
			}
			return false;
		}
	/*	public static string SearchDescription(string Package,string repo)
		{
			return _getDescription(Package, repo);
			
		}*/
		
		
		public string extractNamePackage(string file)
		{
			string[] words = file.Split('-');
			int nb,i=1;
			string packageName="";
			nb=words.Length;
			packageName=words[0];
       		while(i<=nb-3)
       		{
            	packageName=packageName+"-"+words[i];
				i++;
        	}

			return packageName;
		}
		
		public string extractVersionPackage(string file)
		{
			string[] words = file.Split('-');
			int nb;
			string packageVersion="";
			nb=words.Length;
			packageVersion=words[nb-2]+"-"+words[nb-1];
			return packageVersion;
		}
		/// <summary>
		/// Gets the screenshot.
		/// </summary>
		/// <returns>
		/// The screenshot.
		/// </returns>
			public Gdk.Pixbuf GetScreenshot(string str_pkg)
			{
				 try
			﻿  ﻿  ﻿  {
			﻿  ﻿  ﻿  ﻿  HttpWebRequest myReq =(HttpWebRequest)WebRequest.Create("http://screenshots.debian.net/thumbnail/"+str_pkg);﻿  ﻿  ﻿  ﻿  
			﻿  ﻿  ﻿  ﻿  HttpWebResponse response = (HttpWebResponse)myReq.GetResponse();
			﻿  ﻿  ﻿  ﻿  
			﻿  ﻿  ﻿  ﻿  if(response.StatusCode == HttpStatusCode.OK)
			﻿  ﻿  ﻿  ﻿  {
			﻿  ﻿  ﻿  ﻿  ﻿  ﻿  ﻿  return new Gdk.Pixbuf(response.GetResponseStream());﻿  
			﻿  ﻿  ﻿  ﻿   }
			﻿  ﻿  ﻿  ﻿  
			﻿  ﻿  ﻿  }
			﻿  ﻿  ﻿  catch(Exception)
			﻿  ﻿  ﻿  {
			﻿  ﻿  ﻿  }
				return null;
			}
		public bool IsInstalled(string strSearch)
		{
			string dirpkg=ROOT_PATH+PACMANG2_BDD+PACMANG2_LOCAL+"/";
			string[] dirs= Directory.GetDirectories(dirpkg,strSearch+"-*");
			if (dirs.Length==0) return false;
			bool Find = false;
			foreach (string dir in dirs) 
            {
				try
				{
					string tmp=dir;
					tmp=dir.Replace(ROOT_PATH+PACMANG2_BDD+PACMANG2_LOCAL+strSearch,"");
					//tmp contain -1.1.19-1, should find only 2 - for be the same package
					int count = Regex.Matches(tmp, "-").Count;
					if (count==2)
					{
						Find=true;
						break;
					}
				}
				catch{}
			}
			return Find;
		}
	}
	
}

