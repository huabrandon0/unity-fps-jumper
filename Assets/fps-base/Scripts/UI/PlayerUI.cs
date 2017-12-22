// Usage: this script is meant to be placed on a UI object.
// The UI object must be a child of the Player.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour {

    private bool isPaused;
    [SerializeField] private GameObject pauseMenu = null;
    [SerializeField] private GameObject settingsMenu = null;
    [SerializeField] private TakesInput[] disableWhilePaused;

    private bool isTabbed;
    [SerializeField] private GameObject tabMenu = null;
    [SerializeField] private TakesInput[] disableWhileTabbed;

    void Awake()
    {
        if (this.pauseMenu == null)
            Debug.LogError(GetType() + ": No pause menu object assigned");

        if (this.settingsMenu == null)
            Debug.LogError(GetType() + ": No settings menu object assigned");

        if (this.tabMenu == null)
            Debug.LogError(GetType() + ": No tab menu object assigned");

        UnpauseScreen();
        UntabScreen();
    }

    void Update ()
    {
        // FIX LATER: change menu keycode to Esc only on the actual build
        // Unity has weird hotkeys built-in the game tab, so we're using "T" in the meantime.
        if (Input.GetKeyDown(KeyCode.T) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (!this.isPaused)
            {
                if (this.isTabbed)
                    UntabScreen();

                PauseScreen();
            }
            else
                UnpauseScreen();
        }

        if (Input.GetKeyDown(KeyCode.Tab) && !this.isPaused)
        {
            if (!this.isTabbed)
                TabScreen();
            else
                UntabScreen();
        }
    }

    public void PauseScreen()
    {
        // Unlock and show cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Disable Player's controls
        DisableInputs(this.disableWhilePaused);

        // Enable pause menu
        this.pauseMenu.SetActive(true);

        this.isPaused = true;
    }

    public void UnpauseScreen()
    {
        // Lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Enable Player's controls
        EnableInputs(this.disableWhilePaused);

        // Disable pause menu and settings menu
        this.pauseMenu.SetActive(false);
        this.settingsMenu.SetActive(false);

        this.isPaused = false;
    }

    public void TabScreen()
    {
        // Unlock and show cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Disable Player's controls
        DisableInputs(this.disableWhileTabbed);

        // Enable tab menu
        this.tabMenu.SetActive(true);

        this.isTabbed = true;
    }

    public void UntabScreen()
    {
        // Lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Enable Player's controls
        EnableInputs(this.disableWhileTabbed);

        // Disable tab menu
        this.tabMenu.SetActive(false);

        this.isTabbed = false;
    }

    void DisableInputs(TakesInput[] tis)
    {
        for (int i = 0; i < tis.Length; i++)
            tis[i].DisableInput();
    }

    void EnableInputs(TakesInput[] tis)
    {
        for (int i = 0; i < tis.Length; i++)
            tis[i].EnableInput();
    }
}
