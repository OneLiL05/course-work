using Supabase.Gotrue;

namespace trade_compas.Repositories;

public class UsersRepository(Supabase.Client supabaseClient)
{
    public async Task UpdateOne(UserAttributes data)
    {
        await supabaseClient.Auth.Update(data);
    }
}
