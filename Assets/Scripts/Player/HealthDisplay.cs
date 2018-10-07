using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HealthDisplay : MonoBehaviour {

    private Image image;
    public Health health;

	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
	}

    public void FixedUpdate()
    {
        image.fillAmount = (float)health.currentHealth / health.maxHealth;
    }
}
