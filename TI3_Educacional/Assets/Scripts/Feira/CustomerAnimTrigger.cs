using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerAnimTrigger : MonoBehaviour
{
    [SerializeField] Customer customer;

    public void AnimationTrigger()
    {
        customer.GreetingOver();
    }
}
