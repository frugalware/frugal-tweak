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
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
namespace frugalmonotools
{
	public static class Outils
	{
		public static bool ResultAsk ;
		public static bool Ask(string message)
		{
			MessageBox dialog = new MessageBox(message);
			dialog.Run();
			return ResultAsk;
		}
		public static List<string>  WalkDirectoryTree(System.IO.DirectoryInfo root,string pattern)
	    {
			List<string>  strFiles = new List<string>(); 
	        System.IO.FileInfo[] files = null;
	        System.IO.DirectoryInfo[] subDirs = null;
	
	        // First, process all the files directly under this folder
	        try
	        {
	            files = root.GetFiles(pattern);
	        }
	        // This is thrown if even one of the files requires permissions greater
	        // than the application provides.
	        catch (UnauthorizedAccessException e)
	        {
	            // This code just writes out the message and continues to recurse.
	           Console.WriteLine(e.Message);
	        }
	
	        catch (System.IO.DirectoryNotFoundException e)
	        {
	            Console.WriteLine(e.Message);
	        }
	
	        if (files != null)
	        {
	            foreach (System.IO.FileInfo fi in files)
	            {
	             
	               if (Debug.ModeDebug) Console.WriteLine(fi.FullName);
					strFiles.Add(fi.FullName);
	            }
	
	            // Now find all the subdirectories under this directory.
	            subDirs = root.GetDirectories();
	
	            foreach (System.IO.DirectoryInfo dirInfo in subDirs)
	            {
	                // Resursive call for each subdirectory.
					List<string>  strFiles2 = new List<string>(); 
	                strFiles2=WalkDirectoryTree(dirInfo,pattern);
					 foreach (string strfile in strFiles2)
	            	{
						strFiles.Add(strfile);
					}
	            }
	        }      
			return strFiles;
	    }

		public static void Inform(string title,string text)
		{
			try{
				IconSummaryBody notif= new IconSummaryBody();
				if(MainClass.configuration.Get_ShowNotif()) notif.ShowMessage(title,text);
			}
			catch{
				Console.WriteLine("System don't support notification");
				Console.WriteLine(title+" : "+text);
			}
		}
		public static string getoutput( string cmd )
		{
			string[] parts = cmd.Split( ' ' );	
			string cmd_name = parts[ 0 ];	
			string arguments = "";
			if( parts.Length > 1 ){
				for( int i = 1; i < parts.Length; i++ ){
					arguments += " " + parts[ i ];
				}
			}
			Process proc = new Process( );
			proc.StartInfo.FileName = cmd_name;
			proc.StartInfo.Arguments = arguments;
			proc.StartInfo.UseShellExecute = false;
			proc.StartInfo.RedirectStandardError = true;
			proc.StartInfo.RedirectStandardOutput = true;
			try
			{
				if( proc.Start( ) )
				{
					proc.WaitForExit( );
					string output = proc.StandardOutput.ReadToEnd().TrimEnd();
					string error = proc.StandardError.ReadToEnd().TrimEnd();
					if( output.Equals( "" ) || output.Equals( " " ) )
					{
						if (Debug.ModeDebug) Console.WriteLine(error);
						return error;
					}
						else
					{
						if(Debug.ModeDebug) Console.WriteLine(output);
						return output;
					}
				}
			}
			catch( System.ComponentModel.Win32Exception w32e )
			{
				string ret = "Error Thrown: " + w32e.ToString( );
				return ret;
			}
			return "Broke";
		}

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
		public static Boolean ExcecuteAsRoot(string Commande,bool wait)
		{
			string str_CommandeRoot;
			if(MainClass.boRoot)
			{
				string[] parts = Commande.Split( ' ' );	
				str_CommandeRoot = parts[ 0 ];	
				string arguments = "";
				if( parts.Length > 1 ){
				for( int i = 1; i < parts.Length; i++ ){
					arguments += " " + parts[ i ];
					}
				}
				Commande=arguments;
			}
			else
			{
			if(MainClass.pacmanG2.IsInstalled("gksu-frugalware"))
			   str_CommandeRoot="gksu";
			else
			   str_CommandeRoot="ksu";
			}
			System.Diagnostics.Process proc = new System.Diagnostics.Process();
			proc.EnableRaisingEvents=false; 
			proc.StartInfo.FileName = str_CommandeRoot;
			proc.StartInfo.Arguments = Commande;
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
		public static string ReadFile(string fileToRead)
		{
			try
			{
				System.IO.StreamReader textFile = new System.IO.StreamReader(fileToRead);
                string fileContents = textFile.ReadToEnd();
                textFile.Close();
				fileContents = fileContents.Replace("\n\n", "\n");
				fileContents = fileContents.Replace("\\\n", "");
				return fileContents;
			}
			catch(Exception exe)
			{
				Console.WriteLine(exe.Message);
				return "";
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

