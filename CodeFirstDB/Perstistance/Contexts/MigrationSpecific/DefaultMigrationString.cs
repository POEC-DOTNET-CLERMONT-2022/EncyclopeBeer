using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Clef de connection par défaut pour la migration. TODO à remplacer par un couple ConfigurationManager et appsetting.json -> a creuser pour des raisons de sécurité
/// </summary>
namespace Contexts
{
    internal static class DefaultMigrationString
    {
        internal const string DEFAULT_MIGRATION_STRING = @"Server=(localdb)\MSSQLLocalDB;Database=WikiBeer";
    }
}
