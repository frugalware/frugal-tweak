
// This file has been generated by the GUI designer. Do not modify.
namespace frugalmonotools
{
	public partial class WID_System
	{
		private global::Gtk.VBox vbox11;
		private global::Gtk.HBox hbox30;
		private global::Gtk.Image image9;
		private global::Gtk.HBox hbox24;
		private global::Gtk.Label label11;
		private global::Gtk.Entry SAI_Host;
		private global::Gtk.HBox hbox25;
		private global::Gtk.Label label12;
		private global::Gtk.Entry SAI_Distribution;
		private global::Gtk.HBox hbox26;
		private global::Gtk.Label label13;
		private global::Gtk.Entry SAI_Kernel;
		private global::Gtk.HBox hbox27;
		private global::Gtk.Label label14;
		private global::Gtk.Entry SAI_Shell;
		private global::Gtk.HBox hbox28;
		private global::Gtk.Label label15;
		private global::Gtk.Label label16;
		private global::Gtk.ComboBoxEntry CBO_Locale;
		private global::Gtk.HBox hbox2;
		private global::Gtk.Label label18;
		private global::Gtk.ComboBoxEntry CBO_Time;
		private global::Gtk.Label label17;
		private global::Gtk.ComboBoxEntry CBO_Keymap;
		private global::Gtk.HBox hbox29;
		private global::Gtk.Label LIB_Root;
		private global::Gtk.Button BTN_System;
        
		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget frugalmonotools.WID_System
			global::Stetic.BinContainer.Attach (this);
			this.Name = "frugalmonotools.WID_System";
			// Container child frugalmonotools.WID_System.Gtk.Container+ContainerChild
			this.vbox11 = new global::Gtk.VBox ();
			this.vbox11.Name = "vbox11";
			this.vbox11.Spacing = 6;
			// Container child vbox11.Gtk.Box+BoxChild
			this.hbox30 = new global::Gtk.HBox ();
			this.hbox30.Name = "hbox30";
			this.hbox30.Spacing = 6;
			// Container child hbox30.Gtk.Box+BoxChild
			this.image9 = new global::Gtk.Image ();
			this.image9.Name = "image9";
			this.image9.Pixbuf = global::Gdk.Pixbuf.LoadFromResource ("frugalmonotools.Pictures.fw.png");
			this.hbox30.Add (this.image9);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.hbox30 [this.image9]));
			w1.PackType = ((global::Gtk.PackType)(1));
			w1.Position = 2;
			w1.Expand = false;
			w1.Fill = false;
			this.vbox11.Add (this.hbox30);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox11 [this.hbox30]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child vbox11.Gtk.Box+BoxChild
			this.hbox24 = new global::Gtk.HBox ();
			this.hbox24.Name = "hbox24";
			this.hbox24.Spacing = 6;
			// Container child hbox24.Gtk.Box+BoxChild
			this.label11 = new global::Gtk.Label ();
			this.label11.Name = "label11";
			this.label11.LabelProp = global::Mono.Unix.Catalog.GetString ("Hostname   ");
			this.hbox24.Add (this.label11);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox24 [this.label11]));
			w3.Position = 0;
			w3.Expand = false;
			w3.Fill = false;
			// Container child hbox24.Gtk.Box+BoxChild
			this.SAI_Host = new global::Gtk.Entry ();
			this.SAI_Host.CanFocus = true;
			this.SAI_Host.Name = "SAI_Host";
			this.SAI_Host.IsEditable = true;
			this.SAI_Host.MaxLength = 50;
			this.SAI_Host.InvisibleChar = '•';
			this.hbox24.Add (this.SAI_Host);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox24 [this.SAI_Host]));
			w4.Position = 2;
			this.vbox11.Add (this.hbox24);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox11 [this.hbox24]));
			w5.Position = 1;
			w5.Expand = false;
			w5.Fill = false;
			// Container child vbox11.Gtk.Box+BoxChild
			this.hbox25 = new global::Gtk.HBox ();
			this.hbox25.Name = "hbox25";
			this.hbox25.Spacing = 6;
			// Container child hbox25.Gtk.Box+BoxChild
			this.label12 = new global::Gtk.Label ();
			this.label12.Name = "label12";
			this.label12.LabelProp = global::Mono.Unix.Catalog.GetString ("Distribution");
			this.hbox25.Add (this.label12);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.hbox25 [this.label12]));
			w6.Position = 0;
			w6.Expand = false;
			w6.Fill = false;
			// Container child hbox25.Gtk.Box+BoxChild
			this.SAI_Distribution = new global::Gtk.Entry ();
			this.SAI_Distribution.Sensitive = false;
			this.SAI_Distribution.CanFocus = true;
			this.SAI_Distribution.Name = "SAI_Distribution";
			this.SAI_Distribution.IsEditable = true;
			this.SAI_Distribution.InvisibleChar = '•';
			this.hbox25.Add (this.SAI_Distribution);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.hbox25 [this.SAI_Distribution]));
			w7.Position = 2;
			this.vbox11.Add (this.hbox25);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.vbox11 [this.hbox25]));
			w8.Position = 2;
			w8.Expand = false;
			w8.Fill = false;
			// Container child vbox11.Gtk.Box+BoxChild
			this.hbox26 = new global::Gtk.HBox ();
			this.hbox26.Name = "hbox26";
			this.hbox26.Spacing = 6;
			// Container child hbox26.Gtk.Box+BoxChild
			this.label13 = new global::Gtk.Label ();
			this.label13.Name = "label13";
			this.label13.LabelProp = global::Mono.Unix.Catalog.GetString ("Kernel          ");
			this.hbox26.Add (this.label13);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.hbox26 [this.label13]));
			w9.Position = 0;
			w9.Expand = false;
			w9.Fill = false;
			// Container child hbox26.Gtk.Box+BoxChild
			this.SAI_Kernel = new global::Gtk.Entry ();
			this.SAI_Kernel.Sensitive = false;
			this.SAI_Kernel.CanFocus = true;
			this.SAI_Kernel.Name = "SAI_Kernel";
			this.SAI_Kernel.IsEditable = true;
			this.SAI_Kernel.InvisibleChar = '•';
			this.hbox26.Add (this.SAI_Kernel);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.hbox26 [this.SAI_Kernel]));
			w10.Position = 2;
			this.vbox11.Add (this.hbox26);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.vbox11 [this.hbox26]));
			w11.Position = 3;
			w11.Expand = false;
			w11.Fill = false;
			// Container child vbox11.Gtk.Box+BoxChild
			this.hbox27 = new global::Gtk.HBox ();
			this.hbox27.Name = "hbox27";
			this.hbox27.Spacing = 6;
			// Container child hbox27.Gtk.Box+BoxChild
			this.label14 = new global::Gtk.Label ();
			this.label14.Name = "label14";
			this.label14.LabelProp = global::Mono.Unix.Catalog.GetString ("Shell             ");
			this.hbox27.Add (this.label14);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.hbox27 [this.label14]));
			w12.Position = 0;
			w12.Expand = false;
			w12.Fill = false;
			// Container child hbox27.Gtk.Box+BoxChild
			this.SAI_Shell = new global::Gtk.Entry ();
			this.SAI_Shell.Sensitive = false;
			this.SAI_Shell.CanFocus = true;
			this.SAI_Shell.Name = "SAI_Shell";
			this.SAI_Shell.IsEditable = true;
			this.SAI_Shell.InvisibleChar = '•';
			this.hbox27.Add (this.SAI_Shell);
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.hbox27 [this.SAI_Shell]));
			w13.Position = 2;
			this.vbox11.Add (this.hbox27);
			global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.vbox11 [this.hbox27]));
			w14.Position = 4;
			w14.Expand = false;
			w14.Fill = false;
			// Container child vbox11.Gtk.Box+BoxChild
			this.hbox28 = new global::Gtk.HBox ();
			this.hbox28.Name = "hbox28";
			this.hbox28.Spacing = 6;
			// Container child hbox28.Gtk.Box+BoxChild
			this.label15 = new global::Gtk.Label ();
			this.label15.Name = "label15";
			this.label15.LabelProp = global::Mono.Unix.Catalog.GetString ("Language    ");
			this.hbox28.Add (this.label15);
			global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.hbox28 [this.label15]));
			w15.Position = 0;
			w15.Expand = false;
			w15.Fill = false;
			// Container child hbox28.Gtk.Box+BoxChild
			this.label16 = new global::Gtk.Label ();
			this.label16.Name = "label16";
			this.label16.LabelProp = global::Mono.Unix.Catalog.GetString ("Computeur should be restarted");
			this.hbox28.Add (this.label16);
			global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(this.hbox28 [this.label16]));
			w16.Position = 1;
			w16.Expand = false;
			w16.Fill = false;
			// Container child hbox28.Gtk.Box+BoxChild
			this.CBO_Locale = global::Gtk.ComboBoxEntry.NewText ();
			this.CBO_Locale.Name = "CBO_Locale";
			this.hbox28.Add (this.CBO_Locale);
			global::Gtk.Box.BoxChild w17 = ((global::Gtk.Box.BoxChild)(this.hbox28 [this.CBO_Locale]));
			w17.Position = 2;
			w17.Expand = false;
			w17.Fill = false;
			this.vbox11.Add (this.hbox28);
			global::Gtk.Box.BoxChild w18 = ((global::Gtk.Box.BoxChild)(this.vbox11 [this.hbox28]));
			w18.Position = 5;
			w18.Expand = false;
			w18.Fill = false;
			// Container child vbox11.Gtk.Box+BoxChild
			this.hbox2 = new global::Gtk.HBox ();
			this.hbox2.Name = "hbox2";
			this.hbox2.Spacing = 6;
			// Container child hbox2.Gtk.Box+BoxChild
			this.label18 = new global::Gtk.Label ();
			this.label18.Name = "label18";
			this.label18.LabelProp = global::Mono.Unix.Catalog.GetString ("Time config");
			this.hbox2.Add (this.label18);
			global::Gtk.Box.BoxChild w19 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.label18]));
			w19.Position = 0;
			w19.Expand = false;
			w19.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.CBO_Time = global::Gtk.ComboBoxEntry.NewText ();
			this.CBO_Time.Name = "CBO_Time";
			this.hbox2.Add (this.CBO_Time);
			global::Gtk.Box.BoxChild w20 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.CBO_Time]));
			w20.Position = 1;
			w20.Expand = false;
			w20.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.label17 = new global::Gtk.Label ();
			this.label17.Name = "label17";
			this.label17.LabelProp = global::Mono.Unix.Catalog.GetString ("Keymap");
			this.hbox2.Add (this.label17);
			global::Gtk.Box.BoxChild w21 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.label17]));
			w21.Position = 2;
			w21.Expand = false;
			w21.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.CBO_Keymap = global::Gtk.ComboBoxEntry.NewText ();
			this.CBO_Keymap.Name = "CBO_Keymap";
			this.hbox2.Add (this.CBO_Keymap);
			global::Gtk.Box.BoxChild w22 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.CBO_Keymap]));
			w22.Position = 3;
			w22.Expand = false;
			w22.Fill = false;
			this.vbox11.Add (this.hbox2);
			global::Gtk.Box.BoxChild w23 = ((global::Gtk.Box.BoxChild)(this.vbox11 [this.hbox2]));
			w23.Position = 6;
			w23.Expand = false;
			w23.Fill = false;
			// Container child vbox11.Gtk.Box+BoxChild
			this.hbox29 = new global::Gtk.HBox ();
			this.hbox29.Name = "hbox29";
			this.hbox29.Spacing = 6;
			// Container child hbox29.Gtk.Box+BoxChild
			this.LIB_Root = new global::Gtk.Label ();
			this.LIB_Root.Name = "LIB_Root";
			this.LIB_Root.LabelProp = global::Mono.Unix.Catalog.GetString ("Can't save, should be started as root");
			this.hbox29.Add (this.LIB_Root);
			global::Gtk.Box.BoxChild w24 = ((global::Gtk.Box.BoxChild)(this.hbox29 [this.LIB_Root]));
			w24.Position = 0;
			w24.Expand = false;
			w24.Fill = false;
			// Container child hbox29.Gtk.Box+BoxChild
			this.BTN_System = new global::Gtk.Button ();
			this.BTN_System.CanFocus = true;
			this.BTN_System.Name = "BTN_System";
			this.BTN_System.UseUnderline = true;
			// Container child BTN_System.Gtk.Container+ContainerChild
			global::Gtk.Alignment w25 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w26 = new global::Gtk.HBox ();
			w26.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w27 = new global::Gtk.Image ();
			w27.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-apply", global::Gtk.IconSize.Menu);
			w26.Add (w27);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w29 = new global::Gtk.Label ();
			w29.LabelProp = global::Mono.Unix.Catalog.GetString ("Apply");
			w29.UseUnderline = true;
			w26.Add (w29);
			w25.Add (w26);
			this.BTN_System.Add (w25);
			this.hbox29.Add (this.BTN_System);
			global::Gtk.Box.BoxChild w33 = ((global::Gtk.Box.BoxChild)(this.hbox29 [this.BTN_System]));
			w33.PackType = ((global::Gtk.PackType)(1));
			w33.Position = 2;
			w33.Expand = false;
			w33.Fill = false;
			this.vbox11.Add (this.hbox29);
			global::Gtk.Box.BoxChild w34 = ((global::Gtk.Box.BoxChild)(this.vbox11 [this.hbox29]));
			w34.Position = 7;
			w34.Expand = false;
			w34.Fill = false;
			this.Add (this.vbox11);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Hide ();
			this.BTN_System.Clicked += new global::System.EventHandler (this.OnBTNSystemClicked);
		}
	}
}
