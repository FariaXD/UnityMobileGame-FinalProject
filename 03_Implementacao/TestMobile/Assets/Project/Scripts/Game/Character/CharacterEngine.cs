using UnityEngine;

public interface CharacterEngine {
    /*
        ?Interface
        Containing common methods to be implemented
    */
    public void UpdateStatus();

    public Character ReturnAssociatedCharacter();
}