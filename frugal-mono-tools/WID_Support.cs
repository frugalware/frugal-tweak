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
	public partial class WID_Support : Gtk.Bin
	{
		private void _joinIrc(string channel)
		{
			Outils.Excecute("mono","/usr/lib/frugalware-tweak/frugal-irc.exe "+channel,false);		
		}
	
		protected virtual void OnBTNIrcClicked (object sender, System.EventArgs e)
		{
			_joinIrc("frugalware");
		}
		
		
		public WID_Support ()
		{
			this.Build ();
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
		
		protected virtual void OnBTNIrc1Clicked (object sender, System.EventArgs e)
		{
			_joinIrc("frugalware.fr");
		}
		
		protected virtual void OnBTNIrc2Clicked (object sender, System.EventArgs e)
		{
			_joinIrc("frugalware.hu");
		}
		
		
		
		
		
		
		
		
		
		
	}
}

