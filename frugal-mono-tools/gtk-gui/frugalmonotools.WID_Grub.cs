
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

		private global::Gtk.Button BTN_RemoveEntry;

		private global::Gtk.Button BTN_AddEntry;

		private global::Gtk.Button BTN_Cancel;

		private global::Gtk.Button BTN_Save;

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

		private global::Gtk.Button BTN_Apply;

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
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.CBO_Entry]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.LIB_Title = new global::Gtk.Label ();
			this.LIB_Title.Name = "LIB_Title";
			this.LIB_Title.LabelProp = global::Mono.Unix.Catalog.GetString ("Title");
			this.vbox1.Add (this.LIB_Title);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.LIB_Title]));
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
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.SAI_Title]));
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
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.GtkScrolledWindow]));
			w5.Position = 3;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox2 = new global::Gtk.HBox ();
			this.hbox2.Name = "hbox2";
			this.hbox2.Spacing = 6;
			// Container child hbox2.Gtk.Box+BoxChild
			this.BTN_RemoveEntry = new global::Gtk.Button ();
			this.BTN_RemoveEntry.CanFocus = true;
			this.BTN_RemoveEntry.Name = "BTN_RemoveEntry";
			this.BTN_RemoveEntry.UseUnderline = true;
			// Container child BTN_RemoveEntry.Gtk.Container+ContainerChild
			global::Gtk.Alignment w6 = new global::Gtk.Alignment (0.5f, 0.5f, 0f, 0f);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w7 = new global::Gtk.HBox ();
			w7.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w8 = new global::Gtk.Image ();
			w8.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-remove", global::Gtk.IconSize.Menu);
			w7.Add (w8);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w10 = new global::Gtk.Label ();
			w10.LabelProp = global::Mono.Unix.Catalog.GetString ("Remove entry");
			w10.UseUnderline = true;
			w7.Add (w10);
			w6.Add (w7);
			this.BTN_RemoveEntry.Add (w6);
			this.hbox2.Add (this.BTN_RemoveEntry);
			global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.BTN_RemoveEntry]));
			w14.Position = 0;
			w14.Expand = false;
			w14.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.BTN_AddEntry = new global::Gtk.Button ();
			this.BTN_AddEntry.CanFocus = true;
			this.BTN_AddEntry.Name = "BTN_AddEntry";
			this.BTN_AddEntry.UseUnderline = true;
			// Container child BTN_AddEntry.Gtk.Container+ContainerChild
			global::Gtk.Alignment w15 = new global::Gtk.Alignment (0.5f, 0.5f, 0f, 0f);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w16 = new global::Gtk.HBox ();
			w16.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w17 = new global::Gtk.Image ();
			w17.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-add", global::Gtk.IconSize.Menu);
			w16.Add (w17);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w19 = new global::Gtk.Label ();
			w19.LabelProp = global::Mono.Unix.Catalog.GetString ("Add entry");
			w19.UseUnderline = true;
			w16.Add (w19);
			w15.Add (w16);
			this.BTN_AddEntry.Add (w15);
			this.hbox2.Add (this.BTN_AddEntry);
			global::Gtk.Box.BoxChild w23 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.BTN_AddEntry]));
			w23.Position = 1;
			w23.Expand = false;
			w23.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.BTN_Cancel = new global::Gtk.Button ();
			this.BTN_Cancel.CanFocus = true;
			this.BTN_Cancel.Name = "BTN_Cancel";
			this.BTN_Cancel.UseUnderline = true;
			// Container child BTN_Cancel.Gtk.Container+ContainerChild
			global::Gtk.Alignment w24 = new global::Gtk.Alignment (0.5f, 0.5f, 0f, 0f);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w25 = new global::Gtk.HBox ();
			w25.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w26 = new global::Gtk.Image ();
			w26.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-cancel", global::Gtk.IconSize.Menu);
			w25.Add (w26);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w28 = new global::Gtk.Label ();
			w28.LabelProp = global::Mono.Unix.Catalog.GetString ("Cancel");
			w28.UseUnderline = true;
			w25.Add (w28);
			w24.Add (w25);
			this.BTN_Cancel.Add (w24);
			this.hbox2.Add (this.BTN_Cancel);
			global::Gtk.Box.BoxChild w32 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.BTN_Cancel]));
			w32.Position = 2;
			w32.Expand = false;
			w32.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.BTN_Save = new global::Gtk.Button ();
			this.BTN_Save.CanFocus = true;
			this.BTN_Save.Name = "BTN_Save";
			this.BTN_Save.UseUnderline = true;
			// Container child BTN_Save.Gtk.Container+ContainerChild
			global::Gtk.Alignment w33 = new global::Gtk.Alignment (0.5f, 0.5f, 0f, 0f);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w34 = new global::Gtk.HBox ();
			w34.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w35 = new global::Gtk.Image ();
			w35.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-save", global::Gtk.IconSize.Menu);
			w34.Add (w35);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w37 = new global::Gtk.Label ();
			w37.LabelProp = global::Mono.Unix.Catalog.GetString ("Save");
			w37.UseUnderline = true;
			w34.Add (w37);
			w33.Add (w34);
			this.BTN_Save.Add (w33);
			this.hbox2.Add (this.BTN_Save);
			global::Gtk.Box.BoxChild w41 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.BTN_Save]));
			w41.Position = 3;
			w41.Expand = false;
			w41.Fill = false;
			this.vbox1.Add (this.hbox2);
			global::Gtk.Box.BoxChild w42 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hbox2]));
			w42.Position = 4;
			w42.Expand = false;
			w42.Fill = false;
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
			global::Gtk.Box.BoxChild w44 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.label3]));
			w44.Position = 0;
			w44.Expand = false;
			w44.Fill = false;
			// Container child hbox3.Gtk.Box+BoxChild
			this.SAI_Default = new global::Gtk.Entry ();
			this.SAI_Default.CanFocus = true;
			this.SAI_Default.Name = "SAI_Default";
			this.SAI_Default.IsEditable = true;
			this.SAI_Default.InvisibleChar = '•';
			this.hbox3.Add (this.SAI_Default);
			global::Gtk.Box.BoxChild w45 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.SAI_Default]));
			w45.Position = 1;
			this.vbox3.Add (this.hbox3);
			global::Gtk.Box.BoxChild w46 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.hbox3]));
			w46.Position = 0;
			w46.Expand = false;
			w46.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.hbox4 = new global::Gtk.HBox ();
			this.hbox4.Name = "hbox4";
			this.hbox4.Spacing = 6;
			// Container child hbox4.Gtk.Box+BoxChild
			this.label4 = new global::Gtk.Label ();
			this.label4.Name = "label4";
			this.label4.LabelProp = global::Mono.Unix.Catalog.GetString ("Time out");
			this.hbox4.Add (this.label4);
			global::Gtk.Box.BoxChild w47 = ((global::Gtk.Box.BoxChild)(this.hbox4[this.label4]));
			w47.Position = 0;
			w47.Expand = false;
			w47.Fill = false;
			// Container child hbox4.Gtk.Box+BoxChild
			this.SAI_TimeOut = new global::Gtk.Entry ();
			this.SAI_TimeOut.CanFocus = true;
			this.SAI_TimeOut.Name = "SAI_TimeOut";
			this.SAI_TimeOut.IsEditable = true;
			this.SAI_TimeOut.InvisibleChar = '•';
			this.hbox4.Add (this.SAI_TimeOut);
			global::Gtk.Box.BoxChild w48 = ((global::Gtk.Box.BoxChild)(this.hbox4[this.SAI_TimeOut]));
			w48.Position = 1;
			this.vbox3.Add (this.hbox4);
			global::Gtk.Box.BoxChild w49 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.hbox4]));
			w49.Position = 1;
			w49.Expand = false;
			w49.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.hbox5 = new global::Gtk.HBox ();
			this.hbox5.Name = "hbox5";
			this.hbox5.Spacing = 6;
			// Container child hbox5.Gtk.Box+BoxChild
			this.label5 = new global::Gtk.Label ();
			this.label5.Name = "label5";
			this.label5.LabelProp = global::Mono.Unix.Catalog.GetString ("Gfx");
			this.hbox5.Add (this.label5);
			global::Gtk.Box.BoxChild w50 = ((global::Gtk.Box.BoxChild)(this.hbox5[this.label5]));
			w50.Position = 0;
			w50.Expand = false;
			w50.Fill = false;
			// Container child hbox5.Gtk.Box+BoxChild
			this.SAI_Gfx = new global::Gtk.Entry ();
			this.SAI_Gfx.CanFocus = true;
			this.SAI_Gfx.Name = "SAI_Gfx";
			this.SAI_Gfx.IsEditable = true;
			this.SAI_Gfx.InvisibleChar = '•';
			this.hbox5.Add (this.SAI_Gfx);
			global::Gtk.Box.BoxChild w51 = ((global::Gtk.Box.BoxChild)(this.hbox5[this.SAI_Gfx]));
			w51.Position = 1;
			this.vbox3.Add (this.hbox5);
			global::Gtk.Box.BoxChild w52 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.hbox5]));
			w52.Position = 2;
			w52.Expand = false;
			w52.Fill = false;
			this.notebook1.Add (this.vbox3);
			global::Gtk.Notebook.NotebookChild w53 = ((global::Gtk.Notebook.NotebookChild)(this.notebook1[this.vbox3]));
			w53.Position = 1;
			// Notebook tab
			this.label2 = new global::Gtk.Label ();
			this.label2.Name = "label2";
			this.label2.LabelProp = global::Mono.Unix.Catalog.GetString ("Options");
			this.notebook1.SetTabLabel (this.vbox3, this.label2);
			this.label2.ShowAll ();
			this.vbox2.Add (this.notebook1);
			global::Gtk.Box.BoxChild w54 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.notebook1]));
			w54.Position = 0;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox6 = new global::Gtk.HBox ();
			this.hbox6.Name = "hbox6";
			this.hbox6.Spacing = 6;
			// Container child hbox6.Gtk.Box+BoxChild
			this.BTN_Apply = new global::Gtk.Button ();
			this.BTN_Apply.CanFocus = true;
			this.BTN_Apply.Name = "BTN_Apply";
			this.BTN_Apply.UseUnderline = true;
			// Container child BTN_Apply.Gtk.Container+ContainerChild
			global::Gtk.Alignment w55 = new global::Gtk.Alignment (0.5f, 0.5f, 0f, 0f);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w56 = new global::Gtk.HBox ();
			w56.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w57 = new global::Gtk.Image ();
			w57.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-apply", global::Gtk.IconSize.Menu);
			w56.Add (w57);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w59 = new global::Gtk.Label ();
			w59.LabelProp = global::Mono.Unix.Catalog.GetString ("Apply");
			w59.UseUnderline = true;
			w56.Add (w59);
			w55.Add (w56);
			this.BTN_Apply.Add (w55);
			this.hbox6.Add (this.BTN_Apply);
			global::Gtk.Box.BoxChild w63 = ((global::Gtk.Box.BoxChild)(this.hbox6[this.BTN_Apply]));
			w63.PackType = ((global::Gtk.PackType)(1));
			w63.Position = 2;
			w63.Expand = false;
			w63.Fill = false;
			this.vbox2.Add (this.hbox6);
			global::Gtk.Box.BoxChild w64 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hbox6]));
			w64.Position = 1;
			w64.Expand = false;
			w64.Fill = false;
			this.Add (this.vbox2);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.BTN_Cancel.Hide ();
			this.Hide ();
			this.CBO_Entry.Changed += new global::System.EventHandler (this.OnCBOEntryChanged);
			this.BTN_RemoveEntry.Clicked += new global::System.EventHandler (this.OnBTNRemoveEntryClicked);
			this.BTN_AddEntry.Clicked += new global::System.EventHandler (this.OnBTNAddEntryClicked);
			this.BTN_Save.Clicked += new global::System.EventHandler (this.OnBTNSaveClicked);
			this.BTN_Apply.Clicked += new global::System.EventHandler (this.OnBTNApplyClicked);
		}
	}
}
