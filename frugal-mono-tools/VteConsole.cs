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
using System.Text;
using System.Collections;
using Vte;
using Gtk;
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
			VScrollbar vscroll = new VScrollbar (term.Adjustment);
			this.vbox1.PackStart (term);
			this.vbox1.PackStart(vscroll);
			this.vbox1.ShowAll();
		}
		public void Execute(string commande,string [] args)
		{
			int i =0;
			string[] argv;
			//encode to UTF8
			byte[] commutf8 = System.Text.Encoding.UTF8.GetBytes(commande);
			string commandev = System.Text.Encoding.UTF8.GetString(commutf8);
			if (args==null)
				argv=null;
			else
			{
				argv = new string[args.Length];
				foreach (string arg in args)
				{
					byte[] utf8 = System.Text.Encoding.UTF8.GetBytes(arg);
					argv[i]=System.Text.Encoding.UTF8.GetString(utf8);
					i++;
				}
			}
			 string[] envv = new string [Environment.GetEnvironmentVariables ().Count];
             i = 0;
             foreach (DictionaryEntry e in Environment.GetEnvironmentVariables ())
                {
                        if (e.Key.ToString()== "" || e.Value.ToString() == "")
                                continue;
                        string tmp = String.Format ("{0}={1}", e.Key, e.Value);
                        envv[i] = tmp;
                        i ++;
                }
				
 			try
			{
			term.ForkCommand (
				commandev,
				argv,
				envv,
				Environment.CurrentDirectory,
				true,
				true,
				true);
			}
			catch{}
                
		}
	}
}

