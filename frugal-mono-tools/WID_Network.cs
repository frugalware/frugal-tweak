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
		}
	public void InitNetworkManager()
	{
		if(!MainClass.boRoot)
			{
				BTN_Network.Visible=false;
				LIB_Root.Visible=true;
			}
		else
			{
				LIB_Root.Visible=false;
			}
		//network init
		EnableDisable(INT_WICD,"wicd",LIB_WICDNotInstalled);
		EnableDisable(INT_NM,"networkmanager",LIB_NMNotInstalled);
		Service nm = new Service("NetworkManager");
		INT_NM.Active=nm.IsStartedOnBoot();
		Service wicd = new Service("wicd");
		INT_WICD.Active=wicd.IsStartedOnBoot();
		if(!INT_WICD.Active && !INT_NM.Active)
				INT_FW.Active=true;
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
		
	protected virtual void OnBTNNetworkClicked (object sender, System.EventArgs e)
	{
		Outils.Service("NetworkManager",this.INT_NM.Active);
		Outils.Service("wicd",this.INT_WICD.Active);
	}
		
	protected virtual void OnINTFWClicked (object sender, System.EventArgs e)
	{
			if (this.INT_FW.Active)
		{
			this.INT_NM.Active=false;
			this.INT_WICD.Active=false;
		}
		else
		{ if((this.INT_NM.Active==false) && (this.INT_WICD.Active==false))
			this.INT_FW.Active=true;
		}
	}
		
	protected virtual void OnINTNMClicked (object sender, System.EventArgs e)
	{
		if (INT_NM.Inconsistent) return;
		if (this.INT_NM.Active)
		{
			this.INT_FW.Active=false;
			this.INT_WICD.Active=false;
		}
		else
		{ if((this.INT_FW.Active==false) && (this.INT_WICD.Active==false))
			this.INT_NM.Active=true;
		}

	}
		
	protected virtual void OnINTWICDClicked (object sender, System.EventArgs e)
	{
		if (INT_WICD .Inconsistent) return;
		if (this.INT_WICD.Active)
		{
			this.INT_FW.Active=false;
			this.INT_NM.Active=false;
		}
		else
		{ if((this.INT_FW.Active==false) && (this.INT_NM.Active==false))
			this.INT_WICD.Active=true;
		}
	}
		
		
		
		
		

	}
}

