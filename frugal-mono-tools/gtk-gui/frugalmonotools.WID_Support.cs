
// This file has been generated by the GUI designer. Do not modify.
namespace frugalmonotools
{
	public partial class WID_Support
	{
		private global::Gtk.VBox vbox9;

		private global::Gtk.Label label7;

		private global::Gtk.HBox hbox23;

		private global::Gtk.Button BTN_Irc;

		private global::Gtk.Button BTN_Irc1;

		private global::Gtk.Button BTN_Irc2;

		private global::Gtk.Button BTN_Forums;

		private global::Gtk.Button BTN_Wiki;

		private global::Gtk.Button BTN_Bugs;

		private global::Gtk.Label label8;

		private global::Gtk.Button BTN_French;

		private global::Gtk.Button BTN_Danish;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget frugalmonotools.WID_Support
			global::Stetic.BinContainer.Attach (this);
			this.Name = "frugalmonotools.WID_Support";
			// Container child frugalmonotools.WID_Support.Gtk.Container+ContainerChild
			this.vbox9 = new global::Gtk.VBox ();
			this.vbox9.Name = "vbox9";
			this.vbox9.Spacing = 6;
			// Container child vbox9.Gtk.Box+BoxChild
			this.label7 = new global::Gtk.Label ();
			this.label7.Name = "label7";
			this.label7.LabelProp = global::Mono.Unix.Catalog.GetString ("Frugalware provide some helps supports :\n\nForums : http://forums.frugalware.org\n\nWiki : http://wiki.frugalware.org\n\nirc : irc.freenode.net channel #frugalware");
			this.vbox9.Add (this.label7);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.vbox9[this.label7]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child vbox9.Gtk.Box+BoxChild
			this.hbox23 = new global::Gtk.HBox ();
			this.hbox23.Name = "hbox23";
			this.hbox23.Spacing = 6;
			// Container child hbox23.Gtk.Box+BoxChild
			this.BTN_Irc = new global::Gtk.Button ();
			this.BTN_Irc.CanFocus = true;
			this.BTN_Irc.Name = "BTN_Irc";
			this.BTN_Irc.UseUnderline = true;
			this.BTN_Irc.Label = global::Mono.Unix.Catalog.GetString ("Join #frugalware");
			this.hbox23.Add (this.BTN_Irc);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox23[this.BTN_Irc]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child hbox23.Gtk.Box+BoxChild
			this.BTN_Irc1 = new global::Gtk.Button ();
			this.BTN_Irc1.CanFocus = true;
			this.BTN_Irc1.Name = "BTN_Irc1";
			this.BTN_Irc1.UseUnderline = true;
			this.BTN_Irc1.Label = global::Mono.Unix.Catalog.GetString ("Join #frugalware.fr");
			this.hbox23.Add (this.BTN_Irc1);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox23[this.BTN_Irc1]));
			w3.Position = 1;
			w3.Expand = false;
			w3.Fill = false;
			// Container child hbox23.Gtk.Box+BoxChild
			this.BTN_Irc2 = new global::Gtk.Button ();
			this.BTN_Irc2.CanFocus = true;
			this.BTN_Irc2.Name = "BTN_Irc2";
			this.BTN_Irc2.UseUnderline = true;
			this.BTN_Irc2.Label = global::Mono.Unix.Catalog.GetString ("Join #frugalware.hu");
			this.hbox23.Add (this.BTN_Irc2);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox23[this.BTN_Irc2]));
			w4.Position = 2;
			w4.Expand = false;
			w4.Fill = false;
			this.vbox9.Add (this.hbox23);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox9[this.hbox23]));
			w5.Position = 1;
			w5.Expand = false;
			w5.Fill = false;
			// Container child vbox9.Gtk.Box+BoxChild
			this.BTN_Forums = new global::Gtk.Button ();
			this.BTN_Forums.CanFocus = true;
			this.BTN_Forums.Name = "BTN_Forums";
			this.BTN_Forums.UseUnderline = true;
			this.BTN_Forums.Label = global::Mono.Unix.Catalog.GetString ("Forums Frugalware");
			this.vbox9.Add (this.BTN_Forums);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbox9[this.BTN_Forums]));
			w6.Position = 2;
			w6.Expand = false;
			w6.Fill = false;
			// Container child vbox9.Gtk.Box+BoxChild
			this.BTN_Wiki = new global::Gtk.Button ();
			this.BTN_Wiki.CanFocus = true;
			this.BTN_Wiki.Name = "BTN_Wiki";
			this.BTN_Wiki.UseUnderline = true;
			this.BTN_Wiki.Label = global::Mono.Unix.Catalog.GetString ("Wiki Frugalware");
			this.vbox9.Add (this.BTN_Wiki);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.vbox9[this.BTN_Wiki]));
			w7.Position = 3;
			w7.Expand = false;
			w7.Fill = false;
			// Container child vbox9.Gtk.Box+BoxChild
			this.BTN_Bugs = new global::Gtk.Button ();
			this.BTN_Bugs.CanFocus = true;
			this.BTN_Bugs.Name = "BTN_Bugs";
			this.BTN_Bugs.UseUnderline = true;
			this.BTN_Bugs.Label = global::Mono.Unix.Catalog.GetString ("Bugs tracker");
			this.vbox9.Add (this.BTN_Bugs);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.vbox9[this.BTN_Bugs]));
			w8.Position = 4;
			w8.Expand = false;
			w8.Fill = false;
			// Container child vbox9.Gtk.Box+BoxChild
			this.label8 = new global::Gtk.Label ();
			this.label8.Name = "label8";
			this.label8.LabelProp = global::Mono.Unix.Catalog.GetString ("Internationalized Frugalware sites");
			this.vbox9.Add (this.label8);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.vbox9[this.label8]));
			w9.Position = 5;
			w9.Expand = false;
			w9.Fill = false;
			// Container child vbox9.Gtk.Box+BoxChild
			this.BTN_French = new global::Gtk.Button ();
			this.BTN_French.CanFocus = true;
			this.BTN_French.Name = "BTN_French";
			this.BTN_French.UseUnderline = true;
			this.BTN_French.Label = global::Mono.Unix.Catalog.GetString ("French");
			this.vbox9.Add (this.BTN_French);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.vbox9[this.BTN_French]));
			w10.Position = 6;
			w10.Expand = false;
			w10.Fill = false;
			// Container child vbox9.Gtk.Box+BoxChild
			this.BTN_Danish = new global::Gtk.Button ();
			this.BTN_Danish.CanFocus = true;
			this.BTN_Danish.Name = "BTN_Danish";
			this.BTN_Danish.UseUnderline = true;
			this.BTN_Danish.Label = global::Mono.Unix.Catalog.GetString ("Danish");
			this.vbox9.Add (this.BTN_Danish);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.vbox9[this.BTN_Danish]));
			w11.Position = 7;
			w11.Expand = false;
			w11.Fill = false;
			this.Add (this.vbox9);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Hide ();
			this.BTN_Irc.Clicked += new global::System.EventHandler (this.OnBTNIrcClicked);
			this.BTN_Irc1.Clicked += new global::System.EventHandler (this.OnBTNIrc1Clicked);
			this.BTN_Irc2.Clicked += new global::System.EventHandler (this.OnBTNIrc2Clicked);
			this.BTN_Forums.Clicked += new global::System.EventHandler (this.OnBTNForumsClicked);
			this.BTN_Wiki.Clicked += new global::System.EventHandler (this.OnBTNWikiClicked);
			this.BTN_Bugs.Clicked += new global::System.EventHandler (this.OnBTNBugsClicked);
			this.BTN_French.Clicked += new global::System.EventHandler (this.OnBTNFrenchClicked);
			this.BTN_Danish.Clicked += new global::System.EventHandler (this.OnBTNDanishClicked);
		}
	}
}