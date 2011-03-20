
// This file has been generated by the GUI designer. Do not modify.
namespace frugalmonotools
{
	public partial class WID_Notes
	{
		private global::Gtk.Notebook notebook2;
		private global::Gtk.VBox vbox1;
		private global::Gtk.ScrolledWindow GtkScrolledWindow;
		private global::Gtk.TextView TXT_Note;
		private global::Gtk.Label LIB_Info;
		private global::Gtk.HBox hbox1;
		private global::Gtk.Button BTN_Download;
		private global::Gtk.Button BTN_Send;
		private global::Gtk.Button BTN_Save;
		private global::Gtk.Label label1;
		private global::Gtk.VBox vbox2;
		private global::Gtk.Label label3;
		private global::Gtk.Entry SAI_Login;
		private global::Gtk.Entry SAI_Pass;
		private global::Gtk.HBox hbox2;
		private global::Gtk.Button BTN_SaveOptions;
		private global::Gtk.Label label2;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget frugalmonotools.WID_Notes
			global::Stetic.BinContainer.Attach (this);
			this.Name = "frugalmonotools.WID_Notes";
			// Container child frugalmonotools.WID_Notes.Gtk.Container+ContainerChild
			this.notebook2 = new global::Gtk.Notebook ();
			this.notebook2.CanFocus = true;
			this.notebook2.Name = "notebook2";
			this.notebook2.CurrentPage = 0;
			// Container child notebook2.Gtk.Notebook+NotebookChild
			this.vbox1 = new global::Gtk.VBox ();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.TXT_Note = new global::Gtk.TextView ();
			this.TXT_Note.CanFocus = true;
			this.TXT_Note.Name = "TXT_Note";
			this.GtkScrolledWindow.Add (this.TXT_Note);
			this.vbox1.Add (this.GtkScrolledWindow);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.GtkScrolledWindow]));
			w2.Position = 0;
			// Container child vbox1.Gtk.Box+BoxChild
			this.LIB_Info = new global::Gtk.Label ();
			this.LIB_Info.Name = "LIB_Info";
			this.LIB_Info.LabelProp = global::Mono.Unix.Catalog.GetString ("label3");
			this.vbox1.Add (this.LIB_Info);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.LIB_Info]));
			w3.Position = 1;
			w3.Expand = false;
			w3.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox ();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.BTN_Download = new global::Gtk.Button ();
			this.BTN_Download.CanFocus = true;
			this.BTN_Download.Name = "BTN_Download";
			this.BTN_Download.UseUnderline = true;
			// Container child BTN_Download.Gtk.Container+ContainerChild
			global::Gtk.Alignment w4 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w5 = new global::Gtk.HBox ();
			w5.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w6 = new global::Gtk.Image ();
			w6.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-go-down", global::Gtk.IconSize.Menu);
			w5.Add (w6);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w8 = new global::Gtk.Label ();
			w8.LabelProp = global::Mono.Unix.Catalog.GetString ("Get from server");
			w8.UseUnderline = true;
			w5.Add (w8);
			w4.Add (w5);
			this.BTN_Download.Add (w4);
			this.hbox1.Add (this.BTN_Download);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.BTN_Download]));
			w12.Position = 0;
			w12.Expand = false;
			w12.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.BTN_Send = new global::Gtk.Button ();
			this.BTN_Send.CanFocus = true;
			this.BTN_Send.Name = "BTN_Send";
			this.BTN_Send.UseUnderline = true;
			// Container child BTN_Send.Gtk.Container+ContainerChild
			global::Gtk.Alignment w13 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w14 = new global::Gtk.HBox ();
			w14.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w15 = new global::Gtk.Image ();
			w15.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-go-up", global::Gtk.IconSize.Menu);
			w14.Add (w15);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w17 = new global::Gtk.Label ();
			w17.LabelProp = global::Mono.Unix.Catalog.GetString ("Send to server");
			w17.UseUnderline = true;
			w14.Add (w17);
			w13.Add (w14);
			this.BTN_Send.Add (w13);
			this.hbox1.Add (this.BTN_Send);
			global::Gtk.Box.BoxChild w21 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.BTN_Send]));
			w21.Position = 1;
			w21.Expand = false;
			w21.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.BTN_Save = new global::Gtk.Button ();
			this.BTN_Save.CanFocus = true;
			this.BTN_Save.Name = "BTN_Save";
			this.BTN_Save.UseUnderline = true;
			// Container child BTN_Save.Gtk.Container+ContainerChild
			global::Gtk.Alignment w22 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w23 = new global::Gtk.HBox ();
			w23.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w24 = new global::Gtk.Image ();
			w24.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-save", global::Gtk.IconSize.Menu);
			w23.Add (w24);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w26 = new global::Gtk.Label ();
			w26.LabelProp = global::Mono.Unix.Catalog.GetString ("Save");
			w26.UseUnderline = true;
			w23.Add (w26);
			w22.Add (w23);
			this.BTN_Save.Add (w22);
			this.hbox1.Add (this.BTN_Save);
			global::Gtk.Box.BoxChild w30 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.BTN_Save]));
			w30.Position = 2;
			w30.Expand = false;
			w30.Fill = false;
			this.vbox1.Add (this.hbox1);
			global::Gtk.Box.BoxChild w31 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.hbox1]));
			w31.Position = 2;
			w31.Expand = false;
			w31.Fill = false;
			this.notebook2.Add (this.vbox1);
			// Notebook tab
			this.label1 = new global::Gtk.Label ();
			this.label1.Name = "label1";
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString ("Note");
			this.notebook2.SetTabLabel (this.vbox1, this.label1);
			this.label1.ShowAll ();
			// Container child notebook2.Gtk.Notebook+NotebookChild
			this.vbox2 = new global::Gtk.VBox ();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.label3 = new global::Gtk.Label ();
			this.label3.Name = "label3";
			this.label3.LabelProp = global::Mono.Unix.Catalog.GetString ("You can synchronise with Internet. Just choose a login/user.\nInscription is free.");
			this.vbox2.Add (this.label3);
			global::Gtk.Box.BoxChild w33 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.label3]));
			w33.Position = 0;
			w33.Expand = false;
			w33.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.SAI_Login = new global::Gtk.Entry ();
			this.SAI_Login.CanFocus = true;
			this.SAI_Login.Name = "SAI_Login";
			this.SAI_Login.Text = global::Mono.Unix.Catalog.GetString ("login");
			this.SAI_Login.IsEditable = true;
			this.SAI_Login.InvisibleChar = '•';
			this.vbox2.Add (this.SAI_Login);
			global::Gtk.Box.BoxChild w34 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.SAI_Login]));
			w34.Position = 1;
			w34.Expand = false;
			w34.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.SAI_Pass = new global::Gtk.Entry ();
			this.SAI_Pass.CanFocus = true;
			this.SAI_Pass.Name = "SAI_Pass";
			this.SAI_Pass.Text = global::Mono.Unix.Catalog.GetString ("password");
			this.SAI_Pass.IsEditable = true;
			this.SAI_Pass.Visibility = false;
			this.SAI_Pass.InvisibleChar = '•';
			this.vbox2.Add (this.SAI_Pass);
			global::Gtk.Box.BoxChild w35 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.SAI_Pass]));
			w35.Position = 2;
			w35.Expand = false;
			w35.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox2 = new global::Gtk.HBox ();
			this.hbox2.Name = "hbox2";
			this.hbox2.Spacing = 6;
			// Container child hbox2.Gtk.Box+BoxChild
			this.BTN_SaveOptions = new global::Gtk.Button ();
			this.BTN_SaveOptions.CanFocus = true;
			this.BTN_SaveOptions.Name = "BTN_SaveOptions";
			this.BTN_SaveOptions.UseUnderline = true;
			// Container child BTN_SaveOptions.Gtk.Container+ContainerChild
			global::Gtk.Alignment w36 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w37 = new global::Gtk.HBox ();
			w37.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w38 = new global::Gtk.Image ();
			w38.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-save", global::Gtk.IconSize.Menu);
			w37.Add (w38);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w40 = new global::Gtk.Label ();
			w40.LabelProp = global::Mono.Unix.Catalog.GetString ("Save");
			w40.UseUnderline = true;
			w37.Add (w40);
			w36.Add (w37);
			this.BTN_SaveOptions.Add (w36);
			this.hbox2.Add (this.BTN_SaveOptions);
			global::Gtk.Box.BoxChild w44 = ((global::Gtk.Box.BoxChild)(this.hbox2 [this.BTN_SaveOptions]));
			w44.Position = 2;
			w44.Expand = false;
			w44.Fill = false;
			this.vbox2.Add (this.hbox2);
			global::Gtk.Box.BoxChild w45 = ((global::Gtk.Box.BoxChild)(this.vbox2 [this.hbox2]));
			w45.Position = 4;
			w45.Expand = false;
			w45.Fill = false;
			this.notebook2.Add (this.vbox2);
			global::Gtk.Notebook.NotebookChild w46 = ((global::Gtk.Notebook.NotebookChild)(this.notebook2 [this.vbox2]));
			w46.Position = 1;
			// Notebook tab
			this.label2 = new global::Gtk.Label ();
			this.label2.Name = "label2";
			this.label2.LabelProp = global::Mono.Unix.Catalog.GetString ("Options");
			this.notebook2.SetTabLabel (this.vbox2, this.label2);
			this.label2.ShowAll ();
			this.Add (this.notebook2);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Hide ();
			this.BTN_Download.Clicked += new global::System.EventHandler (this.OnBTNDownloadClicked);
			this.BTN_Send.Clicked += new global::System.EventHandler (this.OnBTNSendClicked);
			this.BTN_Save.Clicked += new global::System.EventHandler (this.OnBTNSaveClicked);
			this.BTN_SaveOptions.Clicked += new global::System.EventHandler (this.OnBTNSaveOptionsClicked);
		}
	}
}
