using UnityEngine;

public class AchievemnetObject : MonoBehaviour
{
    [SerializeField] AchievemtManager ACM;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ACM.Achieved();
            Destroy(gameObject);
        }
    }
   
}
