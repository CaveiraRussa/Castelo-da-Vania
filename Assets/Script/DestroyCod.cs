using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCod : MonoBehaviour {
    [SerializeField]
    private Animator vasoAnim;
    
    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("heroiAtaca"))
            vasoAnim.Play("Dano");
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    } 
  
        
    
}
