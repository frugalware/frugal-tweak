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
namespace frugalmonotools
{
	
	public class Package
	{
			public string pkgname;
			public string pkgversion;
			public string pkggroup;
			public string pkgdescription;
			
	}
	public class PacmanG2
	{
		//don't use c# binding generated by swig
		const int PM_LOG_DEBUG=0x01;
		const int PM_OPT_LOGCB = 1;
		const int PM_OPT_LOGMASK =2;
		const int PM_OPT_USESYSLOG=3;
		const int PM_OPT_ROOT=4;
		const int PM_OPT_DBPATH=5;
		const int PM_OPT_CACHEDIR=6;
		const int PM_OPT_LOGFILE=7;
		const int PM_OPT_LOCALDB=8;
		const int PM_OPT_SYNCDB=9;
		const int PM_OPT_NOUPGRADE=10;
		const int PM_OPT_NOEXTRACT=11;
		const int PM_OPT_IGNOREPKG=12;
		const int PM_OPT_UPGRADEDELAY=13;
		/* Download */
		const int PM_OPT_PROXYHOST=14;
		const int PM_OPT_PROXYPORT=15;
		const int PM_OPT_XFERCOMMAND=16;
		const int PM_OPT_NOPASSIVEFTP=17;
		const int PM_OPT_DLCB=18;
		const int PM_OPT_DLFNM=19;
		const int PM_OPT_DLOFFSET=20;
		const int PM_OPT_DLT0=21;
		const int PM_OPT_DLT=22;
		const int PM_OPT_DLRATE=23;
		const int PM_OPT_DLXFERED1=24;
		const int PM_OPT_DLETA_H=25;
		const int PM_OPT_DLETA_M=26;
		const int PM_OPT_DLETA_S=27;
		/* End of download */
		const int PM_OPT_HOLDPKG=28;
		const int PM_OPT_CHOMP=29;
		const int PM_OPT_NEEDLES=30;
		const int PM_OPT_MAXTRIES=31;
		const int PM_OPT_OLDDELAY=32;
		const int PM_OPT_DLREMAIN=33;
		const int PM_OPT_DLHOWMANY=34;
		const int PM_OPT_HOOKSDIR=35;
		//const
		private const string ROOT_PATH="/";
		private const string PACMANG2_BDD="var/lib/pacman-g2/";
		private const string PACMAN_LOCAL="local/";
		private const int pm_errno = -1;
		private const string cch_pacmanconf ="/etc/pacman-g2.conf";
		
		//long maxPathLen = Syscall.pathconf("/", PathconfName._PC_PATH_MAX);
		
		[StructLayout(LayoutKind.Sequential)]
		public struct _pmlist_t
		{
			object data;
			int prev;
        	int next;
        	int last;
		}
		[StructLayout(LayoutKind.Sequential)]
		public struct _pmdb_t
		{
			string path;
			[ MarshalAs( UnmanagedType.ByValTStr, 
               SizeConst=128 )] 
			String treename;
		}
		
		[DllImport("libpacman.so")]
		private static extern int pacman_initialize(string root);
		[DllImport("libpacman.so")]
		private static extern int pacman_parse_config(string file, EnumRepoProcDelegate callback,string this_section);
		private delegate void EnumRepoProcDelegate(string section, string lParam);
		[DllImport("libpacman.so")]
		private static extern _pmdb_t pacman_db_register(string treename);
		/*
		[DllImport("libpacman.so")]
		private static extern int pacman_set_option(int parm, string data);
		[DllImport("libpacman.so")]
		private static extern _pmlist_t pacman_db_search(_pmdb_t pmdb_t);
		*/
		
		_pmdb_t pmdb_t;
		
		//public
		public string repoSelected="";
		public List<string> fwRepo = new List<string>();
		
		private void EnumRepoProc(string section, string lParam)
		{
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
			pmdb_t=pacman_db_register(repo);
			repoSelected=repo;
		}
		
		public List<Package> Search(string strSearch,string repo)
		{
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
				package.pkgname=tmpname;
				package.pkgdescription="";
				package.pkggroup="";
				package.pkgversion="";
                //TODO extract description/group from file desc and extract version from name
				packages.Add(package);
            }
			return packages;
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
		public bool IsInstalled(string strSearch)
		{
			string dirpkg=ROOT_PATH+PACMANG2_BDD+PACMAN_LOCAL+"/";
			string[] dirs= Directory.GetDirectories(dirpkg,strSearch+"-*");
			if (dirs.Length==0) return false;
			return true;
		}
	}
	
}

