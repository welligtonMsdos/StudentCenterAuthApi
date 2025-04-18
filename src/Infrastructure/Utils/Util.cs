using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace StudentCenterAuthApi.src.Infrastructure.Utils;

public static class Util
{
    public static Task<string> GetKeyVault()
    {
		try
		{
            //var client = new SecretClient(new Uri("https://studentcenterkey.vault.azure.net/"), new DefaultAzureCredential());

            //KeyVaultSecret secret = await client.GetSecretAsync("studentCenterKey");

            //return secret.Value;

            return Task.FromResult("fedaf7d8863b48e197b9287d492b708e");
        }
		catch (Exception)
		{
            return Task.FromResult("fedaf7d8863b48e197b9287d492b708e");
        }        
    }    
}
