using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public Collider2D swordCollider;
    public float damage = 3;

    public float knockbackForce = 500f;

    Vector2 attackOffset;

    // Start is called before the first frame update
    private void Start()
    {
        if(swordCollider == null)
        {
            Debug.LogWarning("Sword Collider not set");
        }
        attackOffset = transform.localPosition;
    }

    public void AttackRight()
    {
        swordCollider.enabled = true;
        transform.localPosition = new Vector3(attackOffset.x + 0.4624f, attackOffset.y - 0.1738f);
    }

    public void AttackLeft()
    {
        swordCollider.enabled = true;
        transform.localPosition = new Vector3((attackOffset.x + 0.4624f)*-1, attackOffset.y - 0.1738f);    }

    public void AttackUp()
    {
        swordCollider.enabled = true;
        transform.localPosition = new Vector3(attackOffset.x, attackOffset.y);
    }

    public void AttackDown()
    {
        swordCollider.enabled = true;
        transform.localPosition = new Vector3(attackOffset.x, attackOffset.y - 0.268f);
    }

    public void StopAttack()
    {
        transform.localPosition = new Vector3(attackOffset.x, attackOffset.y);
        swordCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        IDamageable damageableObject = collider.GetComponent<IDamageable>();
        if(damageableObject != null)
        {
            Vector3 parentPosition = gameObject.GetComponentInParent<Transform>().position;

            Vector2 direction = (Vector2) (collider.gameObject.transform.position - parentPosition).normalized;
            Vector2 knockback = direction * knockbackForce;

            damageableObject.OnHit(damage, knockback);

        }
        /*else
        {
            Debug.LogWarning("Collider does not implement IDamageable");
        }*/
    }
}
