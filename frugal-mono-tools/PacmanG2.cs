using System;
 
namespace frugalmonotools
{
	public class PacmanG2
	{
		private const string cch_pacmanconf ="/etc/pacman-g2.conf";
		public string[] fwRepo;
	
		public PacmanG2 ()
		{
			try{
				pacman.pacman_initialize("/");
			/*
				if(pacman.pacman_parse_config(cch_pacmanconf,_db_cd,"")!=-1)
				{
				}
			*/	  
				
			}
			catch{}
		}
				
		private static void _db_cd(string  section,SWIGTYPE_p_f_p_q_const__char_p_struct___pmdb_t__void db)
		{
			Console.WriteLine(section);		
		}
				
	}
}

