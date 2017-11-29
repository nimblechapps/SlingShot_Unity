using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShotManager : MonoBehaviour {

	public GameObject ball;
	public Transform leftAnchor;
	public Transform rightAnchor;

	public Transform ObjectHolder;

	public Transform aimer;

	public LineRenderer[] lines;

	public static SlingShotManager instance;

	void Awake()
	{
		instance = this;
	}
	// Use this for initialization
	void Start () 
	{
		lines [0].SetPosition (0, leftAnchor.position);
		lines [1].SetPosition (0,rightAnchor.position);
		setPath (true);
	}
	
	// Update is called once per frame
	void Update ()
	{
		for (int i = 0; i < lines.Length; i++) {
			lines [i].SetPosition (1,ObjectHolder.position);
		}
	}
	public GameObject[] points;

	public void setPath(bool b)
	{
		float yPos = (aimer.up.y * 50) / points.Length;
		float val = yPos;
		for (int i = 0; i < points.Length; i++) {
			points [i].SetActive (b);
			points [i].transform.parent = aimer;
			points [i].transform.localPosition = new Vector3 (0,val,-0.8f);
			val += yPos;
		}
	}

	public float speed;
	public void throwBall()
	{
		ObjectHolder.GetComponent<Collider> ().enabled = false;
		GameObject clone = Instantiate (ball,ball.transform.position,Quaternion.identity)as GameObject;
		clone.SetActive (true);
		clone.GetComponent<Rigidbody> ().AddForce (aimer.up*speed,ForceMode.Impulse);
		Destroy (clone,3f);
	}
}
