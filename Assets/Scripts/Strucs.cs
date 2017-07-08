using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum GameResult
{
    Win,
    Loss,
    None
};

public enum Affiliation
{
    Player,
    NonPlayer
};

public enum PossibleTarget
{
    Friendly,
    Hostile
};

public enum TargetOffset
{
    Belly,
    Head
};

public enum Facing
{
    Up = 0,
    Right = 1,
    Down = 2,
    Left = 3
}

