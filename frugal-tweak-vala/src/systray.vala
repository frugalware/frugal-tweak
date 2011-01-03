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

using Gtk;
public static class systray
{
	static StatusIcon trayicon ;

	public static void init()
	{
		 /* Create tray icon */
		trayicon = new StatusIcon.from_file("/usr/share/frugalware-tweak/pictures/frugalware-tweak.png");
		trayicon.set_tooltip_text ("Frugalware Tweak !");
		show();
	}
	public static void hide()
	{
		trayicon.set_visible(false);
	}
	public static void show()
	{
		trayicon.set_visible(true);
	}

}
