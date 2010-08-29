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
			MainWindow win = new MainWindow();
			win.Show();
			this.Hide();
			
			//now notifications
			if (MainClass.updatePkg)
			{
				IconSummaryBody notif= new IconSummaryBody();	
				if(Debug.ModeDebug)
				{
					foreach (packageCheck pkg in Update.UpdatePkg)
					{
						Console.WriteLine(pkg.packagename+" can be updated to "+pkg.packageversion);
					}
				}
				notif.ShowMessage("Frugalware","Some update are available.");
				Console.WriteLine("Some packages can be updated.");
			}
			
		}
		public void InitFinish()
		{
			Gtk.Application.Invoke (delegate{Start();});
			
		}
	}
}

