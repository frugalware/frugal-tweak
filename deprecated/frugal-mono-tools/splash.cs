using System;
using System.Threading;
namespace frugalmonotools
{
	public partial class splash : Gtk.Window
	{
	
		public splash () : base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
		
		}
		
		private void Start() {
			this.Hide();
			Console.WriteLine("Start control center");
			//now notifications
			if (MainClass.updatePkg)
			{	
				if(Debug.ModeDebug)
				{
					foreach (packageCheck pkg in Update.UpdatePkg)
					{
						Console.WriteLine(pkg.packagename+" can be updated to "+pkg.packageversion);
					}
				}
				Outils.Inform("Frugalware","Some update are available.");
				Console.WriteLine("Some packages can be updated.");
			}
			Fen_Menu win = new Fen_Menu();
			win.Show();
		}
		public void InitFinish()
		{
			Gtk.Application.Invoke (delegate{Start();});
			
		}
	}
}

