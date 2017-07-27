
using UnityEngine;


public class MethodExecutionResult
{
    public bool sucess;
    public bool resut;
    public string error;

    public MethodExecutionResult()
    {
        sucess = true;
    }

    public MethodExecutionResult(bool isSucessful, bool methodResult)
    {
        sucess = isSucessful;
        resut = methodResult;
    }

    public MethodExecutionResult(bool isSucessful, string errorMessage)
    {
        sucess = isSucessful;
        error = errorMessage;
    }


}