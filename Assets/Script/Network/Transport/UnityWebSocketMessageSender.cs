using Script.Network.Message;

namespace Script.Network.Transport
{
    public class UnityWebSocketMessageSender : IMessageSender
    {
        public void SendMessage(Message.Message message)
        {
            UnityWebSocket.SendMessage(message);
        }
    }
}