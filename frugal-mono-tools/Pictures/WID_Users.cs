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
		private const int columnSelected = 0;

		private Gtk.TreeIter iter;
		string GroupSelect="";
		ListStore ListStoreUser = new Gtk.ListStore (typeof (string));
		ListStore ListStoreUserGroup = new Gtk.ListStore (typeof (bool),typeof (string));
		ListStore ListStoreGroup = new Gtk.ListStore (typeof (string));
		
		public WID_Users ()
		{
			this.Build ();
		}
		public void InitUsers()
		{
			#region treeview users
			// Create a column for the name
			Gtk.TreeViewColumn ColumnUser = new Gtk.TreeViewColumn ();
			ColumnUser.Title = "Users";
			Gtk.CellRendererText NameCellUser = new Gtk.CellRendererText ();
			// Add the cell to the column
			ColumnUser.PackStart (NameCellUser, true);
			TREE_Users.AppendColumn (ColumnUser);
			ColumnUser.AddAttribute (NameCellUser, "text", 0);
			
			ListStoreUser.Clear();
			foreach (User user in Groups.GetAllUsers())
			{
				ListStoreUser.AppendValues (user.Name);
			}
			TREE_Users.Model=ListStoreUser;
			// Event on treeview
			TREE_Users.Selection.Changed += OnSelectionUser;
			#endregion
			
			#region treeview user groups
			Gtk.TreeViewColumn ColumnCheck = new Gtk.TreeViewColumn ();
			ColumnCheck.Title = "";
			Gtk.CellRendererToggle NameCellCheck= new Gtk.CellRendererToggle ();
			NameCellCheck.Activatable = true;
			NameCellCheck.Toggled += new ToggledHandler (SelectToggled);
			// Add the cell to the column
			ColumnCheck.PackStart (NameCellCheck, true);
			TREE_UserGroup.AppendColumn (ColumnCheck);
			ColumnCheck.AddAttribute (NameCellCheck, "active", 0);
			

			// Create a column for the name
			Gtk.TreeViewColumn ColumnGroup = new Gtk.TreeViewColumn ();
			ColumnGroup.Title = "Group";
			Gtk.CellRendererText NameCellGroup= new Gtk.CellRendererText ();
			// Add the cell to the column
			ColumnGroup.PackStart (NameCellGroup, true);
			TREE_UserGroup.AppendColumn (ColumnGroup);
			ColumnGroup.AddAttribute (NameCellGroup, "text", 1);
			TREE_UserGroup.Model=ListStoreUserGroup;
			#endregion
			FindGroupUser("");
			
			#region treeview groups
			// Create a column for the name
			Gtk.TreeViewColumn ColumnGroups = new Gtk.TreeViewColumn ();
			ColumnGroups.Title = "Groups";
			Gtk.CellRendererText NameCellGroups = new Gtk.CellRendererText ();
			// Add the cell to the column
			ColumnGroups.PackStart (NameCellGroups, true);
			TREE_Groups.AppendColumn (ColumnGroups);
			ColumnGroups.AddAttribute (NameCellGroups, "text", 0);
			InitGroup();
			TREE_Groups.Model=ListStoreGroup;
	
			TREE_Groups.Selection.Changed += OnSelectionGroup;
			#endregion
			
		}
		private void SelectToggled (object sender, ToggledArgs args)
	    {
	       TreeIter iter;
	      if (ListStoreUserGroup.GetIterFromString (out iter, args.Path)) {
	         bool val = (bool) ListStoreUserGroup.GetValue (iter, columnSelected);
	         ListStoreUserGroup.SetValue (iter, columnSelected, !val);
	       }
	    }
		
		protected void OnSelectionGroup(object o, EventArgs args)
	    {
	   		try
			{
			 	TreeModel model;
				 if (((TreeSelection)o).GetSelected(out model, out iter))
		        {
		            GroupSelect =(string)model.GetValue (iter, 0);
				}
			}
			catch{}
		}
		protected void OnSelectionUser (object o, EventArgs args)
	    {
	   		try
			{
			 	TreeModel model;
				 if (((TreeSelection)o).GetSelected(out model, out iter))
		        {
		            string T =(string)model.GetValue (iter, 0);
					User user = new User(T);
					SAI_Name.Text=T;
					SAI_Comment.Text=user.Comment;
					SAI_Shell.Text=user.Shell;
					SAI_Home.Text=user.Home;
					SAI_Pass.Text="";
					FindGroupUser(user.Name);
				}
			}
			catch{}
		}
		private void FindGroupUser(string name)
		{
			ListStoreUserGroup.Clear();
			List<GroupUser> groupsUser = Groups.GetGroup(name);
			foreach (GroupUser groupUser in groupsUser) 
		    {
				if(groupUser.Into)
					ListStoreUserGroup.AppendValues (true,groupUser.TheGroup.Name);
				else
					ListStoreUserGroup.AppendValues (false,groupUser.TheGroup.Name);
			}

		}
		private void InitGroup()
		{
			ListStoreGroup.Clear();
			List<GroupUser> groupsUser = Groups.GetGroup("");
			foreach (GroupUser groupUser in groupsUser) 
		    {
					ListStoreGroup.AppendValues (groupUser.TheGroup.Name);
			}
		}
		protected virtual void OnBTNAddUserClicked (object sender, System.EventArgs e)
		{
			SAI_Name.Text="";
			SAI_Comment.Text="";
			SAI_Shell.Text="";
			SAI_Home.Text="";
			SAI_Pass.Text="";
			FindGroupUser("");
		}
		
		protected virtual void OnBTNAddGroupClicked (object sender, System.EventArgs e)
		{
			//create group
			Outils.ExcecuteAsRoot("/usr/sbin/groupadd "+SAI_GroupName.Text,true);
			SAI_GroupName.Text="";
			InitGroup();
		}
		
		protected virtual void OnBTNRemoveGroupClicked (object sender, System.EventArgs e)
		{
			//remove group
			if(GroupSelect=="") return;
			Outils.ExcecuteAsRoot("/usr/sbin/groupdel "+GroupSelect,true);
			InitGroup();
		}
	}
}

