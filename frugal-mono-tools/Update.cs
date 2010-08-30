using System;
using System.Collections;
using System.Collections.Generic;

namespace frugalmonotools
{
	
	public class packageCheck
	{
		public string packagename;
		public string packageversion;
	}
	
	public static class Update
	{
		private static bool _started=false; 
		private static List<packageCheck> InstallPkg = new List<packageCheck>();
		private static List<packageCheck> Pkg = new List<packageCheck>();
		public static List<packageCheck> UpdatePkg = new List<packageCheck>();
		
		private static void _init()
		{
			if(Debug.ModeDebug) Console.WriteLine("Init update");
			_clear();
			
			foreach (string repo in MainClass.pacmanG2.fwRepo)
			{
				if(Debug.ModeDebug) Console.WriteLine(repo);
				if(repo=="local")
				{
					addList(InstallPkg,repo);
				}
				else
				{
					addList(Pkg,repo);
				}
				
			}
			foreach (packageCheck pkginstall in InstallPkg)
			{
				//TODO : respect Ignorepkg
				foreach (packageCheck pkg in Pkg)
				{
					
					if(pkg.packagename==pkginstall.packagename)
					{
						//TODO don't forgot options=("force")
						if(string.Compare(pkginstall.packageversion,pkg.packageversion)<0)
						{
							UpdatePkg.Add(pkg);
						}
						break;
					}
				
				}
				
			}
		}
		public static bool CheckUpdate()
		{
			if(_started) 
			{
				if(Debug.ModeDebug) Console.WriteLine("already started.");
				return false;
			}
			_started=true;
			_init();
			if(Debug.ModeDebug) Console.WriteLine("Check update");
			if(UpdatePkg.Count>0) 
			{
				_started=false;
				return true;
			}
			_started=false;
			return false;
		}
		
		private static void _clear()
		{
			InstallPkg.Clear();
			Pkg.Clear();
			UpdatePkg.Clear();
		}
		
		private static void addList(List<packageCheck> pkgs,string repo)
		{
			List<Package> packages=MainClass.pacmanG2.Search("*",repo);
			foreach (Package package in packages)
			{
				if(repo=="local")
				{
					//for repo local we can added all packages
					packageCheck pkgrepo = new packageCheck();
					pkgrepo.packagename=package.pkgname;
					pkgrepo.packageversion=package.pkgversion;
					pkgs.Add(pkgrepo);
				}
				else
				{
					//don't add the package if already in the list
					//in case of user use some wip
					bool findIt = false;
					foreach (packageCheck pkg in pkgs)
					{
						if(package.pkgname==pkg.packagename)
						{
							findIt=true;
							break;
						}
					}
					if(!findIt)
					{
						packageCheck pkgrepo = new packageCheck();
						pkgrepo.packagename=package.pkgname;
						pkgrepo.packageversion=package.pkgversion;
						pkgs.Add(pkgrepo);
					}
				}
			}
				
		}
		
	}
	
}

