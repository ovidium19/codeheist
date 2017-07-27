using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//TODO FULLY DIE ON BAD METHOD INSIDE FOR LOOP
//code interpreseter script. I think this needs to be async and not extend ScriptableObject. Should be global, need to add in actualy gettign the text
public class CodeInterpreter : MonoBehaviour
{
    public Text errorText;//should be set usng unity UI
    public ErrorConsole errorConsole; //set using Unity Editor
    public bool isRunning = false;
    //used for error catching. This is BAD but you cant try catch around a coroutine so it should work
    public CodeException error;
    public bool isError = false;
    private Robot robot;
    private Vector3 robotPosition;

    private static string nameMatch = "([a-zA-Z0-9]+)";

    private static string methodRegex = @"^\s*" + @nameMatch + "." + nameMatch + @"\(\)$";

    private static string forRegex = @"^\s*for\s?\((\d+)\)\s?{$";

    private string endBreacketRegex = @"\s*}";

    private Dictionary<string, InteractableGameObject> gameObjectsmap = new Dictionary<string, InteractableGameObject>();

    private string[] lines;
    private int linePointer;

    // Use this for initialization
    void Start () {
        //find all object the code can controll
        InteractableGameObject[] interactableObjects = (InteractableGameObject[]) FindObjectsOfType(typeof(InteractableGameObject));// we may want to add this stuff to a param of runCode
        robot = GameObject.FindObjectOfType<Robot>();
        robotPosition = robot.gameObject.transform.position;
        //add them to a dictioanry by the objectName
        foreach (InteractableGameObject obj in interactableObjects)
        {
            gameObjectsmap.Add(obj.getObjectName(), obj);
        }
    }

    //ENTERY POINT
    public void runCode()
    {
        errorText.text = "";//reset errros
        TextMeshProUGUI textArea = GameObject.FindObjectOfType<TextMeshProUGUI>();
        InteractableGameObject igo = gameObjectsmap["door1"];
        igo.GetComponent<Door>().CloseDoor();
        robot.gameObject.transform.position = robotPosition;
        robot.SetPosition(robotPosition);

        Debug.Log(robot.transform.position);
        StartCoroutine(startCode(textArea.text.Replace("\u200b", "")));
      
        
    }

    //start of coroutine code flow
    private IEnumerator startCode(string code)
    {
        yield return new WaitForSeconds(0.1f);
        if (isRunning)
        {
            logError("Code is running alread");
            yield break;
        }

        errorText.text = "";//reset errors

        isRunning = true;
        isError = false;
        lines = code.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
        linePointer = 0;

        while (linePointer < lines.Length)
        {
            yield return StartCoroutine(doLine(lines[linePointer]));
            if (isError)
            {
                break;//if there is an error kill it
            }
        }
        isRunning = false;
    }

    //returns a boolean 
    IEnumerator doLine(string line)
    {       //match the format object.method()
        if (Regex.Match(line, methodRegex).Success){//TODO add method params

            CoroutineWithData cd = new CoroutineWithData(this, doMethodExicution(line));
            yield return cd.coroutine;
            if (isError)
            {
                yield break;
            }
            //cd.result is a bool for the return type if needed in furture

            linePointer++;
        }
        //match if statment
        else if (Regex.Match(line, forRegex).Success)
        {
            //first get times we will be lopping
            Match match = Regex.Match(line, forRegex);
            string timesLoopingString = match.Groups[1].Value;
            int timesLooping = 0;

            try
            {
                timesLooping = Int32.Parse(timesLoopingString);
            
            } catch (FormatException e)
            {
                string error = "attempting to loop but timesLoopingString is not a number at line: " + line;
                stopExicution(error);//kill it
                yield break;
            }


            linePointer++;//get set to fisrt line to be exicuated
            int startingPointer = linePointer;
            //now do the looping
            for (int i = 0; i < timesLooping; i++)
            {
                linePointer = startingPointer;//reset current line
            
                while (!Regex.Match(lines[linePointer], endBreacketRegex).Success)
                {
                    //while we are not at the end of the loop
                    yield return StartCoroutine(doLine(lines[linePointer]));//recursivly process the next line
                    if (isError)
                    {
                        yield break;//kill self if doLine resulted in an error 
                    }
                }
            }
            linePointer++;//current line is for the endbracket for the loop just complted so go to next line
        }//TODO add if statments and veriables
        else
        {
            stopExicution("Command does not appear to be in a recognised format. At line:" + line);
            yield break;
        }
    }

    IEnumerator doMethodExicution(string line)
    {
        Match match = Regex.Match(line, methodRegex);
        string classname = match.Groups[1].Value;
        string methodName = match.Groups[2].Value;
        InteractableGameObject obj = null;
        try
        {
            obj = gameObjectsmap[classname];
        }catch(KeyNotFoundException e)
        {
            //object does not exists
            string error = "You have refrenced an obejct that does not exists at: " + line;
            stopExicution(error);//kill it
            yield break;
        }


        //check if we need to be next to something for it to work
        if (obj.needsToBeNextToGameObjectName() != null)
        {
            Robot robot = (Robot)gameObjectsmap["robot"];//we alraedy have a refrnce to the robot so use it again
            if (robot.isNextToGameObjectName(obj.needsToBeNextToGameObjectName()))
            {
                string error = "The following line requires that you are next to " + obj.needsToBeNextToGameObjectName() + " at line: "+ line;
                stopExicution(error);//kill it
                yield break;
            }
        }
        // we eather dont need to be next to something or are next to it so continue
        //this should block untill it is done after witch we done
        CoroutineWithData cd = new CoroutineWithData(this, obj.executeCommand(methodName));
        yield return cd.coroutine;
        MethodExecutionResult result = (MethodExecutionResult) cd.result;

        //check method compelted
        if (result.sucess)
        {
            yield return result.resut;
        }
        else
        {
            string error = "error : " + result.error + " at line" + line;
            stopExicution(error);//kill it
            yield break;
        }
    }

    void logError(string error)
    {
        if(errorText.text.Length < 0)
        {
            errorText.text = "ERROR:\n";
        }
        errorText.text += error + "\n";
        errorConsole.ShowConsole(errorText.text);
    }

    void stopExicution(string error)
    {
        logError(error);
        this.error = new CodeException(error);
        isError = true;
    }

}
