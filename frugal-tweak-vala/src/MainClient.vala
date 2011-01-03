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
using Popup;
using Tree;
using Module;
using pacman;

[DBus (name = "org.frugalware.tweak")]
class DbusUpd : GLib.Object {

	public void update_available (bool upd) {
		if(upd)
			Popup.PopupShow("frugalware","Updates package available.");		
	}
}

void on_bus_aquired (DBusConnection conn) {
    try {
        conn.register_object ("/org/frugalware/tweak", new DbusUpd ());
	Tools.ConsoleDebug("register dbus application");
    } catch (IOError e) {
        Tools.ConsoleDebug("Could not register service);
    }
}

void* func()
{
	pacman pacmang2 = new pacman();
	if(pacmang2.CheckUpdate())
	{
		Popup.PopupShow("frugalware","Updates package available.");
	}
	return null;
}
int main (string[] args) {

	if(!Thread.supported())
	{
		stdout.printf("Thread is not supported\n");
		return 1;
	}
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
	//register Dbus
	Bus.own_name (BusType.SYSTEM, "org.frugalware.tweak", BusNameOwnerFlags.NONE,
                  on_bus_aquired,
                  () => {},
                  () => Tools.ConsoleDebug("Could not aquire name));
		
	 /* Create tray icon */
        StatusIcon trayicon = new StatusIcon.from_file("/usr/share/frugalware-tweak/pictures/frugalware-tweak.png");
        trayicon.set_tooltip_text ("Frugalware Tweak !");
        trayicon.set_visible(true);
	//TODO
	//trayicon.activate += icon_clicked;
        //create_menu()

	var window = new Window ();
	window.title = "Frugalware Tweak";
	window.set_default_size (400, 300);
	window.position = WindowPosition.CENTER;
	window.destroy.connect (Gtk.main_quit);

	//added treeview for modules
	var view = new TreeView ();
        Tree.setup_treeviewModule (view);
	window.add(view);

	#if DEBUG
		//for tested notification
		Popup.PopupShow("titre test","test");
		//Tools.ConsoleDebug("test1\n");
		Module module = new Module("01.system.xml");
		Tools.ConsoleDebug("test module : "+module.GetTittle()+"\n");
	#endif

	window.show_all ();
	//start thread
	try
	{
		Thread.create(func,false);
	}
	catch
	{
		Tools.ConsoleDebug("Couldn't start thread\n");
	}
	Gtk.main ();
	return 0;
}

