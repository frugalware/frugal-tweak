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
	public partial class WID_Hardware : Gtk.Bin
	{
		public WID_Hardware ()
		{
			this.Build ();
		}
		public void InitHardware()
		{
		if(!MainClass.pacmanG2.IsInstalled("system-config-printer"))
		{
			BTN_Printer.Visible=false;
			LAB_Printer.Visible=true;
		}
		else
		{
			BTN_Printer.Visible=true;
			LAB_Printer.Visible=false;
		}
		if(!MainClass.pacmanG2.IsInstalled("frugalwareutils"))
		{
			BTN_Setup.Visible=false;
			LIB_Setup.Visible=true;
		}
		else
		{
			BTN_Setup.Visible=true;
			LIB_Setup.Visible=false;
		}
		if(!MainClass.boRoot)
		{
			BTN_Setup.Visible = false;
		}
		string dmesgOutput=Outils.ReadFile( "/var/log/syslog");//Outils.getoutput("/bin/dmesg");
		if(dmesgOutput.IndexOf("lirc")>0)
		{
			if (!MainClass.pacmanG2.IsInstalled("lirc"))
			{
				LIB_Lirc.Visible=true;
			}
			else
			{
				LIB_Lirc.Visible=false;
			}
		}
		else
		{
			LIB_Lirc.Visible=false;
		}
				
		
		if(dmesgOutput.IndexOf("Bluetooth")>0)
		{
			if (!MainClass.pacmanG2.IsInstalled("bluez"))
			{
				LIB_Bluez.Visible=true;
			}
			else
			{
				LIB_Bluez.Visible=false;
			}
		}
		else
		{
			LIB_Bluez.Visible=false;
		}
	
		string lspci ="/usr/sbin/lspci";
		try
			{
				lspci=Outils.getoutput(lspci);
				TXT_Lspci.Buffer.Text=lspci;
			}
		catch
			{
				lspci="";
			}

		}
		protected virtual void OnBTNPrinterClicked (object sender, System.EventArgs e)
		{
			Outils.Excecute("system-config-printer","",true);
		}
		
		protected virtual void OnBTNSetupClicked (object sender, System.EventArgs e)
		{
			Outils.Excecute("python","/usr/bin/PyFrugalVTE /sbin/setup",false);		
		}
		
		
		
	}
}

