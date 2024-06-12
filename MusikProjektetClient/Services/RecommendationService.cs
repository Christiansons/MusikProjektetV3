using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusikProjektetClient.Services
{
    public interface IRecommendationService
    {

    }

	public class RecommendationService : IRecommendationService
	{
		private readonly HttpClient _httpClient;
		string baseAddress = "http://localhost:5098";

        public RecommendationService(HttpClient httpClient)
        {
            
        }
    }
}
