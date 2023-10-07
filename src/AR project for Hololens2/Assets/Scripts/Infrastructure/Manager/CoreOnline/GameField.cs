using UI;
using UnityEngine;
using UnityEngine.Events;

namespace Infrastructure.Manager.CoreOnline
{
    public class GameField
    {
        private GameObject _gameFieldGameObject;
        private GameFieldUI _gameFieldUI;

        public void CreatedGameField(GameObject gameFieldPrefab)
        {
            _gameFieldGameObject = Object.Instantiate(gameFieldPrefab);
            _gameFieldUI = _gameFieldGameObject.GetComponent<GameFieldUI>();
        }
        
        public void DestroyGameField() => Object.Destroy(_gameFieldGameObject);

        public void RegisterCellButtonsCallback(UnityAction<int> callback) => _gameFieldUI.RegisterCellButtonsCallback(callback);
        public void RegisterGiveUpButtonsCallback(UnityAction callback) => _gameFieldUI.RegisterGiveUpButtonsCallback(callback);
        public void RegisterExitButtonsCallback(UnityAction callback) => _gameFieldUI.RegisterExitButtonsCallback(callback);
        
        public void SetImageInCell(int cellIndex, Texture2D texture) => _gameFieldUI.SetImageInCell(cellIndex, texture);
    }
}
