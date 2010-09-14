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
	public partial class WID_LoginManager : Gtk.Bin
	{
		const string cch_FileLoginManager=@"/etc/sysconfig/desktop";
		public WID_LoginManager ()
		{
			this.Build ();
			_initLoginManager();
			if(!MainClass.boRoot)
				BTN_LoginManager.Visible=false;
		}
		private void _initLoginManager()
		{
		//Login Manager init
		EnableDisable(INT_XDM,"xdm",LIB_XDM);
		EnableDisable(INT_LXDM,"lxdm",LIB_LXDM);
		EnableDisable(INT_Slim,"slim",LIB_SLIM);
		EnableDisable(INT_GDM,"gdm",LIB_GDM);
		EnableDisable(INT_KDM,"kdm",LIB_KDM);
		 try
            {
                System.IO.StreamReader textFile = new System.IO.StreamReader(cch_FileLoginManager);
                string fileContents = textFile.ReadToEnd();
                textFile.Close();
				fileContents = fileContents.Replace("\n\n", "\n");
                string[] lines = fileContents.Split('\n');
                foreach (string line in lines)
                {
                    if (line.Substring(0, 1) != "#")
                    {
                        //We can search a login manager
                        if (line.IndexOf("/usr/bin/xdm") > 0)
                        {
                            //use xdm
							this.INT_XDM.Active=true;
                        }
                        if (line.IndexOf("/usr/sbin/lxdm") > 0)
                        {
                            //use lxdm
							this.INT_LXDM.Active=true;
                        }
                        if (line.IndexOf("/usr/bin/slim") > 0)
                        {
                            //use Slim
							this.INT_Slim.Active=true;
                        }
                        if (line.IndexOf("/usr/sbin/gdm") > 0)
                        {
                            //use GDM
							this.INT_GDM.Active=true;
                        }
                        if (line.IndexOf("/usr/bin/kdm") > 0)
                        {
                            //use KDM
							this.INT_KDM.Active=true;
                        }

                    }
                }
            }
            catch { }
		}
		
		protected virtual void OnBTNLoginManagerClicked (object sender, System.EventArgs e)
		{
			try
		{
			//enregistrement configuration login manager
			
			/*
			# /etc/sysconfig/desktop
			# Which session manager try to use.
			
			#desktop="/usr/bin/xdm -nodaemon"
			#desktop="/usr/sbin/lxdm"
			#desktop="/usr/bin/slim"
			#desktop="/usr/sbin/gdm --nodaemon"
			#desktop="/usr/bin/kdm -nodaemon"
			*/
			StreamWriter FileLogin = new StreamWriter(cch_FileLoginManager);
			FileLogin.WriteLine("# /etc/sysconfig/desktop");
			FileLogin.WriteLine("# Which session manager try to use.");
			FileLogin.WriteLine("");
			if (this.INT_XDM.Active)
			{
				FileLogin.WriteLine("desktop=\"/usr/bin/xdm -nodaemon\"");
			}
			else
			{
				FileLogin.WriteLine("#desktop=\"/usr/bin/xdm -nodaemon\"");
			}
			if (this.INT_LXDM.Active)
			{
				FileLogin.WriteLine("desktop=\"/usr/sbin/lxdm\"");
			}
			else
			{
				FileLogin.WriteLine("#desktop=\"/usr/sbin/lxdm\"");
			}
			if (this.INT_Slim.Active)
			{
				FileLogin.WriteLine("desktop=\"/usr/bin/slim\"");
			}
			else
			{
				FileLogin.WriteLine("#desktop=\"/usr/bin/slim\"");
			}
			if (this.INT_GDM.Active)
			{
				FileLogin.WriteLine("desktop=\"/usr/sbin/gdm -nodaemon\"");
			}
			else
			{
				FileLogin.WriteLine("#desktop=\"/usr/sbin/gdm -nodaemon\"");
			}
			if (this.INT_KDM.Active)
			{
				FileLogin.WriteLine("desktop=\"/usr/bin/kdm -nodaemon\"");
			}
			else
			{
				FileLogin.WriteLine("#desktop=\"/usr/bin/kdm -nodaemon\"");
			}
			FileLogin.Close();
		}
		
		catch{}
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
		
		
		
		
		
		
	}
}

