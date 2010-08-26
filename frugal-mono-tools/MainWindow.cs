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
	
	//pacman-g2
	// Create a model for treeview pkg
	Gtk.ListStore pkgListStore = new Gtk.ListStore (typeof (string));
	//pacman-g2 initialise
	PacmanG2 Pkg = new PacmanG2();
	ListStore modelRepoList = new ListStore (typeof (string),typeof (int)); 
	
	
	//webkit engine
	private WebKit.WebView webview=null;
	Gtk.ScrolledWindow scroll = new Gtk.ScrolledWindow();
	const int cen_OngHW=2;
	
	
	const string cch_FileLoginManager=@"/etc/sysconfig/desktop";
	const string cch_FileLayoutXorg=@"/etc/X11/xorg.conf.d/10-evdev.conf";
	//http://www.go-mono.com/docs/index.aspx?link=T:Gtk.HTML
	//HTML htl;

	
	//RSS
	const string UrlPlanet="http://planet.frugalware.org/feed.php?type=rss";
	ListStore modelFlux = new ListStore (typeof (string),typeof (int)); 
	RSS FluxRss;
	
	public MainWindow () : base(Gtk.WindowType.Toplevel)
	{
		
		Build ();
		//graphical debug
		if ( Debug.ModeDebug && Debug.ModeDebugGraphique)
		{
			Debug.winDebug = new FEN_Debug(); 
			Debug.winDebug.Show();
		}
		//pacman-g2
		// Create a column for the artist name
		Gtk.TreeViewColumn pkgColumn = new Gtk.TreeViewColumn ();
		pkgColumn.Title = "Package name";
		Gtk.CellRendererText pkgNameCell = new Gtk.CellRendererText ();
		// Add the cell to the column
		pkgColumn.PackStart (pkgNameCell, true);
		treeviewpkg.AppendColumn (pkgColumn);
		pkgColumn.AddAttribute (pkgNameCell, "text", 0);
		
		int i = 0 ;
		foreach (string repo in Pkg.fwRepo)
		{
			modelRepoList.AppendValues(repo,i);
			i++;
		}
		CBO_Repo.Model=modelRepoList;
		
		// Assign the model to the TreeView
		treeviewpkg.Model = pkgListStore;
		
		// Event on treeview
		treeviewpkg.Selection.Changed += OnSelectionEntryPkg;
		
		//webkit engine
		webview = new WebView();
		scroll.Add(webview);
		this.vbox5.Add (this.scroll);
		this.scroll.ShowAll();
		
		//hide notebook not yet implemented
		ONG_principal.RemovePage(cen_OngHW);
		
		//root options
		if (Mono.Unix.Native.Syscall.getuid()!=0)
		{
			BTN_Network.Visible=false;
			BTN_LoginManager.Visible=false;
			BTN_Xorg.Visible=false;
			BTN_Uninstall.Visible=false;
			BTN_Install.Visible=false;
		}
		
		//xorg configuration
		SAI_Layout.Text=this.LayoutXorg();
		
		//network init
		INT_NM.Active=Outils.ServiceOnStartUp("S99rc.networkmanager");
		EnableDisable(INT_NM,"/usr/sbin/NetworkManager",LIB_NMNotInstalled);
		INT_WICD.Active=Outils.ServiceOnStartUp("S99rc.wicd");
		EnableDisable(INT_WICD,"/usr/sbin/wicd",LIB_WICDNotInstalled);
		if((!INT_NM.Active) && (!INT_WICD.Active))
		{
			INT_FW.Active=true;
		}
		else
		{
			INT_FW.Active=false;
		}
		//Login Manager init
		EnableDisable(INT_XDM,"/usr/bin/xdm",LIB_XDM);
		EnableDisable(INT_LXDM,"/usr/sbin/lxdm",LIB_LXDM);
		EnableDisable(INT_Slim,"/usr/bin/slim",LIB_SLIM);
		EnableDisable(INT_GDM,"/usr/sbin/gdm",LIB_GDM);
		EnableDisable(INT_KDM,"/usr/bin/kdm",LIB_KDM);
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

		//RSS
		try{
		CBO_TitleNews.Model=modelFlux;
		FluxRss = new RSS(UrlPlanet);
		i = 0;
		foreach (nodetype n in FluxRss.Nodes)  
       	{  
			
			string titre=n.rss_title;
			modelFlux.AppendValues(titre,i);
			i++;}
			
		}
		catch{}
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
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
		TreeIter iter;
		if ((sender as ComboBox).GetActiveIter (out iter))
		{
			int id =(int)modelFlux.GetValue (iter,1);
			nodetype Node = (nodetype)FluxRss.Nodes[id];
			this.LIB_Titre.LabelProp=Node.rss_pubdate;
			//this.TXT_Description.Buffer.Text=Node.rss_description;
			this.webview.LoadUri(Node.rss_url);
			this.BTN_Link.Label=Node.rss_url;
		}
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
	public void EnableDisable(CheckButton INT_Option,string FileToTest, Label text)
	{
		//check if file existe for works more quickly
		if(!File.Exists(FileToTest))
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
		
	protected virtual void ApplyXorg (object sender, System.EventArgs e)
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
		}
		catch{}
		
	}
	
	protected virtual void OnBTNSearchClicked (object sender, System.EventArgs e)
	{
		try{
			List<Package> packages=Pkg.Search(SAI_pkg.Text,Pkg.repoSelected);
			pkgListStore.Clear();
			foreach (Package package in packages)
			{
				// Add some data to the store
				pkgListStore.AppendValues (package.pkgname);
			}
		}
		catch{}
	}
	
	protected virtual void selectRepo (object sender, System.EventArgs e)
	{
		TreeIter iter;
		if ((sender as ComboBox).GetActiveIter (out iter))
		{
			int id =(int)modelRepoList.GetValue (iter,1);
			Pkg.SelectRepo(Pkg.fwRepo[id]);
		}
	}
	
	
	protected void OnSelectionEntryPkg(object o, EventArgs args)
	    {
	   		try
			{
			 	TreeModel model;
				 if (((TreeSelection)o).GetSelected(out model, out iter))
		        {
		            string T =(string)model.GetValue (iter, 0);
					T=Pkg.extractNamePackage(T);
					Console.WriteLine(T);
				}
			}
			catch{}
		}
	
	
	
	
	
	
	
}

