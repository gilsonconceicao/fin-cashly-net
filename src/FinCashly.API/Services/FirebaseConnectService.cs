using FinCashly.Infrastructure.Settings;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using System.Text.Json;
#nullable disable

namespace FinCashly.API.Services
{
    public class FirebaseConnectService
    {
        public FirebaseConnectService(IConfiguration configuration)
        {
            if (FirebaseApp.DefaultInstance == null)
            {
                try
                {
                    var firebaseConfig = configuration.GetSection("FirebaseSettings").Get<FirebaseSettings>();

                    var jsonConfig = JsonSerializer.Serialize(new
                    {
                        type = firebaseConfig.Type,
                        project_id = firebaseConfig.ProjectId,
                        private_key_id = firebaseConfig.PrivateKeyId,
                        private_key = firebaseConfig.PrivateKey.Replace("\\n", "\n"),
                        client_email = firebaseConfig.ClientEmail,
                        client_id = firebaseConfig.ClientId,
                        auth_uri = firebaseConfig.AuthUri,
                        token_uri = firebaseConfig.TokenUri,
                        auth_provider_x509_cert_url = firebaseConfig.AuthProviderX509CertUrl,
                        client_x509_cert_url = firebaseConfig.ClientX509CertUrl,
                        universe_domain = firebaseConfig.UniverseDomain
                    });


                    FirebaseApp.Create(new AppOptions()
                    {
                        Credential = GoogleCredential.FromJson(jsonConfig)
                    });
                    Console.WriteLine("Firebase inicializado com sucesso.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao inicializar o Firebase: {ex}");
                }
            }
        }
    }
}