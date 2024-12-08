using IW5.DAL;
using IW5.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace IW5.DAL.Initialization
{
    public static class SampleDataInitializer
    {
        internal static void ClearData(FormsDbContext context)
        {
            try
            {
                // Delete from child tables first to avoid foreign key constraint violations
                var entities = new[]
                {
            typeof(Option).FullName,
            typeof(Question).FullName,
            typeof(Form).FullName,
            typeof(User).FullName,
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


        internal static void SeedData(FormsDbContext context)
        {
            try
            {
                ProcessInsert(context, context.Users!, SampleData.Users);
                ProcessInsert(context, context.Forms!, SampleData.Forms);
                ProcessInsert(context, context.Questions!, SampleData.Questions);
                ProcessInsert(context, context.Options!, SampleData.Options);
                
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            static void ProcessInsert<TEntity>(
                FormsDbContext context, DbSet<TEntity> table, List<TEntity> records) where TEntity : class, BaseEntity
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


        internal static void DropAndCreateDatabase(FormsDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.Migrate();
        }

        public static void InitializeData(FormsDbContext context)
        {
            DropAndCreateDatabase(context);
            SeedData(context);
        }

        public static void ClearAndReseedDatabase(FormsDbContext context)
        {
            ClearData(context);
            SeedData(context);
        }
    }
}