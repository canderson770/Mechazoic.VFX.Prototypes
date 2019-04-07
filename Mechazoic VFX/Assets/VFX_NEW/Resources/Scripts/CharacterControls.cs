using UnityEngine;

public class CharacterControls : MonoBehaviour
{
    private CharacterController cc;
    private Vector3 direction;

    public float speed = 10;
    public float gravity = 9.8f;
    public float timeScale = .5f;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        //  if on ground
        if (cc.isGrounded)
        {
            //  get direction
            direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            direction = transform.TransformDirection(direction) * speed;
        }

        //  gravity
        direction.y -= gravity * Time.deltaTime;

        //  move
        cc.Move(direction * Time.deltaTime);

        if (Input.GetButton("Jump"))
        {
            Time.timeScale = timeScale;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
