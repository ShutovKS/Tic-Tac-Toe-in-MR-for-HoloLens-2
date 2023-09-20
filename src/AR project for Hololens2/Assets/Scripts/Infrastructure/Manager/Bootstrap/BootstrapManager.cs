using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.Manager.Bootstrap
{
	public class BootstrapManager : MonoBehaviour
	{
		private void Start()
		{
			SceneManager.LoadScene("2.Meta");
		}
	}
}