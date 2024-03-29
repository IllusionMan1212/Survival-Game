﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {

    [SerializeField]
    private Transform playerRoot, lookRoot;

    [SerializeField]
    private bool invert;

    [SerializeField]
    private bool can_Unlock = true;

    [SerializeField]
    private float sensitivity = 5f;

    [SerializeField]
    private int smooth_Steps = 10;

    [SerializeField]
    private float smooth_Weight = 0.4f;

    [SerializeField]
    private float roll_Angle = 0f;

    [SerializeField]
    private float roll_speed = 3f;

    [SerializeField]
    private Vector2 default_Look_Limits = new Vector2(-60f, 80f);

    private Vector2 look_Angles;

    private Vector2 current_Mouse_Look;
    private Vector2 smooth_Move;

    private float current_Roll_Angle;

    private int last_Look_Frame;


	// Use this for initialization
	void Start () {

        Cursor.lockState = CursorLockMode.Locked;

	}

	// Update is called once per frame
	void Update () {
        LockAndUnlockCursor();

        if (Cursor.lockState == CursorLockMode.Locked) {
            LookAround();
        }
	}

    void LockAndUnlockCursor() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (Cursor.lockState == CursorLockMode.Locked) {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            } else {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    void LookAround() {
        current_Mouse_Look = new Vector2(
                                        Input.GetAxis(MouseAxis.MOUSEY),
                                        Input.GetAxis(MouseAxis.MOUSEX)
                                        );

        look_Angles.x += current_Mouse_Look.x * sensitivity * (invert ? 1f : -1f);
        look_Angles.y += current_Mouse_Look.y * sensitivity;

        look_Angles.x = Mathf.Clamp(look_Angles.x, default_Look_Limits.x, default_Look_Limits.y);

        current_Roll_Angle = Mathf.Lerp(
                                        current_Roll_Angle,
                                        Input.GetAxisRaw(MouseAxis.MOUSEX) * roll_Angle,
                                        Time.deltaTime * roll_speed
                                       );

        lookRoot.localRotation = Quaternion.AngleAxis(look_Angles.x, Vector3.right);
        playerRoot.localRotation = Quaternion.AngleAxis(look_Angles.y, playerRoot.transform.up);
    }
}
