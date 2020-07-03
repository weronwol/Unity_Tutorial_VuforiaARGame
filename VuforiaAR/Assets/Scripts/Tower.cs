using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tower : MonoBehaviour
{
    public static Transform target;

    [Header("塔旋轉部位")]
    public Transform towerRotation;
    [Header("閃爍")]
    public ParticleSystem psShine;
    [Header("血量"), Range(100, 500)]
    public float hp;
    [Header("血條")]
    public Image hpBar;
    [Header("結束畫面")]
    public GameObject final;
    [Header("數量")]
    public Text textCount;
    [Header("殭屍死亡音效")]
    public AudioClip soundZombie;
    [Header("塔受傷音效")]
    public AudioClip soundHit;

    private int count;

    private float hpMax;

    private AudioSource aud;

    private void Start()
    {
        aud = GetComponent<AudioSource>();
        hpMax = hp;
    }

    private void Update()
    {
        Attack();
    }
    private void Attack()
    {
        if (target && hp > 0)
        {
            Vector3 pos = target.position;
            pos.y = towerRotation.position.y;
            psShine.Play();

            towerRotation.LookAt(pos);

            count++;

            aud.PlayOneShot(soundZombie);
        }
    }
    public void Damage(float damage)
    {
        hp -= damage;
        hpBar.fillAmount = hp / hpMax;
        aud.PlayOneShot(soundHit);

        if (hp <= 0)
        {
            final.SetActive(true);
            textCount.text = "zombie killed :" + count;
        }
    }

    public void Replay()
    {
        SceneManager.LoadScene("遊戲場景");
    }

    public void Quit()
    {
        Application.Quit();
    }
} 
