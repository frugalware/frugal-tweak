// /*
//  *  Copyright (C) 2010 by Gaetan Gourdin <bouleetbil@frogdev.info>
//  *
//  *  This program is free software; you can redistribute it and/or modify
//  *  it under the terms of the GNU General Public License as published by
//  *  the Free Software Foundation; either version 2 of the License, or
//  *  (at your option) any later version.
//  *
//  *  This program is distributed in the hope that it will be useful,
//  *  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  *  GNU General Public License for more details.
//  *
//  *  You should have received a copy of the GNU General Public License
//  *  along with this program; if not, write to the Free Software
//  *  Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA 02111-1307, USA.
//  */
using System;
using System.IO;
using System.Text;
namespace frugalmonotools
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class WID_Notes : Gtk.Bin
	{
		const string cch_UrlWebService="http://dors.frugalware.org/webservices/FrugalNotes.asmx";
		const string cch_FileNote= "/notes.txt";
		public WID_Notes ()
		{
			this.Build ();
		}
		public void InitNotes()
		{
			TXT_Note.Buffer.Text=Outils.ReadFile( DirTweak.GetDirFrugalTweak()+cch_FileNote,false);
			SAI_Login.Text=MainClass.configuration.Get_NoteLogin();
			SAI_Pass.Text=MainClass.configuration.Get_NotePass();
			LIB_Info.Visible=false;
		}
		protected virtual void OnBTNSaveClicked (object sender, System.EventArgs e)
		{
			StreamWriter FileNotes = new StreamWriter( DirTweak.GetDirFrugalTweak()+cch_FileNote);
			FileNotes.Write(TXT_Note.Buffer.Text);
			FileNotes.Close();
		}
		
		protected virtual void OnBTNSaveOptionsClicked (object sender, System.EventArgs e)
		{
			MainClass.configuration.Set_NoteLogin(SAI_Login.Text);
			MainClass.configuration.Set_NotePass(SAI_Pass.Text);
			MainClass.configuration.ConfSave();
		}
		
		protected virtual void OnBTNSendClicked (object sender, System.EventArgs e)
		{
			//send to server
			FrugalNotes note = new FrugalNotes();
			string TheNote = TXT_Note.Buffer.Text;
			string NoteCrypte = frugalmonotools.Crypto.EncryptStringAES(TheNote,MainClass.configuration.Get_NoteLogin()+MainClass.configuration.Get_NotePass());
			if(note.SendNote(MainClass.configuration.Get_NoteLogin(),MainClass.configuration.Get_NotePass(),NoteCrypte))
			{
				LIB_Info.Text="Note sending to server";
			}
			else
			{
				LIB_Info.Text="Error : could not send note to the server";
			}
			LIB_Info.Visible=true;
		}
		
		protected virtual void OnBTNDownloadClicked (object sender, System.EventArgs e)
		{
			//get from server
			FrugalNotes note = new FrugalNotes();
			string NoteCrypte = note.GetNote(MainClass.configuration.Get_NoteLogin(),MainClass.configuration.Get_NotePass());
			string NoteDecrypte =  frugalmonotools.Crypto.DecryptStringAES(NoteCrypte,MainClass.configuration.Get_NoteLogin()+MainClass.configuration.Get_NotePass());
			TXT_Note.Buffer.Text = NoteDecrypte;
		}
	}
}

