
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.HBox hbox1;

	private global::Gtk.VBox vbox2;

	private global::Gtk.VBox vbox3;

	private global::Gtk.Entry SAI_Serveur;

	private global::Gtk.Entry SAI_Chan;

	private global::Gtk.HBox hbox3;

	private global::Gtk.Entry SAI_Port;

	private global::Gtk.Entry SAI_User;

	private global::Gtk.Button BTN_Connect;

	private global::Gtk.ScrolledWindow GtkScrolledWindow1;

	private global::Gtk.TextView TXT_Messages;

	private global::Gtk.HBox hbox2;

	private global::Gtk.Entry SAI_Envoi;

	private global::Gtk.Button BTN_Send;

	private global::Gtk.VBox vbox1;

	private global::Gtk.Image image1;

	private global::Gtk.ScrolledWindow GtkScrolledWindow;

	private global::Gtk.TreeView TREE_Users;

	protected virtual void Build ()
	{
		global::Stetic.Gui.Initialize (this);
		// Widget MainWindow
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString ("FrugalIRC");
		this.Icon = global::Gdk.Pixbuf.LoadFromResource ("frugalirc.fw.png");
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		this.DefaultWidth = 800;
		this.DefaultHeight = 400;
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.hbox1 = new global::Gtk.HBox ();
		this.hbox1.Name = "hbox1";
		this.hbox1.Spacing = 6;
		// Container child hbox1.Gtk.Box+BoxChild
		this.vbox2 = new global::Gtk.VBox ();
		this.vbox2.Name = "vbox2";
		this.vbox2.Spacing = 6;
		// Container child vbox2.Gtk.Box+BoxChild
		this.vbox3 = new global::Gtk.VBox ();
		this.vbox3.Name = "vbox3";
		this.vbox3.Spacing = 6;
		// Container child vbox3.Gtk.Box+BoxChild
		this.SAI_Serveur = new global::Gtk.Entry ();
		this.SAI_Serveur.CanFocus = true;
		this.SAI_Serveur.Name = "SAI_Serveur";
		this.SAI_Serveur.Text = global::Mono.Unix.Catalog.GetString ("irc.freenode.net");
		this.SAI_Serveur.IsEditable = false;
		this.SAI_Serveur.InvisibleChar = '•';
		this.vbox3.Add (this.SAI_Serveur);
		global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.SAI_Serveur]));
		w1.Position = 0;
		w1.Expand = false;
		w1.Fill = false;
		// Container child vbox3.Gtk.Box+BoxChild
		this.SAI_Chan = new global::Gtk.Entry ();
		this.SAI_Chan.CanFocus = true;
		this.SAI_Chan.Name = "SAI_Chan";
		this.SAI_Chan.Text = global::Mono.Unix.Catalog.GetString ("#frugalware");
		this.SAI_Chan.IsEditable = false;
		this.SAI_Chan.InvisibleChar = '•';
		this.vbox3.Add (this.SAI_Chan);
		global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.SAI_Chan]));
		w2.Position = 1;
		w2.Expand = false;
		w2.Fill = false;
		// Container child vbox3.Gtk.Box+BoxChild
		this.hbox3 = new global::Gtk.HBox ();
		this.hbox3.Name = "hbox3";
		this.hbox3.Spacing = 6;
		// Container child hbox3.Gtk.Box+BoxChild
		this.SAI_Port = new global::Gtk.Entry ();
		this.SAI_Port.CanFocus = true;
		this.SAI_Port.Name = "SAI_Port";
		this.SAI_Port.Text = global::Mono.Unix.Catalog.GetString ("6667");
		this.SAI_Port.IsEditable = false;
		this.SAI_Port.MaxLength = 4;
		this.SAI_Port.InvisibleChar = '•';
		this.hbox3.Add (this.SAI_Port);
		global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.SAI_Port]));
		w3.Position = 0;
		w3.Expand = false;
		// Container child hbox3.Gtk.Box+BoxChild
		this.SAI_User = new global::Gtk.Entry ();
		this.SAI_User.CanFocus = true;
		this.SAI_User.Name = "SAI_User";
		this.SAI_User.Text = global::Mono.Unix.Catalog.GetString ("FrugalUser");
		this.SAI_User.IsEditable = false;
		this.SAI_User.InvisibleChar = '•';
		this.hbox3.Add (this.SAI_User);
		global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.SAI_User]));
		w4.Position = 1;
		// Container child hbox3.Gtk.Box+BoxChild
		this.BTN_Connect = new global::Gtk.Button ();
		this.BTN_Connect.CanFocus = true;
		this.BTN_Connect.Name = "BTN_Connect";
		this.BTN_Connect.UseUnderline = true;
		this.BTN_Connect.Label = global::Mono.Unix.Catalog.GetString ("Connect");
		this.hbox3.Add (this.BTN_Connect);
		global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.BTN_Connect]));
		w5.Position = 2;
		w5.Expand = false;
		w5.Fill = false;
		this.vbox3.Add (this.hbox3);
		global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.hbox3]));
		w6.Position = 2;
		w6.Expand = false;
		w6.Fill = false;
		this.vbox2.Add (this.vbox3);
		global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.vbox3]));
		w7.Position = 0;
		w7.Expand = false;
		w7.Fill = false;
		// Container child vbox2.Gtk.Box+BoxChild
		this.GtkScrolledWindow1 = new global::Gtk.ScrolledWindow ();
		this.GtkScrolledWindow1.Name = "GtkScrolledWindow1";
		this.GtkScrolledWindow1.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow1.Gtk.Container+ContainerChild
		this.TXT_Messages = new global::Gtk.TextView ();
		this.TXT_Messages.CanFocus = true;
		this.TXT_Messages.Name = "TXT_Messages";
		this.TXT_Messages.Editable = false;
		this.GtkScrolledWindow1.Add (this.TXT_Messages);
		this.vbox2.Add (this.GtkScrolledWindow1);
		global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.GtkScrolledWindow1]));
		w9.Position = 1;
		// Container child vbox2.Gtk.Box+BoxChild
		this.hbox2 = new global::Gtk.HBox ();
		this.hbox2.Name = "hbox2";
		this.hbox2.Spacing = 6;
		// Container child hbox2.Gtk.Box+BoxChild
		this.SAI_Envoi = new global::Gtk.Entry ();
		this.SAI_Envoi.CanFocus = true;
		this.SAI_Envoi.Name = "SAI_Envoi";
		this.SAI_Envoi.IsEditable = true;
		this.SAI_Envoi.InvisibleChar = '•';
		this.hbox2.Add (this.SAI_Envoi);
		global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.SAI_Envoi]));
		w10.Position = 0;
		// Container child hbox2.Gtk.Box+BoxChild
		this.BTN_Send = new global::Gtk.Button ();
		this.BTN_Send.CanFocus = true;
		this.BTN_Send.Name = "BTN_Send";
		this.BTN_Send.UseUnderline = true;
		this.BTN_Send.Label = global::Mono.Unix.Catalog.GetString ("_Send");
		this.hbox2.Add (this.BTN_Send);
		global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.BTN_Send]));
		w11.Position = 1;
		w11.Expand = false;
		w11.Fill = false;
		this.vbox2.Add (this.hbox2);
		global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hbox2]));
		w12.Position = 2;
		w12.Expand = false;
		w12.Fill = false;
		this.hbox1.Add (this.vbox2);
		global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.vbox2]));
		w13.Position = 0;
		// Container child hbox1.Gtk.Box+BoxChild
		this.vbox1 = new global::Gtk.VBox ();
		this.vbox1.Name = "vbox1";
		this.vbox1.Spacing = 6;
		// Container child vbox1.Gtk.Box+BoxChild
		this.image1 = new global::Gtk.Image ();
		this.image1.Name = "image1";
		this.image1.Pixbuf = global::Gdk.Pixbuf.LoadFromResource ("frugalirc.fw.png");
		this.vbox1.Add (this.image1);
		global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.image1]));
		w14.Position = 0;
		w14.Expand = false;
		w14.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
		this.GtkScrolledWindow.Name = "GtkScrolledWindow";
		this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
		this.TREE_Users = new global::Gtk.TreeView ();
		this.TREE_Users.CanFocus = true;
		this.TREE_Users.Name = "TREE_Users";
		this.GtkScrolledWindow.Add (this.TREE_Users);
		this.vbox1.Add (this.GtkScrolledWindow);
		global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.GtkScrolledWindow]));
		w16.Position = 1;
		this.hbox1.Add (this.vbox1);
		global::Gtk.Box.BoxChild w17 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.vbox1]));
		w17.Position = 1;
		w17.Expand = false;
		w17.Fill = false;
		this.Add (this.hbox1);
		if ((this.Child != null)) {
			this.Child.ShowAll ();
		}
		this.SAI_Serveur.Hide ();
		this.SAI_Port.Hide ();
		this.Show ();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
		this.BTN_Connect.Clicked += new global::System.EventHandler (this.OnBTNConnectClicked);
		this.BTN_Send.Clicked += new global::System.EventHandler (this.OnBTNSendClicked);
	}
}
