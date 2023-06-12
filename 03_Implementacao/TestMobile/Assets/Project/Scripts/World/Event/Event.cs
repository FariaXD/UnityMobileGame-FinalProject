using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Event {

    public int id;
    public string name;
    public string prompt;
    public Sprite sprite;
    public List<Option> options = new List<Option>();

    public Event(int _id, string _name, string _prompt, Sprite _sprite){
        this.id = _id;
        this.name = _name;
        this.prompt = _prompt;
        this.sprite = _sprite;
    }
    
}