using UnityEngine;

public class AdventurerTravelSprite : MonoBehaviour
{
    public float speed = 2f;
    private Vector3 guildPOint;
    private Vector3 questPoint;
    private Vector3 target;
    private System.Action onArrival;
    private bool shouldBeIdle = false;
    private bool destroyOnArrival = false;
    public void Setup(Vector3 guildPOint, Vector3 destination)
    {
        this.guildPOint = guildPOint;
        questPoint = destination;
    }

    public void HeadToQuest(System.Action arrivalCallback)
    {
        target = questPoint;
        onArrival = arrivalCallback;
        shouldBeIdle = false;
    }

    public void HeadToGuild(System.Action arrivalCallback)
    {
        target = guildPOint;
        onArrival = arrivalCallback;
        shouldBeIdle = false;
        destroyOnArrival = true; // Destruir al llegar al gremio
    }

    void Update()
    {
        if(shouldBeIdle) return;

        if (Vector3.Distance(transform.position, target) > 0.1f)
        {
            if(target.x -transform.position.x > 0)
                transform.localScale = new Vector3(-1,1,1);
            else
                transform.localScale = new Vector3(1,1,1);
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            
        }
        else
        {
            shouldBeIdle = true;
            onArrival?.Invoke();
            if (destroyOnArrival)
            {
                Destroy(gameObject); // Elimina el sprite al llegar
            }
        }
    }
}
