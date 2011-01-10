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
	Gtk.init (ref args);

	var builder = new Builder ();
        builder.add_from_file ("/usr/share/frugalware-tweak/UI/MainUI.ui");
        EventGtk event = new EventGtk();
	builder.connect_signals (event);
        var window = builder.get_object ("window_pacman") as Window;
	window.destroy.connect (Gtk.main_quit);

	static_obj.my_pacman = new pacman();
	ListStore    model;
	CellRenderer cell;
	string [] repos = static_obj.my_pacman.repos();
	GtkObj.combobox_repo = builder.get_object("combobox_repo") as Gtk.ComboBox;
	model = new ListStore( 2, typeof( string ), typeof( int ) );
	int i = 0;
	while(i<=repos.length)
	{
		TreeIter iter;
		model.append( out iter );
		model.set( iter, 0, repos[i], 1, i );
		i++;
	}
	cell = new CellRendererText();
        GtkObj.combobox_repo.pack_start( cell, false );
        GtkObj.combobox_repo.set_attributes( cell, "text", 0 );
	GtkObj.combobox_repo.model=model;

	window.show_all ();

    Gtk.main ();
    return 0;
}
