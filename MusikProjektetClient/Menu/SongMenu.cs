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
        internal async void ShowMenu()
		{
			
			MenuHelper helper = new MenuHelper();
			string[] menu = { "Add new song to database", "Add a song to your playlist", "Show all songs added to your playlist", "Back to main menu" };
			string whatMenu = "Song menu";
			Console.WriteLine();

			bool showmenu = true;
			int choice = helper.FormatMenu(whatMenu, menu);
			while (showmenu)
			{
				switch (choice.ToString())
				{
					case "1":
						{
							await _songService.AddSong();
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
