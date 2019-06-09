using UnityEngine;
using Rewired;

public class CameraFollow : MonoBehaviour {

	public int cameraID = 0;

	private Transform target;

	public float smoothSpeed = 5.0f;
	public Vector3 isoOffset;


    private void Awake() {
        target = transform.parent.transform;
        transform.parent = null;
    }


	private void LateUpdate() {
        Vector3 desiredPos = target.position + isoOffset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPos;
	}
}