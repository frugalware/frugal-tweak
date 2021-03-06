using System;
using System.Collections;
using System.Collections.Generic;

namespace frugalmonotools
{
	
	public class packageCheck
	{
		public string packagename;
		public string packageversion;
		public string repo;
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
				
				foreach (packageCheck pkg in Pkg)
				{
					
					if(pkg.packagename==pkginstall.packagename)
					{
						bool AddIt = false;
						if(string.Compare(pkginstall.packageversion,pkg.packageversion)<0) 
							AddIt =true;
						
						//pff 
						//3.6.8 is > to 3.6.10 but that can be some string
						//so compare string
						try
						{
							//parse numeric
							string []tmpinst = pkginstall.packageversion.Split('-');
							string []tmpupdate = pkg.packageversion.Split('-');
							string [] versionInstalled= tmpinst[0].Split('.');
							string []updateVersion= tmpupdate[0].Split('.');
							int i =0;
							foreach(string ver in updateVersion)
							{
								if(string.Compare(versionInstalled[i],ver)<0)
									AddIt=true;
								if (int.Parse(versionInstalled[i])<int.Parse(ver))
									AddIt=true;
								i++;
							}
						}
						catch{}
						if ((PacmanG2.ShouldPackageForce(pkg.packagename+"-"+pkg.packageversion,pkg.repo)) && 
						    (pkginstall.packageversion!=pkg.packageversion))
							AddIt=true;
						if(AddIt)
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
			List<Package> packages=MainClass.pacmanG2.Search("*",repo,false);
			foreach (Package package in packages)
			{
				if(repo=="local")
				{
					//for repo local we can added all packages
					packageCheck pkgrepo = new packageCheck();
					pkgrepo.packagename=package.GetPkgname();
					pkgrepo.packageversion=package.GetPkgversion();
					pkgs.Add(pkgrepo);
				}
				else
				{
					bool AddIt = true;
					foreach (string pkgignore in MainClass.pacmanG2.GetignorePkg())
					{
						if(pkgignore==MainClass.pacmanG2.extractNamePackage(package.GetPkgname())) AddIt =false;
						if(MainClass.pacmanG2.extractNamePackage(pkgignore)==MainClass.pacmanG2.extractNamePackage(package.GetPkgname())) AddIt =false;
						
					}
					
					//don't add the package if already in the list
					//in case of user use some wip
					bool findIt = false;
					foreach (packageCheck pkg in pkgs)
					{
						if(package.GetPkgname()==pkg.packagename)
						{
							findIt=true;
							break;
						}
					}
					if((!findIt)&&(AddIt)) 
					{
						packageCheck pkgrepo = new packageCheck();
						pkgrepo.packagename=package.GetPkgname();
						pkgrepo.packageversion=package.GetPkgversion();
						pkgrepo.repo=repo;
						pkgs.Add(pkgrepo);
					}
				}
			}
				
		}
		
	}
	
}

