using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipme.WikiBeer.Tools
{
    /// <summary>
    /// Classe responsable du lancement et de l'arrêt de l'api
    /// /// Note : pour démarer une appli à partir d'une autre appli : (classe process)
    /// https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.process?view=net-6.0
    /// https://stackoverflow.com/questions/70690219/net-6-0-configuration-files
    /// </summary>
    public class ApiLauncher
    {
        private Process Api { get; }
        private string ApiPath { get; }
        public string ApiUrl { get; }
        public string ConnectionString { get; }

        public ApiLauncher(string apiUrl = "https://localhost:5001",
            string apiPath = @"C:\Users\armel\git\Formation_IPME_dot_net\Projet\EncyclopeBeer\WikiBeer\API\bin\Debug\net6.0\Ipme.WikiBeer.Api.exe",
            string apiArgs = @"Data Source = (LocalDb)\MSSQLLocalDB; Initial Catalog = WikiTest; Integrated Security = True;")
        {            
            Api = new Process();
            ApiPath = apiPath;
            ApiUrl = apiUrl;
            ConnectionString = apiArgs;
        }

        public void StartApi()
        {
            ConfigureApi();
            Api.Start();
        }

        public void StopApi()
        {
            Api.Dispose();
            Api.Close();
            //Api.Kill();
        }

        private void ConfigureApi()
        {
            Api.StartInfo = new ProcessStartInfo();
            Api.StartInfo.UseShellExecute = false;
            Api.StartInfo.RedirectStandardOutput = false;
            Api.StartInfo.ArgumentList.Add(ConnectionString);
            Api.StartInfo.FileName = ApiPath;
        }

    }
}
