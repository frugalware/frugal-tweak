
// This file has been generated by the GUI designer. Do not modify.
namespace frugalmonotools
{
	public partial class splash
	{
		private global::Gtk.HBox hbox1;
		private global::Gtk.VBox vbox1;
		private global::Gtk.Image LOGO;
        
		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget frugalmonotools.splash
			this.Name = "frugalmonotools.splash";
			this.Title = global::Mono.Unix.Catalog.GetString ("Frugalware mono tools !");
			this.Icon = global::Gdk.Pixbuf.LoadFromResource ("frugalmonotools.Pictures.header.svg");
			this.TypeHint = ((global::Gdk.WindowTypeHint)(4));
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			this.BorderWidth = ((uint)(2));
			this.Resizable = false;
			this.AllowGrow = false;
			// Container child frugalmonotools.splash.Gtk.Container+ContainerChild
			this.hbox1 = new global::Gtk.HBox ();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.vbox1 = new global::Gtk.VBox ();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.LOGO = new global::Gtk.Image ();
			this.LOGO.Name = "LOGO";
			this.LOGO.Pixbuf = global::Gdk.Pixbuf.LoadFromResource ("frugalmonotools.Pictures.splash.png");
			this.vbox1.Add (this.LOGO);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.LOGO]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			this.hbox1.Add (this.vbox1);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.vbox1]));
			w2.Position = 1;
			w2.Expand = false;
			w2.Fill = false;
			this.Add (this.hbox1);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 400;
			this.DefaultHeight = 348;
			this.Show ();
		}
	}
}
