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
using Tree;

int main (string[] args) {
	Gtk.init (ref args);
	 var builder = new Builder ();
        builder.add_from_file ("/usr/share/frugalware-tweak/UI/MainUI.ui");
        EventGtk event = new EventGtk();
	builder.connect_signals (event);
        var window = builder.get_object ("windowupd") as Window;
	Gtk.TreeView pacman = builder.get_object("treeview_upd") as Gtk.TreeView;
	window.position = WindowPosition.CENTER;
	window.set_default_size (800, 200);
	window.destroy.connect (Gtk.main_quit);
        Tree.setup_treeviewPacmanUpdate (pacman);
	//window.add(view);

	window.show_all ();

    Gtk.main ();
    return 0;
}
