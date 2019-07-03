using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCenter : MonoBehaviour {

	public Transform target;

	public float smoothSpeed = 5.0f;
	public Vector3 isoOffset;


	private void LateUpdate() {
        Vector3 desiredPos = target.position + isoOffset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPos;
	}
}
