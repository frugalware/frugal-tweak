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
using Rss;
using WebKit;

namespace frugalmonotools
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class WID_News : Gtk.Bin
	{
		private WebKit.WebView webview=null;
		Gtk.ScrolledWindow scroll = new Gtk.ScrolledWindow();
	
		//http://www.go-mono.com/docs/index.aspx?link=T:Gtk.HTML
		//HTML htl;

		//RSS
		const string UrlPlanet="http://planet.frugalware.org/feed.php?type=rss";
		ListStore modelFlux = new ListStore (typeof (string),typeof (int)); 
		RssFeed rssFeed ;
		//RSS FluxRss;
	
		public WID_News ()
		{
			this.Build ();
			_initnews();
		}
		private void _initnews()
		{
			//webkit engine
			this.webview = new WebView();
			this.webview.LoadUri("http://www.frugalware.org");
			scroll.Add(webview);
			this.vbox5.Add (this.scroll);
			this.scroll.ShowAll();
			
		//RSS
		try{
			CBO_TitleNews.Model=modelFlux;
			rssFeed =RssFeed.Read(UrlPlanet);
			RssChannel rssChannel = (RssChannel)rssFeed.Channels[0];

			int i = 0;
			string latest="";
			foreach (RssItem item in rssChannel.Items)
			{
				string titre=item.Title;
				modelFlux.AppendValues(titre,i);
				if(latest =="")latest=item.Link.AbsoluteUri.ToString();
				i++;
			}
			InformNewFlux(latest);
		}
		catch{}	
		}
		
		private void InformNewFlux(string latest)
		{
			if (MainClass.cache.GetLatest()!=latest)
			{
				Outils.Inform("Frugalware","News are available.");
				//write cache	
				MainClass.cache.SetLatest(latest);
				MainClass.cache.CacheSave();
			}
		}

		protected virtual void OnCBOTitleNewsChanged (object sender, System.EventArgs e)
		{
			TreeIter iter;
			if ((sender as ComboBox).GetActiveIter (out iter))
			{
				int id =(int)modelFlux.GetValue (iter,1);
				
				RssChannel rssChannel = (RssChannel)rssFeed.Channels[0];
				RssItem item = rssChannel.Items[id];
				this.webview.LoadUri(item.Link.AbsoluteUri.ToString());
				this.BTN_Link.Label=item.Link.AbsoluteUri.ToString();
			}
		}
		
		protected virtual void OnBTNLinkClicked (object sender, System.EventArgs e)
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
		
		
		
	}
}

