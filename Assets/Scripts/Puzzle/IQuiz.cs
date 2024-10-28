using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IQuiz
{
    event Action OnQuizClear;
    void startQuiz();
}
