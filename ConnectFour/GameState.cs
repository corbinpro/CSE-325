namespace ConnectFour;

public class GameState
{
    static GameState()
    {
        CalculateWinningPlaces();
    }

    public enum WinState
    {
        No_Winner = 0,
        Player1_Wins = 1,
        Player2_Wins = 2,
        Tie = 3
    }

    public int PlayerTurn => TheBoard.Count(x => x != 0) % 2 + 1;

    public int CurrentTurn => TheBoard.Count(x => x != 0);

    public static readonly List<int[]> WinningPlaces = new();

    public static void CalculateWinningPlaces()
    {
        for (byte row = 0; row < 6; row++)
        {
            byte rowCol1 = (byte)(row * 7);
            byte rowColEnd = (byte)((row + 1) * 7 - 1);
            byte checkCol = rowCol1;
            while (checkCol <= rowColEnd - 3)
            {
                WinningPlaces.Add(new int[] { checkCol, checkCol + 1, checkCol + 2, checkCol + 3 });
                checkCol++;
            }
        }

        for (byte col = 0; col < 7; col++)
        {
            byte checkRow = col;
            while (checkRow <= 14 + col)
            {
                WinningPlaces.Add(new int[] { checkRow, checkRow + 7, checkRow + 14, checkRow + 21 });
                checkRow += 7;
            }
        }

        for (byte col = 0; col < 4; col++)
        {
            byte checkPos = (byte)(21 + col);
            while (checkPos <= 35 + col)
            {
                WinningPlaces.Add(new int[] { checkPos, checkPos - 6, checkPos - 12, checkPos - 18 });
                checkPos += 7;
            }
        }

        for (byte col = 0; col < 4; col++)
        {
            byte checkPos = col;
            while (checkPos <= 14 + col)
            {
                WinningPlaces.Add(new int[] { checkPos, checkPos + 8, checkPos + 16, checkPos + 24 });
                checkPos += 7;
            }
        }
    }

    public WinState CheckForWin()
    {
        if (TheBoard.Count(x => x != 0) < 7)
        {
            return WinState.No_Winner;
        }

        foreach (var scenario in WinningPlaces)
        {
            if (TheBoard[scenario[0]] != 0 &&
                TheBoard[scenario[0]] == TheBoard[scenario[1]] &&
                TheBoard[scenario[1]] == TheBoard[scenario[2]] &&
                TheBoard[scenario[2]] == TheBoard[scenario[3]])
            {
                return (WinState)TheBoard[scenario[0]];
            }
        }

        return TheBoard.Count(x => x != 0) == 42 ? WinState.Tie : WinState.No_Winner;
    }

    public byte PlayPiece(int column)
    {
        if (CheckForWin() != WinState.No_Winner)
        {
            throw new ArgumentException("Game is over");
        }

        if (column < 0 || column >= 7 || TheBoard[column] != 0)
        {
            throw new ArgumentException("Column is full");
        }

        var landingSpot = column;
        while (landingSpot + 7 < TheBoard.Count && TheBoard[landingSpot + 7] == 0)
        {
            landingSpot += 7;
        }

        TheBoard[landingSpot] = PlayerTurn;
        return ConvertLandingSpotToRow(landingSpot);
    }

    public List<int> TheBoard { get; private set; } = new(new int[42]);

    public void ResetBoard()
    {
        TheBoard = new List<int>(new int[42]);
    }

    private static byte ConvertLandingSpotToRow(int landingSpot)
    {
        return (byte)(Math.Floor(landingSpot / (decimal)7) + 1);
    }
}
