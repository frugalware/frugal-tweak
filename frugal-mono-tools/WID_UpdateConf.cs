// /*
//  *  Copyright (C) 2010 by Gaetan Gourdin <bouleetbil@frogdev.info>
//  *
//  *  This program is free software; you can redistribute it and/or modify
//  *  it under the terms of the GNU General Public License as published by
//  *  the Free Software Foundation; either version 2 of the License, or
//  *  (at your option) any later version.
//  *
//  *  This program is distributed in the hope that it will be useful,
//  *  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  *  GNU General Public License for more details.
//  *
//  *  You should have received a copy of the GNU General Public License
//  *  along with this program; if not, write to the Free Software
//  *  Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA 02111-1307, USA.
//  */
using System;
using System.Collections.Generic;
using System.IO;
using Gtk;

namespace frugalmonotools
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class WID_UpdateConf : Gtk.Bin
	{
		private const string cch_extPacnew = ".pacnew";
		ListStore listStore = new Gtk.ListStore (typeof (string));
		private Gtk.TreeIter iter;
		private string FileSelected="";
		
		public List<string>  FilesUpdated = new List<string>(); 
		public WID_UpdateConf ()
		{
			this.Build ();
			// Create a column for the package name
			Gtk.TreeViewColumn column = new Gtk.TreeViewColumn ();
			column.Title = "Configuration file updated";
			Gtk.CellRendererText nameCell = new Gtk.CellRendererText ();
			// Add the cell to the column
			column.PackStart (nameCell, true);
			TREE_UpdateConf.AppendColumn (column);
			column.AddAttribute (nameCell, "text", 0);
	
			// Event on treeview
			TREE_UpdateConf.Selection.Changed += OnSelectionEntryUpdate;
			TREE_UpdateConf.Model=listStore;
		}
		
		public void InitUpdateConf()
		{
			string dir=@"/etc/";
			string pattern ="*"+cch_extPacnew;
			System.IO.DirectoryInfo rootDir = new System.IO.DirectoryInfo(dir);
            FilesUpdated = Outils.WalkDirectoryTree(rootDir,pattern);
            foreach (string file in FilesUpdated) 
            {
				listStore.AppendValues(file);
			}
		}
		protected void OnSelectionEntryUpdate(object o, EventArgs args)
	    {
	   		try
			{
			 	TreeModel model;
				 if (((TreeSelection)o).GetSelected(out model, out iter))
		        {
		            string T =(string)model.GetValue (iter, 0);
					FileSelected=T;
				}
			}
			catch{}
		}
		
		
	}
}

