using EntityFrameworkCore.Triggered;
using Necnat.Abp.NnLibCommon.Entities;
using Necnat.Abp.NnLibCommon.Utils;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Timing;

namespace Necnat.Abp.NnLibCommon.Repositories
{
    public class TemporalAfterSaveTrigger<TEntity, TEntityTemporal, TKey, TTemporalRepository> : IAfterSaveTrigger<TEntity>
        where TTemporalRepository : ITemporalRepository<TEntityTemporal, TKey, TEntity>
        where TEntityTemporal : class, IEntityTemporal<TKey>
        where TEntity : class, IGetSetEntity<TKey>
    {
        protected readonly ITemporalRepository<TEntityTemporal, TKey, TEntity> _temporalRepository;
        protected readonly IClock _clock;

        public TemporalAfterSaveTrigger(
            ITemporalRepository<TEntityTemporal, TKey, TEntity> temporalRepository,
            IClock clock)
        {
            _temporalRepository = temporalRepository;
            _clock = clock;
        }

        public async Task AfterSave(ITriggerContext<TEntity> context, CancellationToken cancellationToken)
        {
            var now = _clock.Now;

            var lastTemporal = await _temporalRepository.FindAsync(x => x.Id!.Equals(context.Entity.Id) && x.PeriodEnd == null);
            if (lastTemporal != null)
            {
                lastTemporal.PeriodEnd = now;
                await _temporalRepository.UpdateAsync(lastTemporal, false, cancellationToken);
            }

            var temporal = JsonUtil.CloneTo<TEntity, TEntityTemporal>(context.Entity);
            temporal.PeriodStart = now;
            await _temporalRepository.InsertAsync(temporal, false, cancellationToken);
        }
    }
}
