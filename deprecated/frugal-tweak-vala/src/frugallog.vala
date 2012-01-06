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
using fwtweak;

static int main (string[] args) {     
    Gtk.init (ref args);
 
    try {
	Configuration conf = new Configuration();
        var builder = new Builder ();
        builder.add_from_file ("/usr/share/frugalware-tweak/UI//MainUI.ui");
        builder.connect_signals (null);
        var window = builder.get_object ("windowLog") as Window;
	Gtk.TextView xorg = builder.get_object("text_xorg") as Gtk.TextView;
	string display = Tools.ReadLine("echo $DISPLAY");
	string []displays=display.split(":");
	display =displays[1];
	displays=display.split(".");
	display =displays[0];
	Tools.ConsoleDebug(display);
	xorg.buffer.text=Tools.open_file("/var/log/Xorg."+display+".log");

	Gtk.TextView pacman = builder.get_object("text_pacman") as Gtk.TextView;
	string text = Tools.open_file("/var/log/pacman-g2.log2");
	pacman.buffer.text=text;

	Gtk.TextView xsession = builder.get_object("text_xsession") as Gtk.TextView;
	xsession.buffer.text=Tools.open_file(conf.HOMEDIR+"/.xsession-errors");
	

        window.show_all ();
        window.destroy.connect (Gtk.main_quit);
        Gtk.main ();
    } catch (Error e) {
        stderr.printf ("Could not load UI: %s\n", e.message);
        return 1;
    } 
 
    return 0;
}
