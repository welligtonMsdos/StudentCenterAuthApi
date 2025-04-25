using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace StudentCenterAuthApi.src.Infrastructure.Utils;

public static class Util
{  
    public static Task<string> GetKeyVault()
    {
		try
		{
            string keyVaultUrl = "https://jwtsecretsc.vault.azure.net/";

            var client = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());

            KeyVaultSecret secret = client.GetSecret("JwtSecret");

            return Task.FromResult(secret.Value);
        }
		catch (Exception)
		{
            return Task.FromResult("key not found");
        }        
    }
}
