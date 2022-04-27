
using UnityEngine;

public class StatsBuff : Buff
{
    [SerializeField] private float _animBuff = 2.0f;
    public float animBuff { get; set; }
    
    [SerializeField] private float _speedBuff = 1.25f;
    public float speedBuff { get; set; }

    [SerializeField] private float _loopBuff = 2.0f;
    public float loopBuff { get; set; }

    [SerializeField] private float t_speedBuff = 50;
    [SerializeField] private float t_animBuff = 30;
    [SerializeField] private float t_loopBuff = 40;

    protected new void Awake()
    {
        base.Awake();
        speedBuff = _speedBuff;
        loopBuff = _loopBuff;
        animBuff = _animBuff;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        var buffContoller = collision.GetComponent<BuffContoller>();

        if(buffContoller == null)
            return;

        var i = Random.Range(1, 4);
        switch (i)
        {
            case 1:
                buffContoller.AddTime(BuffContoller.Buffs.Anim,t_animBuff,animBuff);
                break;

            case 2:
                buffContoller.AddTime(BuffContoller.Buffs.Speed,t_speedBuff,speedBuff);
                break;
            case 3:
                buffContoller.AddTime(BuffContoller.Buffs.Loop,t_loopBuff,loopBuff);
                break;
        }
        GameObject.Destroy(gameObject);
    }
}
