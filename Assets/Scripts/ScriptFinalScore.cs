﻿using UnityEngine;
using System.Collections;

public class FinalScoreScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		TextMesh text = gameObject.GetComponent<TextMesh>();
		text.text = "Puntos: " + GameManagerScript.Instance.points;
	}
}
