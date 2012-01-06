/*
 *
 * (C) 2010 bouleetbil <bouleetbil@frogdev.info>
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301  USA
 */

using GLib;
using fwtweak;

namespace Tools {
	public static string ReadLine(string commande)
	{
		string result="";
		Configuration conf = new Configuration();
		Posix.system(commande+" > "+conf.GetCacheDir()+"/out.txt");
		result=open_file(conf.GetCacheDir()+"/out.txt");
		return result;
	}
	public static void exec(string commande,string args)
	{
		try{
			Posix.execlp(commande,args);
		}
		catch{}
	}
	public static void ConsoleDebug(string text)
	{
		#if DEBUG
				stdout.printf(text+"\n");	
		#endif
	}
	public static string To_Utf8(string text)
	{
		ssize_t size = text.length;
		return text.locale_to_utf8(size,null,null);
	}
	public static string open_file (string filename) {
		try {
			string text;
			string text2;
			FileUtils.get_contents (filename, out text);
			text2=To_Utf8(text);
			ConsoleDebug(text2);
			return text2;
		} catch (Error e) {
			ConsoleDebug(e.message);
		}
		return "";
	}
	public static void write_file(string filelcoation,string content)
	{
		try {
			// Reference a local file name
			File file = File.new_for_path (filelcoation);
			if (file.query_exists ()) {
				// File or directory exists
				file.delete();	
			}

			// Create a new file with this name
			var file_stream = file.create (FileCreateFlags.NONE);

			// Write text data to file
			var data_stream = new DataOutputStream (file_stream);
			data_stream.put_string (content);
			Tools.ConsoleDebug("write "+ content);
			data_stream.flush();
			data_stream.close();
		} catch (Error e) {
			ConsoleDebug(e.message);
		}
	
	}
	public static void download()
	{
		ConsoleDebug("Donwload File");
	
	}
	public static int run_command(string cmd, string param, bool sync)
	{
	string scmd ="";
	if(cmd == "")
	    return 0;

	scmd = cmd+" "+param;
	try
	{
		if(sync == false) {
			if(Process.spawn_command_line_async(scmd))
			    ConsoleDebug("Execute command : "+ scmd);
		} else {
			if(Process.spawn_command_line_sync(scmd))
	    			ConsoleDebug("Execute command : "+scmd);
		}
	}

	catch(SpawnError s) {
	    ConsoleDebug(s.message);
	    return 1;
	}

	return 0;
	}
	
}
