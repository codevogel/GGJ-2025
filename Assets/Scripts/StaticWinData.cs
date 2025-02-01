public static class WinData
{

   public static int playerCount = 0;

   public static string WinningPlayer = "";

   public static int GetWinningPlayer()
   {
      return int.Parse(WinningPlayer[WinningPlayer.Length - 1].ToString());
   }
}
