using UnityEngine;


public class Portal : MonoBehaviour
{
    [SerializeField] private LayerMask _layerToTP;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _layerToTP)
        {
            // Invoke next Stage to start
        }
    }
}
