using Script.Network.response;
using Script.Utils;

namespace Script.Game.handlers
{
    public class GameState : IResponseHandler
    {
        public void handle(Response response)
        {
            Log.Info("Handling Game State Response");
        }
    }
}