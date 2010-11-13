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
	public struct GroupUser
	{
		public Group TheGroup;
		public bool Into;
	}
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
		
		public static List<GroupUser> GetGroup(string username)
		{
			List<GroupUser> groupsUser = new List<GroupUser>();
				
			string ch_ContentsFileGroup=Outils.ReadFile(Groups.cch_FileGroup);
			string[] lines = ch_ContentsFileGroup.Split('\n');	
			foreach (string line in lines) 
		    {
				//storage::30:hald,gaetan
				if(line.Split(':')[0].ToString().Trim()!="")
				{
					Group Agroup = new Group(line.Split(':')[0]);
					GroupUser groupUser = new GroupUser();
					groupUser.TheGroup=Agroup;
					//extract users from this group
					bool bo_Into = false;
					string[] userNames = line.Split(':')[3].ToString().Split(',');
					foreach (string name in userNames) 
					{
						if(name==username)
						{
							bo_Into=true;
							break;
						}
					}
					groupUser.Into=bo_Into;
					groupsUser.Add(groupUser);
				}
			}
			return groupsUser;
		}
	}
}

