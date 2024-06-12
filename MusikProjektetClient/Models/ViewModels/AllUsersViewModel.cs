using System.Text.Json.Serialization;

namespace MusikProjektetClient.Models.ViewModels
{
	public class AllUsersViewModel
	{
		[JsonPropertyName("UserNames")]
		public List<string> UserNames { get; set; }
	}
}
