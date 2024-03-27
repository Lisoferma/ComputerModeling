using System.Drawing;

namespace Explosion;

public struct Particle
{
    public int X;
    public int Y;
}


public class Explosion
{
    public int MaxX { get; set; }

    public int MaxY { get; set; }

    public int MaxSpeed { get; set; }

    public int ParticleCount { get => _particles.Length; }

    public Color ParticleColor { get; set; }

    public Color BackgroungColor {  get; set; }

    private Particle[] _particles;

    private Color[,] _image;

    private static readonly Random _random = new();


    public Explosion(int maxX, int maxY, int particleCount, int maxSpeed, Color particleColor, Color backgroungColor)
    {
        MaxX = maxX - 1;
        MaxY = maxY - 1;
        MaxSpeed = maxSpeed;
        ParticleColor = particleColor;
        BackgroungColor = backgroungColor;
        _particles = new Particle[particleCount];
        _image = new Color[maxY, maxX];

        SetParticlesOnCenter();
    }


    public void Update()
    {
        int addX, addY, newX, newY;

        for (int i = 0; i < _particles.Length; ++i)
        {
            addX = _random.Next(-MaxSpeed, MaxSpeed + 1);
            addY = _random.Next(-MaxSpeed, MaxSpeed + 1);

            newX = _particles[i].X + addX;
            newY = _particles[i].Y + addY;

            if (newX > MaxX)
                newX = MaxX;
            else if (newX < 0)
                newX = 0;

            if (newY > MaxY)
                newY = MaxY;
            else if (newY < 0)
                newY = 0;

            _particles[i].X = newX;
            _particles[i].Y = newY;
        }
    }


    public Color[,] GetImage()
    {
        ClearImage();

        foreach (var particle in _particles)
            _image[particle.Y, particle.X] = ParticleColor;

        return _image;
    }


    private void ClearImage()
    {
        int rows = _image.GetLength(0);
        int cols = _image.GetLength(1);     

        for (int i = 0; i < rows; ++i)
            for (int j = 0; j < cols; ++j)
                _image[i, j] = BackgroungColor;
    }


    public void SetParticlesOnCenter()
    {
        Particle centerParticle = new()
        { 
            X = (int)Math.Round((double)MaxX / 2),
            Y = (int)Math.Round((double)MaxY / 2)
        };

        for (int i = 0; i < _particles.Length; ++i)
        {
            _particles[i] = centerParticle;
        } 
    }
}
