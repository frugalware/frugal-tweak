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
using Gtk;
namespace frugalmonotools
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class WID_Users : Gtk.Bin
	{
		private Gtk.TreeIter iter;
		ListStore ListStoreUser = new Gtk.ListStore (typeof (string));
		public WID_Users ()
		{
			this.Build ();
		}
		public void InitUsers()
		{
			// Create a column for the package name
			Gtk.TreeViewColumn ColumnUser = new Gtk.TreeViewColumn ();
			ColumnUser.Title = "Users";
			Gtk.CellRendererText NameCellUser = new Gtk.CellRendererText ();
			// Add the cell to the column
			ColumnUser.PackStart (NameCellUser, true);
			TREE_Users.AppendColumn (ColumnUser);
			ColumnUser.AddAttribute (NameCellUser, "text", 0);
			
			ListStoreUser.Clear();
			int i = 0;
			foreach (User user in Groups.GetAllUsers())
			{
				// Add some data to the store
				if (i==0)	
					iter=ListStoreUser.AppendValues (user.Name);
				else
					ListStoreUser.AppendValues (user.Name);
				i++;
			}
			TREE_Users.Model=ListStoreUser;
			
		}
	}
}

