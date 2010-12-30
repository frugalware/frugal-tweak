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
			#if DEBUG==1
				stdout.printf("Initialize pacman-g2\n");		
			#endif
			InitDatabase();
		}
	}


	private void InitDatabase()
	{
		#if DEBUG==1
			stdout.printf("Parse config pacman-g2\n");		
		#endif
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
		#if DEBUG==1
					stdout.printf("Find repo "+section+"\n");		
		#endif
		repos += section;
		return;
	}

	public void UpdateAllDatabase()
	{
		stdout.printf("Update all repo  \n");
		int i =0;
		while(i <repos.length)
		{
			//stdout.printf(repos[i]+" \n");
			this.UpdateDatabase(repos[i]);
			i++;
		}
	}

	public void UpdateDatabase(string section)
	{
		#if DEBUG==1
			stdout.printf("Update repo "+section+" \n");
		#endif
		int	retval = 0;
		sync_db = Pacman.pacman_db_register(section);
		retval = Pacman.pacman_db_update(0,sync_db);
		if (retval==-1)
		{
			stdout.printf("Update repo "+section+" failed \n");
		}
		if (pacman_trans_init(Pacman.OptionTrans.TYPE_SYNC, 0, null, null, null) == -1) {
			stdout.printf("pacman_trans_init "+section+" failed \n");
			return ;
		}
		if (Pacman.pacman_trans_sysupgrade() == -1)
		{
			stdout.printf("pacman_trans_sysupgrade "+section+" failed \n");
			return ;
		}
		pacman_trans_release ();
		
	}
	
	 
}
