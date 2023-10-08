using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using UnityEngine.Windows.WebCam;
using UnityEngine.Events;

public enum eCamType
{
    Home,
    Game,
}

[DefaultExecutionOrder(-1)]
public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get => instance; set => instance = value; }
    static CameraManager instance;

    [SerializeField] public Camera mainCamera;
    [SerializeField] CinemachineVirtualCamera vcam_Game;
    [SerializeField] CinemachineVirtualCamera vcam_Home;
    CinemachineOrbitalTransposer _transposerHome;
    [SerializeField] float _homeCamSpeed = 30f;
    /// <summary> newCamType, previousCamType</summary>
    public UnityEvent<eCamType, eCamType> OnSetCamera; 
    private eCamType _currentCamType;

    private void Awake()
    {
        instance = this;
        this.OnSetCamera.AddListener(this.OnSetCam);
        this._transposerHome = vcam_Home.GetCinemachineComponent<CinemachineOrbitalTransposer>();

    }

    private void Update()
    {
        if (this._currentCamType == eCamType.Home)
        {
            this._transposerHome.m_XAxis.Value += this._homeCamSpeed * Time.deltaTime;
        }
    }

    public void SetCamera(eCamType camType)
    {
        this.DisableAllCamera();
        this.GetCam(camType).Priority = 10;
        OnSetCamera?.Invoke(camType, this._currentCamType);
        this._currentCamType = camType;
    }
    private CinemachineVirtualCamera GetCam(eCamType vcamType)
    {
        switch (vcamType)
        {
            case eCamType.Home:
                return vcam_Home;
            case eCamType.Game:
                return vcam_Game;
            default:
                break;
        }

        return null;
    }
    private void DisableAllCamera()
    {
        foreach (eCamType currentEnumValue in Enum.GetValues(typeof(eCamType)))
        {
            CinemachineVirtualCamera virtCam = this.GetCam(currentEnumValue);
            if (virtCam != null)
            {
                virtCam.Priority = 0;
            }
        }
    }

    private void OnSetCam(eCamType newCamType, eCamType previousCamType)
    {
        if (newCamType == eCamType.Home && previousCamType != newCamType)
        {
            this._transposerHome.m_XAxis.Value = 0;
        }
    }
    private void OnDestroy()
    {
        this.OnSetCamera.RemoveListener(this.OnSetCam);
    }
}
