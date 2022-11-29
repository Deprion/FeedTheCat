public static class Events
{
    public static SimpleEvent<string> Name = new SimpleEvent<string>();
    public static SimpleEvent<float> Time = new SimpleEvent<float>();
    public static SimpleEvent<int> Score = new SimpleEvent<int>();
    public static SimpleEvent<float, float> Energy = new SimpleEvent<float, float>();
    public static SimpleEvent TimeOut = new SimpleEvent();
    public static SimpleEvent Dead = new SimpleEvent();
}
