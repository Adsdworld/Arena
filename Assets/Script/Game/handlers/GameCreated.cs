using Script.Network.response;
using Script.Ui;
using Script.Utils;

namespace Script.Game.handlers
{
    public class GameCreated : IResponseHandler
    {
        public void handle(Response response)
        {
            Log.Info("Handling GameCreated Response");
        }
    }
}