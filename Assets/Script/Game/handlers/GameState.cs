using Script.Game.Entity;
using Script.Network.response;
using Script.Utils;

namespace Script.Game.handlers
{
    public class GameState : IResponseHandler
    {
        public void handle(Response response)
        {
            MainThreadDispatcher.Enqueue((() =>
            {
                EntityManager.Instance.ProcessLivingEntitiesFromServer(response.LivingEntities);

            }));
        }
    }
}