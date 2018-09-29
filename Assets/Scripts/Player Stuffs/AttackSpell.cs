using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpell : MonoBehaviour {

	public enum SpellType
    {
        Ray,
        Projectile,
        Area
    }

    public SpellType type;
    public GameObject spellPrefab;
    public int damage;
    public int timeOut;

    public void CastSpell()
    {
        if (type == SpellType.Ray)
            CastRay();
        else if (type == SpellType.Projectile)
            CastProjectile();
        else if (type == SpellType.Area)
            CastArea();
    }

    private void CastRay()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);

        if (hit.collider != null)
            if (hit.collider.GetComponent<Health>())
                if (hit.collider.tag != tag)
                    hit.collider.GetComponent<Health>().DealDamage(damage);

    }

    //Casts a projectile
    private void CastProjectile()
    {
        GameObject clone;
        Vector3 spawnPoint = transform.position + Vector3.up;
        clone = Instantiate(spellPrefab, spawnPoint, Quaternion.identity);

        //Projectile's hold speed
        MagicProjectile mp = clone.GetComponent<MagicProjectile>();

        mp.caster = this;

        clone.GetComponent<Rigidbody2D>().velocity = mp.speed * Vector2.up;
    }

    private void CastArea()
    {
        GameObject clone;
        Vector3 spawnPoint = transform.position + Vector3.up;
        clone = Instantiate(spellPrefab, spawnPoint, Quaternion.identity);

        //Projectile's hold speed
        MagicProjectile mp = clone.GetComponent<MagicProjectile>();

        mp.caster = this;
    }
}
