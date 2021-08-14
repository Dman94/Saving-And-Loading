using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Idle,
    StandingUp,
    Attack,
    Dead
}
public class EnemyLogic : MonoBehaviour
{
    float MoveSpeed = 3f;
    public float aggroRadius = 4.0f;


    public GameObject Player;
    Animator animator;
    CharacterController characterController;

    EnemyState m_enemyState = EnemyState.Idle;

    // Start is called before the first frame update
    void Start()
    {
        Player =  GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        characterController.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Player) { return; }

        float Distance = Vector3.Distance(Player.transform.position, transform.position);

        if(Distance < aggroRadius && animator  && m_enemyState == EnemyState.Idle)
        {
            m_enemyState = EnemyState.StandingUp;
            animator.SetTrigger("Player Nearby");
           
        }
    }
     void FixedUpdate()
    {
        if (m_enemyState == EnemyState.Attack && characterController)
        {
            characterController.Move(transform.forward * MoveSpeed * Time.deltaTime);
            Debug.Log("ATtack");
        }
       
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.25f);
        Gizmos.DrawSphere(transform.position, aggroRadius);
    }


    public void SetState(EnemyState enemyState)
    {
        m_enemyState = enemyState;

        if(m_enemyState == EnemyState.Attack)
        {
            characterController.enabled = true;
        }
        else if (m_enemyState == EnemyState.Dead && animator)
        {
            characterController.enabled = false;
            animator.SetBool("Dead", true);
            
        }
    }


    public void Save(int index)
    {
        PlayerPrefs.SetInt("EnemyState" + index, (int)m_enemyState);

        PlayerPrefs.SetFloat("EnemyPosX" + index, transform.position.x);
        PlayerPrefs.SetFloat("EnemyPosY" + index, transform.position.y);
        PlayerPrefs.SetFloat("EnemyPosZ" + index, transform.position.z);

                                              // retrieve information on the curent animationstate
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        

                                             //store the info as an integer value named animHash
        int animHash = info.fullPathHash;
                                             //Set an int to be saved for retrieval with its unique I.D plus the animHash value 
        PlayerPrefs.SetInt("EnemyAnimHash" + index, animHash);

                                            // store the animation time in a float called animTime
        float animTime = info.normalizedTime;
        PlayerPrefs.SetFloat("EnemyAnimTime" + index, animTime); //set an float to be saved for retrieval named EnemyAnimTime with its unique I.D plus the animTime value;
    }

    public void Load(int index) // passed index is for unique I.D when iterating through gameobject array of enemies
    {
        m_enemyState = (EnemyState)PlayerPrefs.GetInt("EnemyState" + index);

        float enemyPosX = PlayerPrefs.GetFloat("EnemyPosX" + index);
        float enemyPosY = PlayerPrefs.GetFloat("EnemyPosY" + index);
        float enemyPosZ = PlayerPrefs.GetFloat("EnemyPosZ" + index);

        characterController.enabled = false;
        transform.position = new Vector3(enemyPosX, enemyPosY, enemyPosZ);
        characterController.enabled = m_enemyState == EnemyState.Attack;

        animator.SetBool("Dead", m_enemyState == EnemyState.Dead);

        int animHash = PlayerPrefs.GetInt("EnemyAnimHash" + index);
        float animTime = PlayerPrefs.GetFloat("EnemyAnimTime" + index);
        animator.Play(animHash, 0, animTime);
    }
}
