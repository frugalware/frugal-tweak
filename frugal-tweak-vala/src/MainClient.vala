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
			informUpdate();
	}
}

void on_bus_aquired (DBusConnection conn) {
    try {
        conn.register_object ("/org/frugalware/tweak", new DbusUpd ());
	Tools.ConsoleDebug("register dbus application");
    } catch (IOError e) {
        Tools.ConsoleDebug("Could not register service");
    }
}

void* func()
{
	while (true)
	{
		if (MyConf.GetCheckUpd())
		{
			pacman pacmang2 = new pacman();
			if(pacmang2.CheckUpdate())
			{
				informUpdate();
			}
		}
		Thread.usleep(1800000000);	//1/2 hour
		//roadmap.GetDateRelease();
	}
	return null;
}


void informUpdate()
{
	Popup.PopupShow("Frugalware tweak","Some update are available.");
	systrayIcon.SetTooltip("Some update are available.");
	systrayIcon.SetIco("/usr/share/frugalware-tweak/pictures/frugalware-tweak-update.png");
}

//declarations
ConfSystem confsystem;
Systray systrayIcon;
Configuration MyConf ;

bool onClose(Gdk.Event e)
{
	GtkObj.MainWindow.hide();
	return true;
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
	Bus.own_name (BusType.SESSION, "org.frugalware.tweak", BusNameOwnerFlags.NONE,
                  on_bus_aquired,
                  () => {},
                  () => Tools.ConsoleDebug("Could not aquire name"));
		
	confsystem = new ConfSystem();
	try {
		var builder = new Builder ();
		builder.add_from_file ("/usr/share/frugalware-tweak/UI//MainUI.ui");
		EventGtk event = new EventGtk();
		builder.connect_signals (event);
		
		GtkObj.MainWindow = builder.get_object ("window") as Window;
		GtkObj.MainWindow.delete_event += onClose;

		//added some var to window
		GtkObj.host = builder.get_object("entry_host") as Gtk.Entry;
		GtkObj.host.text=confsystem.GetHostname();

		Gtk.Entry distri = builder.get_object("entry_distri") as Gtk.Entry;
		distri.text=confsystem.GetDitribution();
		distri.editable=false;
		distri.sensitive=false;
		MyConf = new Configuration();
		Gtk.Entry kernel = builder.get_object("entry_kernel") as Gtk.Entry;
		kernel.editable=false;
		kernel.sensitive=false;
		kernel.text=confsystem.GetKernel();
		Gtk.Entry shell = builder.get_object("entry_shell") as Gtk.Entry;
		shell.text=confsystem.GetShell();
		shell.editable=false;
		shell.sensitive=false;

		GtkObj.notif = builder.get_object("chk_notif") as Gtk.CheckButton;
		GtkObj.notif.active=MyConf.GetShowNotif();

		GtkObj.update = builder.get_object("chk_update") as Gtk.CheckButton;
		GtkObj.update.active=MyConf.GetCheckUpd();

		GtkObj.modules = builder.get_object("treeview_modules") as Gtk.TreeView;
		setup_treeviewModule(GtkObj.modules);

		Gtk.TextView about = builder.get_object("textview_about") as Gtk.TextView;
		about.buffer.text=Tools.open_file("/usr/share/frugalware-tweak/LICENCE");

	} catch (Error e) {
		stderr.printf ("Could not load UI: %s\n", e.message);
		return 1;
	} 
	
	/* Create tray icon */
	systrayIcon = new Systray();

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

