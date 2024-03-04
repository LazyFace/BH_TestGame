[System.Serializable]
public struct AnimationData
{
    public string animationName;
    public bool isLoop;

    public AnimationData(string animationName, bool isLoop)
    {
        this.animationName = animationName;
        this.isLoop = isLoop;
    }
}

