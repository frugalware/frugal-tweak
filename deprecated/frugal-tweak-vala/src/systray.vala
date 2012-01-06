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
using GLib;
namespace fwtweak {
	public class Systray
	{
		public static const string defaultIco = "/usr/share/frugalware-tweak/pictures/frugalware-tweak.png";
		private StatusIcon _trayicon ;
		private Menu _popup;
		private Window _window;
		private string _ico ="";
		private Configuration conf = new Configuration();
		public Systray()
		{
			_window = GtkObj.MainWindow;
			_ico=defaultIco;
			 /* Create tray icon */
			try
			{
				_trayicon =new StatusIcon.from_file(_ico);
				_window.set_icon_from_file(_ico);
			}
			catch(GLib.Error err)
			{
				var msg = new Gtk.MessageDialog(null,Gtk.DialogFlags.MODAL,Gtk.MessageType.ERROR,
											Gtk.ButtonsType.OK,"Failed to load "+_ico+"\n"+err.message);
				msg.run();
				msg.destroy();
			}
			_trayicon.set_tooltip_text ("Frugalware Tweak2 !");
			create_menu();
			this.show();
		}
		public void SetTooltip(string text)
		{
			_trayicon.set_tooltip_text (text);
		}
		private void create_menu ()
		{
			_popup = new Menu();
			var menuCheckUpdate = new ImageMenuItem.from_stock(STOCK_INFO, null);
			menuCheckUpdate.label="Check update";
			menuCheckUpdate.activate += update_clicked ;
			_popup.append(menuCheckUpdate);

			var menuTerm = new ImageMenuItem.from_stock(STOCK_EDIT, null);
			menuTerm.label="Terminal";
			menuTerm.activate += terminal_clicked ;
			_popup.append(menuTerm);

			var menuForums = new ImageMenuItem.from_stock(STOCK_NETWORK, null);
			menuForums.label="Forums";
			menuForums.activate += forums_clicked ;
			_popup.append(menuForums);

			var menuWiki = new ImageMenuItem.from_stock(STOCK_NETWORK, null);
			menuWiki.label="Wiki";
			menuWiki.activate += wiki_clicked ;
			_popup.append(menuWiki);

			var menuItem = new ImageMenuItem.from_stock(STOCK_ABOUT, null);
			menuItem.activate += about_clicked ;
			_popup.append(menuItem);

			var menuItem2 = new ImageMenuItem.from_stock(STOCK_QUIT, null);
			menuItem2.activate += exit_app ;
			_popup.append(menuItem2);

			_popup.show_all();
			_trayicon.popup_menu += popup_Menu ;
			_trayicon.activate +=  icon_clicked ;
		
		}
		public delegate void Change_Ico(string ico);
		public void SetIco(string ico)
		{
			try
			{
				_ico=ico;
				Gdk.threads_enter();
				_trayicon.set_from_file(_ico);
				_window.set_icon_from_file(_ico);
				Gdk.threads_leave();
			}
			catch(GLib.Error err)
			{
				var msg = new Gtk.MessageDialog(null,Gtk.DialogFlags.MODAL,Gtk.MessageType.ERROR,
											Gtk.ButtonsType.OK,"Failed to load "+_ico+"\n"+err.message);
				msg.run();
			}
		}
		private void popup_Menu(StatusIcon i , uint button,uint activateTime)
		{
			_popup.popup(null,null,i.position_menu,0 ,activateTime);
		}
		private void  icon_clicked ()
		{
			Tools.ConsoleDebug("clic TrayIcon");
			if (_window.is_active)
			{
				_window.hide();
			}
			else
			{
				_window.show_all();
			}	
		}
		private void  terminal_clicked ()
		{
			Tools.run_command("frugalware-tweak-terminal","",false);
		}
		private void  update_clicked ()
		{
			Tools.run_command("frugalware-tweak-pacman-update","",false);
		}
		private void  forums_clicked ()
		{
			Tools.run_command("frugalware-tweak-browser","http://forums.frugalware.org",false);
		}
		private void  wiki_clicked ()
		{
			Tools.run_command("frugalware-tweak-browser","http://wiki.frugalware.org",false);
		}
		private void  about_clicked ()
		{
			var about = new AboutDialog ();
			about.set_version(conf.Version);
			about.set_program_name("Frugalware Tweak2 : ");
			about.set_comments("This is a Simple tweak frugalware application");
			about.set_copyright("gaetan gourdin alias bouleetbil");
			about.run();
			about.hide();
		}
		private void  exit_app ()
		{
			Gtk.main_quit();
		}
		public void hide()
		{
			_trayicon.set_visible(false);
		}
		public void show()
		{
			_trayicon.set_visible(true);
		}

	}
}
