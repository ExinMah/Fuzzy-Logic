using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fuzzy_Logic;
using JetBrains.Annotations;
using Unity.VisualScripting;

public class AICarMove : MonoBehaviour
{
    public Transform player;
    public TextAsset fuzzyLogicData;
    private FuzzyLogic fuzzyLogic = null;
    public float speed = 5f;
    public float distance;
    public static float lifetime = 0;
    public static float previousLifetime = 1f;
    public static float speedMultiplier = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        fuzzyLogic = FuzzyLogic.Deserialize(fuzzyLogicData.bytes, null);
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseGame.gameIsPaused)
        {
            lifetime = carController.timer;
            fuzzyLogic.evaluate = true;
            distance = Vector3.Distance(transform.position, player.position);

            fuzzyLogic.GetFuzzificationByName("distance").value = distance * 100f;
            fuzzyLogic.GetFuzzificationByName("previous_lifetime").value = previousLifetime;

            speed = fuzzyLogic.Output() * fuzzyLogic.defuzzification.maxValue * speedMultiplier;
            transform.Translate(new Vector3(0f, 0f, -1f) * (speed * Time.deltaTime));
        }
    }
}
