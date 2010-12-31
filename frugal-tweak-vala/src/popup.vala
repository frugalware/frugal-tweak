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

using Notify;
using Tools;

public static class Popup  {
	
	public static void PopupShow(string title,string text) {
		Notify.init("Frugalware-tweak");
		var notification = new Notification (title,text, "icon_name", null);
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
	}
}	

