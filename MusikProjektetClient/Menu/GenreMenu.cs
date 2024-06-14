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
		public GenreMenu(IGenreService genreService)
		{
			_genreService = genreService;
		}

		public async Task ShowMenu()
		{
			MenuHelper helper = new MenuHelper();
			string[] menu = { "Add new genre to database", "Add a genre to your playlist", "Show all your liked genres", "Back to main menu" };
			string whatMenu = "Genre menu";
			Console.WriteLine();

			bool showmenu = true;
			int choice = await helper.FormatMenu(whatMenu, menu);
			while (showmenu)
			{
				switch (choice.ToString())
				{
					case "1":
						{
							//_genreService.AddGenre();
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
