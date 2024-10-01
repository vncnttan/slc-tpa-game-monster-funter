public class WizardFactory : Factory{
    public override PlayerParent generateCharacter(){
        return new Wizard();
    }
}