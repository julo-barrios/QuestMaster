using UnityEngine;

public class AdventurerTravelSprite : MonoBehaviour
{
    public float speed = 2f;
    private Vector3 target;
    private System.Action onArrival;
    private bool shouldBeIdle = false;
    private bool destroyOnArrival = false;
    public void Setup(Vector3 destination, System.Action arrivalCallback)
    {
        target = destination;
        onArrival = arrivalCallback;
        shouldBeIdle = false;
    }

    void Update()
    {
        if(shouldBeIdle) return;

        if (Vector3.Distance(transform.position, target) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
        else
        {
            shouldBeIdle = true;
            onArrival?.Invoke();
            if(destroyOnArrival){
                Destroy(gameObject); // Elimina el sprite al llegar
            }
            destroyOnArrival = true;
        }
    }
}
