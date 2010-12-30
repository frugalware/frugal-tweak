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

int main (string[] args) {
	
	//GTK
	Gtk.init (ref args);

	try {
		var builder = new Builder ();
		builder.add_from_file ("DATA/MainGUI.ui");
		builder.connect_signals (null);
		var window = builder.get_object ("MainWindow") as Window;
		window.show_all ();
		Gtk.main ();
		} catch (Error e) {
		stderr.printf ("Could not load UI: %s\n", e.message);
		return 1;
	}	 

	return 0;
}
