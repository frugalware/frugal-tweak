// /*
//  *  Copyright (C) 2010 by Gaetan Gourdin <bouleetbil@frogdev.info>
//  *
//  *  This program is free software; you can redistribute it and/or modify
//  *  it under the terms of the GNU General Public License as published by
//  *  the Free Software Foundation; either version 2 of the License, or
//  *  (at your option) any later version.
//  *
//  *  This program is distributed in the hope that it will be useful,
//  *  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  *  GNU General Public License for more details.
//  *
//  *  You should have received a copy of the GNU General Public License
//  *  along with this program; if not, write to the Free Software
//  *  Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA 02111-1307, USA.
//  */
using System;
using System.IO;
namespace frugalmonotools
{
	public class ConfSystem
	{
		const  string cch_hostname =@"/etc/HOSTNAME";
		const string cch_release=@"/etc/frugalware-release";
		const string cch_locale=@"/etc/profile.d/lang.sh";
		private string _hostname;
		private string _locale;
		private string _distribution; //can't change it
		public string GetHostname()
		{
			return _hostname;
		}
		public void SetHostname(string hostname)
		{
			this._hostname=hostname;
		}
		public string GetDistribution()
		{
			return _distribution;
		}
		public string GetKernel() {
			return Outils.getoutput("uname -a");
		}
		public string GetUserShell() {
			return Mono.Unix.Native.Syscall.getusershell();
		}	
		public string GetLocale() {
			return Mono.Unix.Native.Syscall.getenv("LANG");
		}
		public void SetLocale(string locale) {
			_locale=locale;
		}
		public ConfSystem ()
		{
			this.SetHostname(Outils.ReadFile(cch_hostname).ToString().Replace("\n",""));
			this._distribution=Outils.ReadFile(cch_release).ToString().Replace("\n","");
		}
		public void Save()
		{
			try
			{
				//save hostname
				StreamWriter FileConf = new StreamWriter(cch_hostname);
				FileConf.WriteLine(this.GetHostname());
				FileConf.Close();
				//locale
				//search export LANG=
				string content = Outils.ReadFile(cch_locale);
				string[] lines = content.Split('\n');
				string[] linesResult = new string[lines.Length];
				string lineResult;
				int i =0;
	            foreach (string line in lines)
	            {
					lineResult=line;
					if (lineResult.IndexOf("export LANG")>=0)
					{
						lineResult="export LANG="+_locale;
					}
					linesResult[i]=lineResult;
					i++;
				}
				//now save locale
				FileConf = new StreamWriter(cch_locale);
				foreach (string line in linesResult)
	            {
					FileConf.WriteLine(line);
				}
				FileConf.Close();
				
			}
			catch(Exception exe)
			{
				Console.WriteLine("Can't save configuration");
				Console.WriteLine(exe.Message);
			}
		}
	}
}

