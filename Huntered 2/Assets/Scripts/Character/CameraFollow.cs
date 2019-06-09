using UnityEngine;
using Rewired;

public class CameraFollow : MonoBehaviour {

	public int cameraID = 0;

	private Transform target;

	public float smoothSpeed = 5.0f;
	public Vector3 isoOffset;

    private float posX = 0;


    public void InitializeCamera() {
        // cameraID = transform.parent.GetComponent<PlayerSheet>().playerID;

        Camera playerCam = this.gameObject.GetComponent<Camera>();
        if (cameraID == 1) { posX = 0.5f; }
        playerCam.rect = new Rect(posX, 0, 0.5f, 1.0f);

        target = transform.parent.transform;
        transform.parent = null;
    }


	private void LateUpdate() {
        Vector3 desiredPos = target.position + isoOffset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPos;
	}
}