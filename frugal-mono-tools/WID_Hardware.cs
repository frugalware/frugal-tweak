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
using System.Net;
using System.Web;
using System.IO;
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
		BTN_Pastbin.Visible=false;
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
		
		
		
		protected void OnBTNPastbinClicked (object sender, System.EventArgs e)
		{
			try
			{
			 
			    HttpWebRequest request = (HttpWebRequest)
			    WebRequest.Create("http://www.frugalware.org/paste/");
			 
			    request.AllowAutoRedirect = false;
			    request.Method = "POST";
			 
			    string post = "&amp;parent_pid=&amp;format=text&amp;code2=" + HttpUtility.UrlEncode(TXT_Lspci.Buffer.Text) + "&amp;poster=FrugalTweak&amp;paste=Send&amp;expiry=m&amp;email=";
			    byte[] data = System.Text.Encoding.ASCII.GetBytes(post);
			 
			    request.ContentType = "application/x-www-form-urlencoded";
			    request.ContentLength = data.Length;
			 
			    Stream response = request.GetRequestStream();
			 
			    response.Write(data,0,data.Length);
			 
			    response.Close();
			 
			    HttpWebResponse res =(HttpWebResponse) request.GetResponse();
			    res.Close();
			    // note that there is no need to hook up a StreamReader and
			    // look at the response data, since it is of no need
			 
			    if (res.StatusCode == HttpStatusCode.Found)
			    {
			        Console.WriteLine(res.Headers["location"]);
			    }
			    else
			    {
			        Console.WriteLine("Error");
			    }
			 
			}
			catch (Exception ex)
			{
			    Console.WriteLine("Error: " + ex.Message);
			
			}
		}

		protected void OnBTNPastbin1Clicked (object sender, System.EventArgs e)
		{
			Outils.OpenUrl("http://www.frugalware.org/paste/");
		}
	}
}

