using System;
using System.Collections;
using System.Collections.Generic;

namespace frugalmonotools
{
	public class packageCheck
	{
		string packagename;
		string packageversion;
	}
	
	public static class Update
	{
		static private List<packageCheck> InstallPkg = new List<packageCheck>();
		static private List<packageCheck> Pkg = new List<packageCheck>();
		static private List<packageCheck> UpdatePkg = new List<packageCheck>();
		static private PacmanG2 pacman = new PacmanG2();
		
		static public bool packageCheck()
		{
			_init();
			
			foreach (string repo in pacman.fwRepo)
			{
				if(repo=="local")
				{
					addList(InstallPkg,repo);
				}
				else
				{
					addList(Pkg,repo);
				}
				
			}
			return true;
		}
		
		static private void _init()
		{
			InstallPkg.Clear();
			Pkg.Clear();
			UpdatePkg.Clear();
		}
		
		static private void addList(List<packageCheck> pkgs,string repo)
		{
			List<Package> packages=pacman.Search("*",repo);
			pkgs.Clear();
			foreach (Package package in packages)
			{
				if(repo=="local")
				{
					//
				}
			}
				
		}
		
	}
}

