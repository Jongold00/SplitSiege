using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class UnitBehavior : MonoBehaviour
{
    Vector3 goal;
    private Animator anim;
    public NavMeshAgent nav;

    NavNode[] nodePath;
    int currentNode = 0;
    float epsilon = 1f;

    Healthbar healthbar;

    float maxHealth = 100;
    float health = 100;
    public float moveSpeed = 1.0f;

    [SerializeField]
    List<StatusEffect> activeEffects = new List<StatusEffect>();
   
    // Start is called before the first frame update
    void Start()
    {
        // setting below as placeholder until it's clear if unitData or the variables inside this class
        // will be used for things like the units health

        anim = GetComponentInChildren<Animator>();

        nodePath = FindObjectOfType<NavHolder>().navNodes;
        nav = GetComponent<NavMeshAgent>();

        goal = nodePath[currentNode].transform.position;
        nav.destination = goal;
        healthbar = GetComponentInChildren<Healthbar>();
        nav.speed = moveSpeed;

    }

    // Update is called once per frame
    public void Update()
    {
        TickStatusEffects(Time.deltaTime);

        if (Vector3.Distance(transform.position, goal) > epsilon)
        {
        }
        else
        {
            currentNode++;
            GetNewGoal();
        }
    }

    void GetNewGoal()
    {
        if (currentNode < nodePath.Length)
        {
            goal = nodePath[currentNode].transform.position;
            nav.destination = goal;
        }
    }

    public float GetDistanceFromEnd()
    {
        return Vector3.Distance(transform.position, goal);
    }

    public void TakeDamage(float delta)
    {
        health -= delta;
        healthbar.value = health / maxHealth;
        isDead();
      
    }
    public void GainHealth(int delta)
    {
        health = Mathf.Max(health + delta, maxHealth);
        float healthpercent = ((float)health / maxHealth);
        healthbar.value = healthpercent;
    }

    public void Stunned(float duration)
    {
        anim.SetBool("isStunned", true);
        Invoke("ResetStun", duration);
    }

    void ResetStun()
    {
        anim.SetBool("isStunned", false);
    }

    bool isDead()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            //nav.speed = 0;
            //anim.SetBool("isDead", true);
            return true;
        }
        return false;
    }



    #region StatusEffectMethods

    public void AttachStatusEffect(StatusEffect effect)
    {
        foreach (StatusEffect curr in activeEffects)
        {
            if (curr.compareID(effect.id))
            {
                curr.ReApply();
                return;
            }
        }
        activeEffects.Add(effect);
        effect.OnApply(this);
    }

    public void StatusEffectExpired(StatusEffect effect)
    {
        activeEffects.Remove(effect);
    }

    void TickStatusEffects(float deltaT)
    {
        if (activeEffects == null)
        {
            return;
        }

        for (int i = 0; i < activeEffects.Count; i++)
        {
            activeEffects[i].Tick(this, deltaT);
        }
    }

    

    #endregion
}
