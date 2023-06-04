using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Turn_Move : MonoBehaviour
{
	public AudioSource audioeffect;
	ScoreManager _scoreManager;
	public int TurnX;
	public int TurnY;
	public int TurnZ;

	public int MoveX;
	public int MoveY;
	public int MoveZ;
	public static int score;
	public bool World;

	// Use this for initialization
	void Start()
	{
		_scoreManager = FindObjectOfType<ScoreManager>();
	}

	// Update is called once per frame
	void Update()
	{
		if (World == true)
		{
			transform.Rotate(TurnX * Time.deltaTime, TurnY * Time.deltaTime, TurnZ * Time.deltaTime, Space.World);
			transform.Translate(MoveX * Time.deltaTime, MoveY * Time.deltaTime, MoveZ * Time.deltaTime, Space.World);
		}
		else
		{
			transform.Rotate(TurnX * Time.deltaTime, TurnY * Time.deltaTime, TurnZ * Time.deltaTime, Space.Self);
			transform.Translate(MoveX * Time.deltaTime, MoveY * Time.deltaTime, MoveZ * Time.deltaTime, Space.Self);
		}
	}
  //  private void OnTriggerEnter(Collider other)
  //  {
  //      if(other.gameObject.tag=="Cube"||other.gameObject.name == "mainCube")
  //      {
		//	audioeffect.Play();
		//	_scoreManager.SetScore(1);
		//	Debug.Log(_scoreManager.Score);
		//	Destroy(gameObject);
		//}
  //  }
}