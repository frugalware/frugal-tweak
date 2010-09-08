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
using System.Timers;
using System.Net;
using System.IO;
using Gtk;
using WebKit;
using System.Collections.Generic;
using Rss;
using frugalmonotools;

public partial class MainWindow : Gtk.Window
{
	protected Gtk.TreeIter iter;
	private string packageSelected="";
	private string UpdateSelected="";
	private string ServiceSelected="";
	private ConfSystem  confSystem = new ConfSystem();
	
	private bool boRoot = false;
	//pacman-g2
	// Create a model for treeview pkg
	ListStore pkgListStore = new Gtk.ListStore (typeof (string),typeof (string),typeof(string));
	ListStore UpdateListStore = new Gtk.ListStore (typeof (string));
	ListStore modelRepoList = new ListStore (typeof (string),typeof (int)); 
	ListStore serviceListStore = new Gtk.ListStore (typeof (string),typeof (string),typeof (string),typeof (string));
	//webkit engine
	private WebKit.WebView webview=null;
	Gtk.ScrolledWindow scroll = new Gtk.ScrolledWindow();
	
	const string cch_FileLoginManager=@"/etc/sysconfig/desktop";
	const string cch_FileNumLock=@"/etc/sysconfig/numlock";
	const string cch_FileLayoutXorg=@"/etc/X11/xorg.conf.d/10-evdev.conf";
	//http://www.go-mono.com/docs/index.aspx?link=T:Gtk.HTML
	//HTML htl;

	//RSS
	const string UrlPlanet="http://planet.frugalware.org/feed.php?type=rss";
	ListStore modelFlux = new ListStore (typeof (string),typeof (int)); 
	RssFeed rssFeed ;
	//RSS FluxRss;
		
	public MainWindow () : base(Gtk.WindowType.Toplevel)
	{
		this.SetDefaultSize (700, 500);
		Build ();
		
		System.Timers.Timer aTimer = new System.Timers.Timer();
		aTimer.Elapsed+=new ElapsedEventHandler(checkRSS);
		// Set the Interval to 1 hour.
		aTimer.Interval=3600000;
		aTimer.Enabled=true;
		//graphical debug
		if ( Debug.ModeDebug && Debug.ModeDebugGraphique)
		{
			Debug.winDebug = new FEN_Debug(); 
			Debug.winDebug.Show();
		}
		// services
		Gtk.TreeViewColumn ColumnServiceName = new Gtk.TreeViewColumn ();
		ColumnServiceName.Title = "Services";
		Gtk.CellRendererText ServiceNameCell = new Gtk.CellRendererText ();
		// Add the cell to the column
		ColumnServiceName.PackStart (ServiceNameCell, true);
		TREE_Services.AppendColumn (ColumnServiceName);
		ColumnServiceName.AddAttribute (ServiceNameCell, "text", 0);
		
		Gtk.TreeViewColumn ColumnServiceStarted = new Gtk.TreeViewColumn ();
		ColumnServiceStarted.Title = "Started";
		Gtk.CellRendererText ServiceStartedCell = new Gtk.CellRendererText ();
		// Add the cell to the column
		ColumnServiceStarted.PackStart (ServiceStartedCell, true);
		TREE_Services.AppendColumn (ColumnServiceStarted);
		ColumnServiceStarted.AddAttribute (ServiceStartedCell, "text", 1);
		
		Gtk.TreeViewColumn ColumnServiceOnBoot = new Gtk.TreeViewColumn ();
		ColumnServiceOnBoot.Title = "Start on boot";
		Gtk.CellRendererText ServiceOnBootCell = new Gtk.CellRendererText ();
		// Add the cell to the column
		ColumnServiceOnBoot.PackStart (ServiceOnBootCell, true);
		TREE_Services.AppendColumn (ColumnServiceOnBoot);
		ColumnServiceOnBoot.AddAttribute (ServiceOnBootCell, "text", 2);
		
		Gtk.TreeViewColumn ColumnServiceDesc = new Gtk.TreeViewColumn ();
		ColumnServiceDesc.Title = "Description";
		Gtk.CellRendererText ServiceDescCell = new Gtk.CellRendererText ();
		// Add the cell to the column
		ColumnServiceDesc.PackStart (ServiceDescCell, true);
		TREE_Services.AppendColumn (ColumnServiceDesc);
		ColumnServiceDesc.AddAttribute (ServiceDescCell, "text", 3);
	
		foreach(Service service in ServicesRc.Services)
		{
			string Etat = "yes";
			if (!service.IsStarted()) Etat="No";
			string OnBoot = "yes";
			if (!service.IsStartedOnBoot()) OnBoot = "No";
			serviceListStore.AppendValues(service.Get_Name(),Etat,OnBoot,service.GetDescription());             
		}
		TREE_Services.Model=serviceListStore;
		// Event on treeview
		TREE_Services.Selection.Changed += OnSelectionEntryService;
		
		//update package list
		// Create a column for the package name
		Gtk.TreeViewColumn pkgupdateColumn = new Gtk.TreeViewColumn ();
		pkgupdateColumn.Title = "Package name";
		Gtk.CellRendererText pkgupdateNameCell = new Gtk.CellRendererText ();
		// Add the cell to the column
		pkgupdateColumn.PackStart (pkgupdateNameCell, true);
		TREE_UpdatePkg.AppendColumn (pkgupdateColumn);
		pkgupdateColumn.AddAttribute (pkgupdateNameCell, "text", 0);
		// Event on treeview
		TREE_UpdatePkg.Selection.Changed += OnSelectionEntryUpdate;
		TREE_UpdatePkg.Model=UpdateListStore;
		//pacman-g2
		// Create a column for the package name
		Gtk.TreeViewColumn pkgColumn = new Gtk.TreeViewColumn ();
		pkgColumn.Title = "Package name";
		Gtk.CellRendererText pkgNameCell = new Gtk.CellRendererText ();
		// Add the cell to the column
		pkgColumn.PackStart (pkgNameCell, true);
		treeviewpkg.AppendColumn (pkgColumn);
		pkgColumn.AddAttribute (pkgNameCell, "text", 0);

		// Create a column for the package group
		Gtk.TreeViewColumn pkgGroupColumn = new Gtk.TreeViewColumn ();
		pkgGroupColumn.Title = "Group";
		Gtk.CellRendererText pkgGroupCell = new Gtk.CellRendererText ();
		// Add the cell to the column
		pkgGroupColumn.PackStart (pkgGroupCell, true);
		treeviewpkg.AppendColumn (pkgGroupColumn);
		pkgGroupColumn.AddAttribute (pkgGroupCell, "text", 1);
		
		Gtk.TreeViewColumn ColumnPkgDesc = new Gtk.TreeViewColumn ();
		ColumnPkgDesc.Title = "Description";
		Gtk.CellRendererText PkgDescCell = new Gtk.CellRendererText ();
		// Add the cell to the column
		ColumnPkgDesc.PackStart (PkgDescCell, true);
		treeviewpkg.AppendColumn (ColumnPkgDesc);
		ColumnPkgDesc.AddAttribute (PkgDescCell, "text", 2);
		
		int i = 0 ;
		TreeIter iter =new TreeIter();
		foreach (string repo in  MainClass.pacmanG2.fwRepo)
		{
			string strRepo=repo;
			if (strRepo=="local") strRepo ="Installed";
			iter = modelRepoList.AppendValues(strRepo,i);
			i++;
		}
		CBO_Repo.Model=modelRepoList;
		CBO_Repo.SetActiveIter(iter); 
		// Assign the model to the TreeView
		treeviewpkg.Model = pkgListStore;
		
		// Event on treeview
		treeviewpkg.Selection.Changed += OnSelectionEntryPkg;
				
		//webkit engine
		this.webview = new WebView();
		this.webview.LoadUri("http://www.frugalware.org");
		scroll.Add(webview);
		this.vbox5.Add (this.scroll);
		this.scroll.ShowAll();
		
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
		}
		else
		{
			boRoot=true;
		}
		
		//xorg configuration
		SAI_Layout.Text=this.LayoutXorg();
		
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
		LIB_XorgGraphic.Text+= GraphicalDevice()+" driver";
		INT_Numlock.Active=IsNumlockOnStartX();
		if ((dmesgOutput.IndexOf("TouchPad")>0) && (!MainClass.pacmanG2.IsInstalled("xf86-input-synaptics")))
			BTN_Synaptics.Visible=true;
		else
			BTN_Synaptics.Visible=false;
		
		//network init
		INT_NM.Active=Outils.ServiceOnStartUp("S99rc.networkmanager");
		EnableDisable(INT_NM,"networkmanager",LIB_NMNotInstalled);
		INT_WICD.Active=Outils.ServiceOnStartUp("S99rc.wicd");
		EnableDisable(INT_WICD,"wicd",LIB_WICDNotInstalled);
		if((!INT_NM.Active) && (!INT_WICD.Active))
		{
			INT_FW.Active=true;
		}
		else
		{
			INT_FW.Active=false;
		}
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

		//RSS
		try{
			CBO_TitleNews.Model=modelFlux;
			rssFeed =RssFeed.Read(UrlPlanet);
			RssChannel rssChannel = (RssChannel)rssFeed.Channels[0];

			i = 0;
			foreach (RssItem item in rssChannel.Items)
			{
				string titre=item.Title;
				modelFlux.AppendValues(titre,i);
				i++;
			}
		}
		catch{}
		
		//system configuration
		SAI_Host.Text=confSystem.GetHostname();
		SAI_Distribution.Text=confSystem.GetDistribution();
		SAI_Kernel.Text=confSystem.GetKernel();
		SAI_Shell.Text=confSystem.GetUserShell();
		SAI_Locale.Text=confSystem.GetLocale();
		
		//configuration
		INT_CheckStartup.Active=MainClass.configuration.Get_CheckUpdate();
		INT_StartWithXSession.Active=MainClass.configuration.Get_StartWithX();
		INT_ShowNotif.Active=MainClass.configuration.Get_ShowNotif();
		INT_ShowSplash.Active=MainClass.configuration.Get_ShowSplash();
		
		//update
		UpdateToTreeview();
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
	private void checkRSS(object source, ElapsedEventArgs e)
	{
		//RSS
		try{
			int count = modelFlux.Data.Count;
			modelFlux.Clear();
			rssFeed =RssFeed.Read(UrlPlanet);
			RssChannel rssChannel = (RssChannel)rssFeed.Channels[0];

			int i = 0;
			foreach (RssItem item in rssChannel.Items)
			{
				string titre=item.Title;
				modelFlux.AppendValues(titre,i);
				i++;
			}
		
			if(modelFlux.Data.Count!=count)
			{
				if(MainClass.configuration.Get_ShowNotif()) 
				{
					IconSummaryBody notif= new IconSummaryBody();		
					notif.ShowMessage("Frugalware","New RSS entry");
				}
			}
		}
		catch{}		
	}
	protected virtual void SelectItem (object sender, System.EventArgs e)
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
	
	protected virtual void OnBTNSearchClicked (object sender, System.EventArgs e)
	{
		_searchPackage();
	}
	private void _searchPackage(){
	try{
			List<Package> packages=MainClass.pacmanG2.Search(SAI_pkg.Text,MainClass.pacmanG2.repoSelected,true);
			pkgListStore.Clear();
			foreach (Package package in packages)
			{
				// Add some data to the store
				pkgListStore.AppendValues (package.GetPkgname()+"-"+package.GetPkgversion(),package.GetGroup()
				                           	,package.GetDescription());
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
			MainClass.pacmanG2.SelectRepo(MainClass.pacmanG2.fwRepo[id]);
		}
	}
	
	
	protected void OnSelectionEntryUpdate(object o, EventArgs args)
	    {
	   		try
			{
			 	TreeModel model;
				 if (((TreeSelection)o).GetSelected(out model, out iter))
		        {
		            string T =(string)model.GetValue (iter, 0);
					UpdateSelected=T;
					UpdateSelected=MainClass.pacmanG2.extractNamePackage(UpdateSelected);
					if(boRoot)
					{
						BTN_Hide.Visible=true;
					}
				}
			}
			catch{}
		}
	protected void OnSelectionEntryPkg (object o, EventArgs args)
	    {
	   		try
			{
			 	TreeModel model;
				 if (((TreeSelection)o).GetSelected(out model, out iter))
		        {
		            string T =(string)model.GetValue (iter, 0);
					T=MainClass.pacmanG2.extractNamePackage(T);
					packageSelected=T;

					if(MainClass.pacmanG2.IsInstalled(T))
					{
						BTN_Uninstall.Visible=true;
						BTN_Install.Visible=false;
					}
					else
					{
						BTN_Uninstall.Visible=false;
						BTN_Install.Visible=true;
					}
					
				}
			}
			catch{}
		}
	protected void OnSelectionEntryService(object o, EventArgs args)
	    {
	   		try
			{
				
			 	TreeModel model;
				 if (((TreeSelection)o).GetSelected(out model, out iter))
		        {
		            string T =(string)model.GetValue (iter, 0);
					ServiceSelected=T;
					if(boRoot)
					{
						BTN_ServiceStop.Visible=false;
						BTN_ServiceStart.Visible=false;
						BTN_ServiceDelBoot.Visible=false;
						BTN_ServiceAddBoot.Visible=false;
						Service service = new Service(T);
						if (service.IsStarted())
							BTN_ServiceStop.Visible=true;
						else
							BTN_ServiceStart.Visible=true;
						if(service.IsStartedOnBoot())
							BTN_ServiceDelBoot.Visible=true;
						else
							BTN_ServiceAddBoot.Visible=true;
						
					}
				}
			}
			catch{}
		}
	
	protected virtual void OnBTNUninstallClicked (object sender, System.EventArgs e)
	{
		if(packageSelected=="") return;
		if(boRoot)
			Outils.Excecute("python","/usr/bin/PyFrugalVTE pacman-g2 -Rc "+packageSelected,true);	
		else
			Outils.ExcecuteAsRoot("python /usr/bin/PyFrugalVTE pacman-g2 -Rc "+packageSelected,true);	
		_searchPackage();
	}
	
	protected virtual void OnBTNInstallClicked (object sender, System.EventArgs e)
	{
		if(packageSelected=="") return;
		if(boRoot)
			Outils.Excecute("python","/usr/bin/PyFrugalVTE pacman-g2 -Sy "+packageSelected,true);
		else
			Outils.ExcecuteAsRoot("python /usr/bin/PyFrugalVTE pacman-g2 -Sy "+packageSelected,true);	
		_searchPackage();
	}
	
	protected virtual void OnBTNUpdateClicked (object sender, System.EventArgs e)
	{
		if(boRoot)
			Outils.Excecute("python","/usr/bin/PyFrugalVTE pacman-g2 -Syu",false);	
		else
			Outils.ExcecuteAsRoot("python /usr/bin/PyFrugalVTE pacman-g2 -Syu",false);	
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
	private void _serviceRefresh()
	{
		serviceListStore.Clear();
		foreach(Service service in ServicesRc.Services)
		{
			string Etat = "yes";
			if (!service.IsStarted()) Etat="No";
			string OnBoot = "yes";
			if (!service.IsStartedOnBoot()) OnBoot = "No";
			serviceListStore.AppendValues(service.Get_Name(),Etat,OnBoot,service.GetDescription());             
		}
	}
	protected virtual void OnBTNServiceStartClicked (object sender, System.EventArgs e)
	{
		if(ServiceSelected!="")
		{
			Service service = new Service(ServiceSelected);
			service.Start();
			_serviceRefresh();
		}
	}
	
	protected virtual void OnBTNServiceStopClicked (object sender, System.EventArgs e)
	{
		if(ServiceSelected!="")
		{
			Service service = new Service(ServiceSelected);
			service.Stop();
			_serviceRefresh();
		}
	}
	
	protected virtual void OnBTNServiceDelBootClicked (object sender, System.EventArgs e)
	{
		if(ServiceSelected!="")
		{
			Service service = new Service(ServiceSelected);
			service.EnableDisableOnBoot(false);
			_serviceRefresh();
		}
	}
	
	protected virtual void OnBTNServiceAddBootClicked (object sender, System.EventArgs e)
	{
		if(ServiceSelected!="")
		{
			Service service = new Service(ServiceSelected);
			service.EnableDisableOnBoot(true);
			_serviceRefresh();
		}
	}
	
	protected virtual void OnBTNIrcClicked (object sender, System.EventArgs e)
	{
			_joinIrc("frugalware");
	}
	private void _joinIrc(string channel){
		Outils.Excecute("python","/usr/bin/PyFrugalVTE python /usr/bin/PyFrugalIRC " +channel,false);		
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
		Update.CheckUpdate();
		UpdateToTreeview();
	}
	public void UpdateToTreeview()
	{
		UpdateListStore.Clear();
		foreach (packageCheck package in Update.UpdatePkg)
		{
			// Add some data to the store
			UpdateListStore.AppendValues (package.packagename+"-"+package.packageversion);
		}
	}
	protected virtual void OnBTNUpdateDatabaseClicked (object sender, System.EventArgs e)
	{
		Outils.Excecute("python","/usr/bin/PyFrugalVTE pacman-g2 -Sy "+packageSelected,false);	
	}
	
	protected virtual void OnBTNHideClicked (object sender, System.EventArgs e)
	{
		try
		{
			if(UpdateSelected=="") return;
			string pacmanConf = @"/etc/pacman-g2.conf";
			string strPacmanConf =Outils.ReadFile(pacmanConf);
			string[] lines = strPacmanConf.Split('\n');	
			string[] result= new string[lines.Length];
			string lineResult;
			int i = 0;
				foreach (string line in lines) 
	            {
					lineResult=line;
					if (System.Text.RegularExpressions.Regex.Matches(line, "IgnorePkg").Count>0)
					{
						//find it :p
						lineResult=lineResult.Replace("=","= "+UpdateSelected+" ");
						lineResult=lineResult.Replace("#","");
						MainClass.pacmanG2.ignorePkg.Add(UpdateSelected);
					}
					result[i]=lineResult;
					i++;
				}
			StreamWriter File = new StreamWriter(pacmanConf);
			foreach (string line in result) 
	        {
				File.WriteLine(line);
			}
			File.Close();
		}
		catch(Exception exe)
		{
			Console.WriteLine("Can't update pacman-g2.conf");
			Console.WriteLine(exe.Message);
		}
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
		confSystem.SetHostname(SAI_Host.Text);
		confSystem.SetLocale(SAI_Locale.Text);
		confSystem.Save();
	}
	
	protected virtual void OnButton2Clicked (object sender, System.EventArgs e)
	{
		MainClass.DbusCom.Hello("test");
	}
	
	
	
	
	
}

