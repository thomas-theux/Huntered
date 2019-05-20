using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class CharacterController : MonoBehaviour {

    public GameObject knifeGO;

    public int charID;
	private CharacterController cc;
    private Vector3 movement;

    private float moveSpeed = 2.0f;

    private bool didSwingKnife = false;

    // REWIRED
	private float moveHorizontal;
	private float moveVertical;
    private float rotateHorizontal;
    private float rotateVertical;
    private bool interactBtn;


    private void Awake() {
        cc = this.gameObject.GetComponent<CharacterController>();
    }


    private void Update() {
        GetInput();

        if (interactBtn && !didSwingKnife) {
            StartCoroutine(KnifeSwing());
        }
    }


    private void FixedUpdate() {
        MoveCharacter();
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void GetInput() {
        moveHorizontal = ReInput.players.GetPlayer(charID).GetAxis("LS Horizontal");
		moveVertical = ReInput.players.GetPlayer(charID).GetAxis("LS Vertical");

        rotateHorizontal = ReInput.players.GetPlayer(charID).GetAxis("RS Horizontal");
        rotateVertical = ReInput.players.GetPlayer(charID).GetAxis("RS Vertical");

        interactBtn = ReInput.players.GetPlayer(charID).GetButtonDown("X");
    }


    private void MoveCharacter() {
        // Get movement input
        movement = new Vector3(moveHorizontal, 0, moveVertical);

        // Normalize diagonal movement
        movement = Vector3.ClampMagnitude(movement, 1);

        // Move character
        cc.Move(movement * moveSpeed * Time.deltaTime);

        // Rotate character in the direction of movement
        if (movement != Vector3.zero) {
            transform.forward = movement;
        }

        // Rotate the player with the right stick
        Vector3 lookDirection = new Vector3(rotateHorizontal, 0, rotateVertical);
        transform.LookAt(transform.position + lookDirection);
    }


    private IEnumerator KnifeSwing() {
        didSwingKnife = true;

        knifeGO.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        knifeGO.SetActive(false);
    }

}
