using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private float health;
    [SerializeField] private Slider slider;
    private Actor curr;
    //private bool hasFlashed;
    // Start is called before the first frame update
    void Start()
    {
        curr = transform.parent.GetComponent<Actor>();
        health = curr.GetHealth();
    }

    private void Update()
    {
        Debug.Log(health);
        health = curr.GetHealth();
        slider.value = health;
    }

}
