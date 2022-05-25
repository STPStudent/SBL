using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthControl : MonoBehaviour
{
    public float MaxHealth;
    public float CurrentHealth;
    public float DamageForceThreshold = 1f;
    public float DamageForceScale = 5f;
    public float spawnTime;

    public void SetHealth()
    {
        CurrentHealth = MaxHealth;
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        //Сейчас проверяет тег если это юнит то выполнябтся условия 
        if(!other.gameObject.CompareTag(gameObject.tag)
        && !other.gameObject.CompareTag("Untagged"))
        {
            //Вычитает из здоровья значение урона
            //если здоровье меньше нуля делает его нулем
            if(other.gameObject.name.Contains("BombPlayer") ||
           other.gameObject.name.Contains("BombEvil"))
            {
                GetDamage(15);
                return;
            }
            var attack = gameObject;
            GetDamage(other.gameObject.GetComponent<HealthControl>().DamageForceScale);
        }
    }

    public void GetDamage(float damage)
    {
        CurrentHealth -= (int)damage;
        CurrentHealth = Mathf.Max(0, CurrentHealth);
    }
    private void OnMouseOver()
    {
        CursorControl.SetAttackCursor();
    }

    private void OnMouseExit()
    {
        CursorControl.SetNormalCursor();
    }

    public virtual void DestroyObject()
    {
        Destroy(gameObject);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(!other.name.Contains("Unit")
            &&!other.name.Contains("Bomb")
            && other.gameObject.GetComponent<HealthControl>().spawnTime < spawnTime
            || name.Contains("Unit"))
        {
            var col = GetComponent<Collider2D>();
            if(other.bounds.Intersects(col.bounds))
            {
                transform.position = transform.position + Vector3.left + Vector3.down;
            }
        }
    }
}
