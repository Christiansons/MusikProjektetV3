using System.Text.Json.Serialization;

namespace MusikProjektetV3.Models.ViewModels
{
	public class AllUsersViewModel
	{
		[JsonPropertyName("UserNames")]
		public List<string> UserNames { get; set; }
	}
}
