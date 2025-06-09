public class Aritmetica
{

    public int Suma(int a ,int b)
    {
        return a + b;
    }

    public void PruebaSumaPositivos()
    {
        //AAA  Arrange -> Act -> Assert
        var aritmetica = new Aritmetica();
        //caso basico -> 1 + 2 = 3
        Assert.That(aritmetica.Suma(1, 2), Is.EqualTo(3));
    }
    [Test]
    public void PruebaSumaNegativa()
    {
        var aritmetica = new Aritmetica();
        Assert.That(aritmetica.Suma(5, -10), Is.EqualTo(0));
    }
    [Test]
    public void PruebaResta()
    {
        var aritmetica = new Aritmetica();
        Assert.That(aritmetica.Suma(5, -1), Is.EqualTo(0));
    }
}