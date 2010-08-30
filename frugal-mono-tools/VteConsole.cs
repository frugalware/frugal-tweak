/*
 *  Copyright (C) 2010 by Gaetan Gourdin <bouleetbil@frogdev.info>
 *
 *  This program is free software; you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation; either version 2 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program; if not, write to the Free Software
 *  Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA 02111-1307, USA.
 */

using System;
using System.Collections;
using Vte;
namespace frugalmonotools
{
	public partial class VteConsole : Gtk.Window
	{
		Vte.Terminal term;
		public VteConsole () : base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
			term = new Vte.Terminal();
			term.CursorBlinks = true;
            term.MouseAutohide = true;
            term.ScrollOnKeystroke = true;
            term.DeleteBinding = TerminalEraseBinding.Auto;
            term.BackspaceBinding = TerminalEraseBinding.Auto;
            term.Encoding = "UTF-8";
            term.FontFromString = "Monospace 12";

			this.vbox1.Add (term);
			this.vbox1.ShowAll();
				
 
             string[] argv = Environment.GetCommandLineArgs ();
             // wants an array of "variable=value"
             string[] envv = new string [Environment.GetEnvironmentVariables ().Count];
             int i = 0;
             foreach (DictionaryEntry e in Environment.GetEnvironmentVariables ())
                {
                        if (e.Key == "" || e.Value == "")
                                continue;
                        string tmp = String.Format ("{0}={1}", e.Key, e.Value);
                        envv[i] = tmp;
                        i ++;
                }
 
			int pid = term.ForkCommand (
				Environment.GetEnvironmentVariable ("SHELL"),
				argv,
				envv,
				Environment.CurrentDirectory,
				false,
				true,
				true);
                Console.WriteLine ("Child pid: {0}", pid);
	
		}
	}
}

