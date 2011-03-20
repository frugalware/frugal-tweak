
// This file has been generated by the GUI designer. Do not modify.
namespace frugalmonotools
{
	public partial class WID_Network
	{
		private global::Gtk.VBox vbox1;
		private global::Gtk.HBox hbox1;
		private global::Gtk.Image image6;
		private global::Gtk.CheckButton INT_FW;
		private global::Gtk.HBox hbox2;
		private global::Gtk.Image image7;
		private global::Gtk.CheckButton INT_NM;
		private global::Gtk.Label LIB_NMNotInstalled;
		private global::Gtk.HBox hbox3;
		private global::Gtk.Image image8;
		private global::Gtk.CheckButton INT_WICD;
		private global::Gtk.Label LIB_WICDNotInstalled;
		private global::Gtk.HBox hbox4;
		private global::Gtk.Label LIB_Root;
		private global::Gtk.Button BTN_Network;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget frugalmonotools.WID_Network
			global::Stetic.BinContainer.Attach (this);
			this.Name = "frugalmonotools.WID_Network";
			// Container child frugalmonotools.WID_Network.Gtk.Container+ContainerChild
			this.vbox1 = new global::Gtk.VBox ();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox ();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.image6 = new global::Gtk.Image ();
			this.image6.Name = "image6";
			this.image6.Pixbuf = global::Gdk.Pixbuf.LoadFromResource ("frugalmonotools.Pictures.fw-mini.png");
			this.hbox1.Add (this.image6);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.image6]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.INT_FW = new global::Gtk.CheckButton ();
			this.INT_FW.CanFocus = true;
			this.INT_FW.Name = "INT_FW";
			this.INT_FW.Label = global::Mono.Unix.Catalog.GetString ("Use Netconfig/Gnetconfig");
			this.INT_FW.DrawIndicator = true;
			this.INT_FW.UseUnderline = true;
			this.hbox1.Add (this.INT_FW);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.INT_FW]));
			w2.Position = 1;
			this.vbox1.Add (this.hbox1);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.hbox1]));
			w3.Position = 0;
			w3.Expand = false;
			w3.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox2 = new global::Gtk.HBox ();
			this.hbox2.Name = "hbox2";
			this.hbox2.Spacing = 6;
			// Container child hbox2.Gtk.Box+BoxChild
			this.image7 = new global::Gtk.Image ();
			this.image7.Name = "image7";
			this.image7.Pixbuf = global::Gdk.Pixbuf.LoadFromResource ("frugalmonotools.Pictures.nmlogo.png");
			this.hbox2.Add (this.image7);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.image7]));
			w4.Position = 0;
			w4.Expand = false;
			w4.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.INT_NM = new global::Gtk.CheckButton ();
			this.INT_NM.CanFocus = true;
			this.INT_NM.Name = "INT_NM";
			this.INT_NM.Label = global::Mono.Unix.Catalog.GetString ("Use NetworkManager");
			this.INT_NM.DrawIndicator = true;
			this.INT_NM.UseUnderline = true;
			this.hbox2.Add (this.INT_NM);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.INT_NM]));
			w5.Position = 1;
			// Container child hbox2.Gtk.Box+BoxChild
			this.LIB_NMNotInstalled = new global::Gtk.Label ();
			this.LIB_NMNotInstalled.Name = "LIB_NMNotInstalled";
			this.LIB_NMNotInstalled.LabelProp = global::Mono.Unix.Catalog.GetString ("Not installed");
			this.hbox2.Add (this.LIB_NMNotInstalled);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.LIB_NMNotInstalled]));
			w6.Position = 2;
			w6.Expand = false;
			w6.Fill = false;
			this.vbox1.Add (this.hbox2);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.hbox2]));
			w7.Position = 1;
			w7.Expand = false;
			w7.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox3 = new global::Gtk.HBox ();
			this.hbox3.Name = "hbox3";
			this.hbox3.Spacing = 6;
			// Container child hbox3.Gtk.Box+BoxChild
			this.image8 = new global::Gtk.Image ();
			this.image8.Name = "image8";
			this.image8.Pixbuf = global::Gdk.Pixbuf.LoadFromResource ("frugalmonotools.Pictures.wicdlogo.png");
			this.hbox3.Add (this.image8);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.image8]));
			w8.Position = 0;
			w8.Expand = false;
			w8.Fill = false;
			// Container child hbox3.Gtk.Box+BoxChild
			this.INT_WICD = new global::Gtk.CheckButton ();
			this.INT_WICD.CanFocus = true;
			this.INT_WICD.Name = "INT_WICD";
			this.INT_WICD.Label = global::Mono.Unix.Catalog.GetString ("Use Wicd");
			this.INT_WICD.DrawIndicator = true;
			this.INT_WICD.UseUnderline = true;
			this.hbox3.Add (this.INT_WICD);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.INT_WICD]));
			w9.Position = 1;
			// Container child hbox3.Gtk.Box+BoxChild
			this.LIB_WICDNotInstalled = new global::Gtk.Label ();
			this.LIB_WICDNotInstalled.Name = "LIB_WICDNotInstalled";
			this.LIB_WICDNotInstalled.LabelProp = global::Mono.Unix.Catalog.GetString ("Not installed");
			this.hbox3.Add (this.LIB_WICDNotInstalled);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.LIB_WICDNotInstalled]));
			w10.Position = 2;
			w10.Expand = false;
			w10.Fill = false;
			this.vbox1.Add (this.hbox3);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.hbox3]));
			w11.Position = 2;
			w11.Expand = false;
			w11.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox4 = new global::Gtk.HBox ();
			this.hbox4.Name = "hbox4";
			this.hbox4.Spacing = 6;
			// Container child hbox4.Gtk.Box+BoxChild
			this.LIB_Root = new global::Gtk.Label ();
			this.LIB_Root.Name = "LIB_Root";
			this.LIB_Root.LabelProp = global::Mono.Unix.Catalog.GetString ("Can't save, should be started as root");
			this.hbox4.Add (this.LIB_Root);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.hbox4 [this.LIB_Root]));
			w12.Position = 0;
			w12.Expand = false;
			w12.Fill = false;
			// Container child hbox4.Gtk.Box+BoxChild
			this.BTN_Network = new global::Gtk.Button ();
			this.BTN_Network.CanFocus = true;
			this.BTN_Network.Name = "BTN_Network";
			this.BTN_Network.UseUnderline = true;
			// Container child BTN_Network.Gtk.Container+ContainerChild
			global::Gtk.Alignment w13 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w14 = new global::Gtk.HBox ();
			w14.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w15 = new global::Gtk.Image ();
			w15.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-apply", global::Gtk.IconSize.Menu);
			w14.Add (w15);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w17 = new global::Gtk.Label ();
			w17.LabelProp = global::Mono.Unix.Catalog.GetString ("Apply");
			w17.UseUnderline = true;
			w14.Add (w17);
			w13.Add (w14);
			this.BTN_Network.Add (w13);
			this.hbox4.Add (this.BTN_Network);
			global::Gtk.Box.BoxChild w21 = ((global::Gtk.Box.BoxChild)(this.hbox4 [this.BTN_Network]));
			w21.PackType = ((global::Gtk.PackType)(1));
			w21.Position = 2;
			w21.Expand = false;
			w21.Fill = false;
			this.vbox1.Add (this.hbox4);
			global::Gtk.Box.BoxChild w22 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.hbox4]));
			w22.PackType = ((global::Gtk.PackType)(1));
			w22.Position = 3;
			w22.Expand = false;
			w22.Fill = false;
			this.Add (this.vbox1);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Hide ();
			this.INT_FW.Clicked += new global::System.EventHandler (this.OnINTFWClicked);
			this.INT_NM.Clicked += new global::System.EventHandler (this.OnINTNMClicked);
			this.INT_WICD.Clicked += new global::System.EventHandler (this.OnINTWICDClicked);
			this.BTN_Network.Clicked += new global::System.EventHandler (this.OnBTNNetworkClicked);
		}
	}
}
