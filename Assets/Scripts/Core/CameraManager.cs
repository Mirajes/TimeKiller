using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera ViewCamera => _viewCamera;
    
    [SerializeField] private Camera _viewCamera;
}