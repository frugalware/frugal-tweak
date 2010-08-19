using System;
namespace frugalmonotools
{
	public static class Debug
	{
		public static Boolean ModeDebug = false;
		public static Boolean ModeDebugGraphique = false;
		public static FEN_Debug winDebug;
				
		public static void print(string texte)
		{
			if (ModeDebug)
			{
				Console.WriteLine (texte);
				if(ModeDebugGraphique) {
					winDebug.AddTexte(texte+"\n");
				}
			}
		}
		
	}
}

