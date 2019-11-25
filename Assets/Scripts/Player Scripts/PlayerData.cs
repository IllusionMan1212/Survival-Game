using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerData : MonoBehaviour {

    public int counter = 0;
    public int average;
    public System.Collections.Generic.List<int> framerates;
    public float sum;

    // Update is called once per frame
    void Update () {
        if (counter % 60 == 0) {
            print(average + "fps");
            framerates.Clear();
            counter = 0;
        }
        framerates.Add(Mathf.RoundToInt(1 / Time.deltaTime));
        average = Mean(framerates);
        counter++;
        
	}

    int Mean(System.Collections.Generic.List<int> Values) {
        sum = 0;
        for (int x = 0; x < Values.Count; ++x) {
            sum += Values[x];
        }
        return Mathf.RoundToInt(sum / Values.Count);
    }
}
