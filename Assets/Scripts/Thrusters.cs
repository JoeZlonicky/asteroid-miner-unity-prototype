using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrusters : MonoBehaviour
{
    [SerializeField] private ShipController ship;
    [SerializeField] private ParticleSystem leftParticles;
    [SerializeField] private ParticleSystem rightParticles;

    private void Update()
    {
        if (ship.InputVector.y > 0 || ship.InputVector.x > 0)
        {
            if (!leftParticles.isEmitting) leftParticles.Play();
        }
        else if (leftParticles.isEmitting)
        {
            leftParticles.Stop();
        }

        if (ship.InputVector.y > 0 || ship.InputVector.x < 0)
        {
            if (!rightParticles.isEmitting) rightParticles.Play();
        }
        else if (rightParticles.isEmitting)
        {
            rightParticles.Stop();
        }
    }
}
