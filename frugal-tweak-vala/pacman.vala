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

using Pacman;

public class pacman
{
	private const string CFG_FILE="/etc/pacman-g2.conf";
	private delegate void* _db_callback_delegate(string db, Pacman.Database d);	

	public pacman()
	{
		Pacman.release();
		if (Pacman.initialize(Pacman.ROOT) != -1)
		{
			#if DEBUG==1
				stdout.printf("Initialize pacman-g2\n");		
			#endif
			InitDatabase();
		}
	}


	private void InitDatabase()
	{
		Pacman.cb_db_register callback = _db_callback;
		Pacman.parse_config(CFG_FILE, callback,"");
		#if DEBUG==1
			stdout.printf("Parse config pacman-g2\n");		
		#endif
	}
	private static void* _db_callback (string db, Pacman.Database d)
	{
		#if DEBUG==1
					stdout.printf("Find repo "+db+"\n");		
		#endif
		return null;	
	}
	public void UpdateDatabase()
	{
		//Pacman.db
	}
	
	 
}
