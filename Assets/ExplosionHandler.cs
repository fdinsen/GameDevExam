using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public IEnumerator DestroyThis(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
