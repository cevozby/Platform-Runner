
public class PlayerControl : Character
{
    

    //When player arrive the finish line, change the boolean
    public override void Finish()
    {
        base.Finish();
        GameControl.instance.isGame = false;
        GameControl.instance.isEnd = true;
    }

}
