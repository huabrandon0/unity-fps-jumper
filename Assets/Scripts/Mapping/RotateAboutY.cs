using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAboutY : MonoBehaviour {

    //public Vector3 rotationPoint;
    public float rotateSpeed = 10f;

	void Update () {
		this.transform.Rotate(new Vector3(0f, rotateSpeed * Time.deltaTime, 0f));
	}
}
