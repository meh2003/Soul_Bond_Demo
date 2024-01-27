using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Demo
{
    public class PlayerHUD : MonoBehaviour
    {
        [SerializeField] private Image healthBar;
         [SerializeField] private Image manaBar;
         [SerializeField] private Image bossHealthBar;//Should be put in a separate class
         

         //Animate smoothly later
         private float _smoothHealth;
         private float _healthLastFrame;
         private float _smoothMana;
         private float _manaLastFrame;
        private void Update()
        {
            _smoothHealth = Mathf.Lerp(healthBar.fillAmount, GamePlayManager.Instance.Player.GetHealthNormalized(), 0.5f);
            _smoothMana = Mathf.Lerp(manaBar.fillAmount, GamePlayManager.Instance.Player.GetManaNormalized(), 0.5f);

            healthBar.fillAmount = _smoothHealth;
            manaBar.fillAmount = _smoothMana;

            bossHealthBar.fillAmount = GamePlayManager.Instance.Boss.GetBossHealthNormalized();
        }
    }
}
