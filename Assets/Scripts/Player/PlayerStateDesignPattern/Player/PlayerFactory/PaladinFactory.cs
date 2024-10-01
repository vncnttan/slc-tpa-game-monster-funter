public class PaladinFactory : Factory{
    public override PlayerParent generateCharacter(){
        return new Paladin();
    }
}