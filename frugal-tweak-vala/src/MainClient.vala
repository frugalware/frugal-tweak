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
using Unique;
using Tree;

int main (string[] args) {
	Unique.App app;
	Gtk.init (ref args);
	app = new Unique.App("org.fwtweak.unique", null);
		
	if(app.is_running) { //not starting if already running
		Unique.Command command;
		Unique.Response response;
		Unique.MessageData message;
		message = new MessageData ();
		command = (Unique.Command) Unique.Command.ACTIVATE;
		response = app.send_message (command, message);
	
		if(response == Unique.Response.OK)
			return 0;
		else
			return 1;
	}
	var window = new Window ();
	window.title = "Frugalware Tweak";
	window.set_default_size (400, 300);
	window.position = WindowPosition.CENTER;
	window.destroy.connect (Gtk.main_quit);

/*
	var button = new Button.with_label ("Click me!");
	button.clicked.connect (() => {
	button.label = "Thank you";
	});

	window.add (button);
*/
	//added treeview for modules
	var view = new TreeView ();
        Tree.setup_treeviewModule (view);
	window.add(view);

	window.show_all ();

    Gtk.main ();
    return 0;
}
