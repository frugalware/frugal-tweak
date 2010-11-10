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
namespace frugalmonotools
{
	public partial class DiagGrub : Gtk.Dialog
	{
		protected virtual void OnBTNAddClicked (object sender, System.EventArgs e)
		{
			GrubEntry entry = new GrubEntry();
			entry.title=SAI_Entry.Text;
			entry.options=TXT_Options.Buffer.Text;
			MainClass.grub.Entrys.Add(entry);
			this.Destroy();
		}
		
		
		public DiagGrub ()
		{
			this.Build ();
		}
		protected virtual void OnButton36Clicked (object sender, System.EventArgs e)
		{
			this.Destroy();
		}
		
		
	}
}

