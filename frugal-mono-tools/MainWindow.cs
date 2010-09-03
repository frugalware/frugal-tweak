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
	private string packageSelected="";
	private string ServiceSelected="";
	
	private bool boRoot = false;
	//pacman-g2
	// Create a model for treeview pkg
	ListStore pkgListStore = new Gtk.ListStore (typeof (string));
	ListStore modelRepoList = new ListStore (typeof (string),typeof (int)); 
	ListStore serviceListStore = new Gtk.ListStore (typeof (string),typeof (string),typeof (string));
	//webkit engine
	private WebKit.WebView webview=null;
	Gtk.ScrolledWindow scroll = new Gtk.ScrolledWindow();
	
	
	
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
		this.SetDefaultSize (700, 500);
		Build ();
		
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
	
		foreach(Service service in ServicesRc.Services)
		{
			string Etat = "yes";
			if (!service.IsStarted()) Etat="No";
			string OnBoot = "yes";
			if (!service.IsStartedOnBoot()) OnBoot = "No";
			serviceListStore.AppendValues(service.Get_Name(),Etat,OnBoot);             
		}
		TREE_Services.Model=serviceListStore;
		// Event on treeview
		TREE_Services.Selection.Changed += OnSelectionEntryService;
		
		//pacman-g2
		// Create a column for the package name
		Gtk.TreeViewColumn pkgColumn = new Gtk.TreeViewColumn ();
		pkgColumn.Title = "Package name";
		Gtk.CellRendererText pkgNameCell = new Gtk.CellRendererText ();
		// Add the cell to the column
		pkgColumn.PackStart (pkgNameCell, true);
		treeviewpkg.AppendColumn (pkgColumn);
		pkgColumn.AddAttribute (pkgNameCell, "text", 0);
		
		int i = 0 ;
		foreach (string repo in  MainClass.pacmanG2.fwRepo)
		{
			string strRepo=repo;
			if (strRepo=="local") strRepo ="Installed";
			modelRepoList.AppendValues(strRepo,i);
			i++;
		}
		CBO_Repo.Model=modelRepoList;
		
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
		string dmesgOutput=Outils.getoutput("dmesg");
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
			BTN_Update.Visible=false;
			BTN_Setup.Visible = false;
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
		string touchpad=Outils.getoutput("dmesg");
		if ((touchpad.IndexOf("TouchPad")>0) && (!MainClass.pacmanG2.IsInstalled("xf86-input-synaptics")))
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
		FluxRss = new RSS(UrlPlanet);
		i = 0;
		foreach (nodetype n in FluxRss.Nodes)  
       		{  
			
			string titre=n.rss_title;
			modelFlux.AppendValues(titre,i);
			i++;
			}
			
			
		}
		catch{}
		
		//configuration
		INT_CheckStartup.Active=MainClass.configuration.Get_CheckUpdate();
		INT_StartWithXSession.Active=MainClass.configuration.Get_StartWithX();
		INT_ShowNotif.Active=MainClass.configuration.Get_ShowNotif();
		INT_ShowSplash.Active=MainClass.configuration.Get_ShowSplash();
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
		}
		catch{}
		
	}
	
	protected virtual void OnBTNSearchClicked (object sender, System.EventArgs e)
	{
		try{
			List<Package> packages=MainClass.pacmanG2.Search(SAI_pkg.Text,MainClass.pacmanG2.repoSelected,true);
			pkgListStore.Clear();
			foreach (Package package in packages)
			{
				// Add some data to the store
				pkgListStore.AppendValues (package.pkgname+"-"+package.pkgversion);
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
	
	
	protected void OnSelectionEntryPkg(object o, EventArgs args)
	    {
	   		try
			{
			 	TreeModel model;
				 if (((TreeSelection)o).GetSelected(out model, out iter))
		        {
		            string T =(string)model.GetValue (iter, 0);
					LIB_Descr.Text=PacmanG2.SearchDescription(T,MainClass.pacmanG2.repoSelected);
					T=MainClass.pacmanG2.extractNamePackage(T);
					packageSelected=T;
					if(boRoot)
					{
						//installed ?
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
		Outils.Excecute("python","/usr/bin/PyFrugalVTE pacman-g2 -Rc "+packageSelected,false);	
	}
	
	protected virtual void OnBTNInstallClicked (object sender, System.EventArgs e)
	{
		if(packageSelected=="") return;
		Outils.Excecute("python","/usr/bin/PyFrugalVTE pacman-g2 -Sy "+packageSelected,false);	
	}
	
	protected virtual void OnBTNUpdateClicked (object sender, System.EventArgs e)
	{
		Outils.Excecute("python","/usr/bin/PyFrugalVTE pacman-g2 -Syu",false);		
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
	
	protected virtual void OnBTNServiceStartClicked (object sender, System.EventArgs e)
	{
		if(ServiceSelected!="")
		{
			Service service = new Service(ServiceSelected);
			service.Start();
		}
	}
	
	protected virtual void OnBTNServiceStopClicked (object sender, System.EventArgs e)
	{
		if(ServiceSelected!="")
		{
			Service service = new Service(ServiceSelected);
			service.Stop();
		}
	}
	
	protected virtual void OnBTNServiceDelBootClicked (object sender, System.EventArgs e)
	{
		if(ServiceSelected!="")
		{
			Service service = new Service(ServiceSelected);
			service.EnableDisableOnBoot(false);
		}
	}
	
	protected virtual void OnBTNServiceAddBootClicked (object sender, System.EventArgs e)
	{
		if(ServiceSelected!="")
		{
			Service service = new Service(ServiceSelected);
			service.EnableDisableOnBoot(true);
		}
	}
	
	protected virtual void OnBTNIrcClicked (object sender, System.EventArgs e)
	{
		Outils.Excecute("python","/usr/bin/PyFrugalVTE python /usr/bin/PyFrugalIRC",false);		
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
	
	
	
	
	
	
	
	
	
	
	
	
	
}

