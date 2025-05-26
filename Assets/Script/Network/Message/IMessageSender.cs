namespace Script.Network.Message
{
    public interface IMessageSender
    {
        void SendMessage(Message message);
    }
}