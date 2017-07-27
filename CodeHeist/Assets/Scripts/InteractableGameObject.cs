using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//all objects that can be controlled in code should override this.
//Such that they can be used with the CodeInterpreter script
public abstract class InteractableGameObject : MonoBehaviour {

    public abstract string getObjectName();//make sure this is over rider this will be the object in the programing language

    public abstract string needsToBeNextToGameObjectName();//if the class required you are next to a block with script then  

    abstract public IEnumerator executeCommand(string methodName);
}
