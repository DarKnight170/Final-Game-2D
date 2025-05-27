using UnityEngine;

public class EnemyLook : MonoBehaviour
{
    public GameObject Player;

    void Update()
    {
        Vector3 direction = Player.transform.position - transform.position;
        if (direction.x >= 0.0f) transform.localScale = new Vector3(.4f, .40f, .50f);
        else transform.localScale = new Vector3(-.40f,.40f,.50f);
}
    }
    
