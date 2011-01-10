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

using GLib;
using Tools;
using Pacman;

public class pacman
{
	
	private static const string CFG_FILE			="/etc/pacman-g2.conf";
	private static const string FW_CURRENT			="frugalware-current";
	private static const string FW_STABLE			="frugalware";
	private static const string FW_LOCAL			="local";
	private static unowned Pacman.PM_DB sync_db	= null;
	private static unowned Pacman.PM_DB local_db	= null;
	public static Pacman.PM_LIST *packages			= null;
	private static string[] _repos					= new string[0];
	
	public pacman()
	{
		Pacman.pacman_release();
		if (Pacman.pacman_initialize(Pacman.PM_ROOT) != -1)
		{
			Tools.ConsoleDebug("Initialize pacman-g2");
			InitDatabase();
		}
	}
	public string [] repos()
	{
		return _repos;
	}
	private void InitDatabase()
	{
		Tools.ConsoleDebug("Parse config pacman-g2");
		
		Pacman.pacman_cb_db_register callback = _db_callback;
		Pacman.pacman_parse_config(CFG_FILE, callback,"");		
		local_db = Pacman.pacman_db_register(FW_LOCAL);
		if(local_db==null)
				Tools.ConsoleDebug("Couldn't register local database");
		_repos += FW_LOCAL;
		 /* set some important pacman-g2 options */
		long _logParam = -1;
		Pacman.pacman_set_option (Pacman.Option.LOGCB,_logParam);
		//Pacman.pacman_set_option (Pacman.Option.LOGMASK,_logParam);
		//Pacman.set_option(Pacman.Option.USESYSLOG,-1);
	}
	public unowned Pacman.PM_DB RegisterRepo(string str_repo)
	{
		if(str_repo==FW_LOCAL)
			return local_db;
		else
			return Pacman.pacman_db_register(str_repo);
	}
	public bool search(string str_search,string str_repo, out unowned Pacman.PM_LIST lst_packages,out unowned Pacman.PM_DB db_search )
	{
		
		db_search = RegisterRepo(str_repo);
		if (db_search == null)
		{
			Tools.ConsoleDebug("Couldn't register '"+ str_repo+"'");
			return false;
		}
		Pacman.pacman_set_option (Pacman.Option.NEEDLES,(long) str_search);
		packages = Pacman.pacman_db_search(db_search);
		lst_packages = packages;
		Tools.ConsoleDebug("search finish :"+str_search+" into "+str_repo);
		return true;
	}
	private static void _db_callback (string section, PM_DB db)
	{
		Tools.ConsoleDebug("Find repo "+section);
		_repos += section;
		return;
	}
	public void UpdateAllDatabase()
	{
		Tools.ConsoleDebug("Update all repo");
		int i =0;
		while(i <_repos.length)
		{
			this.UpdateDatabase(_repos[i]);
			i++;
		}
		this.CheckUpdate();
	}
	public void UpdateDatabase(string section)
	{
		Tools.ConsoleDebug("Update repo "+section);
		sync_db = Pacman.pacman_db_register(section);
		int	retval = 0;
		retval = Pacman.pacman_db_update(0,sync_db);
		if (retval==-1)
		{
			Tools.ConsoleDebug("Update repo "+section+" failed");
			return;
		}
		
	}
	public bool CheckUpdate( )
	{
		bool pkgUpdated = false;
		PM_LIST *i = null;

		if (pacman_trans_init(Pacman.OptionTrans.TYPE_SYNC, 0, null, null, null) == -1) {
			Tools.ConsoleDebug("pacman_trans_init  failed");
			try{
				pacman_trans_release ();
				File file = File.new_for_path("/tmp/pacman-g2.lck");
				if (file.query_exists())
					file.delete();
			}
			catch{}
			return false;
		}
		
		if (Pacman.pacman_trans_sysupgrade() == -1)
		{
			Tools.ConsoleDebug("pacman_trans_sysupgrade failed ");
			return false;
		}
		packages = pacman_trans_getinfo (OptionPM.PACKAGES);
		if (packages == null) 
		{
			Tools.ConsoleDebug("No new updates are available");
		}
		else
		{
			Tools.ConsoleDebug("Updates are available");
			pkgUpdated=true;
			for (i=pacman_list_first(packages);i!=null;i=pacman_list_next(i)) {
					PM_SYNCPKG *spkg = pacman_list_getdata (i);
					PM_PKG *pkg = pacman_sync_getinfo (spkg, OptionPMSYNC.PKG);
					Tools.ConsoleDebug((string)pacman_pkg_getinfo(pkg,OptionPMPKG.NAME));
				}
			
		}
		pacman_trans_release ();
		return pkgUpdated;
	}
}
