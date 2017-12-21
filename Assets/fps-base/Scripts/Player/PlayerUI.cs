// Usage: this script is meant to be placed on a UI object.
// The UI object must be a child of the Player.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour {

    private bool isPaused;
    [SerializeField] private GameObject pauseMenu = null;
    [SerializeField] private TakesInput[] disableWhilePaused;

    private bool isTabbed;
    [SerializeField] private GameObject tabMenu = null;
    [SerializeField] private TakesInput[] disableWhileTabbed;

    void Awake()
    {
        if (this.pauseMenu == null)
            Debug.LogError(GetType() + ": No pause menu object assigned");
        
        this.isPaused = false;
        UnpauseScreen();

        this.isTabbed = false;
        UntabScreen();
    }

    void Update ()
    {
        // FIX LATER: change menu keycode to Esc on the actual build
        // Unity has weird hotkeys built-in the game tab, so we're using "T" in the meantime.
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (!this.isPaused)
            {
                if (this.isTabbed)
                {
                    UntabScreen();
                    this.isTabbed = false;
                }

                PauseScreen();
                this.isPaused = true;
            }
            else
            {
                UnpauseScreen();
                this.isPaused = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab) && !this.isPaused)
        {
            if (!this.isTabbed)
            {
                TabScreen();
                this.isTabbed = true;
            }
            else
            {
                UntabScreen();
                this.isTabbed = false;
            }
        }
    }

    void PauseScreen()
    {
        // Unlock and show cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Disable Player's controls
        DisableInputs(disableWhilePaused);

        // Enable pause menu
        this.pauseMenu.SetActive(true);
    }

    void UnpauseScreen()
    {
        // Lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Enable Player's controls
        EnableInputs(disableWhilePaused);

        // Disable pause menu
        this.pauseMenu.SetActive(false);
    }

    void TabScreen()
    {
        // Unlock and show cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Disable Player's controls
        DisableInputs(disableWhileTabbed);

        // Enable tab menu
        this.tabMenu.SetActive(true);
    }

    void UntabScreen()
    {
        // Lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Enable Player's controls
        EnableInputs(disableWhileTabbed);

        // Disable tab menu
        this.tabMenu.SetActive(false);
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
