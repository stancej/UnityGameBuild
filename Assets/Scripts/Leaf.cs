
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Leaf : MonoBehaviour
{
    public float destroyTime = 25;
    public Animator anim;
    private float _speed = 10;
    private float _animMultiplier = 1;

    
    private Vector3 pos = Vector3.zero;
    private void Start()
    {
        StartCoroutine(nameof(DestroyRoutin));
    }

    public void SetValues(float speed, float animMultiplier)
    {
        _speed = speed;
        _animMultiplier = animMultiplier;

        anim.SetFloat("speed",animMultiplier);
    }

    private IEnumerator DestroyRoutin()
    {
        yield return new WaitForSeconds(destroyTime);
        GameObject.Destroy(gameObject);
    }
    private void LateUpdate()
    {
        transform.position += Vector3.down * _speed * Time.deltaTime;
    }

    public void ChangeImage(Sprite sprite)
    {
        var i = gameObject.GetComponentInChildren<Image>();
        if (i != null)
        {
            i.sprite = sprite;
        }
    }
}
