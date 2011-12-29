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
using System.IO;

namespace frugalmonotools
{
	
	
	public class Conf
	{
		
		private string dirtwitter=Environment.GetEnvironmentVariable("HOME")+"/.frugalware-tweak/";
		private string _NameTwitter;
		
		public string NameTwitter {
			get { return _NameTwitter; }
			set { _NameTwitter = value; }
		}
		private string _PassTwitter;
		
		public string PassTwitter {
			get { return _PassTwitter; }
			set { _PassTwitter = value; }
		}
		private string _NameIdenti;
		
		public string NameIdenti {
			get { return _NameIdenti; }
			set { _NameIdenti = value; }
		}
		private string _PassIdenti;
		
		public string PassIdenti {
			get { return _PassIdenti; }
			set { _PassIdenti = value; }
		}
		private string _Time;
		
		public string Time {
			get { return _Time; }
			set { _Time = value; }
		}		
		
		private bool _Notif;
		public bool Notif {
			get {
				return _Notif;
			}
			set {
				_Notif = value;
			}
		}
		
		/// <summary>
		///sauvegarde la configuration 
		/// </summary>
		/// <returns>
		/// A <see cref="System.Boolean"/>
		/// </returns>
		public Boolean SaveConfig()
		{
			try{	
				
				//création répertoire
				if (!Directory.Exists(dirtwitter)){
					Directory.CreateDirectory(dirtwitter);
				}
				XmlTextWriter myXmlTextWriter = new XmlTextWriter (dirtwitter+"config.xml", System.Text.Encoding.UTF8);
				myXmlTextWriter.Formatting = Formatting.Indented;
				myXmlTextWriter.WriteStartDocument(true);
				myXmlTextWriter.WriteStartElement("Config");
				myXmlTextWriter.WriteAttributeString("NameTwitter", NameTwitter);
				myXmlTextWriter.WriteAttributeString("PassTwitter", PassTwitter);
				myXmlTextWriter.WriteAttributeString("NameIdenti", NameIdenti);
				myXmlTextWriter.WriteAttributeString("PassIdenti", PassIdenti);
				myXmlTextWriter.WriteAttributeString("Time", Time);
				if (Notif)
				{
					myXmlTextWriter.WriteAttributeString("Notif", "1");
				}
				else{
					myXmlTextWriter.WriteAttributeString("Notif", "0");
				}
				
				myXmlTextWriter.WriteEndDocument();
				myXmlTextWriter.Flush();
				myXmlTextWriter.Close();
				return true;
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message.ToString());
				return false;
			}
		}
		
		
		public Boolean LoadConfig()
		{
			try{
				XmlTextReader reader = new XmlTextReader(dirtwitter+"config.xml");
				if (reader.Read())
			    {
      				reader.MoveToContent(); 
      				//pour le moment qu'un noeud on vera plus tard si on ajoute des éléments
			      	if (reader.Name.ToString()=="Config")
 				 	{
 						NameTwitter=reader.GetAttribute("NameTwitter");
 						PassTwitter=reader.GetAttribute("PassTwitter");
 						NameIdenti=reader.GetAttribute("NameIdenti");
 						PassIdenti=reader.GetAttribute("PassIdenti");
 						Time=reader.GetAttribute("Time");
						if(reader.GetAttribute("Notif") == "1")
						{
						 Notif=true;
						}
						else{
							Notif=false;
						}
						
						
 				 		reader.Close();
 				 		return true;
 				 	}
			    }
				reader.Close();
 				return false;//on a rien trouvé	
			}
			catch
			{
				return false;
			}
		}

		
	}
}
