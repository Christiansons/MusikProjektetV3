using MusikProjektetClient.Helpers;
using MusikProjektetClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusikProjektetClient.MenuService
{
	public class GenreMenu
	{
		private readonly IGenreService _genreService;
		private readonly MenuHelper _menuHelper;
		public GenreMenu(IGenreService genreService, MenuHelper menuHelper)
		{
			_genreService = genreService;
			_menuHelper = menuHelper;
		}
		public async Task ShowMenu()
		{
			string[] menu = { "Add new genre to database", "Add a genre to your playlist", "Show all your liked genres", "Back to main menu" };
			string whatMenu = "Genre menu";
			Console.WriteLine();

			bool showmenu = true;
			
			while (showmenu)
			{
				int choice = await  _menuHelper.FormatMenu(whatMenu, menu);
				switch (choice.ToString())
				{
					case "1":
						{
							await _genreService.AddGenre();
							await _menuHelper.BackToMenu();
							break;
						}
					case "2":
						{
							//_genreService.AddGenreToUser();
							break;
						}
					case "3":
						{
							//_genreService.ShowAllGenresConnectedToUser();
							break;
						}
					case "4":
						{
							showmenu = false;
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
}
