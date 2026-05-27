using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    [SerializeField] PlayerController _player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != _player) return;

        SceneManager.LoadScene("Game");
    }
}
