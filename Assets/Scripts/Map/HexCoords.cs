using System.Collections.Generic;

public struct HexCoords
{
    public int Q; // eje horizontal (columna)
    public int R; // eje diagonal (fila)

    public HexCoords(int q, int r)
    {
        Q = q;
        R = r;
    }

    public IEnumerable<HexCoords> GetNeighbors()
    {
        yield return new HexCoords(Q + 1, R);     // E
        yield return new HexCoords(Q - 1, R);     // W
        yield return new HexCoords(Q, R + 1);     // SE
        yield return new HexCoords(Q, R - 1);     // NW
        yield return new HexCoords(Q + 1, R - 1); // NE
        yield return new HexCoords(Q - 1, R + 1); // SW
    }
}
