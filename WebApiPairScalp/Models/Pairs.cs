namespace WebApiPairScalp.Models;

public class Pairs
{
    public int Id { get; set; }
    public string Symbol1 { get; set; }
    public string Symbol2 { get; set; }
    public double Kf1 { get; set; }
    public double Kf2 { get; set; }
    public double Pp { get; set; }
    public double Pv { get; set; }
    public DateTime UpData { get; set; }
}

