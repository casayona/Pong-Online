using Photon.Pun;
using TMPro;
using UnityEngine;

public class Ball : MonoBehaviourPunCallbacks
{
    public int oyuncu1Skor;
    public int oyuncu2Skor;
    public TextMeshProUGUI oyuncu1SkorText;
    public TextMeshProUGUI oyuncu2SkorText;

    private PhotonView pw;
    private Rigidbody2D rb;

    void Start()
    {
        pw = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody2D>();
    }

    [PunRPC]
    public void basla()
    {
        rb.velocity = new Vector3(5, 5, 0);
        Debug.Log("Oyun basladý");
        SkorGoster();
    }

    private void SkorGoster()
    {
        oyuncu1SkorText.text = PhotonNetwork.PlayerList[0].NickName + ":" + oyuncu1Skor.ToString();
        oyuncu2SkorText.text = PhotonNetwork.PlayerList[1].NickName + ":" + oyuncu2Skor.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (pw.IsMine)
        {
            if (collision.gameObject.name == "oyuncu1_kale")
            {
                pw.RPC("Gol", RpcTarget.All, 0, 1);
            }
            else if (collision.gameObject.name == "oyuncu2_kale")
            {
                pw.RPC("Gol", RpcTarget.All, 1, 0);
            }
        }
    }
 

    [PunRPC]
    public void Gol(int oyuncuBir, int oyuncuIki)
    {
        oyuncu1Skor += oyuncuBir;
        oyuncu2Skor += oyuncuIki;

        SkorGoster();
        Servis();
    }

    private void Servis()
    {
        rb.velocity = Vector3.zero;
        transform.position = Vector3.zero;
        rb.velocity = new Vector3(5, 5, 0);
    }
}
