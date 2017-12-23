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
        feSens.SetValue(InputManager.sens);
        feFov.SetValue(InputManager.fov);
        feZoomedSens.SetValue(InputManager.zoomedSens);
        feZoomedFov.SetValue(InputManager.zoomedFov);

        UpdateCameraFov();
    }

    public void SetSensitivity(float sens)
    {
        if (this.feSens.SetValue(sens))
            InputManager.sens = sens;
    }

    public void SetFov(float fov)
    {
        if (this.feFov.SetValue(fov))
            InputManager.fov = fov;

        UpdateCameraFov();
    }

    public void SetZoomedSensitivity(float sens)
    {
        if (this.feZoomedSens.SetValue(sens))
            InputManager.zoomedSens = sens;
    }

    public void SetZoomedFov(float fov)
    {
        if (this.feZoomedFov.SetValue(fov))
            InputManager.zoomedFov = fov;

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

    private void UpdateCameraFov()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu")
            this.camScript.RefreshFov();
    }
}
