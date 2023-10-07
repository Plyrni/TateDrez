using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get => instance; set => instance = value; }
    static CameraManager instance;

    [SerializeField] public Camera mainCamera;

    private void Awake()
    {
        instance = this;
    }
}
