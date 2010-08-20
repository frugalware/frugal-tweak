using System;
namespace frugalmonotools
{
	public static class Outils
	{
		public static Boolean Excecute(String Commande,string Arguments)
		{
			System.Diagnostics.Process proc = new System.Diagnostics.Process();
			proc.EnableRaisingEvents=false; 
			proc.StartInfo.FileName = Commande;
			proc.StartInfo.Arguments = Arguments;
			if (proc.Start())
			{
				proc.WaitForExit();
				return true;
			}
			return false;
		}
		/// <summary>
		///enable disable service,enable = false disable and stop this service 
		/// </summary>
		/// <param name="servicename">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="enable">
		/// A <see cref="System.Boolean"/>
		/// </param>
		public static void Service(string servicename, bool enable)
		{
			if (enable)
			{
				Outils.Excecute("service "+servicename+" add","");
				Outils.Excecute("service "+servicename+" start","");
			}
			else
			{
				Outils.Excecute("service "+servicename+" del","");
				Outils.Excecute("service "+servicename+" stop","");
			}
		}
		/*
		public static String ResultExcecute(String Commande,string Arguments)
		{
			ProcessStartInfo psi = new ProcessStartInfo ();
            psi.FileName = Commande;
            psi.Arguments = Arguments;
            psi.RedirectStandardOutput = true;
            psi.UseShellExecute = false;
 
            Process p =  Process.Start (psi);
            string ret = p.StandardOutput.ReadToEnd ();
            p.WaitForExit ();
			return ret;

		}*/
	}
}

