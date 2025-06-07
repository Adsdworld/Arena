using Script.Game.Player;
using Script.Network.response;
using Script.Utils;

namespace Script.Game.handlers
{
    public class YourEntityIs : IResponseHandler
    {
        public void handle(Response response)
        {
            //Log.Info("YourEntityIs response received: " + response.Uuid);
            var entity = LocalPlayer.Instance;
            entity.GameName = response.GameName;
            entity.SetControlledEntityId("Entity_" + response.Uuid);
        }
    }
}