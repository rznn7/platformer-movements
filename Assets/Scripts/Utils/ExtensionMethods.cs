public static class ExtensionMethods
{
    public static int NormalizeFloat(this float value, float threshold = 0f, int normalizedValue = 1)
    {
        if (value > threshold) return normalizedValue;
        if (value < -threshold) return -normalizedValue;
        return 0;
    }
}