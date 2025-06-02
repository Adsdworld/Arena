using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UIElements;
using Script.Game;
using Script.Game.Player;
using Script.Network.Message;
using Script.Utils;

namespace Script.Ui
{
    public class serverSelectorController : MonoBehaviour
    {
        private UIDocument uiDocument;
        private VisualElement _root;
        private VisualElement _footer;

        private VisualElement _server1;
        private Button _openBtn1;
        private Button _joinBtn1;
        private Button _closeBtn1;
        
        private VisualElement _server2;
        private Button _openBtn2;
        private Button _joinBtn2;
        private Button _closeBtn2;
        
        private VisualElement _server3;
        private Button _openBtn3;
        private Button _joinBtn3;
        private Button _closeBtn3;
        
        private VisualElement _server4;
        private Button _openBtn4;
        private Button _joinBtn4;
        private Button _closeBtn4;
        
        private VisualElement _server5;
        private Button _openBtn5;
        private Button _joinBtn5;
        private Button _closeBtn5;
        

        private void OnEnable()
        {
            uiDocument = GetComponent<UIDocument>();
            _root = uiDocument.rootVisualElement;
            _footer = _root.Q<VisualElement>("Footer");

            if (_footer == null)
            {
                Debug.LogError("Footer introuvable dans l'UI");
                return;
            }
         

            // Game1
            _server1 = _footer.Q<VisualElement>("Server1");
            if (_server1 != null)
            {
                _openBtn1 = _server1.Q<Button>("Open");
                _joinBtn1 = _server1.Q<Button>("Join");
                _closeBtn1 = _server1.Q<Button>("Close");

                if (_openBtn1 != null) _openBtn1.RegisterCallback<ClickEvent>(evt => OnOpenClicked(GameNameEnum.Game1));
                if (_joinBtn1 != null) _joinBtn1.RegisterCallback<ClickEvent>(evt => OnJoinClicked(GameNameEnum.Game1));
                if (_closeBtn1 != null) _closeBtn1.RegisterCallback<ClickEvent>(evt => OnCloseClicked(GameNameEnum.Game1));
            }

            // Game2
            _server2 = _footer.Q<VisualElement>("Server2");
            if (_server2 != null)
            {
                _openBtn2 = _server2.Q<Button>("Open");
                _joinBtn2 = _server2.Q<Button>("Join");
                _closeBtn2 = _server2.Q<Button>("Close");
                

                if (_openBtn2 != null) _openBtn2.RegisterCallback<ClickEvent>(e => OnOpenClicked(GameNameEnum.Game2));
                if (_joinBtn2 != null) _joinBtn2.RegisterCallback<ClickEvent>(e => OnJoinClicked(GameNameEnum.Game2));
                if (_closeBtn2 != null) _closeBtn2.RegisterCallback<ClickEvent>(e => OnCloseClicked(GameNameEnum.Game2));
            }

            // Game3
            _server3 = _footer.Q<VisualElement>("Server3");
            if (_server3 != null)
            {
                _openBtn3 = _server3.Q<Button>("Open");
                _joinBtn3 = _server3.Q<Button>("Join");
                _closeBtn3 = _server3.Q<Button>("Close");

                if (_openBtn3 != null) _openBtn3.RegisterCallback<ClickEvent>(evt => OnOpenClicked(GameNameEnum.Game3));
                if (_joinBtn3 != null) _joinBtn3.RegisterCallback<ClickEvent>(evt => OnJoinClicked(GameNameEnum.Game3));
                if (_closeBtn3 != null) _closeBtn3.RegisterCallback<ClickEvent>(evt => OnCloseClicked(GameNameEnum.Game3));
            }

            // Game4
            _server4 = _footer.Q<VisualElement>("Server4");
            if (_server4 != null)
            {
                _openBtn4 = _server4.Q<Button>("Open");
                _joinBtn4 = _server4.Q<Button>("Join");
                _closeBtn4 = _server4.Q<Button>("Close");

                if (_openBtn4 != null) _openBtn4.RegisterCallback<ClickEvent>(evt => OnOpenClicked(GameNameEnum.Game4));
                if (_joinBtn4 != null) _joinBtn4.RegisterCallback<ClickEvent>(evt => OnJoinClicked(GameNameEnum.Game4));
                if (_closeBtn4 != null) _closeBtn4.RegisterCallback<ClickEvent>(evt => OnCloseClicked(GameNameEnum.Game4));
            }

            // Game5
            _server5 = _footer.Q<VisualElement>("Server5");
            if (_server5 != null)
            {
                _openBtn5 = _server5.Q<Button>("Open");
                _joinBtn5 = _server5.Q<Button>("Join");
                _closeBtn5 = _server5.Q<Button>("Close");

                if (_openBtn5 != null) _openBtn5.RegisterCallback<ClickEvent>(evt => OnOpenClicked(GameNameEnum.Game5));
                if (_joinBtn5 != null) _joinBtn5.RegisterCallback<ClickEvent>(evt => OnJoinClicked(GameNameEnum.Game5));
                if (_closeBtn5 != null) _closeBtn5.RegisterCallback<ClickEvent>(evt => OnCloseClicked(GameNameEnum.Game5));
            }
        }

        private void OnDisable()
        {
            _openBtn1.UnregisterCallback<ClickEvent>(evt => OnOpenClicked(GameNameEnum.Game1));
            _joinBtn1.UnregisterCallback<ClickEvent>(e => OnJoinClicked(GameNameEnum.Game1));
            _closeBtn1.UnregisterCallback<ClickEvent>(e => OnCloseClicked(GameNameEnum.Game1));
            
            _openBtn2.UnregisterCallback<ClickEvent>(evt => OnOpenClicked(GameNameEnum.Game2));
            _joinBtn2.UnregisterCallback<ClickEvent>(e => OnJoinClicked(GameNameEnum.Game2));
            _closeBtn2.UnregisterCallback<ClickEvent>(e => OnCloseClicked(GameNameEnum.Game2));
            
            _openBtn3.UnregisterCallback<ClickEvent>(evt => OnOpenClicked(GameNameEnum.Game3));
            _joinBtn3.UnregisterCallback<ClickEvent>(e => OnJoinClicked(GameNameEnum.Game3));
            _closeBtn3.UnregisterCallback<ClickEvent>(e => OnCloseClicked(GameNameEnum.Game3));
            
            _openBtn4.UnregisterCallback<ClickEvent>(evt => OnOpenClicked(GameNameEnum.Game4));
            _joinBtn4.UnregisterCallback<ClickEvent>(e => OnJoinClicked(GameNameEnum.Game4));
            _closeBtn4.UnregisterCallback<ClickEvent>(e => OnCloseClicked(GameNameEnum.Game4));
            
            _openBtn5.UnregisterCallback<ClickEvent>(evt => OnOpenClicked(GameNameEnum.Game5));
            _joinBtn5.UnregisterCallback<ClickEvent>(e => OnJoinClicked(GameNameEnum.Game5));
            _closeBtn5.UnregisterCallback<ClickEvent>(e => OnCloseClicked(GameNameEnum.Game5));
        }

        private void OnOpenClicked(GameNameEnum game)
        {
            //Log.Info("Opening " + game.GetGameName());
            Message message = new Message();
            message.SetAction(ActionEnum.CreateGame);
            message.SetGameNameEnum(game);
            message.Send();
        }

        private void OnJoinClicked(GameNameEnum game)
        {
            //Log.Info("Joining " + game.GetGameName());
            Message message = new Message();
            message.SetAction(ActionEnum.Join);
            message.SetGameNameEnum(game);
            message.Send();
        }

        private void OnCloseClicked(GameNameEnum game)
        {
            //Log.Info("Closing " + game.GetGameName());
            Message message = new Message();
            message.SetAction(ActionEnum.CloseGame);
            message.SetGameNameEnum(game);
            message.Send();
        }
    }
}

