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

namespace frugalmonotools
{
	
	
	public static class StringExtensions

{

    public static DateTime ParseDateTime(string date)

    {
			DateTime ret ;
			try {
				//Mon Apr 06 9:28:08 +0000 2009
				//Mon Apr 06 09:40:05 +0000 2009
				
		        string words = date;
		        String[] b = words.Split(' ');
				//string first = b[0].Trim();//Which returns the text before space
				//Console.WriteLine(first);
    
					
		        //string dayOfWeek = date.Substring(0, 3).Trim();
				string dayOfWeek = b[0].Trim();
				
					
		        //string month = date.Substring(4, 3).Trim();
				string month = b[1].Trim();
		
		        //string dayInMonth = date.Substring(8, 2).Trim();
				string dayInMonth = b[2].Trim();
					
		        //string time = date.Substring(11, 9).Trim();
				string time = b[3].Trim();
					
		        //string offset = date.Substring(20, 5).Trim();
				//string offset = date.Split(seps,4).ToString().Trim();
		
		        //string year = date.Substring(25, 5).Trim();
				string year = b[5].Trim();
					
		        string dateTime = string.Format("{0}-{1}-{2} {3}", dayInMonth, month, year, time);
		
		        ret = DateTime.Parse(dateTime);
		
		        return ret;
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message.ToString());
				return DateTime.Parse("");	
			}
			
    }

}


}
