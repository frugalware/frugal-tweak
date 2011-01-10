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
using Pacman;

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
		//search into pacman-g2 database
		unowned Pacman.PM_LIST lst_packages = null;
		unowned Pacman.PM_DB db_search = null;
		PM_LIST		*i = null ;
		PM_PKG		*pm_spkg;
		TreeIter iter;
		GtkObj.listmodel_pkg = new ListStore (3, typeof (string), typeof (string),
				          typeof (string));

		GtkObj.tree_pkg.set_model (GtkObj.listmodel_pkg);

		GtkObj.tree_pkg.insert_column_with_attributes (-1, "Package", new CellRendererText (), "text", 0);
		GtkObj.tree_pkg.insert_column_with_attributes (-1, "Version", new CellRendererText (), "text", 1);
		GtkObj.tree_pkg.insert_column_with_attributes (-1, "Description", new CellRendererText (), "text", 2);

		if (static_obj.my_pacman.search(str_search,str_repo,out lst_packages, out db_search))
		{
			for (i=pacman_list_first(lst_packages);i!=null;i=pacman_list_next(i)) {
					pm_spkg = pacman_db_readpkg (db_search, pacman_list_getdata(i));
					GtkObj.listmodel_pkg.append (out iter);
					GtkObj.listmodel_pkg.set (iter, 0, (string)pacman_pkg_getinfo(pm_spkg,OptionPMPKG.NAME), 1, (string)pacman_pkg_getinfo(pm_spkg,OptionPMPKG.VERSION), 2, (string)pacman_pkg_getinfo(pm_spkg,OptionPMPKG.DESC), 3);
					Tools.ConsoleDebug((string)pacman_pkg_getinfo(pm_spkg,OptionPMPKG.NAME));
				}	
		}
	}

	[CCode (cname = "G_MODULE_EXPORT EventGtk_on_button_install_clicked",instance_pos = -1)]
	public void on_BTN_install_clicked (Button source) {
		TreeIter iter;
		TreeModel model;
		string str = "";

		TreeSelection sel = GtkObj.tree_pkg.get_selection();
		if (sel.count_selected_rows() == 1) {
			sel.get_selected( out model, out iter);
			GtkObj.listmodel_pkg.get(iter, 0, out str);
		}
		if(str=="") return;
		Tools.run_command("fwroot" ,"frugalware-tweak-terminal -e pacman-g2 -S "+str,false);
	}
	[CCode (cname = "G_MODULE_EXPORT EventGtk_on_button_uninstall_clicked",instance_pos = -1)]
	public void on_BTN_uninstall_clicked (Button source) {
		TreeIter iter;
		TreeModel model;
		string str = "";
		//can only uninstall package installed
		if( GtkObj.combobox_repo.get_active_text()!=pacman.FW_LOCAL) return ;

		TreeSelection sel = GtkObj.tree_pkg.get_selection();
		if (sel.count_selected_rows() == 1) {
			sel.get_selected( out model, out iter);
			GtkObj.listmodel_pkg.get(iter, 0, out str);
		}
		if(str=="") return;
		Tools.run_command("fwroot" ,"frugalware-tweak-terminal -e pacman-g2 -Rc "+str,false);
	}

}
