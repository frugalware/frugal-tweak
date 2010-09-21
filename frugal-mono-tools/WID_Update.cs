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
	public partial class WID_Update : Gtk.Bin
	{
		//update
		ListStore UpdateListStore = new Gtk.ListStore (typeof (string));
		private Gtk.TreeIter iter;
		private string UpdateSelected="";
		
		//cache
		ListStore listStore = new Gtk.ListStore (typeof (string));
		private string pkgSelected="";
		private string dirCache="/var/cache/pacman-g2";
		
		public WID_Update ()
		{
			this.Build ();
			//update package list
			// Create a column for the package name
			Gtk.TreeViewColumn pkgupdateColumn = new Gtk.TreeViewColumn ();
			pkgupdateColumn.Title = "Package name";
			Gtk.CellRendererText pkgupdateNameCell = new Gtk.CellRendererText ();
			// Add the cell to the column
			pkgupdateColumn.PackStart (pkgupdateNameCell, true);
			TREE_UpdatePkg.AppendColumn (pkgupdateColumn);
			pkgupdateColumn.AddAttribute (pkgupdateNameCell, "text", 0);
			// Event on treeview
			TREE_UpdatePkg.Selection.Changed += OnSelectionEntryUpdate;
			TREE_UpdatePkg.Model=UpdateListStore;
			
			
			//cache
			Gtk.TreeViewColumn column = new Gtk.TreeViewColumn ();
			column.Title = "Package";
			Gtk.CellRendererText nameCell = new Gtk.CellRendererText ();
			// Add the cell to the column
			column.PackStart (nameCell, true);
			TREE_Cache.AppendColumn (column);
			column.AddAttribute (nameCell, "text", 0);
			// Event on treeview
			TREE_Cache.Selection.Changed += OnSelectionEntryCache;
			TREE_Cache.Model=listStore;
		}
		public  void InitUpdate()
		{
		if(!MainClass.boRoot)
		{
			BTN_ApplyIgnorePkg.Visible=false;
			BTN_Hide.Visible=false;
		}
			
		//update
		UpdateToTreeview();
		IgnorepkgToSAI();
		_initCache();
		}
		private void _initCache()
		{

			//cache
			List<string>  fpms = new List<string>(); 
			listStore.Clear();
			
			string pattern ="*.fpm";
			System.IO.DirectoryInfo rootDir = new System.IO.DirectoryInfo(dirCache);
	        fpms = Outils.WalkDirectoryTree(rootDir,pattern);
	        foreach (string file in fpms) 
	        {
				listStore.AppendValues(file);
			}
		}
		private void IgnorepkgToSAI()
		{
		SAI_ignorePkg.Text="";
		foreach(string ignore in MainClass.pacmanG2.ignorePkg)
			{
				SAI_ignorePkg.Text+=" "+ignore;
			}
		}
		private void _refreshUpdate()
		{
			if(Update.CheckUpdate())
			{
				if (MainClass.trayIcon!=null)
				{
					Gdk.Pixbuf ico = global::Gdk.Pixbuf.LoadFromResource ("frugalmonotools.Pictures.systrayupdate.png");
					MainClass.trayIcon.Pixbuf=ico;
				}
			}
			else
			{
				if(MainClass.trayIcon!=null)
				{
					Gdk.Pixbuf ico = global::Gdk.Pixbuf.LoadFromResource ("frugalmonotools.Pictures.systray.png");
					MainClass.trayIcon.Pixbuf=ico;
				}
			}
			UpdateToTreeview();
	}
		public void UpdateToTreeview()
		{
			UpdateListStore.Clear();
			foreach (packageCheck package in Update.UpdatePkg)
				{
				// Add some data to the store
				UpdateListStore.AppendValues (package.packagename+"-"+package.packageversion);
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
					UpdateSelected=T;
					UpdateSelected=MainClass.pacmanG2.extractNamePackage(UpdateSelected);
					if(MainClass.boRoot)
					{
						BTN_Hide.Visible=true;
					}
				}
			}
			catch{}
		}
		
	protected void OnSelectionEntryCache(object o, EventArgs args)
	    {
	   		try
			{
			 	TreeModel model;
				 if (((TreeSelection)o).GetSelected(out model, out iter))
		        {
		            string T =(string)model.GetValue (iter, 0);
					pkgSelected=T;
				}
			}
			catch{}
		}

		protected virtual void OnBTNUpdateDatabaseClicked (object sender, System.EventArgs e)
		{
			if(MainClass.boRoot)
				Outils.Excecute("pacman-g2","-Sy",true);	
			else
				Outils.ExcecuteAsRoot("pacman-g2 -Sy", true);		
			
			_refreshUpdate();
		}
		
		protected virtual void OnBTNRefreshClicked (object sender, System.EventArgs e)
		{
			_refreshUpdate();
		}
		
		protected virtual void OnBTNHideClicked (object sender, System.EventArgs e)
		{
			MainClass.pacmanG2.SetIgnorePkg(UpdateSelected,false);
			IgnorepkgToSAI();
		}
		
		protected virtual void OnBTNApplyIgnorePkgClicked (object sender, System.EventArgs e)
		{
			MainClass.pacmanG2.SetIgnorePkg(SAI_ignorePkg.Text,true);
			IgnorepkgToSAI();
		}
		
		protected virtual void OnBTNUpdateClicked (object sender, System.EventArgs e)
		{
			if(MainClass.boRoot)
				Outils.Excecute("python","/usr/bin/PyFrugalVTE pacman-g2 -Syu",false);	
			else
				Outils.ExcecuteAsRoot("python /usr/bin/PyFrugalVTE pacman-g2 -Syu",false);	
		}
		protected virtual void OnBTNClearCacheClicked (object sender, System.EventArgs e)
		{
			if(MainClass.boRoot)
				Outils.Excecute("rm",dirCache+"/pkg/*.fpm && exit",true);	
			else
				Outils.ExcecuteAsRoot("rm  "+dirCache+"/pkg/*.fpm && exit",true);		
			_initCache();
		}
		
		protected virtual void OnBTNInstallClicked (object sender, System.EventArgs e)
		{
			if (pkgSelected=="") return;
			if(MainClass.boRoot)
				Outils.Excecute("python","/usr/bin/PyFrugalVTE pacman-g2 -U "+pkgSelected,false);	
			else
				Outils.ExcecuteAsRoot("python /usr/bin/PyFrugalVTE pacman-g2 -U "+pkgSelected,false);		
		}
		
		
		
	}
}

