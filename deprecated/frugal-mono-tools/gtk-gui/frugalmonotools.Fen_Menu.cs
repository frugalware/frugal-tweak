
// This file has been generated by the GUI designer. Do not modify.
namespace frugalmonotools
{
	public partial class Fen_Menu
	{
		private global::Gtk.VBox vbox1;
		private global::Gtk.HBox HBOX_Menu;
		private global::Gtk.ScrolledWindow GtkScrolledWindow;
		private global::Gtk.TreeView TREE_Menu;
		private global::Gtk.HBox HBOX_Details;
		private global::Gtk.Statusbar STA_Info;
		private global::Gtk.Label LAB_Info;
        
		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget frugalmonotools.Fen_Menu
			this.Name = "frugalmonotools.Fen_Menu";
			this.Title = global::Mono.Unix.Catalog.GetString ("Frugalware tweak !");
			this.Icon = global::Gdk.Pixbuf.LoadFromResource ("frugalmonotools.Pictures.fw.png");
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			this.Resizable = false;
			this.DefaultWidth = 900;
			this.DefaultHeight = 600;
			// Container child frugalmonotools.Fen_Menu.Gtk.Container+ContainerChild
			this.vbox1 = new global::Gtk.VBox ();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.HBOX_Menu = new global::Gtk.HBox ();
			this.HBOX_Menu.Name = "HBOX_Menu";
			this.HBOX_Menu.Spacing = 6;
			// Container child HBOX_Menu.Gtk.Box+BoxChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.TREE_Menu = new global::Gtk.TreeView ();
			this.TREE_Menu.WidthRequest = 165;
			this.TREE_Menu.HeightRequest = 165;
			this.TREE_Menu.CanFocus = true;
			this.TREE_Menu.Name = "TREE_Menu";
			this.GtkScrolledWindow.Add (this.TREE_Menu);
			this.HBOX_Menu.Add (this.GtkScrolledWindow);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.HBOX_Menu [this.GtkScrolledWindow]));
			w2.Position = 0;
			// Container child HBOX_Menu.Gtk.Box+BoxChild
			this.HBOX_Details = new global::Gtk.HBox ();
			this.HBOX_Details.Name = "HBOX_Details";
			this.HBOX_Details.Spacing = 6;
			this.HBOX_Menu.Add (this.HBOX_Details);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.HBOX_Menu [this.HBOX_Details]));
			w3.Position = 1;
			this.vbox1.Add (this.HBOX_Menu);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.HBOX_Menu]));
			w4.Position = 0;
			// Container child vbox1.Gtk.Box+BoxChild
			this.STA_Info = new global::Gtk.Statusbar ();
			this.STA_Info.Name = "STA_Info";
			this.STA_Info.Spacing = 6;
			// Container child STA_Info.Gtk.Box+BoxChild
			this.LAB_Info = new global::Gtk.Label ();
			this.LAB_Info.Name = "LAB_Info";
			this.STA_Info.Add (this.LAB_Info);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.STA_Info [this.LAB_Info]));
			w5.Position = 1;
			w5.Expand = false;
			w5.Fill = false;
			this.vbox1.Add (this.STA_Info);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.STA_Info]));
			w6.Position = 1;
			w6.Expand = false;
			w6.Fill = false;
			this.Add (this.vbox1);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Hide ();
			this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
		}
	}
}