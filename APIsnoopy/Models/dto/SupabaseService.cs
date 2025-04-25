using Supabase;

public class SupabaseService
{
    private readonly Client _supabase;

    public SupabaseService(IConfiguration configuration)
    {
        string url = configuration["Supabase:BaseUrl"];
        string key = configuration["Supabase:ApiKey"];

        _supabase = new Client(url, key);
    }

    public Client GetClient() => _supabase;
}
