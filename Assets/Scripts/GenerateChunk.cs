﻿using UnityEngine;
using System.Collections;

public class GenerateChunk : MonoBehaviour {

	public GameObject DirtTile;
	public GameObject GrassTile;
	public GameObject StoneTile;

	public int width;
	public float heightMultiplier;
	public int heightAddition;

	public float smoothness;

	[HideInInspector]
	public float seed;

	public GameObject tileCoal;
	public GameObject tileDiamond;
	public GameObject tileGold;
	public GameObject tileIron;

	public float chanceCoal;
	public float chanceDiamond;
	public float chanceGold;
	public float chanceIron;

	void Start () {
		Generate ();
	}

	public void Generate () {
		for (int i = 0; i < width; i++) {
			int h = Mathf.RoundToInt (Mathf.PerlinNoise (seed, (i + transform.position.x) / smoothness) * heightMultiplier) + heightAddition;
			for (int j = 0; j < h; j++) {
				GameObject selectedTile;
				if (j < h - 4) {
					selectedTile = StoneTile;
				} else if (j < h - 1) {
					selectedTile = DirtTile;
				} else {
					selectedTile = GrassTile;
				}

				GameObject newTile = Instantiate (selectedTile, Vector3.zero, Quaternion.identity) as GameObject;
				newTile.transform.parent = this.gameObject.transform;
				newTile.transform.localPosition = new Vector3 (i, j);
			}
		}
		Populate();
	}

	public void Populate() {
		foreach(GameObject t in GameObject.FindGameObjectsWithTag("TileStone")){
			if (t.transform.parent == this.gameObject.transform) {
				float r = Random.Range (0f, 100f);
				GameObject selectedTile = null;

				if (r < chanceDiamond) {
					selectedTile = tileDiamond;
				} else if (r < chanceGold) {
					selectedTile = tileGold;
				} else if (r < chanceIron) {
					selectedTile = tileIron;
				} else if (r < chanceCoal) {
					selectedTile = tileCoal;
				}

				if (selectedTile != null) {
					GameObject newResourceTile = Instantiate (selectedTile, t.transform.position, Quaternion.identity) as GameObject;
					//newResourceTile.transform.parent = transform;
					Destroy (t);
				}
			}
		}
	}
}
