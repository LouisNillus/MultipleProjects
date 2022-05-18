using UnityEngine;
using System;
using System.Collections;

//Original version of the ConditionalHideAttribute created by Brecht Lecluyse (www.brechtos.com)
//Modified by: -

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property |
    AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
public class ConditionalHideAttribute : PropertyAttribute
{
    public string ConditionalSourceField = "";
    public string ConditionalSourceField2 = "";
    public string[] ConditionalSourceFields = new string[] { };
    public bool[] ConditionalSourceFieldInverseBools = new bool[] { };
    public bool HideInInspector = false;
    public bool Inverse = false;
    public bool UseOrLogic = false;

    public bool InverseCondition1 = false;
    public bool InverseCondition2 = false;

    public Range range;

    // Use this for initialization
    public ConditionalHideAttribute(string conditionalSourceField)
    {
        this.ConditionalSourceField = conditionalSourceField;
        this.HideInInspector = false;
        this.Inverse = false;
    }

    public ConditionalHideAttribute(string conditionalSourceField, bool hideInInspector)
    {
        this.ConditionalSourceField = conditionalSourceField;
        this.HideInInspector = hideInInspector;
        this.Inverse = false;
    }

    public ConditionalHideAttribute(string conditionalSourceField, bool hideInInspector, bool inverse)
    {
        this.ConditionalSourceField = conditionalSourceField;
        this.HideInInspector = hideInInspector;
        this.Inverse = inverse;
    }

    public ConditionalHideAttribute(bool hideInInspector = false)
    {
        this.ConditionalSourceField = "";
        this.HideInInspector = hideInInspector;
        this.Inverse = false;
    }

    public ConditionalHideAttribute(string[] conditionalSourceFields,bool[] conditionalSourceFieldInverseBools, bool hideInInspector, bool inverse)
    {
        this.ConditionalSourceFields = conditionalSourceFields;
        this.ConditionalSourceFieldInverseBools = conditionalSourceFieldInverseBools;
        this.HideInInspector = hideInInspector;
        this.Inverse = inverse;
    }

    public ConditionalHideAttribute(string[] conditionalSourceFields, bool hideInInspector, bool inverse)
    {
        this.ConditionalSourceFields = conditionalSourceFields;        
        this.HideInInspector = hideInInspector;
        this.Inverse = inverse;
    }

    public struct Range
    {
        public float min;
        public float max;

        public Range(float min, float max)
        {
            this.min = min;
            this.max = max;
        }
    }

    public ConditionalHideAttribute(string conditionalSourceField = "", bool hideInInspector = false, bool inverse = false, float min = 0, float max = 0)
    {
        this.ConditionalSourceField = conditionalSourceField;
        this.HideInInspector = hideInInspector;
        this.Inverse = inverse;
        this.range = new Range(min, max);
    }
}



