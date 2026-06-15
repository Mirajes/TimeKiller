using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera ExternalCamera => _externalCamera;

    [SerializeField] private Camera _externalCamera;
}