﻿using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class Event
{
    public int id;
    public string name;
    public string prompt;
    public EventOption option1;
    public EventOption option2;
    public EventOption option3;
    public string sprite;
    public bool completed = false;
}

public class EventOption
{
    public string name;
    public EventReward reward;
    public EventPenalty penalty;
    public string result;
}

public class EventReward
{
    public string item;
    public string value;
}

public class EventPenalty
{
    public string item;
    public int value;
}

public class StageEventInitializer
{
    public static List<Event> InitializeEvent(string worldName)
    {
        TextAsset mytxtData = Resources.Load<TextAsset>("data/worlds/" + worldName + "/events");
        string txt = mytxtData.text;
        Dictionary<string, Event> eventData = JsonConvert.DeserializeObject<Dictionary<string, Event>>(txt);

        List<Event> events = new List<Event>(eventData.Values);
        return events;
    }
}