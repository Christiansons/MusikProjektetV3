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
        public RecommendationMenu(IRecommendationService recommendationService)
        {
            _recommendationService = recommendationService;
        }

        public async Task ShowMenu()
		{
			MenuHelper helper = new MenuHelper();
			string[] menu = { "Get a song recommendation", "Get a genre recommendation", "Get a artist recommendation", "back to main menu" };
			string whatMenu = "Recommandation menu";
			Console.WriteLine();

			bool showmenu = true;
			while (showmenu)
			{
				int choice = await helper.FormatMenu(whatMenu, menu);
				switch (choice.ToString())
				{
					case "1":
						{
							await _recommendationService.GetSimilarSongsAsync();
							break;
						}
					case "2":
						{
							await _recommendationService.GetSimilarGenresAsync();
							break;
						}
					case "3":
						{
							await _recommendationService.GetSimilarArtistsAsync();
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
							break;
						}
				}
			}
		}
	}
}
