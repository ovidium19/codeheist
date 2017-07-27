using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeException : Exception
{
    public string errorMessage;

    public CodeException(string error)
    {
        errorMessage = error;
    }
}
