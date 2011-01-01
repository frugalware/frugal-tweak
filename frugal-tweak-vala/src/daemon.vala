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
using DbusServer;
using pacman;

class Deamon : GLib.Object {
	
	public static pacman pacmang2 ;

	public static int main(string[] args) {
	Tools.ConsoleDebug("Start Frugalware Tweak Daemon\n");
	//dbus
	Bus.own_name (BusType.SESSION, "org.frugal.tweak", BusNameOwnerFlags.NONE,
		          on_bus_aquired,
		          () => {},
		          () => stderr.printf ("Could not aquire name\n"));
	
	pacmang2 = new pacman();
	UpdateAllDatabase();
	while(true)
	{
		#if DEBUG==1
 			Thread.usleep(120000); // 2minutes for tested
		#else
			Thread.usleep(1800000000);	//1/2 hour
			Thread.usleep(1800000000); //1/2 hour
		#endif
		UpdateAllDatabase();
		
	}
    }

	public static void UpdateAllDatabase()
	{
		Tools.ConsoleDebug("Updated database pacman-g2\n");			
		pacmang2.UpdateAllDatabase();
		
	}
}
