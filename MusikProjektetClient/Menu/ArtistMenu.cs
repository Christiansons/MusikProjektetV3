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
        public ArtistMenu(IArtistService artistService)
        {
            _artistService = artistService;
        }

        internal static void ShowMenu()
		{
			MenuHelper helper = new MenuHelper();
			string[] menu = { "Add new artist to database", "Add an artist to your playlist", "Show all your liked artists", "Back to main menu" };
			string whatMenu = "Artist menu";
			Console.WriteLine();

			bool showmenu = true;
			int choice = helper.FormatMenu(whatMenu, menu);
			while (showmenu)
			{
				switch (choice.ToString())
				{
					case "1":
						{
							//_artistService.AddArtist();
							break;
						}
					case "2":
						{
							SongService.AddSongToUser();
							break;
						}
					case "3":
						{
							SongService.ShowAllSongsAddedToUser();
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
