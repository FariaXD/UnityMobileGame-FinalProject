using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option {
    public string name;
    public string result;
    public Dictionary<string, string> reward = new Dictionary<string, string>();
    public Dictionary<string, string> penalty = new Dictionary<string, string>();

    public Option(string _name, string _result){
        this.name = _name;
        this.result = _result;
    }
}