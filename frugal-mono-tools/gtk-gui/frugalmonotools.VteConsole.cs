
// This file has been generated by the GUI designer. Do not modify.
namespace frugalmonotools
{
	public partial class VteConsole
	{
		private global::Gtk.VBox vbox1;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget frugalmonotools.VteConsole
			this.Name = "frugalmonotools.VteConsole";
			this.Title = global::Mono.Unix.Catalog.GetString ("Console");
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Container child frugalmonotools.VteConsole.Gtk.Container+ContainerChild
			this.vbox1 = new global::Gtk.VBox ();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			this.Add (this.vbox1);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 400;
			this.DefaultHeight = 300;
			this.Show ();
		}
	}
}