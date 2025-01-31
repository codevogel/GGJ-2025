using UnityEngine;

public class BubbleFloat : MonoBehaviour
{
   public float floatSpeed = 1.0f; 
   public float yMove = 0.5f; 
   public float speed = 0.5f; 
   public float xMove = 0.2f;
   public float zMove = 0.2f;

   private Vector3 startPosition;
   private float timeOffset;

   void Start()
   {
      startPosition = transform.position;
      timeOffset = Random.Range(0f, 2f * Mathf.PI); // Willekeurige offset om bubbels uniek te maken
   }

   void Update()
   {
      float yOffset = Mathf.Sin(Time.time * floatSpeed + timeOffset) * yMove;
      float xOffset = Mathf.Cos(Time.time * speed + timeOffset) * xMove;
      float zOffset = Mathf.Cos(Time.time * speed + timeOffset) * zMove;

      transform.position = startPosition + new Vector3(xOffset, yOffset, zOffset);
   }
}