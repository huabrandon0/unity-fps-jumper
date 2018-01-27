using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;

public class SwitchMenus : MonoBehaviour {

	private Animator anim;

	void Start()
	{
		this.anim = GetComponent<Animator>();
	}

	public void SwitchToMenu(int i)
	{
		this.anim.SetInteger("SwitchToMenu", i);
	}
}
