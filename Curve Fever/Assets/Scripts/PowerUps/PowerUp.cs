using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(CircleCollider2D))]
public abstract class PowerUp : MonoBehaviour
{
    [SerializeField]
    protected float duration = 3f;

    protected abstract void StartAction();

    protected abstract IEnumerator EndAction();

    protected virtual void DisableVisuals()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
    }



}
