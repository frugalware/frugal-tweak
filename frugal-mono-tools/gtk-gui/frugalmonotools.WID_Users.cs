
// This file has been generated by the GUI designer. Do not modify.
namespace frugalmonotools
{
	public partial class WID_Users
	{
		private global::Gtk.Notebook notebook1;

		private global::Gtk.VBox vbox2;

		private global::Gtk.HBox hbox3;

		private global::Gtk.Image image30;

		private global::Gtk.ScrolledWindow GtkScrolledWindow;

		private global::Gtk.TreeView TREE_Users;

		private global::Gtk.HBox hbox1;

		private global::Gtk.Entry SAI_Name;

		private global::Gtk.Entry SAI_Comment;

		private global::Gtk.HBox hbox4;

		private global::Gtk.Entry SAI_Shell;

		private global::Gtk.Entry SAI_Home;

		private global::Gtk.Entry SAI_Groups;

		private global::Gtk.HBox hbox2;

		private global::Gtk.Button BTN_AddUser;

		private global::Gtk.Button BTN_Remove;

		private global::Gtk.Button BTN_Apply;

		private global::Gtk.Label label1;

		private global::Gtk.VBox vbox3;

		private global::Gtk.ScrolledWindow GtkScrolledWindow1;

		private global::Gtk.TreeView TREE_Groups;

		private global::Gtk.Label label2;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget frugalmonotools.WID_Users
			global::Stetic.BinContainer.Attach (this);
			this.Name = "frugalmonotools.WID_Users";
			// Container child frugalmonotools.WID_Users.Gtk.Container+ContainerChild
			this.notebook1 = new global::Gtk.Notebook ();
			this.notebook1.CanFocus = true;
			this.notebook1.Name = "notebook1";
			this.notebook1.CurrentPage = 0;
			// Container child notebook1.Gtk.Notebook+NotebookChild
			this.vbox2 = new global::Gtk.VBox ();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox3 = new global::Gtk.HBox ();
			this.hbox3.Name = "hbox3";
			this.hbox3.Spacing = 6;
			// Container child hbox3.Gtk.Box+BoxChild
			this.image30 = new global::Gtk.Image ();
			this.image30.Name = "image30";
			this.image30.Pixbuf = global::Gdk.Pixbuf.LoadFromResource ("frugalmonotools.Pictures.icons.users.png");
			this.hbox3.Add (this.image30);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.image30]));
			w1.Position = 2;
			w1.Expand = false;
			w1.Fill = false;
			this.vbox2.Add (this.hbox3);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hbox3]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.TREE_Users = new global::Gtk.TreeView ();
			this.TREE_Users.CanFocus = true;
			this.TREE_Users.Name = "TREE_Users";
			this.GtkScrolledWindow.Add (this.TREE_Users);
			this.vbox2.Add (this.GtkScrolledWindow);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.GtkScrolledWindow]));
			w4.Position = 1;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox ();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.SAI_Name = new global::Gtk.Entry ();
			this.SAI_Name.CanFocus = true;
			this.SAI_Name.Name = "SAI_Name";
			this.SAI_Name.IsEditable = true;
			this.SAI_Name.InvisibleChar = '•';
			this.hbox1.Add (this.SAI_Name);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.SAI_Name]));
			w5.Position = 0;
			// Container child hbox1.Gtk.Box+BoxChild
			this.SAI_Comment = new global::Gtk.Entry ();
			this.SAI_Comment.CanFocus = true;
			this.SAI_Comment.Name = "SAI_Comment";
			this.SAI_Comment.IsEditable = true;
			this.SAI_Comment.InvisibleChar = '•';
			this.hbox1.Add (this.SAI_Comment);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.SAI_Comment]));
			w6.Position = 2;
			this.vbox2.Add (this.hbox1);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hbox1]));
			w7.Position = 2;
			w7.Expand = false;
			w7.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox4 = new global::Gtk.HBox ();
			this.hbox4.Name = "hbox4";
			this.hbox4.Spacing = 6;
			// Container child hbox4.Gtk.Box+BoxChild
			this.SAI_Shell = new global::Gtk.Entry ();
			this.SAI_Shell.CanFocus = true;
			this.SAI_Shell.Name = "SAI_Shell";
			this.SAI_Shell.IsEditable = true;
			this.SAI_Shell.InvisibleChar = '•';
			this.hbox4.Add (this.SAI_Shell);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.hbox4[this.SAI_Shell]));
			w8.Position = 0;
			// Container child hbox4.Gtk.Box+BoxChild
			this.SAI_Home = new global::Gtk.Entry ();
			this.SAI_Home.CanFocus = true;
			this.SAI_Home.Name = "SAI_Home";
			this.SAI_Home.IsEditable = true;
			this.SAI_Home.InvisibleChar = '•';
			this.hbox4.Add (this.SAI_Home);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.hbox4[this.SAI_Home]));
			w9.Position = 2;
			this.vbox2.Add (this.hbox4);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hbox4]));
			w10.Position = 3;
			w10.Expand = false;
			w10.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.SAI_Groups = new global::Gtk.Entry ();
			this.SAI_Groups.CanFocus = true;
			this.SAI_Groups.Name = "SAI_Groups";
			this.SAI_Groups.IsEditable = true;
			this.SAI_Groups.InvisibleChar = '•';
			this.vbox2.Add (this.SAI_Groups);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.SAI_Groups]));
			w11.Position = 4;
			w11.Expand = false;
			w11.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox2 = new global::Gtk.HBox ();
			this.hbox2.Name = "hbox2";
			this.hbox2.Spacing = 6;
			// Container child hbox2.Gtk.Box+BoxChild
			this.BTN_AddUser = new global::Gtk.Button ();
			this.BTN_AddUser.CanFocus = true;
			this.BTN_AddUser.Name = "BTN_AddUser";
			this.BTN_AddUser.UseUnderline = true;
			// Container child BTN_AddUser.Gtk.Container+ContainerChild
			global::Gtk.Alignment w12 = new global::Gtk.Alignment (0.5f, 0.5f, 0f, 0f);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w13 = new global::Gtk.HBox ();
			w13.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w14 = new global::Gtk.Image ();
			w14.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-add", global::Gtk.IconSize.Menu);
			w13.Add (w14);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w16 = new global::Gtk.Label ();
			w16.LabelProp = global::Mono.Unix.Catalog.GetString ("Add new user");
			w16.UseUnderline = true;
			w13.Add (w16);
			w12.Add (w13);
			this.BTN_AddUser.Add (w12);
			this.hbox2.Add (this.BTN_AddUser);
			global::Gtk.Box.BoxChild w20 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.BTN_AddUser]));
			w20.Position = 0;
			w20.Expand = false;
			w20.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.BTN_Remove = new global::Gtk.Button ();
			this.BTN_Remove.CanFocus = true;
			this.BTN_Remove.Name = "BTN_Remove";
			this.BTN_Remove.UseUnderline = true;
			// Container child BTN_Remove.Gtk.Container+ContainerChild
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
			w25.LabelProp = global::Mono.Unix.Catalog.GetString ("Remove user");
			w25.UseUnderline = true;
			w22.Add (w25);
			w21.Add (w22);
			this.BTN_Remove.Add (w21);
			this.hbox2.Add (this.BTN_Remove);
			global::Gtk.Box.BoxChild w29 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.BTN_Remove]));
			w29.Position = 1;
			w29.Expand = false;
			w29.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.BTN_Apply = new global::Gtk.Button ();
			this.BTN_Apply.CanFocus = true;
			this.BTN_Apply.Name = "BTN_Apply";
			this.BTN_Apply.UseUnderline = true;
			// Container child BTN_Apply.Gtk.Container+ContainerChild
			global::Gtk.Alignment w30 = new global::Gtk.Alignment (0.5f, 0.5f, 0f, 0f);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w31 = new global::Gtk.HBox ();
			w31.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w32 = new global::Gtk.Image ();
			w32.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-apply", global::Gtk.IconSize.Menu);
			w31.Add (w32);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w34 = new global::Gtk.Label ();
			w34.LabelProp = global::Mono.Unix.Catalog.GetString ("Apply");
			w34.UseUnderline = true;
			w31.Add (w34);
			w30.Add (w31);
			this.BTN_Apply.Add (w30);
			this.hbox2.Add (this.BTN_Apply);
			global::Gtk.Box.BoxChild w38 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.BTN_Apply]));
			w38.Position = 2;
			w38.Expand = false;
			w38.Fill = false;
			this.vbox2.Add (this.hbox2);
			global::Gtk.Box.BoxChild w39 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hbox2]));
			w39.Position = 6;
			w39.Expand = false;
			w39.Fill = false;
			this.notebook1.Add (this.vbox2);
			// Notebook tab
			this.label1 = new global::Gtk.Label ();
			this.label1.Name = "label1";
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString ("Users");
			this.notebook1.SetTabLabel (this.vbox2, this.label1);
			this.label1.ShowAll ();
			// Container child notebook1.Gtk.Notebook+NotebookChild
			this.vbox3 = new global::Gtk.VBox ();
			this.vbox3.Name = "vbox3";
			this.vbox3.Spacing = 6;
			// Container child vbox3.Gtk.Box+BoxChild
			this.GtkScrolledWindow1 = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow1.Name = "GtkScrolledWindow1";
			this.GtkScrolledWindow1.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow1.Gtk.Container+ContainerChild
			this.TREE_Groups = new global::Gtk.TreeView ();
			this.TREE_Groups.CanFocus = true;
			this.TREE_Groups.Name = "TREE_Groups";
			this.GtkScrolledWindow1.Add (this.TREE_Groups);
			this.vbox3.Add (this.GtkScrolledWindow1);
			global::Gtk.Box.BoxChild w42 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.GtkScrolledWindow1]));
			w42.Position = 0;
			this.notebook1.Add (this.vbox3);
			global::Gtk.Notebook.NotebookChild w43 = ((global::Gtk.Notebook.NotebookChild)(this.notebook1[this.vbox3]));
			w43.Position = 1;
			// Notebook tab
			this.label2 = new global::Gtk.Label ();
			this.label2.Name = "label2";
			this.label2.LabelProp = global::Mono.Unix.Catalog.GetString ("Groups");
			this.notebook1.SetTabLabel (this.vbox3, this.label2);
			this.label2.ShowAll ();
			this.Add (this.notebook1);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Hide ();
		}
	}
}
