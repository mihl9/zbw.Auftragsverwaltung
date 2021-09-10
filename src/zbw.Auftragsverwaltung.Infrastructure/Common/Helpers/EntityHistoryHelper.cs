using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using zbw.Auftragsverwaltung.Core.Common.Interfaces;

namespace zbw.Auftragsverwaltung.Infrastructure.Common.Helpers
{
    public class EntityHistoryHelper
    {
        public static async Task CreateHistory(DbContext context)
        {
            context.ChangeTracker.DetectChanges();
            var results = new List<object>();
            var changes = context.ChangeTracker.Entries();
            foreach (var entry in changes.Where(x => x.Entity is IEntityHistorized))
            {
                var entity = await CreateHistory(context, entry);
                if (entity is null)
                    continue;
                results.Add(entity);
            }
            //save changes
            if (results.Count > 0)
                await context.AddRangeAsync(results);

        }

        private static async Task<IEntityHistorized> CreateHistory(DbContext context, EntityEntry entry)
        {

            //only activate if changes are made
            if (!(entry.Entity is IEntityHistorized newEntry) || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged || entry.State == EntityState.Added)
                return null;
            
            //ensure that the entity is not a history element
            if (newEntry.ValidTo != null)
                return null;
            //check if the history element should be deleted
            if (entry.State == EntityState.Deleted)
            {
                //invalidate the entry
                newEntry.ValidTo = DateTime.UtcNow;
                return null;
            }
            //set the state to detached
            entry.State = EntityState.Detached;
            //prepare the old entry and set the needed values
            var oldEntry = (IEntityHistorized)await context.FindAsync(newEntry.GetType(), newEntry.Id, newEntry.ValidFrom);

            //set the new data
            newEntry.ValidFrom = DateTime.UtcNow;


            //set the end date
            oldEntry.ValidTo = newEntry.ValidFrom.AddSeconds(-1);
            //this.Entry(oldEntry).State = EntityState.Modified;

            //add the new entry to the list
            return newEntry;
        }

    }
}
