using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RollerAnimationer : MonoBehaviour
{
    public Animator animator;
    public GameObject player;
    public Seeker seeker;
    public AIPath AIPath;
    public AIDestinationSetter destination;
    private bool seenPlayer;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Vector3.Distance(player.transform.position, gameObject.transform.position));
    }

    // Update is called once per frame
    void Update()
    {
        if ((Vector3.Distance(player.transform.position, gameObject.transform.position) < 3) && !seenPlayer)
        {
            animator.Play("SpotPlayer");
            seenPlayer = true;
        }
    }

    public void EnableAI()
    {
        seeker.enabled = true;
        AIPath.enabled = true;
        destination.enabled = true;
    }
}
