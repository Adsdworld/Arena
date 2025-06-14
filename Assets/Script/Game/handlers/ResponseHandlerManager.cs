using System.Collections.Generic;
using Script.Game.Player;
using Script.Network.response;
using Script.Ui;
using Script.Utils;

namespace Script.Game.handlers
{
    public class ResponseHandlerManager
    {
        private static readonly Dictionary<ResponseEnum, IResponseHandler> _handlers = new Dictionary<ResponseEnum, IResponseHandler>();

        static ResponseHandlerManager()
        {
            _handlers[ResponseEnum.Logged] = new Logged();
            _handlers[ResponseEnum.GameCreated] = new GameCreated();
            _handlers[ResponseEnum.GameAlreadyExists] = new GameAlreadyExists();
            _handlers[ResponseEnum.GamesLimitReached] = new GamesLimitReached();
            _handlers[ResponseEnum.GameClosed] = new GameClosed();
            _handlers[ResponseEnum.Joined] = new Joined();
            _handlers[ResponseEnum.GameState] = new GameState();
            _handlers[ResponseEnum.YourEntityIs] = new YourEntityIs();
            _handlers[ResponseEnum.GameNotFound] = new GameNotFound();
            _handlers[ResponseEnum.Info] = new Info();
        }

        public static void HandleResponse(Response response)
        {
            if (response == null)
            {
                Log.Warn("Response is null, cannot handle.");
            }

            if (response.GetResponse().GetResponseName() == "null")
            {
                Log.Warn("Response is unrecognized, cannot handle.");
            }
            
            if (_handlers.TryGetValue(response.GetResponse(), out var handler))
            {
                if (response.Notify != null)
                {
                    MainThreadDispatcher.Enqueue(() =>
                    {
                        ToastManager.Instance?.Notify(response.Notify);
                    });
                }
                handler.handle(response);
            }
            else
            {
                Log.Warn($"No handler found for response: {response.GetResponse().GetResponseName()}");
            }

        }
    }
}