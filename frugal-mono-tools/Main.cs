using System;
using Gtk;

namespace frugalmonotools
{
	class MainClass
	{
		
		public static void Main (string[] args)
		{
			Application.Init ();
			//should be launch as root
			
			MainWindow win = new MainWindow ();
			win.Show ();
			Application.Run ();
		}
	}
}

