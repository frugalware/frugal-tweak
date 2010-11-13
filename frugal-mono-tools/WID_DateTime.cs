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
	[System.ComponentModel.ToolboxItem(true)]
	public partial class WID_DateTime : Gtk.Bin
	{
		public WID_DateTime ()
		{
			this.Build ();
		}
		public void InitDateTime()
		{
			if(!MainClass.boRoot)
			{
				BTN_Apply.Visible=false;
				LIB_Root.Visible=true;
			}
			else
			{
				BTN_Apply.Visible=true;
				LIB_Root.Visible=false;
			}
			SAI_Date.Text=Outils.getoutput("date +%Y%m%d");
			SAI_Hours.Text=Outils.getoutput("date +%T ");
		}
		protected virtual void OnBTNApplyClicked (object sender, System.EventArgs e)
		{
			//TODO :
			//added calendar widget
			//date +%Y%m%d+%T -s "20101111 13:09:17"
			Outils.Excecute("date"," +%Y%m%d+%T -s '" +SAI_Date.Text+" "+SAI_Hours.Text+"'",false);
			
		}
		
		
	}
}

