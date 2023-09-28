using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.Manager.Bootstrap
{
    public class BootstrapManager : MonoBehaviour
    {
        [SerializeField] private GameObject _mrtkInputSimulator;
        private void Start()
        {
#if UNITY_EDITOR
            InitializedInputSimulator();
#endif
            SceneManager.LoadScene("2.Meta");
        }

        private void InitializedInputSimulator()
        {
            if (_mrtkInputSimulator == null)
            {
                Debug.LogError("MRTK Input Simulator is not set in BootstrapManager");
                return;
            }
            var simulator = Instantiate(_mrtkInputSimulator);
            DontDestroyOnLoad(simulator);
        }
    }
}
