[System.Serializable]
public class GridEncoded
{
    public string ip = "";
    public string timeCode = "";
    public int gridCount = 0;
    public int[] colors;

    public GridEncoded(int gridCount, string ip, string timeCode)
    {
        this.gridCount = gridCount;
        this.ip = ip;
        this.timeCode = timeCode;
    }
}
