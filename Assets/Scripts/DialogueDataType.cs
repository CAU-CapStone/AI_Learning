using System.Collections.Generic;

[System.Serializable]
public class Dialogue
{
    public int id;
    public string speaker;
    public string text;
}

[System.Serializable]
public class DialogueList
{
    public List<Dialogue> dialogues;
}