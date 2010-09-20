
// This file has been generated by the GUI designer. Do not modify.
namespace frugalmonotools
{
	public partial class WID_Hardware
	{
		private global::Gtk.VBox vbox2;

		private global::Gtk.HBox hbox15;

		private global::Gtk.Button BTN_Printer;

		private global::Gtk.Label LAB_Printer;

		private global::Gtk.HBox hbox17;

		private global::Gtk.Button BTN_Setup;

		private global::Gtk.Label LIB_Setup;

		private global::Gtk.HBox hbox18;

		private global::Gtk.Label LIB_Lirc;

		private global::Gtk.HBox hbox19;

		private global::Gtk.Label LIB_Bluez;

		private global::Gtk.ScrolledWindow GtkScrolledWindow3;

		private global::Gtk.TextView TXT_Lspci;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget frugalmonotools.WID_Hardware
			global::Stetic.BinContainer.Attach (this);
			this.Name = "frugalmonotools.WID_Hardware";
			// Container child frugalmonotools.WID_Hardware.Gtk.Container+ContainerChild
			this.vbox2 = new global::Gtk.VBox ();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox15 = new global::Gtk.HBox ();
			this.hbox15.Name = "hbox15";
			this.hbox15.Spacing = 6;
			// Container child hbox15.Gtk.Box+BoxChild
			this.BTN_Printer = new global::Gtk.Button ();
			this.BTN_Printer.CanFocus = true;
			this.BTN_Printer.Name = "BTN_Printer";
			this.BTN_Printer.UseUnderline = true;
			// Container child BTN_Printer.Gtk.Container+ContainerChild
			global::Gtk.Alignment w1 = new global::Gtk.Alignment (0.5f, 0.5f, 0f, 0f);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w2 = new global::Gtk.HBox ();
			w2.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w3 = new global::Gtk.Image ();
			w3.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-print", global::Gtk.IconSize.Menu);
			w2.Add (w3);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w5 = new global::Gtk.Label ();
			w5.LabelProp = global::Mono.Unix.Catalog.GetString ("System Configuration Printer");
			w5.UseUnderline = true;
			w2.Add (w5);
			w1.Add (w2);
			this.BTN_Printer.Add (w1);
			this.hbox15.Add (this.BTN_Printer);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.hbox15[this.BTN_Printer]));
			w9.Position = 0;
			w9.Expand = false;
			w9.Fill = false;
			// Container child hbox15.Gtk.Box+BoxChild
			this.LAB_Printer = new global::Gtk.Label ();
			this.LAB_Printer.Name = "LAB_Printer";
			this.LAB_Printer.LabelProp = global::Mono.Unix.Catalog.GetString ("You should install \"system-config-printer\"");
			this.hbox15.Add (this.LAB_Printer);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.hbox15[this.LAB_Printer]));
			w10.PackType = ((global::Gtk.PackType)(1));
			w10.Position = 1;
			w10.Expand = false;
			w10.Fill = false;
			this.vbox2.Add (this.hbox15);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hbox15]));
			w11.Position = 0;
			w11.Expand = false;
			w11.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox17 = new global::Gtk.HBox ();
			this.hbox17.Name = "hbox17";
			this.hbox17.Spacing = 6;
			// Container child hbox17.Gtk.Box+BoxChild
			this.BTN_Setup = new global::Gtk.Button ();
			this.BTN_Setup.CanFocus = true;
			this.BTN_Setup.Name = "BTN_Setup";
			this.BTN_Setup.UseUnderline = true;
			this.BTN_Setup.Label = global::Mono.Unix.Catalog.GetString ("Frugalware System configuration");
			this.hbox17.Add (this.BTN_Setup);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.hbox17[this.BTN_Setup]));
			w12.Position = 0;
			w12.Expand = false;
			w12.Fill = false;
			// Container child hbox17.Gtk.Box+BoxChild
			this.LIB_Setup = new global::Gtk.Label ();
			this.LIB_Setup.Name = "LIB_Setup";
			this.LIB_Setup.LabelProp = global::Mono.Unix.Catalog.GetString ("You should install frugalwareutils");
			this.hbox17.Add (this.LIB_Setup);
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.hbox17[this.LIB_Setup]));
			w13.PackType = ((global::Gtk.PackType)(1));
			w13.Position = 1;
			w13.Expand = false;
			w13.Fill = false;
			this.vbox2.Add (this.hbox17);
			global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hbox17]));
			w14.Position = 1;
			w14.Expand = false;
			w14.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox18 = new global::Gtk.HBox ();
			this.hbox18.Name = "hbox18";
			this.hbox18.Spacing = 6;
			// Container child hbox18.Gtk.Box+BoxChild
			this.LIB_Lirc = new global::Gtk.Label ();
			this.LIB_Lirc.Name = "LIB_Lirc";
			this.LIB_Lirc.LabelProp = global::Mono.Unix.Catalog.GetString ("You should install lirc");
			this.hbox18.Add (this.LIB_Lirc);
			global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.hbox18[this.LIB_Lirc]));
			w15.PackType = ((global::Gtk.PackType)(1));
			w15.Position = 1;
			w15.Expand = false;
			w15.Fill = false;
			this.vbox2.Add (this.hbox18);
			global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hbox18]));
			w16.Position = 2;
			w16.Expand = false;
			w16.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox19 = new global::Gtk.HBox ();
			this.hbox19.Name = "hbox19";
			this.hbox19.Spacing = 6;
			// Container child hbox19.Gtk.Box+BoxChild
			this.LIB_Bluez = new global::Gtk.Label ();
			this.LIB_Bluez.Name = "LIB_Bluez";
			this.LIB_Bluez.LabelProp = global::Mono.Unix.Catalog.GetString ("You should install bluez");
			this.hbox19.Add (this.LIB_Bluez);
			global::Gtk.Box.BoxChild w17 = ((global::Gtk.Box.BoxChild)(this.hbox19[this.LIB_Bluez]));
			w17.PackType = ((global::Gtk.PackType)(1));
			w17.Position = 1;
			w17.Expand = false;
			w17.Fill = false;
			this.vbox2.Add (this.hbox19);
			global::Gtk.Box.BoxChild w18 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hbox19]));
			w18.Position = 3;
			w18.Expand = false;
			w18.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.GtkScrolledWindow3 = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow3.Name = "GtkScrolledWindow3";
			this.GtkScrolledWindow3.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow3.Gtk.Container+ContainerChild
			this.TXT_Lspci = new global::Gtk.TextView ();
			this.TXT_Lspci.CanFocus = true;
			this.TXT_Lspci.Name = "TXT_Lspci";
			this.GtkScrolledWindow3.Add (this.TXT_Lspci);
			this.vbox2.Add (this.GtkScrolledWindow3);
			global::Gtk.Box.BoxChild w20 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.GtkScrolledWindow3]));
			w20.Position = 4;
			this.Add (this.vbox2);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.BTN_Setup.Hide ();
			this.Hide ();
			this.BTN_Printer.Clicked += new global::System.EventHandler (this.OnBTNPrinterClicked);
			this.BTN_Setup.Clicked += new global::System.EventHandler (this.OnBTNSetupClicked);
		}
	}
}
