<%@ WebService Language="C#" Class="WebService.FrugalNotes" %>

using System;
using System.Web.Services;
using System.IO;

namespace WebService
{
	
	
	internal class FrugalNotes
	{
		string cch_Dir = @"/mnt/sites/notes/";
		string cch_FileNote ="note.txt";
		[WebMethod]
		public bool SendNote (string user,string pass,string note)
		{
			try
			{
			System.IO.Directory.CreateDirectory(cch_Dir+user+pass);
			StreamWriter FileNotes = new StreamWriter( cch_Dir+user+pass+"/"+cch_FileNote);
			FileNotes.Write(note);
			FileNotes.Close();
			}
			catch
			{
				return false;
			}
			return true;
		}
 
		[WebMethod]
		public string GetNote (string user,string pass)
		{
			return ReadFile(cch_Dir+user+pass+"/"+cch_FileNote);
		}
		private string ReadFile(string fileToRead)
		{
			try
			{
				System.IO.StreamReader textFile = new System.IO.StreamReader(fileToRead);
                string fileContents = textFile.ReadToEnd();
                textFile.Close();
				return fileContents;
			}
			catch(Exception exe)
			{
				return "Could not get note";
			}
		}
	}
}



