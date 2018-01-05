using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReset : TakesInput {

    private bool reset = false;

	void Update()
    {
        GetInput();

        if (this.reset)
            GameManager.instance.ResetGame();
	}

    protected override void GetInput()
    {
        if (!this.canReadInput)
            return;

        this.reset = InputManager.instance.GetKeyDown("Reset");
    }

    protected override void ClearInput()
    {
        this.reset = false;
    }

    protected override void GetDefaultState(){}

    protected override void SetDefaultState()
    {
        this.reset = false;
    }
}
