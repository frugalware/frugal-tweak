using System;
using System.Diagnostics;
using System.Timers;

namespace Service
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Outils.Excecute("pacman-g2"," -Sy",true);
			System.ServiceProcess.ServiceBase[] servicesToRun;
 			servicesToRun = new System.ServiceProcess.ServiceBase[] { new Service()};
			System.ServiceProcess.ServiceBase.Run(servicesToRun);
			string strPID = "/tmp/fwmono";
			int pid = Mono.Unix.UnixProcess.GetCurrentProcessId();
			System.IO.File.Delete(strPID);
			System.IO.StreamWriter FilePid = new System.IO.StreamWriter(strPID);
			FilePid.WriteLine("PID="+pid.ToString());
			FilePid.Close();
		}
	}
	public static class Outils
	{
		public static Boolean Excecute(String Commande,string Arguments,bool wait)
		{
			System.Diagnostics.Process proc = new System.Diagnostics.Process();
			proc.EnableRaisingEvents=false; 
			proc.StartInfo.FileName = Commande;
			proc.StartInfo.Arguments = Arguments;
			proc.StartInfo.RedirectStandardError = true;
			//proc.StartInfo.RedirectStandardInput = true;
			proc.StartInfo.RedirectStandardOutput = true;
			proc.StartInfo.UseShellExecute = false;
			if (!proc.Start()) return false;
			if (wait) proc.WaitForExit();
			return true;
		}
		
	}
	public class Service : System.ServiceProcess.ServiceBase 
	{
		System.Timers.Timer aTimer;
		
		public Service ()
		{
			
		}
		protected override void OnStart(string[] args)
		{
			//update packages bdd
			aTimer = new System.Timers.Timer();
			aTimer.Elapsed+=new ElapsedEventHandler(UpdateBDD);
			// Set the Interval to 1 hour.
			aTimer.Interval=3600000;
			aTimer.Enabled=true;
		}
		private static void UpdateBDD(object source, ElapsedEventArgs e)
		{	
			//Console.WriteLine("update pacman-g2 bdd");
			Outils.Excecute("pacman-g2"," -Sy",true);
		}
	}
}

