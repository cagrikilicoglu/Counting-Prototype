using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{

    public PlayerController playerControllerScript;
    public ParticleSystem smokeParticle;

    private void Start()
    {
        playerControllerScript = GameObject.Find("Cannon").GetComponent<PlayerController>();
    }

    // count the number of guns triggering collider inside the box

    private IEnumerator OnTriggerEnter(Collider other)
    {
        var smokeEffect = Instantiate(smokeParticle, other.transform.position, smokeParticle.transform.rotation);
        yield return new WaitForSeconds(0.1f);
        playerControllerScript.ScoreCounter();
        Destroy(gameObject);
        other.gameObject.SetActive(false);
        Destroy(smokeEffect);

    }
}
