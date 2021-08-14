using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerLogic : MonoBehaviour
{

    float m_horizontalInput;
    float m_movementSpeed = 5.0f;

    CharacterController m_characterController;
    Animator m_animator;

    bool m_jump;
    float m_jumpHeight = 0.25f;
    float m_gravity = 0.981f;

    Vector3 m_horizontalMovement;
    Vector3 m_heightMovement;

    // Start is called before the first frame update
    void Start()
    {
        m_characterController = GetComponent<CharacterController>();
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        m_horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            m_jump = true;
        }

        if (m_animator)
        {
            m_animator.SetFloat("MovementInput", Mathf.Abs(m_horizontalInput));
        }
    }

    void UpdateRotation()
    {
        if (m_horizontalInput > 0.0f)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if (m_horizontalInput < 0.0f)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
    }

    void FixedUpdate()
    {
        UpdateRotation();

        if (m_jump)
        {
            m_heightMovement.y = m_jumpHeight;
            m_jump = false;
        }

        m_heightMovement.y -= m_gravity * Time.deltaTime;

        m_horizontalMovement = Vector3.right * m_horizontalInput * m_movementSpeed * Time.deltaTime;

        if (m_characterController)
        {
            m_characterController.Move(m_horizontalMovement + m_heightMovement);
        }

        if (m_characterController.isGrounded)
        {
            m_heightMovement.y = 0.0f;
        }
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("PositionX", transform.position.x);
        PlayerPrefs.SetFloat("PositionY", transform.position.y);
        PlayerPrefs.SetFloat("PositionZ", transform.position.z);

        PlayerPrefs.SetFloat("RotationX", transform.rotation.eulerAngles.x);
        PlayerPrefs.SetFloat("RotationY", transform.rotation.eulerAngles.y);
        PlayerPrefs.SetFloat("RotationZ", transform.rotation.eulerAngles.z);
    }

    public void Load()
    {
        float positionX = PlayerPrefs.GetFloat("PositionX");
        float positionY = PlayerPrefs.GetFloat("PositionY");
        float positionZ = PlayerPrefs.GetFloat("PositionZ");

        float rotationX = PlayerPrefs.GetFloat("RotationX");
        float rotationY = PlayerPrefs.GetFloat("RotationY");
        float rotationZ = PlayerPrefs.GetFloat("RotationZ");

        m_characterController.enabled = false;
        transform.position = new Vector3(positionX, positionY, positionZ);
        transform.rotation = Quaternion.Euler(rotationX, rotationY, rotationZ);
        m_characterController.enabled = true;
    }
}



/*
CharacterController characterController;
Animator animator;

float HorInput;
[SerializeField] float speed;
[SerializeField] bool Jump;
[SerializeField]  float jumpHeight;
[SerializeField]   float Gravity;
float Gravity_Multiplier;

Vector3 HorizontalMovement;
Vector3 HeightMovement;


// Start is called before the first frame update
void Start()
{
    characterController = GetComponent<CharacterController>();
    animator = GetComponent<Animator>();

}

// Update is called once per frame
void Update()
{
    HorInput = Input.GetAxis("Horizontal");

    if (Input.GetButtonDown("Jump"))
    {
        Jump = true;
    }

    if (animator)
    {
        animator.SetFloat("Horizontal Input", Mathf.Abs(HorInput));
    }


}

void FixedUpdate()
{
    UpdateROtation();

    if (Jump)
    {
        HeightMovement.y = jumpHeight;
        Jump = false;
    }

    HeightMovement.y -= Gravity * Time.deltaTime;



    HorizontalMovement = Vector3.right * HorInput * speed * Time.deltaTime;

    if (characterController)
    {
        characterController.Move(HorizontalMovement + HeightMovement);
    }

    if (characterController.isGrounded)
    {
        HeightMovement.y = 0;
    }


}

void UpdateROtation()
{
    if (HorInput > 0.0f)
    {
        transform.rotation = Quaternion.Euler(0, 90, 0);

    }
    else if (HorInput < 0.0f)
    {
        transform.rotation = Quaternion.Euler(0, -90, 0);

    }
}



public void Save()
{
    // PLayerprefs that will be saved

    PlayerPrefs.SetFloat("PositionX", transform.position.x);
    PlayerPrefs.SetFloat("PositionY", transform.position.y);
    PlayerPrefs.SetFloat("PositionZ", transform.position.z);

    PlayerPrefs.SetFloat("RotationX", transform.rotation.eulerAngles.x);
    PlayerPrefs.SetFloat("RotationY", transform.rotation.eulerAngles.y);
    PlayerPrefs.SetFloat("RotationZ", transform.rotation.eulerAngles.z);
}

public void Load()
{
    // PLayerprefs that will be gotten 

    float PositionX = PlayerPrefs.GetFloat("PositionX");
    float PositionY = PlayerPrefs.GetFloat("PositionY");
    float PositionZ = PlayerPrefs.GetFloat("PositionZ");

    float RotationX = PlayerPrefs.GetFloat("RotationX");
    float RotationY = PlayerPrefs.GetFloat("RotationY");
    float RotationZ = PlayerPrefs.GetFloat("RotationZ");

    //disable the controller so there is no fight on where the player position should be
    characterController.enabled = false;

    // Retrieved playerprefs put into a vector3
    transform.position = new Vector3(PositionX, PositionY, PositionZ);
    transform.rotation = Quaternion.Euler(RotationX, RotationY, RotationZ);

    // reenable the controller once position hase ben loaded
    characterController.enabled = true;
}
*/