using System.Runtime.CompilerServices;

namespace Cwiczenia_2_Kontenery;


public enum ContainerType
{
    L,
    G,
    C
}

public class Container
{
    public double loadWeightKg { get; set; }
    public double heightCm { get; set; }
    public double containerWeightKg{ get; set; }
    public double depthInCm{ get; set; }
    public string serialNumber{ get; set; }
    
    public double maxConWeight { get; set; }

    private static int serialCounter = 0;
    protected int SerialCounter => serialCounter;
    
    
    public Container(double loadWeightKg, double heightCm, double containerWeightKg, double depthInCm, double maxConWeight)
    {
        serialCounter++;
        this.loadWeightKg = loadWeightKg;
        this.heightCm = heightCm;
        this.containerWeightKg = containerWeightKg;
        this.depthInCm = depthInCm;
        this.maxConWeight = maxConWeight;
    }

    public void EmptyLoad()
    {
        loadWeightKg = 0;
        Console.WriteLine("Container ID: " + serialNumber +" has been emptied");
    }

    public void LoadLoad(double addLoad)
    {
        if (loadWeightKg + addLoad > maxConWeight)
        {
            throw new Exception("Too much weight!");
        }
        loadWeightKg = loadWeightKg + addLoad;
    }
}