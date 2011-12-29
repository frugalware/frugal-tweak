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
using System.Collections;
using System.Collections.Generic;
namespace frugalmonotools
{
	public class User
	{
		#region private
		private int _id =0;
		private string _name="";
		private string _comment="";
		private string _shell="";
		private string _home="";
		private string _pass="";
		#endregion
		
		#region public
		public string Pass {
			get {
				return this._pass;
			}
			set {
				_pass = value;
			}
		}
		
		public string Comment {
			get {
				return this._comment;
			}
			set {
				_comment = value;
			}
		}

		public string Home {
			get {
				return this._home;
			}
			set {
				_home = value;
			}
		}

		public int Id {
			get {
				return this._id;
			}
			set {
				_id = value;
			}
		}

		public string Name {
			get {
				return this._name;
			}
			set {
				_name = value;
			}
		}

		public string Shell {
			get {
				return this._shell;
			}
			set {
				_shell = value;
			}
		}
		#endregion
		
		public User ()
		{
			
		}
		public User(int id)
		{
			this.Id=id;
			//now should find informations
			string ch_ContentsFileUsers=Outils.ReadFile(Groups.cch_FileUser);
			string[] lines = ch_ContentsFileUsers.Split('\n');	
			foreach (string line in lines) 
		    {
				//gaetan:x:1000:100:gaetan,,,:/home/gaetan:/bin/bash
				if (line.Split(':')[2]==this.Id.ToString())
				{
					//find it :p
					this.Name=line.Split(':')[0];
					this.Shell=line.Split(':')[6];
					this.Home=line.Split(':')[5];
					this.Comment=line.Split(':')[4];
					break;
				}
			}
		}
		public User(string name)
		{
			this.Name=name;
			//now should find informations
			string ch_ContentsFileUsers=Outils.ReadFile(Groups.cch_FileUser);
			string[] lines = ch_ContentsFileUsers.Split('\n');	
			foreach (string line in lines) 
		    {
				//gaetan:x:1000:100:gaetan,,,:/home/gaetan:/bin/bash
				if (line.Split(':')[0]==this.Name)
				{
					//find it :p
					try
					{
						this.Id=Convert.ToInt32(line.Split(':')[2]);
						this.Shell=line.Split(':')[6];
						this.Home=line.Split(':')[5];
						this.Comment=line.Split(':')[4];
					}
					catch{}
					break;
				}
			}
		}
		
		
	}
}

