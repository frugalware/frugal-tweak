/*
 *
 * (C) 2010 bouleetbil <bouleetbil@frogdev.info>
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301  USA
 */

namespace fwtweak {

	#if ENABLEINDICATE
		using Indicate;
	#endif
	using Notify;
	using Tools;
	using pacman;
	public static class Popup  {
	
		public static void PopupShow(string title,string text) {
			Configuration conf= new Configuration();
			if(!conf.GetShowNotif()) return ;

			Notify.init("Frugalware-tweak");
			Notification notification = new Notification (title,text, "icon_name");
			notification.set_timeout(5000);
			notification.set_urgency(Notify.Urgency.LOW);
			try
			{
				notification.show();
				ConsoleDebug(text);
			}
			catch
			{
				ConsoleDebug("Unable to show low notification");
			}
		
			#if ENABLEINDICATE
				//indicator support
				/*var server = Indicate.Server.ref_default();

				server.set_type("message.frugalware-tweak2");
				server.set_desktop_file("/usr/share/indicators/messages/applications/frugalware-tweak2.desktop");
				server.server_display.connect(dirty_activate);
				server.show();*/

				var indicator = new Indicate.Indicator();
				indicator.set_property("subtype", "im");
				indicator.set_property("sender", "frugalware-tweak2");
				indicator.set_property("body", text);
				indicator.user_display.connect(dirty_activate);
				indicator.show();
			#endif

			}
	
			public static void dirty_activate() {
				ConsoleDebug("libindicate activated");
			}
	}	
}

