using System.Collections.Generic;
using TicTacToe;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.Manager.CoreOnline
{
    public class CoreOnlineManager : NetworkBehaviour
    {
        [SerializeField] private GameObject _serverSelectionScreenPrefab;
        [SerializeField] private GameObject _gameFieldPrefab;

        private readonly CellType[] _gameBoard = new CellType[9];
        private readonly Dictionary<ulong, CellType> _clientToCellType = new Dictionary<ulong, CellType>();
        private ulong[] _clientIds = new ulong[2];
        private ulong _currentClient;

        private ServerSelection _serverSelection;
        private GameField _gameField;

        private void Start()
        {
            _gameField = new GameField();
            _serverSelection = new ServerSelection(_serverSelectionScreenPrefab);
            _serverSelection.CreateServerSelectionScreen(Vector3.zero, Quaternion.identity);
            _serverSelection.SetActivePanel(true);
            _serverSelection.RegisterHostButtonCallback(() =>
            {
                NetworkManager.Singleton.StartHost();
                NetworkManager.Singleton.OnClientConnectedCallback += ConnectedClientHandler;
                _serverSelection.SetActivePanel(false);
                CreatedGameField();

                for (var i = 0; i < _gameBoard.Length; i++)
                {
                    _gameBoard[i] = CellType.Empty;
                }
            });
            _serverSelection.RegisterClientButtonCallback(() =>
            {
                NetworkManager.Singleton.StartClient();
                CreatedGameField();
                _serverSelection.SetActivePanel(false);
            });
        }

        private void CreatedGameField()
        {
            _gameField.CreatedGameField(_gameFieldPrefab);
            _gameField.RegisterExitButtonsCallback(ExitFromGameServerRpc);
            _gameField.RegisterGiveUpButtonsCallback(GiveUpServerRpc);
            _gameField.RegisterCellButtonsCallback(cellIndex => { ButtonClickInvokeHandlerServerRpc(cellIndex); });
        }

        #region

        [ServerRpc(RequireOwnership = false)]
        private void ButtonClickInvokeHandlerServerRpc(int cellIndex, ServerRpcParams serverRpcParams = default)
        {
            Debug.Log($"ButtonClickInvokeHandlerServerRpc {cellIndex}");
            if (serverRpcParams.Receive.SenderClientId != _currentClient) return;
            if (_gameBoard[cellIndex] != CellType.Empty) return;
            var cellType = _clientToCellType[_currentClient];
            UpdateCellInBoardClientRpc(cellIndex, cellType);
            _gameBoard[cellIndex] = cellType;

            if (TicTacToeLogic.CheckForWin(_gameBoard, _clientToCellType[_currentClient]))
            {
                RestartGame();
            }
            else if (TicTacToeLogic.IsBoardFull(_gameBoard))
            {
                RestartGame();
            }
            else
            {
                _currentClient = _clientIds[0] == _currentClient ? _clientIds[1] : _clientIds[0];
            }
        }

        [ClientRpc]
        private void UpdateCellInBoardClientRpc(int cellIndex, CellType cellType)
        {
            _gameField.SetImageInCell(
                cellIndex,
                Resources.Load<Texture2D>(cellType == CellType.X
                    ? "Icons/Cross"
                    : "Icons/Zero"));
        }

        #endregion

        #region GiveUp

        [ServerRpc(RequireOwnership = false)]
        private void GiveUpServerRpc()
        {
            RestartGame();
            Debug.Log("GiveUpServerRpc");
        }

        private void RestartGame()
        {
            for (int i = 0; i < 9; i++)
            {
                _gameBoard[i] = CellType.Empty;
            }
            RestartGameClientRpc();
            Debug.Log("RestartGameClientRpc");
        }

        [ClientRpc]
        private void RestartGameClientRpc()
        {
            _gameField.DestroyGameField();
            CreatedGameField();
            Debug.Log("RestartGameClientRpc");
        }

        #endregion

        #region Exit

        [ServerRpc(RequireOwnership = false)]
        private void ExitFromGameServerRpc()
        {
            ExitInMainMenuClientRpc();
            NetworkManager.Singleton.Shutdown();
        }

        [ClientRpc]
        private void ExitInMainMenuClientRpc()
        {
            SceneManager.LoadScene("2.Meta");
        }

        #endregion

        private void ConnectedClientHandler(ulong clientId)
        {
            Debug.Log("ConnectedClientHandler");
            _clientIds[0] = clientId;
            _clientIds[1] = NetworkManager.Singleton.LocalClientId;
            _currentClient = clientId;
            _clientToCellType.Add(clientId, CellType.X);
            _clientToCellType.Add(NetworkManager.Singleton.LocalClientId, CellType.O);
        }
    }
}
