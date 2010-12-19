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
using System.Xml;
using System.Xml.Schema;

namespace frugalmonotools
{
	
	
	public class XmlParser
	{
		private string file;
		
		public string File {
			get {
				return file;
			}
			set {
				file = value;
			}
		}
		
		public XmlParser(string file)
		{
			File=file;
		}
		/// <summary>
		/// Parcours simple du fichier XML (a revoire) 
		/// </summary>
		/// <param name="key">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="System.String"/>
		/// </returns>
		public string GetValue(string key,int id)
		{
			try{
			XmlDocument xDoc = new XmlDocument();
			xDoc.Load(File);
			XmlNodeList Valeur = xDoc.GetElementsByTagName(key);
			return Valeur[id].InnerText;
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message.ToString());
				return "";
			}
		}
		/// <summary>
		/// Compte le nombre d'élèment
		/// </summary>
		/// <param name="key">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="System.String"/>
		/// </returns>
		public int CountValue(string key)
		{
		
			try{
			XmlDocument xDoc = new XmlDocument();
			xDoc.Load(File);
			XmlNodeList Valeur = xDoc.GetElementsByTagName(key);
			return  Convert.ToInt32(Valeur.Count);
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message.ToString());
				return 0;
			}
		}

		
	}
}
