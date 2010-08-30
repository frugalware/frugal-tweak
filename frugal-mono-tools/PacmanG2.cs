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

namespace frugalmonotools
{
	
	public class Package
	{
			public string pkgname;
			public string pkgversion;
			public string pkggroup;
			public string pkgdescription;
			public bool force;
			
	}
	public class PacmanG2
	{
		//const
		private const string ROOT_PATH="/";
		private const string PACMANG2_BDD="var/lib/pacman-g2/";
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
		
		private void EnumRepoProc(string section, string lParam)
		{
			if(Debug.ModeDebug) Console.WriteLine(section);
			fwRepo.Add(section);
		}
		
		public PacmanG2 ()
		{
			try{
				EnumRepoProcDelegate _cd_db = new EnumRepoProcDelegate(EnumRepoProc);
				if (pacman_initialize(ROOT_PATH)!=pm_errno)
				{
					pacman_parse_config(cch_pacmanconf,_cd_db,"");
				}
				fwRepo.Add("local");
				
			}
			catch{}
		}
	
		public void SelectRepo(string repo)
		{
			if (repo==repoInstalled) repo ="local";
			repoSelected=repo;
		}
		
		public List<Package> Search(string strSearch,string repo)
		{
			if (repo==repoInstalled) repo ="local";
			//don't use pacman more quickly to use .net directly
			List<Package> packages = new List<Package>();
			string dirpkg=ROOT_PATH+PACMANG2_BDD+repo+"/";
			string[] dirs= Directory.GetDirectories(dirpkg, "*"+strSearch+"*");
			if (repo=="") return null;
			
		 	//Console.WriteLine("The number of packages is {0}.", dirs.Length);
            foreach (string dir in dirs) 
            {
				Package package = new Package();
				
				string tmpname=dir.Replace(dirpkg,"");
				package.pkgname=extractNamePackage(tmpname);
				package.pkgversion=extractVersionPackage(tmpname);
				package.pkgdescription=_getDescription(package.pkgname+"-"+package.pkgversion,repo);
				package.pkggroup="";
				package.force=_PackageForce(package.pkgname+"-"+package.pkgversion,repo);
                //TODO extract group from file desc and extract version from name
				packages.Add(package);
            }
			return packages;
		}
		public static string SearchDescription(string Package,string repo)
		{
			return _getDescription(Package, repo);
			
		}
		private bool _PackageForce(string Package,string repo)
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
		private static string _getDescription(string Package,string repo)
		{
			string filedesc = ROOT_PATH+PACMANG2_BDD+"/"+repo+"/"+Package+"/desc";
			string content = Outils.ReadFile(filedesc);
			string[] lines = content.Split('\n');
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

