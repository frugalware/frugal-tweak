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
using Gtk;
namespace frugalmonotools
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class WID_Services : Gtk.Bin
	{
		private string ServiceSelected="";
		ListStore serviceListStore = new Gtk.ListStore (typeof (string),typeof (string),typeof (string),typeof (string));
		private Gtk.TreeIter iter;
		
		public WID_Services ()
		{
			this.Build ();
		}
		public void InitService()
		{
		// services
		Gtk.TreeViewColumn ColumnServiceName = new Gtk.TreeViewColumn ();
		ColumnServiceName.Title = "Services";
		Gtk.CellRendererText ServiceNameCell = new Gtk.CellRendererText ();
		// Add the cell to the column
		ColumnServiceName.PackStart (ServiceNameCell, true);
		TREE_Services.AppendColumn (ColumnServiceName);
		ColumnServiceName.AddAttribute (ServiceNameCell, "text", 0);
		
		Gtk.TreeViewColumn ColumnServiceStarted = new Gtk.TreeViewColumn ();
		ColumnServiceStarted.Title = "Started";
		Gtk.CellRendererText ServiceStartedCell = new Gtk.CellRendererText ();
		// Add the cell to the column
		ColumnServiceStarted.PackStart (ServiceStartedCell, true);
		TREE_Services.AppendColumn (ColumnServiceStarted);
		ColumnServiceStarted.AddAttribute (ServiceStartedCell, "text", 1);
		
		Gtk.TreeViewColumn ColumnServiceOnBoot = new Gtk.TreeViewColumn ();
		ColumnServiceOnBoot.Title = "Start on boot";
		Gtk.CellRendererText ServiceOnBootCell = new Gtk.CellRendererText ();
		// Add the cell to the column
		ColumnServiceOnBoot.PackStart (ServiceOnBootCell, true);
		TREE_Services.AppendColumn (ColumnServiceOnBoot);
		ColumnServiceOnBoot.AddAttribute (ServiceOnBootCell, "text", 2);
		
		Gtk.TreeViewColumn ColumnServiceDesc = new Gtk.TreeViewColumn ();
		ColumnServiceDesc.Title = "Description";
		Gtk.CellRendererText ServiceDescCell = new Gtk.CellRendererText ();
		// Add the cell to the column
		ColumnServiceDesc.PackStart (ServiceDescCell, true);
		TREE_Services.AppendColumn (ColumnServiceDesc);
		ColumnServiceDesc.AddAttribute (ServiceDescCell, "text", 3);
	
		foreach(Service service in ServicesRc.Services)
		{
			string Etat = "yes";
			if (!service.IsStarted()) Etat="No";
			string OnBoot = "yes";
			if (!service.IsStartedOnBoot()) OnBoot = "No";
			serviceListStore.AppendValues(service.Get_Name(),Etat,OnBoot,service.GetDescription());             
		}
		TREE_Services.Model=serviceListStore;
		// Event on treeview
		TREE_Services.Selection.Changed += OnSelectionEntryService;
	
		}
		protected void OnSelectionEntryService(object o, EventArgs args)
	    {
	   		try
			{
				
			 	TreeModel model;
				 if (((TreeSelection)o).GetSelected(out model, out iter))
		        {
		            string T =(string)model.GetValue (iter, 0);
					ServiceSelected=T;
					if(MainClass.boRoot)
					{
						BTN_ServiceStop.Visible=false;
						BTN_ServiceStart.Visible=false;
						BTN_ServiceDelBoot.Visible=false;
						BTN_ServiceAddBoot.Visible=false;
						Service service = new Service(T);
						if (service.IsStarted())
							BTN_ServiceStop.Visible=true;
						else
							BTN_ServiceStart.Visible=true;
						if(service.IsStartedOnBoot())
							BTN_ServiceDelBoot.Visible=true;
						else
							BTN_ServiceAddBoot.Visible=true;
						
					}
				}
			}
			catch{}
		}
	private void _serviceRefresh()
	{
		serviceListStore.Clear();
		foreach(Service service in ServicesRc.Services)
		{
			string Etat = "yes";
			if (!service.IsStarted()) Etat="No";
			string OnBoot = "yes";
			if (!service.IsStartedOnBoot()) OnBoot = "No";
			serviceListStore.AppendValues(service.Get_Name(),Etat,OnBoot,service.GetDescription());             
		}
	}
		
	protected virtual void OnBTNServiceStartClicked (object sender, System.EventArgs e)
	{
		if(ServiceSelected!="")
		{
			Service service = new Service(ServiceSelected);
			service.Start();
			_serviceRefresh();
		}
	}
		
	protected virtual void OnBTNServiceStopClicked (object sender, System.EventArgs e)
	{
		if(ServiceSelected!="")
		{
			Service service = new Service(ServiceSelected);
			service.Stop();
			_serviceRefresh();
		}
	}
		
	protected virtual void OnBTNServiceDelBootClicked (object sender, System.EventArgs e)
	{
		if(ServiceSelected!="")
		{
			Service service = new Service(ServiceSelected);
			service.EnableDisableOnBoot(false);
			_serviceRefresh();
		}
	}
		
	protected virtual void OnBTNServiceAddBootClicked (object sender, System.EventArgs e)
	{
		if(ServiceSelected!="")
		{
			Service service = new Service(ServiceSelected);
			service.EnableDisableOnBoot(true);
			_serviceRefresh();
		}
	}
		
		
		
		
		
	}
}

