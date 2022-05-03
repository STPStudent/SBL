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
    
    void OnCollisionEnter2D(Collision2D other) 
    {
        ///Этот метод нужен для получения урона от бомбы
        if(other.gameObject.tag == "BombPlayer" && gameObject.tag == "Bot" ||
           other.gameObject.tag == "BombBot" && gameObject.tag == "Player")
        {
            GetDamage(10);
        }
    }

    void OnCollisionStay2D(Collision2D other) 
    {
        ///Сейчас проверяет тег если это юнит то выполнябтся условия 
        if(other.gameObject.tag != gameObject.tag
        && other.gameObject.tag != "Untagged")
        {
            if(Time.time % 3 < 1.5)
                return;
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
