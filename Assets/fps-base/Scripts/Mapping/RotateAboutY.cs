using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAboutY : MonoBehaviour {

    public Vector3 rotationPoint;
    public float rotateSpeed = 3f;

	void Update () {
        this.transform.RotateAround(this.rotationPoint, Vector3.up, rotateSpeed * Time.deltaTime);
	}
}
