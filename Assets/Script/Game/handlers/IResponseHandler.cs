using Script.Network.response;

namespace Script.Game.handlers
{
    public interface IResponseHandler
    {
        void handle(Response response);
    }
}