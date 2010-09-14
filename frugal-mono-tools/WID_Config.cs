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
	public partial class WID_Config : Gtk.Bin
	{
		
		public WID_Config ()
		{
			this.Build ();
		}
		public  void InitConfig()
		{
			//configuration
			INT_CheckStartup.Active=MainClass.configuration.Get_CheckUpdate();
			INT_StartWithXSession.Active=MainClass.configuration.Get_StartWithX();
			INT_ShowNotif.Active=MainClass.configuration.Get_ShowNotif();
			INT_ShowSplash.Active=MainClass.configuration.Get_ShowSplash();
		}
		protected virtual void OnBTNSaveConfClicked (object sender, System.EventArgs e)
		{
			MainClass.configuration.Set_CheckUpdate(INT_CheckStartup.Active);
			MainClass.configuration.Set_StartWithX(INT_StartWithXSession.Active);
			MainClass.configuration.Set_ShowNotif(INT_ShowNotif.Active);
			MainClass.configuration.Set_ShowSplash(INT_ShowSplash.Active);
			MainClass.configuration.ConfSave();
		}
		
		
	}
}

