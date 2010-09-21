
// This file has been generated by the GUI designer. Do not modify.
namespace frugalmonotools
{
	public partial class WID_Services
	{
		private global::Gtk.VBox vbox7;

		private global::Gtk.ScrolledWindow GtkScrolledWindow1;

		private global::Gtk.TreeView TREE_Services;

		private global::Gtk.HBox hbox20;

		private global::Gtk.Button BTN_ServiceStart;

		private global::Gtk.Button BTN_ServiceStop;

		private global::Gtk.Button BTN_ServiceDelBoot;

		private global::Gtk.Button BTN_ServiceAddBoot;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget frugalmonotools.WID_Services
			global::Stetic.BinContainer.Attach (this);
			this.Name = "frugalmonotools.WID_Services";
			// Container child frugalmonotools.WID_Services.Gtk.Container+ContainerChild
			this.vbox7 = new global::Gtk.VBox ();
			this.vbox7.Name = "vbox7";
			this.vbox7.Spacing = 6;
			// Container child vbox7.Gtk.Box+BoxChild
			this.GtkScrolledWindow1 = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow1.Name = "GtkScrolledWindow1";
			this.GtkScrolledWindow1.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow1.Gtk.Container+ContainerChild
			this.TREE_Services = new global::Gtk.TreeView ();
			this.TREE_Services.CanFocus = true;
			this.TREE_Services.Name = "TREE_Services";
			this.GtkScrolledWindow1.Add (this.TREE_Services);
			this.vbox7.Add (this.GtkScrolledWindow1);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox7[this.GtkScrolledWindow1]));
			w2.Position = 0;
			// Container child vbox7.Gtk.Box+BoxChild
			this.hbox20 = new global::Gtk.HBox ();
			this.hbox20.Name = "hbox20";
			this.hbox20.Spacing = 6;
			// Container child hbox20.Gtk.Box+BoxChild
			this.BTN_ServiceStart = new global::Gtk.Button ();
			this.BTN_ServiceStart.CanFocus = true;
			this.BTN_ServiceStart.Name = "BTN_ServiceStart";
			this.BTN_ServiceStart.UseUnderline = true;
			// Container child BTN_ServiceStart.Gtk.Container+ContainerChild
			global::Gtk.Alignment w3 = new global::Gtk.Alignment (0.5f, 0.5f, 0f, 0f);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w4 = new global::Gtk.HBox ();
			w4.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w5 = new global::Gtk.Image ();
			w5.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-media-play", global::Gtk.IconSize.Menu);
			w4.Add (w5);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w7 = new global::Gtk.Label ();
			w7.LabelProp = global::Mono.Unix.Catalog.GetString ("Start");
			w7.UseUnderline = true;
			w4.Add (w7);
			w3.Add (w4);
			this.BTN_ServiceStart.Add (w3);
			this.hbox20.Add (this.BTN_ServiceStart);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.hbox20[this.BTN_ServiceStart]));
			w11.Position = 0;
			w11.Expand = false;
			w11.Fill = false;
			// Container child hbox20.Gtk.Box+BoxChild
			this.BTN_ServiceStop = new global::Gtk.Button ();
			this.BTN_ServiceStop.CanFocus = true;
			this.BTN_ServiceStop.Name = "BTN_ServiceStop";
			this.BTN_ServiceStop.UseUnderline = true;
			// Container child BTN_ServiceStop.Gtk.Container+ContainerChild
			global::Gtk.Alignment w12 = new global::Gtk.Alignment (0.5f, 0.5f, 0f, 0f);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w13 = new global::Gtk.HBox ();
			w13.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w14 = new global::Gtk.Image ();
			w14.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-media-stop", global::Gtk.IconSize.Menu);
			w13.Add (w14);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w16 = new global::Gtk.Label ();
			w16.LabelProp = global::Mono.Unix.Catalog.GetString ("Stop");
			w16.UseUnderline = true;
			w13.Add (w16);
			w12.Add (w13);
			this.BTN_ServiceStop.Add (w12);
			this.hbox20.Add (this.BTN_ServiceStop);
			global::Gtk.Box.BoxChild w20 = ((global::Gtk.Box.BoxChild)(this.hbox20[this.BTN_ServiceStop]));
			w20.Position = 1;
			w20.Expand = false;
			w20.Fill = false;
			// Container child hbox20.Gtk.Box+BoxChild
			this.BTN_ServiceDelBoot = new global::Gtk.Button ();
			this.BTN_ServiceDelBoot.CanFocus = true;
			this.BTN_ServiceDelBoot.Name = "BTN_ServiceDelBoot";
			this.BTN_ServiceDelBoot.UseUnderline = true;
			// Container child BTN_ServiceDelBoot.Gtk.Container+ContainerChild
			global::Gtk.Alignment w21 = new global::Gtk.Alignment (0.5f, 0.5f, 0f, 0f);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w22 = new global::Gtk.HBox ();
			w22.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w23 = new global::Gtk.Image ();
			w23.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-delete", global::Gtk.IconSize.Menu);
			w22.Add (w23);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w25 = new global::Gtk.Label ();
			w25.LabelProp = global::Mono.Unix.Catalog.GetString ("Don't start it on boot");
			w25.UseUnderline = true;
			w22.Add (w25);
			w21.Add (w22);
			this.BTN_ServiceDelBoot.Add (w21);
			this.hbox20.Add (this.BTN_ServiceDelBoot);
			global::Gtk.Box.BoxChild w29 = ((global::Gtk.Box.BoxChild)(this.hbox20[this.BTN_ServiceDelBoot]));
			w29.Position = 2;
			w29.Expand = false;
			w29.Fill = false;
			// Container child hbox20.Gtk.Box+BoxChild
			this.BTN_ServiceAddBoot = new global::Gtk.Button ();
			this.BTN_ServiceAddBoot.CanFocus = true;
			this.BTN_ServiceAddBoot.Name = "BTN_ServiceAddBoot";
			this.BTN_ServiceAddBoot.UseUnderline = true;
			// Container child BTN_ServiceAddBoot.Gtk.Container+ContainerChild
			global::Gtk.Alignment w30 = new global::Gtk.Alignment (0.5f, 0.5f, 0f, 0f);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w31 = new global::Gtk.HBox ();
			w31.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w32 = new global::Gtk.Image ();
			w32.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-add", global::Gtk.IconSize.Menu);
			w31.Add (w32);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w34 = new global::Gtk.Label ();
			w34.LabelProp = global::Mono.Unix.Catalog.GetString ("Start it on boot");
			w34.UseUnderline = true;
			w31.Add (w34);
			w30.Add (w31);
			this.BTN_ServiceAddBoot.Add (w30);
			this.hbox20.Add (this.BTN_ServiceAddBoot);
			global::Gtk.Box.BoxChild w38 = ((global::Gtk.Box.BoxChild)(this.hbox20[this.BTN_ServiceAddBoot]));
			w38.Position = 3;
			w38.Expand = false;
			w38.Fill = false;
			this.vbox7.Add (this.hbox20);
			global::Gtk.Box.BoxChild w39 = ((global::Gtk.Box.BoxChild)(this.vbox7[this.hbox20]));
			w39.Position = 1;
			w39.Expand = false;
			w39.Fill = false;
			this.Add (this.vbox7);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.BTN_ServiceStart.Hide ();
			this.BTN_ServiceStop.Hide ();
			this.BTN_ServiceDelBoot.Hide ();
			this.BTN_ServiceAddBoot.Hide ();
			this.Hide ();
			this.BTN_ServiceStart.Clicked += new global::System.EventHandler (this.OnBTNServiceStartClicked);
			this.BTN_ServiceStop.Clicked += new global::System.EventHandler (this.OnBTNServiceStopClicked);
			this.BTN_ServiceDelBoot.Clicked += new global::System.EventHandler (this.OnBTNServiceDelBootClicked);
			this.BTN_ServiceAddBoot.Clicked += new global::System.EventHandler (this.OnBTNServiceAddBootClicked);
		}
	}
}
