using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayPanel : MonoBehaviour {
    
    [SerializeField] private FloatEditor feSens;
    [SerializeField] private FloatEditor feFov;
    [SerializeField] private FloatEditor feZoomedSens;
    [SerializeField] private FloatEditor feZoomedFov;

    [SerializeField] private FPCamera camScript;


    void Start()
    {
        RefreshFloatsUI();
        UpdateCameraFov();
    }

    public void SetSensitivity(float sens)
    {
        if (this.feSens.SetValue(sens))
            InputManager.instance.Sensitivity = sens;
    }

    public void SetFov(float fov)
    {
        if (this.feFov.SetValue(fov))
            InputManager.instance.Fov = fov;

        UpdateCameraFov();
    }

    public void SetZoomedSensitivity(float sens)
    {
        if (this.feZoomedSens.SetValue(sens))
            InputManager.instance.ZoomSensitivity = sens;
    }

    public void SetZoomedFov(float fov)
    {
        if (this.feZoomedFov.SetValue(fov))
            InputManager.instance.ZoomFov = fov;

        UpdateCameraFov();
    }

    public void SetSensitivity(string sens)
    {
        SetSensitivity(float.Parse(sens));
    }

    public void SetFov(string fov)
    {
        SetFov(float.Parse(fov));
    }

    public void SetZoomedSensitivity(string sens)
    {
        SetZoomedSensitivity(float.Parse(sens));
    }

    public void SetZoomedFov(string fov)
    {
        SetZoomedFov(float.Parse(fov));
    }

    public void RefreshFloatsUI()
    {
        feSens.SetValue(InputManager.instance.Sensitivity);
        feFov.SetValue(InputManager.instance.Fov);
        feZoomedSens.SetValue(InputManager.instance.ZoomSensitivity);
        feZoomedFov.SetValue(InputManager.instance.ZoomFov);
    }

    public void UpdateCameraFov()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu")
            this.camScript.RefreshFov();
    }
}
