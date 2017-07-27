using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Robot : InteractableGameObject {
    public float speed;
    public AnimationClip jumpAnimation;
    
    private Vector3 targetPosition;
    private Vector3 currentPosition;
    private Animator anim;
    private ParticleSystem parsys;
    private enum Direction { Left, Right, Up, Down };
    private Direction dir=Direction.Right;
    
    private List<GameObject> objectsAvailable;//contains a list of GO's that the robot is next to
	// Use this for initialization
	void Start () {
        currentPosition = transform.position;
        targetPosition = currentPosition;
        anim = GetComponent<Animator>();
        parsys = GetComponent<ParticleSystem>();
        objectsAvailable = new List<GameObject>();
       
        
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(transform.position.x==targetPosition.x && transform.position.y==targetPosition.y);
        
        
		if (currentPosition != targetPosition)
        {
            anim.SetBool("moveBool", true);
            
            switch (dir)
            {
                case Direction.Right:
                    {
                        
                        transform.Translate(Vector3.right * speed * Time.deltaTime);
                        currentPosition = transform.position;
                        if (currentPosition.x >= targetPosition.x)
                        {
                            currentPosition = targetPosition;
                        }
                        
                        break;
                    }
                case Direction.Left:
                    transform.Translate(Vector3.left * speed * Time.deltaTime);
                    currentPosition = transform.position;
                    if (currentPosition.x <= targetPosition.x)
                    {
                        currentPosition = targetPosition;
                    }

                    break;
                case Direction.Down:
                    transform.Translate(Vector3.down * speed * Time.deltaTime);
                    currentPosition = transform.position;
                    if (currentPosition.y <= targetPosition.y)
                    {
                        currentPosition = targetPosition;
                    }
                    break;
                case Direction.Up:
                    transform.Translate(Vector3.up * speed * Time.deltaTime);
                    currentPosition = transform.position;
                    if (currentPosition.y >= targetPosition.y)
                    {
                        currentPosition = targetPosition;
                    }
                    break;
                default: break;
            }
            
        }
        else 
        {
            anim.SetBool("moveBool", false);
            transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
        }
        
	}
    public void MoveRight()
    {
        targetPosition = transform.position + Vector3.right;
        dir = Direction.Right;
        Debug.Log(targetPosition);
    }
    public void MoveLeft()
    {
        targetPosition = transform.position + Vector3.left;
        dir = Direction.Left;
        Debug.Log(targetPosition);
    }
    public void MoveUp()
    {
        targetPosition = transform.position + Vector3.up;
        dir = Direction.Up;
        Debug.Log(targetPosition);
    }
    public void MoveDown()
    {
        targetPosition = transform.position + Vector3.down;
        dir = Direction.Down;
        Debug.Log(targetPosition);
    }
    public void BlinkParticle()
    {
        parsys.Play();
    }
    public void MovePositionWithJump()
    {
        switch (dir)
        {
            case Direction.Down:
                transform.position += new Vector3(0, -2, 0);
                break;
            case Direction.Up:
                transform.position += new Vector3(0, 2, 0);
                break;
            case Direction.Right:
                transform.position += new Vector3(2, 0, 0);
                break;
            case Direction.Left:
                transform.position += new Vector3(0, 2, 0);
                break;
        }
    }
    private Vector3 Rounded(Vector3 par)
    {
        
        par = new Vector3(Mathf.Round(par.x), (float)decimal.Round((decimal)par.y, 2));
        return par;
    }
    public void AddObject(GameObject g)
    {
        objectsAvailable.Add(g);
        
        foreach (GameObject gO in objectsAvailable)
        {
            Debug.Log(gO.name);
        }
    }

    public void RemoveObject(GameObject go)
    {
        objectsAvailable.Remove(go);
    }

    public override IEnumerator executeCommand(string methodName)
    {
        switch (methodName)
        {
            case "moveright":
                MoveRight();
                break;
            case "moveleft":
                MoveLeft();
                break;
            case "moveup":
                MoveUp();
                break;
            case "movedown":
                MoveDown();
                break;
            case "jump":
                Jump();
                yield return new WaitForSeconds(jumpAnimation.length+0.1f);
                break;
            case "opendoor":
                OpenDoor();
                break;
            default:
                yield return new MethodExecutionResult(false, "Method not found: " + methodName);
                yield break;
        }

        yield return new WaitForSeconds(0.1F);

        while (anim.GetBool("moveBool"))
        {
            yield return new WaitForSeconds(0.1F);
        }

        yield return new MethodExecutionResult();
    }

    public bool isNextToGameObjectName(string gameObjectName)
    {
        foreach(GameObject go in objectsAvailable)
        {
            if (go.name == gameObjectName)
            {
                return true;
            }
        }
        return false;
    }

    public override string needsToBeNextToGameObjectName()
    {
        return null;//should always be accessable
    }

    public override string getObjectName()
    {
        return "robot";
    }
    public void Jump()
    {
        anim.SetTrigger("jumpTrigger");
    }
    public bool OpenDoor()
    {
        foreach (GameObject g in objectsAvailable)
        {
            if (g.GetComponent<Door>())
            {
                Door door = g.GetComponent<Door>();
                door.OpenDoor();
                return true;
            }
        }
        return false;
    }
    public void SetPosition(Vector3 pos)
    {
        currentPosition = pos;
    }

}
