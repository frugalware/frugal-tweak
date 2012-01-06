/*
 *
 * (C) 2010 bouleetbil <bouleetbil@frogdev.info>
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301  USA
 */
namespace fwtweak {
	public class ConfSystem
	{
		private const string hostFile="/etc/HOSTNAME";
		private const string distriFile="/etc/frugalware-release";

		public string GetShell()
		{
			string result = "";
			result = Posix.getusershell();
			return result;
		}
		public string GetDitribution()
		{
			string distri = Tools. open_file(distriFile);
			distri = distri.replace("\n","");
			return distri;
		}
		public string GetHostname()
		{
			string host = Tools. open_file(hostFile);
			host = host.replace("\n","");
			return host;
		}
		public void SetHostname(string host)
		{
			Tools.write_file(hostFile,host);
		}
		public string GetKernel()
		{
			string kernel = Tools.ReadLine("uname -a");
			return kernel.replace("\n","");
		}
	}
}
