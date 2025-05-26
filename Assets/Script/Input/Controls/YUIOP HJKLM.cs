using UnityEngine.InputSystem;
using Script.Utils;
using UnityEngine;
using Script.Network.Message;
using Script.Game.Player;
using Script.Game;

namespace Script.Input.Controls
{
    public class YUIOP_HJKLM : MonoBehaviour
    {
        void Update()
        {
            // Top row
            if (Keyboard.current.yKey.wasPressedThisFrame)
            {
                Log.Info("Y key pressed");
                CreateGame(GameNameEnum.Game1);
            }

            if (Keyboard.current.uKey.wasPressedThisFrame)
            {
                Log.Info("U key pressed");
                CreateGame(GameNameEnum.Game2);
            }
                
            if (Keyboard.current.iKey.wasPressedThisFrame)
            {
                Log.Info("I key pressed");
                CreateGame(GameNameEnum.Game3);
            }
            if (Keyboard.current.oKey.wasPressedThisFrame)
            {
                Log.Info("O key pressed");
                CreateGame(GameNameEnum.Game4);
            }
            if (Keyboard.current.pKey.wasPressedThisFrame)
            {
                Log.Info("P key pressed");
                CreateGame(GameNameEnum.Game5);
            }

            // Bottom row
            if (Keyboard.current.hKey.wasPressedThisFrame)
            {
                Log.Info("H key pressed");
                JoinGame(GameNameEnum.Game1);
            }
            
            if (Keyboard.current.jKey.wasPressedThisFrame)
            {
                Log.Info("J Key pressed");
                JoinGame(GameNameEnum.Game2);
            }  
            
            if (Keyboard.current.kKey.wasPressedThisFrame)
            {
                Log.Info("K key pressed");
                JoinGame(GameNameEnum.Game3);
            }
            
            if (Keyboard.current.lKey.wasPressedThisFrame)
            {
                Log.Info("L key pressed");
                JoinGame(GameNameEnum.Game4);
            } 
            
            if (Keyboard.current.mKey.wasPressedThisFrame)
            {
                Log.Info("M key pressed");
                JoinGame(GameNameEnum.Game5);
            }
        }
        
        private void CreateGame (GameNameEnum gameName)
        {
            Message message = new Message();
            message.SetAction(ActionEnum.CreateGame);
            message.SetGameNameEnum(gameName);
            message.Send();
        }

        private void JoinGame(GameNameEnum gameName)
        {
            Message message = new Message();
            message.SetAction(ActionEnum.Join);
            message.SetGameNameEnum(gameName);
            message.Send();
        }
    }
}