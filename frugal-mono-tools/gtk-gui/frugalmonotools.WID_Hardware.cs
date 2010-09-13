
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
			this.BTN_Printer.Label = global::Mono.Unix.Catalog.GetString ("System Configuration Printer");
			this.hbox15.Add (this.BTN_Printer);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.hbox15[this.BTN_Printer]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child hbox15.Gtk.Box+BoxChild
			this.LAB_Printer = new global::Gtk.Label ();
			this.LAB_Printer.Name = "LAB_Printer";
			this.LAB_Printer.LabelProp = global::Mono.Unix.Catalog.GetString ("You should install \"system-config-printer\"");
			this.hbox15.Add (this.LAB_Printer);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox15[this.LAB_Printer]));
			w2.PackType = ((global::Gtk.PackType)(1));
			w2.Position = 1;
			w2.Expand = false;
			w2.Fill = false;
			this.vbox2.Add (this.hbox15);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hbox15]));
			w3.Position = 0;
			w3.Expand = false;
			w3.Fill = false;
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
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox17[this.BTN_Setup]));
			w4.Position = 0;
			w4.Expand = false;
			w4.Fill = false;
			// Container child hbox17.Gtk.Box+BoxChild
			this.LIB_Setup = new global::Gtk.Label ();
			this.LIB_Setup.Name = "LIB_Setup";
			this.LIB_Setup.LabelProp = global::Mono.Unix.Catalog.GetString ("You should install frugalwareutils");
			this.hbox17.Add (this.LIB_Setup);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox17[this.LIB_Setup]));
			w5.PackType = ((global::Gtk.PackType)(1));
			w5.Position = 1;
			w5.Expand = false;
			w5.Fill = false;
			this.vbox2.Add (this.hbox17);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hbox17]));
			w6.Position = 1;
			w6.Expand = false;
			w6.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox18 = new global::Gtk.HBox ();
			this.hbox18.Name = "hbox18";
			this.hbox18.Spacing = 6;
			// Container child hbox18.Gtk.Box+BoxChild
			this.LIB_Lirc = new global::Gtk.Label ();
			this.LIB_Lirc.Name = "LIB_Lirc";
			this.LIB_Lirc.LabelProp = global::Mono.Unix.Catalog.GetString ("You should install lirc");
			this.hbox18.Add (this.LIB_Lirc);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.hbox18[this.LIB_Lirc]));
			w7.PackType = ((global::Gtk.PackType)(1));
			w7.Position = 1;
			w7.Expand = false;
			w7.Fill = false;
			this.vbox2.Add (this.hbox18);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hbox18]));
			w8.Position = 2;
			w8.Expand = false;
			w8.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox19 = new global::Gtk.HBox ();
			this.hbox19.Name = "hbox19";
			this.hbox19.Spacing = 6;
			// Container child hbox19.Gtk.Box+BoxChild
			this.LIB_Bluez = new global::Gtk.Label ();
			this.LIB_Bluez.Name = "LIB_Bluez";
			this.LIB_Bluez.LabelProp = global::Mono.Unix.Catalog.GetString ("You should install bluez");
			this.hbox19.Add (this.LIB_Bluez);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.hbox19[this.LIB_Bluez]));
			w9.PackType = ((global::Gtk.PackType)(1));
			w9.Position = 1;
			w9.Expand = false;
			w9.Fill = false;
			this.vbox2.Add (this.hbox19);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hbox19]));
			w10.Position = 3;
			w10.Expand = false;
			w10.Fill = false;
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
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.GtkScrolledWindow3]));
			w12.Position = 4;
			this.Add (this.vbox2);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.BTN_Setup.Hide ();
			this.Hide ();
		}
	}
}
