using Script.Network.response;
using Script.Utils;

namespace Script.Game.handlers
{
    public class Joined : IResponseHandler
    {
        public void handle(Response response)
        {
            Log.Info("Handling Joined Response");
        }
    }
}