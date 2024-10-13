using UnityEngine;

internal interface IDamageable
{
    public float Health {set; get;}

    public bool Targetable {set; get;}

    public void OnHit(float damage, Vector2 knockback);

    public void OnHit(float damage);

    public void Heal(float boost);
    
    public void OnObjectDestroyed();

    public float getMaxhealth();
}