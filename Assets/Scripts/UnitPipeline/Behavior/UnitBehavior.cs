using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class UnitBehavior : MonoBehaviour
{
    private Animator anim;

    UnitNavigation nav;


    Healthbar healthbar;

    float maxHealth = 100;
    float health = 100;
    public float moveSpeed = 1.0f;

    public static List<UnitBehavior> allEnemies = new List<UnitBehavior>();

    // baseOfUnit is intended to be used for things like AoE which should appear to target
    // the ground under a unit rather than the unit itself
    [SerializeField] Transform baseOfUnit;
    public Transform BaseOfUnit { get => baseOfUnit; set => baseOfUnit = value; }

    private void OnEnable()
    {
        allEnemies.Add(this);
        nav = GetComponent<UnitNavigation>();
    }

    private void OnDisable()
    {
        allEnemies.Remove(this);
    }

    [SerializeField]
    List<StatusEffect> activeEffects = new List<StatusEffect>();



    // Start is called before the first frame update
    void Start()
    {
        // setting below as placeholder until it's clear if unitData or the variables inside this class
        // will be used for things like the units health

        anim = GetComponentInChildren<Animator>();


        healthbar = GetComponentInChildren<Healthbar>();


    }

    // Update is called once per frame
    public void Update()
    {
        TickStatusEffects(Time.deltaTime);

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
            anim.SetTrigger("Death");
            nav.speed = 0;
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
