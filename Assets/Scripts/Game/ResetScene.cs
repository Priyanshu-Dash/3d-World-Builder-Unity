using UnityEngine;

public class ResetScene : MonoBehaviour
{
    public void ResetAll()
    {
        if (GameObject.FindWithTag("Player") != null)
        {
            GameObject.FindWithTag("Player").SetActive(false);
        }

        GameObject[] objects = GameObject.FindGameObjectsWithTag("Object");

        if (objects.Length > 0)
        {
            foreach (GameObject obj in objects)
            {
                Destroy(obj);
            }
        }
    }
}
