using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CoinState
{
    Active,
    Inactive
}

public class CoinLogic : MonoBehaviour
{
    Collider m_collider;
    MeshRenderer m_meshRenderer;

    AudioSource m_audioSource;

    [SerializeField]
    AudioClip m_coinSound;

    CoinState m_coinState = CoinState.Active;

    // Start is called before the first frame update
    void Start()
    {
        m_collider = GetComponent<Collider>();
        m_meshRenderer = GetComponent<MeshRenderer>();

        m_audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(2, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            m_collider.enabled = false;
            m_meshRenderer.enabled = false;

            m_coinState = CoinState.Inactive;

            PlaySound(m_coinSound);
        }
    }

    void PlaySound(AudioClip sound)
    {
        if (m_audioSource && sound)
        {
            m_audioSource.PlayOneShot(sound);
        }
    }

    public void Save(int index)
    {
        PlayerPrefs.SetInt("CoinState" + index, (int)m_coinState);
    }

    public void Load(int index)
    {
        m_coinState = (CoinState)PlayerPrefs.GetInt("CoinState" + index);
        if (m_coinState == CoinState.Active)
        {
            m_collider.enabled = true;
            m_meshRenderer.enabled = true;
        }
        else if (m_coinState == CoinState.Inactive)
        {
            m_collider.enabled = false;
            m_meshRenderer.enabled = false;
        }
    }
}

/*
    public enum CoinState
{
    Active, //MeshRender & Collider enabled
    Inactive //MeshRender & Collider disabled
}


public class CoinLogic : MonoBehaviour
{

    CoinState m_coinState = CoinState.Active; Collider collider;
    MeshRenderer meshRenderer;


    AudioSource audio;[SerializeField] AudioClip pickupSOund;


    void Start()
    {
        collider = GetComponent<Collider>();
        meshRenderer = GetComponent<MeshRenderer>();
        audio = GetComponent<AudioSource>();
        CoinState m_coinState = CoinState.Active;
    }

    void Update()
    {
        // Rotate Coin
        transform.Rotate(transform.right * 80 * Time.deltaTime, Space.World);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            collider.enabled = false;
            meshRenderer.enabled = false;
            audio.PlayOneShot(pickupSOund);

            // set the coin state inactive
            m_coinState = CoinState.Inactive;
        }
    }

    public void Save(int index)
    {
        // what will be saved  
        //Key             //Value
        //Key Value pair( ENum + I.D ) Casted m_coinState
        PlayerPrefs.SetInt("CoinState" + index, (int)m_coinState);

    }

    public void Load(int index)
    {
        //what will be loaded

        CoinState coinState = (CoinState)PlayerPrefs.GetInt("CoinState" + index);

        if (coinState == CoinState.Active)
        {
            collider.enabled = true;
            meshRenderer.enabled = true;
        }
        else if (coinState == CoinState.Inactive)
        {
            collider.enabled = false;
            meshRenderer.enabled = false;
        }
    }
}

*/
