
// This file has been generated by the GUI designer. Do not modify.
namespace frugalmonotools
{
	public partial class WID_Update
	{
		private global::Gtk.VBox vbox10;

		private global::Gtk.ScrolledWindow GtkScrolledWindow4;

		private global::Gtk.TreeView TREE_UpdatePkg;

		private global::Gtk.HBox hbox31;

		private global::Gtk.Label label17;

		private global::Gtk.Entry SAI_ignorePkg;

		private global::Gtk.Button BTN_ApplyIgnorePkg;

		private global::Gtk.HBox hbox22;

		private global::Gtk.Button BTN_Hide;

		private global::Gtk.Button BTN_UpdateDatabase;

		private global::Gtk.Button BTN_Refresh;

		private global::Gtk.Button BTN_Update;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget frugalmonotools.WID_Update
			global::Stetic.BinContainer.Attach (this);
			this.Name = "frugalmonotools.WID_Update";
			// Container child frugalmonotools.WID_Update.Gtk.Container+ContainerChild
			this.vbox10 = new global::Gtk.VBox ();
			this.vbox10.Name = "vbox10";
			this.vbox10.Spacing = 6;
			// Container child vbox10.Gtk.Box+BoxChild
			this.GtkScrolledWindow4 = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow4.Name = "GtkScrolledWindow4";
			this.GtkScrolledWindow4.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow4.Gtk.Container+ContainerChild
			this.TREE_UpdatePkg = new global::Gtk.TreeView ();
			this.TREE_UpdatePkg.CanFocus = true;
			this.TREE_UpdatePkg.Name = "TREE_UpdatePkg";
			this.GtkScrolledWindow4.Add (this.TREE_UpdatePkg);
			this.vbox10.Add (this.GtkScrolledWindow4);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox10[this.GtkScrolledWindow4]));
			w2.Position = 0;
			// Container child vbox10.Gtk.Box+BoxChild
			this.hbox31 = new global::Gtk.HBox ();
			this.hbox31.Name = "hbox31";
			this.hbox31.Spacing = 6;
			// Container child hbox31.Gtk.Box+BoxChild
			this.label17 = new global::Gtk.Label ();
			this.label17.Name = "label17";
			this.label17.LabelProp = global::Mono.Unix.Catalog.GetString ("Package ignored when system is updated");
			this.hbox31.Add (this.label17);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox31[this.label17]));
			w3.Position = 0;
			w3.Expand = false;
			w3.Fill = false;
			// Container child hbox31.Gtk.Box+BoxChild
			this.SAI_ignorePkg = new global::Gtk.Entry ();
			this.SAI_ignorePkg.CanFocus = true;
			this.SAI_ignorePkg.Name = "SAI_ignorePkg";
			this.SAI_ignorePkg.IsEditable = true;
			this.SAI_ignorePkg.InvisibleChar = '•';
			this.hbox31.Add (this.SAI_ignorePkg);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox31[this.SAI_ignorePkg]));
			w4.Position = 1;
			// Container child hbox31.Gtk.Box+BoxChild
			this.BTN_ApplyIgnorePkg = new global::Gtk.Button ();
			this.BTN_ApplyIgnorePkg.CanFocus = true;
			this.BTN_ApplyIgnorePkg.Name = "BTN_ApplyIgnorePkg";
			this.BTN_ApplyIgnorePkg.UseUnderline = true;
			this.BTN_ApplyIgnorePkg.Label = global::Mono.Unix.Catalog.GetString ("Apply");
			this.hbox31.Add (this.BTN_ApplyIgnorePkg);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox31[this.BTN_ApplyIgnorePkg]));
			w5.Position = 2;
			w5.Expand = false;
			w5.Fill = false;
			this.vbox10.Add (this.hbox31);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbox10[this.hbox31]));
			w6.Position = 1;
			w6.Expand = false;
			w6.Fill = false;
			// Container child vbox10.Gtk.Box+BoxChild
			this.hbox22 = new global::Gtk.HBox ();
			this.hbox22.Name = "hbox22";
			this.hbox22.Spacing = 6;
			// Container child hbox22.Gtk.Box+BoxChild
			this.BTN_Hide = new global::Gtk.Button ();
			this.BTN_Hide.CanFocus = true;
			this.BTN_Hide.Name = "BTN_Hide";
			this.BTN_Hide.UseUnderline = true;
			this.BTN_Hide.Label = global::Mono.Unix.Catalog.GetString ("Hide this package");
			this.hbox22.Add (this.BTN_Hide);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.hbox22[this.BTN_Hide]));
			w7.Position = 0;
			w7.Expand = false;
			w7.Fill = false;
			// Container child hbox22.Gtk.Box+BoxChild
			this.BTN_UpdateDatabase = new global::Gtk.Button ();
			this.BTN_UpdateDatabase.CanFocus = true;
			this.BTN_UpdateDatabase.Name = "BTN_UpdateDatabase";
			this.BTN_UpdateDatabase.UseUnderline = true;
			this.BTN_UpdateDatabase.Label = global::Mono.Unix.Catalog.GetString ("Update database");
			this.hbox22.Add (this.BTN_UpdateDatabase);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.hbox22[this.BTN_UpdateDatabase]));
			w8.Position = 1;
			w8.Expand = false;
			w8.Fill = false;
			// Container child hbox22.Gtk.Box+BoxChild
			this.BTN_Refresh = new global::Gtk.Button ();
			this.BTN_Refresh.CanFocus = true;
			this.BTN_Refresh.Name = "BTN_Refresh";
			this.BTN_Refresh.UseUnderline = true;
			this.BTN_Refresh.Label = global::Mono.Unix.Catalog.GetString ("Refresh list");
			this.hbox22.Add (this.BTN_Refresh);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.hbox22[this.BTN_Refresh]));
			w9.Position = 2;
			w9.Expand = false;
			w9.Fill = false;
			// Container child hbox22.Gtk.Box+BoxChild
			this.BTN_Update = new global::Gtk.Button ();
			this.BTN_Update.CanFocus = true;
			this.BTN_Update.Name = "BTN_Update";
			this.BTN_Update.UseUnderline = true;
			this.BTN_Update.Label = global::Mono.Unix.Catalog.GetString ("Update system");
			this.hbox22.Add (this.BTN_Update);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.hbox22[this.BTN_Update]));
			w10.Position = 3;
			w10.Expand = false;
			w10.Fill = false;
			this.vbox10.Add (this.hbox22);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.vbox10[this.hbox22]));
			w11.Position = 2;
			w11.Expand = false;
			w11.Fill = false;
			this.Add (this.vbox10);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.BTN_Hide.Hide ();
			this.Hide ();
			this.BTN_ApplyIgnorePkg.Clicked += new global::System.EventHandler (this.OnBTNApplyIgnorePkgClicked);
			this.BTN_Hide.Clicked += new global::System.EventHandler (this.OnBTNHideClicked);
			this.BTN_UpdateDatabase.Clicked += new global::System.EventHandler (this.OnBTNUpdateDatabaseClicked);
			this.BTN_Refresh.Clicked += new global::System.EventHandler (this.OnBTNRefreshClicked);
			this.BTN_Update.Clicked += new global::System.EventHandler (this.OnBTNUpdateClicked);
		}
	}
}