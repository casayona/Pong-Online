using UnityEngine;
using Photon.Pun;
using TMPro;

public class Hareket : MonoBehaviourPunCallbacks
{
    PhotonView pw;
    public int speed;

    public TextMeshProUGUI sceneText;

    void Start()
    {
        sceneText = GameObject.Find("Canvas/yazi_txt").GetComponent<TextMeshProUGUI>();
        pw = GetComponent<PhotonView>();
        if (!pw.IsMine)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                transform.position = new Vector3(7, 0, 0);
                InvokeRepeating("oyuncukontrol", 0, 0.5f);
            }
            else if (!PhotonNetwork.IsMasterClient)
            {
                transform.position = new Vector3(-7, 0, 0);

            }
        }
    }

    void oyuncukontrol()
    {
        if (PhotonNetwork.PlayerList.Length == 2)
        {
            pw.RPC("yazýsil", RpcTarget.All, null);
            GameObject.Find("Ball").GetComponent<PhotonView>().RPC("oyunbasla", RpcTarget.All, null);
            CancelInvoke("oyuncukontrol");
        }
    }


    //[PunRPC]
    //public void yazýsil()
    //{
    //    yazi_text.text = null;
    //}
    void Update()
    {
        if (photonView.IsMine)
        {

            // Oyuncunun hareketini kontrol etme kodlarý buraya gelir
            float moveVertical = Input.GetAxis("Mouse Y") * Time.deltaTime * speed;

            transform.Translate(0, moveVertical, 0);

        }
    }


}
