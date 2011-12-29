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
using Notifications;
using Indicate;

public class IconSummaryBody
{
	enum Capability {
		CAP_ACTIONS = 0,
		CAP_BODY,
		CAP_BODY_HYPERLINKS,
		CAP_BODY_IMAGES,
		CAP_BODY_MARKUP,
		CAP_ICON_MULTI,
		CAP_ICON_STATIC,
		CAP_SOUND,
		CAP_IMAGE_SVG,
		CAP_SYNCHRONOUS,
		CAP_APPEND,
		CAP_LAYOUT_ICON_ONLY,
		CAP_MAX}

	bool[] m_capabilities = {false,  // actions
					false,  // body
					false,  // body-hyperlinks
					false,  // body-imges
					false,  // body-markup
					false,  // icon-multi
					false,  // icon-static
					false,  // sound
					false,  // image/svg+xml
					false,  // synchronous-hint
					false,  // append-hint
					false}; // icon-only-hint

	private void InitCaps ()
	{

		if (Notifications.Global.Capabilities == null)
			return;

		if (Array.IndexOf (Notifications.Global.Capabilities, "actions") > -1)
			m_capabilities[(int) Capability.CAP_ACTIONS] = true;

		if (Array.IndexOf (Notifications.Global.Capabilities, "body") > -1)
			m_capabilities[(int) Capability.CAP_BODY] = true;

		if (Array.IndexOf (Notifications.Global.Capabilities, "body-hyperlinks") > -1)
			m_capabilities[(int) Capability.CAP_BODY_HYPERLINKS] = true;

		if (Array.IndexOf (Notifications.Global.Capabilities, "body-images") > -1)
			m_capabilities[(int) Capability.CAP_BODY_IMAGES] = true;

		if (Array.IndexOf (Notifications.Global.Capabilities, "body-markup") > -1)
			m_capabilities[(int) Capability.CAP_BODY_MARKUP] = true;

		if (Array.IndexOf (Notifications.Global.Capabilities, "icon-multi") > -1)
			m_capabilities[(int) Capability.CAP_ICON_MULTI] = true;

		if (Array.IndexOf (Notifications.Global.Capabilities, "icon-static") > -1)
			m_capabilities[(int) Capability.CAP_ICON_STATIC] = true;

		if (Array.IndexOf (Notifications.Global.Capabilities, "sound") > -1)
			m_capabilities[(int) Capability.CAP_SOUND] = true;

		if (Array.IndexOf (Notifications.Global.Capabilities, "image/svg+xml") > -1)
			m_capabilities[(int) Capability.CAP_IMAGE_SVG] = true;

		if (Array.IndexOf (Notifications.Global.Capabilities, "private-synchronous") > -1)
			m_capabilities[(int) Capability.CAP_SYNCHRONOUS] = true;

		if (Array.IndexOf (Notifications.Global.Capabilities, "append") > -1)
			m_capabilities[(int) Capability.CAP_APPEND] = true;

		if (Array.IndexOf (Notifications.Global.Capabilities, "private-icon-only") > -1)
			m_capabilities[(int) Capability.CAP_LAYOUT_ICON_ONLY] = true;
	}

	private void PrintCaps ()
	{
		Console.WriteLine ("Name:          "
		                   + Notifications.Global.ServerInformation.Name);
		Console.WriteLine ("Vendor:        "
		                   + Notifications.Global.ServerInformation.Vendor);
		Console.WriteLine ("Version:       "
		                   + Notifications.Global.ServerInformation.Version);
		Console.WriteLine ("Spec. Version: "
		                   + Notifications.Global.ServerInformation.SpecVersion);

		Console.WriteLine ("Supported capabilities/hints:");
		if (m_capabilities[(int) Capability.CAP_ACTIONS])
			Console.WriteLine ("\tactions");
		if (m_capabilities[(int) Capability.CAP_BODY])
			Console.WriteLine ("\tbody");
		if (m_capabilities[(int) Capability.CAP_BODY_HYPERLINKS])
			Console.WriteLine ("\tbody-hyperlinks");
		if (m_capabilities[(int) Capability.CAP_BODY_IMAGES])
			Console.WriteLine ("\tbody-images");
		if (m_capabilities[(int) Capability.CAP_BODY_MARKUP])
			Console.WriteLine ("\tbody-markup");
		if (m_capabilities[(int) Capability.CAP_ICON_MULTI])
			Console.WriteLine ("\ticon-multi");
		if (m_capabilities[(int) Capability.CAP_ICON_STATIC])
			Console.WriteLine ("\ticon-static");
		if (m_capabilities[(int) Capability.CAP_SOUND])
			Console.WriteLine ("\tsound");
		if (m_capabilities[(int) Capability.CAP_IMAGE_SVG])
			Console.WriteLine ("\timage/svg+xml");
		if (m_capabilities[(int) Capability.CAP_SYNCHRONOUS])
			Console.WriteLine ("\tprivate-synchronous");
		if (m_capabilities[(int) Capability.CAP_APPEND])
			Console.WriteLine ("\tappend");
		if (m_capabilities[(int) Capability.CAP_LAYOUT_ICON_ONLY])
			Console.WriteLine ("\tprivate-icon-only");

		Console.WriteLine ("Notes:");
		if (Notifications.Global.ServerInformation.Name == "notify-osd")
		{
			Console.WriteLine ("\tx- and y-coordinates hints are ignored");
			Console.WriteLine ("\texpire-timeout is ignored");
			Console.WriteLine ("\tbody-markup is accepted but filtered");			
		}
		else
			Console.WriteLine ("\tnone");
	}

	
	public IconSummaryBody()
	{
		try{
		// call this so we can savely use the m_capabilities array later
		InitCaps ();
		
		// show what's supported
		PrintCaps ();
		}
		catch{}
	}
	
	public void ShowMessage (string title,string message)
	{
		try
		{
			//Server
			Indicate.Server server = Indicate.Server.RefDefault();
			server.SetType("message.im");
			server.DesktopFile("/usr/share/applications/frugalware-tweak.desktop");
			server.ServerDisplay += new Indicate.ServerDisplayHandler(ServerDisplay);
			server.Show();
			//indicate
			Indicate.Indicator indicator = new Indicate.Indicator();
			indicator.SetProperty("subtype", "im");
			indicator.SetProperty("sender", title);
			indicator.SetProperty("body", message);
			indicator.UserDisplay += new EventHandler(UserDisplay);
			indicator.Show();
	
		}
		catch{}
		try{
		Notification n = new Notification(title,message,
		                                   "notification-message-IM");//TODO : use an icon
		n.Show ();
		}
		catch{}
	}
	
	public static void ServerDisplay (object sender, Indicate.ServerDisplayArgs args)
	{
	      Console.WriteLine ("Server was displayed");
	}
            
	public static void UserDisplay (object sender, System.EventArgs args)
	{
	      Console.WriteLine ("Indicator was displayed");
	      Indicate.Indicator indicator = sender as Indicate.Indicator;
	      indicator.Hide();
	}
	
	public void ShowMessage (string title,string message,Gdk.Pixbuf image )
	{
		try{
		Notification n = new Notification(title,message,
		                                  image);
		n.Show ();
		}
		catch{}
	}
}
