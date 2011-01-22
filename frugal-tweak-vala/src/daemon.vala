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

[DBus (name = "org.frugalware.tweak")]
interface DbusUpd : Object {
	public abstract void update_available(bool update) throws IOError;
}

class Deamon : GLib.Object {
	
	public static pacman pacmang2 ;

	public static int main(string[] args) {
	Tools.ConsoleDebug("Start Frugalware Tweak Daemon");
	//dbus
	DbusUpd dbusUpd = Bus.get_proxy_sync (BusType.SYSTEM, "org.frugalware.tweak", "/org/frugalware/tweak");
	pacmang2 = new pacman();

	while(true)
	{
		Thread.usleep(1800000000);	//1/2 hour
		Thread.usleep(1800000000);	//1/2 hour
		Thread.usleep(1800000000);	//1/2 hour
		if (UpdateAllDatabase())
		{
			Tools.ConsoleDebug("Send dbus event");
			try{
				dbusUpd.update_available(true);
			}
			catch {
        			Tools.ConsoleDebug("couldn't send dbus event");
    			}
		}
	}
    }

	public static bool UpdateAllDatabase()
	{
		Tools.ConsoleDebug("Updated database pacman-g2");			
		pacmang2.UpdateAllDatabase();
		return pacmang2.CheckUpdate();
	}
}

