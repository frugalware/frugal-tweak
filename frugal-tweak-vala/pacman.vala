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
	
	private static const string CFG_FILE				="/etc/pacman-g2.conf";
	private static const string FW_CURRENT			="frugalware-current";
	private static const string FW_STABLE				="frugalware";
	private static const string FW_LOCAL				="local";
	private static unowned Pacman.PM_DB sync_db	= null;
	private static string[] repos						 = new string[0];
	
	public pacman()
	{
		Pacman.pacman_release();
		if (Pacman.pacman_initialize(Pacman.PM_ROOT) != -1)
		{
			Tools.ConsoleDebug("Initialize pacman-g2\n");
			InitDatabase();
		}
	}


	private void InitDatabase()
	{
		Tools.ConsoleDebug("Parse config pacman-g2\n");
		
		Pacman.pacman_cb_db_register callback = _db_callback;
		Pacman.pacman_parse_config(CFG_FILE, callback,"");		
		Pacman.pacman_db_register(FW_LOCAL);
		 /* set some important pacman-g2 options */
		//Pacman.pacman_set_option (Pacman.Option.LOGCB,-1);
		//Pacman.pacman_set_option (Pacman.Option.LOGMASK,-1);
		//Pacman.set_option(Pacman.Option.USESYSLOG,-1);
	}
	private static void _db_callback (string section, PM_DB db)
	{
		Tools.ConsoleDebug("Find repo "+section+"\n");
		repos += section;
		return;
	}

	public void UpdateAllDatabase()
	{
		Tools.ConsoleDebug("Update all repo  \n");
		int i =0;
		while(i <repos.length)
		{
			this.UpdateDatabase(repos[i]);
			i++;
		}
	}

	public void UpdateDatabase(string section)
	{
		Tools.ConsoleDebug("Update repo "+section+" \n");
		int	retval = 0;
		sync_db = Pacman.pacman_db_register(section);
		retval = Pacman.pacman_db_update(0,sync_db);
		if (retval==-1)
		{
			Tools.ConsoleDebug("Update repo "+section+" failed \n");
			return;
		}
		if (pacman_trans_init(Pacman.OptionTrans.TYPE_SYNC, 0, null, null, null) == -1) {
			Tools.ConsoleDebug("pacman_trans_init "+section+" failed \n");
			return ;
		}
		if (Pacman.pacman_trans_sysupgrade() == -1)
		{
			Tools.ConsoleDebug("pacman_trans_sysupgrade "+section+" failed \n");
			return ;
		}
		/*packages = pacman_trans_getinfo (PM_TRANS_PACKAGES);
		if (packages == null) 
		{
				#if DEBUG==1
					 ("No new updates are available\n")
				#endif
		}	*/	
		
		pacman_trans_release ();
		
	}
	
	 
}
