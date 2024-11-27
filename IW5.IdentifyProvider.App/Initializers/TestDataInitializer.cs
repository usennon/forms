using IW5.IdentityProvider.DAL;
using IW5.IdentityProvider.DAL.Entities;
using IW5.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace IW5.DAL.Initialization
{
    public static class SampleDataInitializer
    {
        internal static void ClearData(IdentityProviderDbContext context)
        {
            try
            {
                // Delete from child tables first to avoid foreign key constraint violations
                var entities = new[]
                {
            typeof(AppRoleClaimEntity).FullName,
            typeof(AppRoleEntity).FullName,
            typeof(AppUserClaimEntity).FullName,
            typeof(AppUserEntity).FullName,
            typeof(AppUserLoginEntity).FullName,
            typeof(AppUserRoleEntity).FullName,
            typeof(AppUserTokenEntity).FullName,
        };

                foreach (var entityName in entities)
                {
                    var entity = context.Model.FindEntityType(entityName);
                    var tableName = entity.GetTableName();
                    var schemaName = entity.GetSchema();

                    // Use a parameterized query to avoid SQL injection issues
                    var sql = $"DELETE FROM {schemaName}.{tableName}";

                    context.Database.ExecuteSqlRaw(sql);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error clearing data: {ex.Message}");
                throw; // Re-throw the exception to handle it in the calling code
            }
        }


        internal static void SeedData(IdentityProviderDbContext context)
        {
            try
            {
                //ProcessInsert(context, context.Users!, SampleData.Users);
                //ProcessInsert(context, context.Forms!, SampleData.Forms);
                //ProcessInsert(context, context.Questions!, SampleData.Questions);
                //ProcessInsert(context, context.Options!, SampleData.Options);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            static void ProcessInsert<TEntity>(
                IdentityProviderDbContext context, DbSet<TEntity> table, List<TEntity> records) where TEntity : BaseEntity
            {
                if (table.Any())
                {
                    return;
                }

                IExecutionStrategy strategy = context.Database.CreateExecutionStrategy();
                strategy.Execute(() =>
                {
                    using var transaction = context.Database.BeginTransaction();
                    try
                    {
                        table.AddRange(records); // Add the records to the table
                        context.SaveChanges();    // Save changes to the database
                        transaction.Commit();     // Commit the transaction if successful
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();   // Rollback the transaction in case of an error
                        throw;                    // Rethrow the exception for error handling
                    }
                });
            }

        }


        internal static void DropAndCreateDatabase(IdentityProviderDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.Migrate();
        }

        public static void InitializeData(IdentityProviderDbContext context)
        {
            DropAndCreateDatabase(context);
            SeedData(context);
        }

        public static void ClearAndReseedDatabase(IdentityProviderDbContext context)
        {
            ClearData(context);
            SeedData(context);
        }
    }
}