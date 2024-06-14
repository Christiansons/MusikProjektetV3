using MusikProjektetClient.Helpers;
using MusikProjektetClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusikProjektetClient.MenuService
{
	public class SongMenu
	{
		private readonly ISongService _songService;
        public SongMenu(ISongService songService)
        {
            _songService = songService;
        }
        public async Task ShowMenu()
		{
			
			MenuHelper helper = new MenuHelper();
			string[] menu = { "Add new song to database", "Add a song to your playlist", "Show all songs added to your playlist", "Back to main menu" };
			string whatMenu = "Song menu";
			Console.WriteLine();

			bool showmenu = true;
			
			while (showmenu)
			{
				int choice = await helper.FormatMenu(whatMenu, menu);
				switch (choice.ToString())
				{
					case "1":
						{
							await _songService.AddSong();
							await helper.BackToMenu();
							break;
						}
					case "2":
						{
							await _songService.AddSongToUser();
							await helper.BackToMenu();
							break;
						}
					case "3":
						{
							await _songService.ShowAllSongsAddedToUser();
							await helper.BackToMenu();
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
