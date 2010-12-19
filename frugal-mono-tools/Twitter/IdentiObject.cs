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
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;

namespace frugalmonotools
{
	
	
	public class IdentiObject
	{
		private string IdentiUrlProfil="http://identi.ca/api/account/verify_credentials.xml";
		private string IdentiPostUrl = "http://identi.ca/api/statuses/update.xml";
		private string IdentiFriendsUrl = "http://identi.ca/api/statuses/friends_timeline.xml";

		public int NbTwit;
		public List<Message> LstIdenti; //liste de twits
		
		private string dirtwitter=Environment.GetEnvironmentVariable("HOME")+"/.microblog/identi/";
		public Personne User;

		public IdentiObject(string login, string password)
		{
			User= new Personne(login,password);
			Download.DownloadFile(IdentiUrlProfil,User.Nom,User.Password,dirtwitter,"profil.xml");
			//parser le fichier xml afin de récupérer mon profil
			XmlParser Xmlprofil= new XmlParser(dirtwitter+"profil.xml");
			User.Image=Xmlprofil.GetValue("profile_image_url",0);
			User.Pseudo=Xmlprofil.GetValue("screen_name",0);
		}
			/// <summary>
		/// Téléchargement des twittes
		/// </summary>
		/// <returns>
		/// A <see cref="System.Boolean"/>
		/// </returns>
		public Boolean GetMessages()
		{
			try{
			Download.DownloadFile(IdentiFriendsUrl,User.Nom,User.Password,dirtwitter,"message.xml");
			LstIdenti = new List<Message>();
			//compter le nombre de twitter
			XmlParser XmlTwit= new XmlParser(dirtwitter+"message.xml");
			NbTwit = XmlTwit.CountValue("status");
			//creation d'un twit
			Personne UserTwit;
			Message UnTwit;
			/*for (int cpt = NbTwit-1; cpt>-1;cpt--)
			{
				UserMessage = new Personne("","",XmlTwit.GetValue("screen_name",cpt));
				UserMessage.Image=XmlTwit.GetValue("profile_image_url",cpt);
				UnMessage=new Message(UserMessage,XmlTwit.GetValue("text",cpt),"red",XmlTwit.GetValue("created_at",cpt));
				//TODO ajouter une gestion de cache pour les images
				UnMessage.User.Logo=Download.DonwloadImage(UnMessage.User.Image,User.Nom,User.Password);
				LstIdenti.Add(UnMessage);
			}*/
			XmlDocument MyXml = new XmlDocument();
			MyXml.Load(dirtwitter+"message.xml");
			foreach (XmlElement Child in MyXml.DocumentElement.GetElementsByTagName("status"))
			{
				
				
				string name;
				string image;
				ParseUserNode(Child["user"],out name,out image);
				UserTwit = new Personne("","",name);
				UserTwit.Image=image;
				
				UnTwit=new Message(UserTwit,Child["text"].InnerText,"blue",Child["created_at"].InnerText);
				//UnMessage=new Message(UserMessage,XmlTwit.GetValue("text",cpt),"red",XmlTwit.GetValue("created_at",cpt));
				//TODO ajouter une gestion de cache pour les images
				UnTwit.User.Logo=Download.DonwloadImage(UnTwit.User.Image,User.Nom,User.Password);
				//Console.WriteLine("Le "+UnTwit.Date+" :" +UnTwit.Texte);
				LstIdenti.Add(UnTwit);
				
			}

			//ajout d'un twit dans la liste
			return true;
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message.ToString());
				return false;
			}
		}
		/// <summary>
		/// Recupere un message en particulier
		/// </summary>
		/// <param name="id">
		/// A <see cref="System.Int32"/>
		/// </param>
		/// <returns>
		/// A <see cref="Message"/>
		/// </returns>
		public Message GetUnMessage(int id)
		{
			Message UnTwit = new Message();
			try{
				UnTwit = LstIdenti[id];
				return UnTwit;
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message.ToString());
				return UnTwit;
			}
		
		}
		private void ParseUserNode(XmlNode Element,out string nom, out string image)
		{
			
			nom = Element["screen_name"].InnerText;
			image= Element["profile_image_url"].InnerText;
			
		}
		/// <summary>
		/// Send message to identi
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public Boolean SendText(string text)
		{
			return Download.SendTwit(IdentiPostUrl,this.User.Nom,this.User.Password,text);
		}
	}
}
