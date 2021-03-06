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
	public partial class WID_System : Gtk.Bin
	{
		
		ListStore modelLocale = new ListStore (typeof (string));
		ListStore modelKeymap = new ListStore (typeof (string));
		ListStore modelTime = new ListStore (typeof (string));
		private Gtk.TreeIter iter;
		
		public WID_System ()
		{
			this.Build ();
			
		}
		public void InitSystem()
		{
			BTN_System.Visible=false;	
			if(MainClass.boRoot)
			{
				BTN_System.Visible=true;
				LIB_Root.Visible=false;
			}
			else
			{
				LIB_Root.Visible=true;
			}
		//system configuration
		SAI_Host.Text=MainClass.confSystem.GetHostname();
		SAI_Distribution.Text=MainClass.confSystem.GetDistribution();
		SAI_Kernel.Text=MainClass.confSystem.GetKernel();
		SAI_Shell.Text=MainClass.confSystem.GetUserShell();
		CBO_Locale.Model=modelLocale;
		foreach (string locale in  MainClass.confSystem.LocaleSystem)
			{
				iter=modelLocale.AppendValues(locale);
				if(MainClass.confSystem.GetLocale()==locale)
					CBO_Locale.SetActiveIter(iter);
			}
			CBO_Keymap.Model=modelKeymap;
			foreach (string keymap in  MainClass.confSystem.KeymapSystem)
			{
				iter=modelKeymap.AppendValues(keymap);
				if(MainClass.confSystem.GetKeymap()==keymap)
					CBO_Keymap.SetActiveIter(iter);
			}
			
			CBO_Time.Model=modelTime;
			foreach (string time in  MainClass.confSystem.LocalTimeSystem)
			{
				iter=modelTime.AppendValues(time);
				if(MainClass.confSystem.GetLocalTime()==time)
					CBO_Time.SetActiveIter(iter);
			}

		}
		protected virtual void OnBTNSystemClicked (object sender, System.EventArgs e)
		{
			MainClass.confSystem.SetHostname(SAI_Host.Text);
			MainClass.confSystem.SetLocale(CBO_Locale.Entry.Text);
			MainClass.confSystem.SetKeymap(CBO_Keymap.Entry.Text);
			MainClass.confSystem.SetTime(CBO_Time.Entry.Text);
			MainClass.confSystem.Save();
		}
		
	}
}

