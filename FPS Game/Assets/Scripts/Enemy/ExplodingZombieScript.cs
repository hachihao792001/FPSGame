using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingZombieScript : MonoBehaviour
{
    public GameObject Target;

    public bool dead = false, hitByExplosion = false;
	
    public GameObject SmallZombie, zombieExplosion;

    public int fullHealth, currentHealth;
    public string attackAni, walkAni;

    public Transform[] bodyParts;
    Transform PlayerBody;
    public float moveSpeed;

    public float attackDelay;

    public Transform HealthPivot;

    public float attackRange;
    public float money;
    Animation ani;

    // Start is called before the first frame update
    void Start()
    {
        PlayerBody = FindObjectOfType<FPSController>().transform.parent;
        ani = GetComponent<Animation>();
        currentHealth = fullHealth;
    }

    void CreatePopupText(Vector3 pos, string t, Color c)
    {
        GameObject popupText = Instantiate(FindObjectOfType<GameManager>().PopupText, pos, Quaternion.identity);
        popupText.SendMessage("SetText", t);
        popupText.SendMessage("SetColor", c);
    }

    public void WasAttacked(int damage)
    {
        if (currentHealth <= 0) return;
        currentHealth -= damage;
        CreatePopupText(transform.position, "-" + damage.ToString(), Color.red);

        if (currentHealth <= 0)
        {
            Die();
            return;
        }

        float healthScale = (float)currentHealth / fullHealth;
        HealthPivot.localScale = new Vector3(healthScale, 1, 1);

        for (int i = 0; i < bodyParts.Length; i++)
        {
            bodyParts[i].GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
            if (currentHealth > 2)
                StartCoroutine(SetBackToWhite(bodyParts[i].GetComponent<MeshRenderer>().material));
        }
		
		hitByExplosion = false;
    }
	
	public void SetHitByExplosion(bool hbe){
		hitByExplosion = hbe;
	}

    IEnumerator SetBackToWhite(Material m)
    {
        yield return new WaitForSeconds(0.1f);
        if (!dead)
            m.SetColor("_Color", Color.white);
    }

    void Die()
    {
        dead = true;
        Destroy(HealthPivot.parent.gameObject);
        ani.Stop();
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        transform.Find("EnemyHead").GetComponent<Collider>().enabled = false;

        for (int i = 0; i < bodyParts.Length; i++)
        {
            bodyParts[i].transform.parent = transform;
            bodyParts[i].GetComponent<Renderer>().material.color = Color.black;
            bodyParts[i].GetComponent<Collider>().enabled = true;
            bodyParts[i].GetComponent<Rigidbody>().isKinematic = false;
			if(hitByExplosion) 
				bodyParts[i].GetComponent<Rigidbody>().AddExplosionForce(700f, transform.position + Vector3.down, 5);
        }

        PlayerBody.SendMessage("GainMoney", money);
        StartCoroutine(Explode(detectTarget(attackRange), attackDelay));
        GetComponent<ExplodingZombieScript>().enabled = false;
    }

    GameObject detectTarget(float range)
    {
        Collider[] collides = Physics.OverlapBox(transform.position, Vector3.one * range);
        foreach (Collider c in collides)
        {
            if (c.gameObject == Target)
                return c.gameObject;
        }

        return null;
    }

    void Update()
    {
        if (!GameManager.playing) return;

        if (Target == null)
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag("PlayerSide");
            GameObject closest = targets[0];
            foreach (GameObject t in targets)
            {
                if (Vector3.Distance(transform.position, t.transform.position) < Vector3.Distance(transform.position, closest.transform.position))
                    closest = t;
            }
            Target = closest.transform.parent.gameObject;
        }

        Vector3 look = Target.transform.position - transform.position;
        look.y = 0;
        transform.rotation = Quaternion.LookRotation(look);

        GameObject playerNear = detectTarget(attackRange);

        if (!ani.IsPlaying(attackAni))
        {
            if (playerNear != null)
            {
                ani.Stop(walkAni);
                ani.Play(attackAni);
                GetComponent<Rigidbody>().velocity = new Vector3(0, GetComponent<Rigidbody>().velocity.y, 0);
                transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, moveSpeed * 2 * Time.deltaTime);
                StartCoroutine(Explode(playerNear, attackDelay));
            }
            else
            {
                GetComponent<Rigidbody>().velocity = new Vector3(0, GetComponent<Rigidbody>().velocity.y, 0);
                transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, moveSpeed * Time.deltaTime);
                if (!ani.IsPlaying(walkAni)) ani.Play(walkAni);
            }
        }
        else GetComponent<Rigidbody>().velocity = new Vector3(0, GetComponent<Rigidbody>().velocity.y, 0);

    }

    IEnumerator Explode(GameObject g, float sec)
    {
        yield return new WaitForSeconds(sec);

        Instantiate(zombieExplosion, transform.position+Vector3.down, Quaternion.identity);

        Instantiate(SmallZombie, transform.position+Vector3.up*2, Quaternion.identity);
        Instantiate(SmallZombie, transform.position + Vector3.up * 2, Quaternion.identity);
        Instantiate(SmallZombie, transform.position + Vector3.up * 2, Quaternion.identity);

        Destroy(gameObject);
    }
}
