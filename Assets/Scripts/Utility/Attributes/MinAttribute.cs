using UnityEngine;

public class MinAttribute : PropertyAttribute {

    public readonly float minimum;

    /// <summary>
    /// Makes a float or int variable restricted to be greater than or equal to a specific value.
    /// </summary>
    public MinAttribute(float minimum) {
        this.minimum = minimum;
    }
}
