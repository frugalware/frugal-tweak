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
using System.IO;
using System.Net;

namespace frugalmonotools
{

	
	static public class Download
	{
		//TODO Add the proxy
		
		/// <summary>
		/// Téléchargement d'image
		/// </summary>
		/// <param name="imageUrl">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="login">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="pass">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="Gdk.Pixbuf"/>
		/// </returns>
		public static Gdk.Pixbuf DonwloadImage(string imageUrl,string login,string pass)
		{
			Gdk.Pixbuf pix1 ;
			try {
				HttpWebRequest req = (HttpWebRequest) WebRequest.Create (imageUrl);
				//login, mot de passe
				if (login != null && pass != null)
				{
					req.Credentials = new NetworkCredential(login, pass);
				}
				req.KeepAlive = false;
				req.Timeout = 10000;	
				WebResponse resp = null;
				resp = req.GetResponse ();
				Stream s = resp.GetResponseStream ();
				pix1 = new Gdk.Pixbuf (s);
				resp.Close ();
				return pix1;
			}
			catch(Exception ex)
			{
				Console.WriteLine("Get user image exception: GetTwitterData.cs - GetUserImage()");
				Console.WriteLine(ex.StackTrace);
				return null;
			}
		}


	public static bool SendTwit(string url,string username,string password,string postdata)
		{
			try{

				// encode the username/password
				string user = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(username + ":" + password));
				// determine what we want to upload as a status
				byte[] bytes = System.Text.Encoding.ASCII.GetBytes("status=" + postdata);
				// connect with the update page
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
				// set the method to POST
				request.Method="POST";
				request.ServicePoint.Expect100Continue = false;
				// set the authorisation levels
				request.Headers.Add("Authorization", "Basic " + user);
				request.ContentType="application/x-www-form-urlencoded";
				// set the length of the content
				request.ContentLength = bytes.Length;
				
				// set up the stream
				Stream reqStream = request.GetRequestStream();
				// write to the stream
				reqStream.Write(bytes, 0, bytes.Length);
				// close the stream
				reqStream.Close();
				return true;
			}
			catch(Exception ex )
			{
				Console.WriteLine(ex.Message.ToString());
				return false;
			}
		}

		
		/// <summary>
		/// Téléchargement d'un fichier
		/// </summary>
		public static bool DownloadFile(string url,string user, 
		                                 string pass,string dir , string filename)
		{
			try
			{
				
				WebClient wc = new WebClient();

			  	wc.Proxy = null; //TODO : Implement it
			  	/*
				WebProxy p = new WebProxy ("192.178.10.49", 808);
				p.Credentials = new NetworkCredential ("username", "password");
				// or:
				p.Credentials = new NetworkCredential ("username", "password", "domain");
				
				using (WebClient wc = new WebClient())
				{
				  wc.Proxy = p;
				  ...
				}
				*/
			  	//wc.BaseAddress = url;
				// Authenticate, then upload and download a file to the FTP server.
				// The same approach also works for HTTP and HTTPS.
				
				string username =user;
				string password = pass;
				
				//création répertoire
				if (!Directory.Exists(dir)){
					Directory.CreateDirectory(dir);
				}
				
				wc.Credentials = new NetworkCredential (username, password);
				
				wc.DownloadFile (url, dir+filename);
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message.ToString());
				return false;
			}
			
		}
	}
}
