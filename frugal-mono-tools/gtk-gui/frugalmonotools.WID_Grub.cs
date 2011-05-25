
// This file has been generated by the GUI designer. Do not modify.
namespace frugalmonotools
{
	public partial class WID_Grub
	{
		private global::Gtk.VBox vbox2;
		private global::Gtk.Notebook notebook1;
		private global::Gtk.VBox vbox1;
		private global::Gtk.ComboBox CBO_Entry;
		private global::Gtk.Label LIB_Title;
		private global::Gtk.Entry SAI_Title;
		private global::Gtk.ScrolledWindow GtkScrolledWindow;
		private global::Gtk.TextView TXT_Options;
		private global::Gtk.HBox hbox2;
		private global::Gtk.Button BTN_Modify;
		private global::Gtk.Button BTN_RemoveEntry;
		private global::Gtk.Button BTN_AddEntry;
		private global::Gtk.Label Entrys;
		private global::Gtk.VBox vbox3;
		private global::Gtk.HBox hbox3;
		private global::Gtk.Label label3;
		private global::Gtk.Entry SAI_Default;
		private global::Gtk.HBox hbox4;
		private global::Gtk.Label label4;
		private global::Gtk.Entry SAI_TimeOut;
		private global::Gtk.HBox hbox5;
		private global::Gtk.Label label5;
		private global::Gtk.Entry SAI_Gfx;
		private global::Gtk.Label label2;
		private global::Gtk.HBox hbox6;
		private global::Gtk.Label LIB_Root;
		private global::Gtk.Button BTN_Save;
		private global::Gtk.Button BTN_Apply;
		private global::Gtk.Entry SAI_Hdd;
        
		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget frugalmonotools.WID_Grub
			global::Stetic.BinContainer.Attach (this);
			this.Name = "frugalmonotools.WID_Grub";
			// Container child frugalmonotools.WID_Grub.Gtk.Container+ContainerChild
			this.vbox2 = new global::Gtk.VBox ();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.notebook1 = new global::Gtk.Notebook ();
			this.notebook1.CanFocus = true;
			this.notebook1.Name = "notebook1";
			this.notebook1.CurrentPage = 0;
			// Container child notebook1.Gtk.Notebook+NotebookChild
			this.vbox1 = new global::Gtk.VBox ();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.CBO_Entry = global::Gtk.ComboBox.NewText ();
			this.CBO_Entry.Name = "CBO_Entry";
			this.vbox1.Add (this.CBO_Entry);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.CBO_Entry]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.LIB_Title = new global::Gtk.Label ();
			this.LIB_Title.Name = "LIB_Title";
			this.LIB_Title.LabelProp = global::Mono.Unix.Catalog.GetString ("Title");
			this.vbox1.Add (this.LIB_Title);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.LIB_Title]));
			w2.Position = 1;
			w2.Expand = false;
			w2.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.SAI_Title = new global::Gtk.Entry ();
			this.SAI_Title.CanFocus = true;
			this.SAI_Title.Name = "SAI_Title";
			this.SAI_Title.IsEditable = true;
			this.SAI_Title.InvisibleChar = '•';
			this.vbox1.Add (this.SAI_Title);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.SAI_Title]));
			w3.Position = 2;
			w3.Expand = false;
			w3.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.TXT_Options = new global::Gtk.TextView ();
			this.TXT_Options.CanFocus = true;
			this.TXT_Options.Name = "TXT_Options";
			this.GtkScrolledWindow.Add (this.TXT_Options);
			this.vbox1.Add (this.GtkScrolledWindow);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.GtkScrolledWindow]));
			w5.Position = 3;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox2 = new global::Gtk.HBox ();
			this.hbox2.Name = "hbox2";
			this.hbox2.Spacing = 6;
			// Container child hbox2.Gtk.Box+BoxChild
			this.BTN_Modify = new global::Gtk.Button ();
			this.BTN_Modify.CanFocus = true;
			this.BTN_Modify.Name = "BTN_Modify";
			this.BTN_Modify.UseUnderline = true;
			// Container child BTN_Modify.Gtk.Container+ContainerChild
			global::Gtk.Alignment w6 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w7 = new global::Gtk.HBox ();
			w7.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w8 = new global::Gtk.Image ();
			w8.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-apply", global::Gtk.IconSize.Menu);
			w7.Add (w8);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w10 = new global::Gtk.Label ();
			w10.LabelProp = global::Mono.Unix.Catalog.GetString ("Modify entry");
			w10.UseUnderline = true;
			w7.Add (w10);
			w6.Add (w7);
			this.BTN_Modify.Add (w6);
			this.hbox2.Add (this.BTN_Modify);
			global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.BTN_Modify]));
			w14.Position = 0;
			w14.Expand = false;
			w14.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.BTN_RemoveEntry = new global::Gtk.Button ();
			this.BTN_RemoveEntry.CanFocus = true;
			this.BTN_RemoveEntry.Name = "BTN_RemoveEntry";
			this.BTN_RemoveEntry.UseUnderline = true;
			// Container child BTN_RemoveEntry.Gtk.Container+ContainerChild
			global::Gtk.Alignment w15 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w16 = new global::Gtk.HBox ();
			w16.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w17 = new global::Gtk.Image ();
			w17.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-remove", global::Gtk.IconSize.Menu);
			w16.Add (w17);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w19 = new global::Gtk.Label ();
			w19.LabelProp = global::Mono.Unix.Catalog.GetString ("Remove entry");
			w19.UseUnderline = true;
			w16.Add (w19);
			w15.Add (w16);
			this.BTN_RemoveEntry.Add (w15);
			this.hbox2.Add (this.BTN_RemoveEntry);
			global::Gtk.Box.BoxChild w23 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.BTN_RemoveEntry]));
			w23.Position = 1;
			w23.Expand = false;
			w23.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.BTN_AddEntry = new global::Gtk.Button ();
			this.BTN_AddEntry.CanFocus = true;
			this.BTN_AddEntry.Name = "BTN_AddEntry";
			this.BTN_AddEntry.UseUnderline = true;
			// Container child BTN_AddEntry.Gtk.Container+ContainerChild
			global::Gtk.Alignment w24 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w25 = new global::Gtk.HBox ();
			w25.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w26 = new global::Gtk.Image ();
			w26.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-add", global::Gtk.IconSize.Menu);
			w25.Add (w26);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w28 = new global::Gtk.Label ();
			w28.LabelProp = global::Mono.Unix.Catalog.GetString ("Add entry");
			w28.UseUnderline = true;
			w25.Add (w28);
			w24.Add (w25);
			this.BTN_AddEntry.Add (w24);
			this.hbox2.Add (this.BTN_AddEntry);
			global::Gtk.Box.BoxChild w32 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.BTN_AddEntry]));
			w32.Position = 2;
			w32.Expand = false;
			w32.Fill = false;
			this.vbox1.Add (this.hbox2);
			global::Gtk.Box.BoxChild w33 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.hbox2]));
			w33.Position = 4;
			w33.Expand = false;
			w33.Fill = false;
			this.notebook1.Add (this.vbox1);
			// Notebook tab
			this.Entrys = new global::Gtk.Label ();
			this.Entrys.Name = "Entrys";
			this.Entrys.LabelProp = global::Mono.Unix.Catalog.GetString ("Entrys");
			this.notebook1.SetTabLabel (this.vbox1, this.Entrys);
			this.Entrys.ShowAll ();
			// Container child notebook1.Gtk.Notebook+NotebookChild
			this.vbox3 = new global::Gtk.VBox ();
			this.vbox3.Name = "vbox3";
			this.vbox3.Spacing = 6;
			// Container child vbox3.Gtk.Box+BoxChild
			this.hbox3 = new global::Gtk.HBox ();
			this.hbox3.Name = "hbox3";
			this.hbox3.Spacing = 6;
			// Container child hbox3.Gtk.Box+BoxChild
			this.label3 = new global::Gtk.Label ();
			this.label3.Name = "label3";
			this.label3.LabelProp = global::Mono.Unix.Catalog.GetString ("Default");
			this.hbox3.Add (this.label3);
			global::Gtk.Box.BoxChild w35 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.label3]));
			w35.Position = 0;
			w35.Expand = false;
			w35.Fill = false;
			// Container child hbox3.Gtk.Box+BoxChild
			this.SAI_Default = new global::Gtk.Entry ();
			this.SAI_Default.CanFocus = true;
			this.SAI_Default.Name = "SAI_Default";
			this.SAI_Default.IsEditable = true;
			this.SAI_Default.InvisibleChar = '•';
			this.hbox3.Add (this.SAI_Default);
			global::Gtk.Box.BoxChild w36 = ((global::Gtk.Box.BoxChild)(this.hbox3 [this.SAI_Default]));
			w36.Position = 1;
			this.vbox3.Add (this.hbox3);
			global::Gtk.Box.BoxChild w37 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.hbox3]));
			w37.Position = 0;
			w37.Expand = false;
			w37.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.hbox4 = new global::Gtk.HBox ();
			this.hbox4.Name = "hbox4";
			this.hbox4.Spacing = 6;
			// Container child hbox4.Gtk.Box+BoxChild
			this.label4 = new global::Gtk.Label ();
			this.label4.Name = "label4";
			this.label4.LabelProp = global::Mono.Unix.Catalog.GetString ("Time out");
			this.hbox4.Add (this.label4);
			global::Gtk.Box.BoxChild w38 = ((global::Gtk.Box.BoxChild)(this.hbox4 [this.label4]));
			w38.Position = 0;
			w38.Expand = false;
			w38.Fill = false;
			// Container child hbox4.Gtk.Box+BoxChild
			this.SAI_TimeOut = new global::Gtk.Entry ();
			this.SAI_TimeOut.CanFocus = true;
			this.SAI_TimeOut.Name = "SAI_TimeOut";
			this.SAI_TimeOut.IsEditable = true;
			this.SAI_TimeOut.InvisibleChar = '•';
			this.hbox4.Add (this.SAI_TimeOut);
			global::Gtk.Box.BoxChild w39 = ((global::Gtk.Box.BoxChild)(this.hbox4 [this.SAI_TimeOut]));
			w39.Position = 1;
			this.vbox3.Add (this.hbox4);
			global::Gtk.Box.BoxChild w40 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.hbox4]));
			w40.Position = 1;
			w40.Expand = false;
			w40.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.hbox5 = new global::Gtk.HBox ();
			this.hbox5.Name = "hbox5";
			this.hbox5.Spacing = 6;
			// Container child hbox5.Gtk.Box+BoxChild
			this.label5 = new global::Gtk.Label ();
			this.label5.Name = "label5";
			this.label5.LabelProp = global::Mono.Unix.Catalog.GetString ("Gfx");
			this.hbox5.Add (this.label5);
			global::Gtk.Box.BoxChild w41 = ((global::Gtk.Box.BoxChild)(this.hbox5 [this.label5]));
			w41.Position = 0;
			w41.Expand = false;
			w41.Fill = false;
			// Container child hbox5.Gtk.Box+BoxChild
			this.SAI_Gfx = new global::Gtk.Entry ();
			this.SAI_Gfx.CanFocus = true;
			this.SAI_Gfx.Name = "SAI_Gfx";
			this.SAI_Gfx.IsEditable = true;
			this.SAI_Gfx.InvisibleChar = '•';
			this.hbox5.Add (this.SAI_Gfx);
			global::Gtk.Box.BoxChild w42 = ((global::Gtk.Box.BoxChild)(this.hbox5 [this.SAI_Gfx]));
			w42.Position = 1;
			this.vbox3.Add (this.hbox5);
			global::Gtk.Box.BoxChild w43 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.hbox5]));
			w43.Position = 2;
			w43.Expand = false;
			w43.Fill = false;
			this.notebook1.Add (this.vbox3);
			global::Gtk.Notebook.NotebookChild w44 = ((global::Gtk.Notebook.NotebookChild)(this.notebook1 [this.vbox3]));
			w44.Position = 1;
			// Notebook tab
			this.label2 = new global::Gtk.Label ();
			this.label2.Name = "label2";
			this.label2.LabelProp = global::Mono.Unix.Catalog.GetString ("Options");
			this.notebook1.SetTabLabel (this.vbox3, this.label2);
			this.label2.ShowAll ();
			this.vbox2.Add (this.notebook1);
			global::Gtk.Box.BoxChild w45 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.notebook1]));
			w45.Position = 0;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox6 = new global::Gtk.HBox ();
			this.hbox6.Name = "hbox6";
			this.hbox6.Spacing = 6;
			// Container child hbox6.Gtk.Box+BoxChild
			this.LIB_Root = new global::Gtk.Label ();
			this.LIB_Root.Name = "LIB_Root";
			this.LIB_Root.LabelProp = global::Mono.Unix.Catalog.GetString ("Can't save, should be started as root");
			this.hbox6.Add (this.LIB_Root);
			global::Gtk.Box.BoxChild w46 = ((global::Gtk.Box.BoxChild)(this.hbox6 [this.LIB_Root]));
			w46.Position = 0;
			w46.Expand = false;
			w46.Fill = false;
			// Container child hbox6.Gtk.Box+BoxChild
			this.BTN_Save = new global::Gtk.Button ();
			this.BTN_Save.CanFocus = true;
			this.BTN_Save.Name = "BTN_Save";
			this.BTN_Save.UseUnderline = true;
			// Container child BTN_Save.Gtk.Container+ContainerChild
			global::Gtk.Alignment w47 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w48 = new global::Gtk.HBox ();
			w48.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w49 = new global::Gtk.Image ();
			w49.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-save", global::Gtk.IconSize.Menu);
			w48.Add (w49);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w51 = new global::Gtk.Label ();
			w51.LabelProp = global::Mono.Unix.Catalog.GetString ("Save");
			w51.UseUnderline = true;
			w48.Add (w51);
			w47.Add (w48);
			this.BTN_Save.Add (w47);
			this.hbox6.Add (this.BTN_Save);
			global::Gtk.Box.BoxChild w55 = ((global::Gtk.Box.BoxChild)(this.hbox6 [this.BTN_Save]));
			w55.Position = 1;
			w55.Expand = false;
			w55.Fill = false;
			// Container child hbox6.Gtk.Box+BoxChild
			this.BTN_Apply = new global::Gtk.Button ();
			this.BTN_Apply.CanFocus = true;
			this.BTN_Apply.Name = "BTN_Apply";
			this.BTN_Apply.UseUnderline = true;
			// Container child BTN_Apply.Gtk.Container+ContainerChild
			global::Gtk.Alignment w56 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w57 = new global::Gtk.HBox ();
			w57.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w58 = new global::Gtk.Image ();
			w58.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-apply", global::Gtk.IconSize.Menu);
			w57.Add (w58);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w60 = new global::Gtk.Label ();
			w60.LabelProp = global::Mono.Unix.Catalog.GetString ("Install Grub to");
			w60.UseUnderline = true;
			w57.Add (w60);
			w56.Add (w57);
			this.BTN_Apply.Add (w56);
			this.hbox6.Add (this.BTN_Apply);
			global::Gtk.Box.BoxChild w64 = ((global::Gtk.Box.BoxChild)(this.hbox6 [this.BTN_Apply]));
			w64.Position = 2;
			w64.Expand = false;
			w64.Fill = false;
			// Container child hbox6.Gtk.Box+BoxChild
			this.SAI_Hdd = new global::Gtk.Entry ();
			this.SAI_Hdd.CanFocus = true;
			this.SAI_Hdd.Name = "SAI_Hdd";
			this.SAI_Hdd.Text = global::Mono.Unix.Catalog.GetString ("/dev/sda");
			this.SAI_Hdd.IsEditable = true;
			this.SAI_Hdd.InvisibleChar = '•';
			this.hbox6.Add (this.SAI_Hdd);
			global::Gtk.Box.BoxChild w65 = ((global::Gtk.Box.BoxChild)(this.hbox6 [this.SAI_Hdd]));
			w65.PackType = ((global::Gtk.PackType)(1));
			w65.Position = 3;
			this.vbox2.Add (this.hbox6);
			global::Gtk.Box.BoxChild w66 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.hbox6]));
			w66.Position = 1;
			w66.Expand = false;
			w66.Fill = false;
			this.Add (this.vbox2);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.LIB_Root.Hide ();
			this.Hide ();
			this.CBO_Entry.Changed += new global::System.EventHandler (this.OnCBOEntryChanged);
			this.BTN_Modify.Clicked += new global::System.EventHandler (this.OnBTNModifyClicked);
			this.BTN_RemoveEntry.Clicked += new global::System.EventHandler (this.OnBTNRemoveEntryClicked);
			this.BTN_AddEntry.Clicked += new global::System.EventHandler (this.OnBTNAddEntryClicked);
			this.BTN_Save.Clicked += new global::System.EventHandler (this.OnBTNSaveClicked);
			this.BTN_Apply.Clicked += new global::System.EventHandler (this.OnBTNApplyClicked);
		}
	}
}
