using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Net;
using Gtk;

namespace frugalmonotools
{
	public struct nodetype
	{
		public string rss_title;
		public string rss_description;
		public string rss_url;
		public string rss_pubdate;
	}
	
	/// <summary>
	///a little RSS reader 
	/// </summary>
	public class RSS
	{
		public ArrayList Nodes; //liste des flux
		
		public RSS (string url)
		{
			try{
			// Create a web request for the URL of the feed
        	WebRequest rssFeed = WebRequest.Create(url);

        	// Create a new XmlTextReader and read-in the feed
        	XmlTextReader Reader = new XmlTextReader(rssFeed.GetResponse().GetResponseStream());
			//XmlTextReader Reader = new XmlTextReader(url);
			
            Nodes = new ArrayList();  
 			// Initialize the current variables
        	string strLastText = "";
        	string strLastElement = "";
			Boolean ignore  = true;
            nodetype Node = new nodetype();
            while (Reader.Read())  
            {  	
				switch (Reader.NodeType)
                {
					case XmlNodeType.CDATA:
						strLastText = Reader.Value;
						Node.rss_description= strLastText;
						if (ignore)
								ignore=false;
							else
								Console.WriteLine("rss"+Node.rss_title);
								Nodes.Add(Node);
						break;
						
				 	case XmlNodeType.Element:
                        strLastElement = Reader.Name;
                        break;
						
                    case XmlNodeType.Text:
                        strLastText = Reader.Value;
						Debug.print("ELEMENT : @"+strLastElement+"@");
						Debug.print("VALEUR : "+strLastText);
						switch (strLastElement)
						{
							// Title
							case "title":
							Node.rss_title=strLastText;
							Console.WriteLine(strLastText);
							break;
						
							// Link
							case "link":
							Node.rss_url= strLastText;
							break;
						
							// Description
							// TO DO: once we hit a description, we should read until we find the end of that since this frequently has HTML in it
							case "description": // used by RSS 2.0
							Node.rss_description= strLastText;
							break;
						
							case "summary": // used by RDF
							//Node.rss_pubdate= strLastText;
							break;
						
							case "content:encoded": // used by Atom
							Node.rss_description=strLastText;
							break;
							
							case "dc:date":
							Node.rss_pubdate=strLastText;
							break;
							
							case "dc:creator":
							
							break;
						}
					
                        break;
						
					}
					
					
				
            }  	
		}
		catch
		{
			Debug.print("Crash RSS");
		}
		}
		
 
	}
}

