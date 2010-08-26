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
namespace frugalmonotools
{
	public static class Outils
	{
		public static Boolean Excecute(String Commande,string Arguments,bool wait)
		{
			System.Diagnostics.Process proc = new System.Diagnostics.Process();
			proc.EnableRaisingEvents=false; 
			proc.StartInfo.FileName = Commande;
			proc.StartInfo.Arguments = Arguments;
			if (!proc.Start()) return false;
			if (wait) proc.WaitForExit();
			return true;
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
				Outils.Excecute("service "+servicename+" add","",true);
				Outils.Excecute("service "+servicename+" start","",true);
			}
			else
			{
				Outils.Excecute("service "+servicename+" del","",true);
				Outils.Excecute("service "+servicename+" stop","",true);
			}
		}
		/// <summary>
		/// return true if service is enable on startup
		/// verify on each runlevel 
		/// </summary>
		/// <param name="servicename">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="System.Boolean"/>
		/// </returns>
		public static bool ServiceOnStartUp(string servicename)
		{
			string ch_DirRC="/etc/rc.d/";
	
			if (System.IO.File.Exists(ch_DirRC+"rc0.d/"+servicename)) return true;
			if (System.IO.File.Exists(ch_DirRC+"rc1.d/"+servicename)) return true;
			if (System.IO.File.Exists(ch_DirRC+"rc2.d/"+servicename)) return true;
			if (System.IO.File.Exists(ch_DirRC+"rc3.d/"+servicename)) return true;
			if (System.IO.File.Exists(ch_DirRC+"rc4.d/"+servicename)) return true;
			if (System.IO.File.Exists(ch_DirRC+"rc5.d/"+servicename)) return true;
			return false;
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

