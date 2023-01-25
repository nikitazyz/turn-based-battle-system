using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoroutineManagement
{
    public class CoroutineService : MonoBehaviour
    {
        public Coroutine BeginInvoke(IEnumerator routine) => StartCoroutine(routine);
        public void EndInvoke(Coroutine coroutine) => StopCoroutine(coroutine);

        public static CoroutineService CreateInstance()
        {
            GameObject gameObject = new GameObject("Coroutine Service");
            return gameObject.AddComponent<CoroutineService>();
        }
    }
}