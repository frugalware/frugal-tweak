using System;
namespace frugalmonotools
{
	public partial class FEN_Debug : Gtk.Window
	{
		public FEN_Debug () : base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
		}
		public void AddTexte(string texte)
		{
			this.SortieAPPLI.Buffer.Text+=texte;
		}
	}
}

