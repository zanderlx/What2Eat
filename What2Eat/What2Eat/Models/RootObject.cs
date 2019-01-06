public class RootObject
{
    public Business[] businesses { get; set; }
    public int total { get; set; }
    public Region region { get; set; }
}

public class Region
{
    public Center center { get; set; }
}

public class Center
{
    public float longitude { get; set; }
    public float latitude { get; set; }
}