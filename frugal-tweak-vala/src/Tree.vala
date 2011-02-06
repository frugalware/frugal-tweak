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

namespace fwtweak {
	using Gtk;
	using Pacman;
	using pacman;

	public static class Tree {

		public static void setup_treeviewModule(TreeView view) {

		GtkObj.listmodel_modules = new ListStore (4, typeof (string), typeof (string),
				                  typeof (string), typeof (string));
		view.set_model (GtkObj.listmodel_modules);

		view.insert_column_with_attributes (-1, "Module", new CellRendererText (), "text", 0);
		view.insert_column_with_attributes (-1, "Description", new CellRendererText (), "text", 1);
		TreeIter iter;

		Configuration MyConf = new Configuration();
		//check modules available
		try {
			var directory = File.new_for_path (MyConf.PLUGINSDIR);
			var enumerator = directory.enumerate_children (FILE_ATTRIBUTE_STANDARD_NAME, 0);
			FileInfo file_info;
			while ((file_info = enumerator.next_file ()) != null) {
				//stdout.printf ("%s\n", file_info.get_name ());
				Module module = new Module( file_info.get_name ());
				GtkObj.listmodel_modules.append (out iter);
				GtkObj.listmodel_modules.set (iter, 0, module.GetTittle(), 1,module.GetDescription() , 2, module.GetCommand(),module.GetGroup(),3);
			}
		} 
		catch (Error e) {
			stderr.printf ("Error: %s\n", e.message);
		}

		}

	public static void setup_treeviewPacmanUpdate(TreeView view) {

		var listmodel = new ListStore (3, typeof (string), typeof (string),
				                  typeof (string));
		view.set_model (listmodel);

		view.insert_column_with_attributes (-1, "Package", new CellRendererText (), "text", 0);
		view.insert_column_with_attributes (-1, "Version", new CellRendererText (), "text", 1);
		view.insert_column_with_attributes (-1, "Description", new CellRendererText (), "text", 2);

		minipacman pacmang2 = new minipacman();
		PM_LIST *i = null;

			if (pacman_trans_init(Pacman.OptionTrans.TYPE_SYNC, 0, null, null, null) == -1) {
				Tools.ConsoleDebug("pacman_trans_init  failed \n");
				return ;
			}
		
			if (Pacman.pacman_trans_sysupgrade() == -1)
			{
				Tools.ConsoleDebug("pacman_trans_sysupgrade failed \n");
				return ;
			}
			minipacman.packages = pacman_trans_getinfo (OptionPM.PACKAGES);
			if (minipacman.packages == null) 
			{
				Tools.ConsoleDebug("No new updates are available\n");
			}
			else
			{
				Tools.ConsoleDebug("Updates are available\n");
				TreeIter iter;
			
				for (i=pacman_list_first(minipacman.packages);i!=null;i=pacman_list_next(i)) {
						PM_SYNCPKG *spkg = pacman_list_getdata (i);
						PM_PKG *pkg = pacman_sync_getinfo (spkg, OptionPMSYNC.PKG);
						Tools.ConsoleDebug((string)pacman_pkg_getinfo(pkg,OptionPMPKG.NAME)+"\n");
						listmodel.append (out iter);
						listmodel.set (iter, 0, (string)pacman_pkg_getinfo(pkg,OptionPMPKG.NAME), 1, (string)pacman_pkg_getinfo(pkg,OptionPMPKG.VERSION), 2, (string)pacman_pkg_getinfo(pkg,OptionPMPKG.DESC), 3);
					}
			
			}
			pacman_trans_release ();
		}

	}
}
