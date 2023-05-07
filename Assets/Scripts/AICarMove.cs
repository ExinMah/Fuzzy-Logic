using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FuzzyLogicSystem;
using JetBrains.Annotations;
using Unity.VisualScripting;

public class AICarMove : MonoBehaviour
{
    public Transform player;
    public TextAsset fuzzyLogicData;
    private FuzzyLogic fuzzyLogic = null;
    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        fuzzyLogic = FuzzyLogic.Deserialize(fuzzyLogicData.bytes, null);
    }

    // Update is called once per frame
    void Update()
    {
        fuzzyLogic.evaluate = true;
        fuzzyLogic.GetFuzzificationByName("distance").value = Vector3.Distance(transform.position, player.position);
        fuzzyLogic.GetFuzzificationByName("previous_lifetime").value = Time.deltaTime;

        speed = fuzzyLogic.Output() * fuzzyLogic.defuzzification.maxValue * 0.1f;
        transform.Translate(new Vector3(0f, 0f, -1f) * (speed * Time.deltaTime));
    }
}
