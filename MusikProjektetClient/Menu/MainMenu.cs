using MusikProjektetClient.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusikProjektetClient.MenuService
{
	public class MainMenu
	{
		private readonly SongMenu _songMenu;
		private readonly ArtistMenu _artistMenu;
		private readonly GenreMenu _genreMenu;
		private readonly RecommendationMenu _recommendationMenu;
        public MainMenu(SongMenu songMenu, ArtistMenu artistMenu, GenreMenu genreMenu, RecommendationMenu recommendationMenu)
        {
			_songMenu = songMenu;
			_artistMenu = artistMenu;
			_genreMenu = genreMenu;
			_recommendationMenu = recommendationMenu;
        }

        public void ShowMenu()
		{
			MenuHelper helper = new MenuHelper();
            string[] menu = { "Genre menu", "Artist menu", "Song menu", "Get recommendations", "log out" };
			string whatMenu = "Main menu";
            Console.WriteLine();
            
			int choice = helper.FormatMenu(whatMenu, menu);

            switch (choice.ToString())
            {
				case "1":
					{
						GenreMenu.ShowMenu();
						break;
					}
				case "2":
                    {
						ArtistMenu.ShowMenu();
						break;
                    }
				case "3":
					{
						_songMenu.ShowMenu();
						break;
					}
				case "4":
					{
						RecommendationMenu.ShowMenu();
						break;
					}
				case "5":
					{
						Environment.Exit(0);
						break;
					}
				default:
					{
						Console.WriteLine("Invalid input!");
						Console.Clear();
						break;
					}
			}
        }
		

	}
}
