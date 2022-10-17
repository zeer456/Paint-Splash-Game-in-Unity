using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Player1 : MonoBehaviour
{

    public GameObject Image_2;
    public float speed=5f;
    Vector3 Begin_position;
    float x, y;

	public SpriteRenderer renderer;
	PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
		view = GetComponent<PhotonView>();
		GameObject pl = this.gameObject;
		pl.GetComponent<PhotonView>().Owner.TagObject = pl;

		Begin_position = Image_2.GetComponent<Transform>().position;

		//Image_2 = GameObject.Find("/Canvas/Image two/Image three").gameObject;
		Debug.Log(Image_2.name);
		//FindWithTag("MovePiece").GetComponent<GameObject>();
		//FindGameObjectWithTag("MovePiece")
		if (view.IsMine)
		{
			renderer.color = LobbyManager.getColor((string)PhotonNetwork.LocalPlayer.CustomProperties["playerColor"]);
		}
		else
        {
			renderer.color = LobbyManager.getColor((string)view.Owner.CustomProperties["playerColor"]);
		}
	}

	// Update is called once per frame
	void Update()
    {

		if (view.IsMine)
		{

			Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
			transform.position += input.normalized * speed * Time.deltaTime;

            /*
			x = Image_2.GetComponent<Transform>().position.x - Begin_position.x;
			y = Image_2.GetComponent<Transform>().position.y - Begin_position.y;

			if (x > 0.005 && transform.position.x <= 10)
			{
				transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
				Debug.Log("XXXXX");
				//transform.Translate(speed * Time.deltaTime, 0, 0);
			}
			if (x < -0.005 && transform.position.x >= -10)
			{
				transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
				//transform.Translate(-speed * Time.deltaTime, 0, 0);
			}
			if (y > 0.005 && transform.position.y <= 6)
			{
				transform.position += new Vector3( 0, speed * Time.deltaTime, 0);
				//transform.Translate(0, speed * Time.deltaTime, 0);
			}
			if (y < -0.005 && transform.position.y >= -6)
			{
				transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
				//transform.Translate(0, -speed * Time.deltaTime, 0);
			}*/
		}
    }

}
