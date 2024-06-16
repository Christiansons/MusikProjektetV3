using MusikProjektetClient.Helpers;
using MusikProjektetClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusikProjektetClient.MenuService
{
	public class ArtistMenu
	{
		private readonly IArtistService _artistService;
		private readonly MenuHelper _menuHelper;
        public ArtistMenu(IArtistService artistService, MenuHelper menuHelper)
        {
            _artistService = artistService;
			_menuHelper = menuHelper;
        }

        public async Task ShowMenu()
		{
			string[] menu = { "Add new artist to database", "Add an artist to your playlist", "Show all your liked artists", "Back to main menu" };
			string whatMenu = "Artist menu";
			Console.WriteLine();

			bool showmenu = true;
			
			while (showmenu)
			{
				int choice = await _menuHelper.FormatMenu(whatMenu, menu);
				switch (choice.ToString())
				{
					case "1":
						{
							await _artistService.AddArtist();
							await _menuHelper.BackToMenu();
							break;
						}
					case "2":
						{
							await _artistService.AddArtistToUser();
							await _menuHelper.BackToMenu();
							break;
						}
					case "3":
						{
							await _artistService.ShowAllArtistsAddedToUser();
							await _menuHelper.BackToMenu();
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
