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
	public static class Groups
	{
		public static string cch_FileUser = @"/etc/passwd"; 
		public static string cch_FileGroup = @"/etc/group";
		
		public static List<User> GetAllUsers()
		{
			List<User> users = new List<User>();
			string ch_ContentsFileUsers=Outils.ReadFile(Groups.cch_FileUser);
			string[] lines = ch_ContentsFileUsers.Split('\n');	
			foreach (string line in lines) 
		    {
				if(line.Split(':')[0].ToString().Trim()!="")
				{
					User user = new User(line.Split(':')[0]);
					users.Add(user);
				}
			}
			return users;
		}
		
	}
}

