using System;

public class StringPropertyChangedEventArgs : EventArgs
{
    public string Name;
    public string Data;

    public StringPropertyChangedEventArgs(string name, string data)
    {
        Name = name;
        Data = data;
    }
}

public class FlagPropertyChangedEventArgs : EventArgs
{
    public string Name;
    public int Data;

    public FlagPropertyChangedEventArgs(string name, int data)
    {
        Name = name;
        Data = data;
    }
}

public class Int64PropertyChangedEventArgs : EventArgs
{
    public string Name;
    public long Data;

    public Int64PropertyChangedEventArgs(string name, long data)
    {
        Name = name;
        Data = data;
    }
}

public class DoublePropertyChangedEventArgs : EventArgs
{
    public string Name;
    public double Data;

    public DoublePropertyChangedEventArgs(string name, double data)
    {
        Name = name;
        Data = data;
    }
}