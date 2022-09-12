using Dna;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RadioArchive.Core;

namespace RadioArchive.Relational
{
    /// <summary>
    /// Extension methods for the <see cref="FrameworkConstruction"/>
    /// </summary>
    public static class FrameWorkCunstructionExtensions
    {

        public static FrameworkConstruction AddClientDataStore(this FrameworkConstruction construction) 
        {
            // Inject our SQLite EF data store
            construction.Services.AddDbContext<ClientDataStoreDbContext>(options =>
            {
                // Setup connection string
                options.UseSqlite(construction.Configuration.GetConnectionString("ClientDataStoreConnection"));
            }, contextLifetime: ServiceLifetime.Transient);

            // Add client data store for easy access/use of the backing data store
            // Make it scoped so we can inject the scoped DbContext
            construction.Services.AddTransient<IClientDataStore>(
                provider => new ClientDataStore(provider.GetService<ClientDataStoreDbContext>()!));

            // Return framework for chaining
            return construction;
        }
    }
}
