using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Shoot : MonoBehaviour
{
    public GameObject Player;
    public GameObject window;
    public GameObject Bullet;

    public GameObject painting;
    public Sprite bluePainting;
    public Sprite yellowPainting;
    public Sprite redPainting;
    public Sprite greenPainting;
    public Sprite cyanPainting;

    private bool overlap = false;

    public Text scoreText;
    public static int score = 0;



	// Start is called before the first frame update
	void Start()
    {
		score = 0;
		//gameObject.GetComponent<PhotonView>().Owner;

		var photonViews = UnityEngine.Object.FindObjectsOfType<PhotonView>();
		foreach (var view in photonViews)
		{
			var player = view.Owner;
			//Objects in the scene don't have an owner, its means view.owner will be null
			if (player != null)
			{
				var playerPrefabObject = view.gameObject;

				Player = playerPrefabObject;
			}
		}

		//this.painting.GetComponent<SpriteRenderer>().sprite = getPaintSpriteFromColor((string)PhotonNetwork.LocalPlayer.CustomProperties["playerColor"]);

		//Debug.Log("Photon Shoot Code " + m);

	}

	public Sprite getPaintSpriteFromColor(string colorString)
    {
		if (colorString.Equals("red"))
		{
			return this.redPainting;
		}
		else if (colorString.Equals("green"))
		{
			return this.greenPainting;
		}
		else if (colorString.Equals("blue"))
		{
			return this.bluePainting;
		}
		else if (colorString.Equals("cyan"))
		{
			return this.cyanPainting;
		}
		else
		{
			return this.yellowPainting;
		}
	}

    public void Shot()
    {

		//if (IsMine)
		{
			var bullet = Instantiate(Bullet);

			//go = PhotonNetwork.Instantiate(Bullet, transform.position, transform.rotation, 0);

			bullet.transform.position = Camera.main.transform.position;

			bullet.GetComponent<Rigidbody>().velocity = (Player.transform.position - bullet.transform.position) * 4;

			Debug.Log("P:" + Player.transform.position);
			Debug.Log("B:" + bullet.transform.position);

			if (Player.transform.position.x < window.transform.position.x + window.transform.localScale.x / 2 && Player.transform.position.x > window.transform.position.x - window.transform.localScale.x / 2
				&& Player.transform.position.y < window.transform.position.y + window.transform.localScale.y / 2 && Player.transform.position.y > window.transform.position.y - window.transform.localScale.y / 2)
			{
				Debug.Log("hello");

				foreach (Transform child in window.transform)
				{

					Debug.Log(Player.transform.position.x.ToString());
					Debug.Log(Player.transform.position.y.ToString());
					Debug.Log(child.transform.position.x.ToString());
					Debug.Log(child.transform.position.y.ToString());
					if (Player.transform.position.x < child.transform.position.x + painting.transform.localScale.x && Player.transform.position.x > child.transform.position.x - painting.transform.localScale.x
				&& Player.transform.position.y < child.transform.position.y + painting.transform.localScale.y && Player.transform.position.y > child.transform.position.y - painting.transform.localScale.y)
					{
						Debug.Log("Overlap");
						overlap = true;
					}
				}

				if (!overlap)
				{

					var newPainting = PhotonNetwork.Instantiate(painting.name, Player.transform.position, Quaternion.identity, 0);
						//Instantiate(painting, Player.transform.position, Quaternion.identity);
					newPainting.transform.parent = window.transform;
					//Change Color?
					if (newPainting.GetPhotonView().IsMine)
					{
						newPainting.GetComponent<SpriteRenderer>().sprite = getPaintSpriteFromColor((string)PhotonNetwork.LocalPlayer.CustomProperties["playerColor"]);
					}
					else
					{
						newPainting.GetComponent<SpriteRenderer>().sprite = getPaintSpriteFromColor((string)newPainting.GetPhotonView().Owner.CustomProperties["playerColor"]);
					}
					score += 1;
					scoreText.text = score.ToString();
					//scoreText.text("hello");



				}
				overlap = false;
			}

		}

    }

}
