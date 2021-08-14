using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    GameObject m_player;
    PLayerLogic m_playerLogic;

    GameObject[] m_coins;
    List<CoinLogic> m_coinLogics = new List<CoinLogic>();

    GameObject[] m_enemies;
    List<EnemyLogic> m_enemyLogics = new List<EnemyLogic>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        if (m_player)
        {
            m_playerLogic = m_player.GetComponent<PLayerLogic>();
        }

        m_coins = GameObject.FindGameObjectsWithTag("Coin");
        for (int index = 0; index < m_coins.Length; ++index)
        {
            m_coinLogics.Add(m_coins[index].GetComponent<CoinLogic>());
        }

        m_enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int index = 0; index < m_enemies.Length; ++index)
        {
            m_enemyLogics.Add(m_enemies[index].GetComponent<EnemyLogic>());
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
    }

    public void Save()
    {
        m_playerLogic.Save();

        for (int index = 0; index < m_coinLogics.Count; ++index)
        {
            m_coinLogics[index].Save(index);
        }

        for (int index = 0; index < m_enemyLogics.Count; ++index)
        {
            m_enemyLogics[index].Save(index);
        }
      
        PlayerPrefs.Save();
    }

    public void Load()
    {
        m_playerLogic.Load();

        for (int index = 0; index < m_coinLogics.Count; ++index)
        {
            m_coinLogics[index].Load(index);
        }

        for (int index = 0; index < m_enemyLogics.Count; ++index)
        {
            m_enemyLogics[index].Load(index);
        }
    }

}


/*
public static GameManager instance = null; // empty instance of the GameManger GameObject found in project


GameObject Player;
PLayerLogic playerLogic;

GameObject[] Coins; // The number of coin game objects in the scene
List<CoinLogic> coinLogics = new List<CoinLogic>(); //The list of added conlogics found for each coin GameObject found


void Awake()
{
    // singleton pattern to prevent more than one instance of this object

    if (instance == null)
    {
        instance = this;
    }
    else
    {
        Destroy(this);
    }
}



void Start()
{
    // search for the existence of Player
    Player = GameObject.FindGameObjectWithTag("Player");

    //Check for the existcnce
    if (Player)
    {
        playerLogic = GetComponent<PLayerLogic>();
    }
    //search for the existence of more than one Coin
    Coins = GameObject.FindGameObjectsWithTag("Coin");

    // how to iterate through the collection 
    for (int index = 0; index < Coins.Length; index++)
    {
        // what to do with each one idividually
        coinLogics.Add(Coins[index].GetComponent<CoinLogic>());
    }
}


void Update()
{

    // input checks
    if (Input.GetKeyDown(KeyCode.S))
    {
        Debug.Log("Saving");
        Save();
    }
    if (Input.GetKeyDown(KeyCode.L))
    {
        Load();
    }
}

public void Save()
{
    // what will be saved
    playerLogic.Save();

    // iterate throught the list of conlogics
    for (int index = 0; index < coinLogics.Count; index++)
    {
        // what to do for each conlogic idividually at the current index the iterator is on
        coinLogics[index].Save(index);
    }

    PlayerPrefs.Save();
}

public void Load()
{
    // what will be loaded
    playerLogic.Load();

    for (int index = 0; index < coinLogics.Count; index++)
    {
        // what to do for each conlogic idividually at the current index the iterator is on
        coinLogics[index].Load(index);
    }
}
*/