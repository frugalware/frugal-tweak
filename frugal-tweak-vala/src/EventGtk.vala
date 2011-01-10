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

public class EventGtk{
	
	//frugalware-tweak2
	[CCode (cname = "G_MODULE_EXPORT EventGtk_on_BTN_Save_clicked",instance_pos = -1)]
	public void on_BTN_Save_clicked (Button source) {
		Configuration Myconf = new Configuration();
		Myconf.SetShowNotif(GtkObj.notif.active);
		Myconf.SetCheckUpd(GtkObj.update.active);
	}
	[CCode (cname = "G_MODULE_EXPORT EventGtk_on_BTN_SaveHost",instance_pos = -1)]
	public void on_BTN_SaveHost_clicked (Button source) {
		Tools.run_command("fwroot frugalware-tweak-hostname",GtkObj.host.text,false);
	}
	[CCode (cname = "G_MODULE_EXPORT EventGtk_on_BTN_StartModule_clicked",instance_pos = -1)]
	public void on_BTN_Module_start_clicked (Button source) {
		TreeIter iter;
		TreeModel model;
		string str = "";

		TreeSelection sel = GtkObj.modules.get_selection();
		if (sel.count_selected_rows() == 1) {
			sel.get_selected( out model, out iter);
			GtkObj.listmodel_modules.get(iter, 2, out str);
		}
		Tools.run_command(str,"",false);
	}
	//pacman-g2 update
	[CCode (cname = "G_MODULE_EXPORT EventGtk_On_Update",instance_pos = -1)]
	public void on_BTN_Update_clicked (Button source) {
		Tools.run_command("fwroot frugalware-tweak-terminal ","-e pacman-g2 -Syu",false);
	}
	//mini pacman-g2
	[CCode (cname = "G_MODULE_EXPORT EventGtk_On_PacmanG2Search",instance_pos = -1)]
	public void on_BTN_search_pkg_clicked (Button source) {
		string str_repo = GtkObj.combobox_repo.get_active_text();
		string str_search= GtkObj.entry_search_pkg.text;
		Tools.ConsoleDebug("search package "+str_search+" into "+str_repo+"...");
		if(str_repo== null)
		{
			var msg = new Gtk.MessageDialog(null,Gtk.DialogFlags.MODAL,Gtk.MessageType.INFO,
										Gtk.ButtonsType.OK,"You should select a repo.");
			msg.run();
			msg.destroy();
			return ;
		}
	}

}
