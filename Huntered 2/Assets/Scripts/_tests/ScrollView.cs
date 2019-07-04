using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollView : MonoBehaviour {

    private ScrollRect scrollRectangle;
    public GameObject ScrollContent;

    private float scrollAmount;


    private void Awake() {
        scrollRectangle = GetComponent<ScrollRect>();

        float children = ScrollContent.transform.childCount;

        scrollRectangle.verticalNormalizedPosition = 1;

        float newHeight = (children * 70) + ((children - 1) * 20) + 40;
        ScrollContent.GetComponent<RectTransform>().sizeDelta = new Vector2(100, newHeight);

        float contentHeight = ScrollContent.GetComponent<RectTransform>().sizeDelta.y;
        scrollAmount = 1 / (contentHeight / 8);
    }


    private void Update() {
        // scroll down
        if (Input.GetKey("g")) {
            scrollRectangle.verticalNormalizedPosition -= scrollAmount;
        }

        // scroll up
        if (Input.GetKey("t")) {
            scrollRectangle.verticalNormalizedPosition += scrollAmount;
        }
    }

}
