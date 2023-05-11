using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fuzzy_Logic;
using JetBrains.Annotations;
using Unity.VisualScripting;

public class AICarMove : MonoBehaviour
{
    public Transform player;
    public static float lifetime = 0; // for current gameplay
    public static float speedMultiplier = 0.01f; // to slow down speed from the range of 0 - 100 to 0 - 10

    // Related to fuzzy logic
    public TextAsset fuzzyLogicData; // the data file contain all your fuzzy logic
    private FuzzyLogic fuzzyLogic = null; // the fuzzy logic class constructor
    public float speed = 5f; // output of fuzzy logic, need to defuzzified
    public float distance; // an fuzzification, an input for fuzzy logic
    public static float previousLifetime = 1f; // another fuzzification, another input for fuzzy logic

    // Start is called before the first frame update
    void Start()
    {
        // Deserialize the bytes file to string
        fuzzyLogic = FuzzyLogic.Deserialize(fuzzyLogicData.bytes, null);
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseGame.gameIsPaused)
        {
            lifetime = carController.playingTimer;

            distance = Vector3.Distance(transform.position, player.position);

            fuzzyLogic.evaluate = true;

            // Perform Fuzzification on the distance and previous lifetime
            fuzzyLogic.GetFuzzificationByName("distance").value = distance;
            fuzzyLogic.GetFuzzificationByName("previous_lifetime").value = previousLifetime;

            // Defuzzify the output which is speed
            speed = fuzzyLogic.Output() * fuzzyLogic.defuzzification.maxValue;

            transform.Translate(new Vector3(0f, 0f, -1f) * (speed * Time.deltaTime * speedMultiplier));
        }
    }

}
