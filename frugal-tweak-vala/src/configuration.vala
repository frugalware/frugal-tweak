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


public class Configuration  {

	//Fixes home dir
	public string  HOMEDIR=Environment.get_home_dir ();
	private string  _CACHEDIR="/.cache/frugalware-tweak2";
	public string PLUGINSDIR="/usr/share/frugalware-tweak/plugins/";
	public string GLADEFILE="/usr/share/frugalware-tweak/UI/MainUI.ui";
	public string Version="0.1";
	private string _glibSechema="org.frugalware.frugaltweak";
	
	public string GetCacheDir()
	{
		Posix.mkdir(HOMEDIR+_CACHEDIR,0777);
		return HOMEDIR+_CACHEDIR;
	}
	public bool GetShowNotif()
	{
		var settings = new Settings (_glibSechema);
		return settings.get_boolean ("shownotif");
	}
	public void SetShowNotif(bool enable)
	{
		var settings = new Settings (_glibSechema);
		settings.set_boolean ("shownotif", enable);
	}
	public bool GetCheckUpd()
	{
		var settings = new Settings (_glibSechema);
		return settings.get_boolean ("checkupdate");
	}
	public void SetCheckUpd(bool enable)
	{
		var settings = new Settings (_glibSechema);
		settings.set_boolean ("checkupdate", enable);
	}
}
