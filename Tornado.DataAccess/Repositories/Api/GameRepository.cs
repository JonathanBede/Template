using System;
using System.Data.Entity;
using System.Linq.Expressions;
using Tornado.DataAccess.Contexts;
using Tornado.DataAccess.Interfaces.Api;
using Tornado.Domain.Entities.Api;

namespace Tornado.DataAccess.Repositories.Api
{
    public class GameRepository : BaseRepository<GameEntity, MainContext>, IGameRepository
    {
        protected override DbSet<GameEntity> DbSet(MainContext entityContext)
        {
            return entityContext.GameSet;
        }

        protected override Expression<Func<GameEntity, bool>> IdentifierPredicate(MainContext entityContext, Guid id)
        {
            return (e => e.Id == id);
        }

        //public void AddTopic(Guid gameId, Guid topicId)
        //{
        //    var game = EntityContext.GameSet.Find(gameId);
        //    var topic = EntityContext.TopicSet.Find(topicId);

        //    game?.Topics.Add(topic);

        //    EntityContext.SaveChanges();
        //}
    }
}
