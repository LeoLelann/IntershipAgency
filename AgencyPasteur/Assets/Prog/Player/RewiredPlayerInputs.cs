using UnityEngine;
using System.Collections;
using Rewired;

[RequireComponent(typeof(CharacterController))]
public class MyCharacter : MonoBehaviour
{
    [Header("Rewired")]
    // The Rewired player id of this character
    private Player player; // The Rewired Player
    public int playerId = 0;

    [Header("Player")]
    public float moveSpeed = 3.0f;
    private CharacterController cc;
    private Vector3 moveVector;

    
    //[Header("Action1")]
    //public float bulletSpeed = 15.0f;
    //public GameObject bulletPrefab;
    //private bool fire;


    void Awake()
    {
        player = ReInput.players.GetPlayer(playerId);

        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        GetInput();
        ProcessInput();
    }

    private void GetInput()
    {
        moveVector.x = player.GetAxis("Move Horizontal");
        moveVector.y = player.GetAxis("Move Vertical");
        //fire = player.GetButtonDown("Fire");
    }

    private void ProcessInput()
    {
        if (moveVector.x != 0.0f || moveVector.y != 0.0f)
        {
            cc.Move(moveVector * moveSpeed * Time.deltaTime);
        }

/*        if (action1)
        {
            GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position + transform.right, transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(transform.right * bulletSpeed, ForceMode.VelocityChange);
        }*/
    }
}