using MusikProjektetClient.Helpers;
using MusikProjektetClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusikProjektetClient.MenuService
{
	public class RecommendationMenu
	{
		public IRecommendationService _recommendationService;
		public MenuHelper _menuHelper;
        public RecommendationMenu(IRecommendationService recommendationService, MenuHelper menuHelper)
        {
            _recommendationService = recommendationService;
			_menuHelper = menuHelper;
        }

        public async Task ShowMenu()
		{
			string[] menu = { "Get a song recommendation", "Get a artist recommendation", "back to main menu" };
			string whatMenu = "Recommandation menu";
			Console.WriteLine();

			bool showmenu = true;
			while (showmenu)
			{
				int choice = await _menuHelper.FormatMenu(whatMenu, menu);
				switch (choice.ToString())
				{
					case "1":
						{
							await _recommendationService.GetSimilarSongsAsync();
							break;
						}
					case "2":
						{
							await _recommendationService.GetSimilarArtistsAsync();
							break;
						}
					case "3":
						{
							showmenu = false;
							break;
						}
					default:
						{
							Console.WriteLine("Invalid input!");
							break;
						}
				}
			}
		}
	}
}
