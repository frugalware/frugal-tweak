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
	
	/// <summary>
	/// C'est le twitter de l'utilisateur
	/// </summary>
	public class TwitterObject
	{
		
		private string TwitterUrlProfil="http://www.twitter.com/account/verify_credentials.xml";
		private string TwitterPostUrl = "http://twitter.com/statuses/update.xml";
		private string TwitterFriendsUrl = "http://twitter.com/statuses/friends_timeline.xml";

		public int NbTwit;
		public List<Message> LstTwitter; //liste de twits
		
		private string dirtwitter=Environment.GetEnvironmentVariable("HOME")+"/.microblog/twitter/";
		public Personne User;

		
		public TwitterObject(string login, string password)
		{
			User= new Personne(login,password);
			Download.DownloadFile(TwitterUrlProfil,User.Nom,User.Password,dirtwitter,"profil.xml");
			//parser le fichier xml afin de récupérer mon profil
			XmlParser Xmlprofil= new XmlParser(dirtwitter+"profil.xml");
			User.Image=Xmlprofil.GetValue("profile_image_url",0);
			User.Pseudo=Xmlprofil.GetValue("screen_name",0);
			
		}
		//
		public Message GetUnTwit(int id)
		{
			Message UnTwit = new Message();
			try{
				UnTwit = LstTwitter[id];
				return UnTwit;
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message.ToString());
				return UnTwit;
			}
		
		}
		public Boolean SendText(string text)
		{
			return Download.SendTwit(TwitterPostUrl,this.User.Nom,this.User.Password,text);
		}
		
		private void ParseUserNode(XmlNode Element,out string nom, out string image)
		{
			
			nom = Element["screen_name"].InnerText;
			image= Element["profile_image_url"].InnerText;
			
		}
		
		/// <summary>
		/// Téléchargement des twittes
		/// </summary>
		/// <returns>
		/// A <see cref="System.Boolean"/>
		/// </returns>
		public Boolean GetTwitts()
		{
			try{
			Download.DownloadFile(TwitterFriendsUrl,User.Nom,User.Password,dirtwitter,"message.xml");
			LstTwitter = new List<Message>();
			//compter le nombre de twitter
			XmlParser XmlTwit= new XmlParser(dirtwitter+"message.xml");
			
			
			NbTwit = XmlTwit.CountValue("status");
			//Console.WriteLine("nb twit"+NbTwit);
			//creation d'un twit
			Personne UserTwit;
			Message UnTwit;
			XmlDocument MyXml = new XmlDocument();
			MyXml.Load(dirtwitter+"message.xml");
			foreach (XmlElement Child in MyXml.DocumentElement.GetElementsByTagName("status"))
			{
				//Collection.Add(ParseStatusNode(Child));
				//Mon May 12 15:56:07 +0000 2008
				/*Status.ID = int.Parse(Element["id"].InnerText);
				Status.Created = ParseDateString(Element["created_at"].InnerText);
				Status.Text = Element["text"].InnerText;
				Status.Source = Element["source"].InnerText;
				Status.IsTruncated = bool.Parse(Element["truncated"].InnerText);*/
				//Console.WriteLine(Child["created_at"].InnerText+" : "+Child["text"].InnerText);
				
				
				//UserTwit = new Personne("","",XmlTwit.GetValue("screen_name",cpt));
				//UserTwit.Image=XmlTwit.GetValue("profile_image_url",cpt);
				
				string name;
				string image;
				ParseUserNode(Child["user"],out name,out image);
				UserTwit = new Personne("","",name);
				UserTwit.Image=image;
				
				UnTwit=new Message(UserTwit,Child["text"].InnerText,"blue",Child["created_at"].InnerText);
				//UnMessage=new Message(UserMessage,XmlTwit.GetValue("text",cpt),"red",XmlTwit.GetValue("created_at",cpt));
				//TODO ajouter une gestion de cache pour les images
				UnTwit.User.Logo=Download.DonwloadImage(UnTwit.User.Image,User.Nom,User.Password);
				LstTwitter.Add(UnTwit);
				//cpt--;
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
	
	}


	
		
}
