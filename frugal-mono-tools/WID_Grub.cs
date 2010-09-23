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
		
		public WID_Grub ()
		{
			this.Build ();
		}
		public void InitGrub()
		{
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
			
		}
		protected virtual void OnCBOEntryChanged (object sender, System.EventArgs e)
		{
			TreeIter iter;
			if ((sender as ComboBox).GetActiveIter (out iter))
			{
				int id =(int)model.GetValue (iter,1);
				this.TXT_Options.Buffer.Text=MainClass.grub.Entrys[id].options;
				this.SAI_Title.Text=MainClass.grub.Entrys[id].title;
			}
		}
		
		
	}
}

