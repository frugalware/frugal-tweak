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
	public partial class WID_Pkg : Gtk.Bin
	{
		private Gtk.TreeIter iter;
		private string packageSelected="";
		ListStore modelRepoList = new ListStore (typeof (string),typeof (int)); 
		ListStore pkgListStore = new Gtk.ListStore (typeof (string),typeof (string),typeof(string));
		
		public WID_Pkg ()
		{
			this.Build ();
			_initPkg();
		}
		private void _initPkg()
		{
		//pacman-g2
		// Create a column for the package name
		Gtk.TreeViewColumn pkgColumn = new Gtk.TreeViewColumn ();
		pkgColumn.Title = "Package name";
		Gtk.CellRendererText pkgNameCell = new Gtk.CellRendererText ();
		// Add the cell to the column
		pkgColumn.PackStart (pkgNameCell, true);
		treeviewpkg.AppendColumn (pkgColumn);
		pkgColumn.AddAttribute (pkgNameCell, "text", 0);

		// Create a column for the package group
		Gtk.TreeViewColumn pkgGroupColumn = new Gtk.TreeViewColumn ();
		pkgGroupColumn.Title = "Group";
		Gtk.CellRendererText pkgGroupCell = new Gtk.CellRendererText ();
		// Add the cell to the column
		pkgGroupColumn.PackStart (pkgGroupCell, true);
		treeviewpkg.AppendColumn (pkgGroupColumn);
		pkgGroupColumn.AddAttribute (pkgGroupCell, "text", 1);
		
		Gtk.TreeViewColumn ColumnPkgDesc = new Gtk.TreeViewColumn ();
		ColumnPkgDesc.Title = "Description";
		Gtk.CellRendererText PkgDescCell = new Gtk.CellRendererText ();
		// Add the cell to the column
		ColumnPkgDesc.PackStart (PkgDescCell, true);
		treeviewpkg.AppendColumn (ColumnPkgDesc);
		ColumnPkgDesc.AddAttribute (PkgDescCell, "text", 2);
		
		int i = 0 ;
		TreeIter iter =new TreeIter();
		foreach (string repo in  MainClass.pacmanG2.fwRepo)
		{
			string strRepo=repo;
			if (strRepo=="local") strRepo ="Installed";
			iter = modelRepoList.AppendValues(strRepo,i);
			i++;
		}
		CBO_Repo.Model=modelRepoList;
		CBO_Repo.SetActiveIter(iter); 
		BTN_Uninstall.Visible=false;
		BTN_Install.Visible=false;
		// Assign the model to the TreeView
		treeviewpkg.Model = pkgListStore;
		
		// Event on treeview
		treeviewpkg.Selection.Changed += OnSelectionEntryPkg;
		
		}
			protected void OnSelectionEntryPkg (object o, EventArgs args)
	    {
	   		try
			{
			 	TreeModel model;
				 if (((TreeSelection)o).GetSelected(out model, out iter))
		        {
		            string T =(string)model.GetValue (iter, 0);
					T=MainClass.pacmanG2.extractNamePackage(T);
					packageSelected=T;

					if(MainClass.pacmanG2.IsInstalled(T))
					{
						BTN_Uninstall.Visible=true;
						BTN_Install.Visible=false;
					}
					else
					{
						BTN_Uninstall.Visible=false;
						BTN_Install.Visible=true;
					}
					
				}
			}
			catch{}
		}

		protected virtual void OnCBORepoChanged (object sender, System.EventArgs e)
		{
			TreeIter iter;
			if ((sender as ComboBox).GetActiveIter (out iter))
			{
				int id =(int)modelRepoList.GetValue (iter,1);
				MainClass.pacmanG2.SelectRepo(MainClass.pacmanG2.fwRepo[id]);
			}
		}
		private void _searchPackage(){
		try{
				List<Package> packages=MainClass.pacmanG2.Search(SAI_pkg.Text,MainClass.pacmanG2.repoSelected,true);
				pkgListStore.Clear();
				foreach (Package package in packages)
				{
					// Add some data to the store
					pkgListStore.AppendValues (package.GetPkgname()+"-"+package.GetPkgversion(),package.GetGroup()
					                           	,package.GetDescription());
				}
			}
			catch{}
	}
		
		protected virtual void OnBTNSearchClicked (object sender, System.EventArgs e)
		{
			_searchPackage();
		}
		
		protected virtual void OnBTNUninstallClicked (object sender, System.EventArgs e)
		{
			if(packageSelected=="") return;
			if(MainClass.boRoot)
				Outils.Excecute("python","/usr/bin/PyFrugalVTE pacman-g2 -Rc "+packageSelected,true);	
			else
				Outils.ExcecuteAsRoot("python /usr/bin/PyFrugalVTE pacman-g2 -Rc "+packageSelected,true);	
			_searchPackage();
		}
		
		protected virtual void OnBTNInstallClicked (object sender, System.EventArgs e)
		{
			if(packageSelected=="") return;
			if(MainClass.boRoot)
				Outils.Excecute("python","/usr/bin/PyFrugalVTE pacman-g2 -Sy "+packageSelected,true);
			else
				Outils.ExcecuteAsRoot("python /usr/bin/PyFrugalVTE pacman-g2 -Sy "+packageSelected,true);	
			_searchPackage();
		}
		
		
		
		
	}
}

