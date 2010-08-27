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
	
	public class Update
	{
		private List<packageCheck> InstallPkg = new List<packageCheck>();
		private List<packageCheck> Pkg = new List<packageCheck>();
		public List<packageCheck> UpdatePkg = new List<packageCheck>();
		private PacmanG2 pacman = new PacmanG2();
		
		public Update()
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
			foreach (packageCheck pkginstall in InstallPkg)
			{
				//TODO : respect Ignorepkg
				foreach (packageCheck pkg in Pkg)
				{
					if(pkg.packagename==pkginstall.packagename)
					{
						//basic test for beginning
						if(pkg.packageversion!=pkginstall.packageversion)
						{
							UpdatePkg.Add(pkg);
						}
						break;
					}
				
				}
				
			}
		}
		public bool CheckUpdate()
		{
			if(UpdatePkg.Count>0) return true;
			return false;
		}
		
		private void _init()
		{
			InstallPkg.Clear();
			Pkg.Clear();
			UpdatePkg.Clear();
		}
		
		private void addList(List<packageCheck> pkgs,string repo)
		{
			List<Package> packages=pacman.Search("*",repo);
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

