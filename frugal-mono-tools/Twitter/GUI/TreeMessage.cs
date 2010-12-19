// /*
//  * This file is part of the microblog <http://code.google.com/p/froggymicroblog/>
//  *
//  * Copyright (c) 2009, bouleetbil  <bouleetbil@frogdev.info>
//  * All rights reserved.
//  * 
//  * Redistribution and use in source and binary forms, with or without modification, are 
//  * permitted provided that the following conditions are met:
//  *
//  * - Redistributions of source code must retain the above copyright notice, this list 
//  *   of conditions and the following disclaimer.
//  * - Redistributions in binary form must reproduce the above copyright notice, this list 
//  *   of conditions and the following disclaimer in the documentation and/or other 
//  *   materials provided with the distribution.
//  * - Neither the name of the Twitterizer nor the names of its contributors may be 
//  *   used to endorse or promote products derived from this software without specific 
//  *   prior written permission.
//  *
//  * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
//  * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
//  * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
//  * IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
//  * INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
//  * NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
//  * PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
//  * WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
//  * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
//  * POSSIBILITY OF SUCH DAMAGE.
//  */


using System;
using System.Collections;

namespace frugalmonotools
{
	/// <summary>
	/// Pour la treeview des messages
	/// </summary>
	public class TreeMessage : IComparable 
	{
			private DateTime _date;
			
			private DateTime _dateMess;
			
			
			public string Date {
				get { return _date.ToString(); }
				set {
					try {
						_date = StringExtensions.ParseDateTime(value);
						DateMess=_date;
					}
					catch{}
				}
			}
			string _pseudo;
			
			public string Pseudo {
				get { return _pseudo; }
				set { _pseudo = value; }
			}
			string _message;
			
			public string Message {
				get { return _message; }
				set { _message = value; }
			}
			Gdk.Pixbuf _image;
			
			public Gdk.Pixbuf Image {
				get { return _image; }
				set { _image = value; }
			}
			string id;
			
			public string Id {
				get { return id; }
				set { id = value; }
			}

			public DateTime DateMess {
				get {
					return _dateMess;
				}
				set {
					_dateMess = value;
				}
			}
		string type;
		public string Type {
				get {
					return type;
				}
				set {
					type = value;
				}
			}


			
			
		public TreeMessage(Gdk.Pixbuf image,string date, string pseudo,string message,string id,string type)
		{
			this.Image=image;
			this.Date=date;
			this.Pseudo=pseudo;
			this.Message=message;
			this.Id=id;
			this.Type=type;
		}
		int IComparable.CompareTo(Object o)
		{
			TreeMessage op = (TreeMessage)o;
			int res = DateMess.CompareTo(op.DateMess);
			return -res;
		}
		
	}
}

