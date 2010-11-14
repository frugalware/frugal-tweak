
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

		private global::Gtk.HBox hbox5;

		private global::Gtk.Label label3;

		private global::Gtk.Entry SAI_Pass;

		private global::Gtk.ScrolledWindow GtkScrolledWindow2;

		private global::Gtk.TreeView TREE_UserGroup;

		private global::Gtk.HBox hbox2;

		private global::Gtk.Button BTN_AddUser;

		private global::Gtk.Button BTN_Remove;

		private global::Gtk.Button BTN_Apply;

		private global::Gtk.Label label1;

		private global::Gtk.VBox vbox3;

		private global::Gtk.ScrolledWindow GtkScrolledWindow1;

		private global::Gtk.TreeView TREE_Groups;

		private global::Gtk.Entry SAI_GroupName;

		private global::Gtk.HBox hbox6;

		private global::Gtk.Button BTN_AddGroup;

		private global::Gtk.Button BTN_RemoveGroup;

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
			this.SAI_Name.TooltipMarkup = "Name";
			this.SAI_Name.CanFocus = true;
			this.SAI_Name.Name = "SAI_Name";
			this.SAI_Name.IsEditable = true;
			this.SAI_Name.InvisibleChar = '•';
			this.hbox1.Add (this.SAI_Name);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.SAI_Name]));
			w5.Position = 0;
			// Container child hbox1.Gtk.Box+BoxChild
			this.SAI_Comment = new global::Gtk.Entry ();
			this.SAI_Comment.TooltipMarkup = "Comment";
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
			this.SAI_Shell.TooltipMarkup = "Shell";
			this.SAI_Shell.CanFocus = true;
			this.SAI_Shell.Name = "SAI_Shell";
			this.SAI_Shell.IsEditable = true;
			this.SAI_Shell.InvisibleChar = '•';
			this.hbox4.Add (this.SAI_Shell);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.hbox4[this.SAI_Shell]));
			w8.Position = 0;
			// Container child hbox4.Gtk.Box+BoxChild
			this.SAI_Home = new global::Gtk.Entry ();
			this.SAI_Home.TooltipMarkup = "Home";
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
			this.hbox5 = new global::Gtk.HBox ();
			this.hbox5.Name = "hbox5";
			this.hbox5.Spacing = 6;
			// Container child hbox5.Gtk.Box+BoxChild
			this.label3 = new global::Gtk.Label ();
			this.label3.Name = "label3";
			this.label3.LabelProp = global::Mono.Unix.Catalog.GetString ("Password :");
			this.hbox5.Add (this.label3);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.hbox5[this.label3]));
			w11.Position = 0;
			w11.Expand = false;
			w11.Fill = false;
			// Container child hbox5.Gtk.Box+BoxChild
			this.SAI_Pass = new global::Gtk.Entry ();
			this.SAI_Pass.TooltipMarkup = "Password";
			this.SAI_Pass.CanFocus = true;
			this.SAI_Pass.Name = "SAI_Pass";
			this.SAI_Pass.IsEditable = true;
			this.SAI_Pass.Visibility = false;
			this.SAI_Pass.InvisibleChar = '•';
			this.hbox5.Add (this.SAI_Pass);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.hbox5[this.SAI_Pass]));
			w12.Position = 1;
			this.vbox2.Add (this.hbox5);
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hbox5]));
			w13.Position = 4;
			w13.Expand = false;
			w13.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.GtkScrolledWindow2 = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow2.Name = "GtkScrolledWindow2";
			this.GtkScrolledWindow2.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow2.Gtk.Container+ContainerChild
			this.TREE_UserGroup = new global::Gtk.TreeView ();
			this.TREE_UserGroup.CanFocus = true;
			this.TREE_UserGroup.Name = "TREE_UserGroup";
			this.GtkScrolledWindow2.Add (this.TREE_UserGroup);
			this.vbox2.Add (this.GtkScrolledWindow2);
			global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.GtkScrolledWindow2]));
			w15.Position = 5;
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
			global::Gtk.Alignment w16 = new global::Gtk.Alignment (0.5f, 0.5f, 0f, 0f);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w17 = new global::Gtk.HBox ();
			w17.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w18 = new global::Gtk.Image ();
			w18.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-add", global::Gtk.IconSize.Menu);
			w17.Add (w18);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w20 = new global::Gtk.Label ();
			w20.LabelProp = global::Mono.Unix.Catalog.GetString ("Add user");
			w20.UseUnderline = true;
			w17.Add (w20);
			w16.Add (w17);
			this.BTN_AddUser.Add (w16);
			this.hbox2.Add (this.BTN_AddUser);
			global::Gtk.Box.BoxChild w24 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.BTN_AddUser]));
			w24.Position = 0;
			w24.Expand = false;
			w24.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.BTN_Remove = new global::Gtk.Button ();
			this.BTN_Remove.CanFocus = true;
			this.BTN_Remove.Name = "BTN_Remove";
			this.BTN_Remove.UseUnderline = true;
			// Container child BTN_Remove.Gtk.Container+ContainerChild
			global::Gtk.Alignment w25 = new global::Gtk.Alignment (0.5f, 0.5f, 0f, 0f);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w26 = new global::Gtk.HBox ();
			w26.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w27 = new global::Gtk.Image ();
			w27.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-delete", global::Gtk.IconSize.Menu);
			w26.Add (w27);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w29 = new global::Gtk.Label ();
			w29.LabelProp = global::Mono.Unix.Catalog.GetString ("Remove user");
			w29.UseUnderline = true;
			w26.Add (w29);
			w25.Add (w26);
			this.BTN_Remove.Add (w25);
			this.hbox2.Add (this.BTN_Remove);
			global::Gtk.Box.BoxChild w33 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.BTN_Remove]));
			w33.Position = 1;
			w33.Expand = false;
			w33.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.BTN_Apply = new global::Gtk.Button ();
			this.BTN_Apply.CanFocus = true;
			this.BTN_Apply.Name = "BTN_Apply";
			this.BTN_Apply.UseUnderline = true;
			// Container child BTN_Apply.Gtk.Container+ContainerChild
			global::Gtk.Alignment w34 = new global::Gtk.Alignment (0.5f, 0.5f, 0f, 0f);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w35 = new global::Gtk.HBox ();
			w35.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w36 = new global::Gtk.Image ();
			w36.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-apply", global::Gtk.IconSize.Menu);
			w35.Add (w36);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w38 = new global::Gtk.Label ();
			w38.LabelProp = global::Mono.Unix.Catalog.GetString ("Apply");
			w38.UseUnderline = true;
			w35.Add (w38);
			w34.Add (w35);
			this.BTN_Apply.Add (w34);
			this.hbox2.Add (this.BTN_Apply);
			global::Gtk.Box.BoxChild w42 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.BTN_Apply]));
			w42.Position = 2;
			w42.Expand = false;
			w42.Fill = false;
			this.vbox2.Add (this.hbox2);
			global::Gtk.Box.BoxChild w43 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hbox2]));
			w43.Position = 6;
			w43.Expand = false;
			w43.Fill = false;
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
			global::Gtk.Box.BoxChild w46 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.GtkScrolledWindow1]));
			w46.Position = 0;
			// Container child vbox3.Gtk.Box+BoxChild
			this.SAI_GroupName = new global::Gtk.Entry ();
			this.SAI_GroupName.CanFocus = true;
			this.SAI_GroupName.Name = "SAI_GroupName";
			this.SAI_GroupName.IsEditable = true;
			this.SAI_GroupName.InvisibleChar = '•';
			this.vbox3.Add (this.SAI_GroupName);
			global::Gtk.Box.BoxChild w47 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.SAI_GroupName]));
			w47.Position = 1;
			w47.Expand = false;
			w47.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.hbox6 = new global::Gtk.HBox ();
			this.hbox6.Name = "hbox6";
			this.hbox6.Spacing = 6;
			// Container child hbox6.Gtk.Box+BoxChild
			this.BTN_AddGroup = new global::Gtk.Button ();
			this.BTN_AddGroup.CanFocus = true;
			this.BTN_AddGroup.Name = "BTN_AddGroup";
			this.BTN_AddGroup.UseUnderline = true;
			// Container child BTN_AddGroup.Gtk.Container+ContainerChild
			global::Gtk.Alignment w48 = new global::Gtk.Alignment (0.5f, 0.5f, 0f, 0f);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w49 = new global::Gtk.HBox ();
			w49.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w50 = new global::Gtk.Image ();
			w50.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-add", global::Gtk.IconSize.Menu);
			w49.Add (w50);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w52 = new global::Gtk.Label ();
			w52.LabelProp = global::Mono.Unix.Catalog.GetString ("Add group");
			w52.UseUnderline = true;
			w49.Add (w52);
			w48.Add (w49);
			this.BTN_AddGroup.Add (w48);
			this.hbox6.Add (this.BTN_AddGroup);
			global::Gtk.Box.BoxChild w56 = ((global::Gtk.Box.BoxChild)(this.hbox6[this.BTN_AddGroup]));
			w56.Position = 0;
			w56.Expand = false;
			w56.Fill = false;
			// Container child hbox6.Gtk.Box+BoxChild
			this.BTN_RemoveGroup = new global::Gtk.Button ();
			this.BTN_RemoveGroup.CanFocus = true;
			this.BTN_RemoveGroup.Name = "BTN_RemoveGroup";
			this.BTN_RemoveGroup.UseUnderline = true;
			// Container child BTN_RemoveGroup.Gtk.Container+ContainerChild
			global::Gtk.Alignment w57 = new global::Gtk.Alignment (0.5f, 0.5f, 0f, 0f);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w58 = new global::Gtk.HBox ();
			w58.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w59 = new global::Gtk.Image ();
			w59.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-delete", global::Gtk.IconSize.Menu);
			w58.Add (w59);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w61 = new global::Gtk.Label ();
			w61.LabelProp = global::Mono.Unix.Catalog.GetString ("Remove group");
			w61.UseUnderline = true;
			w58.Add (w61);
			w57.Add (w58);
			this.BTN_RemoveGroup.Add (w57);
			this.hbox6.Add (this.BTN_RemoveGroup);
			global::Gtk.Box.BoxChild w65 = ((global::Gtk.Box.BoxChild)(this.hbox6[this.BTN_RemoveGroup]));
			w65.Position = 1;
			w65.Expand = false;
			w65.Fill = false;
			this.vbox3.Add (this.hbox6);
			global::Gtk.Box.BoxChild w66 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.hbox6]));
			w66.Position = 2;
			w66.Expand = false;
			w66.Fill = false;
			this.notebook1.Add (this.vbox3);
			global::Gtk.Notebook.NotebookChild w67 = ((global::Gtk.Notebook.NotebookChild)(this.notebook1[this.vbox3]));
			w67.Position = 1;
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
			this.BTN_AddUser.Clicked += new global::System.EventHandler (this.OnBTNAddUserClicked);
			this.BTN_Remove.Clicked += new global::System.EventHandler (this.OnBTNRemoveClicked);
			this.BTN_Apply.Clicked += new global::System.EventHandler (this.OnBTNApplyClicked);
			this.BTN_AddGroup.Clicked += new global::System.EventHandler (this.OnBTNAddGroupClicked);
			this.BTN_RemoveGroup.Clicked += new global::System.EventHandler (this.OnBTNRemoveGroupClicked);
		}
	}
}