using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

public class SupabaseService
{
    private readonly HttpClient _client;
    private const string BaseUrl = "//postgres:[YOUR-PASSWORD]@db.etojzsvrqztynlcxytld.supabase.co:5432/postgres";
    private const string ApiKey = "YOUR_SUPABASE_ANON_KEY";

    public SupabaseService()
    {
        _client = new HttpClient();
        _client.BaseAddress = new Uri(BaseUrl);
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);
        _client.DefaultRequestHeaders.Add("apikey", ApiKey);
    }

    public async Task<string> GetDataAsync(string tableName)
    {
        var response = await _client.GetAsync($"{tableName}?select=*");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}
