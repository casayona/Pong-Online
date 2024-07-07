using Photon.Pun;
using TMPro;
using UnityEngine;

public class JoystickMove : MonoBehaviourPunCallbacks
{
    public Joystick movementJoystick;
    public float playerSpeed;
    private Rigidbody2D rb;
    public TextMeshProUGUI startText;

    private PhotonView pw;

    void Start()
    {
        pw = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody2D>();


        if (startText == null)
        {
            startText = GameObject.Find("Canvas/yazi_txt").GetComponent<TextMeshProUGUI>();
        }

        if (movementJoystick == null)
        {
            movementJoystick = GameObject.Find("Canvas/MainJoystick").GetComponent<Joystick>();
        }
        if (pw.IsMine)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                
                transform.position = new Vector3(0, -3, 0);
                transform.rotation = Quaternion.Euler(0, 0, 90);
                InvokeRepeating("oyuncukontrol", 0, 0.5f);
            }
            else if (!pw.IsMine)
            {
                transform.position = new Vector3(0, 3, 0);
                transform.rotation = Quaternion.Euler(0, 0, 90);
            }
        }
    }

    private void FixedUpdate()
    {
        if (pw.IsMine)
        {
            if (movementJoystick.Direction != Vector2.zero)
            {
                rb.velocity = new Vector2(movementJoystick.Direction.x * playerSpeed, movementJoystick.Direction.y * playerSpeed);
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    void oyuncukontrol()
    {
        if (PhotonNetwork.PlayerList.Length == 2)
        {
            pw.RPC("yazisil", RpcTarget.All,null);
            GameObject.Find("Top").GetComponent<PhotonView>().RPC("basla", RpcTarget.All);
            CancelInvoke("oyuncukontrol");
        }
      
    }

    [PunRPC]
    public void yazisil()
    {
        startText.text = null;
    }
}
