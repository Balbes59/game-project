using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    public InfoPlayer player;

    void Start()
    {
        healthBar = FindObjectOfType<Image>();
        player = FindObjectOfType<InfoPlayer>();
    }


    void Update()
    {
        healthBar.fillAmount = player.HP / player.maxHP;
    }
}