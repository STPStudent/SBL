using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthControl : MonoBehaviour
{
    public float MaxHealth;
    public float CurrentHealth;
    public float DamageForceThreshold = 1f;
    public float DamageForceScale = 5f;


    public void SetHealth()
    {
        CurrentHealth = MaxHealth;
    }

    void OnCollisionStay2D(Collision2D other) 
    {
        var timeNow = Time.time;
        if(timeNow - Mathf.Floor(timeNow) > 0.4)
            return;
        ///Сейчас проверяет тег если это юнит то выполнябтся условия 
        if(other.gameObject.tag != gameObject.tag
        && other.gameObject.tag != "Untagged")
        {
            //Вычитает из здоровья значение урона
            //если здоровье меньше нуля делает его нулем
            var attack = gameObject;
            GetDamage(other.gameObject.GetComponent<HealthControl>().DamageForceScale);
        }
    }

    public void GetDamage(float damage)
    {
        CurrentHealth -= (int)damage;
        CurrentHealth = Mathf.Max(0, CurrentHealth);
    }

    public virtual void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
