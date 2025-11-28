using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timebeforegone : MonoBehaviour
{
    // Start is called before the first frame update
    public IEnumerator Explosiontime()
    {
    yield return new WaitForSecondsRealtime(0.4f);
    Destroy(gameObject);
    }
}
