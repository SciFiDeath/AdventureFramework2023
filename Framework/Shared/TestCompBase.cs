using Microsoft.AspNetCore.Components;
using JsonUtilities;

namespace Framework.TestComp;

public class TestCompBase : ComponentBase
{
	[Inject]
	protected JsonUtility JsonUtility { get; set; } = null!;
	// protected HttpClient Http { get; set; } = null!;
	
	
	protected string thing = null!;
	

	protected override async Task OnInitializedAsync()
	{
		List<string> l = await JsonUtility.LoadFromJsonAsync<List<string>>("inventory.json");
		thing = l[0];
	}
}