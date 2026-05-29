using UnityEngine;

public class CameraManagment : MonoBehaviour
{
    public Camera ViewCamera => _viewCamera;
    
    [SerializeField] private Camera _viewCamera;
}