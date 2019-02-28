using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface StackableInterface
{
    int MaxStacks { get; }
    int StackCount { get; set; }
}
