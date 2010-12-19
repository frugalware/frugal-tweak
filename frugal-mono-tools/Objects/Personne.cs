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

﻿

using System;

namespace frugalmonotools
{
	
	
	public class Personne
	{
		private string pseudo ;
		private string nom ;
		private string prenom;
		private string password;
		private string image;
		private Gdk.Pixbuf _Logo;
		
		public Gdk.Pixbuf Logo {
			get { return _Logo; }
			set { _Logo = value; }
		}
		
		public string Pseudo {
			get {
				return pseudo;
			}
			set {
				pseudo = value;
			}
		}

		public string Prenom {
			get {
				return prenom;
			}
			set {
				prenom = value;
			}
		}

		public string Nom {
			get {
				return nom;
			}
			set {
				nom = value;
			}
		}

		public string Password {
			get {
				return password;
			}
			set {
				password = value;
			}
		}

		public string Image {
			get {
				return image;
			}
			set {
				image = value;
			}
		}
		
		
		/// <summary>
		/// Une personne basique
		/// </summary>
		/// <param name="st_nom">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="st_prenom">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="st_pseudo">
		/// A <see cref="System.String"/>
		/// </param>
		public Personne(string st_nom, string st_prenom,string st_pseudo)
		{
			this.Nom=st_nom;
			this.Prenom=st_prenom;
			this.Pseudo=st_pseudo;
		}

		/// <summary>
		/// Création d'une personne identifé
		/// </summary>
		/// <param name="st_login">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="st_pass">
		/// A <see cref="System.String"/>
		/// </param>
		public Personne(string st_login,string st_pass)
		{
			this.Nom=st_login;
			this.Password=st_pass;
		}
	}
}
