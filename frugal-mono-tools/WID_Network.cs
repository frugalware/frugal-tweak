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
	public partial class WID_Network : Gtk.Bin
	{
		public WID_Network ()
		{
			this.Build ();
			_initNetworkManager();
		}
	private void _initNetworkManager()
	{
		//network init
		INT_NM.Active=Outils.ServiceOnStartUp("S99rc.networkmanager");
		EnableDisable(INT_NM,"networkmanager",LIB_NMNotInstalled);
		INT_WICD.Active=Outils.ServiceOnStartUp("S99rc.wicd");
		EnableDisable(INT_WICD,"wicd",LIB_WICDNotInstalled);
		if((!INT_NM.Active) && (!INT_WICD.Active))
		{
			INT_FW.Active=true;
		}
		else
		{
			INT_FW.Active=false;
		}
	}
		
	public void EnableDisable(CheckButton INT_Option,string packageName, Label text)
	{
		//check if file existe for works more quickly
		if(!MainClass.pacmanG2.IsInstalled(packageName))
		{
			INT_Option.Active=false;
			INT_Option.Inconsistent=true;
			text.Visible=true;
		}
		else
		{
			text.Visible=false;
		}
	}	
		

	}
}

