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
	public partial class WID_Grub : Gtk.Bin
	{
		ListStore model = new ListStore (typeof (string),typeof (int)); 
		int EntrySelected = 0;
		public WID_Grub ()
		{
			this.Build ();
			
		}
		public void InitGrub()
		{
			if(!MainClass.boRoot)
			{
				BTN_RemoveEntry.Visible = false;
				BTN_AddEntry.Visible = false;
				BTN_Save.Visible = false;
				BTN_Apply.Visible = false;
				BTN_Modify.Visible=false;
				SAI_Hdd.Visible = false;
				LIB_Root.Visible=true;
			}
			else
			{
				LIB_Root.Visible=false;
			}
			model.Clear();
			CBO_Entry.Model=model;
			int i = 0;
			TreeIter iter =new TreeIter(); 
			foreach (GrubEntry entry in MainClass.grub.Entrys)
			{
				string titre=entry.title;
				iter=model.AppendValues(titre,i);
				if(i==0)
					CBO_Entry.SetActiveIter(iter); 
				i++;
			}
			SAI_Default.Text=MainClass.grub.GetDefault().ToString();
			SAI_TimeOut.Text=MainClass.grub.GetTimeout().ToString();
			SAI_Gfx.Text=MainClass.grub.GetGfx();
			
		}
		protected virtual void OnCBOEntryChanged (object sender, System.EventArgs e)
		{
			TreeIter iter;
			if ((sender as ComboBox).GetActiveIter (out iter))
			{
				int id =(int)model.GetValue (iter,1);
				this.EntrySelected= id;
				this.TXT_Options.Buffer.Text=MainClass.grub.Entrys[id].options;
				this.SAI_Title.Text=MainClass.grub.Entrys[id].title;
			}
		}
		
		protected virtual void OnBTNRemoveEntryClicked (object sender, System.EventArgs e)
		{
			MainClass.grub.Entrys.RemoveAt(this.EntrySelected);
			this.InitGrub();
		}
		
		protected virtual void OnBTNApplyClicked (object sender, System.EventArgs e)
		{
			MainClass.grub.Install(SAI_Hdd.Text);
		}
		
		protected virtual void OnBTNAddEntryClicked (object sender, System.EventArgs e)
		{
			DiagGrub dialog = new DiagGrub();
			dialog.Run();
			this.InitGrub();
		}
		
		protected virtual void OnBTNSaveClicked (object sender, System.EventArgs e)
		{
			MainClass.grub.SetDefault(int.Parse(this.SAI_Default.Text));
			MainClass.grub.SetGfx(this.SAI_Gfx.Text);
			MainClass.grub.SetTimeOut(int.Parse(this.SAI_TimeOut.Text));
			MainClass.grub.Save();
		}
		
		protected virtual void OnBTNModifyClicked (object sender, System.EventArgs e)
		{
			GrubEntry entry = new GrubEntry();
			entry.title=SAI_Title.Text;
			entry.options=TXT_Options.Buffer.Text;
			MainClass.grub.Entrys[EntrySelected]=entry;
		}
		
		
		
		

		
	}
}

