using UnityEngine;

public class Enemy : MonoBehaviour
{
    private SpriteRenderer sr;

    [SerializeField] private float redColorDuration = 1;

    public float currentTimeInGame;
    public float lastTimeWasDamaged;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        ChangeColorIfNeeded();
    }

    private void ChangeColorIfNeeded()
    {
        currentTimeInGame = Time.time; // �A�v���P�[�V�������N�����Ă���̌��݂̃t���[����b�P�ʂŏo��

        if (currentTimeInGame > lastTimeWasDamaged + redColorDuration)
        {
            if (sr.color != Color.white)
                sr.color = Color.white;
        }
    }

    public void TakeDamage()
    {
        sr.color = Color.red;
        lastTimeWasDamaged = Time.time;

    }
}