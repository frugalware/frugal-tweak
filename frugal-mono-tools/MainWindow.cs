/*
 *  Copyright (C) 2010 by Gaetan Gourdin <bouleetbil@frogdev.info>
 *
 *  This program is free software; you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation; either version 2 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program; if not, write to the Free Software
 *  Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA 02111-1307, USA.
 */
using System;

using System.Net;
using System.IO;
using Gtk;
using WebKit;

using System.Collections.Generic;

using frugalmonotools;

public partial class MainWindow : Gtk.Window
{
	protected Gtk.TreeIter iter;
	
	
	
	
	
	private bool boRoot = false;
	//pacman-g2
	// Create a model for treeview pkg
	
	
	
	
	
	
		
	public MainWindow () : base(Gtk.WindowType.Toplevel)
	{
		this.SetDefaultSize (700, 500);
		Build ();
		
		//graphical debug
		if ( Debug.ModeDebug && Debug.ModeDebugGraphique)
		{
			Debug.winDebug = new FEN_Debug(); 
			Debug.winDebug.Show();
		}
		
				
		
		_initHardware();
		
		BTN_Uninstall.Visible=false;
		BTN_Install.Visible=false;
		//root options
		if (Mono.Unix.Native.Syscall.getuid()!=0)
		{
			BTN_Network.Visible=false;
			BTN_LoginManager.Visible=false;
			BTN_Xorg.Visible=false;
			BTN_Setup.Visible = false;
			BTN_UpdateDatabase.Visible = false;
			BTN_System.Visible=false;
			BTN_ApplyIgnorePkg.Visible=false;
		}
		else
		{
			boRoot=true;
		}
		
		

		

		
		
		
	}

	private  void _initHardware()
	{
			//HW
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
	
	}
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		if(MainClass.StartedAutomatic)
			this.Hide();
		else
			Application.Quit ();
		
		a.RetVal = true;
	}
	/// <summary>
	///enregistrement reseau 
	/// </summary>
	/// <param name="sender">
	/// A <see cref="System.Object"/>
	/// </param>
	/// <param name="e">
	/// A <see cref="System.EventArgs"/>
	/// </param>
	protected virtual void ApplyNetwork (object sender, System.EventArgs e)
	{
		Outils.Service("networkmanager",this.INT_NM.Active);
		Outils.Service("wicd",this.INT_WICD.Active);
		
	}
	protected virtual void SelectItem (object sender, System.EventArgs e)
	{
		
	}
	
	
	
	
	protected virtual void OpenLink (object sender, System.EventArgs e)
	{
		//by default use firefox
		if (!Outils.Excecute("firefox",BTN_Link.Label,false))
		{
			if (!Outils.Excecute("midori",BTN_Link.Label,false))
			{
				//last chance :p
				Outils.Excecute("konqueror",BTN_Link.Label,false);
			}
		}
	}
	
	
	
	protected virtual void usefw (object sender, System.EventArgs e)
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
	
	protected virtual void usenm (object sender, System.EventArgs e)
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
	
	protected virtual void usewicd (object sender, System.EventArgs e)
	{
		if (INT_NM.Inconsistent) return;
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
	
	protected virtual void OnINTXDMClicked (object sender, System.EventArgs e)
	{
		if (this.INT_XDM.Active)
		{
			this.INT_KDM.Active=false;
			this.INT_GDM.Active=false;
			this.INT_Slim.Active=false;
			this.INT_LXDM.Active=false;
		}
		
	}
	
	protected virtual void OnINTLXDMClicked (object sender, System.EventArgs e)
	{
		if (this.INT_LXDM.Active)
		{
			this.INT_KDM.Active=false;
			this.INT_GDM.Active=false;
			this.INT_Slim.Active=false;
			this.INT_XDM.Active=false;
		}
	}
	
	protected virtual void OnINTSlimClicked (object sender, System.EventArgs e)
	{
		if (this.INT_Slim.Active)
		{
			this.INT_KDM.Active=false;
			this.INT_GDM.Active=false;
			this.INT_XDM.Active=false;
			this.INT_LXDM.Active=false;
		}
	}
	
	protected virtual void OnINTGDMClicked (object sender, System.EventArgs e)
	{
		if (this.INT_GDM.Active)
		{
			this.INT_KDM.Active=false;
			this.INT_XDM.Active=false;
			this.INT_Slim.Active=false;
			this.INT_LXDM.Active=false;
		}
	}
	
	protected virtual void OnINTKDMClicked (object sender, System.EventArgs e)
	{
		if (this.INT_KDM.Active)
		{
			this.INT_XDM.Active=false;
			this.INT_GDM.Active=false;
			this.INT_Slim.Active=false;
			this.INT_LXDM.Active=false;
		}
	}
	
	protected virtual void OnBTNLoginManagerClicked (object sender, System.EventArgs e)
	{
		
	}
	/// <summary>
	///Enable / Disable some options if package is installed 
	/// </summary>
	/// <param name="INT_Option">
	/// A <see cref="CheckButton"/>
	/// </param>
	/// <param name="FileToTest">
	/// A <see cref="System.String"/>
	/// </param>
	/// <param name="text">
	/// A <see cref="Label"/>
	/// </param>
	
	
	protected virtual void ApplyXorg (object sender, System.EventArgs e)
	{
		
	}
	
	protected virtual void OnBTNSearchClicked (object sender, System.EventArgs e)
	{
		
	}
	
	protected virtual void selectRepo (object sender, System.EventArgs e)
	{
		
	}
	
	
		
	protected virtual void OnBTNUninstallClicked (object sender, System.EventArgs e)
	{
		
	
	}
	
	protected virtual void OnBTNInstallClicked (object sender, System.EventArgs e)
	{
		
	
	}
	
	protected virtual void OnBTNUpdateClicked (object sender, System.EventArgs e)
	{

	}
	
	protected virtual void OnBTNPrinterClicked (object sender, System.EventArgs e)
	{
		Outils.Excecute("system-config-printer","",true);
	}
	
	protected virtual void OnBTNSynapticsClicked (object sender, System.EventArgs e)
	{
		Outils.Excecute("python","/usr/bin/PyFrugalVTE pacman-g2 -Sy xf86-input-synaptics",false);	
	}
	
	protected virtual void OnBTNSetupClicked (object sender, System.EventArgs e)
	{		
		Outils.Excecute("python","/usr/bin/PyFrugalVTE /sbin/setup",false);		
	}
	protected virtual void OnBTNSaveConfClicked (object sender, System.EventArgs e)
	{
		MainClass.configuration.Set_CheckUpdate(INT_CheckStartup.Active);
		MainClass.configuration.Set_StartWithX(INT_StartWithXSession.Active);
		MainClass.configuration.Set_ShowNotif(INT_ShowNotif.Active);
		MainClass.configuration.Set_ShowSplash(INT_ShowSplash.Active);
		MainClass.configuration.ConfSave();
	}
	
	protected virtual void OnBTNServiceStartClicked (object sender, System.EventArgs e)
	{
	
	}
	
	protected virtual void OnBTNServiceStopClicked (object sender, System.EventArgs e)
	{
		
	}
	
	protected virtual void OnBTNServiceDelBootClicked (object sender, System.EventArgs e)
	{
		
	}
	
	protected virtual void OnBTNServiceAddBootClicked (object sender, System.EventArgs e)
	{
		
	}
	
	protected virtual void OnBTNIrcClicked (object sender, System.EventArgs e)
	{
			_joinIrc("frugalware");
	}
	private void _joinIrc(string channel){
		Outils.Excecute("mono","/usr/lib/frugalware-tweak/frugal-irc.exe "+channel,false);		
	}
	protected virtual void OnBTNForumsClicked (object sender, System.EventArgs e)
	{
		WebkitBrowser browser = new WebkitBrowser("http://forums.frugalware.org");
		browser.Show();
	}
	
	protected virtual void OnBTNWikiClicked (object sender, System.EventArgs e)
	{
		WebkitBrowser browser = new WebkitBrowser("http://wiki.frugalware.org");
		browser.Show();
	}
	
	
	
	protected virtual void OnBTNDanishClicked (object sender, System.EventArgs e)
	{
		WebkitBrowser browser = new WebkitBrowser("http://frugalware.dk/");
		browser.Show();
	}
	
	protected virtual void OnBTNFrenchClicked (object sender, System.EventArgs e)
	{
		WebkitBrowser browser = new WebkitBrowser("http://www.frugalware.fr");
		browser.Show();
	}
	
	protected virtual void OnBTNBugsClicked (object sender, System.EventArgs e)
	{
		WebkitBrowser browser = new WebkitBrowser("http://bugs.frugalware.org");
		browser.Show();
	}
	
	protected virtual void OnBTNRefreshClicked (object sender, System.EventArgs e)
	{
		
	}


	protected virtual void OnBTNUpdateDatabaseClicked (object sender, System.EventArgs e)
	{
		
	}
	
	protected virtual void OnBTNHideClicked (object sender, System.EventArgs e)
	{
		
	}
	
	protected virtual void OnBTNIrc1Clicked (object sender, System.EventArgs e)
	{
		_joinIrc("frugalware.fr");
	}
	
	protected virtual void OnBTNIrc2Clicked (object sender, System.EventArgs e)
	{
		_joinIrc("frugalware.hu");
	}
	
	protected virtual void OnBTNSystemClicked (object sender, System.EventArgs e)
	{
		
	}
	
	protected virtual void OnBTNApplyIgnorePkgClicked (object sender, System.EventArgs e)
	{
		
	}
	
	
	
	
	
	
}

