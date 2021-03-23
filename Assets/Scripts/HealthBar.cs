using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider slider;
    [SerializeField] GameObject target;
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = target.GetComponent<Health>().GetHealth();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        slider.value = target.GetComponent<Health>().GetHealth();
    }
}
