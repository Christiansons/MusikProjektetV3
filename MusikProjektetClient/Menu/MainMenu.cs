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
		private readonly MenuHelper _menuHelper;
        public MainMenu(SongMenu songMenu, ArtistMenu artistMenu, GenreMenu genreMenu, RecommendationMenu recommendationMenu, MenuHelper menuHelper)
        {
			_songMenu = songMenu;
			_artistMenu = artistMenu;
			_genreMenu = genreMenu;
			_recommendationMenu = recommendationMenu;
			_menuHelper = menuHelper;
        }

        public void ShowMenu()
		{
            string[] menu = { "Genre menu", "Artist menu", "Song menu", "Get recommendations", "log out" };
			string whatMenu = "Main menu";
            Console.WriteLine();

			foreach (string item in menu)
			{
                Console.WriteLine(item);
            }
			string choice = Console.ReadLine();

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
						_recommendationMenu.ShowMenu();
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
