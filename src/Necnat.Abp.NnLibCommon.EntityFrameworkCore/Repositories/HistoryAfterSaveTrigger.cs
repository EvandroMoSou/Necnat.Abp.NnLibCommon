using EntityFrameworkCore.Triggered;
using Necnat.Abp.NnLibCommon.Entities;
using Necnat.Abp.NnLibCommon.Utils;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Timing;

namespace Necnat.Abp.NnLibCommon.Repositories
{
    public class HistoryAfterSaveTrigger<TEntity, TEntityHistory, TKey, THistoryRepository> : IAfterSaveTrigger<TEntity>
        where THistoryRepository : IHistoryRepository<TEntityHistory, TKey, TEntity>
        where TEntityHistory : class, IEntityHistory<TKey>
        where TEntity : class, IGetSetEntity<TKey>
    {
        protected readonly IHistoryRepository<TEntityHistory, TKey, TEntity> _historyRepository;
        protected readonly IClock _clock;

        public HistoryAfterSaveTrigger(
            IHistoryRepository<TEntityHistory, TKey, TEntity> historyRepository,
            IClock clock)
        {
            _historyRepository = historyRepository;
            _clock = clock;
        }

        public async Task AfterSave(ITriggerContext<TEntity> context, CancellationToken cancellationToken)
        {
            var history = JsonUtil.CloneTo<TEntity, TEntityHistory>(context.Entity);
            history.BaseId = context.Entity.Id;
            history.ChangeType = (int)context.ChangeType;
            history.ChangeTime = _clock.Now;

            await _historyRepository.InsertAsync(history, false, cancellationToken);
        }
    }
}
