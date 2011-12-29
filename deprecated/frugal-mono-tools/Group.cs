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
	public class Group
	{
		#region private
		private int _id=0;
		private string _name="";
		private List<User> _users = new List<User>( );
		#endregion
		
		#region public
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
		#endregion
		
		public Group ()
		{
		}
		public Group(string name)
		{
			this.Name=name;
			string ch_ContentsFileGroups=Outils.ReadFile(Groups.cch_FileGroup);
			string[] lines = ch_ContentsFileGroups.Split('\n');	
			foreach (string line in lines) 
		    {
				//ccache:x:48:gaetan
				if (line.Split(':')[0]==this.Name)
				{
					try
					{
						this.Id=Convert.ToInt32(line.Split(':')[2]);
						//now find users
						string[] ch_Users = line.Split(':')[3].ToString().Split(',');
						foreach (string ch_User in ch_Users) 
		   				{
							User user = new User(ch_User);
							this._users.Add(user);
						}
					}
					catch{}
					break;
				}
			}
		}
	}
}

