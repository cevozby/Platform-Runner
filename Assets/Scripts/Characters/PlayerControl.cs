
public class PlayerControl : Character
{
    


    public override void Finish()
    {
        base.Finish();
        GameControl.instance.isGame = false;
        GameControl.instance.isEnd = true;
    }

}
