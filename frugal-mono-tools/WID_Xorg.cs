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
using System.IO;
using Gtk;
namespace frugalmonotools
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class WID_Xorg : Gtk.Bin
	{
		ListStore modelXorgDrivers = new ListStore (typeof (string));
		protected Gtk.TreeIter iter;
		
		const string cch_FileNumLock=@"/etc/sysconfig/numlock";
		const string cch_FileLayoutXorg=@"/etc/X11/xorg.conf.d/10-evdev.conf";
		
		public WID_Xorg ()
		{
			this.Build ();
			_initXorg();
		}
		
		private void _initXorg()
		{
			//xorg configuration
		SAI_Layout.Text=this.LayoutXorg();
		
		string lspci ="/usr/sbin/lspci";
		try
		{
			lspci=Outils.getoutput(lspci);
		}
		catch
		{
			lspci="";
		}
		string[] lspcis = lspci.Split('\n');
		foreach (string line in lspcis)
        {
			if (line.IndexOf("VGA compatible controller") > 0)
			{
				lspci =line.Split(':')[2];
				break;
			}
		}
		LIB_Lspci.Text=lspci;
		
		CBO_GraphicalDevice.Model=modelXorgDrivers;
		foreach (string driver in  MainClass.xorg.DriversInstalled)
		{
			iter=modelXorgDrivers.AppendValues(driver);
			if(GraphicalDevice()==driver)
				CBO_GraphicalDevice.SetActiveIter(iter);
		}
		
		INT_Numlock.Active=IsNumlockOnStartX();
		string dmesgOutput=Outils.ReadFile( "/var/log/syslog");//Outils.getoutput("/bin/dmesg");
		if ((dmesgOutput.IndexOf("TouchPad")>0) && (!MainClass.pacmanG2.IsInstalled("xf86-input-synaptics")))
			BTN_Synaptics.Visible=true;
		else
			BTN_Synaptics.Visible=false;
		}
		public bool IsNumlockOnStartX()
	{
		try
		{
			string str_content=Outils.ReadFile(cch_FileNumLock);
			string[] lines = str_content.Split('\n');
			foreach (string line in lines)
	            {
					if (line.IndexOf("NUMLOCK_ON")>=0)
					{
						if (line.IndexOf("#")!=0)
						{
								string[] str_val=line.Split('=');
								if (int.Parse( str_val[1])==1)
										return true;
						}
					}
			}
			return false;
		}
		catch{return false;}
	}
		
		public string LayoutXorg()
	{
		try
		{
			string layout="";
			System.IO.StreamReader textFile = new System.IO.StreamReader(cch_FileLayoutXorg);
	        string fileContents = textFile.ReadToEnd();
	        textFile.Close();
	        string[] lines = fileContents.Split('\n');
	        foreach (string line in lines)
	        {
				if (line.IndexOf("xkb_layout") > 0)
				{
					string[]ligne= line.Split('"');
					layout=ligne[3];
				}
				
			}
			return layout;
		}
		catch{
			return "";
		}
	}
	
		protected virtual void OnBTNXorgClicked (object sender, System.EventArgs e)
		{
			try{
		StreamWriter FileX = new StreamWriter(cch_FileLayoutXorg);
		FileX.WriteLine("Section \"InputClass\"");
        FileX.WriteLine("Identifier \"evdev keyboard catchall\"");
        FileX.WriteLine("MatchIsKeyboard \"on\"");
        FileX.WriteLine("MatchDevicePath \"/dev/input/event*\"");
        FileX.WriteLine("Driver \"evdev\"");
        FileX.WriteLine("Option \"xkb_layout\" \""+this.SAI_Layout.Text.Trim()+"\"");
		FileX.WriteLine("Option \"XkbOptions\" \"terminate:ctrl_alt_bksp\"");
		FileX.WriteLine("EndSection");

		FileX.Close();
		
		string graphicalDriver=CBO_GraphicalDevice.Entry.Text;
		FileX = new StreamWriter(@"/etc/X11/xorg.conf.d/20-graphical.conf");
		FileX.WriteLine("Section \"Device\"");
        FileX.WriteLine("Identifier \"Card0\"");
		FileX.WriteLine("Driver \""+graphicalDriver+"\"");
		FileX.WriteLine("EndSection");

		FileX.Close();
		
		FileX = new StreamWriter(cch_FileNumLock);
		FileX.WriteLine("# /etc/sysconfig/numlock");
		FileX.WriteLine("");
		FileX.WriteLine("# Whether Num Lock is turned on or not in console.");
		FileX.WriteLine("# Set 1 if you want to turn Num Lock on, or set 0 if you don't want it.");
		FileX.WriteLine("");
		string str_EnableNum="0";
		if(INT_Numlock.Active) str_EnableNum="1";
		FileX.WriteLine("#NUMLOCK_ON="+str_EnableNum);
		FileX.Close();
		}
		
		catch{}
		}	
		public string GraphicalDevice()
	{
		try
		{
			//search display
			string display = Environment.GetEnvironmentVariable("DISPLAY");
			string []displays=display.Split(':');
			display =displays[1];
			displays=display.Split('.');
			display =displays[0];
			string graphicalDevice="";
			System.IO.StreamReader textFile = new System.IO.StreamReader("/var/log/Xorg."+display+".log");
	        string fileContents = textFile.ReadToEnd();
	        textFile.Close();
	        string[] lines = fileContents.Split('\n');
	        foreach (string line in lines)
	        {
				if (line.IndexOf("/usr/lib/xorg/modules/drivers") > 0)
				{
					string[]ligne= line.Split('/');
					graphicalDevice=ligne[6];
					graphicalDevice=graphicalDevice.Replace("_drv.so","");
				}
				
			}
			return graphicalDevice;
		}
		catch{
			return "";
		}
	}
	
	

	}
}

