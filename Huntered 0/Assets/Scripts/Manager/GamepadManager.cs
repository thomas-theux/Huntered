using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class GamepadManager : MonoBehaviour {

    public void InitializeGamepads() {
        ReInput.ControllerConnectedEvent += OnControllerConnected;
        ReInput.ControllerDisconnectedEvent += OnControllerDisconnected;

        // connectedGamepads = ReInput.controllers.joystickCount;
		GameSettings.ConnectedGamepads = 2;

        GameSettings.PlayerCount = GameSettings.ConnectedGamepads;
    }

    void OnControllerConnected(ControllerStatusChangedEventArgs args) {
		if (GameSettings.ConnectedGamepads < GameSettings.PlayerMax) {
			// connectedGamepads = ReInput.controllers.joystickCount;
		} else {
			// print("No more controllers allowed");
		}
	}

    void OnControllerDisconnected(ControllerStatusChangedEventArgs args) {
		if (GameSettings.ConnectedGamepads > 0) {
			// connectedGamepads = ReInput.controllers.joystickCount;
		} else {
			// print("No more controllers to disconnect");
		}
	}

}
